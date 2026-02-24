--CreateProductCategoryManagement
--Create Category, SubCategory & Product Tables

create table Category(
Id int primary key identity(1,1),
Name varchar(250) not null,
Type varchar(250) not null
)

create table SubCategory(
Id int primary key identity(1,1),
Name varchar(250) not null,
CategoryType int not null foreign key references Category(Id)
)

create table Product(
Id int primary key identity(1,1),
Name varchar(250) not null,
SubCategory int not null foreign key references SubCategory(Id)
)