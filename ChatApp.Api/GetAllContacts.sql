USE [ChatApp]
GO
/****** Object:  StoredProcedure [dbo].[GetAllContacts]    Script Date: 6/18/2024 4:03:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetAllContacts] 
AS
BEGIN
	SELECT C.[Id]
	,C.[ContactName]
	,P.[Id] AS ProfilePhoto
	FROM Contacts AS C
	JOIN ProfilePhotos AS P
	ON P.UserId = C.UserId
	ORDER BY C.[ContactName]
END
