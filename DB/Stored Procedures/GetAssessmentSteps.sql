CREATE PROCEDURE dbo.GetArtistDetails
	@artistId INT
AS
BEGIN	
	SELECT [title]
	  ,[biography]
	  ,[imageURL]
	  ,[heroURL]
	FROM [dbo].[Artist]
	WHERE  artistID = @artistId

	SELECT [title]
      ,[imageURL]
      ,[year]
	FROM [dbo].[Album]
	WHERE artistID = @artistId
	
	SELECT A.[imageURL]
	  ,A.[title] AS albumName
	  ,S.[title] AS songName
      ,S.[bpm]
      ,S.[timeSignature]
      ,S.[multitracks]
      ,S.[customMix]
      ,S.[chart]
      ,S.[rehearsalMix]
      ,S.[patches]
      ,S.[songSpecificPatches]
      ,S.[proPresenter]
	FROM [dbo].[Song] S
	LEFT JOIN [dbo].[Album] A
	ON S.albumID = A.albumID
	WHERE S.artistID = @artistId
END
