using System.Collections.Generic;
using Dictionary.Service.Model;

namespace Dictionary.Service.DtoEdit;

public class ConceptLinkExampleResponse
{
    public ConceptLinkExampleResponse()
    {
        
    }
    public ConceptLinkExampleResponse(List<Example> _examplesList, Concept concept)
    {
        examplesList = _examplesList;
        Concept = concept;
    }
    
    public List<Example> examplesList { get; set; }
    
    public Concept Concept { get; set; }
}