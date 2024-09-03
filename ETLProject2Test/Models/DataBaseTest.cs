using System.Data;
using ETLProject2.Models;
using ETLProject2.Models.DataTransferObjects.DataBaseDTOs;
using NSubstitute;

namespace ETLProject2Test.Models;

[Collection("Test collection")]
public class DataBaseTest
{
    [Theory]
    [InlineData("")]
    [InlineData("DataTable1")]
    [InlineData("DataTable4")]
    [InlineData("Students")]
    public void DataTableExist_MustReturnTrue_WhenDataTableExistOrNameIsEmpty(string dataTableName)
    {
        //Arrange

        //Act
        var result = DataBase.DataTableExist(dataTableName);
        //Assert
        Assert.True(result);
    }

    [Theory]
    [InlineData("randomName")]
    [InlineData("randomName2")]
    [InlineData("DataTable20")]
    public void DataTableExist_MustReturnFalse_WhenDataTableNotExist(string dataTableName)
    {
        //Arrange

        //Act
        var result = DataBase.DataTableExist(dataTableName);
        //Assert
        Assert.False(result);
    }

    [Theory]
    [InlineData("Students")]
    [InlineData("DataTable3")]
    public void GetDataTableWithName_MustReturnDataTable_WhenNameNotEmpty(string dataTableName)
    {
        //Arrange

        //Act
        var result = DataBase.GetDataTableWithName(dataTableName);
        //Assert
        Assert.NotEqual(DataBase.TransformedDataTable, result);
    }

    [Fact]
    public void GetDataTableWithName_MustReturnDataTable_WhenNameIsEmpty()
    {
        //Arrange

        //Act
        var result = DataBase.GetDataTableWithName("");
        //Assert
        Assert.Equal(DataBase.TransformedDataTable, result);
    }

    [Fact]
    public void AddSql_MustAddTableToDatabase_WhenCall()
    {
        // Arrange
        var dbConnection = Substitute.For<IDbConnection>();
        var dbCommand = Substitute.For<IDbCommand>();
        var sqlDto = new SQLDTO("5432", "postgres", "morteza8354", "postgres", "Student");
        dbConnection.When(x => x.Open())
            .Do(x => dbCommand.ExecuteReader().Returns(new DataTableReader(new DataTable())));
        
        // Act
        DataBase.AddSql(sqlDto, dbConnection, dbCommand);

        // Assert
        Assert.True(DataBase.DataTables.ContainsKey(sqlDto.TableName));
    }

    [Fact]
    public void AddSql_MustAddCsvFileAsTableToDatabase_WhenCall()
    {
        //Arrange
        var csvFileDto = new CsvFileDto("industry", "C:\\Users\\mo.torabi\\Downloads\\industry.csv");
        //Act
        DataBase.AddCsv(csvFileDto);
        //Assert
        Assert.True(DataBase.DataTables.ContainsKey(csvFileDto.TableName));
    }
}