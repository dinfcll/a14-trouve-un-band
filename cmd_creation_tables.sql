drop table Join_Band_Musician
drop table Join_Band_Genre
drop table Join_Musician_Instrument
drop table Join_Musician_Genre
drop table musicians;
drop table users;
drop table bands;
drop table instruments;
drop table sub_genres;
drop table genres;
drop table evenements;

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
	Name NVARCHAR(50) not null,
	Skill int not null
);

create table Genres(
	GenreId int IDENTITY(1,1) Primary Key,
	Name NVARCHAR(50) not null
);

create table Sub_Genres(
	Sub_GenreId int IDENTITY(1,1) Primary Key,
	GenreId int constraint fk_sub_genres_genres REFERENCES Genres(GenreId) not null,
	Name NVARCHAR(50) not null
);
	
create table Evenements(
	EventId int IDENTITY(1,1) Primary key,
	EventName NVARCHAR(100) NOT NULL,
	EventLocation NVARCHAR(100) NOT NULL,
	EventAddress NVARCHAR(100) NOT NULL,
	EventDate NVARCHAR(100) NOT NULL,
	EventMaxAudience NVARCHAR(100) NOT NULL,
	EventSalary NVARCHAR(100) NOT NULL,
	EventGender NVARCHAR(100) NOT NULL,
	EventStageSize int
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

insert into Join_Band_Genre values(1, 1);
insert into Join_Band_Genre values(2, 2);
insert into Join_Band_Genre values(2, 4);

insert into Join_Musician_Instrument values(1,1,2);
insert into Join_Musician_Instrument values(1,2,3);
insert into Join_Musician_Instrument values(2,3,4);

--Insertion des genres appartenant à Blues
insert into Genres values('Blues');
insert into Sub_Genres (GenreId, Name) values ('1', 'Blues rock');
insert into Sub_Genres (GenreId, Name) values ('1', 'Country blues');
insert into Sub_Genres (GenreId, Name) values ('1', 'Jazz blues');
insert into Sub_Genres (GenreId, Name) values ('1', 'Piano blues');
insert into Sub_Genres (GenreId, Name) values ('1', 'Soul blues');
--Insertion des genres appartenant à Easy Listening
insert into Genres values('Easy Listening');
insert into Sub_Genres (GenreId, Name) values ('2', 'Background music');
insert into Sub_Genres (GenreId, Name) values ('2', 'Beautiful music');
insert into Sub_Genres (GenreId, Name) values ('2', 'Lounge music');
insert into Sub_Genres (GenreId, Name) values ('2', 'New-age music');
--Insertion des genres appartenant à Electronic
insert into Genres values('Electronic');
insert into Sub_Genres (GenreId, Name) values ('3', 'Ambient');
insert into Sub_Genres (GenreId, Name) values ('3', 'Asian Underground');
insert into Sub_Genres (GenreId, Name) values ('3', 'Breakbeat');
insert into Sub_Genres (GenreId, Name) values ('3', 'Chiptune');
insert into Sub_Genres (GenreId, Name) values ('3', 'Disco');
insert into Sub_Genres (GenreId, Name) values ('3', 'Downtempo');
insert into Sub_Genres (GenreId, Name) values ('3', 'Drum and bass');
insert into Sub_Genres (GenreId, Name) values ('3', 'Electro');
insert into Sub_Genres (GenreId, Name) values ('3', 'Electroacoustic');
insert into Sub_Genres (GenreId, Name) values ('3', 'Electronica');
insert into Sub_Genres (GenreId, Name) values ('3', 'Electronic rock');
insert into Sub_Genres (GenreId, Name) values ('3', 'Hardcore/Hard dance');
insert into Sub_Genres (GenreId, Name) values ('3', 'House');
insert into Sub_Genres (GenreId, Name) values ('3', 'Industrial');
insert into Sub_Genres (GenreId, Name) values ('3', 'Progressive');
insert into Sub_Genres (GenreId, Name) values ('3', 'Techno');
insert into Sub_Genres (GenreId, Name) values ('3', 'Trance');
--Insertion des genres appartenant à Fol
insert into Genres values('Folk');
insert into Sub_Genres (GenreId, Name) values ('4', 'Contemporary folk');
insert into Sub_Genres (GenreId, Name) values ('4', 'Celtic music');
insert into Sub_Genres (GenreId, Name) values ('4', 'Indie folk');
insert into Sub_Genres (GenreId, Name) values ('4', 'Neofolk');
insert into Sub_Genres (GenreId, Name) values ('4', 'Progressive folk');
insert into Sub_Genres (GenreId, Name) values ('4', 'Industrial folk');
insert into Sub_Genres (GenreId, Name) values ('4', 'Techno-folk');
insert into Sub_Genres (GenreId, Name) values ('4', 'Psychedelic folk');
insert into Sub_Genres (GenreId, Name) values ('4', 'Sung poetry');
insert into Sub_Genres (GenreId, Name) values ('4', 'Cowboy/Western music');
--Insertion des genres appartenant à Hip hop
insert into Genres values('Hip hop');
insert into Sub_Genres (GenreId, Name) values ('5', 'Crunkcore');
insert into Sub_Genres (GenreId, Name) values ('5', 'Freestyle music');
insert into Sub_Genres (GenreId, Name) values ('5', 'Freestyle rap');
insert into Sub_Genres (GenreId, Name) values ('5', 'G-Funk');
insert into Sub_Genres (GenreId, Name) values ('5', 'Gangsta rap');
insert into Sub_Genres (GenreId, Name) values ('5', 'Grime');
insert into Sub_Genres (GenreId, Name) values ('5', 'Hip pop');
insert into Sub_Genres (GenreId, Name) values ('5', 'Industrial hip hop');
insert into Sub_Genres (GenreId, Name) values ('5', 'Instrumental hip hop');
insert into Sub_Genres (GenreId, Name) values ('5', 'Jazz rap');
insert into Sub_Genres (GenreId, Name) values ('5', 'Ragga');
insert into Sub_Genres (GenreId, Name) values ('5', 'Reggaeton');
insert into Sub_Genres (GenreId, Name) values ('5', 'Rap rock');
insert into Sub_Genres (GenreId, Name) values ('5', 'Trap');
insert into Sub_Genres (GenreId, Name) values ('5', 'Trip hop');
insert into Sub_Genres (GenreId, Name) values ('5', 'Turntablism');
insert into Sub_Genres (GenreId, Name) values ('5', 'Underground hip hop');
--Insertion des genres appartenant à Jazz
insert into Genres values('Jazz');
insert into Sub_Genres (GenreId, Name) values ('6', 'Acid jazz');
insert into Sub_Genres (GenreId, Name) values ('6', 'Avant-garde jazz');
insert into Sub_Genres (GenreId, Name) values ('6', 'Free improvisation');
insert into Sub_Genres (GenreId, Name) values ('6', 'Gypsy jazz');
insert into Sub_Genres (GenreId, Name) values ('6', 'Hard bop');
insert into Sub_Genres (GenreId, Name) values ('6', 'Jazz fusion');
insert into Sub_Genres (GenreId, Name) values ('6', 'Latin jazz');
insert into Sub_Genres (GenreId, Name) values ('6', 'Modal jazz');
insert into Sub_Genres (GenreId, Name) values ('6', 'Neo-bop jazz');
insert into Sub_Genres (GenreId, Name) values ('6', 'Neo-swing');
insert into Sub_Genres (GenreId, Name) values ('6', 'Novelty ragtime');
insert into Sub_Genres (GenreId, Name) values ('6', 'Nu jazz');
insert into Sub_Genres (GenreId, Name) values ('6', 'Orchestral jazz');
insert into Sub_Genres (GenreId, Name) values ('6', 'Smooth jazz');
insert into Sub_Genres (GenreId, Name) values ('6', 'Swing');
insert into Sub_Genres (GenreId, Name) values ('6', 'Vocal jazz');
--Insertion des genres appartenant à Rock
insert into Genres values('Rock');
insert into Sub_Genres (GenreId, Name) values ('7', 'Alternative rock');
insert into Sub_Genres (GenreId, Name) values ('7', 'Grunge');
insert into Sub_Genres (GenreId, Name) values ('7', 'Indie rock');
insert into Sub_Genres (GenreId, Name) values ('7', 'Nu metal');
insert into Sub_Genres (GenreId, Name) values ('7', 'Hard rock');
insert into Sub_Genres (GenreId, Name) values ('7', 'Heavy metal');
insert into Sub_Genres (GenreId, Name) values ('7', 'Alternative metal');
insert into Sub_Genres (GenreId, Name) values ('7', 'Black metal');
insert into Sub_Genres (GenreId, Name) values ('7', 'Death metal');
insert into Sub_Genres (GenreId, Name) values ('7', 'Doom metal');
insert into Sub_Genres (GenreId, Name) values ('7', 'Metalcore');
insert into Sub_Genres (GenreId, Name) values ('7', 'Deathcore');
insert into Sub_Genres (GenreId, Name) values ('7', 'Progressive metal');
insert into Sub_Genres (GenreId, Name) values ('7', 'Djent');
insert into Sub_Genres (GenreId, Name) values ('7', 'Rap metal');
insert into Sub_Genres (GenreId, Name) values ('7', 'Speed metal');
insert into Sub_Genres (GenreId, Name) values ('7', 'Stoner rock');
insert into Sub_Genres (GenreId, Name) values ('7', 'Symphonic metal');
insert into Sub_Genres (GenreId, Name) values ('7', 'Thrash metal');
insert into Sub_Genres (GenreId, Name) values ('7', 'Jazz rock');
insert into Sub_Genres (GenreId, Name) values ('7', 'Pop rock');
insert into Sub_Genres (GenreId, Name) values ('7', 'Progressive rock');
insert into Sub_Genres (GenreId, Name) values ('7', 'Psychedelic rock');
insert into Sub_Genres (GenreId, Name) values ('7', 'Deathrock');
insert into Sub_Genres (GenreId, Name) values ('7', 'Grindcore');
insert into Sub_Genres (GenreId, Name) values ('7', 'Emo');
insert into Sub_Genres (GenreId, Name) values ('7', 'Punk');