USE [Invoices]
GO

/****** Object:  Table [dbo].[Invoices]    Script Date: 12/09/2021 8:46:15 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Invoices](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[TransactionIdentificator] [nvarchar](50) NOT NULL,
	[Amount] [decimal](15, 2) NOT NULL,
	[CurrenyCode] [nvarchar](5) NOT NULL,
	[TransactionDate] [datetime] NOT NULL,
	[Status] [nvarchar](20) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO