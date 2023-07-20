using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dictionary.Service.DtoEdit;
using Dictionary.Service.Exceptions;
using Dictionary.Service.Interfaces.Repo;
using Dictionary.Service.Interfaces.Service;
using Dictionary.Service.Model;
using Microsoft.AspNetCore.Http;

namespace Dictionary.Service.Service;

public class ConceptService : BaseService, IConceptService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IConceptRepo _conceptRepo;
    private readonly IConceptRelationshipRepo _conceptRelationshipRepo;

    public ConceptService(IConceptRepo conceptRepo,
        IHttpContextAccessor httpContextAccessor) : base(conceptRepo)
    {
        _conceptRepo = conceptRepo;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<object> GetNumberRecord(string dictionaryId)
    {
        return await _conceptRepo.GetNumberRecord(dictionaryId);
    }

    public async Task<ListConceptResponse> GetListConcept(string dictionaryId)
    {
        var field = "dictionary_id";
        var concept = await _conceptRepo.GetAsync<Concept>(field, dictionaryId);
        DateTime now = DateTime.Now;

        return new ListConceptResponse(concept, now);
    }

    public async Task<Concept> CreateConcept(ConceptDTO conceptDto)
    {
        var newConcept = new Concept();
        newConcept.concept_id = Guid.NewGuid();
        newConcept.dictionary_id = conceptDto.dictionary_id;
        newConcept.description = conceptDto.description;
        newConcept.title = conceptDto.title;
        newConcept.created_date = DateTime.Now;

        return await _conceptRepo.InsertAsync<Concept>(newConcept);
    }

    public async Task<Concept> UpdateConcept(string conceptId, ConceptDTO conceptDto)
    {
        var existedConcept = await _conceptRepo.GetByIdAsync<Concept>(conceptId);

        if (existedConcept == null)
        {
            throw new ValidateException("Concept doesn't exist", null, 400);
        }

        existedConcept.title = conceptDto.title;
        existedConcept.description = conceptDto.description;
        existedConcept.modified_date = DateTime.Now;

        await _conceptRepo.UpdateAsync<Concept>(existedConcept);
        return existedConcept;
    }

    public async Task<Concept> DeleteConcept(string conceptId, bool isForced)
    {
        if (string.IsNullOrEmpty(conceptId))
        {
            throw new ArgumentException("conceptId không hợp lệ.");
        }

        var concept = await _conceptRepo.GetByIdAsync<Concept>(conceptId);

        if (isForced)
        {
            await _conceptRepo.DeleteAsync(concept);
        }
        else
        {
            // Kiểm tra ràng buộc trước khi xóa khái niệm
            // ...

            // Xóa khái niệm
            // ...
        }

        return concept;
    }

    public async Task<ConceptLinkExampleResponse> GetConcept(string conceptId)
    {
        if (string.IsNullOrEmpty(conceptId))
        {
            throw new ArgumentException("conceptId không hợp lệ.");
        }

        var concept = await _conceptRepo.GetByIdAsync<Concept>(conceptId);
        var exampleList = await _conceptRepo.GetListExampleLinkConcept(conceptId);

        return new ConceptLinkExampleResponse(exampleList, concept);
    }

    public async Task<List<Concept>> SearchConcept(string searchKey, string dictionaryId)
    {
        var conceptList = await _conceptRepo.SearchConcept(searchKey, dictionaryId);

        return conceptList;
    }

    public async Task<List<ConceptRelationship>> GetConceptRelationship(string conceptId, string parentId)
    {
        var conceptList = await _conceptRelationshipRepo.GetConceptRelationship(conceptId, parentId);

        return conceptList;
    }
}