drop table Join_Band_Musician
drop table Join_Band_Genre
drop table Join_Musician_Instrument
drop table Join_Musician_Genre
drop table users;
drop table musicians;
drop table bands;
drop table instruments;
drop table genres;


create table Users(
	UserId int IDENTITY(1,1) Primary key,
	FirstName NVARCHAR(100) NOT NULL,
	LastName NVARCHAR(100) NOT NULL,
	BirthDate DATETIME NOT NULL,
	Nickname NVARCHAR(100) NOT NULL,
	Email NVARCHAR(100) NOT NULL,
	Password NVARCHAR(100) NOT NULL,
	Location NVARCHAR(100) NOT NULL,
	Photo varbinary(max),
	Gender NVARCHAR(100),
	Latitude FLOAT(40),
	Longitude FLOAT(40)
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

create table Event(
	EventId int IDENTITY(1,1) Primary key,
	EventName NVARCHAR(100) NOT NULL,
	EventLocation NVARCHAR(100) NOT NULL,
	EventAddress NVARCHAR(100) NOT NULL,
	EventCity NVARCHAR(100) NOT NULL,
	EventDate datetime NOT NULL,
	EventMaxAudience NVARCHAR(100) NOT NULL,
	EventSalary float(10) NOT NULL,
	EventGender NVARCHAR(100) not null,
	EventStageSize int,
	EventPhoto varbinary(max),
	EventCreator NVARCHAR(100)
);


create table Advert(
	AdvertId int IDENTITY(1,1) Primary key,
	Type NVARCHAR(100) NOT NULL,
	Creator int FOREIGN KEY REFERENCES Users(UserId) NOT NULL,
	GenresAdvert int FOREIGN KEY REFERENCES Genres(GenreId) NOT NULL,
	Description NVARCHAR(100) NOT NULL,
	Status nvarchar(100) NOT NULL,
	CreationDate DATETIME NOT NULL,
	ExpirationDate DATETIME NOT NULL,
);

create table Join_Musician_Genre(
	MusicianId int REFERENCES Musicians(MusicianId) not null,
	GenreId int REFERENCES Genres(GenreId) not null,
	constraint pk_join_musician_genre Primary Key (MusicianId, GenreId)
);

create table Join_Musician_Instrument(
	MusicianId int REFERENCES Musicians(MusicianId) not null,
	InstrumentId int REFERENCES Instruments(InstrumentId) not null,
	Skills int not null,
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


insert into Bands(Name, Description, Location)
	values('The wild Mofos', 'We are the Wild Mofos and we fear nothing, even your mom', 'Las Vegas');

insert into Bands(Name, Description, Location)
	values('Loss Sanchez', 'Lost in the desert cooking pancakes!', 'Unknown');
