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
	EventDate Date NOT NULL,
	EventMaxAudience NVARCHAR(100) NOT NULL,
	EventSalary float(10) NOT NULL,
	EventGender NVARCHAR(100) NOT NULL,
	EventStageSize int,
	EventPhoto varbinary(max)
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


insert into Users(FirstName,LastName,BirthDate,Nickname,Email,Password,Location) 
	values('Steven','Seagel',convert(datetime,'10/04/1952'),'Stevy','stevenseagel@hotmail.com','12345','Lévis');

insert into Users(FirstName,LastName,BirthDate,Nickname,Email,Password,Location)
	values('Chuck','Norris',convert(datetime,'10/04/1952'),'Chucky','chucky@hotmail.com','12345','Lévis');

insert into Bands(Name, Description, Location)
	values('The wild Mofos', 'We are the Wild Mofos and we fear nothing, even your mom', 'Las Vegas');

insert into Bands(Name, Description, Location)
	values('Loss Sanchez', 'Lost in the desert cooking pancakes!', 'Unknown');

insert into Musicians(UserId, Description)
  values(1, 'Good');

insert into Musicians(UserId, Description)
  values(2, 'Expérience comme joueur de triangle');

insert into Join_Band_Musician values(1, 1);
insert into Join_Band_Musician values(1, 2);

insert into Genres(Name) values ('Metal');
insert into Genres(Name) values ('Rock');
insert into Genres(Name) values ('Classique');
insert into Genres(Name) values ('Jazz');
insert into Genres(Name) values ('Chuck Norris Style');

insert into Instruments(Name) Values('Guitare');
insert into Instruments(Name) Values('Basse');
insert into Instruments(Name) Values('Piano');
insert into Instruments(Name) Values('Drums');

insert into Join_Band_Genre values(1, 1);
insert into Join_Band_Genre values(2, 2);
insert into Join_Band_Genre values(2, 4);

insert into Join_Musician_Instrument values(1,1);
insert into Join_Musician_Instrument values(1,2);
insert into Join_Musician_Instrument values(2,3);
