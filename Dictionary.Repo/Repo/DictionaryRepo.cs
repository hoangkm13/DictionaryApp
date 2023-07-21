using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dictionary.Service.Interfaces.Repo;
using Dictionary.Service.Model;
using Microsoft.Extensions.Configuration;

namespace Dictionary.Repo.Repo
{
    public class DictionaryRepo : BaseRepo, IDictionaryRepo
    {
       
        public DictionaryRepo(IConfiguration configuration) : base(configuration)
        {
            
        }

        public async Task<DictionaryEntity> GetDictionaryByUserId(Guid userId)
        {
            var sql = "SELECT * FROM dictionary WHERE user_id = '" + userId + "'";
            var dictionary = await Provider.QueryAsync<DictionaryEntity>(sql, null);

            return dictionary[0];
        }
    }
}