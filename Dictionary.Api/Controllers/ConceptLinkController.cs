using System;
using System.Threading.Tasks;
using Dictionary.Service.Constants;
using Dictionary.Service.Interfaces.Repo;
using Dictionary.Service.Interfaces.Service;
using Dictionary.Service.Model;
using Dictionary.Service.Properties;
using Microsoft.AspNetCore.Mvc;

namespace Dictionary.Api.Controllers;

[Route("api/[controller]s")]
public class ConceptLinkController : BaseController<ConceptLink>
{
    private readonly IConceptLinkRepo _iConceptLinkRepo;

    private readonly IConceptLinkService _conceptLinkService;

    public ConceptLinkController(IConceptLinkService conceptLinkService, IConceptLinkRepo conceptLinkRepo,
        IServiceProvider serviceProvider) : base(
        conceptLinkService, conceptLinkRepo, serviceProvider)
    {
        _conceptLinkService = conceptLinkService;
        _iConceptLinkRepo = conceptLinkRepo;
    }


    [HttpGet("get_list_concept_link")]
    public async Task<IActionResult> GetConceptLink()
    {
        try
        {
            var contextData = _contextService.Get();
            var res = await _conceptLinkService.GetListConceptLink(contextData.UserId);
            if (res != null)
            {
                var actionResult = new ServiceResult((int)ApiStatus.Success, Resources.getDataSuccess, "", res, "");
                return Ok(actionResult);
            }
            else
            {
                var actionResult = new ServiceResult((int)ApiStatus.Fail, Resources.noReturnData, "", new Object(), "");
                return Ok(actionResult);
            }
        }
        catch (Exception exception)
        {
            var actionResult = new ServiceResult((int)ApiStatus.Exception, Resources.error, "", exception.Message, "");
            return Ok(actionResult);
        }
    }
}