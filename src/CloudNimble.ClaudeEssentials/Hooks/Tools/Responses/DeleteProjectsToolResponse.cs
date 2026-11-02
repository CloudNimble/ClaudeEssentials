using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools.Responses
{

    /// <summary>
    /// Represents the Projects tool response for the <c>project_delete</c> method.
    /// </summary>
    public class DeleteProjectsToolResponse : ProjectsToolResponse
    {

        #region Properties

        /// <summary>
        /// Gets or sets a value indicating whether the document was deleted.
        /// </summary>
        [JsonPropertyName("deleted")]
        public bool Deleted { get; set; }

        /// <summary>
        /// Gets or sets the document path.
        /// </summary>
        [JsonPropertyName("path")]
        public string Path { get; set; } = string.Empty;

        #endregion

    }

}
