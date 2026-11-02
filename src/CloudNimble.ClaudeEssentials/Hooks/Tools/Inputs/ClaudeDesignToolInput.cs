using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs
{

    /// <summary>
    /// Represents the input parameters for the ClaudeDesign tool.
    /// </summary>
    public class ClaudeDesignToolInput
    {

        #region Properties

        /// <summary>
        /// Gets or sets the action input object, validated by the server. Pass an empty object for operations that take no input.
        /// </summary>
        [JsonPropertyName("arguments")]
        public Dictionary<string, JsonElement> Arguments { get; set; } = [];

        /// <summary>
        /// Gets or sets the Claude Design action to perform. Call with <c>list</c> first to discover the available operations and their argument schemas.
        /// </summary>
        [JsonPropertyName("operation")]
        public string Operation { get; set; } = string.Empty;

        #endregion

    }

}
