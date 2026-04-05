using CloudNimble.ClaudeEssentials.Hooks;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Responses;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.Json;

namespace CloudNimble.ClaudeEssentials.Tests.Hooks.Tools
{

    /// <summary>
    /// Tests for <see cref="NotebookReadToolResponse"/> deserialization and round-trips.
    /// Schema-based payloads — no captured notebook Read logs exist in this repo.
    /// </summary>
    [TestClass]
    public class NotebookReadToolResponseTests
    {

        #region Deserialization Tests

        [TestMethod]
        public void DeserializeNotebookRead_WithCells_ShouldDeserializeCorrectly()
        {
            var json = """
                {
                    "tool_name": "Read",
                    "tool_input": {
                        "file_path": "D:\\Projects\\TestProject\\analysis.ipynb"
                    },
                    "tool_response": {
                        "type": "notebook",
                        "file": {
                            "filePath": "D:\\Projects\\TestProject\\analysis.ipynb",
                            "cells": [
                                {
                                    "cell_type": "markdown",
                                    "source": ["# Sample Notebook"]
                                },
                                {
                                    "cell_type": "code",
                                    "execution_count": 1,
                                    "source": ["print('hello')"],
                                    "outputs": [
                                        {
                                            "output_type": "stream",
                                            "text": ["hello\n"]
                                        }
                                    ]
                                }
                            ]
                        }
                    },
                    "tool_use_id": "toolu_01AAAAAAAAAAAAAAAAAAAAAA"
                }
                """;

            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.ReadPostToolUsePayload);

            result.Should().NotBeNull();
            result!.ToolResponse.Should().BeOfType<NotebookReadToolResponse>();
            var notebook = (NotebookReadToolResponse)result.ToolResponse!;
            notebook.File.FilePath.Should().Contain("analysis.ipynb");
            notebook.File.Cells.Should().HaveCount(2);
        }

        [TestMethod]
        public void DeserializeNotebookRead_WithEmptyCells_ShouldDeserializeCorrectly()
        {
            var json = """
                {
                    "tool_name": "Read",
                    "tool_input": {
                        "file_path": "D:\\Projects\\TestProject\\empty.ipynb"
                    },
                    "tool_response": {
                        "type": "notebook",
                        "file": {
                            "filePath": "D:\\Projects\\TestProject\\empty.ipynb",
                            "cells": []
                        }
                    },
                    "tool_use_id": "toolu_01BBBBBBBBBBBBBBBBBBBBBB"
                }
                """;

            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.ReadPostToolUsePayload);

            var notebook = (NotebookReadToolResponse)result!.ToolResponse!;
            notebook.File.Cells.Should().BeEmpty();
        }

        #endregion

        #region Round-Trip Tests

        [TestMethod]
        public void NotebookReadToolResponse_RoundTrip_ShouldMaintainData()
        {
            var response = new NotebookReadToolResponse
            {
                File = new NotebookReadToolFileInfo
                {
                    FilePath = "/tmp/test.ipynb",
                    Cells = [JsonDocument.Parse("""{"cell_type":"code","source":["x = 1"]}""").RootElement]
                }
            };

            var json = JsonSerializer.Serialize<ReadToolResponse>(response, ClaudeHooksJsonContext.Default.ReadToolResponse);
            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.ReadToolResponse);

            result.Should().BeOfType<NotebookReadToolResponse>();
            var notebook = (NotebookReadToolResponse)result!;
            notebook.File.FilePath.Should().Be("/tmp/test.ipynb");
            notebook.File.Cells.Should().HaveCount(1);
        }

        #endregion

    }

}
