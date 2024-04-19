CREATE PROCEDURE AddSchoolWithoutDescription @IdCity int, @SchoolName varchar(200), @StudentCount int
AS
BEGIN
    DECLARE @CityName varchar(200);

    SELECT TOP 1 @CityName = Name 
    From City
    WHERE Id = @IdCity;

    IF @CityName IS NULL
    BEGIN
        RAISERROR('Invalid paramater: Provided ID City does not exist.', 18, 0);
        RETURN;
    END;

    SET XACT_ABORT ON;  
    BEGIN TRAN;

    INSERT INTO School(CityId, Name, StudentCount, Description)
    VALUES(@IdCity, @SchoolName, @StudentCount, null); 

    COMMIT:
END