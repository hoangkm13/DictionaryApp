using Dictionary.Service.Interfaces.Repo;
using Microsoft.Extensions.Configuration;

namespace Dictionary.Repo.Repo
{
    public class DictionaryRepo : BaseRepo, IDictionaryRepo
    {
        public DictionaryRepo(IConfiguration configuration) : base(configuration)
        {
        }
    }
}