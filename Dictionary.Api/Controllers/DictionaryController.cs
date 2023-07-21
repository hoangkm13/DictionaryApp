using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dictionary.Service.Constants;
using Dictionary.Service.DtoEdit.Dictionary;
using Dictionary.Service.Exceptions;
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

        [HttpGet("get_list_dictionary")]
        public async Task<IActionResult> GetDictionaries()
        {
            try
            {
                // var contexData = _contextService.Get();

                var res = await _dictionaryService.GetDictionaries(new Guid("b0da65f9-dc39-11ed-a1e6-a44cc8756a37"));
                if (res != null)
                {
                    var actionResult =
                        new ServiceResult((int)ApiStatus.Success, Resources.getDataSuccess, "", res, null);
                    return Ok(actionResult);
                }
                else
                {
                    var actionResult = new ServiceResult((int)ApiStatus.Fail, Resources.noReturnData, "",
                        new List<DictionaryEntity>(), "204");
                    return Ok(actionResult);
                }
            }
            catch (Exception exception)
            {
                var actionResult = new ServiceResult((int)ApiStatus.Exception, Resources.error, exception.Message, null,
                    "500");
                return Ok(actionResult);
            }
        }


        [HttpPost("add_dictionary")]
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
                        new ServiceResult((int)ApiStatus.Success, Resources.getDataSuccess, "", res, null);
                    return Ok(actionResult);
                }
                else
                {
                    var actionResult = new ServiceResult((int)ApiStatus.Fail, Resources.noReturnData, "",
                        new List<DictionaryEntity>(), "204");
                    return Ok(actionResult);
                }
            }
            catch (Exception exception)
            {
                var actionResult = new ServiceResult((int)ApiStatus.Exception, Resources.error, exception.Message, null,
                    "500");
                return Ok(actionResult);
            }
        }

        [HttpPut("update_dictionary")]
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
                        new ServiceResult((int)ApiStatus.Success, Resources.getDataSuccess, "", res, null);
                    return Ok(actionResult);
                }
                else
                {
                    var actionResult = new ServiceResult((int)ApiStatus.Fail, Resources.noReturnData, "",
                        new List<DictionaryEntity>(), "204");
                    return Ok(actionResult);
                }
            }
            catch (Exception exception)
            {
                var actionResult = new ServiceResult((int)ApiStatus.Exception, Resources.error, exception.Message, null,
                    "500");
                return Ok(actionResult);
            }
        }

        [HttpDelete("delete_dictionary/{dictionary_id}")]
        public async Task<IActionResult> DeleteDictionary(Guid dictionary_id)
        {
            try
            {
                var res = await _dictionaryService.DeleteDictionary(dictionary_id);
                if (res != null)
                {
                    var actionResult =
                        new ServiceResult((int)ApiStatus.Success, Resources.getDataSuccess, "", res, null);
                    return Ok(actionResult);
                }
                else
                {
                    var actionResult = new ServiceResult((int)ApiStatus.Fail, Resources.noReturnData, "",
                        new List<DictionaryEntity>(), "204");
                    return Ok(actionResult);
                }
            }
            catch (Exception exception)
            {
                var actionResult = new ServiceResult((int)ApiStatus.Exception, Resources.error, exception.Message, null,
                    "500");
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
                        new ServiceResult((int)ApiStatus.Success, Resources.getDataSuccess, "", res, null);
                    return Ok(actionResult);
                }
                else
                {
                    var actionResult = new ServiceResult((int)ApiStatus.Fail, Resources.noReturnData, "",
                        new List<DictionaryEntity>(), "204");
                    return Ok(actionResult);
                }
            }
            catch (Exception exception)
            {
                var actionResult = new ServiceResult((int)ApiStatus.Exception, Resources.error, exception.Message, null,
                    "500");
                return Ok(actionResult);
            }
        }

        [HttpPost("transfer_dictionary")]
        public async Task<IActionResult> TransferDictionary(TransferDictionary transferDictionary)
        {
            try
            {
                var res = await _dictionaryService.TransferDictionary(transferDictionary);
                if (res != null)
                {
                    var actionResult =
                        new ServiceResult((int)ApiStatus.Success, Resources.getDataSuccess, "", res, null);
                    return Ok(actionResult);
                }
                else
                {
                    var actionResult = new ServiceResult((int)ApiStatus.Fail, Resources.noReturnData, "",
                        new List<DictionaryEntity>(), "204");
                    return Ok(actionResult);
                }
            }

            catch (ValidateException exception)
            {
                var actionResult = new ServiceResult((int)ApiStatus.Exception, exception.Message, null, null,
                    exception.resultCode.ToString());
                return Ok(actionResult);
            }
            catch (Exception exception)
            {
                var actionResult = new ServiceResult((int)ApiStatus.Exception, Resources.error, exception.Message, null,
                    "500");
                return Ok(actionResult);
            }
        }
    }
}