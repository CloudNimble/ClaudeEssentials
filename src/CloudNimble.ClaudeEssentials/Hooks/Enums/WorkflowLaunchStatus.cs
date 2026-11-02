using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks
{

    /// <summary>
    /// Represents how a workflow was launched.
    /// </summary>
    public enum WorkflowLaunchStatus
    {

        /// <summary>
        /// The workflow was launched as a local background task.
        /// </summary>
        [JsonStringEnumMemberName("async_launched")]
        AsyncLaunched,

        /// <summary>
        /// The workflow was dispatched to a remote cloud session.
        /// </summary>
        [JsonStringEnumMemberName("remote_launched")]
        RemoteLaunched

    }

}
