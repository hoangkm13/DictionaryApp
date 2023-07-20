using System;
using Dictionary.Service.Interfaces.Repo;
using Dictionary.Service.Interfaces.Service;
using Dictionary.Service.Model;
using Microsoft.AspNetCore.Mvc;

namespace Dictionary.Api.Controllers;

[Route("api/[controller]s")]
public class ConceptLinkController : BaseController<ConceptLink>
{
    private readonly IConceptLinkRepo _iConceptLinkRepo;
    
    private readonly IConceptLinkService _conceptLinkService;
    
    public ConceptLinkController(IConceptLinkService conceptLinkService, IConceptLinkRepo conceptLinkRepo, IServiceProvider serviceProvider) : base(
         conceptLinkService, conceptLinkRepo, serviceProvider)
    {
        _conceptLinkService = conceptLinkService;
        _iConceptLinkRepo = conceptLinkRepo;
    }

}