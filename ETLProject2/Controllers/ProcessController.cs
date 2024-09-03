using ETLProject2.Models;
using ETLProject2.Models.DataTransferObjects.ProcessDTOs;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ETLProject2.Controllers;

public class ProcessController(ILogger<ProcessController> logger) : Controller
{
    private ILogger<ProcessController> _logger = logger;

    [HttpPut]
    public IActionResult AddCondition([FromBody] ConditionDto conditionDto)
    {
        if (!DataBase.DataTableExist(conditionDto.DataTableName))
            return NotFound("table with this name not exist");

        try
        {
            var result = Calculator.SelectWithCondition(conditionDto);
            return Ok(JsonConvert.SerializeObject(result));
        }
        catch (Exception e)
        {
            return BadRequest(e.ToString());
        }
    }

    [HttpPut]
    public IActionResult AddAggregation([FromBody] AggregationDTO aggregationDto)
    {
        if (!DataBase.DataTableExist(aggregationDto.DataTableName))
            return NotFound("table with this name not exist");

        try
        {
            var result = Calculator.Aggregation(aggregationDto);
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.ToString());
        }
    }
}