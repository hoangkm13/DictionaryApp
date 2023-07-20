using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dictionary.Service.Interfaces.Repo;
using Dictionary.Service.Interfaces.Service;
using Dictionary.Service.Model;

namespace Dictionary.Service.Service;

public class ConceptLinkService: BaseService, IConceptLinkService
{
    private IConceptLinkRepo _conceptLinkRepo;


    public ConceptLinkService(IConceptLinkRepo conceptLinkRepo) : base(conceptLinkRepo)
    {
        _conceptLinkRepo = conceptLinkRepo;
    }

    public async Task<List<ConceptLink>> GetListConceptLink(Guid user_id)
    {
        var list = await _conceptLinkRepo.GetAsync<ConceptLink>("user_id", user_id);

        return list;
    }
}