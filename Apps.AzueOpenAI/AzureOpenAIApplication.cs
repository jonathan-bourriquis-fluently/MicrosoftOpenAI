using Blackbird.Applications.Sdk.Common;
using System;

namespace Apps.AzureOpenAI;

public class AzureOpenAIApplication : IApplication
{
    public string Name
    {
        get => "Azure OpenAI";
        set { }
    }

    public T GetInstance<T>()
    {
        throw new NotImplementedException();
    }
}