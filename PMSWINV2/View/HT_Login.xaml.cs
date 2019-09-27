﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using System.Threading;
using System.ComponentModel;
using System.ComponentModel.Design;
using MTPMSWIN.Model;
using System.Data.SqlClient;
using System.Data;

namespace MTPMSWIN.View
{
    
    public partial class HT_Login : Window
    {
        SqlDataAdapter SQLAdaptor = null;
        DataSet DSetMain = null;

        public BackgroundWorker bw = new BackgroundWorker();

        private Thickness Pos_Def,Pos_New;
        private bool Pos_Expend = false;
        public HT_Login()
        {
            InitializeComponent();

            bw.WorkerReportsProgress = true;
            bw.WorkerSupportsCancellation = true;
            bw.DoWork += new DoWorkEventHandler(bw_DoWork);
            bw.ProgressChanged += new ProgressChangedEventHandler(bw_ProgressChanged);
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);
        }
       

      #region "EVENT"
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Pos_Def = new Thickness(txtPassWord.Margin.Left, txtPassWord.Margin.Top, txtPassWord.Margin.Right, txtPassWord.Margin.Bottom);
            Pos_New = new Thickness(txtUserName.Margin.Left, txtUserName.Margin.Top, txtUserName.Margin.Right, txtUserName.Margin.Bottom);
            
            string mUsername = MTGlobal.MT_USER_LOGIN;
            string mPassword =MTGlobal.MT_REMEMBER==true? MTGlobal.MT_USER_PASS:"";

            if (MTGlobal.MT_REMEMBER) { chkRemember.IsChecked = true; }

            if (mUsername != "")
            {
                txtUserName.Text = mUsername.Trim().ToUpper();
                txtPassWord.Password = mPassword;
                lblAccount.Content = "Welcome " + mUsername.Trim().ToUpper();
                lblUsername.Visibility = Visibility.Hidden;
                txtUserName.Visibility = Visibility.Hidden;
                SetPosControl(false);
            }
            else
            {
                txtUserName.Text = "";
                txtUserName.Focus();
                lblUsername.Visibility = Visibility.Visible;
                txtUserName.Visibility = Visibility.Visible;
            }
        }

        private void lblAccount_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            lblUsername.Visibility = Visibility.Visible;
            txtUserName.Visibility = Visibility.Visible;
            SetPosControl(!Pos_Expend);
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            DevExpress.Utils.WaitDialogForm Dlg = new DevExpress.Utils.WaitDialogForm("Đang nạp dữ liệu...", "QUẢN LÝ KHO HÀNG");
            Dlg.Show();

            try
            {
                String userName = txtUserName.Text.ToUpper().Trim();
                string pass = txtPassWord.Password;
                lblMSG.Visibility = Visibility.Hidden;
                lblLoading.Visibility = Visibility.Visible;
                Mouse.OverrideCursor = Cursors.Wait;
                txtUserName.IsEnabled = false;
                txtPassWord.IsEnabled = false;
                btnLogin.IsEnabled = false;

                string mLoginRs = new modMember().fValidMember(userName, pass);
                if (mLoginRs == "T")
                {
                    MTGlobal.SetConfigKey("MT_ACCOUNT", userName);
                    if (chkRemember.IsChecked == true)
                    {
                        MTGlobal.SetConfigKey("MT_PASSWORD", txtPassWord.Password.ToString());
                        MTGlobal.SetConfigKey("MT_REMEMBER", "1");
                    }
                    else
                    {
                        MTGlobal.SetConfigKey("MT_PASSWORD", "");
                        MTGlobal.SetConfigKey("MT_REMEMBER", "0");
                    }
                    if (bw.IsBusy == false)
                    {
                        bw.RunWorkerAsync();
                    }


                    Thread vThread = new Thread(() =>{
                        try{
                            MTSynData.doSyncCheckActive();
                            MTGlobal.onSetPara();
                        }
                        catch { }
                    });
                    vThread.Start();                          
                }
                else
                {
                    lblLoading.Visibility = Visibility.Hidden;
                    lblMSG.Visibility = Visibility.Visible;
                    lblMSG.Content = mLoginRs;
                    txtUserName.IsEnabled = true;
                    txtUserName.BorderBrush = Brushes.Red;
                    txtPassWord.IsEnabled = true;
                    txtPassWord.BorderBrush = Brushes.Red;
                    btnLogin.IsEnabled = true;
                    Mouse.OverrideCursor = null;
                    if (Pos_Expend) { txtUserName.Focus(); txtUserName.SelectAll(); }
                    else { txtPassWord.Focus(); txtPassWord.SelectAll(); }
                }
            }
            catch (Exception ex) {}

            if (Dlg != null) { Dlg.Close(); }
        }

        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            Thread.Sleep(200);
        }

        private void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if ((e.Cancelled == true))
            {
                lblMSG.Visibility = Visibility.Visible;
                lblMSG.Content = "Canceled!";
            }

            else if (!(e.Error == null))
            {
                lblMSG.Visibility = Visibility.Visible;
                lblMSG.Content = "Error: " + e.Error.Message;
            }
            else
            {
                lblLoading.Visibility = Visibility.Hidden;
                lblMSG.Visibility = Visibility.Visible;
                lblMSG.Content = "LOGIN SUCCESSFUL";
                Mouse.OverrideCursor = null;
                txtUserName.IsEnabled = true;
                txtPassWord.IsEnabled = true;
                btnLogin.IsEnabled = true;

                //MTSync oM = new MTSync();
                MTMain oM = new MTMain();
                this.Close();
                oM.Show();
            }
        }

        private void bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }

        private void Window_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void txtUserName_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtUserName.BorderBrush = Brushes.White;
        }

        private void txtPassWord_PasswordChanged(object sender, RoutedEventArgs e)
        {
            txtPassWord.BorderBrush = Brushes.White;
        }
        private void txtUserName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (txtUserName.Text.Trim() == "") { txtUserName.Focus(); lblMSG.Content = "Bạn chưa nhập tài khoản đăng nhập !"; }
                else
                {
                    txtPassWord.Focus();
                }
            }
        }

        private void txtPassWord_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (txtPassWord.Password.Trim() == "") { txtPassWord.Focus(); lblMSG.Content = "Bạn chưa nhập mật khẩu đăng nhập !"; }
                else
                {
                    btnLogin.Focus();
                }
            }
        }
      #endregion


     #region "FUNCTION"
        private void SetPosControl(bool isExpand) {
            try
            {
                Pos_Expend = isExpand;
                if (isExpand)
                {
                    txtPassWord.Margin = Pos_Def;
                    lblPassword.Margin = new Thickness(lblPassword.Margin.Left, lblPassword.Margin.Top + 74, 0, 0);
                    btnLogin.Margin = new Thickness(btnLogin.Margin.Left, btnLogin.Margin.Top + 74, 0, 0);

                    lblUsername.Visibility = Visibility.Visible;
                    txtUserName.Visibility = Visibility.Visible;
                    txtUserName.Focus();
                }
                else {
                    txtPassWord.Margin = Pos_New;
                    lblPassword.Margin = new Thickness(lblPassword.Margin.Left,lblPassword.Margin.Top -74,0,0);
                    btnLogin.Margin = new Thickness(btnLogin.Margin.Left,btnLogin.Margin.Top-74, 0, 0);
                    lblUsername.Visibility = Visibility.Hidden;
                    txtUserName.Visibility = Visibility.Hidden;
                    txtPassWord.Focus();
                }
            }
            catch { }
        }
     #endregion

         

    }
}
