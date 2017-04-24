INSERT INTO [MailPusherData].[dbo].[Publishers](
		[Name],					[Domain],											[Language],
		[Status],				[Created],											[CreatorId],
		[Updated],				[UpdaterId],										[CountryCompanyCode],
		[AmountEmployees],		[NACEID])
SELECT	d.[Name],				d.F4,												'DK',
		1,						GETDATE(),											'0c7370f8-5ba7-4c2c-8cc2-27a68eab2c2e',
		GETDATE(),				'0c7370f8-5ba7-4c2c-8cc2-27a68eab2c2e',				d.CountryCompanyCode,
		d.AmountEmployees,		n.ID
FROM [tmpData].[dbo].[Denmark$] d
JOIN [MailPusherData].dbo.NACE n
	ON d.NACE = n.Code 