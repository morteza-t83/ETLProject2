using System.Data;
using ETLProject2.Models.DataTransferObjects.DataBaseDTOs;
using Microsoft.VisualBasic.FileIO;

namespace ETLProject2.Models;

public static class DataBase
{
    public static readonly Dictionary<string, DataTable> DataTables = new();
    public static DataTable TransformedDataTable { set; get; } = new();

    private static void AddDataTable(string name, DataTable dataTable)
    {
        DataTables.Add(name, dataTable);
    }

    public static DataTable GetDataTableWithName(string name)
    {
        return string.IsNullOrWhiteSpace(name) ? TransformedDataTable : DataTables[name];
    }

    public static void AddSql(SQLDTO sqlDto, IDbConnection dbConnection, IDbCommand dbCommand)
    {
        var connectionString =
            $"Host=localhost;Port={sqlDto.Port};Database={sqlDto.DataBaseName};User Id={sqlDto.UserName};Password={sqlDto.Password};";

        dbConnection.ConnectionString = connectionString;
        dbConnection.Open();
        dbCommand.CommandText = $"SELECT * FROM {sqlDto.TableName}";
        dbCommand.Connection = dbConnection;
        using var reader = dbCommand.ExecuteReader();
        var table = new DataTable();
        table.Load(reader);
        AddDataTable(sqlDto.TableName, table);
        dbConnection.Dispose();
    }

    public static bool DataTableExist(string name)
    {
        return string.IsNullOrWhiteSpace(name) || DataTables.ContainsKey(name);
    }

    public static void AddCsv(CsvFileDto csvFileDto)
    {
        var table = GetDataTableFromCsvFile(csvFileDto.UrlFile);
        AddDataTable(csvFileDto.TableName, table);
    }

    private static DataTable GetDataTableFromCsvFile(string csvFilePath)
    {
        var csvData = new DataTable();
        try
        {
            using var csvReader = new TextFieldParser(csvFilePath);
            csvReader.SetDelimiters([","]);
            csvReader.HasFieldsEnclosedInQuotes = true;
            var colFields = csvReader.ReadFields();
            foreach (var column in colFields)
            {
                var dataColumn = new DataColumn(column);
                dataColumn.AllowDBNull = true;
                csvData.Columns.Add(dataColumn);
            }

            while (!csvReader.EndOfData)
            {
                var fieldData = csvReader.ReadFields();
                for (var i = 0; i < fieldData.Length; i++)
                {
                    if (fieldData[i] == "")
                    {
                        fieldData[i] = null;
                    }
                }

                csvData.Rows.Add(fieldData);
            }
        }
        catch (Exception)
        {
            return null;
        }

        return csvData;
    }
}