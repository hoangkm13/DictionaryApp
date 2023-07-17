using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dictionary.Service.DtoEdit.Dictionary;
using Dictionary.Service.Model;

namespace Dictionary.Service.Interfaces.Service
{
    public interface IDictionaryService : IBaseService
    {
        Task<DictionaryEntity> CreateDictionary(CreateDictionary createDictionary, Guid userId);

        Task<DictionaryEntity> UpdateDictionary(UpdateDictionary updateDictionary, Guid userId);

        Task<DictionaryEntity> DeleteDictionary(Guid dictionaryId);
        Task<GetListDictionary> LoadDictionary(Guid dictionaryId);
        
        Task<List<GetListDictionary>> GetDictionaries(Guid user_id);
    }
}