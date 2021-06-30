CREATE DATABASE [BD2CLOB]
GO
USE [BD2CLOB]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Documents] (
    [Id] int NOT NULL IDENTITY,
    [Title] nvarchar(255) NOT NULL,
    [Author] nvarchar(255) NOT NULL,
    [Description] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Documents] PRIMARY KEY ([Id])
);
GO