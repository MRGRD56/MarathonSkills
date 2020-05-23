create table Staff (
	StaffId int primary key not null identity,
	FirstName varchar(80) not null,
	LastName varchar(80) not null,
	Email varchar(100) not null,
	DateOfDBirth datetime not null,
	Gender varchar(10) not null,
	PositionId int not null foreign key references Position(PositionId),
	Comments varchar(2000) 
)