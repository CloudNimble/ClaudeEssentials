using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks
{

    /// <summary>
    /// Represents the type of background task registered for a workflow.
    /// </summary>
    public enum WorkflowTaskType
    {

        /// <summary>
        /// An in-process workflow run.
        /// </summary>
        [JsonStringEnumMemberName("local_workflow")]
        LocalWorkflow,

        /// <summary>
        /// A workflow dispatched to a remote agent.
        /// </summary>
        [JsonStringEnumMemberName("remote_agent")]
        RemoteAgent

    }

}
