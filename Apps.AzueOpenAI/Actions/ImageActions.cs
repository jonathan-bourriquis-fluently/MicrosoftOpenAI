using Azure.AI.OpenAI;
using Azure;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Invocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apps.AzureOpenAI.Models.Requests.Image;
using Apps.AzureOpenAI.Models.Responses.Image;

namespace Apps.AzureOpenAI.Actions
{
    [ActionList]
    public class ImageActions : BaseInvocable
    {
        private OpenAIClient Client { get; set; }

        private string DeploymentName { get; set; }

        public ImageActions(InvocationContext invocationContext) : base(invocationContext)
        {
            DeploymentName = InvocationContext.AuthenticationCredentialsProviders.First(x => x.KeyName == "deployment").Value;
            Client = new OpenAIClient(
                new Uri(InvocationContext.AuthenticationCredentialsProviders.First(x => x.KeyName == "url").Value),
                new AzureKeyCredential(InvocationContext.AuthenticationCredentialsProviders.First(x => x.KeyName == "apiKey").Value));
        }

        [Action("Generate image", Description = "Generates an image based on a prompt")]
        public async Task<ImageResponse> GenerateImage([ActionParameter] ImageRequest input)
        {
            var images = await Client.GetImageGenerationsAsync(new ImageGenerationOptions(input.Prompt)
            {
                Size = input.Size,
                ImageCount = 1
            });
            return new()
            {
                Url = images.Value.Data.First().Url.ToString()
            };
        }
    }
}
