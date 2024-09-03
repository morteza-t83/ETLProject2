using ETLProject2.Models;
using ETLProject2.Models.DataTransferObjects.DataBaseDTOs;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Npgsql;

namespace ETLProject2.Controllers;

public class DataController(ILogger<DataController> logger) : Controller
{
    private ILogger<DataController> _logger = logger;

    [HttpPost]
    public IActionResult AddSql([FromBody] SQLDTO sqlDto)
    {
        if (DataBase.DataTableExist(sqlDto.TableName))
            return BadRequest("table name exist please change name");
        try
        {
            DataBase.AddSql(sqlDto, new NpgsqlConnection(), new NpgsqlCommand());
            return Ok("data added successfully");
        }
        catch (Exception e)
        {
            return BadRequest(e.ToString());
        }
        
    }

    [HttpPost]
    public IActionResult AddCsv([FromBody] CsvFileDto csvFileDto)
    {
        if (DataBase.DataTableExist(csvFileDto.TableName))
            return BadRequest("table name exist please change name");
        try
        {
            DataBase.AddCsv(csvFileDto);
            return Ok("data added successfully");
        }
        catch (Exception e)
        {
            return BadRequest(e.ToString());
        }
    }

    [HttpGet]
    public IActionResult GetData(string? name)
    {
        if (!DataBase.DataTableExist(name))
        {
            return NotFound("table with this name not exist");
        }

        return Ok(JsonConvert.SerializeObject(DataBase.GetDataTableWithName(name)));
    }
}