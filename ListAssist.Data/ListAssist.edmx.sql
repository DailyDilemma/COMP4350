-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 01/23/2016 20:27:03
-- Generated from EDMX file: E:\OneDrive\University\COMP4350\Project\Source\COMP4350\ListAssist.WebAPI\Models\ListAssist.edmx
-- --------------------------------------------------

-- Create a new ListAssist database if it doesn't already exist
IF NOT EXISTS (SELECT name FROM master.sys.databases WHERE name = N'ListAssist') 
	CREATE DATABASE ListAssist

SET QUOTED_IDENTIFIER OFF;
GO
USE [ListAssist];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_ListListItem]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ListItems] DROP CONSTRAINT [FK_ListListItem];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Lists]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Lists];
GO
IF OBJECT_ID(N'[dbo].[ListItems]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ListItems];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Lists'
CREATE TABLE [dbo].[Lists] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'ListItems'
CREATE TABLE [dbo].[ListItems] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [Done] bit  NOT NULL,
    [ListId] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Lists'
ALTER TABLE [dbo].[Lists]
ADD CONSTRAINT [PK_Lists]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ListItems'
ALTER TABLE [dbo].[ListItems]
ADD CONSTRAINT [PK_ListItems]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [ListId] in table 'ListItems'
ALTER TABLE [dbo].[ListItems]
ADD CONSTRAINT [FK_ListListItem]
    FOREIGN KEY ([ListId])
    REFERENCES [dbo].[Lists]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ListListItem'
CREATE INDEX [IX_FK_ListListItem]
ON [dbo].[ListItems]
    ([ListId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------