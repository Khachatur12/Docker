create table Users
(
	id int identity,
	username nvarchar(50) not null
)
go
insert into Users values ('Nabu'), ('Shamil'), ('Adolf')
go 
select * from Users