create table if not exists FileQueue
(
	FileId serial primary key,
	UserId int,
	FileName varchar(50),
	FileContent varchar(100),
	FilePath varchar(100),
	ShouldBeDeleted bool
);
