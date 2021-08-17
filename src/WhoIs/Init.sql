\connect whoisdb

CREATE TABLE Contacts
(
    Id UUID PRIMARY KEY,
    Name VARCHAR (50) NOT NULL,
    Organization VARCHAR (100) NOT NULL,
    Street VARCHAR (50) NOT NULL,
    City VARCHAR (50) NOT NULL,
    State VARCHAR (50) NOT NULL,
    PostalCode VARCHAR (50) NOT NULL,
    Country VARCHAR (50) NOT NULL,
    Phone VARCHAR (50) NOT NULL,
    PhoneExt VARCHAR (50) NOT NULL,
    Fax VARCHAR (50) NOT NULL,
    FaxExt VARCHAR (50) NOT NULL,
    Email VARCHAR (50) NOT NULL
);

CREATE TABLE Domains
(
    Id UUID PRIMARY KEY,
    Name VARCHAR (50) NOT NULL,
    Status VARCHAR (100) NOT NULL,
    NameServer VARCHAR (100) NOT NULL,
    DnsSec VARCHAR (100) NOT NULL,
    CreationDate TIMESTAMP NOT NULL,
    UpdateDate TIMESTAMP NOT NULL,
    Registrant UUID REFERENCES Contacts (Id),
    Admin UUID REFERENCES Contacts (Id),
    Tech UUID REFERENCES Contacts (Id)
);

ALTER TABLE Domains OWNER TO testuser;
ALTER TABLE Contacts OWNER TO testuser;

INSERT INTO Contacts VALUES ('739086aa-3f02-47fa-9292-78c5cc262774', 'Name-1', 'Organization-1', 'Street-1', 'City-1', 'State-1', 'PostalCode-1', 'Country-1', 'Phone-1', 'PhoneExt-1', 'Fax-1', 'FaxExt-1', 'Email-1');
INSERT INTO Contacts VALUES ('f54a66a7-3e46-4bed-8f01-1739d3b7745b', 'Name-2', 'Organization-2', 'Street-2', 'City-2', 'State-2', 'PostalCode-2', 'Country-2', 'Phone-2', 'PhoneExt-2', 'Fax-2', 'FaxExt-2', 'Email-2');
INSERT INTO Contacts VALUES ('98c4d431-466a-4f04-8fd9-d83186ffa00f', 'Name-3', 'Organization-3', 'Street-3', 'City-3', 'State-3', 'PostalCode-3', 'Country-3', 'Phone-3', 'PhoneExt-3', 'Fax-3', 'FaxExt-3', 'Email-3');

INSERT INTO Contacts VALUES ('57e32f21-67fe-423a-8270-e11150071877', 'Name-4', 'Organization-4', 'Street-4', 'City-4', 'State-4', 'PostalCode-4', 'Country-4', 'Phone-4', 'PhoneExt-4', 'Fax-4', 'FaxExt-4', 'Email-4');
INSERT INTO Contacts VALUES ('d6a56afc-d10c-4fb7-b8bb-c2fcba74519c', 'Name-5', 'Organization-5', 'Street-5', 'City-5', 'State-5', 'PostalCode-5', 'Country-5', 'Phone-5', 'PhoneExt-5', 'Fax-5', 'FaxExt-5', 'Email-5');
INSERT INTO Contacts VALUES ('a98df2f4-9215-43a5-b9e5-1ff68b5d77d8', 'Name-6', 'Organization-6', 'Street-6', 'City-6', 'State-6', 'PostalCode-6', 'Country-6', 'Phone-6', 'PhoneExt-6', 'Fax-6', 'FaxExt-6', 'Email-6');

INSERT INTO Domains VALUES ('a55142eb-ff3c-496b-953b-a48b7889df91', 'domain-1', 'Status-1', 'NameServer-1', 'DnsSec-1', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP, '739086aa-3f02-47fa-9292-78c5cc262774', '57e32f21-67fe-423a-8270-e11150071877', 'a98df2f4-9215-43a5-b9e5-1ff68b5d77d8');
INSERT INTO Domains VALUES ('94a6143a-c440-4e5c-a1df-c50fe2f7bda4', 'domain-2', 'Status-2', 'NameServer-2', 'DnsSec-2', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP, 'f54a66a7-3e46-4bed-8f01-1739d3b7745b', 'd6a56afc-d10c-4fb7-b8bb-c2fcba74519c', 'a98df2f4-9215-43a5-b9e5-1ff68b5d77d8');
INSERT INTO Domains VALUES ('e75a37ad-8ce9-49c5-8e9e-6480c8096612', 'domain-3', 'Status-3', 'NameServer-3', 'DnsSec-3', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP, '98c4d431-466a-4f04-8fd9-d83186ffa00f', '57e32f21-67fe-423a-8270-e11150071877', 'a98df2f4-9215-43a5-b9e5-1ff68b5d77d8');
INSERT INTO Domains VALUES ('886ab657-f806-4697-865a-7638db3e48e6', 'domain-4', 'Status-4', 'NameServer-4', 'DnsSec-4', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP, 'f54a66a7-3e46-4bed-8f01-1739d3b7745b', 'f54a66a7-3e46-4bed-8f01-1739d3b7745b', 'f54a66a7-3e46-4bed-8f01-1739d3b7745b');
