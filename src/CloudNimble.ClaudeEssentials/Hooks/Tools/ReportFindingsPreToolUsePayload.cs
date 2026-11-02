using CloudNimble.ClaudeEssentials.Hooks.Inputs;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools
{

    /// <summary>
    /// Represents the complete payload delivered to a PreToolUse hook when the ReportFindings tool is about to be invoked.
    /// </summary>
    /// <remarks>
    /// The ReportFindings tool reports code review findings as a typed list. Use this type for strongly-typed deserialization of PreToolUse hook payloads when <c>tool_name</c> is "ReportFindings".
    /// </remarks>
    /// <example>
    /// <code>
    /// var payload = ClaudeHooksSerializer.DeserializeReportFindingsPreToolUse(json);
    /// var input = payload?.ToolInput;
    /// </code>
    /// </example>
    public sealed class ReportFindingsPreToolUsePayload : PreToolUseHookInput<ReportFindingsToolInput>
    {
    }

}
