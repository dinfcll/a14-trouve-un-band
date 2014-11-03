
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 11/03/2014 08:51:16
-- Generated from EDMX file: Z:\a14-trouve-un-band\TrouveUnBand\Models\TUBModels.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [master];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK__Join_Musi__Instr__02084FDA]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Join_Musician_Instrument] DROP CONSTRAINT [FK__Join_Musi__Instr__02084FDA];
GO
IF OBJECT_ID(N'[dbo].[FK__Join_Musi__Music__01142BA1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Join_Musician_Instrument] DROP CONSTRAINT [FK__Join_Musi__Music__01142BA1];
GO
IF OBJECT_ID(N'[dbo].[fk_musicians_users]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Musicians] DROP CONSTRAINT [fk_musicians_users];
GO
IF OBJECT_ID(N'[dbo].[FK_Join_Band_Genre_Bands]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Join_Band_Genre] DROP CONSTRAINT [FK_Join_Band_Genre_Bands];
GO
IF OBJECT_ID(N'[dbo].[FK_Join_Band_Genre_Genres]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Join_Band_Genre] DROP CONSTRAINT [FK_Join_Band_Genre_Genres];
GO
IF OBJECT_ID(N'[dbo].[FK_Join_Band_Musician_Bands]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Join_Band_Musician] DROP CONSTRAINT [FK_Join_Band_Musician_Bands];
GO
IF OBJECT_ID(N'[dbo].[FK_Join_Band_Musician_Musicians]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Join_Band_Musician] DROP CONSTRAINT [FK_Join_Band_Musician_Musicians];
GO
IF OBJECT_ID(N'[dbo].[FK_Join_Musician_Genre_Genres]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Join_Musician_Genre] DROP CONSTRAINT [FK_Join_Musician_Genre_Genres];
GO
IF OBJECT_ID(N'[dbo].[FK_Join_Musician_Genre_Musicians]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Join_Musician_Genre] DROP CONSTRAINT [FK_Join_Musician_Genre_Musicians];
GO
IF OBJECT_ID(N'[dbo].[FK__Advert__Creator__2E1BDC42]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Adverts] DROP CONSTRAINT [FK__Advert__Creator__2E1BDC42];
GO
IF OBJECT_ID(N'[dbo].[FK__Advert__GenresAd__2F10007B]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Adverts] DROP CONSTRAINT [FK__Advert__GenresAd__2F10007B];
GO
IF OBJECT_ID(N'[dbo].[FK__Join_User__Instr__38B96646]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Join_Users_Instrument] DROP CONSTRAINT [FK__Join_User__Instr__38B96646];
GO
IF OBJECT_ID(N'[dbo].[FK__Join_User__UserI__37C5420D]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Join_Users_Instrument] DROP CONSTRAINT [FK__Join_User__UserI__37C5420D];
GO
IF OBJECT_ID(N'[dbo].[FK_Join_Band_Users_Band]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Join_Band_Users] DROP CONSTRAINT [FK_Join_Band_Users_Band];
GO
IF OBJECT_ID(N'[dbo].[FK_Join_Band_Users_User]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Join_Band_Users] DROP CONSTRAINT [FK_Join_Band_Users_User];
GO
IF OBJECT_ID(N'[dbo].[FK_Join_Users_Genre_Genre]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Join_Users_Genre] DROP CONSTRAINT [FK_Join_Users_Genre_Genre];
GO
IF OBJECT_ID(N'[dbo].[FK_Join_Users_Genre_User]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Join_Users_Genre] DROP CONSTRAINT [FK_Join_Users_Genre_User];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Bands]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Bands];
GO
IF OBJECT_ID(N'[dbo].[Genres]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Genres];
GO
IF OBJECT_ID(N'[dbo].[Instruments]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Instruments];
GO
IF OBJECT_ID(N'[dbo].[Join_Musician_Instrument]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Join_Musician_Instrument];
GO
IF OBJECT_ID(N'[dbo].[Musicians]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Musicians];
GO
IF OBJECT_ID(N'[dbo].[Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users];
GO
IF OBJECT_ID(N'[dbo].[Events]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Events];
GO
IF OBJECT_ID(N'[dbo].[Adverts]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Adverts];
GO
IF OBJECT_ID(N'[dbo].[Evenements]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Evenements];
GO
IF OBJECT_ID(N'[dbo].[Join_Users_Instrument]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Join_Users_Instrument];
GO
IF OBJECT_ID(N'[dbo].[Join_Band_Genre]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Join_Band_Genre];
GO
IF OBJECT_ID(N'[dbo].[Join_Band_Musician]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Join_Band_Musician];
GO
IF OBJECT_ID(N'[dbo].[Join_Musician_Genre]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Join_Musician_Genre];
GO
IF OBJECT_ID(N'[dbo].[Join_Band_Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Join_Band_Users];
GO
IF OBJECT_ID(N'[dbo].[Join_Users_Genre]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Join_Users_Genre];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Bands'
CREATE TABLE [dbo].[Bands] (
    [BandId] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(50)  NOT NULL,
    [Description] varchar(max)  NULL,
    [Location] nvarchar(100)  NOT NULL
);
GO

-- Creating table 'Genres'
CREATE TABLE [dbo].[Genres] (
    [GenreId] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'Instruments'
CREATE TABLE [dbo].[Instruments] (
    [InstrumentId] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'Join_Musician_Instrument'
CREATE TABLE [dbo].[Join_Musician_Instrument] (
    [MusicianId] int  NOT NULL,
    [InstrumentId] int  NOT NULL,
    [Skills] int  NOT NULL
);
GO

-- Creating table 'Musicians'
CREATE TABLE [dbo].[Musicians] (
    [MusicianId] int IDENTITY(1,1) NOT NULL,
    [UserId] int  NOT NULL,
    [Description] varchar(max)  NULL
);
GO

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [UserId] int IDENTITY(1,1) NOT NULL,
    [FirstName] nvarchar(100)  NOT NULL,
    [LastName] nvarchar(100)  NOT NULL,
    [BirthDate] datetime  NOT NULL,
    [Nickname] nvarchar(100)  NOT NULL,
    [Email] nvarchar(100)  NOT NULL,
    [Password] nvarchar(100)  NOT NULL,
    [Location] nvarchar(100)  NOT NULL,
    [Photo] varbinary(max)  NULL,
    [Gender] nvarchar(100)  NULL,
    [Latitude] float  NULL,
    [Longitude] float  NULL,
    [Description] varchar(max)  NULL
);
GO

-- Creating table 'Events'
CREATE TABLE [dbo].[Events] (
    [EventId] int IDENTITY(1,1) NOT NULL,
    [EventName] nvarchar(100)  NOT NULL,
    [EventLocation] nvarchar(100)  NOT NULL,
    [EventAddress] nvarchar(100)  NOT NULL,
    [EventCity] nvarchar(100)  NOT NULL,
    [EventDate] datetime  NOT NULL,
    [EventMaxAudience] nvarchar(100)  NOT NULL,
    [EventSalary] real  NOT NULL,
    [EventGender] nvarchar(100)  NOT NULL,
    [EventStageSize] int  NULL,
    [EventPhoto] varbinary(max)  NULL,
    [EventCreator] nvarchar(100)  NULL
);
GO

-- Creating table 'Adverts'
CREATE TABLE [dbo].[Adverts] (
    [AdvertId] int IDENTITY(1,1) NOT NULL,
    [Type] nvarchar(100)  NOT NULL,
    [Creator] int  NOT NULL,
    [GenresAdvert] int  NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [Status] nvarchar(100)  NOT NULL,
    [CreationDate] datetime  NOT NULL,
    [ExpirationDate] datetime  NOT NULL,
    [Location] nvarchar(100)  NULL,
    [AdvertPhoto] varbinary(max)  NULL
);
GO

-- Creating table 'Evenements'
CREATE TABLE [dbo].[Evenements] (
    [EventId] int IDENTITY(1,1) NOT NULL,
    [EventName] nvarchar(100)  NOT NULL,
    [EventLocation] nvarchar(100)  NOT NULL,
    [EventAddress] nvarchar(100)  NOT NULL,
    [EventDate] nvarchar(100)  NOT NULL,
    [EventMaxAudience] nvarchar(100)  NOT NULL,
    [EventSalary] nvarchar(100)  NOT NULL,
    [EventGender] nvarchar(100)  NOT NULL,
    [EventStageSize] int  NULL
);
GO

-- Creating table 'Join_Users_Instrument'
CREATE TABLE [dbo].[Join_Users_Instrument] (
    [UserId] int  NOT NULL,
    [InstrumentId] int  NOT NULL,
    [Skills] int  NOT NULL
);
GO

-- Creating table 'Join_Band_Genre'
CREATE TABLE [dbo].[Join_Band_Genre] (
    [Bands_BandId] int  NOT NULL,
    [Genres_GenreId] int  NOT NULL
);
GO

-- Creating table 'Join_Band_Musician'
CREATE TABLE [dbo].[Join_Band_Musician] (
    [Bands_BandId] int  NOT NULL,
    [Musicians_MusicianId] int  NOT NULL
);
GO

-- Creating table 'Join_Musician_Genre'
CREATE TABLE [dbo].[Join_Musician_Genre] (
    [Genres_GenreId] int  NOT NULL,
    [Musicians_MusicianId] int  NOT NULL
);
GO

-- Creating table 'Join_Band_Users'
CREATE TABLE [dbo].[Join_Band_Users] (
    [Bands_BandId] int  NOT NULL,
    [Users_UserId] int  NOT NULL
);
GO

-- Creating table 'Join_Users_Genre'
CREATE TABLE [dbo].[Join_Users_Genre] (
    [Genres_GenreId] int  NOT NULL,
    [Users_UserId] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [BandId] in table 'Bands'
ALTER TABLE [dbo].[Bands]
ADD CONSTRAINT [PK_Bands]
    PRIMARY KEY CLUSTERED ([BandId] ASC);
GO

-- Creating primary key on [GenreId] in table 'Genres'
ALTER TABLE [dbo].[Genres]
ADD CONSTRAINT [PK_Genres]
    PRIMARY KEY CLUSTERED ([GenreId] ASC);
GO

-- Creating primary key on [InstrumentId] in table 'Instruments'
ALTER TABLE [dbo].[Instruments]
ADD CONSTRAINT [PK_Instruments]
    PRIMARY KEY CLUSTERED ([InstrumentId] ASC);
GO

-- Creating primary key on [MusicianId], [InstrumentId] in table 'Join_Musician_Instrument'
ALTER TABLE [dbo].[Join_Musician_Instrument]
ADD CONSTRAINT [PK_Join_Musician_Instrument]
    PRIMARY KEY CLUSTERED ([MusicianId], [InstrumentId] ASC);
GO

-- Creating primary key on [MusicianId] in table 'Musicians'
ALTER TABLE [dbo].[Musicians]
ADD CONSTRAINT [PK_Musicians]
    PRIMARY KEY CLUSTERED ([MusicianId] ASC);
GO

-- Creating primary key on [UserId] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([UserId] ASC);
GO

-- Creating primary key on [EventId] in table 'Events'
ALTER TABLE [dbo].[Events]
ADD CONSTRAINT [PK_Events]
    PRIMARY KEY CLUSTERED ([EventId] ASC);
GO

-- Creating primary key on [AdvertId] in table 'Adverts'
ALTER TABLE [dbo].[Adverts]
ADD CONSTRAINT [PK_Adverts]
    PRIMARY KEY CLUSTERED ([AdvertId] ASC);
GO

-- Creating primary key on [EventId] in table 'Evenements'
ALTER TABLE [dbo].[Evenements]
ADD CONSTRAINT [PK_Evenements]
    PRIMARY KEY CLUSTERED ([EventId] ASC);
GO

-- Creating primary key on [UserId], [InstrumentId] in table 'Join_Users_Instrument'
ALTER TABLE [dbo].[Join_Users_Instrument]
ADD CONSTRAINT [PK_Join_Users_Instrument]
    PRIMARY KEY CLUSTERED ([UserId], [InstrumentId] ASC);
GO

-- Creating primary key on [Bands_BandId], [Genres_GenreId] in table 'Join_Band_Genre'
ALTER TABLE [dbo].[Join_Band_Genre]
ADD CONSTRAINT [PK_Join_Band_Genre]
    PRIMARY KEY NONCLUSTERED ([Bands_BandId], [Genres_GenreId] ASC);
GO

-- Creating primary key on [Bands_BandId], [Musicians_MusicianId] in table 'Join_Band_Musician'
ALTER TABLE [dbo].[Join_Band_Musician]
ADD CONSTRAINT [PK_Join_Band_Musician]
    PRIMARY KEY NONCLUSTERED ([Bands_BandId], [Musicians_MusicianId] ASC);
GO

-- Creating primary key on [Genres_GenreId], [Musicians_MusicianId] in table 'Join_Musician_Genre'
ALTER TABLE [dbo].[Join_Musician_Genre]
ADD CONSTRAINT [PK_Join_Musician_Genre]
    PRIMARY KEY NONCLUSTERED ([Genres_GenreId], [Musicians_MusicianId] ASC);
GO

-- Creating primary key on [Bands_BandId], [Users_UserId] in table 'Join_Band_Users'
ALTER TABLE [dbo].[Join_Band_Users]
ADD CONSTRAINT [PK_Join_Band_Users]
    PRIMARY KEY NONCLUSTERED ([Bands_BandId], [Users_UserId] ASC);
GO

-- Creating primary key on [Genres_GenreId], [Users_UserId] in table 'Join_Users_Genre'
ALTER TABLE [dbo].[Join_Users_Genre]
ADD CONSTRAINT [PK_Join_Users_Genre]
    PRIMARY KEY NONCLUSTERED ([Genres_GenreId], [Users_UserId] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [InstrumentId] in table 'Join_Musician_Instrument'
ALTER TABLE [dbo].[Join_Musician_Instrument]
ADD CONSTRAINT [FK__Join_Musi__Instr__02084FDA]
    FOREIGN KEY ([InstrumentId])
    REFERENCES [dbo].[Instruments]
        ([InstrumentId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK__Join_Musi__Instr__02084FDA'
CREATE INDEX [IX_FK__Join_Musi__Instr__02084FDA]
ON [dbo].[Join_Musician_Instrument]
    ([InstrumentId]);
GO

-- Creating foreign key on [MusicianId] in table 'Join_Musician_Instrument'
ALTER TABLE [dbo].[Join_Musician_Instrument]
ADD CONSTRAINT [FK__Join_Musi__Music__01142BA1]
    FOREIGN KEY ([MusicianId])
    REFERENCES [dbo].[Musicians]
        ([MusicianId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [UserId] in table 'Musicians'
ALTER TABLE [dbo].[Musicians]
ADD CONSTRAINT [fk_musicians_users]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[Users]
        ([UserId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'fk_musicians_users'
CREATE INDEX [IX_fk_musicians_users]
ON [dbo].[Musicians]
    ([UserId]);
GO

-- Creating foreign key on [Bands_BandId] in table 'Join_Band_Genre'
ALTER TABLE [dbo].[Join_Band_Genre]
ADD CONSTRAINT [FK_Join_Band_Genre_Bands]
    FOREIGN KEY ([Bands_BandId])
    REFERENCES [dbo].[Bands]
        ([BandId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Genres_GenreId] in table 'Join_Band_Genre'
ALTER TABLE [dbo].[Join_Band_Genre]
ADD CONSTRAINT [FK_Join_Band_Genre_Genres]
    FOREIGN KEY ([Genres_GenreId])
    REFERENCES [dbo].[Genres]
        ([GenreId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Join_Band_Genre_Genres'
CREATE INDEX [IX_FK_Join_Band_Genre_Genres]
ON [dbo].[Join_Band_Genre]
    ([Genres_GenreId]);
GO

-- Creating foreign key on [Bands_BandId] in table 'Join_Band_Musician'
ALTER TABLE [dbo].[Join_Band_Musician]
ADD CONSTRAINT [FK_Join_Band_Musician_Bands]
    FOREIGN KEY ([Bands_BandId])
    REFERENCES [dbo].[Bands]
        ([BandId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Musicians_MusicianId] in table 'Join_Band_Musician'
ALTER TABLE [dbo].[Join_Band_Musician]
ADD CONSTRAINT [FK_Join_Band_Musician_Musicians]
    FOREIGN KEY ([Musicians_MusicianId])
    REFERENCES [dbo].[Musicians]
        ([MusicianId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Join_Band_Musician_Musicians'
CREATE INDEX [IX_FK_Join_Band_Musician_Musicians]
ON [dbo].[Join_Band_Musician]
    ([Musicians_MusicianId]);
GO

-- Creating foreign key on [Genres_GenreId] in table 'Join_Musician_Genre'
ALTER TABLE [dbo].[Join_Musician_Genre]
ADD CONSTRAINT [FK_Join_Musician_Genre_Genres]
    FOREIGN KEY ([Genres_GenreId])
    REFERENCES [dbo].[Genres]
        ([GenreId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Musicians_MusicianId] in table 'Join_Musician_Genre'
ALTER TABLE [dbo].[Join_Musician_Genre]
ADD CONSTRAINT [FK_Join_Musician_Genre_Musicians]
    FOREIGN KEY ([Musicians_MusicianId])
    REFERENCES [dbo].[Musicians]
        ([MusicianId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Join_Musician_Genre_Musicians'
CREATE INDEX [IX_FK_Join_Musician_Genre_Musicians]
ON [dbo].[Join_Musician_Genre]
    ([Musicians_MusicianId]);
GO

-- Creating foreign key on [Creator] in table 'Adverts'
ALTER TABLE [dbo].[Adverts]
ADD CONSTRAINT [FK__Advert__Creator__2E1BDC42]
    FOREIGN KEY ([Creator])
    REFERENCES [dbo].[Users]
        ([UserId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK__Advert__Creator__2E1BDC42'
CREATE INDEX [IX_FK__Advert__Creator__2E1BDC42]
ON [dbo].[Adverts]
    ([Creator]);
GO

-- Creating foreign key on [GenresAdvert] in table 'Adverts'
ALTER TABLE [dbo].[Adverts]
ADD CONSTRAINT [FK__Advert__GenresAd__2F10007B]
    FOREIGN KEY ([GenresAdvert])
    REFERENCES [dbo].[Genres]
        ([GenreId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK__Advert__GenresAd__2F10007B'
CREATE INDEX [IX_FK__Advert__GenresAd__2F10007B]
ON [dbo].[Adverts]
    ([GenresAdvert]);
GO

-- Creating foreign key on [InstrumentId] in table 'Join_Users_Instrument'
ALTER TABLE [dbo].[Join_Users_Instrument]
ADD CONSTRAINT [FK__Join_User__Instr__38B96646]
    FOREIGN KEY ([InstrumentId])
    REFERENCES [dbo].[Instruments]
        ([InstrumentId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK__Join_User__Instr__38B96646'
CREATE INDEX [IX_FK__Join_User__Instr__38B96646]
ON [dbo].[Join_Users_Instrument]
    ([InstrumentId]);
GO

-- Creating foreign key on [UserId] in table 'Join_Users_Instrument'
ALTER TABLE [dbo].[Join_Users_Instrument]
ADD CONSTRAINT [FK__Join_User__UserI__37C5420D]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[Users]
        ([UserId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Bands_BandId] in table 'Join_Band_Users'
ALTER TABLE [dbo].[Join_Band_Users]
ADD CONSTRAINT [FK_Join_Band_Users_Band]
    FOREIGN KEY ([Bands_BandId])
    REFERENCES [dbo].[Bands]
        ([BandId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Users_UserId] in table 'Join_Band_Users'
ALTER TABLE [dbo].[Join_Band_Users]
ADD CONSTRAINT [FK_Join_Band_Users_User]
    FOREIGN KEY ([Users_UserId])
    REFERENCES [dbo].[Users]
        ([UserId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Join_Band_Users_User'
CREATE INDEX [IX_FK_Join_Band_Users_User]
ON [dbo].[Join_Band_Users]
    ([Users_UserId]);
GO

-- Creating foreign key on [Genres_GenreId] in table 'Join_Users_Genre'
ALTER TABLE [dbo].[Join_Users_Genre]
ADD CONSTRAINT [FK_Join_Users_Genre_Genre]
    FOREIGN KEY ([Genres_GenreId])
    REFERENCES [dbo].[Genres]
        ([GenreId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Users_UserId] in table 'Join_Users_Genre'
ALTER TABLE [dbo].[Join_Users_Genre]
ADD CONSTRAINT [FK_Join_Users_Genre_User]
    FOREIGN KEY ([Users_UserId])
    REFERENCES [dbo].[Users]
        ([UserId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Join_Users_Genre_User'
CREATE INDEX [IX_FK_Join_Users_Genre_User]
ON [dbo].[Join_Users_Genre]
    ([Users_UserId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------