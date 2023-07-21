
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dictionary.Service.DtoEdit;
using Dictionary.Service.Model;

namespace Dictionary.Service.Interfaces.Repo;

public interface IConceptRepo : IBaseRepo
{
    
    Task<object> GetNumberRecord( string dictionaryId);

    Task<List<Example>> GetListExampleLinkConcept(string conceptId);
    
    Task<List<Concept>> SearchConcept(string searchKey, string dictionaryId);

    Task<List<Concept>> GetSavedSearch(Guid dictionaryId);

    Task<object> DeleteAllConcept();

    Task<Concept> GetConceptRecommend(string keyword);
}