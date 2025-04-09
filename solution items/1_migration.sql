CREATE TABLE Owner (
    Id   TEXT (36) PRIMARY KEY
                   UNIQUE
                   NOT NULL,
    Name TEXT      CONSTRAINT UK_Name UNIQUE ON CONFLICT ROLLBACK
                   NOT NULL
);

CREATE TABLE Car (
    Id      TEXT (36) PRIMARY KEY,
    OwnerId TEXT (36) REFERENCES Owner (Id) ON DELETE CASCADE
                                            ON UPDATE CASCADE,
    Name    TEXT (50) NOT NULL
);

CREATE TABLE CarService (
    Id         TEXT (36) PRIMARY KEY,
    CarId      TEXT (36) REFERENCES Car (Id) ON DELETE CASCADE
                                             ON UPDATE CASCADE,
    ServicedBy TEXT      NOT NULL,
    ServicedAt TEXT      NOT NULL,
    WorkDone   TEXT      NOT NULL
);