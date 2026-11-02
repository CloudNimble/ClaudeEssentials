using CloudNimble.ClaudeEssentials.Hooks.Inputs;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools
{

    /// <summary>
    /// Represents the complete payload delivered to a PreToolUse hook when the OfferChromeSetup tool is about to be invoked.
    /// </summary>
    /// <remarks>
    /// The OfferChromeSetup tool offers to connect the user's browser. Use this type for strongly-typed deserialization of PreToolUse hook payloads when <c>tool_name</c> is "OfferChromeSetup".
    /// </remarks>
    /// <example>
    /// <code>
    /// var payload = ClaudeHooksSerializer.DeserializeOfferChromeSetupPreToolUse(json);
    /// var input = payload?.ToolInput;
    /// </code>
    /// </example>
    public sealed class OfferChromeSetupPreToolUsePayload : PreToolUseHookInput<OfferChromeSetupToolInput>
    {
    }

}
