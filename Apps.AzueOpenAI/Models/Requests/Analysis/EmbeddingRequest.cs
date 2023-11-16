﻿using Apps.AzureOpenAI.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.AzureOpenAI.Models.Requests.Analysis;

public class EmbeddingRequest
{
    [Display("Text to embed")]
    public string Text { get; set; }

}