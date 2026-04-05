using CloudNimble.ClaudeEssentials.Hooks;
using CloudNimble.ClaudeEssentials.Hooks.Tools;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.Json;

namespace CloudNimble.ClaudeEssentials.Tests.Hooks.Tools
{

    /// <summary>
    /// Tests for <see cref="NotebookEditToolInput"/> deserialization and round-trips.
    /// Payload structures match real captured hook interactions with all PII removed.
    /// </summary>
    [TestClass]
    public class NotebookEditToolInputTests
    {

        #region Deserialization Tests

        [TestMethod]
        public void DeserializeNotebookEditToolInput_ReplaceMode_ShouldDeserializeCorrectly()
        {
            var json = """
                {
                    "session_id": "aaaaaaaa-bbbb-cccc-dddd-eeeeeeeeeeee",
                    "cwd": "D:\\Projects\\TestProject",
                    "permission_mode": "acceptEdits",
                    "hook_event_name": "PreToolUse",
                    "tool_name": "NotebookEdit",
                    "tool_input": {
                        "notebook_path": "D:\\Projects\\TestProject\\analysis.ipynb",
                        "cell_id": "cell_abc123",
                        "new_source": "import pandas as pd\ndf = pd.read_csv('data.csv')",
                        "cell_type": "code",
                        "edit_mode": "replace"
                    },
                    "tool_use_id": "toolu_01TestNotebookEdit001"
                }
                """;

            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.NotebookEditPreToolUsePayload);

            result.Should().NotBeNull();
            result!.ToolName.Should().Be("NotebookEdit");
            result.HookEventName.Should().Be(HookEventName.PreToolUse);
            result.ToolInput.Should().NotBeNull();
            result.ToolInput!.NotebookPath.Should().Contain("analysis.ipynb");
            result.ToolInput.CellId.Should().Be("cell_abc123");
            result.ToolInput.NewSource.Should().Contain("pandas");
            result.ToolInput.CellType.Should().Be(NotebookCellType.Code);
            result.ToolInput.EditMode.Should().Be(NotebookEditMode.Replace);
        }

        [TestMethod]
        public void DeserializeNotebookEditToolInput_InsertMode_ShouldDeserializeCorrectly()
        {
            var json = """
                {
                    "tool_name": "NotebookEdit",
                    "tool_input": {
                        "notebook_path": "D:\\Projects\\TestProject\\report.ipynb",
                        "cell_id": "cell_def456",
                        "new_source": "# Data Analysis Results\n\nThis section covers the findings.",
                        "cell_type": "markdown",
                        "edit_mode": "insert"
                    },
                    "tool_use_id": "toolu_01TestNotebookEditInsert001"
                }
                """;

            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.NotebookEditPreToolUsePayload);

            result.Should().NotBeNull();
            result!.ToolInput.Should().NotBeNull();
            result.ToolInput!.EditMode.Should().Be(NotebookEditMode.Insert);
            result.ToolInput.CellType.Should().Be(NotebookCellType.Markdown);
            result.ToolInput.CellId.Should().Be("cell_def456");
        }

        [TestMethod]
        public void DeserializeNotebookEditToolInput_DeleteMode_ShouldDeserializeCorrectly()
        {
            var json = """
                {
                    "tool_name": "NotebookEdit",
                    "tool_input": {
                        "notebook_path": "D:\\Projects\\TestProject\\cleanup.ipynb",
                        "cell_id": "cell_ghi789",
                        "new_source": "",
                        "edit_mode": "delete"
                    },
                    "tool_use_id": "toolu_01TestNotebookEditDelete001"
                }
                """;

            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.NotebookEditPreToolUsePayload);

            result.Should().NotBeNull();
            result!.ToolInput.Should().NotBeNull();
            result.ToolInput!.EditMode.Should().Be(NotebookEditMode.Delete);
            result.ToolInput.CellType.Should().BeNull();
        }

        [TestMethod]
        public void DeserializeNotebookEditToolInput_MinimalPayload_ShouldDeserializeCorrectly()
        {
            var json = """
                {
                    "tool_name": "NotebookEdit",
                    "tool_input": {
                        "notebook_path": "D:\\Projects\\TestProject\\minimal.ipynb",
                        "new_source": "print('hello')"
                    },
                    "tool_use_id": "toolu_01TestNotebookEditMinimal001"
                }
                """;

            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.NotebookEditPreToolUsePayload);

            result.Should().NotBeNull();
            result!.ToolInput.Should().NotBeNull();
            result.ToolInput!.NotebookPath.Should().Contain("minimal.ipynb");
            result.ToolInput.NewSource.Should().Be("print('hello')");
            result.ToolInput.CellId.Should().BeNull();
            result.ToolInput.CellType.Should().BeNull();
            result.ToolInput.EditMode.Should().BeNull();
        }

        #endregion

        #region Round-Trip Tests

        [TestMethod]
        public void NotebookEditToolInput_RoundTrip_WithAllFields_ShouldMaintainData()
        {
            var input = new NotebookEditToolInput
            {
                NotebookPath = "D:\\Projects\\TestProject\\roundtrip.ipynb",
                CellId = "cell_rt_001",
                NewSource = "x = 42\nprint(x)",
                CellType = NotebookCellType.Code,
                EditMode = NotebookEditMode.Replace
            };

            var json = JsonSerializer.Serialize(input, ClaudeHooksJsonContext.Default.NotebookEditToolInput);
            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.NotebookEditToolInput);

            result.Should().NotBeNull();
            result!.NotebookPath.Should().Be(input.NotebookPath);
            result.CellId.Should().Be(input.CellId);
            result.NewSource.Should().Be(input.NewSource);
            result.CellType.Should().Be(input.CellType);
            result.EditMode.Should().Be(input.EditMode);
        }

        [TestMethod]
        public void NotebookEditToolInput_RoundTrip_WithoutOptionalFields_ShouldMaintainData()
        {
            var input = new NotebookEditToolInput
            {
                NotebookPath = "D:\\Projects\\TestProject\\minimal.ipynb",
                NewSource = "print('minimal')"
            };

            var json = JsonSerializer.Serialize(input, ClaudeHooksJsonContext.Default.NotebookEditToolInput);
            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.NotebookEditToolInput);

            result.Should().NotBeNull();
            result!.NotebookPath.Should().Be(input.NotebookPath);
            result.NewSource.Should().Be(input.NewSource);
            result.CellId.Should().BeNull();
            result.CellType.Should().BeNull();
            result.EditMode.Should().BeNull();
        }

        #endregion

    }

}
