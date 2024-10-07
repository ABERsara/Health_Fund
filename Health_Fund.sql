--DDL -create database
create database Macabi
go
use Macabi
--dr
create table doctors
(id smallint identity(1,1) primary key,
name varchar(20)not null,
city varchar(20),
specialty varchar(20)
)
--users
create table patients
(id varchar(9) primary key,
name varchar(20) ,
birthDate date
)
--Medicines
 create table medicines
(id smallint identity(1,1) primary key,
name varchar(20) unique,
price float,
dosage smallint,
duration smallint,
 )
--appointments
create table appointments
(id smallint identity(1,1) primary key,
--change the rules of deleted- that trigger can delete the references
patient varchar(9) references patients on delete cascade,
doctor smallint references doctors,
medicine smallint references medicines,
appointmentDate date
)
--appointmentsArchiv
create table appointmentsArchiv
(id smallint primary key,
patient varchar(9) references patients,
doctor smallint references doctors,
medicine smallint references medicines,
appointmentDate date
)
go
--------------------------------------------------DML---
insert into doctors(name,specialty) values ('Martina','Kardiolog')
insert into doctors(name,specialty) values ('king','Family')
insert into doctors(name,specialty) values ('Bril','Womens')
insert into doctors(name,specialty) values ('Levi','Dentist')
insert into doctors(name,specialty) values ('noa','Dietan')


insert into patients values('326641685','Miri','2004-06-22')
insert into patients values('333005569','Sari','2002-06-22')
insert into patients values('334857521','Pinches','2010-06-22')
insert into patients values('309631851','Isacc','2020-06-22')


insert into medicines values('Zovirax',25.0,3,7)
insert into medicines values('Macbimol',15.0,4,30)
insert into medicines values('Nurofen',17.0,3,25)

insert into appointments values('326641685',1,1,'2024-01-28')
insert into appointments values('326641685',1,2,'2023-12-28')
insert into appointments values('309631851',2,3,'2024-01-30')

go
select * from doctors
--------------------------------------------------view---
alter  view appointmentsView as
select appointments.id,appointments.patient,patients.name as patientName,appointments.doctor,doctors.name as doctorName,appointments.medicine,medicines.name,medicines.price
from appointments join patients 
	on appointments.patient=patients.id
	join doctors on appointments.doctor=doctors.id
	join medicines on appointments.medicine=medicines.id

select * from appointmentsView
go

-----------------------------------------------procedure--------------
--function that can to update the database
create procedure MoveToArchive as
begin
--copy and delete the old appointments
insert into appointmentsArchiv(id,patient,doctor,medicine,appointmentDate)
select id,patient,doctor,medicine,appointmentDate from appointments
delete appointments
end
--exec the procedure
exec MoveToArchive

----------------------------------------------trigger-----------------
alter trigger deletePatient on patients after delete
as
begin
declare @deletePatient varchar(9)
select @deletePatient=id from deleted
if exists(select*from appointments where  appointmentDate>GETDATE() and patient=@deletePatient)
	begin
		RAISERROR ('The patient cannot be deleted because there are future appointments for him',11,0 )
		rollback
	end
end
--שורת הפעלה לטריגר
delete from patients where id='326641685'

-------------------------------------------scalar function------------------------------------

--A function that receives a patient and returns an amount to be paid for all his medications
go
alter function amountToPay(@patientId varchar(9)) returns float
as
begin
declare @sumPay float
select @sumPay=sum(price) from [appointmentsView] where patient=@patientId
return @sumPay
end
go
declare @id varchar(9)
set @id='326641685'
print(dbo.amountToPay('326641685'))


----------------------------------------------------.Table Function------------------------------------
--A function to display a queue table for the accepted doctor
go
create function queueForDr(@DrId smallint) returns table
return select * from [appointmentsView] where @DrId=doctor
--exec the function
select * from dbo.queueForDr(1)




