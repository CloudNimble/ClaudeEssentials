using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs
{

    /// <summary>
    /// Represents the input parameters for the RemoteTrigger tool, which manages scheduled cloud agents.
    /// </summary>
    public class RemoteTriggerToolInput
    {

        #region Properties

        /// <summary>
        /// Gets or sets the action to perform.
        /// </summary>
        [JsonPropertyName("action")]
        public RemoteTriggerAction Action { get; set; }

        /// <summary>
        /// Gets or sets the request body. Required for create and update, and optional for run.
        /// </summary>
        [JsonPropertyName("body")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Dictionary<string, JsonElement>? Body { get; set; }

        /// <summary>
        /// Gets or sets the <c>next_cursor</c> from a previous list_runs or get_run_log page.
        /// </summary>
        [JsonPropertyName("cursor")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Cursor { get; set; }

        /// <summary>
        /// Gets or sets the run session ID. Required for get_run_log.
        /// </summary>
        [JsonPropertyName("session_id")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? SessionId { get; set; }

        /// <summary>
        /// Gets or sets the trigger ID. Required for get, update, run, and list_runs.
        /// </summary>
        [JsonPropertyName("trigger_id")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? TriggerId { get; set; }

        #endregion

    }

}
