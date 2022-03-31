USE [CodeChallenge]

-- Create schema
IF NOT EXISTS ( SELECT  *
                FROM    sys.schemas
                WHERE   name = N'wallet' )
    EXEC('CREATE SCHEMA [wallet]');
GO

-- Create Account Table
IF NOT EXISTS(SELECT * FROM sys.tables WHERE SCHEMA_NAME(schema_id) LIKE 'wallet' AND name like 'Account')  
BEGIN
	CREATE TABLE [wallet].[Account]
		(
		Id int NOT NULL IDENTITY (1000000, 1),
		Owner varchar(100) NOT NULL
		)  ON [PRIMARY]
	
	ALTER TABLE [wallet].Account ADD CONSTRAINT
		PK_Account PRIMARY KEY CLUSTERED 
		(
		Id
		) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
END

-- Create Transaction Table
IF NOT EXISTS(SELECT * FROM sys.tables WHERE SCHEMA_NAME(schema_id) LIKE 'wallet' AND name like 'Transaction')  
BEGIN
	CREATE TABLE [wallet].[Transaction](
		[Id] [int] IDENTITY(1,1) NOT NULL,
		[ReferenceId] [int] NOT NULL,
		[AccountId] [int] NOT NULL,
		[Amount] [numeric](18, 0) NOT NULL,
		[Direction] [bit] NOT NULL,
	 CONSTRAINT [PK_Transaction] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
	) ON [PRIMARY]
	
	ALTER TABLE [wallet].[Transaction] ADD CONSTRAINT
		FK_Transaction_Account FOREIGN KEY
		(
		AccountId
		) REFERENCES [wallet].[Account]
		(
		Id
		) ON UPDATE  NO ACTION 
		 ON DELETE  NO ACTION 
END

IF NOT EXISTS (SELECT 1 FROM [wallet].[wallet])
BEGIN
	INSERT INTO [wallet].[wallet] (Owner)
	VALUES ('John')
	
END