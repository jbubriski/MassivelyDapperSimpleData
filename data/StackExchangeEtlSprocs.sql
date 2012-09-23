SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[usp_ETL_Load_Badges]
AS
  SET NOCOUNT  ON
  
  CREATE TABLE #WorkingTable (
    Data XML)
  
  /* Create the badges table if it doesn't exist. I added a numeric PK for uniqueness even though the file didn't have one. */
  IF NOT EXISTS (SELECT *
                 FROM   sys.objects
                 WHERE  object_id = OBJECT_ID(N'[dbo].[Badges]')
                        AND type in (N'U'))
    CREATE TABLE [dbo].[Badges] (
      [Id]            [int]   IDENTITY ( 1 , 1 )   NOT NULL
      ,[UserId]       [int]   NULL
      ,[Name]         [nvarchar](50)   NULL
      ,[CreationDate] [datetime]   NULL,
      CONSTRAINT [PK_Badges] PRIMARY KEY CLUSTERED ( [Id] ASC ) WITH ( PAD_INDEX = OFF,STATISTICS_NORECOMPUTE = OFF,IGNORE_DUP_KEY = OFF,ALLOW_ROW_LOCKS = ON,ALLOW_PAGE_LOCKS = ON ) ON [PRIMARY])
    ON [PRIMARY]
  ELSE
    TRUNCATE TABLE dbo.Badges
  
  /* Populate the temp table with the badges.xml file */
  INSERT INTO #WorkingTable
  SELECT *
  FROM   OPENROWSET(BULK 'c:\temp\badges.xml',SINGLE_BLOB) AS data
  
  /* Import the users records from the working table */
  DECLARE  @XML    AS XML
           ,@hDoc  AS INT
  
  SELECT @XML = Data
  FROM   #WorkingTable
  
  EXEC sp_xml_preparedocument
    @hDoc OUTPUT ,
    @XML
  
  INSERT INTO dbo.Badges
             (UserId
              ,Name
              ,CreationDate)
  SELECT UserId
         ,Name
         ,CAST(CreationDate AS DATETIME)
  FROM   OPENXML (@hDoc, '/badges/row', 1)
            WITH (UserId       INT          '@UserId',
                  Name         NVARCHAR(50) '@Name',
                  CreationDate VARCHAR(50)  '@Date')
  
  /* Clean up and empty out our temporary table */
  EXEC sp_xml_removedocument
    @hDoc
  
  DROP TABLE #WorkingTable
  
  SET NOCOUNT  OFF



GO

/****** Object:  StoredProcedure [dbo].[usp_ETL_Load_Comments]    Script Date: 06/08/2009 07:08:38 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[usp_ETL_Load_Comments]
AS
  SET NOCOUNT  ON
  
  CREATE TABLE #WorkingTable (
    Data XML)
  
  /* Create the Comments table */
  IF NOT EXISTS (SELECT *
                 FROM   sys.objects
                 WHERE  object_id = OBJECT_ID(N'[dbo].[Comments]')
                        AND type in (N'U'))
    CREATE TABLE [dbo].[Comments] (
      [Id]            [int]   NOT NULL
      ,[PostId]       [int]   NULL
      ,[Text]         [nvarchar](max)   NULL
      ,[CreationDate] [datetime]   NULL
      ,[UserId]       [int]   NULL,
      CONSTRAINT [PK_Comments] PRIMARY KEY CLUSTERED ( [Id] ASC ) WITH ( PAD_INDEX = OFF,STATISTICS_NORECOMPUTE = OFF,IGNORE_DUP_KEY = OFF,ALLOW_ROW_LOCKS = ON,ALLOW_PAGE_LOCKS = ON ) ON [PRIMARY])
    ON [PRIMARY]
  ELSE
    TRUNCATE TABLE dbo.Comments
  
  /* Populate the temp table with the comments.xml file */
  INSERT INTO #WorkingTable
  SELECT *
  FROM   OPENROWSET(BULK 'c:\temp\comments.xml',SINGLE_BLOB) AS data
  
  /* Import the users records from the working table */
  DECLARE  @XML    AS XML
           ,@hDoc  AS INT
  
  SELECT @XML = Data
  FROM   #WorkingTable
  
  EXEC sp_xml_preparedocument
    @hDoc OUTPUT ,
    @XML
  
  INSERT INTO dbo.Comments
             (Id
              ,PostId
              ,[Text]
              ,CreationDate
              ,UserId)
  SELECT Id
         ,PostId
         ,[Text]
         ,CAST(CreationDate AS DATETIME)
         ,UserId
  FROM   OPENXML (@hDoc, '/comments/row', 1)
            WITH (Id           INT           '@Id',
                  PostId       INT           '@PostId',
                  [Text]       NVARCHAR(MAX) '@Text',
                  CreationDate VARCHAR(50)   '@CreationDate',
                  UserId       INT           '@UserId')
  
  /* Clean up and empty out our temporary table */
  EXEC sp_xml_removedocument
    @hDoc
  
  DROP TABLE #WorkingTable
  
  SET NOCOUNT  OFF

