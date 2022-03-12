
if not exists (select * from dbo.sysobjects where id = object_id(N'{databaseOwner}[{objectQualifier}DNN_IdentitySwitcherLog]') and OBJECTPROPERTY(id, N'IsTable') = 1)
	BEGIN
		CREATE TABLE {databaseOwner}[{objectQualifier}DNN_IdentitySwitcherLog]
		(
			Id int NOT NULL IDENTITY (1, 1),
			RequestId nvarchar(50) NOT NULL,
			RequestByUserId int NOT NULL,
			RequestDate datetime NOT NULL,
			RequestIP nvarchar(50) NOT NULL,
			SwitchToUserId int NOT NULL,
			ApprovalDate datetime NULL,
			ApprovalByUserId int NULL,
			ApprovalIP nvarchar(50) NULL
		) 

		ALTER TABLE {databaseOwner}[{objectQualifier}DNN_IdentitySwitcherLog] ADD CONSTRAINT [PK_{objectQualifier}DNN_IdentitySwitcherLog] PRIMARY KEY CLUSTERED ([Id])
	END
GO
