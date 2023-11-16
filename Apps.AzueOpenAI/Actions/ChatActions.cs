using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Apps.AzureOpenAI.Extensions;
using Apps.AzureOpenAI.Models.Requests.Chat;
using Apps.AzureOpenAI.Models.Responses.Chat;
using Azure.AI.OpenAI;
using Azure;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Invocation;
using System.Reflection;

namespace Apps.AzureOpenAI.Actions;

[ActionList]
public class ChatActions : BaseInvocable
{
    private OpenAIClient Client { get; set; }

    private string DeploymentName { get; set; }

    public ChatActions(InvocationContext invocationContext) : base(invocationContext)
    {
        DeploymentName = InvocationContext.AuthenticationCredentialsProviders.First(x => x.KeyName == "deployment").Value;
        Client = new OpenAIClient(
            new Uri(InvocationContext.AuthenticationCredentialsProviders.First(x => x.KeyName == "url").Value), 
            new AzureKeyCredential(InvocationContext.AuthenticationCredentialsProviders.First(x => x.KeyName == "apiKey").Value));
    }

    #region Chat actions

    [Action("Generate completion", Description = "Completes the given prompt")]
    public async Task<CompletionResponse> CreateCompletion([ActionParameter] CompletionRequest input)
    {
        var completion = await Client.GetCompletionsAsync(
            new CompletionsOptions(DeploymentName, 
            new List<string>() { input.Prompt })
        {
            MaxTokens = input.MaximumTokens,
            Temperature = input.Temperature,
            PresencePenalty = input.PresencePenalty,
            FrequencyPenalty = input.FrequencyPenalty
        });
        return new()
        {
            CompletionText = completion.Value.Choices.First().Text
        };
    }

    [Action("Chat", Description = "Gives a response given a chat message")]
    public async Task<ChatResponse> ChatMessageRequest([ActionParameter] ChatRequest input)
    {
        var response = await Client.GetChatCompletionsAsync(
            new ChatCompletionsOptions(DeploymentName,
            new List<ChatMessage>() { new ChatMessage(ChatRole.User, input.Message) })
            {
                MaxTokens = input.MaximumTokens,
                Temperature = input.Temperature,
                PresencePenalty = input.PresencePenalty,
                FrequencyPenalty = input.FrequencyPenalty
            });
        return new()
        {
            Message = response.Value.Choices.First().Message.Content
        };
    }

    [Action("Chat with system prompt",
        Description = "Gives a response given a chat message and a configurable system prompt")]
    public async Task<ChatResponse> ChatWithSystemMessageRequest([ActionParameter] SystemChatRequest input)
    {
        var response = await Client.GetChatCompletionsAsync(
            new ChatCompletionsOptions(DeploymentName,
            new List<ChatMessage>() { new ChatMessage(ChatRole.System, input.SystemPrompt), new ChatMessage(ChatRole.User, input.Message) })
            {
                MaxTokens = input.MaximumTokens,
                Temperature = input.Temperature,
                PresencePenalty = input.PresencePenalty,
                FrequencyPenalty = input.FrequencyPenalty
            });
        return new()
        {
            Message = response.Value.Choices.First().Message.Content
        };
    }

    #endregion
}