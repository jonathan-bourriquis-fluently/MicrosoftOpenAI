﻿using System.Collections.Generic;
using System.Linq;
using Apps.AzureOpenAI.Extensions;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.AzureOpenAI.DataSourceHandlers;

public class PenaltyDataSourceHandler : BaseInvocable, IDataSourceHandler
{
    public PenaltyDataSourceHandler(InvocationContext invocationContext) : base(invocationContext)
    {
    }

    public Dictionary<string, string> GetData(DataSourceContext context)
    {
        return DataSourceHandlersExtensions.GenerateFormattedFloatArray(-2.0f, 2.0f, 0.1f)
            .Where(p => context.SearchString == null || p.Contains(context.SearchString))
            .ToDictionary(p => p, p => p);
    }
}