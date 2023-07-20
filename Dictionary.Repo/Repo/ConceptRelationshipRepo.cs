
using System.Collections.Generic;
using System.Threading.Tasks;
using Dictionary.Service.DtoEdit;
using Dictionary.Service.Interfaces.Repo;
using Dictionary.Service.Model;
using Microsoft.Extensions.Configuration;

namespace Dictionary.Repo.Repo;

public class ConceptRelationshipRepo : BaseRepo, IConceptRelationshipRepo
{
    private IConfiguration _configuration;

    public ConceptRelationshipRepo(IConfiguration configuration) : base(configuration)
    {
        _configuration = configuration;
    }

   
    
    public async Task<List<ConceptRelationship>> GetConceptRelationship(string conceptId, string parentId)
    {
        string sql = "SELECT * FROM concept_relationship WHERE concept_id = @conceptId AND parent_id = @parentId";

        var param = new Dictionary<string,object>
        {
            { "conceptId", conceptId},
            { "parent_id", parentId},
        };
        
        var result = await Provider.QueryAsync<ConceptRelationship>(sql, param);

        return result;
    }
    
}
