drop table Join_Band_Musician
drop table Join_Band_Subgenre
--drop table Join_Band_Genre
drop table Join_Musician_Instrument
drop table Join_Musician_SubGenre
--drop table Join_Musician_Genre
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
	Name NVARCHAR(50) not null
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
/*
create table Join_Musician_Genre(
	MusicianId int REFERENCES Musicians(MusicianId) not null,
	GenreId int REFERENCES Genres(GenreId) not null,
	constraint pk_join_musician_genre Primary Key (MusicianId, GenreId)
);
*/

create table Join_Musician_Subgenre(
	MusicianId int REFERENCES Musicians(MusicianId) not null,
	Sub_GenreId int REFERENCES Sub_Genres(Sub_GenreId) not null,
	constraint pk_join_musician_subgenre Primary Key (MusicianId, Sub_GenreId)
);

create table Join_Musician_Instrument(
	MusicianId int REFERENCES Musicians(MusicianId) not null,
	InstrumentId int REFERENCES Instruments(InstrumentId) not null,
	Skills int not null,
	constraint pk_join_musician_instrument primary key (MusicianId, InstrumentId)
);
/*
create table Join_Band_Genre(
	BandId int REFERENCES Bands(BandId) not null,
	GenreId int REFERENCES Genres(GenreId) not null,
	constraint pk_join_band_genre Primary Key (BandId, GenreId)
);
*/

create table Join_Band_Subgenre(
	BandId int REFERENCES Bands(BandId) not null,
	Sub_GenreId int REFERENCES Sub_Genres(Sub_GenreId) not null,
	constraint pk_join_band_subgenre Primary Key (BandId, Sub_GenreId)
);

create table Join_Band_Musician(
	BandId int REFERENCES Bands(BandId) not null,
	MusicianId int REFERENCES Musicians(MusicianId) not null,
	constraint pk_join_band_musician Primary Key (BandId, MusicianId)
);

