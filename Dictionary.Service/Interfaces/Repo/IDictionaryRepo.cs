using System;
using System.Threading.Tasks;
using Dictionary.Service.Model;

namespace Dictionary.Service.Interfaces.Repo
{
    public interface IDictionaryRepo : IBaseRepo
    {
        Task<DictionaryEntity> GetDictionaryByUserId(Guid userId);
    }
}