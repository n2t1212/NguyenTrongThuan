namespace MTPosInMa
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.grdSanpham = new DevExpress.XtraGrid.GridControl();
            this.gvSanpham = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colMaspid = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMasp = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTensp = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDvt = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colQuycach = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMacode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grdSanphamc = new DevExpress.XtraGrid.GridControl();
            this.gvSanphamc = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colMaspidc = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMaspc = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTenspc = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDvtc = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colQuycachc = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMacodeC = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCanin = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDongiac = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cmdExit = new DevExpress.XtraEditors.SimpleButton();
            this.cmdRemoveAll = new DevExpress.XtraEditors.SimpleButton();
            this.cmdRemove = new DevExpress.XtraEditors.SimpleButton();
            this.cmdAddAll = new DevExpress.XtraEditors.SimpleButton();
            this.cmdAdd = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.chkThung = new DevExpress.XtraEditors.CheckEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.cbMautem = new DevExpress.XtraEditors.ComboBoxEdit();
            this.chkQR = new DevExpress.XtraEditors.CheckEdit();
            this.cmdIn = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.spinSoluong = new DevExpress.XtraEditors.SpinEdit();
            this.chkGiaban = new DevExpress.XtraEditors.CheckEdit();
            this.pnlTitleForm = new DevExpress.XtraEditors.PanelControl();
            this.cmdAppMini = new DevExpress.XtraEditors.SimpleButton();
            this.cmdAppExit = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.grdSanpham)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvSanpham)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdSanphamc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvSanphamc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkThung.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbMautem.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkQR.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinSoluong.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkGiaban.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlTitleForm)).BeginInit();
            this.pnlTitleForm.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            this.SuspendLayout();
            // 
            // grdSanpham
            // 
            this.grdSanpham.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.grdSanpham.Cursor = System.Windows.Forms.Cursors.Hand;
            this.grdSanpham.Location = new System.Drawing.Point(1, 42);
            this.grdSanpham.MainView = this.gvSanpham;
            this.grdSanpham.Name = "grdSanpham";
            this.grdSanpham.Size = new System.Drawing.Size(446, 362);
            this.grdSanpham.TabIndex = 2;
            this.grdSanpham.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvSanpham});
            // 
            // gvSanpham
            // 
            this.gvSanpham.ColumnPanelRowHeight = 35;
            this.gvSanpham.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colMaspid,
            this.colMasp,
            this.colTensp,
            this.colDvt,
            this.colQuycach,
            this.colMacode});
            this.gvSanpham.GridControl = this.grdSanpham;
            this.gvSanpham.Name = "gvSanpham";
            this.gvSanpham.OptionsView.ColumnAutoWidth = false;
            this.gvSanpham.OptionsView.ShowGroupPanel = false;
            this.gvSanpham.OptionsView.ShowViewCaption = true;
            this.gvSanpham.RowHeight = 30;
            this.gvSanpham.ViewCaption = "DANH MỤC HÀNG HÓA";
            this.gvSanpham.ViewCaptionHeight = 35;
            // 
            // colMaspid
            // 
            this.colMaspid.Caption = "MASPID";
            this.colMaspid.FieldName = "Maspid";
            this.colMaspid.Name = "colMaspid";
            // 
            // colMasp
            // 
            this.colMasp.AppearanceCell.BackColor = System.Drawing.Color.Honeydew;
            this.colMasp.AppearanceCell.Options.UseBackColor = true;
            this.colMasp.Caption = "Mã Hàng";
            this.colMasp.FieldName = "Masp";
            this.colMasp.Name = "colMasp";
            this.colMasp.Visible = true;
            this.colMasp.VisibleIndex = 0;
            this.colMasp.Width = 88;
            // 
            // colTensp
            // 
            this.colTensp.AppearanceCell.BackColor = System.Drawing.Color.Honeydew;
            this.colTensp.AppearanceCell.Options.UseBackColor = true;
            this.colTensp.Caption = "Tên hàng hóa";
            this.colTensp.FieldName = "Tensp";
            this.colTensp.Name = "colTensp";
            this.colTensp.Visible = true;
            this.colTensp.VisibleIndex = 1;
            this.colTensp.Width = 176;
            // 
            // colDvt
            // 
            this.colDvt.AppearanceCell.BackColor = System.Drawing.Color.Honeydew;
            this.colDvt.AppearanceCell.Options.UseBackColor = true;
            this.colDvt.Caption = "ĐVT";
            this.colDvt.FieldName = "Dvt";
            this.colDvt.Name = "colDvt";
            this.colDvt.Visible = true;
            this.colDvt.VisibleIndex = 2;
            this.colDvt.Width = 69;
            // 
            // colQuycach
            // 
            this.colQuycach.AppearanceCell.BackColor = System.Drawing.Color.Honeydew;
            this.colQuycach.AppearanceCell.Options.UseBackColor = true;
            this.colQuycach.Caption = "Quy cách";
            this.colQuycach.FieldName = "Quycach";
            this.colQuycach.Name = "colQuycach";
            this.colQuycach.Visible = true;
            this.colQuycach.VisibleIndex = 3;
            this.colQuycach.Width = 100;
            // 
            // colMacode
            // 
            this.colMacode.Caption = "Mã Code";
            this.colMacode.FieldName = "Macode";
            this.colMacode.Name = "colMacode";
            // 
            // grdSanphamc
            // 
            this.grdSanphamc.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdSanphamc.Cursor = System.Windows.Forms.Cursors.Hand;
            this.grdSanphamc.Location = new System.Drawing.Point(517, 42);
            this.grdSanphamc.MainView = this.gvSanphamc;
            this.grdSanphamc.Name = "grdSanphamc";
            this.grdSanphamc.Size = new System.Drawing.Size(655, 362);
            this.grdSanphamc.TabIndex = 3;
            this.grdSanphamc.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvSanphamc});
            // 
            // gvSanphamc
            // 
            this.gvSanphamc.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colMaspidc,
            this.colMaspc,
            this.colTenspc,
            this.colDvtc,
            this.colQuycachc,
            this.colMacodeC,
            this.colCanin,
            this.colDongiac});
            this.gvSanphamc.GridControl = this.grdSanphamc;
            this.gvSanphamc.Name = "gvSanphamc";
            this.gvSanphamc.OptionsView.ShowGroupPanel = false;
            this.gvSanphamc.OptionsView.ShowViewCaption = true;
            this.gvSanphamc.ViewCaption = "DANH MỤC HÀNG HÓA CẦN IN";
            // 
            // colMaspidc
            // 
            this.colMaspidc.Caption = "MASPID";
            this.colMaspidc.FieldName = "Maspid";
            this.colMaspidc.Name = "colMaspidc";
            // 
            // colMaspc
            // 
            this.colMaspc.AppearanceCell.BackColor = System.Drawing.Color.Honeydew;
            this.colMaspc.AppearanceCell.Options.UseBackColor = true;
            this.colMaspc.Caption = "Mã Hàng";
            this.colMaspc.FieldName = "Masp";
            this.colMaspc.Name = "colMaspc";
            this.colMaspc.Visible = true;
            this.colMaspc.VisibleIndex = 0;
            this.colMaspc.Width = 88;
            // 
            // colTenspc
            // 
            this.colTenspc.AppearanceCell.BackColor = System.Drawing.Color.Honeydew;
            this.colTenspc.AppearanceCell.Options.UseBackColor = true;
            this.colTenspc.Caption = "Tên hàng hóa";
            this.colTenspc.FieldName = "Tensp";
            this.colTenspc.Name = "colTenspc";
            this.colTenspc.Visible = true;
            this.colTenspc.VisibleIndex = 1;
            this.colTenspc.Width = 176;
            // 
            // colDvtc
            // 
            this.colDvtc.AppearanceCell.BackColor = System.Drawing.Color.Honeydew;
            this.colDvtc.AppearanceCell.Options.UseBackColor = true;
            this.colDvtc.Caption = "ĐVT";
            this.colDvtc.FieldName = "Dvt";
            this.colDvtc.Name = "colDvtc";
            this.colDvtc.Visible = true;
            this.colDvtc.VisibleIndex = 2;
            this.colDvtc.Width = 69;
            // 
            // colQuycachc
            // 
            this.colQuycachc.AppearanceCell.BackColor = System.Drawing.Color.Honeydew;
            this.colQuycachc.AppearanceCell.Options.UseBackColor = true;
            this.colQuycachc.Caption = "Quy cách";
            this.colQuycachc.FieldName = "Quycach";
            this.colQuycachc.Name = "colQuycachc";
            this.colQuycachc.Visible = true;
            this.colQuycachc.VisibleIndex = 3;
            this.colQuycachc.Width = 100;
            // 
            // colMacodeC
            // 
            this.colMacodeC.Caption = "Mã Code";
            this.colMacodeC.FieldName = "Macode";
            this.colMacodeC.Name = "colMacodeC";
            this.colMacodeC.Visible = true;
            this.colMacodeC.VisibleIndex = 4;
            // 
            // colCanin
            // 
            this.colCanin.Caption = "Cần in";
            this.colCanin.DisplayFormat.FormatString = "n";
            this.colCanin.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colCanin.FieldName = "Soluong";
            this.colCanin.Name = "colCanin";
            this.colCanin.Visible = true;
            this.colCanin.VisibleIndex = 5;
            // 
            // colDongiac
            // 
            this.colDongiac.Caption = "Đơn giá";
            this.colDongiac.DisplayFormat.FormatString = "###,###,###.0";
            this.colDongiac.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colDongiac.FieldName = "Dongia";
            this.colDongiac.Name = "colDongiac";
            this.colDongiac.Visible = true;
            this.colDongiac.VisibleIndex = 6;
            // 
            // cmdExit
            // 
            this.cmdExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmdExit.Location = new System.Drawing.Point(1076, 11);
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Size = new System.Drawing.Size(92, 41);
            this.cmdExit.TabIndex = 4;
            this.cmdExit.Text = "Thoát";
            this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
            // 
            // cmdRemoveAll
            // 
            this.cmdRemoveAll.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmdRemoveAll.Location = new System.Drawing.Point(22, 215);
            this.cmdRemoveAll.Name = "cmdRemoveAll";
            this.cmdRemoveAll.Size = new System.Drawing.Size(38, 30);
            this.cmdRemoveAll.TabIndex = 8;
            this.cmdRemoveAll.Text = "|<";
            this.cmdRemoveAll.Click += new System.EventHandler(this.cmdRemoveAll_Click);
            // 
            // cmdRemove
            // 
            this.cmdRemove.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmdRemove.Location = new System.Drawing.Point(22, 179);
            this.cmdRemove.Name = "cmdRemove";
            this.cmdRemove.Size = new System.Drawing.Size(38, 30);
            this.cmdRemove.TabIndex = 7;
            this.cmdRemove.Text = "<";
            this.cmdRemove.Click += new System.EventHandler(this.cmdRemove_Click);
            // 
            // cmdAddAll
            // 
            this.cmdAddAll.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmdAddAll.Location = new System.Drawing.Point(22, 131);
            this.cmdAddAll.Name = "cmdAddAll";
            this.cmdAddAll.Size = new System.Drawing.Size(38, 30);
            this.cmdAddAll.TabIndex = 6;
            this.cmdAddAll.Text = ">|";
            this.cmdAddAll.Click += new System.EventHandler(this.cmdAddAll_Click);
            // 
            // cmdAdd
            // 
            this.cmdAdd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmdAdd.Location = new System.Drawing.Point(22, 95);
            this.cmdAdd.Name = "cmdAdd";
            this.cmdAdd.Size = new System.Drawing.Size(38, 30);
            this.cmdAdd.TabIndex = 5;
            this.cmdAdd.Text = ">";
            this.cmdAdd.Click += new System.EventHandler(this.cmdAdd_Click);
            // 
            // panelControl1
            // 
            this.panelControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelControl1.Controls.Add(this.chkThung);
            this.panelControl1.Controls.Add(this.labelControl3);
            this.panelControl1.Controls.Add(this.cbMautem);
            this.panelControl1.Controls.Add(this.chkQR);
            this.panelControl1.Controls.Add(this.cmdIn);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Controls.Add(this.spinSoluong);
            this.panelControl1.Controls.Add(this.chkGiaban);
            this.panelControl1.Controls.Add(this.cmdExit);
            this.panelControl1.Location = new System.Drawing.Point(1, 404);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1179, 104);
            this.panelControl1.TabIndex = 6;
            // 
            // chkThung
            // 
            this.chkThung.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkThung.Location = new System.Drawing.Point(21, 33);
            this.chkThung.Name = "chkThung";
            this.chkThung.Properties.Caption = "Cho Thùng/Lẻ";
            this.chkThung.Size = new System.Drawing.Size(179, 19);
            this.chkThung.TabIndex = 12;
            this.chkThung.ToolTip = "QR: Thung/Lẻ=(A/B), Barcode: Thùng/Lẻ=(1/2)";
            this.chkThung.CheckedChanged += new System.EventHandler(this.chkThung_CheckedChanged);
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.labelControl3.Location = new System.Drawing.Point(461, 25);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(48, 14);
            this.labelControl3.TabIndex = 11;
            this.labelControl3.Text = "Mẫu tem";
            // 
            // cbMautem
            // 
            this.cbMautem.EditValue = "3 Tem/Dòng (35mmx22mm)";
            this.cbMautem.Location = new System.Drawing.Point(515, 18);
            this.cbMautem.Name = "cbMautem";
            this.cbMautem.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.cbMautem.Properties.Appearance.Options.UseBackColor = true;
            this.cbMautem.Properties.AutoHeight = false;
            this.cbMautem.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.cbMautem.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbMautem.Properties.ButtonsStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.cbMautem.Properties.DropDownItemHeight = 30;
            this.cbMautem.Properties.Items.AddRange(new object[] {
            "3 Tem/Dòng (35mmx22mm)",
            "4 Tem/Dòng(25mmx15mm)"});
            this.cbMautem.Size = new System.Drawing.Size(199, 31);
            this.cbMautem.TabIndex = 10;
            // 
            // chkQR
            // 
            this.chkQR.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkQR.EditValue = true;
            this.chkQR.Location = new System.Drawing.Point(322, 21);
            this.chkQR.Name = "chkQR";
            this.chkQR.Properties.Caption = "In mã QR/Barcode";
            this.chkQR.Size = new System.Drawing.Size(124, 19);
            this.chkQR.TabIndex = 9;
            this.chkQR.ToolTip = "(893)-01-00001";
            // 
            // cmdIn
            // 
            this.cmdIn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdIn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmdIn.Location = new System.Drawing.Point(944, 11);
            this.cmdIn.Name = "cmdIn";
            this.cmdIn.Size = new System.Drawing.Size(129, 41);
            this.cmdIn.TabIndex = 8;
            this.cmdIn.Text = "In";
            this.cmdIn.Click += new System.EventHandler(this.cmdIn_Click);
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.labelControl1.Location = new System.Drawing.Point(743, 23);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(85, 18);
            this.labelControl1.TabIndex = 7;
            this.labelControl1.Text = "Số lượng In";
            // 
            // spinSoluong
            // 
            this.spinSoluong.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spinSoluong.Location = new System.Drawing.Point(834, 18);
            this.spinSoluong.Name = "spinSoluong";
            this.spinSoluong.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.spinSoluong.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.spinSoluong.Properties.Appearance.Options.UseBackColor = true;
            this.spinSoluong.Properties.Appearance.Options.UseFont = true;
            this.spinSoluong.Properties.AutoHeight = false;
            this.spinSoluong.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.spinSoluong.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spinSoluong.Size = new System.Drawing.Size(77, 31);
            this.spinSoluong.TabIndex = 6;
            this.spinSoluong.EditValueChanged += new System.EventHandler(this.spinSoluong_EditValueChanged);
            // 
            // chkGiaban
            // 
            this.chkGiaban.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkGiaban.Location = new System.Drawing.Point(21, 9);
            this.chkGiaban.Name = "chkGiaban";
            this.chkGiaban.Properties.Caption = "Tích hợp giá bán";
            this.chkGiaban.Size = new System.Drawing.Size(156, 19);
            this.chkGiaban.TabIndex = 5;
            // 
            // pnlTitleForm
            // 
            this.pnlTitleForm.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlTitleForm.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(122)))), ((int)(((byte)(165)))), ((int)(((byte)(220)))));
            this.pnlTitleForm.Appearance.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(164)))), ((int)(((byte)(190)))), ((int)(((byte)(224)))));
            this.pnlTitleForm.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.pnlTitleForm.Appearance.Options.UseBackColor = true;
            this.pnlTitleForm.Controls.Add(this.cmdAppMini);
            this.pnlTitleForm.Controls.Add(this.cmdAppExit);
            this.pnlTitleForm.Controls.Add(this.labelControl2);
            this.pnlTitleForm.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pnlTitleForm.Location = new System.Drawing.Point(1, 0);
            this.pnlTitleForm.Name = "pnlTitleForm";
            this.pnlTitleForm.Size = new System.Drawing.Size(1179, 45);
            this.pnlTitleForm.TabIndex = 7;
            this.pnlTitleForm.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnlTitleForm_MouseDown);
            this.pnlTitleForm.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pnlTitleForm_MouseMove);
            this.pnlTitleForm.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pnlTitleForm_MouseUp);
            // 
            // cmdAppMini
            // 
            this.cmdAppMini.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdAppMini.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmdAppMini.Location = new System.Drawing.Point(1098, 8);
            this.cmdAppMini.Name = "cmdAppMini";
            this.cmdAppMini.Size = new System.Drawing.Size(34, 28);
            this.cmdAppMini.TabIndex = 10;
            this.cmdAppMini.Text = "-";
            this.cmdAppMini.Click += new System.EventHandler(this.cmdAppMini_Click);
            // 
            // cmdAppExit
            // 
            this.cmdAppExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdAppExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmdAppExit.Location = new System.Drawing.Point(1134, 8);
            this.cmdAppExit.Name = "cmdAppExit";
            this.cmdAppExit.Size = new System.Drawing.Size(34, 28);
            this.cmdAppExit.TabIndex = 9;
            this.cmdAppExit.Text = "X";
            this.cmdAppExit.Click += new System.EventHandler(this.cmdAppExit_Click);
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 11F);
            this.labelControl2.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(122)))), ((int)(((byte)(165)))), ((int)(((byte)(220)))));
            this.labelControl2.Location = new System.Drawing.Point(8, 8);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(143, 18);
            this.labelControl2.TabIndex = 0;
            this.labelControl2.Text = "IN MÃ QR/BAR CODE";
            // 
            // panelControl2
            // 
            this.panelControl2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panelControl2.Controls.Add(this.cmdRemoveAll);
            this.panelControl2.Controls.Add(this.cmdAdd);
            this.panelControl2.Controls.Add(this.cmdRemove);
            this.panelControl2.Controls.Add(this.cmdAddAll);
            this.panelControl2.Location = new System.Drawing.Point(439, 37);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(86, 402);
            this.panelControl2.TabIndex = 8;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1181, 509);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.pnlTitleForm);
            this.Controls.Add(this.grdSanpham);
            this.Controls.Add(this.grdSanphamc);
            this.Controls.Add(this.panelControl2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "IN MÃ CODE";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdSanpham)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvSanpham)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdSanphamc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvSanphamc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkThung.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbMautem.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkQR.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinSoluong.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkGiaban.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlTitleForm)).EndInit();
            this.pnlTitleForm.ResumeLayout(false);
            this.pnlTitleForm.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl grdSanpham;
        private DevExpress.XtraGrid.Views.Grid.GridView gvSanpham;
        private DevExpress.XtraGrid.Columns.GridColumn colMaspid;
        private DevExpress.XtraGrid.Columns.GridColumn colMasp;
        private DevExpress.XtraGrid.Columns.GridColumn colTensp;
        private DevExpress.XtraGrid.Columns.GridColumn colDvt;
        private DevExpress.XtraGrid.Columns.GridColumn colQuycach;
        private DevExpress.XtraGrid.GridControl grdSanphamc;
        private DevExpress.XtraGrid.Views.Grid.GridView gvSanphamc;
        private DevExpress.XtraGrid.Columns.GridColumn colMaspidc;
        private DevExpress.XtraGrid.Columns.GridColumn colMaspc;
        private DevExpress.XtraGrid.Columns.GridColumn colTenspc;
        private DevExpress.XtraGrid.Columns.GridColumn colDvtc;
        private DevExpress.XtraGrid.Columns.GridColumn colQuycachc;
        private DevExpress.XtraGrid.Columns.GridColumn colDongiac;
        private DevExpress.XtraEditors.SimpleButton cmdExit;
        private DevExpress.XtraEditors.SimpleButton cmdRemoveAll;
        private DevExpress.XtraEditors.SimpleButton cmdRemove;
        private DevExpress.XtraEditors.SimpleButton cmdAddAll;
        private DevExpress.XtraEditors.SimpleButton cmdAdd;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.CheckEdit chkGiaban;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SpinEdit spinSoluong;
        private DevExpress.XtraEditors.SimpleButton cmdIn;
        private DevExpress.XtraEditors.CheckEdit chkQR;
        private DevExpress.XtraEditors.PanelControl pnlTitleForm;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.SimpleButton cmdAppMini;
        private DevExpress.XtraEditors.SimpleButton cmdAppExit;
        private DevExpress.XtraGrid.Columns.GridColumn colMacodeC;
        private DevExpress.XtraGrid.Columns.GridColumn colCanin;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.ComboBoxEdit cbMautem;
        private DevExpress.XtraGrid.Columns.GridColumn colMacode;
        private DevExpress.XtraEditors.CheckEdit chkThung;
    }
}

