/*use SocialMedia
GO
if not exists(select * from sys.server_principals where name = 'SMUser')
BEGIN
    CREATE Login SMUser with password=N'SmPA$$06500', Default_Database=SocialMedia
END

if not exists(select * from sys.database_principals where name = 'SMUser')
BEGIN
   Exec sp_adduser 'SMUser','SMUser','db_owner';
   END
   */