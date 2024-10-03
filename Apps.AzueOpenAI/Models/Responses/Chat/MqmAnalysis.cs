﻿using Blackbird.Applications.Sdk.Common;
using Newtonsoft.Json;

namespace Apps.AzureOpenAI.Models.Responses.Chat
{
    public class MqmAnalysis
    {
        [Display("Terminology")]
        [JsonProperty("terminology")]        
        public int Terminology { get; set; }

        [Display("Accuracy")]
        [JsonProperty("accuracy")]
        public int Accuracy { get; set; }

        [Display("Linguistic Conventions")]
        [JsonProperty("linguistic_conventions")]
        public int LinguisticConventions { get; set; }

        [Display("Style")]
        [JsonProperty("style")]
        public int Style { get; set; }

        [Display("Locale Conventions")]
        [JsonProperty("locale_conventions")]
        public int LocaleConventions { get; set; }

        [Display("Audience Appropriateness")]
        [JsonProperty("audience_appropriateness")]
        public int AudienceAppropriateness { get; set; }

        [Display("Design And Markup")]
        [JsonProperty("design_and_markup")]
        public int DesignAndMarkup { get; set; }

        [Display("Proposed Translation")]
        [JsonProperty("proposed_translation")]
        public string ProposedTranslation { get; set; }
    }
}
