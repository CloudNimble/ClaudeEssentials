using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs
{

    /// <summary>
    /// Represents the input parameters for the ProposeGoal tool, which proposes a completion condition for the session.
    /// </summary>
    public class ProposeGoalToolInput
    {

        #region Properties

        /// <summary>
        /// Gets or sets a value indicating whether to ask the user for approval before the goal is set. Defaults to true.
        /// </summary>
        [JsonPropertyName("ask_user")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public bool? AskUser { get; set; }

        /// <summary>
        /// Gets or sets the completion condition to propose, written so that a separate evaluator can verify it. At most 500 characters.
        /// </summary>
        [JsonPropertyName("condition")]
        public string Condition { get; set; } = string.Empty;

        #endregion

    }

}
