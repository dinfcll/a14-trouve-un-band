create table Users(
	UserId int IDENTITY(1,1) Primary key,
	FirstName NVARCHAR(100) NOT NULL,
	LastName NVARCHAR(100) NOT NULL,
	BirthDate DATETIME NOT NULL,
	Nickname NVARCHAR(100) NOT NULL,
	Email NVARCHAR(100) NOT NULL,
	Password NVARCHAR(100) NOT NULL,
	Location NVARCHAR(100) NOT NULL
);

create table Musicians(
	MusicianId int IDENTITY(1,1) Primary key,
	UserId int constraint fk_musicians_users REFERENCES Users(UserId) not null,
	Description text null
);

create table Bands(
	BandId int IDENTITY(1,1) Primary key,
	Name NVARCHAR(50) not null,
	Description text null,
	Location NVARCHAR(100) NOT NULL
);

create table Instruments(
	InstrumentId int IDENTITY(1,1) Primary Key,
	Name NVARCHAR(50) not null
);

create table Genres(
	GenreId int IDENTITY(1,1) Primary Key,
	Name NVARCHAR(50) not null
);

create table Join_Musician_Genre(
	MusicianId int REFERENCES Musicians(MusicianId) not null,
	GenreId int REFERENCES Genres(GenreId) not null,
	constraint pk_join_musician_genre Primary Key (MusicianId, GenreId)
);

create table Join_Musician_Instrument(
	MusicianId int REFERENCES Musicians(MusicianId) not null,
	InstrumentId int REFERENCES Instruments(InstrumentId) not null,
	constraint pk_join_musician_instrument primary key (MusicianId, InstrumentId)
);

create table Join_Band_Genre(
	BandId int REFERENCES Bands(BandId) not null,
	GenreId int REFERENCES Genres(GenreId) not null,
	constraint pk_join_band_genre Primary Key (BandId, GenreId)
);

create table Join_Band_Musician(
	BandId int REFERENCES Bands(BandId) not null,
	MusicianId int REFERENCES Musicians(MusicianId) not null,
	constraint pk_join_band_musician Primary Key (BandId, MusicianId)
);
