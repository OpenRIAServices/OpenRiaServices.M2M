
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 07/28/2010 20:56:11
-- Generated from EDMX file: C:\undergit\RIA\RIAM2M\M2MDemo\M2MDemo.Web\M2MModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [M2M];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_PatientDoctor_Patient]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PatientDoctor] DROP CONSTRAINT [FK_PatientDoctor_Patient];
GO
IF OBJECT_ID(N'[dbo].[FK_PatientDoctor_Doctor]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PatientDoctor] DROP CONSTRAINT [FK_PatientDoctor_Doctor];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Patients]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Patients];
GO
IF OBJECT_ID(N'[dbo].[Doctors]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Doctors];
GO
IF OBJECT_ID(N'[dbo].[PatientDoctor]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PatientDoctor];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Patients'
CREATE TABLE [dbo].[Patients] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Doctors'
CREATE TABLE [dbo].[Doctors] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'PatientDoctor'
CREATE TABLE [dbo].[PatientDoctor] (
    [PatientSet_Id] int  NOT NULL,
    [DoctorSet_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Patients'
ALTER TABLE [dbo].[Patients]
ADD CONSTRAINT [PK_Patients]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Doctors'
ALTER TABLE [dbo].[Doctors]
ADD CONSTRAINT [PK_Doctors]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [PatientSet_Id], [DoctorSet_Id] in table 'PatientDoctor'
ALTER TABLE [dbo].[PatientDoctor]
ADD CONSTRAINT [PK_PatientDoctor]
    PRIMARY KEY NONCLUSTERED ([PatientSet_Id], [DoctorSet_Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [PatientSet_Id] in table 'PatientDoctor'
ALTER TABLE [dbo].[PatientDoctor]
ADD CONSTRAINT [FK_PatientDoctor_Patient]
    FOREIGN KEY ([PatientSet_Id])
    REFERENCES [dbo].[Patients]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [DoctorSet_Id] in table 'PatientDoctor'
ALTER TABLE [dbo].[PatientDoctor]
ADD CONSTRAINT [FK_PatientDoctor_Doctor]
    FOREIGN KEY ([DoctorSet_Id])
    REFERENCES [dbo].[Doctors]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_PatientDoctor_Doctor'
CREATE INDEX [IX_FK_PatientDoctor_Doctor]
ON [dbo].[PatientDoctor]
    ([DoctorSet_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------