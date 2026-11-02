using CloudNimble.ClaudeEssentials.Hooks.Inputs;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools
{

    /// <summary>
    /// Represents the complete payload delivered to a PreToolUse hook when the Artifact tool is about to be invoked.
    /// </summary>
    /// <remarks>
    /// The Artifact tool publishes and manages artifacts hosted on claude.ai. Use this type for strongly-typed deserialization of PreToolUse hook payloads when <c>tool_name</c> is "Artifact".
    /// </remarks>
    /// <example>
    /// <code>
    /// var payload = ClaudeHooksSerializer.DeserializeArtifactPreToolUse(json);
    /// var input = payload?.ToolInput;
    /// </code>
    /// </example>
    public sealed class ArtifactPreToolUsePayload : PreToolUseHookInput<ArtifactToolInput>
    {
    }

}
