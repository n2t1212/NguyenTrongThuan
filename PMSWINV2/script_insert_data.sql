USE [MTSMPOS]
GO
/****** Object:  Table [dbo].[TICHLUY_KHACHHANG]    Script Date: 11/03/2019 14:17:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TICHLUY_KHACHHANG](
	[Makh] [nvarchar](50) NOT NULL,
	[Masp] [nvarchar](20) NOT NULL,
	[Doanhso] [numeric](18, 3) NULL,
	[DiemDs] [numeric](18, 0) NULL,
	[Diemdatru] [numeric](18, 0) NULL
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[DM_TICHLUYDIEM]    Script Date: 11/03/2019 14:17:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DM_TICHLUYDIEM](
	[Masp] [nvarchar](20) NOT NULL,
	[DoanhSo] [numeric](18, 3) NULL,
	[DoanhsoNo] [numeric](18, 3) NULL,
	[QDDiemDS] [numeric](18, 0) NOT NULL,
	[QDDiemDSNo] [numeric](18, 0) NULL,
	[Ngayapdung] [date] NULL,
	[Denngay] [date] NULL,
	[Apdung] [bit] NULL,
 CONSTRAINT [PK_DM_TICHLUYDIEM] PRIMARY KEY CLUSTERED 
(
	[Masp] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

INSERT [dbo].[DM_CHUCNANG] ([macn], [macnroot], [kyhieu], [chucnang], [tenform], [loaicn], [thumb], [icon], [cap], [muccon], [stt], [khongapdung]) VALUES (N'014A7705-A9A4-44A5-831E-3158CEE36CD7', NULL, N'mnuHH', N'Hàng Hóa', NULL, N'HH', NULL, NULL, N'0', NULL, NULL, NULL)
INSERT [dbo].[DM_CHUCNANG] ([macn], [macnroot], [kyhieu], [chucnang], [tenform], [loaicn], [thumb], [icon], [cap], [muccon], [stt], [khongapdung]) VALUES (N'03C46C08-3496-4895-9F2E-A51EEE8CBCC0', NULL, N'mnuDM', N'Quản lý danh mục', NULL, N'DM', NULL, NULL, N'0', NULL, N'', NULL)
INSERT [dbo].[DM_CHUCNANG] ([macn], [macnroot], [kyhieu], [chucnang], [tenform], [loaicn], [thumb], [icon], [cap], [muccon], [stt], [khongapdung]) VALUES (N'03C46C08-3496-4895-9F2E-A51EEE8CBCC1', N'03C46C08-3496-4895-9F2E-A51EEE8CBCC0', N'mnuDM_KHO', N'Danh mục kho', NULL, N'DM', NULL, NULL, N'1', NULL, N'', NULL)
INSERT [dbo].[DM_CHUCNANG] ([macn], [macnroot], [kyhieu], [chucnang], [tenform], [loaicn], [thumb], [icon], [cap], [muccon], [stt], [khongapdung]) VALUES (N'03C46C08-3496-4895-9F2E-A51EEE8CBCC2', N'71135D27-1DDC-4E5A-B402-90B9A75161AF', N'mnuBC_THEKHO', N'Thẻ Kho', NULL, N'BC', NULL, NULL, N'1', NULL, N'', NULL)
INSERT [dbo].[DM_CHUCNANG] ([macn], [macnroot], [kyhieu], [chucnang], [tenform], [loaicn], [thumb], [icon], [cap], [muccon], [stt], [khongapdung]) VALUES (N'03C46C08-3496-4895-9F2E-A51EEE8CBCC3', N'014A7705-A9A4-44A5-831E-3158CEE36CD7', N'mnuHH_PHIEUXUAT', N'Phiếu Xuất', NULL, N'PX', NULL, NULL, N'1', NULL, N'', NULL)
INSERT [dbo].[DM_CHUCNANG] ([macn], [macnroot], [kyhieu], [chucnang], [tenform], [loaicn], [thumb], [icon], [cap], [muccon], [stt], [khongapdung]) VALUES (N'03C46C08-3496-4895-9F2E-A51EEE8CBCC4', N'014A7705-A9A4-44A5-831E-3158CEE36CD7', N'mnuHH_PHIEUNHAP', N'Phiếu Nhập', NULL, N'PN', NULL, NULL, N'1', NULL, N'', NULL)
INSERT [dbo].[DM_CHUCNANG] ([macn], [macnroot], [kyhieu], [chucnang], [tenform], [loaicn], [thumb], [icon], [cap], [muccon], [stt], [khongapdung]) VALUES (N'03C46C08-3496-4895-9F2E-A51EEE8CBCC5', N'03C46C08-3496-4895-9F2E-A51EEE8CBCC0', N'mnuDM_SANPHAM', N'Danh mục sản phẩm', NULL, N'DM', NULL, NULL, N'1', NULL, N'', NULL)
INSERT [dbo].[DM_CHUCNANG] ([macn], [macnroot], [kyhieu], [chucnang], [tenform], [loaicn], [thumb], [icon], [cap], [muccon], [stt], [khongapdung]) VALUES (N'03C46C08-3496-4895-9F2E-A51EEE8CBCC6', N'03C46C08-3496-4895-9F2E-A51EEE8CBCC0', N'mnuDM_NHOMSANPHAM', N'Danh mục nhóm sản phẩm', NULL, N'DM', NULL, NULL, N'1', NULL, N'', NULL)
INSERT [dbo].[DM_CHUCNANG] ([macn], [macnroot], [kyhieu], [chucnang], [tenform], [loaicn], [thumb], [icon], [cap], [muccon], [stt], [khongapdung]) VALUES (N'03C46C08-3496-4895-9F2E-A51EEE8CBCC7', N'03C46C08-3496-4895-9F2E-A51EEE8CBCC0', N'mnuDM_XE', N'Danh mục Xe', NULL, N'DM', NULL, NULL, N'1', NULL, N'', NULL)
INSERT [dbo].[DM_CHUCNANG] ([macn], [macnroot], [kyhieu], [chucnang], [tenform], [loaicn], [thumb], [icon], [cap], [muccon], [stt], [khongapdung]) VALUES (N'03C46C08-3496-4895-9F2E-A51EEE8CBCC8', N'03C46C08-3496-4895-9F2E-A51EEE8CBCC0', N'mnuDM_DONGIA', N'Đơn giá xuất', NULL, N'DM', NULL, NULL, N'1', NULL, N'', NULL)
INSERT [dbo].[DM_CHUCNANG] ([macn], [macnroot], [kyhieu], [chucnang], [tenform], [loaicn], [thumb], [icon], [cap], [muccon], [stt], [khongapdung]) VALUES (N'03C46C08-3496-4895-9F2E-A51EEE8CBCC9', N'014A7705-A9A4-44A5-831E-3158CEE36CD7', N'mnuHH_CHANHXE', N'Chành xe', NULL, N'CX', NULL, NULL, N'1', NULL, N'', NULL)
INSERT [dbo].[DM_CHUCNANG] ([macn], [macnroot], [kyhieu], [chucnang], [tenform], [loaicn], [thumb], [icon], [cap], [muccon], [stt], [khongapdung]) VALUES (N'03C46C08-3496-4895-9F2E-A51EEE8CBCF1', N'03C46C08-3496-4895-9F2E-A51EEE8CBCC0', N'mnuDM_KHACHHANG', N'Khách hàng', NULL, N'DM', NULL, NULL, N'1', NULL, NULL, NULL)
INSERT [dbo].[DM_CHUCNANG] ([macn], [macnroot], [kyhieu], [chucnang], [tenform], [loaicn], [thumb], [icon], [cap], [muccon], [stt], [khongapdung]) VALUES (N'03C46C08-3496-4895-9F2E-A51EEE8CBCF2', N'03C46C08-3496-4895-9F2E-A51EEE8CBCC0', N'mnuDM_CHIPHI', N'Chi phí', NULL, N'DM', NULL, NULL, N'1', NULL, NULL, NULL)
INSERT [dbo].[DM_CHUCNANG] ([macn], [macnroot], [kyhieu], [chucnang], [tenform], [loaicn], [thumb], [icon], [cap], [muccon], [stt], [khongapdung]) VALUES (N'03C46C08-3496-4895-9F2E-A51EEE8CBCF3', N'03C46C08-3496-4895-9F2E-A51EEE8CBCC0', N'mnuDM_TAIKHOAN', N'Tài khoản', NULL, N'DM', NULL, NULL, N'1', NULL, NULL, NULL)
INSERT [dbo].[DM_CHUCNANG] ([macn], [macnroot], [kyhieu], [chucnang], [tenform], [loaicn], [thumb], [icon], [cap], [muccon], [stt], [khongapdung]) VALUES (N'03C46C08-3496-4895-9F2E-A51EEE8CBCF4', N'03C46C08-3496-4895-9F2E-A51EEE8CBCC0', N'mnuDM_NHANVIEN', N'Nhân viên', NULL, N'DM', NULL, NULL, N'1', NULL, NULL, NULL)
INSERT [dbo].[DM_CHUCNANG] ([macn], [macnroot], [kyhieu], [chucnang], [tenform], [loaicn], [thumb], [icon], [cap], [muccon], [stt], [khongapdung]) VALUES (N'03C46C08-3496-4895-9F2E-A51EEE8CBCF5', N'03C46C08-3496-4895-9F2E-A51EEE8CBCC0', N'mnuDM_BANGGIA', N'Bảng giá', NULL, N'DM', NULL, NULL, N'1', NULL, NULL, NULL)
INSERT [dbo].[DM_CHUCNANG] ([macn], [macnroot], [kyhieu], [chucnang], [tenform], [loaicn], [thumb], [icon], [cap], [muccon], [stt], [khongapdung]) VALUES (N'03C46C08-3496-4895-9F2E-A51EEE8CBCF6', N'03C46C08-3496-4895-9F2E-A51EEE8CBCC0', N'mnuDM_TICHLUYDIEM', N'Định mức tích lũy điểm', NULL, N'DM', NULL, NULL, N'1', NULL, NULL, NULL)
INSERT [dbo].[DM_CHUCNANG] ([macn], [macnroot], [kyhieu], [chucnang], [tenform], [loaicn], [thumb], [icon], [cap], [muccon], [stt], [khongapdung]) VALUES (N'03F8CD09-B558-413F-8937-9BC31F551C8D', N'03C46C08-3496-4895-9F2E-A51EEE8CBCC0', N'mnuDM_LYDO', N'Danh mục Lý do', NULL, N'DM', NULL, NULL, N'1', NULL, NULL, NULL)
INSERT [dbo].[DM_CHUCNANG] ([macn], [macnroot], [kyhieu], [chucnang], [tenform], [loaicn], [thumb], [icon], [cap], [muccon], [stt], [khongapdung]) VALUES (N'0F053CB7-6A35-4106-BD3E-3636D9203A2B', NULL, N'mnuHT_THAMSO', N'Khai báo hệ thống', NULL, N'HT', NULL, NULL, N'1', NULL, NULL, NULL)
INSERT [dbo].[DM_CHUCNANG] ([macn], [macnroot], [kyhieu], [chucnang], [tenform], [loaicn], [thumb], [icon], [cap], [muccon], [stt], [khongapdung]) VALUES (N'3887199A-D12F-45D7-9C25-5462EFAECFEF', NULL, N'mnuHT_PHUCHOI', N'Phục Hồi Dữ Liệu', NULL, N'HT', NULL, NULL, N'1', NULL, N'4', NULL)
INSERT [dbo].[DM_CHUCNANG] ([macn], [macnroot], [kyhieu], [chucnang], [tenform], [loaicn], [thumb], [icon], [cap], [muccon], [stt], [khongapdung]) VALUES (N'3FCA4675-5B3B-4A1C-8C29-C4695DF2896F', NULL, N'mnuHT_QUYENHAN', N'Quyền Hạn', NULL, N'HT', NULL, NULL, N'1', NULL, N'2', NULL)
INSERT [dbo].[DM_CHUCNANG] ([macn], [macnroot], [kyhieu], [chucnang], [tenform], [loaicn], [thumb], [icon], [cap], [muccon], [stt], [khongapdung]) VALUES (N'54B18EDC-F3B8-4467-A601-BCAFB478637D', NULL, N'mnuHT_SAOLUU', N'Sao Lưu Dữ Liệu', NULL, N'HT', NULL, NULL, N'1', NULL, N'3', NULL)
INSERT [dbo].[DM_CHUCNANG] ([macn], [macnroot], [kyhieu], [chucnang], [tenform], [loaicn], [thumb], [icon], [cap], [muccon], [stt], [khongapdung]) VALUES (N'662651E2-E8D3-456A-BEF0-66B60CBAB098', NULL, N'mnuHT_THONGTIN', N'Thông Tin Chung', NULL, N'HT', NULL, NULL, N'1', NULL, N'5', NULL)
INSERT [dbo].[DM_CHUCNANG] ([macn], [macnroot], [kyhieu], [chucnang], [tenform], [loaicn], [thumb], [icon], [cap], [muccon], [stt], [khongapdung]) VALUES (N'69CD8E3E-2D5D-4587-953D-EB1BCB346927', NULL, N'mnuHT_NGUOIDUNG', N'Người Dùng', NULL, N'HT', NULL, NULL, N'1', NULL, N'1', NULL)
INSERT [dbo].[DM_CHUCNANG] ([macn], [macnroot], [kyhieu], [chucnang], [tenform], [loaicn], [thumb], [icon], [cap], [muccon], [stt], [khongapdung]) VALUES (N'71135D27-1DDC-4E5A-B402-90B9A75161AA', N'71135D27-1DDC-4E5A-B402-90B9A75161AF', N'mnuBC_DIEMTICHLUY', N'Quản lý điểm tích lũy', NULL, N'BC', NULL, NULL, N'1', NULL, NULL, NULL)
INSERT [dbo].[DM_CHUCNANG] ([macn], [macnroot], [kyhieu], [chucnang], [tenform], [loaicn], [thumb], [icon], [cap], [muccon], [stt], [khongapdung]) VALUES (N'71135D27-1DDC-4E5A-B402-90B9A75161AF', NULL, N'mnuBC', N'Báo cáo - Thống Kê', NULL, N'BC', NULL, NULL, N'0', NULL, NULL, NULL)
INSERT [dbo].[DM_CHUCNANG] ([macn], [macnroot], [kyhieu], [chucnang], [tenform], [loaicn], [thumb], [icon], [cap], [muccon], [stt], [khongapdung]) VALUES (N'71135D27-1DDC-4E5A-B402-90B9A75161BH', N'71135D27-1DDC-4E5A-B402-90B9A75161AF', N'mnuBC_BANHANG', N'Báo cáo bán hàng', NULL, N'BC', NULL, NULL, N'1', NULL, N'2', NULL)
INSERT [dbo].[DM_CHUCNANG] ([macn], [macnroot], [kyhieu], [chucnang], [tenform], [loaicn], [thumb], [icon], [cap], [muccon], [stt], [khongapdung]) VALUES (N'71135D27-1DDC-4E5A-B402-90B9A75161SS', N'71135D27-1DDC-4E5A-B402-90B9A75161AF', N'mnuBC_TKGUICHANH', N'Thống kê gửi chành', NULL, N'BC', NULL, NULL, N'1', NULL, NULL, NULL)
INSERT [dbo].[DM_CHUCNANG] ([macn], [macnroot], [kyhieu], [chucnang], [tenform], [loaicn], [thumb], [icon], [cap], [muccon], [stt], [khongapdung]) VALUES (N'71135D27-1DDC-4E5A-B402-90B9A75161ZZ', N'71135D27-1DDC-4E5A-B402-90B9A75161AF', N'mnuBC_NHAPXUATTON', N'Báo cáo nhập xuất tồn', NULL, N'BC', NULL, NULL, N'1', NULL, N'3', NULL)
INSERT [dbo].[DM_CHUCNANG] ([macn], [macnroot], [kyhieu], [chucnang], [tenform], [loaicn], [thumb], [icon], [cap], [muccon], [stt], [khongapdung]) VALUES (N'7411F1C8-01CA-457F-84AB-B85D7D61E200', NULL, N'mnuBH', N'Bán Hàng', NULL, N'BH', NULL, NULL, N'0', NULL, N'0', NULL)
INSERT [dbo].[DM_CHUCNANG] ([macn], [macnroot], [kyhieu], [chucnang], [tenform], [loaicn], [thumb], [icon], [cap], [muccon], [stt], [khongapdung]) VALUES (N'7411F1C8-01CA-457F-84AB-B85D7D61E219', NULL, N'mnuHT', N'Hệ Thống', NULL, N'HT', NULL, NULL, N'0', NULL, NULL, NULL)
INSERT [dbo].[DM_CHUCNANG] ([macn], [macnroot], [kyhieu], [chucnang], [tenform], [loaicn], [thumb], [icon], [cap], [muccon], [stt], [khongapdung]) VALUES (N'7411F1C8-01CA-457F-84AB-B85D7D61E220', NULL, N'mnuBC_PHAITHU', N'Công nợ phải thu', NULL, N'BC', NULL, NULL, N'0', NULL, NULL, NULL)
INSERT [dbo].[DM_CHUCNANG] ([macn], [macnroot], [kyhieu], [chucnang], [tenform], [loaicn], [thumb], [icon], [cap], [muccon], [stt], [khongapdung]) VALUES (N'7411F1C8-01CA-457F-84AB-B85D7D61E221', N'7411F1C8-01CA-457F-84AB-B85D7D61E220', N'mnuBC_CONGNOTHU', N'Công nợ phải thu - Chi tiết', NULL, N'BC', NULL, NULL, N'1', NULL, NULL, NULL)
INSERT [dbo].[DM_CHUCNANG] ([macn], [macnroot], [kyhieu], [chucnang], [tenform], [loaicn], [thumb], [icon], [cap], [muccon], [stt], [khongapdung]) VALUES (N'7411F1C8-01CA-457F-84AB-B85D7D61E222', N'7411F1C8-01CA-457F-84AB-B85D7D61E220', N'mnuBC_CONGNOTHUTH', N'Công nợ phải thu - Tổng hợp', NULL, N'BC', NULL, NULL, N'1', NULL, NULL, NULL)
INSERT [dbo].[DM_CHUCNANG] ([macn], [macnroot], [kyhieu], [chucnang], [tenform], [loaicn], [thumb], [icon], [cap], [muccon], [stt], [khongapdung]) VALUES (N'7411F1C8-01CA-457F-84AB-B85D7D61E223', N'7411F1C8-01CA-457F-84AB-B85D7D61E220', N'mnuBC_CONGNOTRA', N'Công nợ phải trả', NULL, N'BC', NULL, NULL, N'1', NULL, NULL, NULL)
INSERT [dbo].[DM_CHUCNANG] ([macn], [macnroot], [kyhieu], [chucnang], [tenform], [loaicn], [thumb], [icon], [cap], [muccon], [stt], [khongapdung]) VALUES (N'7411F1C8-01CA-457F-84AB-B85D7D61E244', N'7411F1C8-01CA-457F-84AB-B85D7D61E219', N'mnuHT_KETCHUYENSODU', N'Kết chuyển số dư', NULL, N'HT', NULL, NULL, N'1', NULL, N'2', NULL)
INSERT [dbo].[DM_CHUCNANG] ([macn], [macnroot], [kyhieu], [chucnang], [tenform], [loaicn], [thumb], [icon], [cap], [muccon], [stt], [khongapdung]) VALUES (N'7411F1C8-01CA-457F-84AB-B85D7D61E245', NULL, N'mnuTC', N'Phiếu Thu Chi', NULL, N'TC', NULL, NULL, N'0', NULL, NULL, NULL)
INSERT [dbo].[DM_CHUCNANG] ([macn], [macnroot], [kyhieu], [chucnang], [tenform], [loaicn], [thumb], [icon], [cap], [muccon], [stt], [khongapdung]) VALUES (N'7411F1C8-01CA-457F-84AB-B85D7D61E246', N'7411F1C8-01CA-457F-84AB-B85D7D61E245', N'mnuTC_PHIEUTHU', N'Phiếu thu', NULL, N'TC', NULL, NULL, N'1', NULL, NULL, NULL)
INSERT [dbo].[DM_CHUCNANG] ([macn], [macnroot], [kyhieu], [chucnang], [tenform], [loaicn], [thumb], [icon], [cap], [muccon], [stt], [khongapdung]) VALUES (N'7411F1C8-01CA-457F-84AB-B85D7D61E247', N'7411F1C8-01CA-457F-84AB-B85D7D61E245', N'mnuTC_PHIEUCHI', N'Phiếu chi', NULL, N'TC', NULL, NULL, N'1', NULL, NULL, NULL)
INSERT [dbo].[DM_CHUCNANG] ([macn], [macnroot], [kyhieu], [chucnang], [tenform], [loaicn], [thumb], [icon], [cap], [muccon], [stt], [khongapdung]) VALUES (N'7411F1C8-01CA-457F-84AB-B85D7D61E2EE', N'7411F1C8-01CA-457F-84AB-B85D7D61E219', N'mnuHT_EXIT', N'Thoát', NULL, N'HT', NULL, NULL, N'1', NULL, N'3', NULL)
