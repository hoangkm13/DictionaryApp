namespace Dictionary.Service.Contexts;

public class WebContextService : IContextService
{
    private ContextData _contextData;

    public void Set(ContextData contextData)
    {
        _contextData = contextData;
    }

    public ContextData Get()
    {
        return _contextData;
    }
}