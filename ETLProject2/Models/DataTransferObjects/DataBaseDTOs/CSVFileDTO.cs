namespace ETLProject2.Models.DataTransferObjects.DataBaseDTOs;

public class CsvFileDto(string name, string urlFile)
{
    public string TableName { get; init; } = name;
    public string UrlFile { get; init; } = urlFile;
    public string Password { get; init; }
}