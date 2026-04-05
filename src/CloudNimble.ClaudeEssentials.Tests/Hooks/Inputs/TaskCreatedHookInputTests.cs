using System.Text.Json;
using CloudNimble.ClaudeEssentials.Hooks;
using CloudNimble.ClaudeEssentials.Hooks.Inputs;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CloudNimble.ClaudeEssentials.Tests.Hooks.Inputs
{

    /// <summary>
    /// Tests for deserializing <see cref="TaskCreatedHookInput"/> payloads from JSON.
    /// </summary>
    [TestClass]
    public class TaskCreatedHookInputTests
    {

        #region Deserialization Tests

        [TestMethod]
        public void DeserializeTaskCreatedInput_WithAllFields_ShouldDeserializeCorrectly()
        {
            var json = """
                {
                    "session_id": "sess-tc-01",
                    "task_id": "task-001",
                    "task_subject": "Refactor authentication module",
                    "task_description": "Split the monolithic auth service into separate components",
                    "team_name": "backend-team",
                    "teammate_name": "claude-agent-1"
                }
                """;

            var result = ClaudeHooksSerializer.DeserializeTaskCreatedInput(json);

            result.Should().NotBeNull();
            result!.SessionId.Should().Be("sess-tc-01");
            result.TaskId.Should().Be("task-001");
            result.TaskSubject.Should().Be("Refactor authentication module");
            result.TaskDescription.Should().Be("Split the monolithic auth service into separate components");
            result.TeamName.Should().Be("backend-team");
            result.TeammateName.Should().Be("claude-agent-1");
        }

        [TestMethod]
        public void DeserializeTaskCreatedInput_WithMinimalFields_ShouldDeserializeCorrectly()
        {
            var json = """
                {
                    "task_id": "task-002",
                    "task_subject": "Fix build errors"
                }
                """;

            var result = ClaudeHooksSerializer.DeserializeTaskCreatedInput(json);

            result.Should().NotBeNull();
            result!.TaskId.Should().Be("task-002");
            result.TaskSubject.Should().Be("Fix build errors");
            result.TaskDescription.Should().BeNull();
            result.TeamName.Should().BeNull();
            result.TeammateName.Should().BeNull();
        }

        #endregion

        #region Malformed Payload Tests

        [TestMethod]
        public void DeserializeTaskCreatedInput_WithGarbageJson_ShouldThrowJsonException()
        {
            var action = () => ClaudeHooksSerializer.DeserializeTaskCreatedInput("not json");
            action.Should().Throw<JsonException>();
        }

        [TestMethod]
        public void DeserializeTaskCreatedInput_WithEmptyObject_ShouldReturnObjectWithDefaults()
        {
            var result = ClaudeHooksSerializer.DeserializeTaskCreatedInput("{}");
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
