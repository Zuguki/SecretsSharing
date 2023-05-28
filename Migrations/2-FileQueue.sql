create table if not exists FileQueue
(
	FileId serial primary key,
	UserId int,
	FileName varchar(50),
	FileContent text,
	FilePath varchar(100),
	ShouldBeDeleted bool
);