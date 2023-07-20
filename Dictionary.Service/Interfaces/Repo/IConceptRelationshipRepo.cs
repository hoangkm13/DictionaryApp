
using System.Collections.Generic;
using System.Threading.Tasks;
using Dictionary.Service.DtoEdit;
using Dictionary.Service.Model;

namespace Dictionary.Service.Interfaces.Repo;

public interface IConceptRelationshipRepo : IBaseRepo
{
    
    Task<List<ConceptRelationship>> GetConceptRelationship(string conceptId, string parentId);
    
}