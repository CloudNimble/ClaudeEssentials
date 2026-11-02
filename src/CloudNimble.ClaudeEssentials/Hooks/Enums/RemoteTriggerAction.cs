using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks
{

    /// <summary>
    /// Represents the action performed by the RemoteTrigger tool.
    /// </summary>
    public enum RemoteTriggerAction
    {

        /// <summary>
        /// Lists triggers.
        /// </summary>
        [JsonStringEnumMemberName("list")]
        List,

        /// <summary>
        /// Gets a trigger.
        /// </summary>
        [JsonStringEnumMemberName("get")]
        Get,

        /// <summary>
        /// Creates a trigger.
        /// </summary>
        [JsonStringEnumMemberName("create")]
        Create,

        /// <summary>
        /// Updates a trigger.
        /// </summary>
        [JsonStringEnumMemberName("update")]
        Update,

        /// <summary>
        /// Runs a trigger.
        /// </summary>
        [JsonStringEnumMemberName("run")]
        Run,

        /// <summary>
        /// Creates a webhook trigger.
        /// </summary>
        [JsonStringEnumMemberName("create_webhook_trigger")]
        CreateWebhookTrigger,

        /// <summary>
        /// Lists the runs of a trigger.
        /// </summary>
        [JsonStringEnumMemberName("list_runs")]
        ListRuns,

        /// <summary>
        /// Gets the log of a run.
        /// </summary>
        [JsonStringEnumMemberName("get_run_log")]
        GetRunLog

    }

}
