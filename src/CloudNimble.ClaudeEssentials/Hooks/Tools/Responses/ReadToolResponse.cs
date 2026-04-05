using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools.Responses
{

    /// <summary>
    /// Represents the base response payload returned by the Claude Code Read tool after reading a file.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The Read tool reads files from the local filesystem and returns their content along with
    /// metadata about the file. This response is received in the <see cref="ReadPostToolUsePayload"/>
    /// when the <c>tool_name</c> is "Read".
    /// </para>
    /// <para>
    /// The Read tool supports reading text files, images (PNG, JPG, etc.), PDF files, and Jupyter notebooks (.ipynb).
    /// The <c>type</c> field in the JSON determines which derived type is used for deserialization.
    /// </para>
    /// </remarks>
    [JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
    [JsonDerivedType(typeof(TextReadToolResponse), "text")]
    [JsonDerivedType(typeof(ImageReadToolResponse), "image")]
    [JsonDerivedType(typeof(NotebookReadToolResponse), "notebook")]
    [JsonDerivedType(typeof(PdfReadToolResponse), "pdf")]
    [JsonDerivedType(typeof(PartsReadToolResponse), "parts")]
    [JsonDerivedType(typeof(FileUnchangedReadToolResponse), "file_unchanged")]
    public abstract class ReadToolResponse
    {
    }

}
