using Azure.AI.OpenAI;
using Azure;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Invocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apps.AzureOpenAI.Models.Requests.Audio;
using Apps.AzureOpenAI.Models.Responses.Audio;
using Blackbird.Applications.Sdk.Common.Actions;

namespace Apps.AzureOpenAI.Actions
{
    public class AudioActions : BaseInvocable
    {
        private OpenAIClient Client { get; set; }

        private string DeploymentName { get; set; }

        public AudioActions(InvocationContext invocationContext) : base(invocationContext)
        {
            DeploymentName = InvocationContext.AuthenticationCredentialsProviders.First(x => x.KeyName == "deployment").Value;
            Client = new OpenAIClient(
                new Uri(InvocationContext.AuthenticationCredentialsProviders.First(x => x.KeyName == "url").Value),
                new AzureKeyCredential(InvocationContext.AuthenticationCredentialsProviders.First(x => x.KeyName == "apiKey").Value));
        }

        [Action("Create English translation", Description = "Generates a translation into English given an audio or " +
                                                       "video file (mp3, mp4, mpeg, mpga, m4a, wav, or webm).")]
        public async Task<TranslationResponse> CreateTranslation([ActionParameter] TranslationRequest input)
        {
            var translation = await Client.GetAudioTranslationAsync(new AudioTranslationOptions(DeploymentName, new BinaryData(input.File.Bytes))
            {
               Prompt = input.Prompt,
               Temperature = input.Temperature,
               ResponseFormat = AudioTranslationFormat.Verbose
            });
            return new()
            {
                TranslatedText = translation.Value.Text
            };
        }

        [Action("Create transcription", Description = "Generates a transcription given an audio or video file. ( mp3, " +
                                                      "mp4, mpeg, mpga, m4a, wav, or webm)")]
        public async Task<TranscriptionResponse> CreateTranscription([ActionParameter] TranscriptionRequest input)
        {
            var transcription = await Client.GetAudioTranscriptionAsync(new AudioTranscriptionOptions(DeploymentName, new BinaryData(input.File.Bytes))
            {
                Language = input.Language,
                Prompt = input.Prompt,
                Temperature = input.Temperature,
                ResponseFormat = AudioTranscriptionFormat.Verbose
            });
            return new()
            {
                Transcription = transcription.Value.Text
            };
        }
    }
}
