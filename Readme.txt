CREATE TABLE Users
(
    UserId INT IDENTITY(1,1) PRIMARY KEY,  
    Username NVARCHAR(100) NOT NULL,        
    PasswordHash NVARCHAR(255) NOT NULL,    
    AccessLevel NVARCHAR(50) NOT NULL       
);

