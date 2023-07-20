
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Dictionary.Service.Constants;
using Dictionary.Service.DtoEdit;
using Dictionary.Service.Interfaces.Repo;
using Dictionary.Service.Interfaces.Service;
using Dictionary.Service.Model;
using Dictionary.Service.Properties;
using Microsoft.AspNetCore.Mvc;

namespace Dictionary.Api.Controllers;

[Route("api/[controller]s")]
public class ConceptController : BaseController<Concept>
{
    private readonly IConceptRepo _iConceptRepo;
    
    private readonly IConceptService _iConceptService;
    
    public ConceptController(IConceptService conceptService, IConceptRepo conceptRepo, IServiceProvider serviceProvider) : base(
        conceptService, conceptRepo, serviceProvider)
    {
        _iConceptService = conceptService;
        _iConceptRepo = conceptRepo;
    }
    
    [HttpGet("get_number_record")]
    public async Task<IActionResult> GetNumberRecord([FromRoute] string dictionaryId)
    {
        try
        {
            var res = await _iConceptService.GetNumberRecord(dictionaryId);
            if (res != null)
            {
                var actionResult = new ServiceResult((int)ApiStatus.Success, Resources.getDataSuccess, "", res, "200");
                return Ok(actionResult);
            }
            else
            {
                var actionResult = new ServiceResult((int)ApiStatus.Fail, Resources.noReturnData, "", new Object(), "204");
                return Ok(actionResult);
            }
        }
        catch (Exception exception)
        {
            var actionResult = new ServiceResult((int)ApiStatus.Exception, Resources.error, "", exception.Message, "500");
            return Ok(actionResult);
        }
    }
    
    [HttpGet("get_list_concept")]
    public async Task<IActionResult> GetListConcept([FromRoute] string dictionaryId)
    {
        try
        {
            var res = await _iConceptService.GetListConcept(dictionaryId);
            if (res != null)
            {
                var actionResult = new ServiceResult((int)ApiStatus.Success, Resources.getDataSuccess, "", res, "200");
                return Ok(actionResult);
            }
            else
            {
                var actionResult = new ServiceResult((int)ApiStatus.Fail, Resources.noReturnData, "", new List<Concept>(), "204");
                return Ok(actionResult);
            }
        }
        catch (Exception exception)
        {
            var actionResult = new ServiceResult((int)ApiStatus.Exception, Resources.error, "", exception.Message, "500");
            return Ok(actionResult);
        }
    }


    [HttpPost("add_concept")]
    public async Task<IActionResult> AddConcept([FromBody] ConceptDTO conceptDto)
    {
        try
        {
            var res = await _iConceptService.CreateConcept(conceptDto);
            if (res != null)
            {
                var actionResult = new ServiceResult((int)ApiStatus.Success, Resources.addDataSuccess, "", res, "200");
                return Ok(actionResult);
            }
            else
            {
                var actionResult = new ServiceResult((int)ApiStatus.Fail, Resources.addDataFail, "", new Concept(), "204");
                return Ok(actionResult);
            }
        }
        catch (Exception exception)
        {
            var actionResult = new ServiceResult((int)ApiStatus.Exception, Resources.error, "", exception.Message, "500");
            return Ok(actionResult);
        }
    }
    
    
    [HttpPut("update_concept/{conceptId}")]
    public async Task<IActionResult> UpdateConcept([Required] [FromRoute] string conceptId, [FromBody] ConceptDTO conceptDto)
    {
        try
        {
            var res = await _iConceptService.UpdateConcept(conceptId, conceptDto);
            if (res != null)
            {
                var actionResult = new ServiceResult((int)ApiStatus.Success, Resources.editDataSuccess, "", res, "200");
                return Ok(actionResult);
            }
            else
            {
                var actionResult = new ServiceResult((int)ApiStatus.Fail, Resources.editDataFail, "", new Concept(), "204");
                return Ok(actionResult);
            }
        }
        catch (Exception exception)
        {
            var actionResult = new ServiceResult((int)ApiStatus.Exception, Resources.error, "", exception.Message, "500");
            return Ok(actionResult);
        }
    }
    
    
    [HttpDelete("delete_concept")]
    public async Task<IActionResult> DeleteConcept([FromQuery][Required] string conceptId, [FromQuery] bool isForced)
    {
        try
        {
            var res = await _iConceptService.DeleteConcept(conceptId, isForced);
            if (res != null)
            {
                var actionResult = new ServiceResult((int)ApiStatus.Success, Resources.deleteDataSuccess, "", res, "200");
                return Ok(actionResult);
            }
            else
            {
                var actionResult = new ServiceResult((int)ApiStatus.Fail, Resources.deleteDataFail, "", new Concept(), "204");
                return Ok(actionResult);
            }
        }
        catch (Exception exception)
        {
            var actionResult = new ServiceResult((int)ApiStatus.Exception, Resources.error, "", exception.Message, "500");
            return Ok(actionResult);
        }
    }
    
    [HttpGet("get_concept/{conceptId}")]
    public async Task<IActionResult> GetConcept([FromRoute] [Required] string conceptId)
    {
        try
        {
            var res = await _iConceptService.GetConcept(conceptId);
            if (res != null)
            {
                var actionResult = new ServiceResult((int)ApiStatus.Success, Resources.getDataSuccess, "", res, "200");
                return Ok(actionResult);
            }
            else
            {
                var actionResult = new ServiceResult((int)ApiStatus.Fail, Resources.noReturnData, "", new Concept(), "200");
                return Ok(actionResult);
            }
        }
        catch (Exception exception)
        {
            var actionResult = new ServiceResult((int)ApiStatus.Exception, Resources.error, "", exception.Message, "500");
            return Ok(actionResult);
        }
    }
    
    [HttpGet("search_concept")]
    public async Task<IActionResult> SearchConcept([FromQuery][Required] string searchKey, [FromQuery] string dictionaryId)
    {
        try
        {
            var res = await _iConceptService.SearchConcept(searchKey, dictionaryId);
            if (res != null)
            {
                var actionResult = new ServiceResult((int)ApiStatus.Success, Resources.getDataSuccess, "", res, "200");
                return Ok(actionResult);
            }
            else
            {
                var actionResult = new ServiceResult((int)ApiStatus.Fail, Resources.noReturnData, "", new Object(), "204");
                return Ok(actionResult);
            }
        }
        catch (Exception exception)
        {
            var actionResult = new ServiceResult((int)ApiStatus.Exception, Resources.error, "", exception.Message, "500");
            return Ok(actionResult);
        }
    }
}