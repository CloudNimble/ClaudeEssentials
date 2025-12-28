using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Inputs
{

    /// <summary>
    /// Represents the input received by a UserPromptSubmit hook.
    /// This hook runs when the user submits a prompt, before Claude processes it.
    /// </summary>
    public class UserPromptSubmitHookInput : HookInputBase
    {

        /// <summary>
        /// Gets or sets the text of the user's submitted prompt.
        /// </summary>
        [JsonPropertyName("prompt")]
        public string Prompt { get; set; } = string.Empty;

    }

}
