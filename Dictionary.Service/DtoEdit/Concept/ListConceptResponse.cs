using System;
using System.Collections.Generic;
using Dictionary.Service.Model;

namespace Dictionary.Service.DtoEdit;

public class ListConceptResponse
{
    public ListConceptResponse()
    {
        
    }
    
    public ListConceptResponse(List<Concept> _conceptEntities, DateTime _dateTime)
    {
        conceptEntities = _conceptEntities;
        dateTime = _dateTime;
    }
    
    private List<Concept> conceptEntities;

    private DateTime dateTime;
}