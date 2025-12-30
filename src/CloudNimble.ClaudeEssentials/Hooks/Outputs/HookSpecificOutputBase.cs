using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Outputs
{

    /// <summary>
    /// Base class for hook-specific output data.
    /// Contains the hook event name and serves as base for specific output types.
    /// </summary>
    public abstract class HookSpecificOutputBase
    {

        /// <summary>
        /// Gets or sets the name of the hook event this output corresponds to.
        /// </summary>
        [JsonPropertyName("hookEventName")]
        public abstract HookEventName HookEventName { get; }

    }

}
