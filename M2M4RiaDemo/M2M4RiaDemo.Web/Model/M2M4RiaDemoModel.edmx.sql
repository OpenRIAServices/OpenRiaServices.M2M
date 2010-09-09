
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 08/28/2010 22:06:59
-- Generated from EDMX file: C:\undergit\RIA\m2m4ria\M2M4RiaDemo\M2M4RiaDemo.Web\Model\M2M4RiaDemoModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [M2M4RiaDemo];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_DogTrainer_Dog]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DogTrainer] DROP CONSTRAINT [FK_DogTrainer_Dog];
GO
IF OBJECT_ID(N'[dbo].[FK_DogTrainer_Trainer]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DogTrainer] DROP CONSTRAINT [FK_DogTrainer_Trainer];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Trainers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Trainers];
GO
IF OBJECT_ID(N'[dbo].[Animals_Dog]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Animals_Dog];
GO
IF OBJECT_ID(N'[dbo].[DogTrainer]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DogTrainer];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Trainers'
CREATE TABLE [dbo].[Trainers] (
    [TrainerId] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Dogs'
CREATE TABLE [dbo].[Dogs] (
    [ChasesCars] bit  NOT NULL,
    [DogId] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'DogTrainer'
CREATE TABLE [dbo].[DogTrainer] (
    [Dogs_DogId] int  NOT NULL,
    [Trainers_TrainerId] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [TrainerId] in table 'Trainers'
ALTER TABLE [dbo].[Trainers]
ADD CONSTRAINT [PK_Trainers]
    PRIMARY KEY CLUSTERED ([TrainerId] ASC);
GO

-- Creating primary key on [DogId] in table 'Dogs'
ALTER TABLE [dbo].[Dogs]
ADD CONSTRAINT [PK_Dogs]
    PRIMARY KEY CLUSTERED ([DogId] ASC);
GO

-- Creating primary key on [Dogs_DogId], [Trainers_TrainerId] in table 'DogTrainer'
ALTER TABLE [dbo].[DogTrainer]
ADD CONSTRAINT [PK_DogTrainer]
    PRIMARY KEY NONCLUSTERED ([Dogs_DogId], [Trainers_TrainerId] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Dogs_DogId] in table 'DogTrainer'
ALTER TABLE [dbo].[DogTrainer]
ADD CONSTRAINT [FK_DogTrainer_Dog]
    FOREIGN KEY ([Dogs_DogId])
    REFERENCES [dbo].[Dogs]
        ([DogId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Trainers_TrainerId] in table 'DogTrainer'
ALTER TABLE [dbo].[DogTrainer]
ADD CONSTRAINT [FK_DogTrainer_Trainer]
    FOREIGN KEY ([Trainers_TrainerId])
    REFERENCES [dbo].[Trainers]
        ([TrainerId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_DogTrainer_Trainer'
CREATE INDEX [IX_FK_DogTrainer_Trainer]
ON [dbo].[DogTrainer]
    ([Trainers_TrainerId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------