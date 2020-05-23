create table Timesheet (
	TimesheetId int not null identity primary key,
	StaffId int not null foreign key references Staff(StaffId),
	StartDateTime datetime,
	EndDateTime datetime,
	PayAmount decimal(10,2)
)