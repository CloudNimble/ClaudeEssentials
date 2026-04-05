using System.Text.Json;
using CloudNimble.ClaudeEssentials.Hooks;
using CloudNimble.ClaudeEssentials.Hooks.Inputs;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CloudNimble.ClaudeEssentials.Tests.Hooks.Inputs
{

    /// <summary>
    /// Tests for deserializing <see cref="InstructionsLoadedHookInput"/> payloads from JSON.
    /// </summary>
    [TestClass]
    public class InstructionsLoadedHookInputTests
    {

        #region Deserialization Tests

        [TestMethod]
        public void DeserializeInstructionsLoadedInput_WithValidJson_ShouldDeserializeCorrectly()
        {
            var json = """
                {
                    "session_id": "sess-inst-01",
                    "transcript_path": "/tmp/transcript.jsonl",
                    "cwd": "/home/user/project",
                    "file_path": "/home/user/project/CLAUDE.md",
                    "globs": ["**/*.cs", "src/**/*.ts"],
                    "load_reason": "session_start",
                    "memory_type": "Project",
                    "parent_file_path": "/home/user/.claude/CLAUDE.md",
                    "trigger_file_path": "/home/user/project/src/main.cs"
                }
                """;

            var result = ClaudeHooksSerializer.DeserializeInstructionsLoadedInput(json);

            result.Should().NotBeNull();
            result!.SessionId.Should().Be("sess-inst-01");
            result.FilePath.Should().Be("/home/user/project/CLAUDE.md");
            result.Globs.Should().BeEquivalentTo(["**/*.cs", "src/**/*.ts"]);
            result.LoadReason.Should().Be(InstructionsLoadReason.SessionStart);
            result.MemoryType.Should().Be(MemoryType.Project);
            result.ParentFilePath.Should().Be("/home/user/.claude/CLAUDE.md");
            result.TriggerFilePath.Should().Be("/home/user/project/src/main.cs");
        }

        [TestMethod]
        public void DeserializeInstructionsLoadedInput_WithMinimalJson_ShouldDeserializeCorrectly()
        {
            var json = """
                {
                    "file_path": "/home/user/CLAUDE.md",
                    "load_reason": "compact",
                    "memory_type": "User"
                }
                """;

            var result = ClaudeHooksSerializer.DeserializeInstructionsLoadedInput(json);

            result.Should().NotBeNull();
            result!.FilePath.Should().Be("/home/user/CLAUDE.md");
            result.LoadReason.Should().Be(InstructionsLoadReason.Compact);
            result.MemoryType.Should().Be(MemoryType.User);
            result.Globs.Should().BeNull();
            result.ParentFilePath.Should().BeNull();
            result.TriggerFilePath.Should().BeNull();
        }

        #endregion

        #region Malformed Payload Tests

        [TestMethod]
        public void DeserializeInstructionsLoadedInput_WithGarbageJson_ShouldThrowJsonException()
        {
            var action = () => ClaudeHooksSerializer.DeserializeInstructionsLoadedInput("not json");
            action.Should().Throw<JsonException>();
        }

        [TestMethod]
        public void DeserializeInstructionsLoadedInput_WithEmptyObject_ShouldReturnObjectWithDefaults()
        {
            var result = ClaudeHooksSerializer.DeserializeInstructionsLoadedInput("{}");
            result.Should().NotBeNull();
            result!.SessionId.Should().BeEmpty();
            result.FilePath.Should().BeEmpty();
            result.Globs.Should().BeNull();
            result.ParentFilePath.Should().BeNull();
            result.TriggerFilePath.Should().BeNull();
        }

        #endregion

    }

}
