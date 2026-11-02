using CloudNimble.ClaudeEssentials.Hooks.Inputs;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Responses;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools
{

    /// <summary>
    /// Represents the complete payload delivered to a PostToolUse hook after the OfferChromeSetup tool has executed.
    /// </summary>
    /// <remarks>
    /// The OfferChromeSetup tool offers to connect the user's browser. Your hook receives both the request parameters and the result.
    /// </remarks>
    /// <example>
    /// <code>
    /// var payload = ClaudeHooksSerializer.DeserializeOfferChromeSetupPostToolUse(json);
    /// var response = payload?.ToolResponse;
    /// </code>
    /// </example>
    public sealed class OfferChromeSetupPostToolUsePayload : PostToolUseHookInput<OfferChromeSetupToolInput, OfferChromeSetupToolResponse>
    {
    }

}
