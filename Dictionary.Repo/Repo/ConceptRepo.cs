
using System.Collections.Generic;
using System.Threading.Tasks;
using Dictionary.Service.DtoEdit;
using Dictionary.Service.Interfaces.Repo;
using Dictionary.Service.Model;
using Microsoft.Extensions.Configuration;

namespace Dictionary.Repo.Repo;

public class ConceptRepo : BaseRepo, IConceptRepo
{
    private IConfiguration _configuration;

    public ConceptRepo(IConfiguration configuration) : base(configuration)
    {
        _configuration = configuration;
    }

    public async Task<object> GetNumberRecord(string dictionaryId)
    {
        string sql = "SELECT COUNT(e.example_id) AS example_count, " +
                     "COUNT(c.concept_id) AS concept_count " +
                     "FROM dictionary d " +
                     "LEFT JOIN example e ON d.dictionary_id = e.dictionary_id " +
                     "LEFT JOIN concept c ON d.dictionary_id = c.dictionary_id " +
                     "WHERE d.dictionary_id = @dictionaryId GROUP BY d.dictionary_name";
        var result =
            await Provider.QueryAsync<object>(sql, new Dictionary<string, object> { { "dictionaryId", dictionaryId } });
        
        return result;
    }

    public async Task<List<Example>> GetListExampleLinkConcept(string conceptId)
    {
        string sql = "SELECT e.* FROM example e " +
                     "INNER JOIN dictionary d ON e.dictionary_id = d.dictionary_id " +
                     "INNER JOIN concept c ON c.dictionary_id = d.dictionary_id " +
                     "WHERE c.concept_id = @conceptId";
        
        var result = await Provider.QueryAsync<Example>(sql, new Dictionary<string, object>{{"conceptId", conceptId}});

        return result;
    }

    public async Task<List<Concept>> SearchConcept(string searchKey, string dictionaryId)
    {
        string sql = "SELECT * FROM concept WHERE dictionary_id = @dictionaryId AND title LIKE @searchKey";

        var param = new Dictionary<string,object>
        {
            { "dictionaryId", dictionaryId},
            { "searchKey", searchKey},
        };
        
        var result = await Provider.QueryAsync<Concept>(sql, param);

        return result;
    }
    
}