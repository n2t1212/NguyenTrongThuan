USE [MTSMPOS]
GO
/****** Object:  StoredProcedure [dbo].[rptTC_Phieuthuchi]    Script Date: 11/03/2019 14:20:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROC [dbo].[rptTC_Phieuthuchi]  
 @Phieutcid nvarchar(50),
 @Nguoidung nvarchar(50)
 As   
  Begin  
    
    SELECT A.Phieutcid,A.Sophieu,A.Ngayct,A.Ngayps,A.Soctgoc,C.Tenkh,E.Lydo,C.Diachi,A.Ghichu,A.Sotien,
           NgayPhieu=N'Ngày '+right('0'+cast(DAY(A.Ngayct) as nvarchar(2)),2)+N' Tháng '+ right('0'+cast(Month(A.Ngayct) as nvarchar(2)),2)+N' Năm '+ CAST(YEAR(A.Ngayct) as nvarchar),
           STT=ROW_NUMBER() OVER(PARTITION BY A.Phieutcid  ORDER BY  A.Phieutcid  asc),
           B.Phieutcctid,B.TKNo,B.TKCo,B.Nguyente,B.Thanhtien,B.Diengiai,B.SoHD,B.NChono
    FROM TC_PHIEUTC A WITH(NOLOCK) INNER JOIN TC_PHIEUTCCT B WITH(NOLOCK)  ON A.Phieutcid=B.Phieutcid
                                   LEFT JOIN DM_KHACHHANG C WITH(NOLOCK) ON A.Madt=C.Makh
                                   LEFT JOIN DM_LYDO E WITH(NOLOCK) ON A.Mald=E.Mald
    WHERE A.Phieutcid=@Phieutcid
    ORDER BY  A.Phieutcid asc,C.Makh asc 
  End


USE [MTSMPOS]
GO
/****** Object:  StoredProcedure [dbo].[sp_TC_AddPhieuTC]    Script Date: 11/03/2019 14:21:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



ALTER proc [dbo].[sp_TC_AddPhieuTC]
	@tblPhieutc dbo.Type_TC_PHIEUTC readonly,
	@tblPhieutcct dbo.Type_TC_PHIEUTCCT readonly,
	@nguoidung nvarchar(50),
	@ketqua nvarchar(255) output
as
	begin
		set dateformat DMY
		set @ketqua=''
		declare @mPhieutcid nvarchar(50)
		set @mPhieutcid = NEWID()
		declare @dNguyente numeric(18,3),
				@dSotien numeric(18,0)
		select @dNguyente=SUM(ISNULL(Nguyente,0)),
			   @dSotien=SUM(ISNULL(Sotien,0))
		from @tblPhieutc
		
		if not exists(select * from TC_PHIEUTC with(nolock)
							   where Phieutcid in (select V.Phieutcid
												   from @tblPhieutc V))
			begin
				insert into TC_PHIEUTC(Phieutcid,Mant,Madt,Loaiphieu,Sophieu,Ngayct,Ngayps,Tygia,Soctgoc,TKDu,Nguyente,Sotien,Hoten,Nguoilap,Ngaylap,Ghiso,Mald,Ghichu,Loaitien)
				select distinct @mPhieutcid,Mant,Madt,Loaiphieu,Sophieu,Ngayct,Ngayps,Tygia,Soctgoc,TKDu,Nguyente,Sotien,Hoten,@nguoidung,GETDATE(),Ghiso,Mald,Ghichu,Loaitien
				from @tblPhieutc
				
				insert into TC_PHIEUTCCT(Phieutcctid,Phieutcid,Macp,Madt,Matk,TKNo,TKCo,Nguyente,Thanhtien,Diengiai,SoHD,NChono)
				select distinct NEWID(),@mPhieutcid,Macp,Madt,Matk,TKNo,TKCo,ISNULL(Nguyente,0),ISNULL(Thanhtien,0),Diengiai,SoHD,NChono
				from @tblPhieutcct
			end
		else
			begin
				update TC_PHIEUTC
				set Mant=B.Mant,Madt=B.Madt,Loaiphieu=B.Loaiphieu,
					Sophieu=B.Sophieu,Ngayct=B.Ngayct,Ngayps=B.Ngayps,Tygia=B.Tygia,
					Soctgoc=B.Soctgoc,TKDu=B.TKDu,Nguyente=isnull(B.Nguyente,0),
					Sotien=ISNULL(B.Sotien,0),Hoten=B.Hoten,Nguoisua=@nguoidung,Ngaysua=GETDATE(),
					Ghiso=B.Ghiso,Mald=B.Mald,Ghichu=B.Ghichu,Loaitien=B.Loaitien	
				from TC_PHIEUTC A inner join @tblPhieutc B on A.Phieutcid=B.Phieutcid
				
				delete TC_PHIEUTCCT where Phieutcid in (select V.Phieutcid from @tblPhieutc V)
				insert into TC_PHIEUTCCT(Phieutcctid,Phieutcid,Macp,Madt,Matk,TKNo,TKCo,Nguyente,Thanhtien,Diengiai,SoHD,NChono)
				select distinct Phieutcctid,Phieutcid,Macp,Madt,Matk,TKNo,TKCo,ISNULL(Nguyente,0),ISNULL(Thanhtien,0),Diengiai,SoHD,NChono
				from @tblPhieutcct
			end
	end


USE [MTSMPOS]
GO
/****** Object:  StoredProcedure [dbo].[spTL_AddTichLuyKhachHang]    Script Date: 11/03/2019 14:21:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER proc [dbo].[spTL_AddTichLuyKhachHang]
	@Makh nvarchar(50),
	@Phieubhct dbo.Type_BH_PHIEUBHCT readonly,
	@ketqua nvarchar(255) output     
	as
	begin
	
		declare @masp nvarchar(20)
		declare cur cursor for select distinct(Masp) from @Phieubhct;
		
		declare @mDoanhsoDatDuoc numeric(18,3)
		declare @DoanhsoDM numeric(18,3)
		declare @QDDiemDSDM numeric(18,3)
		declare @mDoanhSoMoi numeric(18,3)
		declare @mDiemDoanhSo numeric(18,3)
		declare @mDoanhsoCurrent numeric(18,3)
		declare @mDiemDaTruCurrent numeric(18,3)
		declare @mSoLuongQua numeric(18,3)
		open cur
		fetch next from cur into @masp
		while @@FETCH_STATUS = 0
		begin
			set @mDoanhsoCurrent=(select Doanhso from TICHLUY_KHACHHANG where Makh=@Makh and Masp=@masp);
			set @mDiemDaTruCurrent=(select sum(Diemdatru) from TICHLUY_KHACHHANG where Makh=@Makh);
			set @mDoanhsoDatDuoc=(select Soluong from @Phieubhct where Masp=@masp);
			set @DoanhsoDM=(select DoanhSo from DM_TICHLUYDIEM where Masp=@masp and DATEDIFF(DAY,Ngayapdung,GETDATE())>=0 and DATEDIFF(DAY,GETDATE(),Denngay)>=0 AND ISNULL(Apdung,0)=1);
			set @QDDiemDSDM=(select QDDiemDS from DM_TICHLUYDIEM where Masp=@masp and DATEDIFF(DAY,Ngayapdung,GETDATE())>=0 and DATEDIFF(DAY,GETDATE(),Denngay)>=0 AND ISNULL(Apdung,0)=1);
			set @mDoanhSoMoi=ISNULL(@mDoanhsoCurrent,0) + isnull(@mDoanhsoDatDuoc,0);
			set @mDiemDoanhSo =  @QDDiemDSDM *floor((@mDoanhSoMoi/isnull(@DoanhsoDM,1)));
			set @mSoLuongQua = (select Soluong from @Phieubhct where Masp=@masp and Quatang = 1)
			
			if not exists (select * from TICHLUY_KHACHHANG with(nolock) where Makh=@Makh and Masp=@masp)
				begin
					if (ISNULL(@mSoLuongQua,0) <> 0) -- Qua tang tinh diem da tru
						begin
							insert into TICHLUY_KHACHHANG(Makh,Masp,Doanhso,DiemDs,Diemdatru)
							values(@Makh,@masp,0,0,@mSoLuongQua*@QDDiemDSDM + @mDiemDaTruCurrent)
						end
					else
						begin
							insert into TICHLUY_KHACHHANG(Makh,Masp,Doanhso,DiemDs,Diemdatru)
							values(@Makh,@masp,@mDoanhSoMoi,@mDiemDoanhSo,0)
						end
					
				end
			else
				begin
					if (ISNULL(@mSoLuongQua,0) <> 0) -- Qua tang tinh diem da tru
						begin
							update TICHLUY_KHACHHANG
							set Diemdatru=@mSoLuongQua*@QDDiemDSDM + @mDiemDaTruCurrent
						where Makh=@Makh and Masp=@masp
						end
					else
						begin
							update TICHLUY_KHACHHANG
							set Doanhso=@mDoanhSoMoi,
								DiemDs=@mDiemDoanhSo
							where Makh=@Makh and Masp=@masp
						end
					
				end 
				fetch next from cur into @masp
		end
		close cur;
		deallocate cur;
	end
	
	
	USE [MTSMPOS]
GO
/****** Object:  StoredProcedure [dbo].[rptBH_InBill]    Script Date: 11/03/2019 14:21:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROC [dbo].[rptBH_InBill]    
 @Phieubhid nvarchar(50),     
 @Nguoidung nvarchar(50)    
AS    
 BEGIN     
   DECLARE @BHCT AS TABLE(Loai Nvarchar(50),Phieubhid nvarchar(50),Masp nvarchar(20),Tensp nvarchar(100),Dvt nvarchar(30),Quycach nvarchar(50),    
                          Soluong numeric(18,3),Dongia numeric(18,3),Nguyente numeric(18,3),Ck numeric(18,3),TTCk numeric(18,3),    
                          Thanhtien numeric(18,3), Ghichu nvarchar(255))    
      
   INSERT INTO @BHCT(Loai,Phieubhid,Masp,Tensp,Dvt,Quycach,Soluong,Dongia,Nguyente,Ck,TTCk,Thanhtien,Ghichu)    
   SELECT Loai=(CASE WHEN ISNULL(Quatang,0)=1 THEN  2 ELSE 1 END),Phieubhid, Masp,Tensp,Dvt,Quycach,Soluong,Dongia,Nguyente,Ck,TTCk,Thanhtien,Ghichu     
   FROM BH_PHIEUBHCT WHERE Phieubhid=@Phieubhid  
   
   DECLARE @tongDiemTL numeric(18,3)
   DECLARE @tongDiemDaTru numeric(18,3)
   DECLARE @diemHienTai numeric(18,3)
   SET @tongDiemTL = (SELECT SUM(DiemDS) FROM TICHLUY_KHACHHANG WHERE Makh IN (SELECT Makh FROM BH_PHIEUBH A WHERE Phieubhid=@Phieubhid))  
   SET @tongDiemDaTru = (SELECT SUM(Diemdatru) FROM TICHLUY_KHACHHANG WHERE Makh IN (SELECT Makh FROM BH_PHIEUBH A WHERE Phieubhid=@Phieubhid))  
   SET @diemHienTai = ISNULL(@tongDiemTL,0) - ISNULL(@tongDiemDaTru,0)
   
   SELECT STT=ROW_NUMBER()OVER(PARTITION BY Loai ORDER BY B.Loai,B.Masp asc),     
          A.Phieubhid,A.Sophieu,A.Ngayct,A.Ngaylap,A.Makh,A.Tenkh,A.Dienthoai,A.Diachi,A.Quay,A.Ca,A.Thungan,    
          Tienhang=A.Nguyente,TongCk=A.Ck,TongTTCk=A.TTCk,Thanhtoan=A.Thanhtien,A.Tientra,A.Tienthoi,A.Ghichu,     
          B.Loai,Diengiai=B.Tensp+' '+ B.Quycach, B.Masp,B.Tensp,B.Dvt,B.Quycach,B.Dongia,B.Soluong,B.Nguyente,B.Ck,B.TTCk,B.Thanhtien,GHICHUCT=B.Ghichu,DiemTichLuy=@diemHienTai    
   FROM BH_PHIEUBH A LEFT JOIN @BHCT B ON A.Phieubhid=B.Phieubhid    
   WHERE A.Phieubhid=@Phieubhid    
   ORDER BY B.Loai,B.Masp ASC     
 END
 
 USE [MTSMPOS]
GO
/****** Object:  StoredProcedure [dbo].[rptTL_TongDiemTL]    Script Date: 11/03/2019 14:22:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROC [dbo].[rptTL_TongDiemTL]
 @isChiTiet numeric (18,0),  
 @tblMADT [dbo].[Type_Makh] READONLY   
AS    
 BEGIN
	-- xem chi tiet
	IF ISNULL(@isChiTiet,0) = 1
		BEGIN 
			 SELECT STT=ROW_NUMBER() OVER(ORDER BY TMP.Makh asc), TMP.*, TMP.TongDiemDs - TMP.TongDiemdatru AS Diemhientai
			 FROM
			   (SELECT SUM(a.Doanhso) as TongDoanhso, SUM(a.DiemDs) as TongDiemDs, SUM(a.Diemdatru) as TongDiemdatru, b.Makh, b.Tenkh, b.Diachi 
			   FROM TICHLUY_KHACHHANG a LEFT JOIN DM_KHACHHANG b ON a.Makh = b.Makh 
			   WHERE a.Makh IN (SELECT v.Maso FROM @tblMADT v) 
			   GROUP BY a.Makh, b.Makh, b.Tenkh, b.Diachi) as TMP
		END
	ELSE
		BEGIN
			SELECT TMP.*, TMP.TongDiemDs - TMP.TongDiemdatru AS Diemhientai
			 FROM
			   (SELECT SUM(a.Doanhso) as TongDoanhso, SUM(a.DiemDs) as TongDiemDs, SUM(a.Diemdatru) as TongDiemdatru, b.Makh, b.Tenkh, b.Diachi 
			   FROM TICHLUY_KHACHHANG a LEFT JOIN DM_KHACHHANG b ON a.Makh = b.Makh 
			   WHERE a.Makh IN (SELECT v.Maso FROM @tblMADT v) 
			   GROUP BY a.Makh, b.Makh, b.Tenkh, b.Diachi) as TMP
		END
 END
 
 
 
 USE [MTSMPOS]
GO
/****** Object:  UserDefinedFunction [dbo].[fnCN_Canthu]    Script Date: 11/03/2019 14:22:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER FUNCTION [dbo].[fnCN_Canthu](@Tungay varchar(10),@Denngay varchar(10), @Matk varchar(12),@Mant varchar(10))   
RETURNS @RetTable TABLE (  
   Matk nvarchar(12),Makh nvarchar(20),Phanhe tinyint,Phieuid nvarchar(50),  
   LoaiCT nvarchar(2),Loaiphieu nvarchar(2),Sophieu nvarchar(20),Soctgoc nvarchar(50),
   Ngayct Date,Nchono numeric (3),NgayDH Date,Sotien numeric (19,2),TKNo nvarchar(12),TKCo nvarchar(12),      
   SotienQH numeric (19,2),Diengiai nvarchar(255),Nguoilap nvarchar(50))  
AS  
  
BEGIN  
  DECLARE @mTungay Date, @mDenngay Date;   
  SET @mTungay = CONVERT(Date,@Tungay,103);   
  SET @mDenngay= CONVERT(Date,@Denngay,103);   
       
  INSERT INTO @RetTable (Matk,Makh,Phanhe,Phieuid,LoaiCT,Loaiphieu,Sophieu,Soctgoc,Ngayct,Nchono,NgayDH,Sotien,TKNo,TKCo,SotienQH,Diengiai,Nguoilap)  
  SELECT DISTINCT Matk=@Matk,A.Makh,Phanhe=2,A.Phieuid,LoaiCT='TP',A.Loaiphieu,A.Sophieu,A.Soctgoc,A.Ngayct,B.Nchono,
         NgayDH=DATEADD(DD,ISNULL(B.Nchono,0),A.Ngayps),B.Sotien,A.TKno,A.TKCo,SotienQH=0,
         Diengiai= C.Lydo,A.Nguoilap
  FROM vNX_PhieuNX A 
  INNER JOIN     
	  (SELECT x.Phieuid, Nchono=Isnull(x.Nchono,0) , 
			 Sotien=SUM(ROUND(ISNULL(x.Soluong,0)*ISNULL(x.Dongia,0),0)+ROUND(ISNULL(x.Soluong,0)*ISNULL(x.Dongia,0)*(ISNULL(x.vatCT,0)/100),0))       
	  FROM vNX_PhieuNX x
	  WHERE ISNULL(x.Ghiso,0)=1 AND x.Loaiphieu IN('PN','PX') AND
			DATEADD(DD,Isnull(x.Nchono,0),x.NgayPS)>=@mTungay AND
			DATEADD(DD,Isnull(x.Nchono,0),x.NgayPS)<=@mDenngay AND
			x.Loaitien=@Mant AND (LEFT(x.TKNo,len(@Matk))=@Matk OR LEFT(x.TKCo,len(@Matk))=@Matk)
	  GROUP BY x.Phieuid, Isnull(x.Nchono,0)) 
  B ON A.Phieuid=B.Phieuid
  LEFT JOIN DM_LYDO C ON A.Mald=C.Mald
  
  UNION ALL
  
  SELECT Matk=@Matk,Makh=A.Madt,Phanhe=1,A.Phieuid,LoaiCT='CN',A.Loaiphieu,A.Sophieu,A.Soctgoc,A.Ngayct,A.Nchono,
         NgayDH=DATEADD(DD,ISNULL(A.Nchono,0),A.Ngayps),A.ThanhtienCT,A.TKno,A.TKCo,SotienQH=0,
         Diengiai= B.Lydo,A.Nguoilap 
  FROM vTC_PhieuTC A LEFT JOIN DM_LYDO B ON A.Mald=B.Mald
  
  UNION ALL
  
  SELECT DISTINCT 
  Matk=@Matk,A.Makh,Phanhe=1,A.Phieuid,LoaiCT='CN',A.Loaiphieu,A.Sophieu,Soctgoc='',A.Ngayct,Nchono=1,
         NgayDH=DATEADD(DD,ISNULL(1,0),A.Ngayps),A.Thanhtien-A.Tientra,A.TKNo,A.TKCo,SotienQH=0,
         Diengiai=N'Bán khách lẻ',A.Nguoilap 
	FROM vBH_PhieuBH A
	WHERE A.Tientra - A.Thanhtien < 0
  
 RETURN;  
END;  

USE [MTSMPOS]
GO
/****** Object:  UserDefinedFunction [dbo].[fnCN_Gom_Taikhoan]    Script Date: 11/03/2019 14:22:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER FUNCTION [dbo].[fnCN_Gom_Taikhoan] (@Tungay varchar(10),@Denngay varchar(10))  
RETURNS 
 @RetTable TABLE (Phieuid nvarchar(50),Mant nvarchar(3),Macp nvarchar(4),Madt nvarchar(15),  
                  Matk nvarchar(12),TKDu nvarchar(12),PSNo numeric (18,3),QDPSNo numeric (18,3), PSCo numeric (18,3),
                  QDPSCo numeric (18,3),Hopdong nvarchar(50))  
AS  
BEGIN  
    DECLARE @mTKCN varchar(200),@mTKTU varchar(200), @mTungay Date,@mDenngay Date;        
    DECLARE @DataTH TABLE (Phieuid nvarchar(50),Ngayct date,Mant nvarchar(3),Macp nvarchar(4),Madt nvarchar(15), 
                         TKNo nvarchar(12),TKCo nvarchar (12),Nguyente numeric (18,3),Sotien numeric (18,3),Hopdong nvarchar(50));
     
	SET @mTungay = CONVERT(date,@Tungay,103);  
	SET @mDenngay = CONVERT(date,@Denngay,103);  

	SELECT @mTKCN = PARA_VAL FROM HT_PARA WHERE UPPER(PARA_NAME) = 'MATK_CN';  
	SELECT @mTKTU = PARA_VAL FROM HT_PARA WHERE UPPER(PARA_NAME) = 'MATK_TU';  
	SET @mTKCN = ISNULL(@mTKCN,'138,131,311,331,144,244,341,338,128');  
	SET @mTKTU = ISNULL(@mTKTU,'141');  
    
    INSERT INTO @DataTH (Phieuid,Ngayct,Mant,Macp,Madt,TKNo,TKCo,Nguyente,Sotien,Hopdong)
    SELECT DISTINCT Phieuid,Ngayct,Mant=Loaitien,Macp='',Madt=Makh,TKNo,TKCo,
           Nguyente=ROUND(ISNULL(v.Soluong,0)*ISNULL(v.Dongia,0)*(1+ISNULL(v.VAT,0)),0),
           Sotien=ROUND(ISNULL(v.Soluong,0)*ISNULL(v.Dongia,0)*(1+ISNULL(v.VAT,0)),0)*Isnull(Tygia,0),
           SoHD=''
     FROM vNX_PhieuNX v 
     WHERE v.Ngayct BETWEEN @mTungay and @mDenngay AND ISNULL(v.Ghiso,0)=1 AND 
           ISNULL(v.TKNo,'')<>''AND ISNULL(v.TKCo,'')<>''    
    UNION ALL    
    SELECT DISTINCT Phieuid,Ngayct,Mant,Macp='',Madt,TKNo,TKCo,
           Nguyente=ROUND(ISNULL(v.NguyenteCT,0),0),
           Sotien=ROUND(ISNULL(v.ThanhtienCT,0),0),SoHD
    FROM vTC_PhieuTC v 
    WHERE v.Ngayct BETWEEN @mTungay and @mDenngay AND ISNULL(v.Ghiso,0)=1 AND 
           ISNULL(v.TKNo,'')<>''AND ISNULL(v.TKCo,'')<>''
    ---- Union view Phieu ban hang
	UNION ALL
	SELECT DISTINCT Phieuid,
					Ngayct,
					Mant=Nguyente,
					Macp='',
					Madt=Makh,
					TKNo,
					TKCo,
					Nguyente=ROUND(ISNULL(v.NguyenteCT,0),0),
					Sotien=ROUND(ISNULL(v.ThanhtienCT,0),0),SoHd=''
	FROM vBH_PhieuBH v
    WHERE v.Ngayct BETWEEN @mTungay and @mDenngay AND ISNULL(v.Ghiso,0)=1 AND 
           ISNULL(v.TKNo,'')<>''AND ISNULL(v.TKCo,'')<>''
   
    INSERT INTO @RetTable(Phieuid,Mant,Macp,Madt,Matk,TKDu,PSNo,QDPSNo, PSCo,QDPSCo,Hopdong)  
    SELECT Phieuid,Mant,Macp,Madt,Matk=TKNo,TKDu=TKCo,PSNo=Isnull(Nguyente,0),QDPSNo=Isnull(Sotien,0),
           PSCo=0,QDPSCo=0,Hopdong
    FROM @DataTH
    Union all
    SELECT Phieuid,Mant,Macp,Madt,Matk=TKCo,TKDu=TKNo,
           PSNo=0,QDPSNo=0,PSCo=Isnull(Nguyente,0),QDPSCo=Isnull(Sotien,0),Hopdong  
    FROM @DataTH 
    
  RETURN;     
 END;  