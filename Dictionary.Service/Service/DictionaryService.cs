using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dictionary.Service.DtoEdit.Dictionary;
using Dictionary.Service.Exceptions;
using Dictionary.Service.Interfaces.Repo;
using Dictionary.Service.Interfaces.Service;
using Dictionary.Service.Model;

namespace Dictionary.Service.Service
{
    public class DictionaryService : BaseService, IDictionaryService
    {
        private IDictionaryRepo _dictionaryRepo;
        

        public DictionaryService(IDictionaryRepo dictionaryRepo) : base(dictionaryRepo)
        {
            _dictionaryRepo = dictionaryRepo;
        }

        public async Task<DictionaryEntity> CreateDictionary(CreateDictionary createDictionary, Guid userId)
        {
            var newDictionary = new DictionaryEntity();
            
            var existedDictionaryClone = await _dictionaryRepo.GetByIdAsync<DictionaryEntity>(new Guid(createDictionary.clone_dictionary_id));
            var existedDictionary = (await _dictionaryRepo
                .GetAsync<DictionaryEntity>("dictionary_name", createDictionary.dictionary_name))?.FirstOrDefault();
            
            if (existedDictionaryClone!= null)
            {

                newDictionary.user_id = userId;
                newDictionary.dictionary_name = existedDictionaryClone.dictionary_name;
                newDictionary.last_view_at = existedDictionaryClone.last_view_at;
                newDictionary.created_at = DateTime.Now;
                
            }
            else if (existedDictionary == null)
            {
                newDictionary.dictionary_id = Guid.NewGuid();
                newDictionary.user_id = userId;
                newDictionary.dictionary_name = createDictionary.dictionary_name;
                newDictionary.last_view_at = createDictionary.last_view_at;
                newDictionary.created_at = DateTime.Now;
            }
            else
            {
                throw new ValidateException("Dictionary name already in use", "");
            }

            await _dictionaryRepo.InsertAsync<DictionaryEntity>(newDictionary);

            return newDictionary;
        }

        public async Task<DictionaryEntity> UpdateDictionary(UpdateDictionary updateDictionary, Guid user_id)
        {
            var existedDictionary =
                await _dictionaryRepo.GetByIdAsync<DictionaryEntity>(updateDictionary.dictionary_id);

            if (existedDictionary == null)
            {
                throw new ValidateException("Your Dictionary doesn't exist", "");
            }
            
            var existedDictionaryName = (await _dictionaryRepo
                .GetAsync<DictionaryEntity>("dictionary_name", updateDictionary.dictionary_name))?.FirstOrDefault();
            
            if (existedDictionaryName != null)
            {
                throw new ValidateException("Dictionary name already in use", "");
            }

            existedDictionary.user_id = user_id;
            existedDictionary.dictionary_name = updateDictionary.dictionary_name;
            existedDictionary.modified_at = DateTime.Now;

            await _dictionaryRepo.UpdateAsync<DictionaryEntity>(existedDictionary);

            return existedDictionary;
        }

        public async Task<DictionaryEntity> DeleteDictionary(Guid dictionaryId)
        {
            var existedDictionary = await _dictionaryRepo.GetByIdAsync<DictionaryEntity>(dictionaryId);

            if (existedDictionary == null)
            {
                throw new ValidateException("Your Dictionary doesn't exist", "");
            }

            await _dictionaryRepo.DeleteAsync(existedDictionary);

            return existedDictionary;
        }
        
        public async Task<GetListDictionary> LoadDictionary(Guid dictionaryId)
        {
            var existedDictionary = await _dictionaryRepo.GetByIdAsync<DictionaryEntity>(dictionaryId);

            if (existedDictionary == null)
            {
                throw new ValidateException("Your Dictionary doesn't exist", "");
            }

            var getListDictionary = new GetListDictionary();
            getListDictionary.dictionary_id = existedDictionary.dictionary_id;
            getListDictionary.dictionary_name = existedDictionary.dictionary_name;
            getListDictionary.last_view_at = existedDictionary.last_view_at;

            return getListDictionary;
        }

        public async Task<List<GetListDictionary>> GetDictionaries(Guid user_id)
        {
            var dictionaryList = await _dictionaryRepo.GetAsync<DictionaryEntity>("user_id", user_id);
            dictionaryList = dictionaryList.OrderByDescending(x => x.created_at)
                .ToList();

            var getListDictionargeies = new List<GetListDictionary>();
            foreach (var dictionaryEntity in dictionaryList)
            {
                var getListDictionary = new GetListDictionary();
                getListDictionary.dictionary_id = dictionaryEntity.dictionary_id;
                getListDictionary.dictionary_name = dictionaryEntity.dictionary_name;
                getListDictionary.last_view_at = dictionaryEntity.last_view_at;
                getListDictionargeies.Add(getListDictionary);
            }

            return getListDictionargeies;
        }
    }
}