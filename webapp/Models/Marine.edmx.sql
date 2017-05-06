
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 05/05/2017 18:52:25
-- Generated from EDMX file: C:\Users\Karolinka\Documents\Visual Studio 2015\Projects\webapp\webapp\webapp\Models\Marine.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [marine];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_PortSkipper_Port]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PortSkipper] DROP CONSTRAINT [FK_PortSkipper_Port];
GO
IF OBJECT_ID(N'[dbo].[FK_PortSkipper_Skipper]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PortSkipper] DROP CONSTRAINT [FK_PortSkipper_Skipper];
GO
IF OBJECT_ID(N'[dbo].[FK_BoatSkipper]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[BoatSet] DROP CONSTRAINT [FK_BoatSkipper];
GO
IF OBJECT_ID(N'[dbo].[FK_BoatPort]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[BoatSet] DROP CONSTRAINT [FK_BoatPort];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[PortSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PortSet];
GO
IF OBJECT_ID(N'[dbo].[BoatSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[BoatSet];
GO
IF OBJECT_ID(N'[dbo].[SkipperSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SkipperSet];
GO
IF OBJECT_ID(N'[dbo].[PortSkipper]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PortSkipper];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'PortSet'
CREATE TABLE [dbo].[PortSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Location] nvarchar(max)  NOT NULL,
    [Spots] int  NOT NULL,
    [Contact] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'BoatSet'
CREATE TABLE [dbo].[BoatSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Type] nvarchar(max)  NOT NULL,
    [Model] nvarchar(max)  NOT NULL,
    [Price] float  NOT NULL,
    [Bunks] int  NOT NULL,
    [isRented] bit  NOT NULL,
    [SkipperId] int  NOT NULL,
    [PortId] int  NOT NULL
);
GO

-- Creating table 'SkipperSet'
CREATE TABLE [dbo].[SkipperSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Email] nvarchar(max)  NOT NULL,
    [PhoneNumber] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'PortSkipper'
CREATE TABLE [dbo].[PortSkipper] (
    [Port_Id] int  NOT NULL,
    [Skipper_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'PortSet'
ALTER TABLE [dbo].[PortSet]
ADD CONSTRAINT [PK_PortSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'BoatSet'
ALTER TABLE [dbo].[BoatSet]
ADD CONSTRAINT [PK_BoatSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'SkipperSet'
ALTER TABLE [dbo].[SkipperSet]
ADD CONSTRAINT [PK_SkipperSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Port_Id], [Skipper_Id] in table 'PortSkipper'
ALTER TABLE [dbo].[PortSkipper]
ADD CONSTRAINT [PK_PortSkipper]
    PRIMARY KEY CLUSTERED ([Port_Id], [Skipper_Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Port_Id] in table 'PortSkipper'
ALTER TABLE [dbo].[PortSkipper]
ADD CONSTRAINT [FK_PortSkipper_Port]
    FOREIGN KEY ([Port_Id])
    REFERENCES [dbo].[PortSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Skipper_Id] in table 'PortSkipper'
ALTER TABLE [dbo].[PortSkipper]
ADD CONSTRAINT [FK_PortSkipper_Skipper]
    FOREIGN KEY ([Skipper_Id])
    REFERENCES [dbo].[SkipperSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PortSkipper_Skipper'
CREATE INDEX [IX_FK_PortSkipper_Skipper]
ON [dbo].[PortSkipper]
    ([Skipper_Id]);
GO

-- Creating foreign key on [SkipperId] in table 'BoatSet'
ALTER TABLE [dbo].[BoatSet]
ADD CONSTRAINT [FK_BoatSkipper]
    FOREIGN KEY ([SkipperId])
    REFERENCES [dbo].[SkipperSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_BoatSkipper'
CREATE INDEX [IX_FK_BoatSkipper]
ON [dbo].[BoatSet]
    ([SkipperId]);
GO

-- Creating foreign key on [PortId] in table 'BoatSet'
ALTER TABLE [dbo].[BoatSet]
ADD CONSTRAINT [FK_BoatPort]
    FOREIGN KEY ([PortId])
    REFERENCES [dbo].[PortSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_BoatPort'
CREATE INDEX [IX_FK_BoatPort]
ON [dbo].[BoatSet]
    ([PortId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------