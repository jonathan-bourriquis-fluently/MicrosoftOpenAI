﻿using Blackbird.Applications.Sdk.Common;

namespace Apps.AzureOpenAI.Models.Responses.Chat;

public class CompletionResponse
{
    [Display("Completed text")]
    public string CompletionText { get; set; }
}