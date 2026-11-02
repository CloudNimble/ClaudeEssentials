using CloudNimble.ClaudeEssentials.Hooks.Inputs;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Responses;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools
{

    /// <summary>
    /// Represents the complete payload delivered to a PostToolUse hook after the ShowOnboardingRolePicker tool has executed.
    /// </summary>
    /// <remarks>
    /// The ShowOnboardingRolePicker tool asks the user to pick a role during onboarding. Your hook receives both the request parameters and the result.
    /// </remarks>
    /// <example>
    /// <code>
    /// var payload = ClaudeHooksSerializer.DeserializeShowOnboardingRolePickerPostToolUse(json);
    /// var response = payload?.ToolResponse;
    /// </code>
    /// </example>
    public sealed class ShowOnboardingRolePickerPostToolUsePayload : PostToolUseHookInput<ShowOnboardingRolePickerToolInput, ShowOnboardingRolePickerToolResponse>
    {
    }

}