insert into Instruments(Name) Values('Aucun');
insert into Instruments (Name) values('Abo�s');
insert into Instruments (Name) values('Accord�on');
insert into Instruments (Name) values('Accordina');
insert into Instruments (Name) values('Acordo');
insert into Instruments (Name) values('Adjalin');
insert into Instruments (Name) values('A�rophon');
insert into Instruments (Name) values('Agog� ');
insert into Instruments (Name) values('Alboka');
insert into Instruments (Name) values('Allun');
insert into Instruments (Name) values('Alto');
insert into Instruments (Name) values('Ampongabe');
insert into Instruments (Name) values('Angklung');
insert into Instruments (Name) values('Antsiva ');
insert into Instruments (Name) values('Appeau');
insert into Instruments (Name) values('Archicistre');
insert into Instruments (Name) values('Archiluth');
insert into Instruments (Name) values('Arghoul');
insert into Instruments (Name) values('Atabal ');
insert into Instruments (Name) values('Atabaque ');
insert into Instruments (Name) values('Atranatrana ');
insert into Instruments (Name) values('Autoharpe');
insert into Instruments (Name) values('Azina');
insert into Instruments (Name) values('Baglama');
insert into Instruments (Name) values('Bagpipe ');
insert into Instruments (Name) values('Balafon');
insert into Instruments (Name) values('Balala�ka');
insert into Instruments (Name) values('Bandola');
insert into Instruments (Name) values('Bandon�on');
insert into Instruments (Name) values('Bandourka ');
insert into Instruments (Name) values('Bandura ');
insert into Instruments (Name) values('Bangdi');
insert into Instruments (Name) values('Banjo');
insert into Instruments (Name) values('Banshr� ');
insert into Instruments (Name) values('Bar�taka ');
insert into Instruments (Name) values('Barbitos');
insert into Instruments (Name) values('Baryton');
insert into Instruments (Name) values('Basse quatre cylindres');
insert into Instruments (Name) values('Bassethorn');
insert into Instruments (Name) values('Basson');
insert into Instruments (Name) values('Bat� ');
insert into Instruments (Name) values('B�ton de pluie');
insert into Instruments (Name) values('Batterie');
insert into Instruments (Name) values('Batterie �lectronique');
insert into Instruments (Name) values('Batyphon');
insert into Instruments (Name) values('Bawoo ');
insert into Instruments (Name) values('Bayan ');
insert into Instruments (Name) values('Bazantar ');
insert into Instruments (Name) values('B�chonnet ');
insert into Instruments (Name) values('Belek ');
insert into Instruments (Name) values('Bendir ');
insert into Instruments (Name) values('Bendr� ');
insert into Instruments (Name) values('Benju');
insert into Instruments (Name) values('Berimbau');
insert into Instruments (Name) values('Biniou ');
insert into Instruments (Name) values('Birbyne ');
insert into Instruments (Name) values('Bitu-uvu ');
insert into Instruments (Name) values('Biva');
insert into Instruments (Name) values('Biwa ');
insert into Instruments (Name) values('Bobre');
insert into Instruments (Name) values('Bodega ');
insert into Instruments (Name) values('Bodhr�n ');
insert into Instruments (Name) values('Boha ');
insert into Instruments (Name) values('Bomba ');
insert into Instruments (Name) values('Bombarde');
insert into Instruments (Name) values('Bombardon');
insert into Instruments (Name) values('Bombo ');
insert into Instruments (Name) values('Bongo');
insert into Instruments (Name) values('Bourdon');
insert into Instruments (Name) values('Bouzouki');
insert into Instruments (Name) values('Bratsch violon � 3 cordes roumain');
insert into Instruments (Name) values('Bugle');
insert into Instruments (Name) values('Bugle ');
insert into Instruments (Name) values('Bugle ');
insert into Instruments (Name) values('Buhai ');
insert into Instruments (Name) values('Bulbultara ');
insert into Instruments (Name) values('Cabassa');
insert into Instruments (Name) values('Cabrette ');
insert into Instruments (Name) values('C�i ban nhac');
insert into Instruments (Name) values('C�i chuong chua');
insert into Instruments (Name) values('C�i dan nha tro');
insert into Instruments (Name) values('C�i k�n d�i');
insert into Instruments (Name) values('C�i-nhi');
insert into Instruments (Name) values('Caisse claire');
insert into Instruments (Name) values('Caisse ');
insert into Instruments (Name) values('Caisse roulante');
insert into Instruments (Name) values('Caixa ');
insert into Instruments (Name) values('Caj�n ');
insert into Instruments (Name) values('Calebasse');
insert into Instruments (Name) values('�aradiya-vina');
insert into Instruments (Name) values('Car�m�re');
insert into Instruments (Name) values('Carillon');
insert into Instruments (Name) values('Carillon de fanfare');
insert into Instruments (Name) values('Carnyx');
insert into Instruments (Name) values('Castagnettes');
insert into Instruments (Name) values('�auktika-vina');
insert into Instruments (Name) values('Caval ');
insert into Instruments (Name) values('Cavaquinho');
insert into Instruments (Name) values('Caxixi');
insert into Instruments (Name) values('C�lesta');
insert into Instruments (Name) values('Cellulophone');
insert into Instruments (Name) values('Chabrette ');
insert into Instruments (Name) values('Chalemie');
insert into Instruments (Name) values('Chalumeau');
insert into Instruments (Name) values('Chang-kou');
insert into Instruments (Name) values('Chapeau chinois');
insert into Instruments (Name) values('Chapey');
insert into Instruments (Name) values('Charango ');
insert into Instruments (Name) values('Cheipour');
insert into Instruments (Name) values('Chitarrone');
insert into Instruments (Name) values('Chkacheks');
insert into Instruments (Name) values('Choro');
insert into Instruments (Name) values('Choron ');
insert into Instruments (Name) values('Cialamella');
insert into Instruments (Name) values('Cistre');
insert into Instruments (Name) values('Cistre fran�ais ');
insert into Instruments (Name) values('Cistre japonais ');
insert into Instruments (Name) values('Cithare');
insert into Instruments (Name) values('Cithare anglaise');
insert into Instruments (Name) values('Cithare japonaise');
insert into Instruments (Name) values('Citole chinoise');
insert into Instruments (Name) values('Clairon');
insert into Instruments (Name) values('Clarinette');
insert into Instruments (Name) values('Clarinette basse');
insert into Instruments (Name) values('Clavecin ');
insert into Instruments (Name) values('Claves');
insert into Instruments (Name) values('Clavicorde ');
insert into Instruments (Name) values('Clavicytherium ');
insert into Instruments (Name) values('Clavinet');
insert into Instruments (Name) values('Clavi harp');
insert into Instruments (Name) values('Cloche');
insert into Instruments (Name) values('Cloches de bois');
insert into Instruments (Name) values('Cloches tubulaires');
insert into Instruments (Name) values('Cobza ');
insert into Instruments (Name) values('Colachon ');
insert into Instruments (Name) values('Collier de grelots');
insert into Instruments (Name) values('Concertina');
insert into Instruments (Name) values('Congas ');
insert into Instruments (Name) values('Conque ');
insert into Instruments (Name) values('Contrebasse � cordes');
insert into Instruments (Name) values('Contrebasse � vent');
insert into Instruments (Name) values('Contrebasson');
insert into Instruments (Name) values('Cor � pistons');
insert into Instruments (Name) values('Cor anglais');
insert into Instruments (Name) values('Cor de basset');
insert into Instruments (Name) values('Cor de chasse');
insert into Instruments (Name) values('Cor des Alpes');
insert into Instruments (Name) values('Cor naturel');
insert into Instruments (Name) values('Cornamuse');
insert into Instruments (Name) values('Corne');
insert into Instruments (Name) values('Corne de brume');
insert into Instruments (Name) values('Cornemuse');
insert into Instruments (Name) values('Cornet � bouquin');
insert into Instruments (Name) values('Cornet � pistons');
insert into Instruments (Name) values('Cornu');
insert into Instruments (Name) values('Cosmophone');
insert into Instruments (Name) values('Courtaud');
insert into Instruments (Name) values('Cow antler');
insert into Instruments (Name) values('Cow bells ');
insert into Instruments (Name) values('Cristal Baschet');
insert into Instruments (Name) values('Cromorne ');
insert into Instruments (Name) values('Crouth');
insert into Instruments (Name) values('Cuatro ');
insert into Instruments (Name) values('Cuica ');
insert into Instruments (Name) values('Cumbus');
insert into Instruments (Name) values('Cura ');
insert into Instruments (Name) values('Cymbale antique');
insert into Instruments (Name) values('Cymbales');
insert into Instruments (Name) values('Cymbalum');
insert into Instruments (Name) values('Dabakan ');
insert into Instruments (Name) values('Daf');
insert into Instruments (Name) values('D�m�na');
insert into Instruments (Name) values('Danbolino');
insert into Instruments (Name) values('Danhoun');
insert into Instruments (Name) values('Danyen');
insert into Instruments (Name) values('Dap ');
insert into Instruments (Name) values('Darbouka');
insert into Instruments (Name) values('Darvyra');
insert into Instruments (Name) values('Def ');
insert into Instruments (Name) values('Dehol ');
insert into Instruments (Name) values('Diapason');
insert into Instruments (Name) values('Didgeridoo ');
insert into Instruments (Name) values('Dimplito ');
insert into Instruments (Name) values('D�zi ');
insert into Instruments (Name) values('Djemb�');
insert into Instruments (Name) values('Djoz�');
insert into Instruments (Name) values('Doedelzak');
insert into Instruments (Name) values('Dolcian');
insert into Instruments (Name) values('Domra');
insert into Instruments (Name) values('D�ngxiao ');
insert into Instruments (Name) values('Dou-co');
insert into Instruments (Name) values('Doudou�aine ');
insert into Instruments (Name) values('Doudouk ');
insert into Instruments (Name) values('Doutara');
insert into Instruments (Name) values('Dubreq Stylophone ');
insert into Instruments (Name) values('Duda ');
insert into Instruments (Name) values('Dulcimer');
insert into Instruments (Name) values('Dulcitone');
insert into Instruments (Name) values('Dulzaina');
insert into Instruments (Name) values('Dum dum');
insert into Instruments (Name) values('Egg shaker');
insert into Instruments (Name) values('Eka-tantrika');
insert into Instruments (Name) values('Eka-tara');
insert into Instruments (Name) values('�pinette ');
insert into Instruments (Name) values('�pinette des Vosges');
insert into Instruments (Name) values('Erh-hou-hou');
insert into Instruments (Name) values('Erh-huang-hou');
insert into Instruments (Name) values('Erhu');
insert into Instruments (Name) values('Esrar');
insert into Instruments (Name) values('Estive');
insert into Instruments (Name) values('Euphonium');
insert into Instruments (Name) values('Fa-haotong');
insert into Instruments (Name) values('Fakroum');
insert into Instruments (Name) values('Fango-fango ');
insert into Instruments (Name) values('Fifre');
insert into Instruments (Name) values('Flabiol ');
insert into Instruments (Name) values('Flageolet');
insert into Instruments (Name) values('Fl�ute de Behaigne ');
insert into Instruments (Name) values('Flaviol');
insert into Instruments (Name) values('Flexatone');
insert into Instruments (Name) values('Fluierul mic ');
insert into Instruments (Name) values('Fl�te � bec');
insert into Instruments (Name) values('Fl�te � bec double');
insert into Instruments (Name) values('Fl�te b�arnaise');
insert into Instruments (Name) values('Fl�te de Pan');
insert into Instruments (Name) values('Fl�te de saule');
insert into Instruments (Name) values('Fl�te en sol');
insert into Instruments (Name) values('Fl�te harmonique');
insert into Instruments (Name) values('Fl�te irlandaise');
insert into Instruments (Name) values('Fl�te traversi�re');
insert into Instruments (Name) values('Forte');
insert into Instruments (Name) values('Foul�');
insert into Instruments (Name) values('Foyera');
insert into Instruments (Name) values('Frestel');
insert into Instruments (Name) values('Fujara');
insert into Instruments (Name) values('Gadoulka');
insert into Instruments (Name) values('Ga�da ');
insert into Instruments (Name) values('Gaita ');
insert into Instruments (Name) values('Gaita gallega ');
insert into Instruments (Name) values('Galoubet');
insert into Instruments (Name) values('Gambri');
insert into Instruments (Name) values('Gamelan');
insert into Instruments (Name) values('Ganibri');
insert into Instruments (Name) values('Gankeke');
insert into Instruments (Name) values('Ganza');
insert into Instruments (Name) values('Gardon ');
insert into Instruments (Name) values('Gasb� ');
insert into Instruments (Name) values('Gassaa');
insert into Instruments (Name) values('Geige');
insert into Instruments (Name) values('Gemshorn ');
insert into Instruments (Name) values('Gendang-boeleo');
insert into Instruments (Name) values('Gheteh ');
insert into Instruments (Name) values('Gig');
insert into Instruments (Name) values('Gigue');
insert into Instruments (Name) values('Girine');
insert into Instruments (Name) values('Gita ');
insert into Instruments (Name) values('Glassharmonica');
insert into Instruments (Name) values('Glockenspiel');
insert into Instruments (Name) values('Gnaccara');
insert into Instruments (Name) values('Gong');
insert into Instruments (Name) values('Gongsa');
insert into Instruments (Name) values('Gonkogui');
insert into Instruments (Name) values('Gopi-yantra');
insert into Instruments (Name) values('Gota ');
insert into Instruments (Name) values('Goto');
insert into Instruments (Name) values('Gra�le ');
insert into Instruments (Name) values('Grelots');
insert into Instruments (Name) values('Grosse caisse');
insert into Instruments (Name) values('Guedra');
insert into Instruments (Name) values('Guimbarde');
insert into Instruments (Name) values('Guiro');
insert into Instruments (Name) values('Guitare');
insert into Instruments (Name) values('Guitare acoustique');
insert into Instruments (Name) values('Guitare basse');
insert into Instruments (Name) values('Guitare �lectrique');
insert into Instruments (Name) values('Guitare folk');
insert into Instruments (Name) values('Guitare mauresque');
insert into Instruments (Name) values('Guitarra ');
insert into Instruments (Name) values('Guitarron');
insert into Instruments (Name) values('Guiterne');
insert into Instruments (Name) values('Guqin ');
insert into Instruments (Name) values('Gusli');
insert into Instruments (Name) values('Gut-komm');
insert into Instruments (Name) values('Guzheng ');
insert into Instruments (Name) values('Guzla ');
insert into Instruments (Name) values('Hackbrett');
insert into Instruments (Name) values('Halam s�n�galais');
insert into Instruments (Name) values('Hangdoung');
insert into Instruments (Name) values('Hang');
insert into Instruments (Name) values('Hapetan de Sumatra');
insert into Instruments (Name) values('Hardingfele');
insert into Instruments (Name) values('Harmonica ');
insert into Instruments (Name) values('Harmonica � bouche');
insert into Instruments (Name) values('Harmonica � lames de verre');
insert into Instruments (Name) values('Harmonica � clavier');
insert into Instruments (Name) values('Harmonicor');
insert into Instruments (Name) values('Harmonifl�te');
insert into Instruments (Name) values('Harmonino');
insert into Instruments (Name) values('Harmonium ');
insert into Instruments (Name) values('Harpe ');
insert into Instruments (Name) values('Harpe � p�dales');
insert into Instruments (Name) values('Harpe celtique');
insert into Instruments (Name) values('Harpe chinoise');
insert into Instruments (Name) values('Harpe des Pahouins');
insert into Instruments (Name) values('Harpe �gyptienne');
insert into Instruments (Name) values('Harpu ');
insert into Instruments (Name) values('Hautbois');
insert into Instruments (Name) values('Hautbois � capsule');
insert into Instruments (Name) values('Heang-teih');
insert into Instruments (Name) values('Heckelphone');
insert into Instruments (Name) values('Hegelung');
insert into Instruments (Name) values('H�licon');
insert into Instruments (Name) values('H�liophone');
insert into Instruments (Name) values('Hiachi-riki');
insert into Instruments (Name) values('Hochet');
insert into Instruments (Name) values('Hochet � grelots');
insert into Instruments (Name) values('Horn bugle');
insert into Instruments (Name) values('Hornpipe ');
insert into Instruments (Name) values('Huayllaca');
insert into Instruments (Name) values('H�q�n ');
insert into Instruments (Name) values('Hwang chong tch�');
insert into Instruments (Name) values('Hwang-teih');
insert into Instruments (Name) values('Ichigenkin');
insert into Instruments (Name) values('Inanga');
insert into Instruments (Name) values('Ingomba');
insert into Instruments (Name) values('Insibi');
insert into Instruments (Name) values('Jabisen');
insert into Instruments (Name) values('Jagajhampa');
insert into Instruments (Name) values('Jaleika ');
insert into Instruments (Name) values('Jamblock');
insert into Instruments (Name) values('Jarana');
insert into Instruments (Name) values('Jejy voatavo');
insert into Instruments (Name) values('Jenbe ');
insert into Instruments (Name) values('Jeu de timbre');
insert into Instruments (Name) values('Jouhikko');
insert into Instruments (Name) values('Ka ');
insert into Instruments (Name) values('Kabosse ');
insert into Instruments (Name) values('Kacapi');
insert into Instruments (Name) values('Kacha-v�n�bossy');
insert into Instruments (Name) values('Kachap�-v�n�');
insert into Instruments (Name) values('Kagoura-fouye');
insert into Instruments (Name) values('Ka�r�ta-v�n�');
insert into Instruments (Name) values('Kalimba');
insert into Instruments (Name) values('Kamalen k�ni');
insert into Instruments (Name) values('Kamanja');
insert into Instruments (Name) values('Kandang indien');
insert into Instruments (Name) values('K�nih ');
insert into Instruments (Name) values('Kankangui ');
insert into Instruments (Name) values('Kanoon');
insert into Instruments (Name) values('Kanoun ');
insert into Instruments (Name) values('Kantele ');
insert into Instruments (Name) values('Kao kao javanais');
insert into Instruments (Name) values('Kara ');
insert into Instruments (Name) values('Karabid');
insert into Instruments (Name) values('Karkabet');
insert into Instruments (Name) values('Karna');
insert into Instruments (Name) values('Kass ');
insert into Instruments (Name) values('Kasso ');
insert into Instruments (Name) values('Kattyauma-v�n�');
insert into Instruments (Name) values('Kaval ');
insert into Instruments (Name) values('Kayamb ');
insert into Instruments (Name) values('Kazoo');
insert into Instruments (Name) values('Kema�e ');
insert into Instruments (Name) values('Kem�ngeh � gouz');
insert into Instruments (Name) values('Kem�ngeh-roumy');
insert into Instruments (Name) values('Kena ');
insert into Instruments (Name) values('Kesbate ');
insert into Instruments (Name) values('Kese kese');
insert into Instruments (Name) values('Keseng keseng');
insert into Instruments (Name) values('K�tipoeng ');
insert into Instruments (Name) values('K�tjapi');
insert into Instruments (Name) values('Kharatala');
insert into Instruments (Name) values('Kh�n ');
insert into Instruments (Name) values('Khol');
insert into Instruments (Name) values('Khoradah');
insert into Instruments (Name) values('Khurdra kattyauna-v�n�');
insert into Instruments (Name) values('K�n');
insert into Instruments (Name) values('Kin-kon');
insert into Instruments (Name) values('Kinan ');
insert into Instruments (Name) values('King');
insert into Instruments (Name) values('Kinnari-v�n�');
insert into Instruments (Name) values('Kinnery');
insert into Instruments (Name) values('K�ringie ');
insert into Instruments (Name) values('Kissar ');
insert into Instruments (Name) values('Klaxon');
insert into Instruments (Name) values('Klentony');
insert into Instruments (Name) values('Koant-ts� ');
insert into Instruments (Name) values('Koholo ');
insert into Instruments (Name) values('Kokiou ');
insert into Instruments (Name) values('K�nk�ni');
insert into Instruments (Name) values('Kora');
insert into Instruments (Name) values('Kotek');
insert into Instruments (Name) values('Koto ');
insert into Instruments (Name) values('Kou');
insert into Instruments (Name) values('Koundyeh');
insert into Instruments (Name) values('Kousser');
insert into Instruments (Name) values('Kpanouhoun');
insert into Instruments (Name) values('Kp�zin');
insert into Instruments (Name) values('Kuitra ');
insert into Instruments (Name) values('Kulintang ');
insert into Instruments (Name) values('Kunjerry');
insert into Instruments (Name) values('Langeleik ');
insert into Instruments (Name) values('Lapa');
insert into Instruments (Name) values('Laya bansi');
insert into Instruments (Name) values('Lira da braccio');
insert into Instruments (Name) values('Lithophone');
insert into Instruments (Name) values('Lituus');
insert into Instruments (Name) values('Lo');
insert into Instruments (Name) values('Lo kou ');
insert into Instruments (Name) values('Lokombi ');
insert into Instruments (Name) values('Loure');
insert into Instruments (Name) values('Lu tchun');
insert into Instruments (Name) values('Lung-tao-ty');
insert into Instruments (Name) values('Luth');
insert into Instruments (Name) values('Lyre ');
insert into Instruments (Name) values('Lyro-guitare ');
insert into Instruments (Name) values('Lyrone');
insert into Instruments (Name) values('Ma-ca-doi');
insert into Instruments (Name) values('Madiumba ');
insert into Instruments (Name) values('Magondi');
insert into Instruments (Name) values('Magrouna');
insert into Instruments (Name) values('Maha-mandira');
insert into Instruments (Name) values('Mahati-v�n�');
insert into Instruments (Name) values('Mainty kely');
insert into Instruments (Name) values('Malakat');
insert into Instruments (Name) values('Mandira ');
insert into Instruments (Name) values('Mandoline');
insert into Instruments (Name) values('Mandore');
insert into Instruments (Name) values('Mandurria ');
insert into Instruments (Name) values('Manichordion');
insert into Instruments (Name) values('Maracas ');
insert into Instruments (Name) values('Maravanne ');
insert into Instruments (Name) values('Marddala');
insert into Instruments (Name) values('Marimba');
insert into Instruments (Name) values('Marouvan�');
insert into Instruments (Name) values('Mattauphone');
insert into Instruments (Name) values('Mayuri');
insert into Instruments (Name) values('Mazhar ');
insert into Instruments (Name) values('Medylenara');
insert into Instruments (Name) values('Megyoung');
insert into Instruments (Name) values('Meia-lua');
insert into Instruments (Name) values('Mellophone');
insert into Instruments (Name) values('Mellotron');
insert into Instruments (Name) values('M�lodica');
insert into Instruments (Name) values('Merline');
insert into Instruments (Name) values('M�tronome');
insert into Instruments (Name) values('Mezoued ');
insert into Instruments (Name) values('Mina-sarangi');
insert into Instruments (Name) values('Mirliton');
insert into Instruments (Name) values('Mizmar ');
insert into Instruments (Name) values('Mochanga');
insert into Instruments (Name) values('Mon yu');
insert into Instruments (Name) values('Moraharpa');
insert into Instruments (Name) values('Mridang');
insert into Instruments (Name) values('Mridangam');
insert into Instruments (Name) values('Muselaar ');
insert into Instruments (Name) values('Musette');
insert into Instruments (Name) values('Muscal');
insert into Instruments (Name) values('Nacaire');
insert into Instruments (Name) values('Nadeshvara-v�n�');
insert into Instruments (Name) values('Nafir');
insert into Instruments (Name) values('Nagara ');
insert into Instruments (Name) values('Nagelgeige Allemagne');
insert into Instruments (Name) values('Nagelharmonica');
insert into Instruments (Name) values('Nagharats ');
insert into Instruments (Name) values('Na�');
insert into Instruments (Name) values('Nanga');
insert into Instruments (Name) values('Napura');
insert into Instruments (Name) values('Nay');
insert into Instruments (Name) values('N�o-cor');
insert into Instruments (Name) values('Nhac');
insert into Instruments (Name) values('Nicolo');
insert into Instruments (Name) values('Niho�hagi');
insert into Instruments (Name) values('Nimfali');
insert into Instruments (Name) values('Nira');
insert into Instruments (Name) values('Nixenharfe');
insert into Instruments (Name) values('Nkwanga');
insert into Instruments (Name) values('Noordische balk');
insert into Instruments (Name) values('Ny�staranga');
insert into Instruments (Name) values('Nyckelharpa ');
insert into Instruments (Name) values('Ocarina');
insert into Instruments (Name) values('Ocean drum');
insert into Instruments (Name) values('Octobasse');
insert into Instruments (Name) values('Octoblock');
insert into Instruments (Name) values('Olifant');
insert into Instruments (Name) values('Ombi');
insert into Instruments (Name) values('Omerti');
insert into Instruments (Name) values('Omni');
insert into Instruments (Name) values('Ondes Martenot');
insert into Instruments (Name) values('Ongo ');
insert into Instruments (Name) values('Ophicl�ide');
insert into Instruments (Name) values('Organistrum');
insert into Instruments (Name) values('Orgue');
insert into Instruments (Name) values('Orgue num�rique');
insert into Instruments (Name) values('Orgue de Barbarie');
insert into Instruments (Name) values('Orph�on');
insert into Instruments (Name) values('Orph�oron');
insert into Instruments (Name) values('Orphica');
insert into Instruments (Name) values('Ottavino');
insert into Instruments (Name) values('Oud ');
insert into Instruments (Name) values('Oukazasio');
insert into Instruments (Name) values('Oukpw�');
insert into Instruments (Name) values('Pakay');
insert into Instruments (Name) values('Pakh�waj');
insert into Instruments (Name) values('Pandeiro');
insert into Instruments (Name) values('Pandereta');
insert into Instruments (Name) values('Pandore');
insert into Instruments (Name) values('Pandura');
insert into Instruments (Name) values('Pandurina');
insert into Instruments (Name) values('Pang ts�');
insert into Instruments (Name) values('Patola');
insert into Instruments (Name) values('Pedal steel');
insert into Instruments (Name) values('Pee ');
insert into Instruments (Name) values('Pennywhistle');
insert into Instruments (Name) values('Percuphone');
insert into Instruments (Name) values('Phorminx');
insert into Instruments (Name) values('Pi-li ');
insert into Instruments (Name) values('Piano');
insert into Instruments (Name) values('Piano � queue');
insert into Instruments (Name) values('Piano � tangentes');
insert into Instruments (Name) values('Piano droit');
insert into Instruments (Name) values('Piano-forte');
insert into Instruments (Name) values('Pib-gorn');
insert into Instruments (Name) values('Pibrock');
insert into Instruments (Name) values('Piccolo');
insert into Instruments (Name) values('Pifferta ');
insert into Instruments (Name) values('Pipa');
insert into Instruments (Name) values('Planche � laver');
insert into Instruments (Name) values('Pochette de ma�tre � danser');
insert into Instruments (Name) values('Poun-goun');
insert into Instruments (Name) values('Psalt�rion');
insert into Instruments (Name) values('Pu�li ');
insert into Instruments (Name) values('Pung');
insert into Instruments (Name) values('Pungi ');
insert into Instruments (Name) values('Pyrophone');
insert into Instruments (Name) values('Q-Chord ');
insert into Instruments (Name) values('Qanun');
insert into Instruments (Name) values('Qaraqebs');
insert into Instruments (Name) values('Qarnay ');
insert into Instruments (Name) values('Qilaut ');
insert into Instruments (Name) values('Qin ');
insert into Instruments (Name) values('Quena');
insert into Instruments (Name) values('Quinterna ');
insert into Instruments (Name) values('Quinton');
insert into Instruments (Name) values('Rab�b ');
insert into Instruments (Name) values('Rajiok ');
insert into Instruments (Name) values('Rasarani vina ');
insert into Instruments (Name) values('Rauschpfeife');
insert into Instruments (Name) values('Ravanastron');
insert into Instruments (Name) values('ReacTable');
insert into Instruments (Name) values('R�bana ');
insert into Instruments (Name) values('Rebec ');
insert into Instruments (Name) values('R�gale');
insert into Instruments (Name) values('Rek ');
insert into Instruments (Name) values('Repinique');
insert into Instruments (Name) values('Rhombe ');
insert into Instruments (Name) values('Riqq ');
insert into Instruments (Name) values('Romouze');
insert into Instruments (Name) values('Roudadar ');
insert into Instruments (Name) values('Roul�r');
insert into Instruments (Name) values('Rovana');
insert into Instruments (Name) values('Rudra-vina');
insert into Instruments (Name) values('Sabar');
insert into Instruments (Name) values('Sacqueboute ');
insert into Instruments (Name) values('Sagat ');
insert into Instruments (Name) values('Salamouri ');
insert into Instruments (Name) values('Salpinx ');
insert into Instruments (Name) values('Sambucca');
insert into Instruments (Name) values('San-heen');
insert into Instruments (Name) values('S�n�i');
insert into Instruments (Name) values('Sang�');
insert into Instruments (Name) values('Santir');
insert into Instruments (Name) values('Santour');
insert into Instruments (Name) values('Sanyogi');
insert into Instruments (Name) values('Sanza ');
insert into Instruments (Name) values('Sarala-bensi');
insert into Instruments (Name) values('S�rang� ');
insert into Instruments (Name) values('Sarciros');
insert into Instruments (Name) values('Sarod');
insert into Instruments (Name) values('Sarou ');
insert into Instruments (Name) values('Sarrussophone');
insert into Instruments (Name) values('Sat�r�');
insert into Instruments (Name) values('Sato ');
insert into Instruments (Name) values('Saung ');
insert into Instruments (Name) values('Saw dorang ');
insert into Instruments (Name) values('Saw-duang');
insert into Instruments (Name) values('Saw-tai');
insert into Instruments (Name) values('Saxhorn');
insert into Instruments (Name) values('Saxhorn alto');
insert into Instruments (Name) values('Saxophone');
insert into Instruments (Name) values('Saxophone alto');
insert into Instruments (Name) values('Saxophone baryton');
insert into Instruments (Name) values('Saxophone basse');
insert into Instruments (Name) values('Saxophone contrebasse');
insert into Instruments (Name) values('Saxophone sopranino');
insert into Instruments (Name) values('Saxophone sopranissimo');
insert into Instruments (Name) values('Saxophone soprano');
insert into Instruments (Name) values('Saxophone t�nor');
insert into Instruments (Name) values('Saxotromba');
insert into Instruments (Name) values('Saz ');
insert into Instruments (Name) values('Schalmey ');
insert into Instruments (Name) values('Schiguene ');
insert into Instruments (Name) values('Schiti-gekkin');
insert into Instruments (Name) values('Schlagzither');
insert into Instruments (Name) values('Scho');
insert into Instruments (Name) values('Schofar');
insert into Instruments (Name) values('Schoko');
insert into Instruments (Name) values('Schounga');
insert into Instruments (Name) values('Scie musicale');
insert into Instruments (Name) values('Sciotang');
insert into Instruments (Name) values('Seaou-po ');
insert into Instruments (Name) values('Selantan');
insert into Instruments (Name) values('Selompret');
insert into Instruments (Name) values('Serdam');
insert into Instruments (Name) values('Serinette');
insert into Instruments (Name) values('Serpent');
insert into Instruments (Name) values('Set�r');
insert into Instruments (Name) values('Shakuhachi');
insert into Instruments (Name) values('Shamisen');
insert into Instruments (Name) values('Sharadiya-vina');
insert into Instruments (Name) values('Shekere');
insert into Instruments (Name) values('Shehnai');
insert into Instruments (Name) values('Sheng ');
insert into Instruments (Name) values('Shophar');
insert into Instruments (Name) values('Showktica-vina');
insert into Instruments (Name) values('Shuang-kin');
insert into Instruments (Name) values('Siao');
insert into Instruments (Name) values('Sifflet');
insert into Instruments (Name) values('Sigou-mbarva');
insert into Instruments (Name) values('Sigou-nihou');
insert into Instruments (Name) values('Siku ');
insert into Instruments (Name) values('Silbote');
insert into Instruments (Name) values('Sinbi');
insert into Instruments (Name) values('Siotantka ');
insert into Instruments (Name) values('Sira');
insert into Instruments (Name) values('Sistre ');
insert into Instruments (Name) values('Sitar ');
insert into Instruments (Name) values('Sodina, sody, soly, antsoly');
insert into Instruments (Name) values('Sona');
insert into Instruments (Name) values('Sonaja');
insert into Instruments (Name) values('Sorna');
insert into Instruments (Name) values('Soubassophone');
insert into Instruments (Name) values('Soudzou');
insert into Instruments (Name) values('Souff�rrah');
insert into Instruments (Name) values('Soug');
insert into Instruments (Name) values('Souma-koto ');
insert into Instruments (Name) values('Soung-king');
insert into Instruments (Name) values('Sounna�a ');
insert into Instruments (Name) values('Souqqarah');
insert into Instruments (Name) values('Sourdine');
insert into Instruments (Name) values('Sousounou');
insert into Instruments (Name) values('Spitzharfe');
insert into Instruments (Name) values('Sringa');
insert into Instruments (Name) values('Sruti-vina');
insert into Instruments (Name) values('Stamentien-pfeiffe');
insert into Instruments (Name) values('Steel-drum');
insert into Instruments (Name) values('Stick');
insert into Instruments (Name) values('Stock-horn');
insert into Instruments (Name) values('Stopf-trumpet');
insert into Instruments (Name) values('Streich-zither ');
insert into Instruments (Name) values('Stretch machine');
insert into Instruments (Name) values('Suka');
insert into Instruments (Name) values('Suling');
insert into Instruments (Name) values('Suona');
insert into Instruments (Name) values('Sur-bahara ');
insert into Instruments (Name) values('Sur-v�h�ra ');
insert into Instruments (Name) values('Sur-vina ');
insert into Instruments (Name) values('Surdo');
insert into Instruments (Name) values('Swedish bagpipe ');
insert into Instruments (Name) values('Synth�tiseur');
insert into Instruments (Name) values('Syntophone');
insert into Instruments (Name) values('Sze-hou-hsien');
insert into Instruments (Name) values('Ta huang hou kin ');
insert into Instruments (Name) values('Taakan');
insert into Instruments (Name) values('Taarija');
insert into Instruments (Name) values('Tabl� ');
insert into Instruments (Name) values('Tablat');
insert into Instruments (Name) values('Taiko ');
insert into Instruments (Name) values('Ta�sene');
insert into Instruments (Name) values('Taki-koto');
insert into Instruments (Name) values('Talain');
insert into Instruments (Name) values('Tam-tam');
insert into Instruments (Name) values('Tama ');
insert into Instruments (Name) values('Tamborim ');
insert into Instruments (Name) values('Tambour ');
insert into Instruments (Name) values('Tambour boulghary ');
insert into Instruments (Name) values('Tambourin ');
insert into Instruments (Name) values('Tamb�r');
insert into Instruments (Name) values('Tan-tan');
insert into Instruments (Name) values('Tao-kou');
insert into Instruments (Name) values('Tarogato');
insert into Instruments (Name) values('Tapan ');
insert into Instruments (Name) values('T�r ');
insert into Instruments (Name) values('Tarambouka');
insert into Instruments (Name) values('Tarole');
insert into Instruments (Name) values('Tarqa ');
insert into Instruments (Name) values('Tasa');
insert into Instruments (Name) values('Tashepoto ');
insert into Instruments (Name) values('Tatchoota');
insert into Instruments (Name) values('Tawaya ');
insert into Instruments (Name) values('Tbal ');
insert into Instruments (Name) values('Tchang-kou ');
insert into Instruments (Name) values('Tch� ');
insert into Instruments (Name) values('Tchengue ');
insert into Instruments (Name) values('Tchogor');
insert into Instruments (Name) values('Tchong');
insert into Instruments (Name) values('Tchong-tou');
insert into Instruments (Name) values('Tchou');
insert into Instruments (Name) values('T�-tchong ');
insert into Instruments (Name) values('Tebashoul ');
insert into Instruments (Name) values('Tebilats');
insert into Instruments (Name) values('Tebloun');
insert into Instruments (Name) values('Telharmonium ');
insert into Instruments (Name) values('Tembour ');
insert into Instruments (Name) values('Tenora ');
insert into Instruments (Name) values('Terab-enguiz');
insert into Instruments (Name) values('Terpodion ');
insert into Instruments (Name) values('Terr');
insert into Instruments (Name) values('Tet-jer ');
insert into Instruments (Name) values('Thar');
insert into Instruments (Name) values('Thari ');
insert into Instruments (Name) values('Th�orbe');
insert into Instruments (Name) values('Th�r�mine ');
insert into Instruments (Name) values('Thumgo-tuapan');
insert into Instruments (Name) values('Thurnerhorn');
insert into Instruments (Name) values('Ti-kin ');
insert into Instruments (Name) values('Tien-kou ');
insert into Instruments (Name) values('Tilinca ');
insert into Instruments (Name) values('Timba');
insert into Instruments (Name) values('Timbale');
insert into Instruments (Name) values('Timbales, et timbalitos et timbalon');
insert into Instruments (Name) values('Timple');
insert into Instruments (Name) values('Tiple');
insert into Instruments (Name) values('Tj�-tj� ');
insert into Instruments (Name) values('Tohona');
insert into Instruments (Name) values('Tombah ');
insert into Instruments (Name) values('Tombak ');
insert into Instruments (Name) values('To rung ');
insert into Instruments (Name) values('Toumourah');
insert into Instruments (Name) values('Tournebout');
insert into Instruments (Name) values('Tourti');
insert into Instruments (Name) values('Toutsoumi ');
insert into Instruments (Name) values('Trawanga');
insert into Instruments (Name) values('Tres');
insert into Instruments (Name) values('Triangle');
insert into Instruments (Name) values('Tritantri-vina ');
insert into Instruments (Name) values('Tritare');
insert into Instruments (Name) values('Troccola ');
insert into Instruments (Name) values('Trombone � coulisse');
insert into Instruments (Name) values('Trombone � pistons');
insert into Instruments (Name) values('Trompe de chasse');
insert into Instruments (Name) values('Trompe suisse');
insert into Instruments (Name) values('Trompette de mail coach');
insert into Instruments (Name) values('Trompette marine');
insert into Instruments (Name) values('Trompette m�di�vale');
insert into Instruments (Name) values('Tseng');
insert into Instruments (Name) values('Tsikadraha');
insert into Instruments (Name) values('Tsou-kou');
insert into Instruments (Name) values('Tsou toung hou-kin');
insert into Instruments (Name) values('Tsouma-koto');
insert into Instruments (Name) values('Tsouzoumi');
insert into Instruments (Name) values('Tuba');
insert into Instruments (Name) values('Tuba corva');
insert into Instruments (Name) values('Tubilattes');
insert into Instruments (Name) values('Tubri');
insert into Instruments (Name) values('Tubular bells');
insert into Instruments (Name) values('Tumburu-vina');
insert into Instruments (Name) values('Turi');
insert into Instruments (Name) values('Turr');
insert into Instruments (Name) values('Txalaparta ');
insert into Instruments (Name) values('Txanbela');
insert into Instruments (Name) values('Txirula ');
insert into Instruments (Name) values('Txistu ');
insert into Instruments (Name) values('Ty');
insert into Instruments (Name) values('Tympanon');
insert into Instruments (Name) values('Uilacapitztli');
insert into Instruments (Name) values('Ukeke-laau ');
insert into Instruments (Name) values('Ukul�l� ');
insert into Instruments (Name) values('Uliuli ');
insert into Instruments (Name) values('Utricularium');
insert into Instruments (Name) values('Udu');
insert into Instruments (Name) values('Valiha toritenany ');
insert into Instruments (Name) values('Van�ali ');
insert into Instruments (Name) values('Venu ');
insert into Instruments (Name) values('Veuze ');
insert into Instruments (Name) values('Vibraphone');
insert into Instruments (Name) values('Vibraslap, fouet vobrant');
insert into Instruments (Name) values('Vi�le');
insert into Instruments (Name) values('Vielle � roue');
insert into Instruments (Name) values('Vihuela');
insert into Instruments (Name) values('Villancoyel');
insert into Instruments (Name) values('V�n� ');
insert into Instruments (Name) values('Vioar� cu goarn�');
insert into Instruments (Name) values('Viole');
insert into Instruments (Name) values('Viole de gambe');
insert into Instruments (Name) values('Violon');
insert into Instruments (Name) values('Violon � pavillon ');
insert into Instruments (Name) values('Violon alto');
insert into Instruments (Name) values('Violon savart');
insert into Instruments (Name) values('Violoncelle');
insert into Instruments (Name) values('Violone');
insert into Instruments (Name) values('Vipancia vina');
insert into Instruments (Name) values('Virginal ');
insert into Instruments (Name) values('Voix');
insert into Instruments (Name) values('Wakrapuku ');
insert into Instruments (Name) values('Wamb�e');
insert into Instruments (Name) values('Wangong');
insert into Instruments (Name) values('Waterphone');
insert into Instruments (Name) values('Whit-horn ');
insert into Instruments (Name) values('Wistaka ');
insert into Instruments (Name) values('Wood-block');
insert into Instruments (Name) values('Xaphoon ');
insert into Instruments (Name) values('Xeremia');
insert into Instruments (Name) values('Xiao');
insert into Instruments (Name) values('Xun ');
insert into Instruments (Name) values('Xylophone');
insert into Instruments (Name) values('Yabara');
insert into Instruments (Name) values('Yakoumakoto ');
insert into Instruments (Name) values('Yang');
insert into Instruments (Name) values('Yang-kin');
insert into Instruments (Name) values('Yo');
insert into Instruments (Name) values('Yotsu-dake ');
insert into Instruments (Name) values('You-kin');
insert into Instruments (Name) values('Youkoul�l�, voir ukul�l�');
insert into Instruments (Name) values('Yun-lo ');
insert into Instruments (Name) values('Zampogna simplice ');
insert into Instruments (Name) values('Zampo�a');
insert into Instruments (Name) values('Zamr-el-k�byr ');
insert into Instruments (Name) values('Zamr-el-soghair ');
insert into Instruments (Name) values('Zanza ');
insert into Instruments (Name) values('Zarb ');
insert into Instruments (Name) values('Zithera ');
insert into Instruments (Name) values('Zokra');
insert into Instruments (Name) values('Zongora ');
insert into Instruments (Name) values('Zourna ');
insert into Instruments (Name) values('Zumm�rah arbaou�a ');
insert into Instruments (Name) values('Zumm�rah khamsaou�a ');
insert into Instruments (Name) values('Zumm�rah settaou�a ');
insert into Instruments (Name) values('Zumm�rah sabaou�a ');


