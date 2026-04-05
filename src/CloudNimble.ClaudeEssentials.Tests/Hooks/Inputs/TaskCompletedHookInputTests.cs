using System.Text.Json;
using CloudNimble.ClaudeEssentials.Hooks;
using CloudNimble.ClaudeEssentials.Hooks.Inputs;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CloudNimble.ClaudeEssentials.Tests.Hooks.Inputs
{

    /// <summary>
    /// Tests for deserializing <see cref="TaskCompletedHookInput"/> payloads from JSON.
    /// </summary>
    [TestClass]
    public class TaskCompletedHookInputTests
    {

        #region Deserialization Tests

        [TestMethod]
        public void DeserializeTaskCompletedInput_WithAllFields_ShouldDeserializeCorrectly()
        {
            var json = """
                {
                    "session_id": "sess-tcomp-01",
                    "task_id": "task-003",
                    "task_subject": "Add unit tests for serializer",
                    "task_description": "Write comprehensive tests for all 16 new hook types",
                    "team_name": "testing-team",
                    "teammate_name": "claude-agent-2"
                }
                """;

            var result = ClaudeHooksSerializer.DeserializeTaskCompletedInput(json);

            result.Should().NotBeNull();
            result!.SessionId.Should().Be("sess-tcomp-01");
            result.TaskId.Should().Be("task-003");
            result.TaskSubject.Should().Be("Add unit tests for serializer");
            result.TaskDescription.Should().Be("Write comprehensive tests for all 16 new hook types");
            result.TeamName.Should().Be("testing-team");
            result.TeammateName.Should().Be("claude-agent-2");
        }

        #endregion

        #region Malformed Payload Tests

        [TestMethod]
        public void DeserializeTaskCompletedInput_WithGarbageJson_ShouldThrowJsonException()
        {
            var action = () => ClaudeHooksSerializer.DeserializeTaskCompletedInput("{{invalid}}");
            action.Should().Throw<JsonException>();
        }

        [TestMethod]
        public void DeserializeTaskCompletedInput_WithEmptyObject_ShouldReturnObjectWithDefaults()
        {
            var result = ClaudeHooksSerializer.DeserializeTaskCompletedInput("{}");
            result.Should().NotBeNull();
            result!.SessionId.Should().BeEmpty();
            result.TaskId.Should().BeEmpty();
            result.TaskSubject.Should().BeEmpty();
            result.TaskDescription.Should().BeNull();
            result.TeamName.Should().BeNull();
            result.TeammateName.Should().BeNull();
        }

        #endregion

    }

}
