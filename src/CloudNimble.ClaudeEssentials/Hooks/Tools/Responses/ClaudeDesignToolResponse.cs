using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools.Responses
{

    /// <summary>
    /// Represents the response payload returned by the ClaudeDesign tool.
    /// </summary>
    public class ClaudeDesignToolResponse
    {

        #region Properties

        /// <summary>
        /// Gets or sets the content blocks returned by the operation.
        /// </summary>
        [JsonPropertyName("content")]
        public List<Dictionary<string, JsonElement>> Content { get; set; } = [];

        /// <summary>
        /// Gets or sets a value indicating whether the operation failed.
        /// </summary>
        [JsonPropertyName("isError")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public bool? IsError { get; set; }

        /// <summary>
        /// Gets or sets the operation that was performed.
        /// </summary>
        [JsonPropertyName("operation")]
        public string Operation { get; set; } = string.Empty;

        #endregion

    }

}
