USE [MTSMPOS]
GO

/****** Object:  Table [dbo].[TC_PHIEUTC]    Script Date: 10/26/2019 08:44:17 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[TC_PHIEUTC](
	[Phieutcid] [nvarchar](50) NOT NULL,
	[Mant] [nvarchar](50) NOT NULL,
	[Madt] [nvarchar](15) NULL,
	[Loaiphieu] [nvarchar](2) NOT NULL,
	[Sophieu] [nvarchar](20) NOT NULL,
	[Ngayct] [date] NOT NULL,
	[Ngayps] [date] NOT NULL,
	[Tygia] [numeric](6, 0) NOT NULL,
	[Soctgoc] [nvarchar](20) NULL,
	[TKDu] [nvarchar](12) NOT NULL,
	[Nguyente] [numeric](18, 3) NOT NULL,
	[Sotien] [numeric](18, 0) NOT NULL,
	[Hoten] [nvarchar](100) NULL,
	[Nguoilap] [nvarchar](50) NULL,
	[Ngaylap] [datetime] NULL,
	[Nguoisua] [nvarchar](50) NULL,
	[Ngaysua] [datetime] NULL,
	[Ghiso] [bit] NOT NULL,
	[Mald] [nvarchar](15) NULL,
	[Ghichu] [nvarchar](255) NULL,
	[Loaitien] [nvarchar](50) NULL,
 CONSTRAINT [PK_TC_PHIEUTC] PRIMARY KEY CLUSTERED 
(
	[Phieutcid] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


CREATE TABLE [dbo].[TC_PHIEUTCCT](
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
	[NChono] [numeric](3, 0) NULL,
 CONSTRAINT [PK_TC_PHIEUTCCT] PRIMARY KEY CLUSTERED 
(
	[Phieutcctid] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE TABLE [dbo].[DM_CHIPHI](
	[Macp] [nvarchar](10) NOT NULL,
	[Chiphi] [nvarchar](50) NULL,
 CONSTRAINT [PK_DM_CHIPHI] PRIMARY KEY CLUSTERED 
(
	[Macp] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE TABLE [dbo].[CN_KETSODU](
	[Ketsoduid] [nvarchar](50) NOT NULL,
	[Thangnam] [nvarchar](6) NOT NULL,
	[Matk] [nvarchar](12) NOT NULL,
	[Mant] [nvarchar](3) NOT NULL,
	[Madt] [nvarchar](15) NOT NULL,
	[TygiaQD] [numeric](15, 3) NULL,
	[DDNo] [numeric](15, 2) NULL,
	[DDCo] [numeric](15, 2) NULL,
	[DCNo] [numeric](15, 2) NULL,
	[DCCo] [numeric](15, 2) NULL,
	[Khoa] [bit] NULL,
 CONSTRAINT [PK_CN_KETSODU] PRIMARY KEY CLUSTERED 
(
	[Ketsoduid] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE TABLE [dbo].[DM_TAIKHOAN](
	[Matk] [nvarchar](12) NOT NULL,
	[Manhom] [nvarchar](2) NULL,
	[Taikhoan] [nvarchar](100) NOT NULL,
	[Tietkhoan] [bit] NULL,
	[Candoi] [nvarchar](50) NULL,
 CONSTRAINT [PK_DM_TAIKHOAN] PRIMARY KEY CLUSTERED 
(
	[Matk] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE TABLE [dbo].[HT_KHOASO](
	[Khoasoid] [nvarchar](50) NOT NULL,
	[Thangnam] [nvarchar](6) NULL,
	[Ngaythangnam] [date] NULL,
	[Phanhe] [nvarchar](5) NULL,
	[Khoa] [bit] NULL,
	[Nguoikhoa] [nvarchar](50) NULL,
	[Ngaykhoa] [datetime] NULL,
 CONSTRAINT [PK_HT_KHOASO] PRIMARY KEY CLUSTERED 
(
	[Khoasoid] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE TABLE [dbo].[NX_KETSOTON](
	[Ketsotonid] [nvarchar](50) NOT NULL,
	[Thangnam] [nvarchar](6) NULL,
	[Masp] [nvarchar](20) NULL,
	[Makho] [nvarchar](20) NULL,
	[Dauky] [numeric](18, 3) NULL,
	[TrigiaD] [numeric](18, 3) NULL,
	[Nhap] [numeric](18, 3) NULL,
	[TrigiaN] [numeric](18, 3) NULL,
	[Xuat] [numeric](18, 3) NULL,
	[TrigiaX] [numeric](18, 3) NULL,
	[Cuoiky] [numeric](18, 3) NULL,
	[TrigiaC] [numeric](18, 3) NULL,
	[Khoa] [bit] NULL,
 CONSTRAINT [PK_NX_KETSOTON] PRIMARY KEY CLUSTERED 
(
	[Ketsotonid] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO