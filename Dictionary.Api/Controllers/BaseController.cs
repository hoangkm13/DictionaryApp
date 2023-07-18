using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dictionary.Service.Constants;
using Dictionary.Service.Contexts;
using Dictionary.Service.DtoEdit.Authentication;
using Dictionary.Service.Exceptions;
using Dictionary.Service.Interfaces.Repo;
using Dictionary.Service.Interfaces.Service;
using Dictionary.Service.Model;
using Dictionary.Service.Properties;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Dictionary.Api.Controllers;

/// <summary>
///     Base Controller
/// </summary>
/// <typeparam name="T">Một thực thể</typeparam>
[ApiController]
public class BaseController<T> : ControllerBase where T : class
{
    #region CONSTRUCTOR

    /// <summary>
    ///     Phương thức khởi tạo
    /// </summary>
    /// <param name="baseService"></param>
    public BaseController(IBaseService baseService, IBaseRepo baseRepo, IServiceProvider serviceProvider)
    {
        _baseService = baseService;
        _baseRepo = baseRepo;
        _contextService = serviceProvider.GetRequiredService<IContextService>();
    }

    #endregion

    #region DECLARE

    /// <summary>
    ///     Base service
    /// </summary>
    private readonly IBaseService _baseService;

    /// <summary>
    ///     Base Repo
    /// </summary>
    private readonly IBaseRepo _baseRepo;

    protected readonly IContextService _contextService;

    #endregion

    #region METHODS

    /// <summary>
    ///     Lấy tất cả thực thể T
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetAsync()
    {
        var abc = _contextService.Get();
        try
        {
            var res = await _baseRepo.GetAsync<T>();
            if (res?.Count > 0)
            {
                var actionResult = new ServiceResult((int)ApiStatus.Success, Resources.getDataSuccess, "", res, null);
                return Ok(actionResult);
            }
            else
            {
                var actionResult = new ServiceResult((int)ApiStatus.Fail, Resources.noReturnData, "", new List<T>(), "204");
                return Ok(actionResult);
            }
        }
        catch (Exception exception)
        {
            var actionResult = new ServiceResult((int)ApiStatus.Exception, Resources.error, exception.Message, null, "500");
            return Ok(actionResult);
        }
    }

    /// <summary>
    ///     Lấy thông tin thực thể t
    /// </summary>
    /// <param name="id">id thực thể</param>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(Guid id)
    {
        try
        {
            var res = await _baseRepo.GetByIdAsync<T>(id);
            if (res != null)
            {
                var actionResult = new ServiceResult((int)ApiStatus.Success, Resources.getDataSuccess, "", res, null);
                return Ok(actionResult);
            }
            else
            {
                var actionResult = new ServiceResult((int)ApiStatus.Fail, Resources.noReturnData, "", null, "204");
                return Ok(actionResult);
            }
        }
        catch (Exception exception)
        {
            var actionResult = new ServiceResult((int)ApiStatus.Exception, Resources.error, exception.Message, null, "500");
            return Ok(actionResult);
        }
    }

    /// <summary>
    ///     Insert một thực thể t
    /// </summary>
    /// <param name="t">Thông tin thực thể t</param>
    [HttpPost]
    public async Task<IActionResult> Insert(T t)
    {
        try
        {
            var row = await _baseService.InsertAsync<T>(t);
            if (row != null)
            {
                var actionResult = new ServiceResult((int)ApiStatus.Success, Resources.getDataSuccess, "", row, null);
                return Ok(actionResult);
            }
            else
            {
                var actionResult = new ServiceResult((int)ApiStatus.Fail, Resources.noReturnData, "", null, "204");
                return Ok(actionResult);
            }
        }
        catch (ValidateException exception)
        {
            var actionResult = new ServiceResult((int)ApiStatus.Exception, exception.Message, "", exception.DataErr, "400");
            return Ok(actionResult);
        }
        catch (Exception exception)
        {
            var actionResult = new ServiceResult((int)ApiStatus.Exception, Resources.error, exception.Message, null, "500");
            return Ok(actionResult);
        }
    }

    /// <summary>
    ///     Update một thực thể t
    /// </summary>
    /// <param name="t">Thông tin thực thể t</param>
    [HttpPut]
    public async Task<IActionResult> Update(T t)
    {
        try
        {
            var row = await _baseService.UpdateAsync<T>(t);
            if (row != null)
            {
                var actionResult = new ServiceResult((int)ApiStatus.Success, Resources.editDataSuccess, "", row, null);
                return Ok(actionResult);
            }
            else
            {
                var actionResult = new ServiceResult((int)ApiStatus.Fail, Resources.editDataFail, "", null, "204");
                return Ok(actionResult);
            }
        }
        catch (ValidateException exception)
        {
            var actionResult = new ServiceResult((int)ApiStatus.Exception, exception.Message, "", 0, "400");
            return Ok(actionResult);
        }
        catch (Exception exception)
        {
            var actionResult = new ServiceResult((int)ApiStatus.Exception, Resources.error, exception.Message, null, "500");
            return Ok(actionResult);
        }
    }

    /// <summary>
    ///     Delete một thực thể t
    /// </summary>
    /// <param name="id">id thực thể</param>
    [HttpPost("delete/{id}")]
    public async Task<IActionResult> Delete(T t)
    {
        try
        {
            var result = await _baseService.DeleteAsync(t);
            if (result)
            {
                var actionResult = new ServiceResult((int)ApiStatus.Success, Resources.deleteDataSuccess, "", result, null);
                return Ok(actionResult);
            }
            else
            {
                var actionResult = new ServiceResult((int)ApiStatus.Fail, Resources.deleteDataFail, "", false, "204");
                return Ok(actionResult);
            }
        }
        catch (ValidateException exception)
        {
            var actionResult = new ServiceResult((int)ApiStatus.Exception, exception.Message, "", 0, "400");
            return Ok(actionResult);
        }
        catch (Exception exception)
        {
            var actionResult = new ServiceResult((int)ApiStatus.Exception, Resources.error, exception.Message, null, "500");
            return Ok(actionResult);
        }
    }


    /// <summary>
    ///     Lấy dữ liệu bảng
    /// </summary>
    [HttpPost("dataTable")]
    public async Task<IActionResult> GetDataTable([FromBody] FilterTable param)
    {
        try
        {
            var result = await _baseService.GetDataTable<T>(param);
            return Ok(result);
        }
        catch (Exception exception)
        {
            var actionResult = new ServiceResult((int)ApiStatus.Exception, Resources.error, exception.Message, null, "500");
            return Ok(actionResult);
        }
    }

    /// <summary>
    ///     Lưu dữ liệu
    /// </summary>
    [HttpPost("saveData/{mode}")]
    public virtual async Task<IActionResult> SaveData([FromBody] T model, int mode)
    {
        try
        {
            var result = await _baseService.SaveData(model, mode);
            return Ok(new ServiceResult((int)ApiStatus.Success, Resources.addDataSuccess, "", result, null));
        }
        catch (Exception exception)
        {
            var actionResult = new ServiceResult((int)ApiStatus.Exception, Resources.error, exception.Message, null, "500");
            return Ok(actionResult);
        }
    }

    #endregion
}