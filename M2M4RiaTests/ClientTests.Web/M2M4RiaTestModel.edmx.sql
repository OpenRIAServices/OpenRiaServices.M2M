
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 12/14/2010 10:06:53
-- Generated from EDMX file: C:\undergit\RIA\m2m4ria\M2M4RiaTests\ClientTests.Web\M2M4RiaTestModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [M2M4RiaTest];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_AnimalVet_Animal]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AnimalVet] DROP CONSTRAINT [FK_AnimalVet_Animal];
GO
IF OBJECT_ID(N'[dbo].[FK_AnimalVet_Vet]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AnimalVet] DROP CONSTRAINT [FK_AnimalVet_Vet];
GO
IF OBJECT_ID(N'[dbo].[FK_DogFireHydrant_Dog]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DogFireHydrant] DROP CONSTRAINT [FK_DogFireHydrant_Dog];
GO
IF OBJECT_ID(N'[dbo].[FK_DogFireHydrant_FireHydrant]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DogFireHydrant] DROP CONSTRAINT [FK_DogFireHydrant_FireHydrant];
GO
IF OBJECT_ID(N'[dbo].[FK_OwnerAnimal]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Animals] DROP CONSTRAINT [FK_OwnerAnimal];
GO
IF OBJECT_ID(N'[dbo].[FK_DogChewedShoe]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ChewedShoes] DROP CONSTRAINT [FK_DogChewedShoe];
GO
IF OBJECT_ID(N'[dbo].[FK_DogTrainer_Dog]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DogTrainer] DROP CONSTRAINT [FK_DogTrainer_Dog];
GO
IF OBJECT_ID(N'[dbo].[FK_DogTrainer_Trainer]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DogTrainer] DROP CONSTRAINT [FK_DogTrainer_Trainer];
GO
IF OBJECT_ID(N'[dbo].[FK_AnimalFood_Animal]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AnimalFood] DROP CONSTRAINT [FK_AnimalFood_Animal];
GO
IF OBJECT_ID(N'[dbo].[FK_AnimalFood_Food]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AnimalFood] DROP CONSTRAINT [FK_AnimalFood_Food];
GO
IF OBJECT_ID(N'[dbo].[FK_Dog_inherits_Animal]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Animals_Dog] DROP CONSTRAINT [FK_Dog_inherits_Animal];
GO
IF OBJECT_ID(N'[dbo].[FK_Cat_inherits_Animal]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Animals_Cat] DROP CONSTRAINT [FK_Cat_inherits_Animal];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Animals]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Animals];
GO
IF OBJECT_ID(N'[dbo].[Vets]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Vets];
GO
IF OBJECT_ID(N'[dbo].[FireHydrants]', 'U') IS NOT NULL
    DROP TABLE [dbo].[FireHydrants];
GO
IF OBJECT_ID(N'[dbo].[Owners]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Owners];
GO
IF OBJECT_ID(N'[dbo].[ChewedShoes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ChewedShoes];
GO
IF OBJECT_ID(N'[dbo].[Trainers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Trainers];
GO
IF OBJECT_ID(N'[dbo].[Foods]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Foods];
GO
IF OBJECT_ID(N'[dbo].[Animals_Dog]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Animals_Dog];
GO
IF OBJECT_ID(N'[dbo].[Animals_Cat]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Animals_Cat];
GO
IF OBJECT_ID(N'[dbo].[AnimalVet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AnimalVet];
GO
IF OBJECT_ID(N'[dbo].[DogFireHydrant]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DogFireHydrant];
GO
IF OBJECT_ID(N'[dbo].[DogTrainer]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DogTrainer];
GO
IF OBJECT_ID(N'[dbo].[AnimalFood]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AnimalFood];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Animals'
CREATE TABLE [dbo].[Animals] (
    [AnimalId] int IDENTITY(1,1) NOT NULL,
    [OwnerOwnerId] int  NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Vets'
CREATE TABLE [dbo].[Vets] (
    [VetId] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'FireHydrants'
CREATE TABLE [dbo].[FireHydrants] (
    [FireHydrantId] int IDENTITY(1,1) NOT NULL,
    [Location] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Owners'
CREATE TABLE [dbo].[Owners] (
    [OwnerId] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'ChewedShoes'
CREATE TABLE [dbo].[ChewedShoes] (
    [ChewedShoeId] int IDENTITY(1,1) NOT NULL,
    [DogAnimalId] int  NOT NULL,
    [Type] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Trainers'
CREATE TABLE [dbo].[Trainers] (
    [TrainerId] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Foods'
CREATE TABLE [dbo].[Foods] (
    [FoodId] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Animals_Dog'
CREATE TABLE [dbo].[Animals_Dog] (
    [ChasesCars] bit  NOT NULL,
    [AnimalId] int  NOT NULL
);
GO

-- Creating table 'Animals_Cat'
CREATE TABLE [dbo].[Animals_Cat] (
    [SleepsAlot] bit  NOT NULL,
    [AnimalId] int  NOT NULL
);
GO

-- Creating table 'AnimalVet'
CREATE TABLE [dbo].[AnimalVet] (
    [Animals_AnimalId] int  NOT NULL,
    [Vets_VetId] int  NOT NULL
);
GO

-- Creating table 'DogFireHydrant'
CREATE TABLE [dbo].[DogFireHydrant] (
    [DogFireHydrant_FireHydrant_AnimalId] int  NOT NULL,
    [FireHydrants_FireHydrantId] int  NOT NULL
);
GO

-- Creating table 'DogTrainer'
CREATE TABLE [dbo].[DogTrainer] (
    [Dogs_AnimalId] int  NOT NULL,
    [Trainers_TrainerId] int  NOT NULL
);
GO

-- Creating table 'AnimalFood'
CREATE TABLE [dbo].[AnimalFood] (
    [AnimalFood_Food_AnimalId] int  NOT NULL,
    [AnimalFood_Animal_FoodId] int  NOT NULL
);
GO

-- Creating table 'CatAnimal'
CREATE TABLE [dbo].[CatAnimal] (
    [Cats_AnimalId] int  NOT NULL,
    [Animals_AnimalId] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [AnimalId] in table 'Animals'
ALTER TABLE [dbo].[Animals]
ADD CONSTRAINT [PK_Animals]
    PRIMARY KEY CLUSTERED ([AnimalId] ASC);
GO

-- Creating primary key on [VetId] in table 'Vets'
ALTER TABLE [dbo].[Vets]
ADD CONSTRAINT [PK_Vets]
    PRIMARY KEY CLUSTERED ([VetId] ASC);
GO

-- Creating primary key on [FireHydrantId] in table 'FireHydrants'
ALTER TABLE [dbo].[FireHydrants]
ADD CONSTRAINT [PK_FireHydrants]
    PRIMARY KEY CLUSTERED ([FireHydrantId] ASC);
GO

-- Creating primary key on [OwnerId] in table 'Owners'
ALTER TABLE [dbo].[Owners]
ADD CONSTRAINT [PK_Owners]
    PRIMARY KEY CLUSTERED ([OwnerId] ASC);
GO

-- Creating primary key on [ChewedShoeId] in table 'ChewedShoes'
ALTER TABLE [dbo].[ChewedShoes]
ADD CONSTRAINT [PK_ChewedShoes]
    PRIMARY KEY CLUSTERED ([ChewedShoeId] ASC);
GO

-- Creating primary key on [TrainerId] in table 'Trainers'
ALTER TABLE [dbo].[Trainers]
ADD CONSTRAINT [PK_Trainers]
    PRIMARY KEY CLUSTERED ([TrainerId] ASC);
GO

-- Creating primary key on [FoodId] in table 'Foods'
ALTER TABLE [dbo].[Foods]
ADD CONSTRAINT [PK_Foods]
    PRIMARY KEY CLUSTERED ([FoodId] ASC);
GO

-- Creating primary key on [AnimalId] in table 'Animals_Dog'
ALTER TABLE [dbo].[Animals_Dog]
ADD CONSTRAINT [PK_Animals_Dog]
    PRIMARY KEY CLUSTERED ([AnimalId] ASC);
GO

-- Creating primary key on [AnimalId] in table 'Animals_Cat'
ALTER TABLE [dbo].[Animals_Cat]
ADD CONSTRAINT [PK_Animals_Cat]
    PRIMARY KEY CLUSTERED ([AnimalId] ASC);
GO

-- Creating primary key on [Animals_AnimalId], [Vets_VetId] in table 'AnimalVet'
ALTER TABLE [dbo].[AnimalVet]
ADD CONSTRAINT [PK_AnimalVet]
    PRIMARY KEY NONCLUSTERED ([Animals_AnimalId], [Vets_VetId] ASC);
GO

-- Creating primary key on [DogFireHydrant_FireHydrant_AnimalId], [FireHydrants_FireHydrantId] in table 'DogFireHydrant'
ALTER TABLE [dbo].[DogFireHydrant]
ADD CONSTRAINT [PK_DogFireHydrant]
    PRIMARY KEY NONCLUSTERED ([DogFireHydrant_FireHydrant_AnimalId], [FireHydrants_FireHydrantId] ASC);
GO

-- Creating primary key on [Dogs_AnimalId], [Trainers_TrainerId] in table 'DogTrainer'
ALTER TABLE [dbo].[DogTrainer]
ADD CONSTRAINT [PK_DogTrainer]
    PRIMARY KEY NONCLUSTERED ([Dogs_AnimalId], [Trainers_TrainerId] ASC);
GO

-- Creating primary key on [AnimalFood_Food_AnimalId], [AnimalFood_Animal_FoodId] in table 'AnimalFood'
ALTER TABLE [dbo].[AnimalFood]
ADD CONSTRAINT [PK_AnimalFood]
    PRIMARY KEY NONCLUSTERED ([AnimalFood_Food_AnimalId], [AnimalFood_Animal_FoodId] ASC);
GO

-- Creating primary key on [Cats_AnimalId], [Animals_AnimalId] in table 'CatAnimal'
ALTER TABLE [dbo].[CatAnimal]
ADD CONSTRAINT [PK_CatAnimal]
    PRIMARY KEY NONCLUSTERED ([Cats_AnimalId], [Animals_AnimalId] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Animals_AnimalId] in table 'AnimalVet'
ALTER TABLE [dbo].[AnimalVet]
ADD CONSTRAINT [FK_AnimalVet_Animal]
    FOREIGN KEY ([Animals_AnimalId])
    REFERENCES [dbo].[Animals]
        ([AnimalId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Vets_VetId] in table 'AnimalVet'
ALTER TABLE [dbo].[AnimalVet]
ADD CONSTRAINT [FK_AnimalVet_Vet]
    FOREIGN KEY ([Vets_VetId])
    REFERENCES [dbo].[Vets]
        ([VetId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_AnimalVet_Vet'
CREATE INDEX [IX_FK_AnimalVet_Vet]
ON [dbo].[AnimalVet]
    ([Vets_VetId]);
GO

-- Creating foreign key on [DogFireHydrant_FireHydrant_AnimalId] in table 'DogFireHydrant'
ALTER TABLE [dbo].[DogFireHydrant]
ADD CONSTRAINT [FK_DogFireHydrant_Dog]
    FOREIGN KEY ([DogFireHydrant_FireHydrant_AnimalId])
    REFERENCES [dbo].[Animals_Dog]
        ([AnimalId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [FireHydrants_FireHydrantId] in table 'DogFireHydrant'
ALTER TABLE [dbo].[DogFireHydrant]
ADD CONSTRAINT [FK_DogFireHydrant_FireHydrant]
    FOREIGN KEY ([FireHydrants_FireHydrantId])
    REFERENCES [dbo].[FireHydrants]
        ([FireHydrantId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_DogFireHydrant_FireHydrant'
CREATE INDEX [IX_FK_DogFireHydrant_FireHydrant]
ON [dbo].[DogFireHydrant]
    ([FireHydrants_FireHydrantId]);
GO

-- Creating foreign key on [OwnerOwnerId] in table 'Animals'
ALTER TABLE [dbo].[Animals]
ADD CONSTRAINT [FK_OwnerAnimal]
    FOREIGN KEY ([OwnerOwnerId])
    REFERENCES [dbo].[Owners]
        ([OwnerId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_OwnerAnimal'
CREATE INDEX [IX_FK_OwnerAnimal]
ON [dbo].[Animals]
    ([OwnerOwnerId]);
GO

-- Creating foreign key on [DogAnimalId] in table 'ChewedShoes'
ALTER TABLE [dbo].[ChewedShoes]
ADD CONSTRAINT [FK_DogChewedShoe]
    FOREIGN KEY ([DogAnimalId])
    REFERENCES [dbo].[Animals_Dog]
        ([AnimalId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_DogChewedShoe'
CREATE INDEX [IX_FK_DogChewedShoe]
ON [dbo].[ChewedShoes]
    ([DogAnimalId]);
GO

-- Creating foreign key on [Dogs_AnimalId] in table 'DogTrainer'
ALTER TABLE [dbo].[DogTrainer]
ADD CONSTRAINT [FK_DogTrainer_Dog]
    FOREIGN KEY ([Dogs_AnimalId])
    REFERENCES [dbo].[Animals_Dog]
        ([AnimalId])
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

-- Creating foreign key on [AnimalFood_Food_AnimalId] in table 'AnimalFood'
ALTER TABLE [dbo].[AnimalFood]
ADD CONSTRAINT [FK_AnimalFood_Animal]
    FOREIGN KEY ([AnimalFood_Food_AnimalId])
    REFERENCES [dbo].[Animals]
        ([AnimalId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [AnimalFood_Animal_FoodId] in table 'AnimalFood'
ALTER TABLE [dbo].[AnimalFood]
ADD CONSTRAINT [FK_AnimalFood_Food]
    FOREIGN KEY ([AnimalFood_Animal_FoodId])
    REFERENCES [dbo].[Foods]
        ([FoodId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_AnimalFood_Food'
CREATE INDEX [IX_FK_AnimalFood_Food]
ON [dbo].[AnimalFood]
    ([AnimalFood_Animal_FoodId]);
GO

-- Creating foreign key on [Cats_AnimalId] in table 'CatAnimal'
ALTER TABLE [dbo].[CatAnimal]
ADD CONSTRAINT [FK_CatAnimal_Cat]
    FOREIGN KEY ([Cats_AnimalId])
    REFERENCES [dbo].[Animals_Cat]
        ([AnimalId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Animals_AnimalId] in table 'CatAnimal'
ALTER TABLE [dbo].[CatAnimal]
ADD CONSTRAINT [FK_CatAnimal_Animal]
    FOREIGN KEY ([Animals_AnimalId])
    REFERENCES [dbo].[Animals]
        ([AnimalId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CatAnimal_Animal'
CREATE INDEX [IX_FK_CatAnimal_Animal]
ON [dbo].[CatAnimal]
    ([Animals_AnimalId]);
GO

-- Creating foreign key on [AnimalId] in table 'Animals_Dog'
ALTER TABLE [dbo].[Animals_Dog]
ADD CONSTRAINT [FK_Dog_inherits_Animal]
    FOREIGN KEY ([AnimalId])
    REFERENCES [dbo].[Animals]
        ([AnimalId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [AnimalId] in table 'Animals_Cat'
ALTER TABLE [dbo].[Animals_Cat]
ADD CONSTRAINT [FK_Cat_inherits_Animal]
    FOREIGN KEY ([AnimalId])
    REFERENCES [dbo].[Animals]
        ([AnimalId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------