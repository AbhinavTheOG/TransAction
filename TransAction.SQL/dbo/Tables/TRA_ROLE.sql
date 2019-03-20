﻿CREATE TABLE [dbo].[TRA_ROLE] (
    [ROLE_ID]     INT            IDENTITY (1, 1) NOT NULL,
    [NAME] VARCHAR(255)            NOT NULL,
    [DESCRIPTION]  VARCHAR(1024) NOT NULL,
    [EFFECTIVE_START_DATE] DATETIME NULL, 
	[EFFECTIVE_END_DATE] DATETIME NULL, 
	[CREATED_BY_USER] VARCHAR(255) NULL, 
	[CREATED_BY_DATE] DATETIME NULL, 
	[LAST_UPDATED_BY_USER] VARCHAR(255) NULL, 
	[LAST_UPDATED_BY_DATE] DATETIME NULL, 
    CONSTRAINT [PK_ROLE] PRIMARY KEY CLUSTERED ([ROLE_ID] ASC)
);
