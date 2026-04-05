using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs
{

    /// <summary>
    /// Represents the input parameters for the ExitPlanMode tool.
    /// The ExitPlanMode tool transitions from plan mode to execution mode.
    /// </summary>
    public class ExitPlanModeToolInput
    {

        /// <summary>
        /// Gets or sets the prompt-based permissions needed to implement the plan.
        /// These describe categories of actions rather than specific commands.
        /// </summary>
        [JsonPropertyName("allowedPrompts")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<ExitPlanModeAllowedPrompt>? AllowedPrompts { get; set; }

    }

}
