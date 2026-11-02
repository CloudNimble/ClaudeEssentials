using CloudNimble.ClaudeEssentials.Hooks;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Responses;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.Json;

namespace CloudNimble.ClaudeEssentials.Tests.Hooks.Tools
{

    /// <summary>
    /// Tests for <see cref="NotebookEditToolResponse"/> deserialization and round-trips.
    /// Payload structures match real captured hook interactions with all PII removed.
    /// </summary>
    [TestClass]
    public class NotebookEditToolResponseTests
    {

        #region Deserialization Tests

        [TestMethod]
        public void DeserializeNotebookEditToolResponse_Success_ShouldDeserializeCorrectly()
        {
            var json = """
                {
                    "session_id": "aaaaaaaa-bbbb-cccc-dddd-eeeeeeeeeeee",
                    "cwd": "D:\\Projects\\TestProject",
                    "permission_mode": "acceptEdits",
                    "hook_event_name": "PostToolUse",
                    "tool_name": "NotebookEdit",
                    "tool_input": {
                        "notebook_path": "D:\\Projects\\TestProject\\analysis.ipynb",
                        "cell_id": "cell_abc123",
                        "new_source": "import pandas as pd",
                        "cell_type": "code",
                        "edit_mode": "replace"
                    },
                    "tool_response": {
                        "notebook_path": "D:\\Projects\\TestProject\\analysis.ipynb",
                        "cell_id": "cell_abc123",
                        "edit_mode": "replace",
                        "new_source": "import pandas as pd",
                        "cell_type": "code",
                        "language": "python"
                    },
                    "tool_use_id": "toolu_01TestNotebookEditPost001"
                }
                """;

            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.NotebookEditPostToolUsePayload);

            result.Should().NotBeNull();
            result!.ToolName.Should().Be("NotebookEdit");
            result.HookEventName.Should().Be(HookEventName.PostToolUse);
            result.ToolResponse.Should().NotBeNull();
            result.ToolResponse!.NotebookPath.Should().Contain("analysis.ipynb");
            result.ToolResponse.CellId.Should().Be("cell_abc123");
            result.ToolResponse.EditMode.Should().Be(NotebookEditMode.Replace);
            result.ToolResponse.NewSource.Should().Be("import pandas as pd");
            result.ToolResponse.CellType.Should().Be(NotebookCellType.Code);
            result.ToolResponse.Language.Should().Be("python");
            result.ToolResponse.Error.Should().BeNull();
        }

        [TestMethod]
        public void DeserializeNotebookEditToolResponse_WithFileContents_ShouldDeserializeCorrectly()
        {
            var json = """
                {
                    "tool_name": "NotebookEdit",
                    "tool_input": {
                        "notebook_path": "D:\\Projects\\TestProject\\report.ipynb",
                        "cell_id": "cell_def456",
                        "new_source": "# Updated heading",
                        "edit_mode": "replace"
                    },
                    "tool_response": {
                        "notebook_path": "D:\\Projects\\TestProject\\report.ipynb",
                        "cell_id": "cell_def456",
                        "edit_mode": "replace",
                        "new_source": "# Updated heading",
                        "cell_type": "markdown",
                        "original_file": "{\"cells\":[{\"id\":\"cell_def456\",\"source\":[\"# Old heading\"]}]}",
                        "updated_file": "{\"cells\":[{\"id\":\"cell_def456\",\"source\":[\"# Updated heading\"]}]}"
                    },
                    "tool_use_id": "toolu_01TestNotebookEditFile001"
                }
                """;

            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.NotebookEditPostToolUsePayload);

            result.Should().NotBeNull();
            result!.ToolResponse.Should().NotBeNull();
            result.ToolResponse!.OriginalFile.Should().Contain("Old heading");
            result.ToolResponse.UpdatedFile.Should().Contain("Updated heading");
        }

        [TestMethod]
        public void DeserializeNotebookEditToolResponse_Error_ShouldDeserializeCorrectly()
        {
            var json = """
                {
                    "tool_name": "NotebookEdit",
                    "tool_input": {
                        "notebook_path": "D:\\Projects\\TestProject\\missing.ipynb",
                        "cell_id": "cell_nonexistent",
                        "new_source": "x = 1"
                    },
                    "tool_response": {
                        "notebook_path": "D:\\Projects\\TestProject\\missing.ipynb",
                        "error": "Cell not found: cell_nonexistent"
                    },
                    "tool_use_id": "toolu_01TestNotebookEditError001"
                }
                """;

            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.NotebookEditPostToolUsePayload);

            result.Should().NotBeNull();
            result!.ToolResponse.Should().NotBeNull();
            result.ToolResponse!.Error.Should().Contain("Cell not found");
            result.ToolResponse.CellId.Should().BeNull();
        }

        #endregion

        #region Round-Trip Tests

        [TestMethod]
        public void NotebookEditToolResponse_RoundTrip_WithAllFields_ShouldMaintainData()
        {
            var response = new NotebookEditToolResponse
            {
                NotebookPath = "D:\\Projects\\TestProject\\roundtrip.ipynb",
                CellId = "cell_rt_001",
                EditMode = NotebookEditMode.Insert,
                NewSource = "print('inserted')",
                CellType = NotebookCellType.Code,
                Language = "python",
                OriginalFile = "{\"cells\":[]}",
                UpdatedFile = "{\"cells\":[{\"source\":[\"print('inserted')\"]}]}"
            };

            var json = JsonSerializer.Serialize(response, ClaudeHooksJsonContext.Default.NotebookEditToolResponse);
            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.NotebookEditToolResponse);

            result.Should().NotBeNull();
            result!.NotebookPath.Should().Be(response.NotebookPath);
            result.CellId.Should().Be(response.CellId);
            result.EditMode.Should().Be(response.EditMode);
            result.NewSource.Should().Be(response.NewSource);
            result.CellType.Should().Be(response.CellType);
            result.Language.Should().Be(response.Language);
            result.OriginalFile.Should().Be(response.OriginalFile);
            result.UpdatedFile.Should().Be(response.UpdatedFile);
        }

        [TestMethod]
        public void NotebookEditToolResponse_RoundTrip_ErrorCase_ShouldMaintainData()
        {
            var response = new NotebookEditToolResponse
            {
                NotebookPath = "D:\\Projects\\TestProject\\error.ipynb",
                Error = "Notebook format is invalid"
            };

            var json = JsonSerializer.Serialize(response, ClaudeHooksJsonContext.Default.NotebookEditToolResponse);
            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.NotebookEditToolResponse);

            result.Should().NotBeNull();
            result!.NotebookPath.Should().Be(response.NotebookPath);
            result.Error.Should().Be(response.Error);
            result.CellId.Should().BeNull();
        }

        #endregion

    }

}
