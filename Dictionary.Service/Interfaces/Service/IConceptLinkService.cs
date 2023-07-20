using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dictionary.Service.Model;

namespace Dictionary.Service.Interfaces.Service;

public interface IConceptLinkService : IBaseService
{
    Task<List<ConceptLink>> GetListConceptLink(Guid user_id);
}