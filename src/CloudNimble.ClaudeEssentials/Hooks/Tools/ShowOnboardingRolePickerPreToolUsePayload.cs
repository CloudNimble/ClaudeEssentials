using CloudNimble.ClaudeEssentials.Hooks.Inputs;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools
{

    /// <summary>
    /// Represents the complete payload delivered to a PreToolUse hook when the ShowOnboardingRolePicker tool is about to be invoked.
    /// </summary>
    /// <remarks>
    /// The ShowOnboardingRolePicker tool asks the user to pick a role during onboarding. Use this type for strongly-typed deserialization of PreToolUse hook payloads when <c>tool_name</c> is "ShowOnboardingRolePicker".
    /// </remarks>
    /// <example>
    /// <code>
    /// var payload = ClaudeHooksSerializer.DeserializeShowOnboardingRolePickerPreToolUse(json);
    /// var input = payload?.ToolInput;
    /// </code>
    /// </example>
    public sealed class ShowOnboardingRolePickerPreToolUsePayload : PreToolUseHookInput<ShowOnboardingRolePickerToolInput>
    {
    }

}
