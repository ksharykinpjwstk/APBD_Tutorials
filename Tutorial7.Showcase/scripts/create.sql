CREATE TABLE Country {
    Id int not null identity,
    Name varchar(100) not null,
    CONSTRAINT Country_pk PRIMARY KEY (Id)
};

CREATE TABLE City {
    Id int not null identity,
    CountryId int not null,
    Name varchar(200) not null,
    CONSTRAINT City_pk PRIMARY KEY (Id),
    CONSTRAINT FK_City_Country FOREIGN KEY (CountryId) REFERENCES Country(Id)
};

CREATE TABLE School {
    Id int not null identity,
    CityId int not null,
    Name varchar(200) not null,
    StudentCount int not null,
    Description varchar(2000),
    CONSTRAINT School_pk PRIMARY KEY (Id),
    CONSTRAINT FK_School_City FOREIGN KEY (CityId) REFERENCES City(Id)
};