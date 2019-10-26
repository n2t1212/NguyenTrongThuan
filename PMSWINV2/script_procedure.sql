USE [MTSMPOS]
GO
/****** Object:  UserDefinedTableType [dbo].[Type_TC_PHIEUTCCT]    Script Date: 10/26/2019 08:37:57 ******/
CREATE TYPE [dbo].[Type_TC_PHIEUTCCT] AS TABLE(
	[Phieutcctid] [nvarchar](50) NOT NULL,
	[Phieutcid] [nvarchar](50) NULL,
	[Macp] [nvarchar](10) NULL,
	[Madt] [nvarchar](15) NULL,
	[Matk] [nvarchar](12) NULL,
	[TKNo] [nvarchar](12) NOT NULL,
	[TKCo] [nvarchar](12) NOT NULL,
	[Nguyente] [numeric](18, 3) NULL,
	[Thanhtien] [numeric](18, 0) NOT NULL,
	[Diengiai] [nvarchar](150) NOT NULL,
	[SoHD] [nvarchar](20) NULL,
	[NChono] [numeric](3, 0) NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[Type_TC_PHIEUTC]    Script Date: 10/26/2019 08:37:57 ******/
CREATE TYPE [dbo].[Type_TC_PHIEUTC] AS TABLE(
	[Phieutcid] [nvarchar](50) NOT NULL,
	[Mant] [nvarchar](50) NULL,
	[Madt] [nvarchar](15) NULL,
	[Loaiphieu] [nvarchar](2) NULL,
	[Sophieu] [nvarchar](20) NULL,
	[Ngayct] [date] NULL,
	[Ngayps] [date] NULL,
	[Tygia] [numeric](6, 0) NULL,
	[Soctgoc] [nvarchar](20) NULL,
	[TKDu] [nvarchar](12) NULL,
	[Nguyente] [numeric](18, 3) NULL,
	[Sotien] [numeric](18, 0) NULL,
	[Hoten] [nvarchar](100) NULL,
	[Nguoilap] [nvarchar](50) NULL,
	[Ngaylap] [datetime] NULL,
	[Nguoisua] [nvarchar](50) NULL,
	[Ngaysua] [datetime] NULL,
	[Ghiso] [bit] NULL,
	[Mald] [nvarchar](15) NULL,
	[Ghichu] [nvarchar](255) NULL,
	[Loaitien] [nvarchar](50) NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[Type_SP_MASP]    Script Date: 10/26/2019 08:37:57 ******/
CREATE TYPE [dbo].[Type_SP_MASP] AS TABLE(
	[Maspid] [nvarchar](50) NOT NULL,
	[Masp] [nvarchar](20) NULL,
	[Soluong] [int] NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[Type_SoPhieu]    Script Date: 10/26/2019 08:37:57 ******/
CREATE TYPE [dbo].[Type_SoPhieu] AS TABLE(
	[Sophieu] [nvarchar](50) NOT NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[Type_NX_PHIEUNXCT]    Script Date: 10/26/2019 08:37:57 ******/
CREATE TYPE [dbo].[Type_NX_PHIEUNXCT] AS TABLE(
	[Phieunxctid] [nvarchar](50) NOT NULL,
	[Phieunxid] [nvarchar](50) NULL,
	[Maspid] [nvarchar](50) NULL,
	[Masp] [nvarchar](50) NULL,
	[Tensp] [nvarchar](50) NULL,
	[Quycach] [nvarchar](50) NULL,
	[Dvt] [nvarchar](50) NULL,
	[SLThung] [numeric](18, 3) NULL,
	[Soluong] [numeric](18, 3) NULL,
	[Dongia] [numeric](18, 3) NULL,
	[Nguyente] [numeric](18, 3) NULL,
	[Vat] [numeric](6, 3) NULL,
	[TTVat] [numeric](15, 3) NULL,
	[Thanhtien] [numeric](18, 3) NULL,
	[Songaychono] [numeric](18, 0) NULL,
	[Ghiso] [bit] NULL,
	[Ghichu] [nvarchar](255) NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[Type_NX_PHIEUNX]    Script Date: 10/26/2019 08:37:57 ******/
CREATE TYPE [dbo].[Type_NX_PHIEUNX] AS TABLE(
	[Phieunxid] [nvarchar](50) NOT NULL,
	[Sophieu] [nvarchar](20) NULL,
	[Loaiphieu] [nvarchar](2) NULL,
	[Makho] [nvarchar](15) NULL,
	[Mald] [nvarchar](15) NULL,
	[Makh] [nvarchar](15) NULL,
	[Ngayct] [date] NULL,
	[Ngayps] [date] NULL,
	[Soctgoc] [nvarchar](30) NULL,
	[Loaitien] [nvarchar](5) NULL,
	[Tygia] [numeric](18, 3) NULL,
	[Nguyente] [numeric](18, 3) NULL,
	[Vat] [numeric](18, 3) NULL,
	[TTVat] [numeric](18, 3) NULL,
	[Thanhtien] [numeric](18, 3) NULL,
	[Ghichu] [nvarchar](255) NULL,
	[Giaonhan] [nvarchar](70) NULL,
	[Ngaylap] [datetime] NULL,
	[Nguoilap] [nvarchar](50) NULL,
	[Ngaysua] [datetime] NULL,
	[Nguoisua] [nvarchar](50) NULL,
	[Dongbo] [bit] NULL,
	[TKNo] [nvarchar](12) NULL,
	[TKCo] [nvarchar](20) NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[Type_NX_CHANHXECT]    Script Date: 10/26/2019 08:37:56 ******/
CREATE TYPE [dbo].[Type_NX_CHANHXECT] AS TABLE(
	[Chanhxectid] [nvarchar](50) NOT NULL,
	[Chanhxeid] [nvarchar](50) NULL,
	[Maspid] [nvarchar](50) NULL,
	[Masp] [nvarchar](20) NULL,
	[SLThung] [numeric](18, 3) NULL,
	[Soluong] [numeric](18, 3) NULL,
	[Chiphi] [numeric](18, 3) NULL,
	[Ghichu] [nvarchar](100) NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[Type_NX_CHANHXE]    Script Date: 10/26/2019 08:37:56 ******/
CREATE TYPE [dbo].[Type_NX_CHANHXE] AS TABLE(
	[Chanhxeid] [nvarchar](50) NOT NULL,
	[Sophieu] [nvarchar](20) NULL,
	[Loaiphieu] [nvarchar](2) NULL,
	[Ngayct] [date] NULL,
	[Ngayps] [date] NULL,
	[Makh] [nvarchar](50) NULL,
	[Tenkh] [nvarchar](100) NULL,
	[Diachi] [nvarchar](255) NULL,
	[Dienthoai] [nvarchar](15) NULL,
	[Email] [nvarchar](50) NULL,
	[Soxe] [nvarchar](20) NULL,
	[Loaixe] [nvarchar](50) NULL,
	[Taixe] [nvarchar](50) NULL,
	[Dienthoaixe] [nvarchar](15) NULL,
	[Trongluong] [numeric](18, 3) NULL,
	[Chiphi] [numeric](18, 3) NULL,
	[HTThanhtoan] [nvarchar](50) NULL,
	[Trangthai] [tinyint] NULL,
	[Ghichu] [nvarchar](255) NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[Type_Masp]    Script Date: 10/26/2019 08:37:56 ******/
CREATE TYPE [dbo].[Type_Masp] AS TABLE(
	[Maspid] [nvarchar](50) NOT NULL,
	[Masp] [nvarchar](20) NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[Type_Makh]    Script Date: 10/26/2019 08:37:56 ******/
CREATE TYPE [dbo].[Type_Makh] AS TABLE(
	[Maso] [nvarchar](15) NOT NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[Type_HT_Para]    Script Date: 10/26/2019 08:37:56 ******/
CREATE TYPE [dbo].[Type_HT_Para] AS TABLE(
	[PARA_NAME] [nvarchar](30) NOT NULL,
	[PARA_VAL] [nvarchar](50) NULL,
	[PARA_DESC] [nvarchar](100) NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[Type_HT_NHOMQUYEN_CHUCNANG]    Script Date: 10/26/2019 08:37:56 ******/
CREATE TYPE [dbo].[Type_HT_NHOMQUYEN_CHUCNANG] AS TABLE(
	[soid] [nvarchar](50) NOT NULL,
	[manhom] [nvarchar](50) NULL,
	[macn] [nvarchar](50) NULL,
	[them] [bit] NULL,
	[sua] [bit] NULL,
	[xoa] [bit] NULL,
	[duyet] [bit] NULL,
	[in] [bit] NULL,
	[nguoilap] [nvarchar](50) NULL,
	[ngaylap] [datetime] NULL,
	[nguoisua] [nvarchar](50) NULL,
	[ngaysua] [datetime] NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[Type_HT_NHOMQUYEN]    Script Date: 10/26/2019 08:37:56 ******/
CREATE TYPE [dbo].[Type_HT_NHOMQUYEN] AS TABLE(
	[soid] [nvarchar](50) NOT NULL,
	[manhom] [nvarchar](50) NULL,
	[tennhom] [nvarchar](50) NOT NULL,
	[nguoilap] [nvarchar](50) NULL,
	[ngaylap] [datetime] NULL,
	[nguoisua] [nvarchar](50) NULL,
	[ngaysua] [datetime] NULL,
	[ngaysync] [datetime] NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[Type_HT_NGUOIDUNG]    Script Date: 10/26/2019 08:37:56 ******/
CREATE TYPE [dbo].[Type_HT_NGUOIDUNG] AS TABLE(
	[soid] [nvarchar](50) NOT NULL,
	[manv] [nvarchar](50) NULL,
	[taikhoan] [nvarchar](50) NOT NULL,
	[matkhau] [nvarchar](50) NULL,
	[hoten] [nvarchar](50) NULL,
	[nguoilap] [nvarchar](50) NULL,
	[ngaylap] [datetime] NULL,
	[nguoisua] [nvarchar](50) NULL,
	[ngaysua] [datetime] NULL,
	[ngaysync] [datetime] NULL,
	[isync] [bit] NULL,
	[kyhieu] [nvarchar](50) NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[Type_DM_SANPHAM]    Script Date: 10/26/2019 08:37:56 ******/
CREATE TYPE [dbo].[Type_DM_SANPHAM] AS TABLE(
	[spid] [nvarchar](50) NOT NULL,
	[manhomspid] [nvarchar](50) NULL,
	[masp] [nvarchar](20) NULL,
	[tensp] [nvarchar](100) NULL,
	[dvt] [nvarchar](30) NULL,
	[quycach] [nvarchar](50) NULL,
	[quydoikgl] [numeric](10, 4) NULL,
	[quydoithung] [numeric](10, 4) NULL,
	[nhacungcap] [nvarchar](100) NULL,
	[mabarcode] [nvarchar](25) NULL,
	[maqrcode] [nvarchar](50) NULL,
	[ngaylap] [datetime] NULL,
	[nguoilap] [nvarchar](50) NULL,
	[ngaysua] [datetime] NULL,
	[nguoisua] [nvarchar](50) NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[Type_DM_NHANVIEN]    Script Date: 10/26/2019 08:37:56 ******/
CREATE TYPE [dbo].[Type_DM_NHANVIEN] AS TABLE(
	[Manvid] [nvarchar](50) NOT NULL,
	[Mabp] [nvarchar](15) NULL,
	[Macv] [nvarchar](15) NULL,
	[Manv] [nvarchar](15) NULL,
	[Tennv] [nvarchar](70) NULL,
	[Ngaysinh] [date] NULL,
	[Diachi] [nvarchar](255) NULL,
	[Dienthoai] [nvarchar](15) NULL,
	[Email] [nvarchar](50) NULL,
	[Ngaythuviec] [date] NULL,
	[Ngaychinhthuc] [date] NULL,
	[Ngaylap] [datetime] NULL,
	[Nguoilap] [nvarchar](50) NULL,
	[Ngaysua] [datetime] NULL,
	[Nguoisua] [nvarchar](50) NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[Type_DM_KHACHHANG]    Script Date: 10/26/2019 08:37:56 ******/
CREATE TYPE [dbo].[Type_DM_KHACHHANG] AS TABLE(
	[Makhid] [nvarchar](50) NOT NULL,
	[Makh] [nvarchar](50) NULL,
	[Maloai] [nvarchar](10) NULL,
	[Tenkh] [nvarchar](100) NULL,
	[Dienthoai] [nvarchar](50) NULL,
	[Diachi] [nvarchar](50) NULL,
	[Xa] [nvarchar](50) NULL,
	[Huyen] [nvarchar](50) NULL,
	[Tinh] [nvarchar](50) NULL,
	[Email] [nvarchar](50) NULL,
	[Mst] [nvarchar](50) NULL,
	[Sinhnhat] [datetime] NULL,
	[Mathe] [nvarchar](30) NULL,
	[Ngaycap] [date] NULL,
	[Ngayhethan] [date] NULL,
	[Nguon] [nvarchar](10) NULL,
	[Ghichu] [nvarchar](255) NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[Type_DM_BANGGIA_CHITIET]    Script Date: 10/26/2019 08:37:56 ******/
CREATE TYPE [dbo].[Type_DM_BANGGIA_CHITIET] AS TABLE(
	[banggiactid] [nvarchar](50) NOT NULL,
	[banggiaid] [nvarchar](50) NOT NULL,
	[mavung] [nvarchar](50) NULL,
	[spid] [nvarchar](50) NOT NULL,
	[masp] [nvarchar](50) NULL,
	[tensp] [nvarchar](50) NULL,
	[giagoc] [numeric](18, 3) NULL,
	[giasi] [numeric](18, 3) NULL,
	[giale] [numeric](18, 3) NULL,
	[ghichu] [nvarchar](50) NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[Type_BH_PHIEUBHCT]    Script Date: 10/26/2019 08:37:56 ******/
CREATE TYPE [dbo].[Type_BH_PHIEUBHCT] AS TABLE(
	[Phieubhctid] [nvarchar](50) NOT NULL,
	[Phieubhid] [nvarchar](50) NULL,
	[Mavach] [nvarchar](50) NULL,
	[Maspid] [nvarchar](50) NULL,
	[Masp] [nvarchar](20) NULL,
	[Tensp] [nvarchar](100) NULL,
	[Quycach] [nvarchar](50) NULL,
	[Dvt] [nvarchar](30) NULL,
	[SLThung] [numeric](18, 3) NULL,
	[Soluong] [numeric](18, 3) NULL,
	[Dongia] [numeric](18, 3) NULL,
	[Nguyente] [numeric](18, 3) NULL,
	[Vat] [numeric](6, 3) NULL,
	[TTVat] [numeric](15, 3) NULL,
	[Ck] [numeric](4, 0) NULL,
	[TTCk] [numeric](18, 3) NULL,
	[Thanhtien] [numeric](18, 3) NULL,
	[Quatang] [bit] NULL,
	[Ghiso] [bit] NULL,
	[Ghichu] [nvarchar](255) NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[Type_BH_PHIEUBH]    Script Date: 10/26/2019 08:37:56 ******/
CREATE TYPE [dbo].[Type_BH_PHIEUBH] AS TABLE(
	[Phieubhid] [nvarchar](50) NOT NULL,
	[Sophieu] [nvarchar](20) NULL,
	[Loaiphieu] [nvarchar](2) NULL,
	[Makho] [nvarchar](15) NULL,
	[Mald] [nvarchar](15) NULL,
	[Makh] [nvarchar](20) NULL,
	[Tenkh] [nvarchar](100) NULL,
	[Dienthoai] [nvarchar](15) NULL,
	[Diachi] [nvarchar](200) NULL,
	[LoaiKH] [nvarchar](10) NULL,
	[Khmoi] [bit] NULL,
	[Ngayct] [date] NULL,
	[Ngayps] [date] NULL,
	[Quay] [nvarchar](30) NULL,
	[Ca] [nvarchar](20) NULL,
	[Thungan] [nvarchar](50) NULL,
	[Nguyente] [numeric](18, 3) NULL,
	[Vat] [numeric](18, 3) NULL,
	[TTVat] [numeric](18, 3) NULL,
	[Ck] [numeric](4, 0) NULL,
	[TTCk] [numeric](18, 3) NULL,
	[Thanhtien] [numeric](18, 3) NULL,
	[Tientra] [numeric](18, 3) NULL,
	[Tienthoi] [numeric](18, 3) NULL,
	[Ghichu] [nvarchar](255) NULL,
	[Ngaylap] [datetime] NULL,
	[Nguoilap] [nvarchar](50) NULL,
	[Ngaysua] [datetime] NULL,
	[Nguoisua] [nvarchar](50) NULL,
	[Dongbo] [bit] NULL,
	[TKNo] [nvarchar](12) NULL,
	[TKCo] [nvarchar](12) NULL
)
GO
/****** Object:  UserDefinedFunction [dbo].[fn_ThangTruoc]    Script Date: 10/26/2019 08:37:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[fn_ThangTruoc](@Thang varchar(6))
 RETURNS VARCHAR(6)
BEGIN
    DECLARE @ThangTruoc VARCHAR(6), @Date Date;        
    SET @Date = convert(Date,'01/'+LEFT(@Thang,2)+ '/'+RIGHT(@Thang,4),103);        
    SET @Date = DATEADD(DD, -1,@Date);    
    IF LEN(CAST(MONTH(@Date) AS VARCHAR)) = 1 
	   SET @ThangTruoc = '0' + CAST(MONTH(@Date) AS VARCHAR) + CAST(YEAR(@Date) AS VARCHAR);
	ELSE
	   SET @ThangTruoc = CAST(MONTH(@Date) AS VARCHAR) + CAST(YEAR(@Date) AS VARCHAR);	   	   
	RETURN @ThangTruoc;	   
END;
GO
/****** Object:  UserDefinedFunction [dbo].[fn_NgayIn]    Script Date: 10/26/2019 08:37:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[fn_NgayIn](@Ngay nvarchar(15))  
   RETURNS NVARCHAR(30)
  AS
    BEGIN
       DECLARE @DAY DATE
       SET @DAY=CONVERT(DATE,@Ngay,103)
       
       RETURN N'Ngày '+ right('0'+CAST(DAY(@DAY) as nvarchar(2)),2)+N' tháng '+ RIGHT('0'+CAST(month(@DAY) as nvarchar(2)),2) + N' năm '+ CAST(YEAR(@DAY) AS NVARCHAR)
    END
GO
/****** Object:  UserDefinedFunction [dbo].[fn_NgayDauThang]    Script Date: 10/26/2019 08:37:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[fn_NgayDauThang](@Thangnam varchar(6))
RETURNS VARCHAR(15) 
BEGIN
    DECLARE @mNgaydauthang DATE, @RetNgaydauthang VARCHAR(15);
    IF len(@Thangnam) <> 6 
     SET @mNgaydauthang = convert(Date,'01/01/2010',103);
    ELSE
     BEGIN      
      SET @mNgaydauthang = convert(Date,'01/' + LEFT(@Thangnam,2) + '/'+RIGHT(@Thangnam,4),103);    
     END; 
     SET @RetNgaydauthang= LEFT(CONVERT(VARCHAR, @mNgaydauthang, 103), 10);
    RETURN @RetNgaydauthang;
END;
GO
/****** Object:  UserDefinedFunction [dbo].[fn_NgayCuoiThang]    Script Date: 10/26/2019 08:37:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[fn_NgayCuoiThang](@Thangnam varchar(6))  
RETURNS VARCHAR(15)   
BEGIN  
    DECLARE @mNgayDau DATE, @RetNgaydau VARCHAR(10);  
    IF len(@Thangnam) <> 6   
     SET  @mNgayDau = convert(Date,'01/01/2017',103);  
    ELSE  
     BEGIN        
      SET  @mNgayDau = convert(Date,'01/' + LEFT(@Thangnam,2) + '/'+RIGHT(@Thangnam,4),103);      
      SET  @mNgayDau = DATEADD(DD, -1, DATEADD(M, 1,  @mNgayDau))  
     END;   
     SET @RetNgaydau = LEFT(CONVERT(VARCHAR, @mNgayDau, 103), 10);  
    RETURN @RetNgaydau;  
END;
GO
/****** Object:  StoredProcedure [dbo].[BH_shwPhieuBHList]    Script Date: 10/26/2019 08:37:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[BH_shwPhieuBHList]      
 @LOAIP NVARCHAR(5),      
 @TUNGAY NVARCHAR(15),      
 @DENNGAY NVARCHAR(15),
 @CA NVARCHAR(15),
 @QUAY NVARCHAR(15), 
 @NGUOIDUNG NVARCHAR(50)      
AS      
 BEGIN      
   SET DATEFORMAT DMY      
   SET @CA=NULL;
   SET @QUAY=NULL;
         
   IF ISNULL(@LOAIP,'') IN('BH')      
     BEGIN      
       SELECT A.*,Loaikh='',B.mst,B.email      
       FROM BH_PHIEUBH A WITH(NOLOCK) LEFT JOIN DM_KHACHHANG B WITH(NOLOCK) ON A.makh=B.makh                                  
       WHERE loaiphieu=@LOAIP AND DATEDIFF(DAY,@TUNGAY,ngayct)>=0 AND DATEDIFF(DAY,ngayct,@DENNGAY)>=0   
             AND Ca= ISNULL(@CA,Ca) AND Quay= ISNULL(@QUAY,Quay) 
       ORDER BY sophieu ASC      
     END       
END
GO
/****** Object:  UserDefinedFunction [dbo].[fn_DonGia]    Script Date: 10/26/2019 08:37:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[fn_DonGia] 
( 
  @masp nvarchar(20),
  @denngay nvarchar(20),
  @loai nvarchar(5) 
  )
RETURNS numeric(18,3) 
AS
  BEGIN
      DECLARE @Dongia numeric(18,3)
      
      IF ISNULL(@loai,'')='SI'
        BEGIN
			  SET @Dongia=(SELECT TOP 1 giasi FROM DM_BANGGIA_CHITIET A WITH(NOLOCK) LEFT JOIN DM_BANGGIA B WITH(NOLOCK) ON A.banggiaid=B.banggiaid
			  WHERE A.masp=@masp and DATEDIFF(DAY,B.ngayapdung,@denngay)>=0 and DATEDIFF(DAY,@denngay,B.denngay)>=0
					AND ISNULL(B.apdung,0)=1 ORDER BY Mavung DESC)
        END
      ELSE
        BEGIN
        	  SET @Dongia=(SELECT TOP 1 giale FROM DM_BANGGIA_CHITIET A WITH(NOLOCK) LEFT JOIN DM_BANGGIA B WITH(NOLOCK) ON A.banggiaid=B.banggiaid
			  WHERE A.masp=@masp and DATEDIFF(DAY,B.ngayapdung,@denngay)>=0 and DATEDIFF(DAY,@denngay,B.denngay)>=0
					AND ISNULL(B.apdung,0)=1  ORDER BY Mavung DESC )		       
        END  
        
       RETURN ISNULL(@Dongia,0) 
    END
GO
/****** Object:  UserDefinedFunction [dbo].[fn_ThangKhoaSo]    Script Date: 10/26/2019 08:37:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[fn_ThangKhoaSo]  
(  
   @PhanHe Varchar(2),
   @Denngay smallDatetime  
)  
RETURNS NVARCHAR(6)  
AS  
BEGIN
	DECLARE @Thangkhoaso nvarchar(6),@Ngaykhoitao nvarchar(15)
	 
    SET @Thangkhoaso= (SELECT TOP 1 Thangnam FROM HT_KHOASO 
                       WHERE Phanhe=Isnull(@Phanhe,Phanhe) AND DATEDIFF(DAY,Ngaythangnam,@Denngay)>=0 
                       ORDER BY Ngaythangnam DESC)
    IF ISNULL(@Thangkhoaso,'')=''
      BEGIN
         SET @Ngaykhoitao=(SELECT PARA_VAL FROM HT_PARA WHERE PARA_NAME='SYS_INIT_DATE')
         IF ISNULL(@Ngaykhoitao,'')<>''
             SET @Thangkhoaso= SUBSTRING(Left(Convert(Varchar,@Ngaykhoitao, 103),10),4,2) + Right(Left(Convert(Varchar,@Ngaykhoitao, 103),10),4)  
          ELSE
            SET @Thangkhoaso= SUBSTRING(Left( Convert(NVARCHAR,DATEADD(year,-1,GETDATE()),103),4),0,2) + Right(Left( Convert(NVARCHAR,DATEADD(year,-1,GETDATE()),103) ,10),4)   
      END
     
     Return @Thangkhoaso
END
GO
/****** Object:  UserDefinedFunction [dbo].[fn_SoPhieu]    Script Date: 10/26/2019 08:37:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[fn_SoPhieu] 
( 
  @KyhieuKho NVARCHAR(2),
  @LoaiPhieu NVARCHAR(2),
  @Thang INT,
  @Nam INT,
  @Nguoidung Nvarchar(50)
  )
RETURNS Nvarchar(20) 
AS
  BEGIN
    DECLARE @SoTT int ,@SoPhieu nvarchar(20),@KyhieuNguoiDung nvarchar(2)
    
    SELECT TOP 1 @KyhieuNguoiDung=RIGHT('XX'+ISNULL(KYHIEU,''),1) 
    FROM HT_NGUOIDUNG where Taikhoan=@Nguoidung    
    
    IF ISNULL(@KyhieuNguoiDung,'')='' SET @KyhieuNguoiDung= 'X'         
    IF ISNULL(@KyhieuKho,'')=''        SET @KyhieuKho='A' 
    
    IF ISNULL(@LoaiPhieu,'') IN('PN','PX') 
     BEGIN                  
		   SELECT TOP 1 @SoTT=MAX(LEFT(RIGHT('0000000000'+ISNULL(Sophieu,''), 9),4)) 
		   FROM NX_PHIEUNX WITH(NOLOCK)      
		   WHERE MONTH(ngayct) = @Thang and YEAR(ngayct) =@Nam AND ISNULL(loaiphieu,'')=ISNULL(@LoaiPhieu,'')
				 AND ISNUMERIC(LEFT(RIGHT('0000000000'+ISNULL(sophieu,''), 9),4))=1
     END  
     
    ELSE IF ISNULL(@LoaiPhieu,'')='CX'  
     BEGIN 
       SELECT TOP 1 @SoTT=MAX(LEFT(RIGHT('0000000000'+ISNULL(Sophieu,''), 9),4))  
       FROM NX_CHANHXE WITH(NOLOCK)      
       WHERE MONTH(ngayct) = @Thang and YEAR(ngayct) =@Nam AND ISNUMERIC(LEFT(RIGHT('0000000000'+ISNULL(sophieu,''), 9),4))=1 
     END
      
    ELSE IF ISNULL(@LoaiPhieu,'')='BH'  
     BEGIN 
       SELECT TOP 1 @SoTT=MAX(LEFT(RIGHT('0000000000'+ISNULL(Sophieu,''), 9),4))  
       FROM BH_PHIEUBH WITH(NOLOCK)      
       WHERE MONTH(ngayct) = @Thang and YEAR(ngayct) =@Nam AND ISNUMERIC(LEFT(RIGHT('0000000000'+ISNULL(sophieu,''), 9),4))=1 
     END
    ELSE IF ISNULL(@LoaiPhieu,'') IN ('PT','PC')
		BEGIN
			SELECT TOP 1 @SoTT=MAX(LEFT(RIGHT('0000000000'+ISNULL(Sophieu,''), 9),4)) 
		   FROM tc_PHIEUTC WITH(NOLOCK)      
		   WHERE MONTH(ngayct) = @Thang and YEAR(ngayct) =@Nam AND ISNULL(loaiphieu,'')=ISNULL(@LoaiPhieu,'')
				 AND ISNUMERIC(LEFT(RIGHT('0000000000'+ISNULL(sophieu,''), 9),4))=1
		END
       
     DECLARE @ThangNam NVARCHAR(5)   
     SET @ThangNam=RIGHT('0'+CAST(@Thang AS NVARCHAR),2)+RIGHT(CAST(@Nam AS NVARCHAR),2)       
     IF ISNULL(@SoTT,0)=0 
          SET @SoPhieu=@KyhieuKho+@KyhieuNguoiDung+@LoaiPhieu+ '0001/'+@ThangNam
      ELSE 
          SET @SoPhieu=@KyhieuKho+@KyhieuNguoiDung+@LoaiPhieu+ right('00000'+cast(cast(@SoTT as int)+1 as nvarchar),4)+'/'+@ThangNam                              
          
      RETURN @SoPhieu 
    END
GO
/****** Object:  StoredProcedure [dbo].[HT_addPara]    Script Date: 10/26/2019 08:37:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[HT_addPara]  
  @tblPara dbo.Type_HT_Para readonly,  
  @Nguoidung nvarchar(50)  
AS  
 BEGIN  
   UPDATE HT_PARA SET PARA_VAL=B.PARA_VAL  
   FROM HT_PARA A INNER JOIN @tblPara B on A.PARA_NAME=b.PARA_NAME  
 END
GO
/****** Object:  StoredProcedure [dbo].[NX_shwPhieuList]    Script Date: 10/26/2019 08:37:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[NX_shwPhieuList]  
 @LOAIP NVARCHAR(5),  
 @TUNGAY NVARCHAR(15),  
 @DENNGAY NVARCHAR(15),  
 @NGUOIDUNG NVARCHAR(50)  
AS  
 BEGIN  
   SET DATEFORMAT DMY  
     
   IF ISNULL(@LOAIP,'') IN('PN','PX')  
     BEGIN  
       SELECT A.*,B.Tenkho,C.Lydo,D.Tenkh   
       FROM NX_PHIEUNX A LEFT JOIN DM_KHO B WITH(NOLOCK) ON A.makho=B.makho  
                         LEFT JOIN DM_LYDO C WITH(NOLOCK) ON A.mald=C.Mald  
                         LEFT JOIN DM_KHACHHANG D WITH(NOLOCK) ON A.makh=D.makh                              
       WHERE loaiphieu=@LOAIP AND DATEDIFF(DAY,@TUNGAY,ngayps)>=0 AND DATEDIFF(DAY,ngayps,@DENNGAY)>=0  
       ORDER BY sophieu ASC  
     END   
END
GO
/****** Object:  StoredProcedure [dbo].[NX_shwChanhxeList]    Script Date: 10/26/2019 08:37:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[NX_shwChanhxeList]  
 @LOAIP NVARCHAR(5),  
 @TUNGAY NVARCHAR(15),  
 @DENNGAY NVARCHAR(15),  
 @NGUOIDUNG NVARCHAR(50)  
AS  
 BEGIN  
   SET DATEFORMAT DMY  
     
   IF ISNULL(@LOAIP,'') IN('CX')  
     BEGIN  
       SELECT * FROM NX_CHANHXE WITH(NOLOCK)                       
       WHERE Loaiphieu=@LOAIP AND DATEDIFF(DAY,@TUNGAY,ngayps)>=0 AND DATEDIFF(DAY,ngayps,@DENNGAY)>=0  
       ORDER BY Sophieu ASC  
     END   
END
GO
/****** Object:  StoredProcedure [dbo].[mt_ChonPhieu]    Script Date: 10/26/2019 08:37:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[mt_ChonPhieu]  
 @LOAIP NVARCHAR(5),  
 @TUNGAY NVARCHAR(15),  
 @DENNGAY NVARCHAR(15),  
 @NGUOIDUNG NVARCHAR(50)  
AS  
 BEGIN  
   SET DATEFORMAT DMY  
     
   IF ISNULL(@LOAIP,'') IN('PN','PX')  
     BEGIN  
       SELECT Phieunxid,Sophieu,Ngayps,Ghichu FROM NX_PHIEUNX WITH(NOLOCK)   
       WHERE loaiphieu=@LOAIP AND DATEDIFF(DAY,@TUNGAY,ngayps)>=0 AND DATEDIFF(DAY,ngayps,@DENNGAY)>=0  
       ORDER BY sophieu ASC  
     END   
   ELSE IF ISNULL(@LOAIP,'')='CX'  
     BEGIN  
       SELECT Chanhxeid,Sophieu,Ngayps,Ghichu FROM NX_CHANHXE WITH(NOLOCK)  
       WHERE DATEDIFF(DAY,@TUNGAY,ngayps)>=0 AND DATEDIFF(DAY,ngayps,@DENNGAY)>=0  
       ORDER BY sophieu ASC  
     END  
   ELSE IF ISNULL(@LOAIP,'')='SP'  
     BEGIN  
       SELECT Maspid,Masp,Tensp,Dvt,Quycach FROM DM_SANPHAM WITH(NOLOCK) ORDER BY masp ASC  
     END    
         
 END
GO
/****** Object:  StoredProcedure [dbo].[spNX_SyncPostConfirm]    Script Date: 10/26/2019 08:37:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[spNX_SyncPostConfirm]  
   @PostResult nvarchar(4000),  
   @PosType nvarchar(10)  
 AS  
   BEGIN  
     IF ISNULL(@PosType,'')='NX'  
       UPDATE NX_PHIEUNX SET Dongbo=1 WHERE CHARINDEX(Phieunxid,ISNULL(@PostResult,''))>0  
     ELSE IF ISNULL(@PosType,'')='BH'  
        UPDATE BH_PHIEUBH SET Dongbo=1 WHERE  CHARINDEX(Phieubhid,ISNULL(@PostResult,''))>0  
     ELSE IF ISNULL(@PosType,'')='CX'  
        UPDATE NX_CHANHXE SET Dongbo=1 WHERE  CHARINDEX(Chanhxeid,ISNULL(@PostResult,''))>0  
          
   END
GO
/****** Object:  StoredProcedure [dbo].[sp_TC_DelPhieuTC]    Script Date: 10/26/2019 08:37:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_TC_DelPhieuTC]
 @Phieutcid nvarchar(50),
 @Nguoidung nvarchar(50),
 @Ketqua nvarchar(100) output
AS
 BEGIN
  SET @Ketqua=''
  DELETE TC_PHIEUTCCT WHERE phieutcid=@Phieutcid
  DELETE TC_PHIEUTC WHERE phieutcid=@Phieutcid
 END
GO
/****** Object:  StoredProcedure [dbo].[sp_TC_AddPhieuTC]    Script Date: 10/26/2019 08:37:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[sp_TC_AddPhieuTC]
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
GO
/****** Object:  StoredProcedure [dbo].[rptTC_Phieuthuchi]    Script Date: 10/26/2019 08:37:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[rptTC_Phieuthuchi]  
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
GO
/****** Object:  StoredProcedure [dbo].[rptDM_SanPham]    Script Date: 10/26/2019 08:37:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[rptDM_SanPham]
 As 
  Begin
    SELECT STT=ROW_NUMBER() OVER(PARTITION BY A.Manhomspid  ORDER BY  A.Manhomspid asc, A.Tensp asc),
    A.*,B.Tennhom FROM DM_SANPHAM A WITH(NOLOCK) LEFT JOIN DM_NHOMSP B WITH(NOLOCK) ON A.Manhomspid=B.Manhomspid
    ORDER BY A.Manhomspid asc, A.Tensp asc
  End
GO
/****** Object:  StoredProcedure [dbo].[TC_shwPhieuTCList]    Script Date: 10/26/2019 08:37:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[TC_shwPhieuTCList]  
 @LOAIP NVARCHAR(5),  
 @TUNGAY NVARCHAR(15),  
 @DENNGAY NVARCHAR(15),  
 @NGUOIDUNG NVARCHAR(50)  
AS  
 BEGIN  
   SET DATEFORMAT DMY  
     
   IF ISNULL(@LOAIP,'') IN('PT','PC')  
     BEGIN  
       SELECT A.*,B.Lydo as Lydo,C.Tenkh as Dt
       FROM TC_PHIEUTC A LEFT JOIN DM_LYDO B WITH(NOLOCK) ON A.Mald=B.Mald  
                         LEFT JOIN DM_KHACHHANG C WITH(NOLOCK) ON A.Madt=C.Makh                           
       WHERE A.Loaiphieu=@LOAIP AND DATEDIFF(DAY,@TUNGAY,A.Ngayps)>=0 AND DATEDIFF(DAY,A.Ngayps,@DENNGAY)>=0  
       ORDER BY A.Sophieu ASC  
     END   
END
GO
/****** Object:  StoredProcedure [dbo].[spHT_AddNhomQuyenChucNang]    Script Date: 10/26/2019 08:37:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[spHT_AddNhomQuyenChucNang]
 @nhomQuyenChucNangTbl dbo.Type_HT_NHOMQUYEN_CHUCNANG readonly,
 @nguoidung nvarchar(50),
 @ketqua nvarchar(255) output  
 AS
  BEGIN
    SET DATEFORMAT DMY
    SET @ketqua='' 
	IF NOT EXISTS(SELECT * FROM HT_NHOMQUYEN_CHUCNANG WITH(NOLOCK) 
                   WHERE soid IN(SELECT V1.soid FROM @nhomQuyenChucNangTbl V1))
		BEGIN
			 INSERT INTO HT_NHOMQUYEN_CHUCNANG(soid,manhom,macn,them,sua,xoa,duyet,[in],nguoilap,ngaylap,nguoisua,ngaysua)
			 SELECT CAST(NEWID() AS NVARCHAR(50)),manhom,macn,them,sua,xoa,duyet,[in],nguoilap,ngaylap,nguoisua,ngaysua from @nhomQuyenChucNangTbl
		END	
	ELSE
		BEGIN
			DECLARE @soid VARCHAR(50)
			DECLARE mCursor CURSOR FOR
			SELECT DISTINCT(soid) FROM @nhomQuyenChucNangTbl;
			
			OPEN mCursor
			FETCH NEXT FROM mCursor INTO @soid
			
			WHILE @@FETCH_STATUS = 0
			BEGIN
			
				UPDATE HT_NHOMQUYEN_CHUCNANG
				SET
					ngaysua = GETDATE(),
					nguoisua = @nguoidung,
					them = input.them,
					sua = input.sua,
					xoa = input.xoa,
					duyet = input.duyet,
					[in] = input.[in]
					FROM
						(SELECT them,sua,xoa,duyet,[in] FROM @nhomQuyenChucNangTbl WHERE soid = @soid) as input
				WHERE 
					soid = @soid
			
			FETCH NEXT FROM mCursor INTO @soid
			END
		
			CLOSE mCursor;
			DEALLOCATE mCursor;
		END
  END
GO
/****** Object:  StoredProcedure [dbo].[spHT_AddNhomQuyen]    Script Date: 10/26/2019 08:37:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[spHT_AddNhomQuyen]
 @NhomQuyenTbl dbo.Type_HT_NHOMQUYEN readonly,
 @nguoidung nvarchar(50)
AS
 BEGIN
 SET DATEFORMAT DMY
	
  IF NOT EXISTS(SELECT * FROM HT_NHOMQUYEN WITH(NOLOCK) 
                   WHERE manhom IN(SELECT V.manhom FROM @NhomQuyenTbl V))
      BEGIN
      
		DECLARE @mSoid nvarchar(50)
         SET @mSoid=CAST(NEWID() AS NVARCHAR(50))
         INSERT INTO HT_NHOMQUYEN(soid,manhom,tennhom,nguoilap,ngaylap,nguoisua,ngaysua,ngaysync)
         SELECT @mSoid,manhom,tennhom,nguoilap,ngaylap,nguoisua,ngaysua,ngaysync from @NhomQuyenTbl
      END
	END
GO
/****** Object:  StoredProcedure [dbo].[spHT_AddNguoiDung]    Script Date: 10/26/2019 08:37:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[spHT_AddNguoiDung]
 @nguoidungDT dbo.Type_HT_NGUOIDUNG readonly,
 @nguoidung nvarchar(50),
 @maNhomQuyen nvarchar(50)
 AS
  BEGIN
    SET DATEFORMAT DMY

    DECLARE @mSoid nvarchar(50)
    IF NOT EXISTS(SELECT * FROM HT_NGUOIDUNG WITH(NOLOCK) 
                   WHERE manv IN(SELECT V.manv FROM @nguoidungDT V))
      BEGIN
         SET @mSoid=CAST(NEWID() AS NVARCHAR(50))
         INSERT INTO HT_NGUOIDUNG(soid,manv,taikhoan,matkhau,hoten,nguoilap,ngaylap,nguoisua,ngaysua,ngaysync,isync,kyhieu)
         SELECT @mSoid,manv,taikhoan,matkhau,hoten,nguoilap,ngaylap,nguoisua,ngaysua,ngaysync,isync,kyhieu from @nguoidungDT
        
		 DECLARE @mNhomQuyenId nvarchar(50)
		 SET @mNhomQuyenId = (select soid from HT_NHOMQUYEN where manhom = @maNhomQuyen)
		 INSERT INTO HT_QUYENHAN(soid,soid_nguoidung,soid_nhomquyen,soid_congty,nguoilap,ngaylap,nguoisua,ngaysua)
		 values(CAST(NEWID() AS NVARCHAR(50)),@mSoid,@mNhomQuyenId,'',@nguoidung, GETDATE(),'',NULL)
      END
      else
      begin
		update  HT_NGUOIDUNG
		set
			ngaysua = GETDATE(),
			nguoisua = @nguoidung,
			taikhoan = nd.taikhoan,
			matkhau = nd.matkhau,
			kyhieu = nd.kyhieu
			from (select taikhoan,matkhau,kyhieu from @nguoidungDT) as nd
		where soid = (select soid from @nguoidungDT)
		
		 DECLARE @mNQuyenId nvarchar(50)
		 SET @mNQuyenId = (select soid from HT_NHOMQUYEN where manhom = @maNhomQuyen)
		 update HT_QUYENHAN
		 set ngaysua = GETDATE(),
		 nguoisua = @nguoidung,
		 soid_nhomquyen = @mNQuyenId
		 where soid_nguoidung = (select soid from @nguoidungDT)
      end

  END
GO
/****** Object:  StoredProcedure [dbo].[spDM_ImportSanPham]    Script Date: 10/26/2019 08:37:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[spDM_ImportSanPham]
 ( @tblSanPham dbo.Type_DM_SANPHAM readonly,
  @Nhomid nvarchar(50),
  @Loai nvarchar(10),
  @Nguoidung nvarchar(50)
  )
 AS
  BEGIN
     IF ISNULL(@Loai,'') IN ('MOI')
       BEGIN
         DELETE DM_SANPHAM WHERE Manhomspid=@Nhomid
         INSERT INTO DM_SANPHAM(Maspid,Manhomspid,Masp,Tensp,Dvt,Quycach,Quydoikgl,Quydoithung,Nhacungcap,Mabarcode,Maqrcode,Ngaylap,Nguoilap,Ngaysua,Nguoisua)
         SELECT CAST(NEWID() as nvarchar(50)),@Nhomid,masp,tensp,dvt,quycach,quydoikgl,quydoithung,nhacungcap,mabarcode,maqrcode,ngaylap,nguoilap,ngaysua,nguoisua FROM @tblSanPham
       END
     ELSE
       BEGIN
			DECLARE @soid VARCHAR(50)
			DECLARE mCursor CURSOR FOR
			SELECT DISTINCT(spid) FROM @tblSanPham;
			
			OPEN mCursor
			FETCH NEXT FROM mCursor INTO @soid
			
			WHILE @@FETCH_STATUS = 0
			BEGIN
				IF NOT EXISTS(SELECT * FROM DM_SANPHAM WITH(NOLOCK) 
                   WHERE Masp IN(SELECT V.masp FROM @tblSanPham V WHERE V.spid = @soid) and Manhomspid = @Nhomid )
					BEGIN
						INSERT INTO DM_SANPHAM(Maspid,Manhomspid,Masp,Tensp,Dvt,Quycach,Quydoikgl,Quydoithung,Nhacungcap,Mabarcode,Maqrcode,Ngaylap,Nguoilap,Ngaysua,Nguoisua)
						SELECT CAST(NEWID() as nvarchar(50)),@Nhomid,masp,tensp,dvt,quycach,quydoikgl,quydoithung,nhacungcap,mabarcode,maqrcode,ngaylap,nguoilap,ngaysua,nguoisua FROM @tblSanPham
						WHERE spid = @soid
					END
				ELSE
					BEGIN
						UPDATE DM_SANPHAM
						SET
							ngaysua = GETDATE(),
							nguoisua = @nguoidung,
							Masp = input.masp,
							Tensp = input.tensp,
							Dvt = input.dvt,
							Quycach = input.quycach,
							Quydoikgl = input.quydoikgl,
							Quydoithung = input.quydoithung
							FROM
								(SELECT masp,tensp,dvt,quycach,quydoikgl,quydoithung FROM @tblSanPham WHERE spid = @soid) as input
						WHERE 
							Maspid = @soid
					END	
			FETCH NEXT FROM mCursor INTO @soid
			END
		
			CLOSE mCursor;
			DEALLOCATE mCursor;
       END  
  END
GO
/****** Object:  StoredProcedure [dbo].[spDM_ImportBanggia]    Script Date: 10/26/2019 08:37:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[spDM_ImportBanggia]
 ( @tblBanggiaCT dbo.Type_DM_BANGGIA_CHITIET readonly,
  @Banggiaid nvarchar(50),
  @Loai nvarchar(10),
  @Nguoidung nvarchar(50)
  )
 AS
  BEGIN
     IF ISNULL(@Loai,'') IN ('MOI')
       BEGIN
         DELETE DM_BANGGIA_CHITIET WHERE Banggiaid=@Banggiaid
         INSERT INTO DM_BANGGIA_CHITIET(Banggiactid,Banggiaid,Maspid,Masp,Tensp,Giagoc,Giasi,Giale,Ghichu,Mavung)
         SELECT CAST(NEWID() as nvarchar(50)),@Banggiaid,spid,masp,tensp,giagoc,giasi,giale,ghichu,mavung FROM @tblBanggiaCT
       END
     else
       BEGIN
         DELETE DM_BANGGIA_CHITIET WHERE Masp IN(SELECT v.masp FROM @tblBanggiaCT v) and Banggiaid=@Banggiaid
         INSERT INTO DM_BANGGIA_CHITIET(Banggiactid,Banggiaid,Maspid,Masp,Tensp,Giagoc,Giasi,Giale,Ghichu,Mavung)
         SELECT CAST(NEWID() as nvarchar(50)),@Banggiaid,spid,masp,tensp,giagoc,giasi,giale,ghichu,mavung FROM @tblBanggiaCT
       END  
  END
GO
/****** Object:  StoredProcedure [dbo].[spDM_AddNhanVien]    Script Date: 10/26/2019 08:37:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[spDM_AddNhanVien]
 @nhanvien dbo.Type_DM_NHANVIEN readonly,
 @mabophan nvarchar(50),
 @nguoidung nvarchar(10)
 
 AS
  BEGIN
    SET DATEFORMAT DMY

    DECLARE @mSoid nvarchar(50)
    IF NOT EXISTS(SELECT * FROM DM_NHANVIEN WITH(NOLOCK) 
                   WHERE Manv IN(SELECT V.Manv FROM @nhanvien V) and Mabp = @mabophan)
      BEGIN
         insert into DM_NHANVIEN(Manvid,Mabp,Macv,Manv,Tennv,Ngaysinh,Diachi,Dienthoai,Email,Ngaythuviec,Ngaychinhthuc,Ngaylap,Nguoilap,Ngaysua,Nguoisua)
         select Manvid,@mabophan,Macv,Manv,Tennv,Ngaysinh,Diachi,Dienthoai,Email,Ngaythuviec,Ngaychinhthuc,Ngaylap,Nguoilap,Ngaysua,Nguoisua from @nhanvien
      END
     END
GO
/****** Object:  StoredProcedure [dbo].[spDM_AddKhachHang]    Script Date: 10/26/2019 08:37:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[spDM_AddKhachHang]
 @khachhang [dbo].[Type_DM_KHACHHANG] readonly,
 @nguoidung nvarchar(10)
 
 AS
  BEGIN
    SET DATEFORMAT DMY

    DECLARE @mSoid nvarchar(50)
    IF NOT EXISTS(SELECT * FROM DM_KHACHHANG WITH(NOLOCK) 
                   WHERE Makh IN(SELECT V.Makh FROM @khachhang V))
      BEGIN
         insert into DM_KHACHHANG(Makhid,Makh,Maloai,Tenkh,Dienthoai,Diachi,Xa,Huyen,Tinh,Email,Mst,Sinhnhat,Mathe,Ngaycap,Ngayhethan,Nguon,Ghichu)
         select Makhid,Makh,Maloai,Tenkh,Dienthoai,Diachi,Xa,Huyen,Tinh,Email,Mst,Sinhnhat,Mathe,Ngaycap,Ngayhethan,Nguon,Ghichu from @khachhang
      END
     END
GO
/****** Object:  StoredProcedure [dbo].[spCN_BaocaoList]    Script Date: 10/26/2019 08:37:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROC [dbo].[spCN_BaocaoList]
 @LoaiBC nvarchar(10),
 @Nguoidung nvarchar(50)
 As
  BEGIN
    IF ISNULL(@LoaiBC,'') IN('PTHU','PTHUTH')
      BEGIN
        SELECT Maloai='NGAY',TenLoai=N'Theo ngày'
        UNION ALL
        SELECT Maloai='THANG',TenLoai=N'Theo tháng'
        UNION ALL
        SELECT Maloai='QUY',TenLoai=N'Theo quý'
        UNION ALL
        SELECT Maloai='NAM',TenLoai=N'Theo năm'
        UNION ALL
        SELECT Maloai='6TDAU',TenLoai=N'Theo 6 tháng đầu năm'
        UNION ALL
        SELECT Maloai='6TCUOI',TenLoai=N'Theo 6 tháng cuối năm'
        
        SELECT * FROM DM_TAIKHOAN WHERE LEFT(ISNULL(Matk,'')+'000',3) IN('131') ORDER BY Matk ASC         
      END
     IF ISNULL(@LoaiBC,'')='PTRA'
      BEGIN
        SELECT Maloai='NGAY',TenLoai=N'Theo ngày'
        UNION ALL
        SELECT Maloai='THANG',TenLoai=N'Theo tháng'
        UNION ALL
        SELECT Maloai='QUY',TenLoai=N'Theo quý'
        UNION ALL
        SELECT Maloai='NAM',TenLoai=N'Theo năm'
        UNION ALL
        SELECT Maloai='6TDAU',TenLoai=N'Theo 6 tháng đầu năm'
        UNION ALL
        SELECT Maloai='6TCUOI',TenLoai=N'Theo 6 tháng cuối năm'
        
        SELECT * FROM DM_TAIKHOAN WHERE LEFT(ISNULL(Matk,'')+'000',3) IN('331') ORDER BY Matk ASC         
      END 
  END
GO
/****** Object:  View [dbo].[vTC_PhieuTC]    Script Date: 10/26/2019 08:37:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vTC_PhieuTC]   
AS      
SELECT Phieuid=A.Phieutcid,A.Mant,A.Madt,A.Loaiphieu,A.Sophieu,A.Ngayct,A.Ngayps,A.Tygia,A.Soctgoc,A.Nguyente,A.Sotien,A.Hoten,A.Nguoilap,
       A.Ngaylap,A.Nguoisua,A.Ngaysua,A.Ghiso,A.Mald,A.Ghichu,
       Phieuctid=B.Phieutcctid,B.Macp,B.Matk,B.TKNo,B.TKCo,NguyenteCT=B.Nguyente,ThanhtienCT=B.Thanhtien,B.Diengiai,B.SoHD,B.NChono 
FROM dbo.TC_PHIEUTC A WITH (NOLOCK) INNER JOIN dbo.TC_PHIEUTCCT B WITH (NOLOCK) ON A.Phieutcid = B.Phieutcid
GO
/****** Object:  StoredProcedure [dbo].[rptBH_InBill]    Script Date: 10/26/2019 08:37:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[rptBH_InBill]    
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
   
       
   SELECT STT=ROW_NUMBER()OVER(PARTITION BY Loai ORDER BY B.Loai,B.Masp asc),     
          A.Phieubhid,A.Sophieu,A.Ngayct,A.Ngaylap,A.Makh,A.Tenkh,A.Dienthoai,A.Diachi,A.Quay,A.Ca,A.Thungan,    
          Tienhang=A.Nguyente,TongCk=A.Ck,TongTTCk=A.TTCk,Thanhtoan=A.Thanhtien,A.Tientra,A.Tienthoi,A.Ghichu,     
          B.Loai,Diengiai=B.Tensp+' '+ B.Quycach, B.Masp,B.Tensp,B.Dvt,B.Quycach,B.Dongia,B.Soluong,B.Nguyente,B.Ck,B.TTCk,B.Thanhtien,GHICHUCT=B.Ghichu    
   FROM BH_PHIEUBH A LEFT JOIN @BHCT B ON A.Phieubhid=B.Phieubhid    
   WHERE A.Phieubhid=@Phieubhid    
   ORDER BY B.Loai,B.Masp ASC     
 END
GO
/****** Object:  View [dbo].[vNX_PhieuNX]    Script Date: 10/26/2019 08:37:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vNX_PhieuNX]   
AS      
SELECT  Phieuid=A.Phieunxid,A.Sophieu,A.Loaiphieu,A.Makho,A.Mald,A.TKNo,A.TKCo,A.Makh,A.Ngayct,A.Ngayps,A.Soctgoc,
        A.Loaitien,A.Tygia,A.Nguyente,A.Vat,A.TTVat,A.Thanhtien,A.Ghichu,A.Giaonhan,A.Ngaylap,A.Nguoilap,A.Ngaysua,A.Nguoisua,A.Dongbo,
        Phieuctid=B.Phieunxctid,B.Maspid,B.Masp,B.Tensp,B.Quycach,B.Dvt,B.Soluong,B.Dongia,NguyenteCT=B.Nguyente,VatCT=B.Vat,
        TTVatCT=B.TTVat,ThanhtienCT=B.Thanhtien,Nchono=B.Songaychono,B.SLThung,B.Ghiso,GhichuCT=B.Ghichu   
FROM dbo.NX_PHIEUNX A WITH (NOLOCK) INNER JOIN dbo.NX_PHIEUNXCT B WITH (NOLOCK) ON A.Phieunxid = B.Phieunxid
GO
/****** Object:  View [dbo].[vBH_PhieuBH]    Script Date: 10/26/2019 08:37:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vBH_PhieuBH]   
AS     
SELECT  Phieuid=A.Phieubhid,A.Sophieu,A.Loaiphieu,A.Makho,A.Mald,A.TKNo,A.TKCo,A.Makh,A.Tenkh,A.Dienthoai,A.Diachi,A.LoaiKH,A.Ngayct,A.Ngayps,
        A.Quay,A.Ca,A.Thungan,A.Nguyente,A.Vat,A.TTVat,A.Ck,A.TTCk,A.Thanhtien,A.Tientra,A.Tienthoi,A.Ghichu,A.Ngaylap,A.Nguoilap,
        A.Ngaysua,A.Nguoisua,A.Dongbo,Phieuctid=B.Phieubhctid,B.Mavach,B.Maspid,B.Masp,B.Tensp,B.Dvt,B.Quycach,B.Soluong,B.Dongia,
        NguyenteCT=B.Nguyente,VatCT=B.Vat,TTVatCT=B.TTVat,CkCT=B.Ck,TTCkCT=B.TTCk,ThanhtienCT=B.Thanhtien,B.Quatang,B.SLThung,
        B.QDThung,B.DGThung,B.Ghiso,GhichuCT=B.Ghichu 
FROM dbo.BH_PHIEUBH A WITH (NOLOCK) INNER JOIN dbo.BH_PHIEUBHCT B WITH (NOLOCK) ON A.Phieubhid = B.Phieubhid
GO
/****** Object:  StoredProcedure [dbo].[spBH_DelPhieuBH]    Script Date: 10/26/2019 08:37:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[spBH_DelPhieuBH]  
 @Phieubhid nvarchar(50),  
 @Nguoidung nvarchar(50),  
 @Ketqua nvarchar(100) output  
AS  
 BEGIN  
  SET @Ketqua=''  
  DELETE BH_PHIEUBHCT WHERE Phieubhid=@Phieubhid   
  DELETE BH_PHIEUBH WHERE Phieubhid=@Phieubhid  
 END
GO
/****** Object:  StoredProcedure [dbo].[spBH_AddPhieuBH]    Script Date: 10/26/2019 08:37:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[spBH_AddPhieuBH]      
 @Phieubh dbo.Type_BH_PHIEUBH readonly,      
 @Phieubhct dbo.Type_BH_PHIEUBHCT readonly,       
 @nguoidung nvarchar(50),      
 @ketqua nvarchar(255) output      
 AS      
  BEGIN      
    SET DATEFORMAT DMY      
          
    SET @ketqua=''      
    DECLARE @mPhieuid nvarchar(50)      
    DECLARE @dNguyente numeric(18,3),@dThanthien numeric(18,3), @dCK numeric(4),@dTTCk numeric(18,3)    
      
    IF NOT EXISTS(SELECT * FROM BH_PHIEUBH WITH(NOLOCK) WHERE Phieubhid IN(SELECT V.Phieubhid FROM @Phieubh V))      
      BEGIN    
         INSERT INTO BH_PHIEUBH(Phieubhid,Sophieu,Loaiphieu,Makho,Mald,Makh,Tenkh,Dienthoai,Diachi,LoaiKH,Ngayct,Ngayps,Quay,Ca,Thungan,Nguyente,  
                                Ck,TTCk,Thanhtien,Tientra,Tienthoi,Ghichu,Ngaylap,Nguoilap,TKNo,TKCo)      
         SELECT distinct Phieubhid,Sophieu,Loaiphieu,Makho,Mald,Makh,Tenkh,Dienthoai,Diachi,LoaiKH,Ngayct,Ngayps,Quay,Ca,Thungan,Nguyente,  
                               Ck,TTCk,Thanhtien,Tientra,Tienthoi,Ghichu,GETDATE(),@nguoidung,TKNo,TKCo    
         FROM @Phieubh     
             
         INSERT INTO BH_PHIEUBHCT(Phieubhctid,Phieubhid,Mavach,Maspid,Masp,Tensp,Dvt,Quycach,SLThung,Soluong,Dongia,Nguyente,Vat,TTVat,Ck,    
                                  TTCk,Thanhtien,Quatang,Ghiso,Ghichu)    
         SELECT Phieubhctid,Phieubhid,Mavach,Maspid,Masp,Tensp,Dvt,Quycach,ISNULL(SLThung,0),ISNULL(Soluong,0),ISNULL(Dongia,0),ISNULL(Nguyente,0),    
               ISNULL(Vat,0),ISNULL(TTVat,0),ISNULL(Ck,0),ISNULL(TTCk,0),ISNULL(Thanhtien,0),ISNULL(Quatang,0),ISNULL(Ghiso,0),Ghichu      
         FROM @Phieubhct                              
       END      
    ELSE     
      BEGIN    
        UPDATE BH_PHIEUBH    
        SET sophieu=B.Sophieu,loaiphieu=B.loaiphieu,ngayct=B.ngayct,ngayps=B.ngayps,makh=B.makh,tenkh=B.Tenkh,Dienthoai=B.Dienthoai,Diachi=B.Diachi,Loaikh=B.LoaiKH,
            makho=B.makho,mald=B.mald,nguyente=B.Nguyente,thanhtien=B.Thanhtien,Ck=B.Ck,TTCk=B.TTCk,Tientra=B.Tientra,Tienthoi=B.Tienthoi,    
            ghichu=B.ghichu    
        FROM BH_PHIEUBH A INNER JOIN @Phieubh B ON A.Phieubhid=B.Phieubhid    
            
        DELETE BH_PHIEUBHCT WHERE Phieubhid IN(SELECT V.Phieubhid FROM @Phieubh V)    
        INSERT INTO BH_PHIEUBHCT(Phieubhctid,Phieubhid,Mavach,Maspid,Masp,Tensp,Dvt,Quycach,SLThung,Soluong,Dongia,Nguyente,Vat,TTVat,Ck,    
                                  TTCk,Thanhtien,Quatang,Ghiso,Ghichu)    
        SELECT Phieubhctid,Phieubhid,Mavach,Maspid,Masp,Tensp,Dvt,Quycach,ISNULL(SLThung,0),ISNULL(Soluong,0),ISNULL(Dongia,0),ISNULL(Nguyente,0),    
               ISNULL(Vat,0),ISNULL(TTVat,0),ISNULL(Ck,0),ISNULL(TTCk,0),ISNULL(Thanhtien,0),ISNULL(Quatang,0),ISNULL(Ghiso,0),Ghichu      
        FROM @Phieubhct      
      END  
      
    IF EXISTS(SELECT * FROM @Phieubh WHERE ISNULL(Khmoi,0)=1)
      BEGIN
        DECLARE @MAKHID NVARCHAR(50)
        SET @MAKHID=(SELECT TOP 1 A.Makhid FROM DM_KHACHHANG A WITH(NOLOCK) INNER JOIN @Phieubh B ON A.Makh=B.Makh OR A.Dienthoai=B.Dienthoai)
        IF ISNULL(@MAKHID,'')=''
          BEGIN
            INSERT INTO DM_KHACHHANG(Makhid,Makh,Tenkh,Diachi,Dienthoai,Maloai,Nguon)
            SELECT CAST(NEWID() AS NVARCHAR(50)),Makh,Tenkh,Diachi,Dienthoai,LoaiKH,'BH' FROM @Phieubh
          END 
      END   
  END
GO
/****** Object:  StoredProcedure [dbo].[rptDM_InMaQRCode]    Script Date: 10/26/2019 08:37:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[rptDM_InMaQRCode]      
    @tblMasp dbo.Type_SP_MASP readonly,      
    @Loai tinyint,      
    @Cogia bit,  
    @Thung bit,    
    @Denngay nvarchar(15),      
    @Nguoidung nvarchar(50)      
  AS      
    BEGIN      
     --QRCODE  
     --BARCODE:MAQG=893+MADN=01+MANHOM=01+MASP=00001'
     SET DATEFORMAT DMY           
     DECLARE @Masp nvarchar(20), @Soluong int,@Kyhieuthung nvarchar(1)  
     DECLARE @tblBill AS TABLE(Maspid NVARCHAR(50),Masp NVARCHAR(20),Tensp nvarchar(100),Dvt nvarchar(30),Quycach nvarchar(50),  
                               Macode nvarchar(50),MacodeText nvarchar(50),Dongia numeric(18,3),STT int)  
        
     
     IF ISNULL(@Thung,0)=1
      BEGIN
       IF ISNULL(@Loai,0)=0
         SET @Kyhieuthung='A'
       ELSE 
         SET @Kyhieuthung='1'
      END
     ELSE
      BEGIN
       IF ISNULL(@Loai,0)=0
         SET @Kyhieuthung='B'
       ELSE 
         SET @Kyhieuthung='2'
      END
            
       
       --isnull(Maqrcode,masp)+'$'+CAST(250000 as nvarchar)   
       IF ISNULL(@Loai,0)=0      
        BEGIN      
          INSERT INTO @tblBill(Maspid,Masp,Tensp,Dvt,Quycach,Macode,MacodeText,Dongia,STT)  
          SELECT Maspid, Masp,Tensp,Dvt,Quycach,Macode=@Kyhieuthung+isnull(Maqrcode,masp),MacodeText=isnull(Maqrcode,masp),
                 Dongia=(CASE WHEN ISNULL(@Thung,0)=1 THEN dbo.fn_DonGia(Masp,@Denngay,'SI') ELSE   dbo.fn_DonGia(Masp,@Denngay,'LE') END),STT=1     
          FROM DM_SANPHAM WHERE masp IN(SELECT V.Masp FROM @tblMasp V)      
        END      
      ELSE      
        BEGIN      
          INSERT INTO @tblBill(Maspid,Masp,Tensp,Dvt,Quycach,Macode,MacodeText,Dongia,STT)  
           SELECT Maspid, Masp,Tensp,Dvt,Quycach,Macode=@Kyhieuthung+isnull(Mabarcode,Masp),MacodeText=isnull(Maqrcode,masp),
                  Dongia=(CASE WHEN ISNULL(@Thung,0)=1 THEN dbo.fn_DonGia(Masp,@Denngay,'SI') ELSE   dbo.fn_DonGia(Masp,@Denngay,'LE') END),STT=1       
          FROM DM_SANPHAM WHERE masp IN(SELECT V.Masp FROM @tblMasp V)      
        END         
           
        
     DECLARE C_INMA CURSOR FOR                               
     SELECT Masp,Soluong FROM @tblMasp ORDER BY Masp asc                   
     OPEN C_INMA                             
     FETCH NEXT FROM C_INMA INTO @Masp,@Soluong                             
     WHILE @@FETCH_STATUS = 0                               
       BEGIN                        
           DECLARE @iStt INT  
		   SET @iStt = 2  
		   WHILE (@iStt <=@Soluong)  
		   BEGIN   
			INSERT INTO @tblBill(Maspid,Masp,Tensp,Dvt,Quycach,Macode,MacodeText,Dongia,STT)  
			SELECT Maspid,Masp,Tensp,Dvt,Quycach,Macode,MacodeText,Dongia,@iStt FROM @tblBill WHERE Masp=@Masp AND STT=1  
			SET @iStt = @iStt+ 1  
		   END  
               
          FETCH NEXT FROM C_INMA INTO @Masp,@Soluong                            
       END  
       CLOSE C_INMA   
       DEALLOCATE C_INMA   
           
     SELECT * FROM @tblBill ORDER BY Masp,Stt asc  
   END
GO
/****** Object:  UserDefinedFunction [dbo].[fnHH_PhatSinhNX]    Script Date: 10/26/2019 08:37:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[fnHH_PhatSinhNX] (
  @Tungay Smalldatetime,@Denngay Smalldatetime
) 
  Returns @tblPSNX Table(Makho NVARCHAR(20),Masp Nvarchar(20),PSNhap  Numeric(18,3),TrigiaN numeric(18,3),PSXuat Numeric(18,3),TrigiaX numeric(18,3))  
  AS
	BEGIN
	   DECLARE @tblCTNX Table(Makho NVARCHAR(20),Masp Nvarchar(20),PSNhap  Numeric(18,3),TrigiaN numeric(18,3),PSXuat Numeric(18,3),TrigiaX numeric(18,3)) 
	   
	   INSERT INTO @tblCTNX(Makho,Masp,PSNhap,TrigiaN,PSXuat,TrigiaX) 
	   SELECT ISNULL(Makho,'NA'),B.Masp,
	         PSNhap=(CASE WHEN ISNULL(Loaiphieu,'')='PN' THEN ISNULL(B.Soluong,0) ELSE 0 END),
	         TrigiaN= (CASE WHEN ISNULL(Loaiphieu,'')='PN' THEN ISNULL(B.Soluong,0)* ISNULL(B.Dongia,0) ELSE 0 END), 
	         PSXuat=(CASE WHEN ISNULL(Loaiphieu,'')='PX' THEN ISNULL(B.Soluong,0) ELSE 0 END),
	         TrigiaX= (CASE WHEN ISNULL(Loaiphieu,'')='PX' THEN ISNULL(B.Soluong,0)* ISNULL(B.Dongia,0) ELSE 0 END)
	   FROM NX_PHIEUNX A INNER JOIN NX_PHIEUNXCT B ON A.Phieunxid=B.Phieunxid
	   WHERE DATEDIFF(DAY,@Tungay,A.Ngayct)>=0 AND DATEDIFF(DAY,A.Ngayct,@Denngay)>=0
	   
	   INSERT INTO @tblPSNX(Makho,Masp,PSNhap,TrigiaN,PSXuat,TrigiaX)
	   SELECT Makho,Masp,Sum(Isnull(PSNhap,0)),Sum(Isnull(TrigiaN,0)),Sum(Isnull(PSXuat,0)),Sum(Isnull(TrigiaX,0))
	   FROM @tblCTNX GROUP BY  Makho,Masp
	   
	   RETURN	   
	END
GO
/****** Object:  StoredProcedure [dbo].[rptNX_Thongkeguichanh]    Script Date: 10/26/2019 08:37:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[rptNX_Thongkeguichanh]    
 @tblSophieu dbo.Type_SoPhieu readonly,
 @Ngaydau nvarchar(15),
 @Ngaycuoi nvarchar(15),  
 @Nguoidung nvarchar(50)  
 As     
  Begin   
    SET DATEFORMAT DMY
    
    DECLARE @NGAYD DATE, @NGAYC DATE;
    
    IF ISNULL(@Ngaydau,'')=''
      SET @NGAYD=GETDATE()
    ELSE
      SET @NGAYD=CONVERT(DATE,@Ngaydau,103)
      
    IF ISNULL(@Ngaycuoi,'')=''
      SET @NGAYC=GETDATE()
    ELSE
      SET @NGAYC=CONVERT(DATE,@Ngaycuoi,103)
  
   IF EXISTS(SELECT * FROM @tblSophieu)
     BEGIN     
			SELECT GRP_TAG=A.Chanhxeid, A.Chanhxeid,Sophieu,A.Ngayct,A.Ngayps,A.Soxe,A.Taixe,A.Loaixe,A.Dienthoaixe,A.Makh,A.Tenkh,A.Dienthoai,A.Diachi,  
				   A.Ghichu,A.Trongluong,A.Chiphi,A.HTThanhtoan,  
				   NgayPhieu=N'Ngày '+right('0'+cast(DAY(A.Ngayct) as nvarchar(2)),2)+N' Tháng '+ right('0'+cast(Month(A.Ngayct) as nvarchar(2)),2)+N' Năm '+ CAST(YEAR(A.Ngayct) as nvarchar),  
				   STT=ROW_NUMBER() OVER(PARTITION BY A.Chanhxeid  ORDER BY  A.Chanhxeid asc,B.Masp asc),  
				   B.Maspid,B.Masp,Hanghoa=ISNULL(C.Tensp,'')+'('+isnull(C.Quycach,0)+')',   
				   C.Tensp,C.Dvt,C.Quycach,SoluongCT=0,B.Soluong,GhichuHH=B.Ghichu   
			FROM NX_CHANHXE A WITH(NOLOCK) INNER JOIN NX_CHANHXECT B WITH(NOLOCK)  ON A.Chanhxeid=B.Chanhxeid  
										   LEFT JOIN DM_SANPHAM C ON B.Masp=C.Masp   
		                                   
			WHERE A.Sophieu IN (SELECT Sophieu FROM @tblSophieu v)  
			ORDER BY  A.Chanhxeid asc,B.Masp asc   
     END
   ELSE
     BEGIN
          SELECT GRP_TAG=A.Chanhxeid, A.Chanhxeid,Sophieu,A.Ngayct,A.Ngayps,A.Soxe,A.Taixe,A.Loaixe,A.Dienthoaixe,A.Makh,A.Tenkh,A.Dienthoai,A.Diachi,  
				   A.Ghichu,A.Trongluong,A.Chiphi,A.HTThanhtoan,  
				   NgayPhieu=N'Ngày '+right('0'+cast(DAY(A.Ngayct) as nvarchar(2)),2)+N' Tháng '+ right('0'+cast(Month(A.Ngayct) as nvarchar(2)),2)+N' Năm '+ CAST(YEAR(A.Ngayct) as nvarchar),  
				   STT=ROW_NUMBER() OVER(PARTITION BY A.Chanhxeid  ORDER BY  A.Chanhxeid asc,B.Masp asc),  
				   B.Maspid,B.Masp,Hanghoa=ISNULL(C.Tensp,'')+'('+isnull(C.Quycach,0)+')',   
				   C.Tensp,C.Dvt,C.Quycach,SoluongCT=0,B.Soluong,GhichuHH=B.Ghichu   
			FROM NX_CHANHXE A WITH(NOLOCK) INNER JOIN NX_CHANHXECT B WITH(NOLOCK)  ON A.Chanhxeid=B.Chanhxeid  
										   LEFT JOIN DM_SANPHAM C ON B.Masp=C.Masp   
		                                   
			WHERE DATEDIFF(DAY,@NGAYD,A.Ngayct)>=0 AND DATEDIFF(DAY,A.Ngayct,@NGAYC)>=0
			ORDER BY  A.Chanhxeid asc,B.Masp asc   
     END			
  End
GO
/****** Object:  StoredProcedure [dbo].[rptNX_Phieunhapxuat]    Script Date: 10/26/2019 08:37:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[rptNX_Phieunhapxuat]  
 @Phieunxid nvarchar(50),
 @Nguoidung nvarchar(50)
 As   
  Begin  
    
    SELECT A.Phieunxid,A.Sophieu,A.Ngayct,A.Ngayps,A.Soctgoc,A.Giaonhan,C.Tenkh,D.Makho,D.Tenkho,E.Lydo,D.Diachi,A.Ghichu,
           NgayPhieu=N'Ngày '+right('0'+cast(DAY(A.Ngayct) as nvarchar(2)),2)+N' Tháng '+ right('0'+cast(Month(A.Ngayct) as nvarchar(2)),2)+N' Năm '+ CAST(YEAR(A.Ngayct) as nvarchar),
           STT=ROW_NUMBER() OVER(PARTITION BY A.Phieunxid  ORDER BY  A.Phieunxid asc,B.Masp asc),
           B.Maspid,B.Masp,Hanghoa=ISNULL(B.Tensp,'')+'('+isnull(B.Quycach,0)+')', 
           B.Tensp,B.Dvt,B.Quycach,SoluongCT=0,B.Soluong,B.Dongia,B.Vat,B.TTVat,B.Thanhtien,B.Ghichu
    FROM NX_PHIEUNX A WITH(NOLOCK) INNER JOIN NX_PHIEUNXCT B WITH(NOLOCK)  ON A.Phieunxid=B.Phieunxid
                                   LEFT JOIN DM_KHACHHANG C WITH(NOLOCK) ON A.Makh=C.Makh
                                   LEFT JOIN DM_KHO D WITH(NOLOCK) ON A.Makho=D.Makho
                                   LEFT JOIN DM_LYDO E WITH(NOLOCK) ON A.Mald=E.Mald
    WHERE A.Phieunxid=@Phieunxid
    ORDER BY  A.Phieunxid asc,B.Masp asc 
  End
GO
/****** Object:  StoredProcedure [dbo].[rptNX_Phieunchanh]    Script Date: 10/26/2019 08:37:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[rptNX_Phieunchanh]  
 @Phieuid nvarchar(50),
 @Nguoidung nvarchar(50)
 As   
  Begin   
    SELECT A.Chanhxeid,Sophieu,A.Ngayct,A.Ngayps,A.Soxe,A.Taixe,A.Loaixe,A.Dienthoaixe,A.Makh,A.Tenkh,A.Dienthoai,A.Diachi,
           A.Ghichu,A.Trongluong,A.Chiphi,A.HTThanhtoan,
           NgayPhieu=N'Ngày '+right('0'+cast(DAY(A.Ngayct) as nvarchar(2)),2)+N' Tháng '+ right('0'+cast(Month(A.Ngayct) as nvarchar(2)),2)+N' Năm '+ CAST(YEAR(A.Ngayct) as nvarchar),
           STT=ROW_NUMBER() OVER(PARTITION BY A.Chanhxeid  ORDER BY  A.Chanhxeid asc,B.Masp asc),
           B.Maspid,B.Masp,Hanghoa=ISNULL(C.Tensp,'')+'('+isnull(C.Quycach,0)+')', 
           C.Tensp,C.Dvt,C.Quycach,SoluongCT=0,B.Soluong,GhichuHH=B.Ghichu 
    FROM NX_CHANHXE A WITH(NOLOCK) INNER JOIN NX_CHANHXECT B WITH(NOLOCK)  ON A.Chanhxeid=B.Chanhxeid
                                   LEFT JOIN DM_SANPHAM C ON B.Masp=C.Masp 
                                 
    WHERE A.Chanhxeid=@Phieuid
    ORDER BY  A.Chanhxeid asc,B.Masp asc 
  End
GO
/****** Object:  StoredProcedure [dbo].[rptNX_Baocaobanhang]    Script Date: 10/26/2019 08:37:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[rptNX_Baocaobanhang]      
 @tblSanpham dbo.Type_Masp readonly,  
 @Ngaydau nvarchar(15),  
 @Ngaycuoi nvarchar(15),    
 @Nguoidung nvarchar(50) ,
 @Ngayin nvarchar(50) OUTPUT   
 As       
  Begin     
    SET DATEFORMAT DMY  
      
    DECLARE @NGAYD DATE, @NGAYC DATE;  
      
    IF ISNULL(@Ngaydau,'')=''  
      SET @NGAYD=GETDATE()  
    ELSE  
      SET @NGAYD=CONVERT(DATE,@Ngaydau,103)  
        
    IF ISNULL(@Ngaycuoi,'')=''  
      SET @NGAYC=GETDATE()  
    ELSE  
      SET @NGAYC=CONVERT(DATE,@Ngaycuoi,103)  
    
   set @Ngayin=dbo.fn_NgayIn(CONVERT(NVARCHAR,GETDATE(),103))
   
   DECLARE @tblTHPX AS TABLE(Sophieu NVARCHAR(30), Ngayct date,Masp nvarchar(20), 
                             Soluong numeric(18,3),Dongia numeric(18,3),Thanhtien numeric(18,3),TTVat numeric(18,3))
   DECLARE @tblTHBH AS TABLE(Sophieu NVARCHAR(30), Ngayct date,Masp nvarchar(20), 
                             Soluong numeric(18,3),Dongia numeric(18,3),Thanhtien numeric(18,3),TTCK numeric(18,3))
  
   DECLARE @tblBCBH AS TABLE(Masp nvarchar(20),Tensp nvarchar(100),Dvt nvarchar(30), Quycach nvarchar(50), 
                             Soluong numeric(18,3),Dongia numeric(18,3),Thanhtien numeric(18,3),Tralai numeric(18,3),
                             Phidv numeric(18,3),TTCK numeric(18,3), TTVat numeric(18,3), Tongcong numeric(18,3))
                             
   
   INSERT INTO @tblTHPX(Sophieu,Ngayct,Masp,Soluong,Dongia,Thanhtien,TTVat)
   SELECT  A.Sophieu,A.Ngayct,B.Masp,B.Soluong,B.Dongia,B.Thanhtien,B.TTVat
   FROM NX_PHIEUNX A LEFT JOIN NX_PHIEUNXCT B ON A.Phieunxid=B.Phieunxid
   WHERE DATEDIFF(DAY,@NGAYD,Ngayct)>=0 AND DATEDIFF(DAY,Ngayct,@NGAYC)>=0 AND A.Loaiphieu = 'PX'
     
   INSERT INTO @tblTHBH(Sophieu,Ngayct,Masp,Soluong,Dongia,Thanhtien,TTCK)
   SELECT  A.Sophieu,A.Ngayct,B.Masp,B.Soluong,B.Dongia,B.Thanhtien,B.TTVat
   FROM BH_PHIEUBH A LEFT JOIN BH_PHIEUBHCT B ON A.Phieubhid=B.Phieubhid
   WHERE DATEDIFF(DAY,@NGAYD,Ngayct)>=0 AND DATEDIFF(DAY,Ngayct,@NGAYC)>=0
    
   INSERT INTO @tblBCBH(Masp,Soluong,Dongia,Thanhtien,Tralai,Phidv,TTVat,TTCK)
   SELECT Masp,SUM(ISNULL(A.Soluong,0)),MAX(ISNULL(A.Dongia,0)),SUM(ISNULL(Thanhtien,0)),SUM(Tralai),SUM(Phidv),SUM(TTVat),SUM(TTCk)
    FROM(
	   SELECT Masp,Soluong,Dongia,Thanhtien,Tralai=0,Phidv=0,TTVat,TTCk=0 FROM @tblTHPX 
		 UNION ALL
	   SELECT Masp,Soluong,Dongia,Thanhtien,Tralai=0,Phidv=0,TTVat=0,TTCk FROM @tblTHBH 
   )A GROUP BY Masp
   
   
   UPDATE @tblBCBH SET Tensp=B.Tensp,Dvt=B.Dvt,Quycach=B.Quycach
   FROM @tblBCBH A INNER JOIN DM_SANPHAM B ON A.Masp=B.Masp  
   
   UPDATE @tblBCBH SET Tongcong=Thanhtien- (Tralai+Phidv+TTVat+TTCK)
       
   IF EXISTS(SELECT * FROM @tblSanpham)  
	 SELECT STT=ROW_NUMBER()OVER(ORDER BY Tensp asc),* 	        
	 FROM @tblBCBH WHERE Masp IN(SELECT Masp from @tblSanpham v)
	 ORDER BY Tensp ASC
   ELSE
    SELECT STT=ROW_NUMBER() OVER(ORDER BY Tensp asc),*
    FROM @tblBCBH ORDER BY Tensp ASC
	 
  END
GO
/****** Object:  StoredProcedure [dbo].[spNX_SyncPost]    Script Date: 10/26/2019 08:37:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[spNX_SyncPost]
@NguoiDung nvarchar(50)
AS
BEGIN

SELECT Phieuid=A.Phieunxid,A.Sophieu,A.Loaiphieu,A.Makho,A.Mald,A.Makh,A.Ngayct,A.Ngayps,A.Soctgoc,A.Loaitien,A.Tygia,NguyenteH=A.Nguyente,VatH=A.Vat,TTVatH=A.TTVat,ThanhtienH=A.Thanhtien,GhichuH=A.Ghichu,a.Giaonhan,A.Ngaylap,A.Nguoilap,
Phieuctid=B.Phieunxctid,B.Maspid,B.Masp,B.Tensp,B.Dvt,B.Quycach,B.Soluong,b.Dongia,B.Nguyente,B.Vat,B.TTVat,B.Thanhtien,B.Songaychono,B.SLThung,B.Ghiso,B.Ghichu
FROM NX_PHIEUNX A INNER JOIN NX_PHIEUNXCT B ON A.Phieunxid=B.Phieunxid
WHERE ISNULL(Dongbo,0)=0
ORDER BY A.Phieunxid,B.Phieunxctid ASC

SELECT Phieuid=A.Phieubhid,A.Sophieu,A.Loaiphieu,A.Makho,A.Mald,A.Makh,A.Tenkh,A.Dienthoai,A.Diachi,A.LoaiKH,A.Ngayct,A.Ngayps,A.Quay,A.Ca,A.Thungan,
NguyenteH=A.Nguyente,VatH=A.Vat,TTVatH=A.TTVat,CkH=A.Ck,TTCkH=A.TTCk,ThanhtienH=A.Thanhtien,Tientra,Tienthoi,GhichuH=A.Ghichu,Ngaylap,Nguoilap,
Phieuctid=B.Phieubhctid,B.Maspid,B.Masp,B.Tensp,B.Dvt,B.Quycach,B.Soluong,B.Dongia,B.Nguyente,B.Vat,B.TTVat,B.Ck,B.TTCk,B.Thanhtien,B.Quatang,B.SLThung,B.Ghiso,B.Ghichu
FROM BH_PHIEUBH A INNER JOIN BH_PHIEUBHCT B ON A.Phieubhid=B.Phieubhid
WHERE ISNULL(Dongbo,0)=0
ORDER BY A.Phieubhid,B.Phieubhctid ASC


SELECT TOP 0 Phieuid=A.Chanhxeid,A.Sophieu,A.Loaiphieu,A.Ngayct,A.Ngayps,A.Makh,A.Tenkh,A.Diachi,A.Dienthoai,A.Email,A.Soxe,A.Loaixe,A.Taixe,
A.Dienthoaixe,A.Trongluong,A.Chiphi,A.HTThanhtoan,A.Trangthai,A.Ghichu,A.Ngaylap,A.Nguoilap,
Phieuctid=B.Chanhxectid,B.Masp,B.SLThung,B.Soluong,B.Chiphi,B.Ghichu
FROM NX_CHANHXE A INNER JOIN NX_CHANHXECT B ON A.Chanhxeid=B.Chanhxeid
WHERE ISNULL(Dongbo,0)=0
ORDER BY A.Chanhxeid,B.Chanhxeid ASC
END
GO
/****** Object:  StoredProcedure [dbo].[spNX_DelPhieuNX]    Script Date: 10/26/2019 08:37:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[spNX_DelPhieuNX]
 @Phieunxid nvarchar(50),
 @Nguoidung nvarchar(50),
 @Ketqua nvarchar(100) output
AS
 BEGIN
  SET @Ketqua=''
  DELETE NX_PHIEUNXCT WHERE phieunxid=@Phieunxid
  DELETE NX_PHIEUNX WHERE phieunxid=@Phieunxid
 END
GO
/****** Object:  StoredProcedure [dbo].[spNX_DelPhieuCX]    Script Date: 10/26/2019 08:37:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[spNX_DelPhieuCX]  
 @Phieucxid nvarchar(50),  
 @Nguoidung nvarchar(50),  
 @Ketqua nvarchar(100) output  
AS  
 BEGIN  
  SET @Ketqua=''  
  DELETE NX_CHANHXECT WHERE Chanhxeid=@Phieucxid
  DELETE NX_CHANHXE WHERE Chanhxeid=@Phieucxid 
 END
GO
/****** Object:  StoredProcedure [dbo].[spNX_AddPhieuNX]    Script Date: 10/26/2019 08:37:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[spNX_AddPhieuNX]      
 @tblPhieunx dbo.Type_NX_PHIEUNX readonly,      
 @tblPphieunxct dbo.Type_NX_PHIEUNXCT readonly,      
 @nguoidung nvarchar(50),      
 @ketqua nvarchar(255) output      
 AS      
  BEGIN      
    SET DATEFORMAT DMY      
          
    SET @ketqua=''      
    DECLARE @mPhieunxid nvarchar(50)      
    DECLARE @dNguyente numeric(18,3),@dTT_VAT numeric(18,3), @dThanthien numeric(18,3)    
        
    SELECT @dNguyente=SUM(isnull(Nguyente,0)),@dTT_VAT=SUM(ISNULL(TTVat,0)), @dThanthien=SUM(isnull(Thanhtien,0))     
    FROM @tblPphieunxct    
        
    IF NOT EXISTS(SELECT * FROM NX_PHIEUNX WITH(NOLOCK) WHERE Phieunxid IN(SELECT V.Phieunxid FROM @tblPhieunx V))      
      BEGIN      
         INSERT INTO NX_PHIEUNX(phieunxid,sophieu,loaiphieu,makho,mald,makh,ngayct,ngayps,soctgoc,loaitien,tygia,nguyente,vat,ttvat,thanhtien,ghichu,Giaonhan,ngaylap,nguoilap,dongbo,TKNo, TKCo)      
         SELECT distinct phieunxid,sophieu,loaiphieu,makho,mald,makh,ngayct,ngayps,soctgoc,loaitien,tygia,@dNguyente,vat,@dTT_VAT,@dThanthien,ghichu,Giaonhan,getdate(),@nguoidung,0, TKNo, TKCo     
         from @tblPhieunx  
             
         INSERT INTO NX_PHIEUNXCT(phieunxctid,phieunxid,Maspid,masp,tensp,quycach,dvt,SLThung,Soluong,dongia,nguyente,vat,TTVat,thanhtien,songaychono,Ghiso,ghichu)      
         SELECT phieunxctid,phieunxid,B.Maspid,A.masp,A.tensp,A.quycach,A.dvt,ISNULL(SLThung,0),ISNULL(soluong,0),ISNULL(dongia,0),ISNULL(nguyente,0),ISNULL(vat,0),ISNULL(TTVat,0),    
                ISNULL(thanhtien,0),ISNULL(songaychono,0),ISNULL(A.Ghiso,0),A.ghichu    
         FROM @tblPphieunxct A LEFT JOIN DM_SANPHAM B ON A.masp=B.masp    
      END      
    ELSE     
      BEGIN    
        UPDATE NX_PHIEUNX     
        SET sophieu=B.sophieu,loaiphieu=B.loaiphieu,ngayct=B.ngayct,ngayps=B.ngayps,loaitien=B.loaitien,    
            tygia=B.tygia,makh=B.makh,makho=B.makho,mald=B.mald,nguyente=@dNguyente,ttvat=@dTT_VAT,thanhtien=@dThanthien,    
            Giaonhan=B.Giaonhan,ghichu=B.ghichu    
        FROM NX_PHIEUNX A INNER JOIN @tblPhieunx B ON A.phieunxid=B.phieunxid    
            
        DELETE NX_PHIEUNXCT WHERE phieunxid IN(SELECT V.phieunxid FROM @tblPhieunx V)               
        INSERT INTO NX_PHIEUNXCT(phieunxctid,phieunxid,Maspid,masp,tensp,quycach,dvt,SLThung,soluong,dongia,nguyente,vat,TTVat,thanhtien,songaychono,Ghiso,ghichu)      
        SELECT DISTINCT phieunxctid,phieunxid,B.Maspid,A.masp,A.tensp,A.quycach,A.dvt,ISNULL(A.SLThung,0),ISNULL(soluong,0),ISNULL(dongia,0),ISNULL(nguyente,0),ISNULL(vat,0),ISNULL(TTVat,0),    
                      ISNULL(thanhtien,0),ISNULL(songaychono,0),ISNULL(A.Ghiso,0) ,A.ghichu    
        FROM @tblPphieunxct A LEFT JOIN DM_SANPHAM B ON A.masp=B.masp    
      END    
  END
GO
/****** Object:  StoredProcedure [dbo].[spNX_AddChanhXe]    Script Date: 10/26/2019 08:37:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[spNX_AddChanhXe]      
 @phieucx dbo.Type_NX_CHANHXE readonly,      
 @phieucxct dbo.Type_NX_CHANHXECT readonly,      
 @nguoidung nvarchar(50),      
 @ketqua nvarchar(255) output      
 AS      
  BEGIN      
    SET DATEFORMAT DMY      
          
    SET @ketqua=''       
    IF NOT EXISTS(SELECT * FROM NX_CHANHXE WHERE Chanhxeid IN(SELECT V.Chanhxeid FROM @phieucx V))      
      BEGIN       
         INSERT INTO NX_CHANHXE(Chanhxeid,Sophieu,Loaiphieu,Ngayct,Ngayps,Makh,Tenkh,Diachi,Dienthoai,Email,Soxe,Loaixe,Taixe,Dienthoaixe,Trongluong,Chiphi,HTThanhtoan,Trangthai,Ghichu,Ngaylap,Nguoilap)  
         SELECT Chanhxeid,Sophieu,Loaiphieu,Ngayct,Ngayps,Makh,Tenkh,Diachi,Dienthoai,Email,Soxe,Loaixe,Taixe,Dienthoaixe,Trongluong,Chiphi,HTThanhtoan,Trangthai,Ghichu,getdate(),@nguoidung   
         FROM @phieucx   
         
         INSERT INTO NX_CHANHXECT(Chanhxectid,Chanhxeid,Maspid,Masp,SLThung,Soluong,Chiphi,Ghichu)  
         SELECT CAST(newid() as nvarchar(50)),Chanhxeid,B.Maspid,A.Masp,SUM(ISNULL(SLThung,0)),SUM(ISNULL(Soluong,0)),SUM(ISNULL(Chiphi,0)),MAX(Ghichu)      
         FROM @phieucxct A LEFT JOIN DM_SANPHAM B ON A.Masp=B.Masp   
         GROUP BY Chanhxeid,B.Maspid,A.Masp  
      END      
    ELSE     
      BEGIN    
        UPDATE NX_CHANHXE  
        SET Sophieu=B.Sophieu,Loaiphieu=B.Loaiphieu,Ngayct=B.Ngayct,Ngayps=B.Ngayps,Makh=B.Makh,Tenkh=B.Tenkh,  
            Diachi=B.Diachi,Dienthoai=B.Dienthoai,Email=B.Email,Soxe=B.Soxe,Loaixe=B.Loaixe,  
            Taixe=B.Taixe,Dienthoaixe=B.Dienthoaixe,Chiphi=B.Chiphi,HTThanhtoan=B.HTThanhtoan,Trangthai=B.Trangthai,  
            Ghichu=B.Ghichu,Ngaysua=GETDATE(), Nguoisua=@nguoidung  
        FROM NX_CHANHXE A INNER JOIN @phieucx B ON A.Chanhxeid=B.Chanhxeid  
          
        DELETE NX_CHANHXECT WHERE Chanhxeid IN(SELECT V.Chanhxeid FROM @phieucx V)   
        INSERT INTO NX_CHANHXECT(Chanhxectid,Chanhxeid,Maspid,Masp,SLThung,Soluong,Chiphi,Ghichu)  
        SELECT CAST(newid() as nvarchar(50)),Chanhxeid,B.Maspid,A.Masp,SUM(ISNULL(SLThung,0)),SUM(ISNULL(Soluong,0)),SUM(ISNULL(Chiphi,0)),MAX(Ghichu)      
        FROM @phieucxct A LEFT JOIN DM_SANPHAM B ON A.Masp=B.Masp   
        GROUP BY Chanhxeid,B.Maspid,A.Masp  
      END    
  END
GO
/****** Object:  UserDefinedFunction [dbo].[fnHH_TonkhoDenngay]    Script Date: 10/26/2019 08:37:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION  [dbo].[fnHH_TonkhoDenngay] (@Denngay varchar(15))  
  RETURNS @RetTONKHO TABLE (Makho nvarchar(20),Masp nvarchar(20), Dauky numeric (18,0),TrigiaD numeric(18,3),
                            Nhap numeric (18,0),TrigiaN numeric(18,3),Xuat numeric(18,0),TrigiaX numeric(18,3),
                            Cuoiky numeric(18,0),TrigiaC numeric(18,3))     
AS   
BEGIN  
  DECLARE @Thangkhoaso varchar(6),@Tungay varchar(15); 
  DECLARE @tblTON AS TABLE(Makho nvarchar(20),Masp nvarchar(20), Dauky numeric (18,0),TrigiaD numeric(18,3),
                           Nhap numeric (18,0),TrigiaN numeric(18,3),Xuat numeric(18,0),TrigiaX numeric(18,3),
                           Cuoiky numeric(18,0),TrigiaC numeric(18,3))

  --[LẤY CK THÁNG KHÓA SỔ GẦN NHẤT]
  SET @Thangkhoaso= dbo.fn_ThangKhoaSo('HH',@Denngay);  
  -- LẤY NGÀY CUỐI CỦA THÁNG KHÓA SỔ + 1 = NGÀY ĐẦU 
  SET @Tungay=dbo.fn_NgayCuoiThang(@Thangkhoaso); 
  IF ISNULL(@Tungay,'')<>'' SET @Tungay=Convert(NVARCHAR,DATEADD(DAY,1,@Tungay),103)

  INSERT INTO @tblTON(Makho,Masp,Dauky,TrigiaD,Nhap,TrigiaN,Xuat,TrigiaX)
  SELECT Makho,Masp,Dauky=Cuoiky,TrigiaD=TrigiaC,PSNhap=0,TrigiaN=0,PSXuat=0,TrigiaX=0 FROM NX_KETSOTON WHERE Thangnam=@Thangkhoaso
  UNION ALL
   SELECT Makho,Masp,Dauky=0,TrigiaD=0,PSNhap,TrigiaN,PSXuat,TrigiaX FROM dbo.fnHH_PhatSinhNX(@Tungay,@Denngay)

   UPDATE @tblTON 
   SET Cuoiky=ISNULL(Dauky,0)+ISNULL(Nhap,0)-ISNULL(Xuat,0),
       TrigiaC=ISNULL(TrigiaD,0)+ISNULL(TrigiaN,0)-ISNULL(TrigiaX,0)           
 
 
   INSERT INTO @RetTONKHO(Makho,Masp,Dauky,TrigiaD,Nhap,TrigiaN,Xuat,TrigiaX,Cuoiky,TrigiaC)
   SELECT Makho,Masp,Sum(isnull(Dauky,0)),Sum(Isnull(TrigiaD,0)),SUM(ISNULL(Nhap,0)),Sum(Isnull(TrigiaN,0)),
         SUM(ISNULL(Xuat,0)),Sum(Isnull(TrigiaX,0)),SUM(ISNULL(Cuoiky,0)),Sum(Isnull(TrigiaC,0)) 
   FROM @tblTON GROUP BY Makho,Masp
   
  RETURN;    
END;
GO
/****** Object:  UserDefinedFunction [dbo].[fnCN_Gom_Taikhoan]    Script Date: 10/26/2019 08:37:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[fnCN_Gom_Taikhoan] (@Tungay varchar(10),@Denngay varchar(10))  
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
GO
/****** Object:  UserDefinedFunction [dbo].[fnCN_Canthu]    Script Date: 10/26/2019 08:37:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[fnCN_Canthu](@Tungay varchar(10),@Denngay varchar(10), @Matk varchar(12),@Mant varchar(10))   
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
         NgayDH=DATEADD(DD,ISNULL(A.Nchono,0),A.Ngayps),A.NguyenteCT,A.TKno,A.TKCo,SotienQH=0,
         Diengiai= B.Lydo,A.Nguoilap 
  FROM vTC_PhieuTC A LEFT JOIN DM_LYDO B ON A.Mald=B.Mald
  
 RETURN;  
END;
GO
/****** Object:  StoredProcedure [dbo].[spHT_Ketsoton]    Script Date: 10/26/2019 08:37:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[spHT_Ketsoton]
 @Thangnam nvarchar(6),
 @Nguoidung nvarchar(50),
 @Output tinyint output
AS
 BEGIN
   SET DATEFORMAT DMY
   DECLARE @Thangtruoc nvarchar(6),@Ngaydau smalldatetime, @Ngaycuoi smalldatetime
   SET @Thangtruoc=dbo.fn_Thangtruoc(@Thangnam)
   SET @Ngaydau=CONVERT(DATE, dbo.fn_NgayDauThang(@Thangnam),103)
   SET @Ngaycuoi=CONVERT(DATE,dbo.fn_NgayCuoiThang(@Thangnam),103)
   
    IF EXISTS(SELECT * FROM NX_KETSOTON WHERE Thangnam=@Thangtruoc)      
      IF NOT EXISTS(SELECT * FROM NX_KETSOTON WHERE Thangnam=@Thangtruoc AND ISNULL(Khoa,0)=1)
        BEGIN
          SET @Output=1
          RETURN
        END
  
  
   DECLARE @tblDAUKY AS TABLE(Makho nvarchar(20),Masp nvarchar(20),Cuoiky numeric(18,3),TrigiaC numeric(18,3))  
   DECLARE @tblPSNX AS TABLE(Makho nvarchar(20),Masp nvarchar(20),PSNhap numeric(18,3),TrigiaN numeric(18,3),PSXuat numeric(18,3),TrigiaX numeric(18,3))  
   DECLARE @tblKETSO AS TABLE(Thangnam nvarchar(6),Makho nvarchar(20),Masp nvarchar(20),Dauky numeric(18,3),TrigiaD numeric(18,3),
                              PSNhap numeric(18,3),TrigiaN numeric(18,3),PSXuat numeric(18,3),TrigiaX numeric(18,3),Cuoiky numeric(18,3),TrigiaC numeric(18,3))  
   --[LẤY SỐ DƯ ĐẦU KỲ THÁNG TRƯỚC]  
   INSERT INTO @tblDAUKY(Makho,Masp,Cuoiky,TrigiaC)  
   SELECT Makho,Masp,SUM(Cuoiky),SUM(ISNULL(TrigiaC,0)) 
   FROM NX_KETSOTON WHERE Thangnam=@Thangtruoc GROUP BY Makho, Masp  
  
  
   --[LẤY PS NHẬP XUẤT TRONG THÁNG]
   INSERT INTO @tblPSNX(Makho,Masp,PSNhap,TrigiaN,PSXuat,TrigiaX)
   SELECT Makho,Masp,PSNhap,TrigiaN,PSXuat,TrigiaX FROM dbo.fnHH_PhatSinhNX(@Ngaydau,@Ngaycuoi)   
   
   --[TÍNH TRỊ GIÁ CUỐI THEO ĐƠN GIÁ BÌNH QUÂN]--
   INSERT INTO @tblKETSO(Thangnam,Makho,Masp,Dauky,TrigiaD,PSNhap,TrigiaN,PSXuat,TrigiaX)
   SELECT Thangnam,Makho,Masp,SUM(ISNULL(Dauky,0)),SUM(ISNULL(TrigiaD,0)),SUM(ISNULL(PSNhap,0)),SUM(ISNULL(TrigiaN,0)),
          SUM(ISNULL(PSXuat,0)),SUM(ISNULL(TrigiaX,0)) 
   FROM(
	   SELECT Thangnam=@Thangnam,Makho,Masp,Dauky=Cuoiky,TrigiaD=TrigiaC,PSNhap=0,TrigiaN=0,PSXuat=0,TrigiaX=0 FROM @tblDAUKY
	   UNION ALL
	   SELECT Thangnam=@Thangnam,Makho,Masp,Dauky=0,TrigiaD=0,PSNhap,TrigiaN,PSXuat,TrigiaX FROM @tblPSNX   
   )A GROUP BY Thangnam,Makho,Masp
   
   UPDATE @tblKETSO 
   SET Cuoiky=ISNULL(Dauky,0)+ISNULL(PSNhap,0)-ISNULL(PSXuat,0),
       TrigiaC=ISNULL(TrigiaD,0)+ISNULL(TrigiaN,0)-ISNULL(TrigiaX,0)
       
   DELETE NX_KETSOTON WHERE Thangnam=@Thangnam
   
   INSERT INTO NX_KETSOTON(Ketsotonid,Thangnam,Makho,Masp,Dauky,TrigiaD,Nhap,TrigiaN,Xuat,TrigiaX,Cuoiky,TrigiaC,Khoa)
   SELECT  CAST(NEWID() AS NVARCHAR(50)), Thangnam,Makho,Masp,Dauky,TrigiaD,PSNhap,TrigiaN,PSXuat,TrigiaX,Cuoiky,TrigiaC,1 FROM @tblKETSO    
 END
GO
/****** Object:  StoredProcedure [dbo].[spHH_Xemtonkho]    Script Date: 10/26/2019 08:37:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROC [dbo].[spHH_Xemtonkho]  
 @tblSP dbo.Type_Masp READONLY,  
 @Denngay nvarchar(15),  
 @Nguoidung nvarchar(50)  
AS  
 BEGIN  
   SET DATEFORMAT DMY  
   IF EXISTS(SELECT * FROM @tblSP)  
     BEGIN         
       SELECT B.Masp,B.Tensp,B.Dvt,B.Quycach,A.Cuoiky,C.Makho,C.Tenkho   
       FROM fnHH_TonkhoDenngay(@Denngay) A INNER JOIN DM_SANPHAM B ON A.Masp=B.Masp  
                                               LEFT JOIN DM_KHO C ON A.Makho=C.Makho  
       WHERE A.Masp IN(SELECT v.Masp FROM @tblSP v)                                          
       ORDER BY Makho,Masp ASC                                          
     END  
   ELSE  
     BEGIN  
       SELECT B.Masp,B.Tensp,B.Dvt,B.Quycach,A.Cuoiky,C.Makho,C.Tenkho   
       FROM fnHH_TonkhoDenngay(@Denngay) A INNER JOIN DM_SANPHAM B ON A.Masp=B.Masp  
                                               LEFT JOIN DM_KHO C ON A.Makho=C.Makho   
       ORDER BY Makho,Masp ASC     
     END  
 END
GO
/****** Object:  StoredProcedure [dbo].[rptHH_Thekho]    Script Date: 10/26/2019 08:37:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[rptHH_Thekho]      
 @tblSanpham dbo.Type_Masp readonly,  
 @Ngaydau nvarchar(15),  
 @Ngaycuoi nvarchar(15),    
 @Nguoidung nvarchar(50) ,
 @Ngayin nvarchar(50) OUTPUT   
 As       
  Begin     
    SET DATEFORMAT DMY  
      
    DECLARE @NgayDK nvarchar(15), @NGAYD DATE, @NGAYC DATE;  
      
    IF ISNULL(@Ngaydau,'')=''  
      SET @NGAYD=GETDATE()  
    ELSE  
      SET @NGAYD=CONVERT(DATE,@Ngaydau,103)  
        
    IF ISNULL(@Ngaycuoi,'')=''  
      SET @NGAYC=GETDATE()  
    ELSE  
      SET @NGAYC=CONVERT(DATE,@Ngaycuoi,103)  
   
   SET @NgayDK=CONVERT(NVARCHAR,DATEADD(DAY,-1,@NGAYD),103)
    
   set @Ngayin=dbo.fn_NgayIn(CONVERT(NVARCHAR,GETDATE(),103))
   
   DECLARE @tblTHEKHO AS TABLE(Ngayct date,SophieuN nvarchar(20),SophieuX nvarchar(20),Ngayps date,
                             STT int,Masp nvarchar(20),Tensp nvarchar(100),Dvt nvarchar(30), Quycach nvarchar(50),
                             SLNhap numeric(18,3),SLXuat numeric(18,3),Cuoiky numeric(18,3),Diengiai nvarchar(255))
   
   DECLARE @tblPSNX AS TABLE(STT int,Ngayct date,SophieuN nvarchar(20),SophieuX nvarchar(20),NgayPS date,
                             Masp nvarchar(20),Tensp nvarchar(100),Dvt nvarchar(30), Quycach nvarchar(50),
                             Dauky numeric(18,3),SLNhap numeric(18,3),SLXuat numeric(18,3),Cuoiky numeric(18,3),Diengiai nvarchar(255))
   DECLARE @tblDAUKY AS TABLE(Masp nvarchar(20),Dauky numeric(18,3))
   
   --[LẤT TỒN ĐẦU KỲ ĐẾN TRƯỚC 1 NGÀY]
   INSERT INTO @tblDAUKY(Masp,Dauky)
   SELECT Masp,SUM(Cuoiky) FROM dbo.fnHH_TonkhoDenngay(@NgayDK) GROUP BY Masp
   
   --[TỔNG HỢP PS NHẬP XUẤT]
   INSERT INTO @tblPSNX(STT,Ngayct,NgayPS,SophieuN,SophieuX,Masp,SLNhap,SLXuat,Diengiai)
   SELECT STT=ROW_NUMBER() OVER(PARTITION BY Masp ORDER BY B.Masp asc,Loaiphieu,A.Ngayct desc), a.Ngayct,a.Ngayps,
          SophieuN=(CASE WHEN ISNULL(Loaiphieu,'')='PN' THEN ISNULL(A.Sophieu,'') ELSE '' END),
          SophieuX=(CASE WHEN ISNULL(Loaiphieu,'')='PX' THEN ISNULL(A.Sophieu,'') ELSE '' END),B.Masp,          
          PSNhap=(CASE WHEN ISNULL(Loaiphieu,'')='PN' THEN ISNULL(B.Soluong,0) ELSE 0 END),
	      PSXuat=(CASE WHEN ISNULL(Loaiphieu,'')='PX' THEN ISNULL(B.Soluong,0) ELSE 0 END) ,
	      Diengiai=D.Tenkh
	FROM NX_PHIEUNX A INNER JOIN NX_PHIEUNXCT B ON A.Phieunxid=B.Phieunxid 
	                     LEFT JOIN DM_KHACHHANG D ON A.Makh=D.Makh
	WHERE B.Masp IN(SELECT V.Masp FROM @tblSanpham V) AND
	      DATEDIFF(DAY,@NGAYD,A.Ngayct)>=0 AND DATEDIFF(DAY,A.Ngayct,@NGAYC)>=0
	ORDER BY B.Masp Asc,Loaiphieu ,A.Ngayct Desc      
 
 	
 	DECLARE @Masp nvarchar(20),@STT int,@Dauky Numeric(18,3),@PSN numeric(18,3),@PSX numeric(18,3),
 	       @Cuoiky numeric(18,3)
 	       
	DECLARE curThekho CURSOR FOR SELECT Masp,STT,SLNhap,SLXuat from @tblPSNX ORDER BY Masp, STT asc
	OPEN curThekho
	FETCH NEXT FROM curThekho INTO @Masp, @STT,@PSN,@PSX
	WHILE @@FETCH_STATUS = 0 
		BEGIN
		   IF ISNULL(@STT,0)=1
			 BEGIN
			    SET @Dauky=(SELECT TOP 1 Dauky FROM @tblDAUKY WHERE Masp=@Masp)		
			    SET @Cuoiky=ISNULL(@Dauky,0)+ISNULL(@PSN,0)-ISNULL(@PSX,0)
			             
				INSERT INTO @tblTHEKHO(Ngayct,NgayPS,Masp,STT,Diengiai,Cuoiky)
				VALUES(@NgayDK,@NgayDK,@Masp,0,N'Đầu kỳ',ISNULL(@Dauky,0)) 
				
			    INSERT INTO @tblTHEKHO(Ngayct,NgayPS,SophieuN,SophieuX,STT,Masp,SLNhap,SLXuat,Cuoiky,Diengiai)
			    SELECT Ngayct,NgayPS,SophieuN,SophieuX,STT,Masp,SLNhap,SLXuat,@Cuoiky,Diengiai 
			    FROM @tblPSNX WHERE STT=@STT AND Masp=@Masp 
			 END
	       ELSE
	         BEGIN
	           SET @Cuoiky=ISNULL(@Dauky,0)+ISNULL(@PSN,0)-ISNULL(@PSX,0)
	           INSERT INTO @tblTHEKHO(Ngayct,NgayPS,SophieuN,SophieuX,STT,Masp,SLNhap,SLXuat,Cuoiky,Diengiai)
			   SELECT Ngayct,NgayPS,SophieuN,SophieuX,STT,Masp,SLNhap,SLXuat,@Cuoiky,Diengiai 
			   FROM @tblPSNX WHERE STT=@STT AND Masp=@Masp 
	         END
	         	     
		  SET @Dauky=@Cuoiky
		   	        
		 FETCH NEXT FROM curThekho INTO @Masp, @STT,@PSN,@PSX
		END

	CLOSE curThekho
	DEALLOCATE curThekho
	
	UPDATE @tblTHEKHO SET Tensp=B.Tensp,Dvt=B.Dvt,Quycach=B.Quycach
	FROM @tblTHEKHO A INNER JOIN DM_SANPHAM B ON A.Masp=B.Masp
	
	SELECT * FROM @tblTHEKHO ORDER BY Masp,STT ASC
  END
GO
/****** Object:  StoredProcedure [dbo].[rptHH_Baocaonhapxuatton]    Script Date: 10/26/2019 08:37:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[rptHH_Baocaonhapxuatton]        
 @tblSanpham dbo.Type_Masp readonly,    
 @Ngaydau nvarchar(15),    
 @Ngaycuoi nvarchar(15),      
 @Nguoidung nvarchar(50) ,  
 @Ngayin nvarchar(50) OUTPUT     
 As         
  Begin       
    SET DATEFORMAT DMY    
        
    DECLARE @NgayDK nvarchar(15), @NGAYD DATE, @NGAYC DATE;    
        
    IF ISNULL(@Ngaydau,'')=''    
      SET @NGAYD=GETDATE()    
    ELSE    
      SET @NGAYD=CONVERT(DATE,@Ngaydau,103)    
          
    IF ISNULL(@Ngaycuoi,'')=''    
      SET @NGAYC=GETDATE()    
    ELSE    
      SET @NGAYC=CONVERT(DATE,@Ngaycuoi,103)    
     
   SET @NgayDK=CONVERT(NVARCHAR,DATEADD(DAY,-1,@NGAYD),103)  
      
   set @Ngayin=dbo.fn_NgayIn(CONVERT(NVARCHAR,GETDATE(),103))  
     
   DECLARE @tblBCNXT AS TABLE(Manhom nvarchar(20), Nhomhh nvarchar(50),
                             Masp nvarchar(20),Tensp nvarchar(100),Dvt nvarchar(30), Quycach nvarchar(50),Dongia numeric(18,3),
                             Dauky numeric(18,3),TrigiaD numeric(18,3),PSNhap numeric(18,3),TrigiaN numeric(18,3),
                             PSXuat numeric(18,3),TrigiaX numeric(18,3),Cuoiky numeric(18,3),TrigiaC numeric(18,3))  
   
   INSERT INTO @tblBCNXT(Masp,Dauky,TrigiaD,PSNhap,TrigiaN,PSXuat,TrigiaX)
   SELECT Masp,SUM(Isnull(Dauky,0)),SUM(ISNULL(TrigiaD,0)),SUM(Isnull(PSNhap,0)),
          SUM(Isnull(TrigiaN,0)),SUM(ISNULL(PSXuat,0)),SUM(Isnull(TrigiaX,0)) 
   FROM(
   SELECT Masp,Dauky=Cuoiky,TrigiaD=TrigiaC,PSNhap=0,TrigiaN=0,PSXuat=0,TrigiaX=0 
   FROM dbo.fnHH_TonkhoDenngay(@NgayDK)  
   UNION ALL
   SELECT Masp,Dauky=0,TrigiaD=0,PSNhap,TrigiaN,PSXuat,TrigiaX FROM dbo.fnHH_PhatSinhNX(@NGAYD,@NGAYC)
   ) A GROUP BY Masp
   
   
   UPDATE @tblBCNXT
   SET Cuoiky=ISNULL(Dauky,0)+ISNULL(PSNhap,0)-ISNULL(PSXuat,0),
       TrigiaC=ISNULL(TrigiaD,0)+ISNULL(TrigiaN,0)-ISNULL(TrigiaX,0)
   
   UPDATE @tblBCNXT SET Dongia=(ISNULL(TrigiaD,0)+ISNULL(TrigiaN,0))/ (ISNULL(Dauky,0)+ISNULL(PSNhap,0))
   WHERE (ISNULL(Dauky,0)+ISNULL(PSNhap,0))>0
   
   UPDATE @tblBCNXT 
   SET Tensp=B.Tensp,Dvt=B.Dvt,Quycach=B.Quycach,Manhom=C.Manhom,Nhomhh=C.Tennhom
   FROM @tblBCNXT A INNER JOIN DM_SANPHAM B ON A.Masp=B.Masp
                    LEFT JOIN DM_NHOMSP C ON B.Manhomspid=C.Manhomspid   
                    
   SELECT STT=ROW_NUMBER() OVER(PARTITION BY Manhom ORDER BY Manhom, Masp asc), * 
   FROM @tblBCNXT ORDER BY Manhom,Masp asc                 
  END
GO
/****** Object:  UserDefinedFunction [dbo].[fnCN_SoduDenngay]    Script Date: 10/26/2019 08:37:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create FUNCTION [dbo].[fnCN_SoduDenngay] (@Denngay varchar(10), @Matk varchar(12),@Mant varchar(10))        
 RETURNS 
   @RetTable TABLE (Matk nvarchar(12),Mant nvarchar(3),Madt nvarchar(20),DCNo numeric (18,3),DCCo numeric(18,3))           
AS        
BEGIN        
 DECLARE @Thangkhoaso varchar(6),@Ngaycuoikhoaso VARCHAR(10),@NgaytinhPS VARCHAR(10),@cd tinyint       
 DECLARE @Sodu TABLE (Phieuid NVARCHAR(50),Mant nvarchar(3),Madt nvarchar(15),Matk nvarchar(12),      
                      PSNo numeric (19,2),QDPSNo numeric (19), PSCo numeric (19,2),QDPSCo numeric (19));      
 
 SET @Thangkhoaso=dbo.fn_ThangKhoaSo('CN',@Denngay)
 SET @Ngaycuoikhoaso=dbo.fn_NgayCuoiThang(@Thangkhoaso) 
 SET @NgaytinhPS =  CONVERT(VARCHAR,DATEADD(DD,+1,CONVERT(date,@Denngay,103)),103);      
 SET @cd=Len(@Matk)    
 
 --[NẾU LẤY SỐ DƯ ĐÃ KHÓA SỒ]
 IF @Ngaycuoikhoaso=@Denngay
   BEGIN
      INSERT INTO @RetTable(Matk,Mant,Madt,DCNo,DCCo)
      SELECT Matk,Mant,Madt,ISNULL(DCNo,0),ISNULL(DCCo,0) 
      FROM CN_KETSODU WHERE Thangnam=@Thangkhoaso AND LEFT(Matk,@cd)=@Matk AND Mant=ISNULL(@Mant,'VND')
   END
 ELSE
   BEGIN
       --[GOM Phát sinh từ ngày đến ngày]
       INSERT INTO @Sodu(Phieuid,Mant,Matk,Madt,PSNo,QDPSNo,PSCo,QDPSCo)
       SELECT Phieuid,Mant,Matk,Madt,PSNo,QDPSNo,PSCo,QDPSCo 
       FROM fnCN_Gom_Taikhoan(@Ngaycuoikhoaso,@Denngay)
       WHERE LEFT(Matk,@cd)=@Matk 
       
       INSERT INTO @RetTable(Matk,Mant,Madt,DCNo,DCCo)
       SELECT Matk,Mant,Madt,DCNo=(CASE WHEN DCNo>DCCo THEN DCNo-DCCo ELSE 0 END),
              DCCo=(CASE WHEN DCNo>DCCo THEN 0 ELSE DCCo-DCNo END)
       FROM(       
       SELECT Matk,Mant,Madt,DCNo=Sum(Isnull(DCNo,0)),DCCo=Sum(Isnull(DCCo,0))
       FROM(
         SELECT Matk,Mant,Madt,DCNo=ISNULL(DCNo,0),DCCo=ISNULL(DCCo,0) 
         FROM CN_KETSODU WHERE Thangnam=@Thangkhoaso AND LEFT(Matk,@cd)=@Matk AND Mant=ISNULL(@Mant,'VND')
           UNION ALL
         SELECT Matk,Mant,Madt,PSNo,PSCo FROM @Sodu WHERE Mant=ISNULL(@Mant,'VND'))A
         GROUP BY  A.Matk,A.Mant,A.Madt)TH 
   END   
  RETURN;    
END;
GO
/****** Object:  StoredProcedure [dbo].[rptCN_CanthuTH]    Script Date: 10/26/2019 08:37:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROC [dbo].[rptCN_CanthuTH]
 @Tungay varchar(10),
 @Denngay varchar(10),
 @Matk varchar(12),
 @Mant varchar(3),
 @tblMADT [dbo].[Type_Makh] READONLY
AS
 BEGIN
   SET DATEFORMAT DMY
   DECLARE @Ngaytruoc varchar(10),@cd tinyint
   
   DECLARE @tblSDDau TABLE (Matk nvarchar(12),Mant nvarchar(3),Madt nvarchar(12),Dudau numeric (18,3))  
   DECLARE @tblPS TABLE (Phieuid nvarchar(50),Matk nvarchar(12),Madt nvarchar(10),Phan tinyint,    
                         Loaict nvarchar(2),Loaiphieu nvarchar(2),Sophieu nvarchar(20),
                         Soctgoc nvarchar(50),Ngayct Date,Nchono numeric (3),Ngaydh Date,Sotien numeric (18,3),
                         TKNo nvarchar(12),TKCo nvarchar(12),SotienQH numeric (19,2),Diengiai nvarchar(255),
                         Nguoilap nvarchar(50));    
        
  
  SET @Ngaytruoc = CONVERT(VARCHAR,DATEADD(DD,-1,@Tungay),103);    
  SET @cd = LEN(@Matk);  
 
  --[LẤY SỐ DƯ ĐẦU KỲ]
  INSERT INTO @tblSDDau(Matk,Mant,Madt,Dudau)
  SELECT Matk,Mant,Madt,Dudau=SUM(DCNo-DCCo) 
  FROM fnCN_SoduDenngay(@Ngaytruoc,@Matk,@Mant)
  GROUP BY Matk,Mant,Madt
   
  --[PHÁT SINH CẦN THU TRONG KỲ]   
  INSERT INTO @tblPS(Phieuid,Matk,Madt,Phan,Loaict,Loaiphieu,Sophieu,Soctgoc,    
                     Ngayct,Nchono,Ngaydh,Sotien,TKNo,TKCo,Diengiai,Nguoilap)    
  SELECT Phieuid,Matk,Makh,Phanhe,LoaiCT,Loaiphieu,Sophieu,Soctgoc,Ngayct, 
         Nchono,NgayDH,Sotien,TKNo,TKCo,Diengiai,Nguoilap
  FROM fnCN_Canthu(@Tungay,@Denngay,@Matk,@Mant)
  WHERE Makh in(SELECT v.Maso FROM @tblMADT v)
   
   
  --[TÍNH: DƯ ĐẦU , PHAITHU,DATRA, DƯ CUỐI]
  SELECT Matk=ISNULL(A.Matk,B.Matk),Madt=ISNULL(A.Madt,B.Madt),
         Phaithu=ISNULL(A.Phaithu,0),Datra=ISNULL(A.Datra,0),Dudau=ISNULL(B.Dudau,0),
         Ducuoi=ISNULL(B.Dudau,0)+ISNULL(A.Phaithu,0)-ISNULL(A.Datra,0) 
  INTO #CN_PT       
  FROM(
	  SELECT Matk,Madt,
			 Phaithu=SUM(CASE WHEN LEFT(TKNo,@cd)=@Matk THEN Sotien ELSE 0 END),
			 Datra=SUM(CASE WHEN LEFT(TKCo,@cd)=@Matk THEN Sotien ELSE 0 END)
	  FROM @tblPS GROUP BY Matk,Madt)A FULL OUTER JOIN @tblSDDau B ON A.Matk=B.Matk AND A.Madt=B.Madt


--[PHÂN TÍCH CHI TIẾT THANH TOÁN]
   SELECT  A.Phieuid,A.Loaiphieu ,A.Loaict,A.Matk,A.Mant,C.Tinh,A.Madt,C.Tenkh,C.Diachi,C.Dienthoai,Daidien='',A.Ngayct,A.Soctgoc,
     A.Sophieu,A.Diengiai,A.Sotien,A.Nchono,A.Ngaydh,Phan=A.Muc,B.Dudau,B.Phaithu,B.Datra,B.Ducuoi,
     CT=CASE WHEN A.Phan=1 THEN 'TC' ELSE 'NX' END, A.Nguoilap
   FROM
    (
	   SELECT Muc=1,Phieuid,Sophieu,Loaict,Loaiphieu,Matk,Mant=@Mant,Madt,Ngayct,Soctgoc,Diengiai,Sotien,Nchono,Ngaydh,Phan,Nguoilap   
	   FROM @tblPS WHERE LEFT(TKNo,@cd)=@Matk AND Phan=1   
		 UNION ALL
	   SELECT Muc=2,Phieuid,Sophieu,Loaict,Loaiphieu,Matk,Mant=@Mant,Madt,Ngayct,Soctgoc,Diengiai,Sotien,Nchono,Ngaydh,Phan,Nguoilap   
	   FROM @tblPS WHERE LEFT(TKCo,@cd)=@Matk AND Phan=1   
		 UNION ALL
	   SELECT Muc=1,Phieuid,Sophieu,Loaict,Loaiphieu,Matk,Mant=@Mant,Madt,Ngayct,Soctgoc,Diengiai,Sotien,Nchono,Ngaydh,Phan,Nguoilap   
	   FROM @tblPS WHERE LEFT(TKNo,@cd)=@Matk AND Phan=2 
		 UNION ALL
	   SELECT Muc=2,Phieuid,Sophieu,Loaict,Loaiphieu,Matk,Mant=@Mant,Madt,Ngayct,Soctgoc,Diengiai,Sotien,Nchono,Ngaydh,Phan,Nguoilap   
	   FROM @tblPS WHERE LEFT(TKCo,@cd)=@Matk AND Phan=2 
     )A LEFT OUTER JOIN #CN_PT B ON A.Matk=B.Matk AND A.Madt=B.Madt
        LEFT OUTER JOIN DM_KHACHHANG C ON A.Madt=C.Makh
     ORDER BY A.Matk,A.Madt,A.Muc,A.Ngaydh 
     	
  --[PHÂN TÍCH TỔNG HỢP CN]
   SELECT STT=ROW_NUMBER()over(Partition by Isnull(B.Tinh,N'Không xác định') order by B.Tenkh asc),
         A.Madt,B.Tenkh,B.Diachi,B.Dienthoai,Daidien='',Tinh=Isnull(B.Tinh,N'Không xác định'),A.Matk,
          A.Dudau,A.Phaithu,A.Datra,A.Ducuoi
   FROM #CN_PT A LEFT JOIN DM_KHACHHANG B ON A.Madt=B.Makh
   ORDER BY B.Tinh,B.Tenkh ASC          
 END
GO
/****** Object:  StoredProcedure [dbo].[rptCN_CanthuCT]    Script Date: 10/26/2019 08:37:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[rptCN_CanthuCT]
 @Tungay varchar(10),
 @Denngay varchar(10),
 @Matk varchar(12),
 @Mant varchar(3),
 @tblMADT [dbo].[Type_Makh] READONLY
AS
 BEGIN
   SET DATEFORMAT DMY
   DECLARE @Ngaytruoc varchar(10),@cd tinyint
   
   DECLARE @tblSDDau TABLE (Matk nvarchar(12),Mant nvarchar(3),Madt nvarchar(12),Dudau numeric (18,3))  
   DECLARE @tblPS TABLE (Phieuid nvarchar(50),Matk nvarchar(12),Madt nvarchar(10),Phan tinyint,    
                         Loaict nvarchar(2),Loaiphieu nvarchar(2),Sophieu nvarchar(20),
                         Soctgoc nvarchar(50),Ngayct Date,Nchono numeric (3),Ngaydh Date,Sotien numeric (18,3),
                         TKNo nvarchar(12),TKCo nvarchar(12),SotienQH numeric (19,2),Diengiai nvarchar(255),
                         Nguoilap nvarchar(50));    
        
  
  SET @Ngaytruoc = CONVERT(VARCHAR,DATEADD(DD,-1,@Tungay),103);    
  SET @cd = LEN(@Matk);  
 
  --[LẤY SỐ DƯ ĐẦU KỲ]
  INSERT INTO @tblSDDau(Matk,Mant,Madt,Dudau)
  SELECT Matk,Mant,Madt,Dudau=SUM(DCNo-DCCo) 
  FROM fnCN_SoduDenngay(@Ngaytruoc,@Matk,@Mant)
  GROUP BY Matk,Mant,Madt
   
  --[PHÁT SINH CẦN THU TRONG KỲ]   
  INSERT INTO @tblPS(Phieuid,Matk,Madt,Phan,Loaict,Loaiphieu,Sophieu,Soctgoc,    
                     Ngayct,Nchono,Ngaydh,Sotien,TKNo,TKCo,Diengiai,Nguoilap)    
  SELECT Phieuid,Matk,Makh,Phanhe,LoaiCT,Loaiphieu,Sophieu,Soctgoc,Ngayct, 
         Nchono,NgayDH,Sotien,TKNo,TKCo,Diengiai,Nguoilap
  FROM fnCN_Canthu(@Tungay,@Denngay,@Matk,@Mant)
  WHERE Makh in(SELECT v.Maso FROM @tblMADT v)
   
   
  --[TÍNH: DƯ ĐẦU , PHAITHU,DATRA, DƯ CUỐI]
  SELECT Matk=ISNULL(A.Matk,B.Matk),Madt=ISNULL(A.Madt,B.Madt),
         Phaithu=ISNULL(A.Phaithu,0),Datra=ISNULL(A.Datra,0),Dudau=ISNULL(B.Dudau,0),
         Ducuoi=ISNULL(B.Dudau,0)+ISNULL(A.Phaithu,0)-ISNULL(A.Datra,0) 
  INTO #CN_PT       
  FROM(
	  SELECT Matk,Madt,
			 Phaithu=SUM(CASE WHEN LEFT(TKNo,@cd)=@Matk THEN Sotien ELSE 0 END),
			 Datra=SUM(CASE WHEN LEFT(TKCo,@cd)=@Matk THEN Sotien ELSE 0 END)
	  FROM @tblPS GROUP BY Matk,Madt)A FULL OUTER JOIN @tblSDDau B ON A.Matk=B.Matk AND A.Madt=B.Madt

	
  --[PHÂN TÍCH CHI TIẾT THANH TOÁN]
   SELECT  A.Phieuid,A.Loaiphieu ,A.Loaict,A.Matk,A.Mant,C.Tinh,A.Madt,C.Tenkh,C.Diachi,C.Dienthoai,Daidien='',A.Ngayct,A.Soctgoc,
     A.Sophieu,A.Diengiai,A.Sotien,A.Nchono,A.Ngaydh,Phan=A.Muc,B.Dudau,B.Phaithu,B.Datra,B.Ducuoi,
     CT=CASE WHEN A.Phan=1 THEN 'TC' ELSE 'NX' END, A.Nguoilap
   FROM
    (
	   SELECT Muc=1,Phieuid,Sophieu,Loaict,Loaiphieu,Matk,Mant=@Mant,Madt,Ngayct,Soctgoc,Diengiai,Sotien,Nchono,Ngaydh,Phan,Nguoilap   
	   FROM @tblPS WHERE LEFT(TKNo,@cd)=@Matk AND Phan=1   
		 UNION ALL
	   SELECT Muc=2,Phieuid,Sophieu,Loaict,Loaiphieu,Matk,Mant=@Mant,Madt,Ngayct,Soctgoc,Diengiai,Sotien,Nchono,Ngaydh,Phan,Nguoilap   
	   FROM @tblPS WHERE LEFT(TKCo,@cd)=@Matk AND Phan=1   
		 UNION ALL
	   SELECT Muc=1,Phieuid,Sophieu,Loaict,Loaiphieu,Matk,Mant=@Mant,Madt,Ngayct,Soctgoc,Diengiai,Sotien,Nchono,Ngaydh,Phan,Nguoilap   
	   FROM @tblPS WHERE LEFT(TKNo,@cd)=@Matk AND Phan=2 
		 UNION ALL
	   SELECT Muc=2,Phieuid,Sophieu,Loaict,Loaiphieu,Matk,Mant=@Mant,Madt,Ngayct,Soctgoc,Diengiai,Sotien,Nchono,Ngaydh,Phan,Nguoilap   
	   FROM @tblPS WHERE LEFT(TKCo,@cd)=@Matk AND Phan=2 
     )A LEFT OUTER JOIN #CN_PT B ON A.Matk=B.Matk AND A.Madt=B.Madt
        LEFT OUTER JOIN DM_KHACHHANG C ON A.Madt=C.Makh
     ORDER BY A.Matk,A.Madt,A.Muc,A.Ngaydh 
 END
GO
