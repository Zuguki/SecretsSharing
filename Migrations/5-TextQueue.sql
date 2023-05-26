create table if not exists TextQueue
(
	TextId serial primary key,
	UserId int,
	Title varchar(50),
	Text text
);