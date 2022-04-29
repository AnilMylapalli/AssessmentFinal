USE[TempDb1]  
GO  
SET ANSI_NULLS ON  
GO  
SET QUOTED_IDENTIFIER ON  
GO  
CREATE TABLE[dbo].[Product](  
    [slNo][int] IDENTITY(1, 1) NOT NULL, [Product_Name] [nvarchar](100) NULL, [Price][money] NULL, [url][nvarchar](350) NULL, [Description][text] NULL, 
	CONSTRAINT[PK_Product] PRIMARY KEY (slNo) 

);  


select * from [product];

insert into [dbo].[product] (price,url,Description,Product_Name)
values (14,'product1.jpg','Cleanser','HeadSet'),
 (10,'product2.jpg','Cleanser2','Watch'),
 (13,'product3.jpg','Cleanser3','Camera'),
 (14,'product4.jpg','Cleanser4','CocoOil'),
 (7,'product5.jpg','Cleanser5','RedShoe')


USE[TempDb1]  
GO  
SET ANSI_NULLS ON  
GO  
SET QUOTED_IDENTIFIER ON  
GO  
CREATE TABLE[dbo].[Product_Purchase_History](  
    [PID][int] IDENTITY(1, 1) NOT NULL,
	[SID] [int] NOT NULL,
	[Product][nvarchar](100) NULL, 
	[Price][money] NULL,
	[url][nvarchar](350) NULL, 
	[Description][text] NULL, 
	[TotalAmount] [int] NOT NULL,
	[UserName] [nvarchar](100) NULL,
	[Purchase_Date] datetime,
	[Quantity] int NULL,
	CONSTRAINT[PK_Product_Purchase_History] PRIMARY KEY (PID) 
);

select * from [Product_Purchase_History];



USE[TempDb1]
GO 
SET ANSI_NULLS ON  
GO  
SET QUOTED_IDENTIFIER ON  
GO
CREATE TABLE [dbo].[LoginInfo]
(
[CustomerId][int] IDENTITY(1, 1) NOT NULL, [UN][nvarchar](100) NULL, [PWD][nvarchar](100) NULL, 
	[isAdmin][int] NULL
	CONSTRAINT[PK_CompDetails] PRIMARY KEY (CustomerId)
);
GO