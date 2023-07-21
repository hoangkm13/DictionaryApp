
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dictionary.Service.DtoEdit;
using Dictionary.Service.Model;

namespace Dictionary.Service.Interfaces.Service;

public interface IConceptService : IBaseService
{
    Task<object> GetNumberRecord(string dictionaryId);
    
    Task<ListConceptResponse> GetListConcept(string dictionaryId);

    Task<Concept> CreateConcept(ConceptDTO conceptDto);
    
    Task<Concept> UpdateConcept(string conceptId, ConceptDTO conceptDto);

    Task<Concept> DeleteConcept(string conceptId, bool isForced);

    Task<ConceptLinkExampleResponse> GetConcept(string conceptId);
    
    Task<List<Concept>> SearchConcept(string searchKey, string dictionaryId);

    Task<List<Concept>> GetSavedSearch(Guid dictionaryId);

    Task<object> DeleteSavedSearch(string conceptName, Boolean doDeleteAll);
    Task<List<Concept>> GetListRecommendConcept(List<string> listKeyword);
    Task<List<ConceptRelationship>> GetConceptRelationship(string conceptId, string parentId);
}