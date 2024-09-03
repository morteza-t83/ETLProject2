namespace ETLProject2.Models.DataTransferObjects.DataBaseDTOs;

public class SQLDTO(string port, string userName, string password, string dataBaseName, string tableName)
{
    public string Port { get; init; } = port;
    public string UserName { get; init; } = userName;
    public string Password { get; init; } = password;
    public string DataBaseName { get; init; } = dataBaseName;
    public string TableName { get; init; } = tableName;
}