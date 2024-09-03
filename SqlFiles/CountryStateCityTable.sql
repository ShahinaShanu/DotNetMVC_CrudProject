Create Table Country(
Row_Id int identity(1,1) Primary Key,
CountryName Nvarchar(100) not null
)

CREATE TABLE State (
    Row_Id INT IDENTITY(1,1) PRIMARY KEY,
    CountryId INT NOT NULL,
    StateName NVARCHAR(100) NOT NULL,
    FOREIGN KEY (CountryId) REFERENCES Country(Row_Id)
)


CREATE TABLE City (
    Row_Id INT IDENTITY(1,1) PRIMARY KEY,
    StateId INT NOT NULL,
    CityName NVARCHAR(100) NOT NULL,
    FOREIGN KEY (StateId) REFERENCES State(Row_Id)
);

--drop table Country
select * from Country
