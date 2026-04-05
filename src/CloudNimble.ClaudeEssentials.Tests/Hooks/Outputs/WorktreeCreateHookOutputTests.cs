using System.Text.Json;
using CloudNimble.ClaudeEssentials.Hooks;
using CloudNimble.ClaudeEssentials.Hooks.Outputs;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CloudNimble.ClaudeEssentials.Tests.Hooks.Outputs
{

    /// <summary>
    /// Tests for serializing <see cref="WorktreeCreateHookOutput"/> payloads to JSON.
    /// </summary>
    [TestClass]
    public class WorktreeCreateHookOutputTests
    {

        #region Serialization Tests

        [TestMethod]
        public void SerializeWorktreeCreateOutput_WithWorktreePath_ShouldSerializeCorrectly()
        {
            var output = new WorktreeCreateHookOutput
            {
                Continue = true,
                HookSpecificOutput = new WorktreeCreateSpecificOutput
                {
                    WorktreePath = "/tmp/worktrees/feature-branch"
                }
            };

            var json = ClaudeHooksSerializer.SerializeWorktreeCreateOutput(output);
            var doc = JsonDocument.Parse(json);

            doc.RootElement.GetProperty("hookSpecificOutput")
                .GetProperty("worktreePath").GetString()
                .Should().Be("/tmp/worktrees/feature-branch");
            doc.RootElement.GetProperty("hookSpecificOutput")
                .GetProperty("hookEventName").GetString().Should().Be("WorktreeCreate");
        }

        #endregion

    }

}
