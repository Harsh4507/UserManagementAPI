CREATE TABLE USERS(
Id int Identity(1,1) Primary Key,
UserName varchar(100) not null,
Name varchar(100) not null,
Age int not null,
Gender varchar(100) not null,
City varchar(100) not null,
)

Insert Into Users
Values ('harsh@gmail.com','Harsh',23,'male','Bijnor'),
('rhino@gmail.com','Rhino',21,'female','Delhi'),
('thakur@gmail.com','Thakur',22,'female','Mumbai'),
('ro@gmail.com','Ro',23,'female','Chandigarh'),
('sita@gmail.com','Sita',24,'female','Chennai'),
('virat@gmail.com','Virat',26,'male','Jaipur'),
('rohit@gmail.com','Rohit',28,'male','Panchkula'),
('kohli@gmail.com','Kohli',21,'male','Noida'),
('sharma@gmail.com','Sharma',24,'male','Gurgaon'),
('jaat@gmail.com','Jaat',28,'male','Banglore')