insert into Users(FirstName,LastName,BirthDate,Nickname,Email,Password,Location) 
	values('Steven','Seagel',convert(datetime,'10/04/1952'),'Stevy','stevenseagel@hotmail.com','12345','L�vis');

insert into Users(FirstName,LastName,BirthDate,Nickname,Email,Password,Location)
	values('Chuck','Norris',convert(datetime,'10/04/1952'),'Chucky','chucky@hotmail.com','12345','L�vis');

insert into Bands(Name, Description, Location)
	values('The wild Mofos', 'We are the Wild Mofos and we fear nothing, even your mom', 'Las Vegas');

insert into Bands(Name, Description, Location)
	values('Loss Sanchez', 'Lost in the desert cooking pancakes!', 'Unknown');

insert into Musicians(UserId, Description)
  values(1, 'Good');

insert into Musicians(UserId, Description)
  values(2, 'Exp�rience comme joueur de triangle');

insert into Join_Band_Musician values(1, 1);
insert into Join_Band_Musician values(1, 2);

insert into Join_Band_Subgenre values(1, 1);
insert into Join_Band_Subgenre values(2, 2);
insert into Join_Band_Subgenre values(2, 100);

insert into Join_Musician_Instrument values(1,1,2);
insert into Join_Musician_Instrument values(1,2,3);
insert into Join_Musician_Instrument values(2,3,4);

--Insertion des genres appartenant � Blues
insert into Genres values('Blues');
insert into Sub_Genres (GenreId, Name) values ('1', 'Blues rock');
insert into Sub_Genres (GenreId, Name) values ('1', 'Country blues');
insert into Sub_Genres (GenreId, Name) values ('1', 'Jazz blues');
insert into Sub_Genres (GenreId, Name) values ('1', 'Piano blues');
insert into Sub_Genres (GenreId, Name) values ('1', 'Soul blues');
--Insertion des genres appartenant � Easy Listening
insert into Genres values('Easy Listening');
insert into Sub_Genres (GenreId, Name) values ('2', 'Background music');
insert into Sub_Genres (GenreId, Name) values ('2', 'Beautiful music');
insert into Sub_Genres (GenreId, Name) values ('2', 'Lounge music');
insert into Sub_Genres (GenreId, Name) values ('2', 'New-age music');
--Insertion des genres appartenant � Electronic
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
--Insertion des genres appartenant � Fol
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
--Insertion des genres appartenant � Hip hop
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
--Insertion des genres appartenant � Jazz
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
--Insertion des genres appartenant � Rock
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