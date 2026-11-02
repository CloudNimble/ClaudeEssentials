using CloudNimble.ClaudeEssentials.Hooks.Inputs;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools
{

    /// <summary>
    /// Represents the complete payload delivered to a PreToolUse hook when the Projects tool is about to be invoked.
    /// </summary>
    /// <remarks>
    /// The Projects tool reads and writes documents in a claude.ai project. Use this type for strongly-typed deserialization of PreToolUse hook payloads when <c>tool_name</c> is "Projects".
    /// </remarks>
    /// <example>
    /// <code>
    /// var payload = ClaudeHooksSerializer.DeserializeProjectsPreToolUse(json);
    /// var input = payload?.ToolInput;
    /// </code>
    /// </example>
    public sealed class ProjectsPreToolUsePayload : PreToolUseHookInput<ProjectsToolInput>
    {
    }

}
