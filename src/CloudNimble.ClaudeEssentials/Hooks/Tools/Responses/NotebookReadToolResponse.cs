using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools.Responses
{

    /// <summary>
    /// Represents the Read tool response for a Jupyter notebook (.ipynb) file.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Jupyter notebooks are interactive documents that combine code, text, and visualizations.
    /// The response returns all cells with their outputs.
    /// </para>
    /// </remarks>
    public class NotebookReadToolResponse : ReadToolResponse
    {

        /// <summary>
        /// Gets or sets the notebook file information containing the path and cells.
        /// </summary>
        [JsonPropertyName("file")]
        public NotebookReadToolFileInfo File { get; set; } = new();

    }

}
