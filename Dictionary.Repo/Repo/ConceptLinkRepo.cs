using Dictionary.Service.Interfaces.Repo;
using Microsoft.Extensions.Configuration;

namespace Dictionary.Repo.Repo;

public class ConceptLinkRepo : BaseRepo, IConceptLinkRepo
{
    private IConfiguration _configuration;

    public ConceptLinkRepo(IConfiguration configuration) : base(configuration)
    {
        _configuration = configuration;
    }

}