CREATE TABLE [dbo].[Licenses](
	[Id] [uniqueidentifier] NOT NULL,
	[KeyHash] [nvarchar](100) NOT NULL,
	[Salt] [nvarchar](100) NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[ActivationDate] [datetime] NULL,
 CONSTRAINT [PK_Licenses] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Licenses] ADD  CONSTRAINT [DF_Licenses_Id]  DEFAULT (newid()) FOR [Id]
GO