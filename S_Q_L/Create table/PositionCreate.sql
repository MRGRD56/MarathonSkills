create table Position (
	PositionId int primary key not null identity,
	PositionName varchar(50) not null,
	PositionDescription varchar(1000),
	PayPeriod varchar(10) not null,
	Payrate decimal(10,2) not null
)