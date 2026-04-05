using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools.Responses
{

    /// <summary>
    /// Represents the response payload returned by the Claude Code Agent tool.
    /// This is a polymorphic base type — the actual response will be either
    /// <see cref="CompletedAgentToolResponse"/> or <see cref="AsyncLaunchedAgentToolResponse"/>
    /// depending on whether the agent ran synchronously or was launched in the background.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The discriminator is the <c>"status"</c> field in the JSON payload:
    /// <list type="bullet">
    /// <item><description><c>"completed"</c> — The agent finished and returned its results inline</description></item>
    /// <item><description><c>"async_launched"</c> — The agent was launched in the background</description></item>
    /// </list>
    /// </para>
    /// </remarks>
    [JsonPolymorphic(TypeDiscriminatorPropertyName = "status")]
    [JsonDerivedType(typeof(CompletedAgentToolResponse), "completed")]
    [JsonDerivedType(typeof(AsyncLaunchedAgentToolResponse), "async_launched")]
    public abstract class AgentToolResponse
    {
    }

}
