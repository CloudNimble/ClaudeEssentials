using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs
{

    /// <summary>
    /// Represents the input parameters for the ExitPlanMode tool.
    /// The ExitPlanMode tool transitions from plan mode to execution mode.
    /// </summary>
    public class ExitPlanModeToolInput
    {

        /// <summary>
        /// Gets or sets the allowed prompts. This value is deprecated and no longer used.
        /// </summary>
        [JsonPropertyName("allowedPrompts")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<ExitPlanModeAllowedPrompt>? AllowedPrompts { get; set; }

        /// <summary>
        /// Gets or sets any additional properties present in the payload that are not modeled explicitly.
        /// </summary>
        [JsonExtensionData]
        public Dictionary<string, JsonElement>? ExtensionData { get; set; }

    }

}
