using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs
{

    /// <summary>
    /// Represents the WebSocket to open for the Monitor tool.
    /// </summary>
    public class MonitorWebSocketOptions
    {

        #region Properties

        /// <summary>
        /// Gets or sets the WebSocket subprotocols to request.
        /// </summary>
        [JsonPropertyName("protocols")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<string>? Protocols { get; set; }

        /// <summary>
        /// Gets or sets the WebSocket URL.
        /// </summary>
        [JsonPropertyName("url")]
        public string Url { get; set; } = string.Empty;

        #endregion

    }

}
