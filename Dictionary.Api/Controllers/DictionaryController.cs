using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dictionary.Service.Constants;
using Dictionary.Service.DtoEdit.Dictionary;
using Dictionary.Service.Interfaces.Repo;
using Dictionary.Service.Interfaces.Service;
using Dictionary.Service.Model;
using Dictionary.Service.Properties;
using Microsoft.AspNetCore.Mvc;

namespace Dictionary.Api.Controllers
{
    [Route("api/Dictionaries")]
    public class DictionaryController : BaseController<DictionaryEntity>
    {
        IDictionaryService _dictionaryService;

        IDictionaryRepo _dictionaryRepo;

        public DictionaryController(IDictionaryService dictionaryService, IDictionaryRepo dictionaryRepo,
            IServiceProvider serviceProvider) : base(dictionaryService, dictionaryRepo, serviceProvider)
        {
            _dictionaryService = dictionaryService;
            _dictionaryRepo = dictionaryRepo;
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetDictionaries()
        {
            try
            {
                // var contexData = _contextService.Get();

                var res = await _dictionaryService.GetDictionaries(new Guid("b0da65f9-dc39-11ed-a1e6-a44cc8756a37"));
                if (res != null)
                {
                    var actionResult =
                        new ServiceResult((int)ApiStatus.Success, Resources.getDataSuccess, "", res, 200);
                    return Ok(actionResult);
                }
                else
                {
                    var actionResult = new ServiceResult((int)ApiStatus.Fail, Resources.noReturnData, "",
                        new List<DictionaryEntity>(), 204);
                    return Ok(actionResult);
                }
            }
            catch (Exception exception)
            {
                var actionResult = new ServiceResult((int)ApiStatus.Exception, Resources.error, exception.Message, null,
                    500);
                return Ok(actionResult);
            }
        }


        [HttpPost("create")]
        public async Task<IActionResult> CreateDictionary([FromBody] CreateDictionary createDictionary)
        {
            try
            {
                // var contexData = _contextService.Get();
                // createDictionary.user_id = contexData.UserId;
                var user_id = new Guid("b0da65f9-dc39-11ed-a1e6-a44cc8756a37");
                var res = await _dictionaryService.CreateDictionary(createDictionary, user_id);
                if (res != null)
                {
                    var actionResult =
                        new ServiceResult((int)ApiStatus.Success, Resources.getDataSuccess, "", res, 200);
                    return Ok(actionResult);
                }
                else
                {
                    var actionResult = new ServiceResult((int)ApiStatus.Fail, Resources.noReturnData, "",
                        new List<DictionaryEntity>(), 204);
                    return Ok(actionResult);
                }
            }
            catch (Exception exception)
            {
                var actionResult = new ServiceResult((int)ApiStatus.Exception, Resources.error, exception.Message, null,
                    2001);
                return Ok(actionResult);
            }
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateDictionary([FromBody] UpdateDictionary updateDictionary)
        {
            try
            {
                // var contextService = _contextService.Get();
                // updateDictionary.user_id = contextService.UserId;
                var user_id = new Guid("b0da65f9-dc39-11ed-a1e6-a44cc8756a37");
                var res = await _dictionaryService.UpdateDictionary(updateDictionary, user_id);
                if (res != null)
                {
                    var actionResult =
                        new ServiceResult((int)ApiStatus.Success, Resources.getDataSuccess, "", res, 200);
                    return Ok(actionResult);
                }
                else
                {
                    var actionResult = new ServiceResult((int)ApiStatus.Fail, Resources.noReturnData, "",
                        new List<DictionaryEntity>(), 204);
                    return Ok(actionResult);
                }
            }
            catch (Exception exception)
            {
                var actionResult = new ServiceResult((int)ApiStatus.Exception, Resources.error, exception.Message, null,
                    500);
                return Ok(actionResult);
            }
        }

        [HttpDelete("delete/{dictionary_id}")]
        public async Task<IActionResult> DeleteDictionary(Guid dictionary_id)
        {
            try
            {
                var res = await _dictionaryService.DeleteDictionary(dictionary_id);
                if (res != null)
                {
                    var actionResult =
                        new ServiceResult((int)ApiStatus.Success, Resources.getDataSuccess, "", res, 200);
                    return Ok(actionResult);
                }
                else
                {
                    var actionResult = new ServiceResult((int)ApiStatus.Fail, Resources.noReturnData, "",
                        new List<DictionaryEntity>(), 204);
                    return Ok(actionResult);
                }
            }
            catch (Exception exception)
            {
                var actionResult = new ServiceResult((int)ApiStatus.Exception, Resources.error, exception.Message, null,
                    500);
                return Ok(actionResult);
            }
        }
        
        [HttpDelete("load_dictionary/{dictionary_id}")]
        public async Task<IActionResult> LoadDictionary(Guid dictionary_id)
        {
            try
            {
                var res = await _dictionaryService.LoadDictionary(dictionary_id);
                if (res != null)
                {
                    var actionResult =
                        new ServiceResult((int)ApiStatus.Success, Resources.getDataSuccess, "", res, 200);
                    return Ok(actionResult);
                }
                else
                {
                    var actionResult = new ServiceResult((int)ApiStatus.Fail, Resources.noReturnData, "",
                        new List<DictionaryEntity>(), 2000);
                    return Ok(actionResult);
                }
            }
            catch (Exception exception)
            {
                var actionResult = new ServiceResult((int)ApiStatus.Exception, Resources.error, exception.Message, null,
                    500);
                return Ok(actionResult);
            }
        }
    }
}