GO

/****** Object:  StoredProcedure [dbo].[usp_ETL_Load_Posts]    Script Date: 06/08/2009 07:08:38 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[usp_ETL_Load_Posts]
AS
  SET NOCOUNT  ON
  
  CREATE TABLE #WorkingTable (
    Data XML)
  
  /* Create the Posts table */
  IF NOT EXISTS (SELECT *
                 FROM   sys.objects
                 WHERE  object_id = OBJECT_ID(N'[dbo].[Posts]')
                        AND type in (N'U'))
CREATE TABLE [dbo].[Posts](
  [Id] [int] NOT NULL,
  [PostTypeId] [int] NULL,
  [AcceptedAnswerId] [int] NULL,
  [CreationDate] [datetime] NULL,
  [Score] [int] NULL,
  [ViewCount] [int] NULL,
  [Body] [nvarchar](max) NULL,
  [OwnerUserId] [int] NULL,
  [OwnerDisplayName] [nvarchar](40) NULL,
  [LastEditorUserId] [int] NULL,
  [LastEditDate] [datetime] NULL,
  [LastActivityDate] [datetime] NULL,
  [Title] [nvarchar](250) NULL,
  [Tags] [nvarchar](150) NULL,
  [AnswerCount] [int] NULL,
  [CommentCount] [int] NULL,
  [FavoriteCount] [int] NULL,
  [ClosedDate] [datetime] NULL,
  [ParentId] [int] NULL,
 CONSTRAINT [PK_Posts] PRIMARY KEY CLUSTERED 
(
  [Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
  ELSE
    TRUNCATE TABLE dbo.Posts
  
  /* Populate the temp table with the posts.xml file */
  INSERT INTO #WorkingTable
  SELECT *
  FROM   OPENROWSET(BULK 'c:\temp\posts.xml',SINGLE_BLOB) AS data
  
  /* Import the posts records from the working table */
  DECLARE  @XML    AS XML
           ,@hDoc  AS INT
  
  SELECT @XML = Data
  FROM   #WorkingTable
  
  EXEC sp_xml_preparedocument
    @hDoc OUTPUT ,
    @XML
  
  INSERT INTO dbo.Posts
             (Id
              ,PostTypeId
              ,AcceptedAnswerId
              ,CreationDate
              ,Score
              ,ViewCount
              ,Body
              ,OwnerUserId
              ,OwnerDisplayName
              ,LastEditorUserId
              ,LastEditDate
              ,LastActivityDate
              ,Title
              ,Tags
              ,AnswerCount
              ,CommentCount
              ,FavoriteCount
              ,ClosedDate
              ,ParentId
)
  SELECT Id
         ,PostTypeId
         ,AcceptedAnswerId
         ,CAST(CreationDate AS DATETIME)
         ,Score
         ,ViewCount
         ,Body
         ,OwnerUserId
         ,OwnerDisplayName
         ,LastEditorUserId
         ,CAST(LastEditDate AS DATETIME)
         ,CAST(LastActivityDate AS DATETIME)
         ,Title
         ,Tags
         ,AnswerCount
         ,CommentCount
         ,FavoriteCount
         ,CAST(ClosedDate AS DATETIME)
         ,ParentId
  FROM   OPENXML (@hDoc, '/posts/row', 1)
            WITH (Id               INT           '@Id',
                  PostTypeId       INT           '@PostTypeId',
                  AcceptedAnswerId INT           '@AcceptedAnswerId',
                  CreationDate     VARCHAR(50)   '@CreationDate',
                  Score            INT           '@Score',
                  ViewCount        INT           '@ViewCount',
                  Body             NVARCHAR(MAX) '@Body',
                  OwnerUserId      INT           '@OwnerUserId',
                  OwnerDisplayName NVARCHAR(40)  '@OwnerDisplayName',
                  LastEditorUserId INT           '@LastEditorUserId',
                  LastEditDate     VARCHAR(50)   '@LastEditDate',
                  LastActivityDate VARCHAR(50)   '@LastActivityDate',
                  Title            NVARCHAR(250) '@Title',
                  Tags             NVARCHAR(150) '@Tags',
                  AnswerCount      INT           '@AnswerCount',
                  CommentCount     INT           '@CommentCount',
                  FavoriteCount    INT           '@FavoriteCount',
                  ParentId         INT           '@ParentId',
                  ClosedDate       VARCHAR(50)   '@ClosedDate')
  
  /* Clean up and empty out our temporary table */
  EXEC sp_xml_removedocument
    @hDoc
  
  DELETE #WorkingTable
  
  SET NOCOUNT  OFF


GO


/****** Object:  StoredProcedure [dbo].[usp_ETL_Load_Users]    Script Date: 06/08/2009 07:08:38 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[usp_ETL_Load_Users]
AS
  SET NOCOUNT  ON
  
  CREATE TABLE #WorkingTable (
    Data XML)
  
  /* Create the Users table */
  IF NOT EXISTS (SELECT *
                 FROM   sys.objects
                 WHERE  object_id = OBJECT_ID(N'[dbo].[Users]')
                        AND type in (N'U'))
    CREATE TABLE [dbo].[Users] (
      [Id]              [int]   NOT NULL
      ,[Reputation]     [int]   NULL
      ,[CreationDate]   [datetime]   NULL
      ,[DisplayName]    [nvarchar](40)   NULL
      ,[LastAccessDate] [datetime]   NULL
      ,[WebsiteUrl]     [nvarchar](200)   NULL
      ,[Location]       [nvarchar](100)   NULL
      ,[Age]            [int]   NULL
      ,[AboutMe]        [nvarchar](max)   NULL
      ,[Views]          [int]   NULL
      ,[UpVotes]        [int]   NULL
      ,[DownVotes]      [int]   NULL,
      CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED ( [Id] ASC ) WITH ( PAD_INDEX = OFF,STATISTICS_NORECOMPUTE = OFF,IGNORE_DUP_KEY = OFF,ALLOW_ROW_LOCKS = ON,ALLOW_PAGE_LOCKS = ON ) ON [PRIMARY])
    ON [PRIMARY]
  ELSE
    TRUNCATE TABLE dbo.Users
  
  /* Populate the temp table with the users.xml file */
  INSERT INTO #WorkingTable
  SELECT *
  FROM   OPENROWSET(BULK 'c:\temp\users.xml',SINGLE_BLOB) AS data
  
  /* Import the users records from the working table */
  DECLARE  @XML    AS XML
           ,@hDoc  AS INT
  
  SELECT @XML = Data
  FROM   #WorkingTable
  
  EXEC sp_xml_preparedocument
    @hDoc OUTPUT ,
    @XML
  
  INSERT INTO dbo.Users
             (Id
              ,Reputation
              ,CreationDate
              ,DisplayName
              ,LastAccessDate
              ,WebsiteUrl
              ,Location
              ,Age
              ,AboutMe
              ,[Views]
              ,UpVotes
              ,DownVotes)
  SELECT Id
         ,Reputation
         ,CAST(CreationDate AS DATETIME)
         ,DisplayName
         ,CAST(LastAccessDate AS DATETIME)
         ,WebsiteUrl
         ,Location
         ,Age
         ,AboutMe
         ,[Views]
         ,UpVotes
         ,DownVotes
  FROM   OPENXML (@hDoc, '/users/row', 1)
            WITH (Id             INT           '@Id',
                  Reputation     INT           '@Reputation',
                  CreationDate   VARCHAR(50)   '@CreationDate',
                  DisplayName    NVARCHAR(40)  '@DisplayName',
                  LastAccessDate VARCHAR(50)   '@LastAccessDate',
                  WebsiteUrl     NVARCHAR(200) '@WebsiteUrl',
                  Location       NVARCHAR(100) '@Location',
                  Age            INT           '@Age',
                  AboutMe        NVARCHAR(MAX) '@AboutMe',
                  [Views]        INT           '@Views',
                  UpVotes        INT           '@UpVotes',
                  DownVotes      INT           '@DownVotes')
  
  /* Clean up and empty out our temporary table */
  EXEC sp_xml_removedocument
    @hDoc
  
  DROP TABLE #WorkingTable
  
  SET NOCOUNT  OFF


GO

/****** Object:  StoredProcedure [dbo].[usp_ETL_Load_Votes]    Script Date: 06/08/2009 07:08:38 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[usp_ETL_Load_Votes]
AS
  SET NOCOUNT  ON
  
  CREATE TABLE #WorkingTable (
    Data XML)
  
  /* Create the votes table */
  IF NOT EXISTS (SELECT *
                 FROM   sys.objects
                 WHERE  object_id = OBJECT_ID(N'[dbo].[Votes]')
                        AND type in (N'U'))
    CREATE TABLE [dbo].[Votes] (
      [Id]            [int]   NOT NULL
      ,[PostId]       [int]   NULL
      ,[VoteTypeId]   [int]   NULL
      ,[CreationDate] [datetime]   NULL
      ,[UserId] [int] NULL
      ,CONSTRAINT [PK_Votes] PRIMARY KEY CLUSTERED ( [Id] ASC ) WITH ( PAD_INDEX = OFF,STATISTICS_NORECOMPUTE = OFF,IGNORE_DUP_KEY = OFF,ALLOW_ROW_LOCKS = ON,ALLOW_PAGE_LOCKS = ON ) ON [PRIMARY])
    ON [PRIMARY]
  ELSE
    TRUNCATE TABLE dbo.Votes
  
  /* Populate the temp table with the votes.xml file */
  INSERT INTO #WorkingTable
  SELECT *
  FROM   OPENROWSET(BULK 'c:\temp\votes.xml',SINGLE_BLOB) AS data
  
  /* Import the users records from the working table */
  DECLARE  @XML    AS XML
           ,@hDoc  AS INT
  
  SELECT @XML = Data
  FROM   #WorkingTable
  
  EXEC sp_xml_preparedocument
    @hDoc OUTPUT ,
    @XML
  
  INSERT INTO dbo.Votes
             (Id
              ,PostId
              ,VoteTypeId
              ,CreationDate
              ,UserId)
  SELECT Id
         ,PostId
         ,VoteTypeId
         ,CAST(CreationDate AS DATETIME)
         ,UserId
  FROM   OPENXML (@hDoc, '/votes/row', 1)
            WITH (Id           INT         '@Id',
                  PostId       INT         '@PostId',
                  VoteTypeId   INT         '@VoteTypeId',
                  CreationDate VARCHAR(50) '@CreationDate',
                  UserId INT '@UserId')
  
  /* Clean up and empty out our temporary table */
  EXEC sp_xml_removedocument
    @hDoc
  
  DROP TABLE #WorkingTable


GO


CREATE PROCEDURE [dbo].[usp_ETL_Load_PostsTags]
AS
  SET NOCOUNT  ON
  
  /* Reload PostsTags */
  DECLARE  @PostId INT
           ,@Tags  NVARCHAR(150)
  
  IF NOT EXISTS (SELECT *
                 FROM   sys.objects
                 WHERE  object_id = OBJECT_ID(N'[dbo].[PostsTags]')
                        AND type in (N'U'))
    CREATE TABLE [dbo].[PostsTags] (
      [PostId] [int]   NOT NULL
      ,[Tag]   [nvarchar](50)   NOT NULL,
      CONSTRAINT [PK_PostsTags] PRIMARY KEY CLUSTERED ( [PostId] ASC,[Tag] ASC ) WITH ( PAD_INDEX = OFF,STATISTICS_NORECOMPUTE = OFF,IGNORE_DUP_KEY = OFF,ALLOW_ROW_LOCKS = ON,ALLOW_PAGE_LOCKS = ON ) ON [PRIMARY])
    ON [PRIMARY]
  ELSE
    TRUNCATE TABLE dbo.PostsTags
  
  SET @PostId = (SELECT   TOP 1 Id
                 FROM     dbo.Posts WITH (NOLOCK)
                 WHERE    Tags IS NOT NULL
                 ORDER BY Id)
  
  WHILE @PostId IS NOT NULL
    BEGIN
      SELECT TOP 1 @Tags = Tags
      FROM   dbo.Posts
      WHERE  Id = @PostId
      
      INSERT INTO dbo.PostsTags
                 (PostId
                  ,Tag)
      SELECT @PostId
             ,fnt.Tag
      FROM   dbo.fn_NormalizeTags(@Tags) fnt
             LEFT OUTER JOIN dbo.PostsTags pt
               ON pt.PostId = @PostId
                  AND fnt.Tag = pt.Tag
      WHERE  pt.PostId IS NULL
      
      SET @PostId = (SELECT TOP 1 Id
                     FROM   dbo.Posts
                     WHERE  Id > @PostId
                            AND Tags IS NOT NULL)
    END
  
  SET NOCOUNT  OFF
GO
CREATE FUNCTION [dbo].[fn_NormalizeTags] (@Tags NVARCHAR(150)) 
RETURNS @TagsOutput TABLE (Tag NVARCHAR(50)) 
AS
BEGIN
  WHILE LEN(@Tags) > 0
  BEGIN
    IF NOT EXISTS (SELECT Tag FROM @TagsOutput WHERE Tag = ( SUBSTRING(@Tags, (CHARINDEX('<',@Tags,1) + 1),(CHARINDEX('>',@Tags,1)-2))))
    BEGIN
      INSERT INTO @TagsOutput (Tag)
        VALUES( SUBSTRING(@Tags, (CHARINDEX('<',@Tags,1) + 1),(CHARINDEX('>',@Tags,1)-2)))
    END
    SET @Tags = RIGHT(@Tags, (LEN(@Tags) - CHARINDEX('>',@Tags,1)))
  END
  RETURN
END
GO
