using CloudNimble.ClaudeEssentials.Hooks;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Responses;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.Json;

namespace CloudNimble.ClaudeEssentials.Tests.Hooks.Tools
{

    /// <summary>
    /// Tests for <see cref="ExitPlanModeToolResponse"/> deserialization and round-trips.
    /// Payload structures match real captured hook interactions with all PII removed.
    /// </summary>
    [TestClass]
    public class ExitPlanModeToolResponseTests
    {

        #region Deserialization Tests

        [TestMethod]
        public void DeserializeExitPlanMode_WithAllFields_ShouldDeserializeCorrectly()
        {
            var json = """
                {
                    "session_id": "aaaaaaaa-bbbb-cccc-dddd-eeeeeeeeeeee",
                    "cwd": "D:\\Projects\\TestProject",
                    "permission_mode": "default",
                    "hook_event_name": "PostToolUse",
                    "tool_name": "ExitPlanMode",
                    "tool_input": {
                        "plan": "## Implementation Plan\n\n1. Create new models..."
                    },
                    "tool_response": {
                        "plan": "## Implementation Plan\n\n1. Create new models...",
                        "isAgent": false,
                        "filePath": "/tmp/plan-abc123.md",
                        "hasTaskTool": true,
                        "planWasEdited": false,
                        "awaitingLeaderApproval": false,
                        "requestId": "req-abc-123"
                    },
                    "tool_use_id": "toolu_01AAAAAAAAAAAAAAAAAAAAAA"
                }
                """;

            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.ExitPlanModePostToolUsePayload);

            result.Should().NotBeNull();
            result!.ToolResponse.Should().NotBeNull();
            result.ToolResponse!.Plan.Should().Contain("Implementation Plan");
            result.ToolResponse.IsAgent.Should().BeFalse();
            result.ToolResponse.FilePath.Should().Be("/tmp/plan-abc123.md");
            result.ToolResponse.HasTaskTool.Should().BeTrue();
            result.ToolResponse.PlanWasEdited.Should().BeFalse();
            result.ToolResponse.AwaitingLeaderApproval.Should().BeFalse();
            result.ToolResponse.RequestId.Should().Be("req-abc-123");
        }

        [TestMethod]
        public void DeserializeExitPlanMode_WithMinimalFields_ShouldDeserializeCorrectly()
        {
            var json = """
                {
                    "tool_name": "ExitPlanMode",
                    "tool_input": {
                        "plan": "Simple plan"
                    },
                    "tool_response": {
                        "plan": "Simple plan",
                        "isAgent": true
                    },
                    "tool_use_id": "toolu_01BBBBBBBBBBBBBBBBBBBBBB"
                }
                """;

            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.ExitPlanModePostToolUsePayload);

            result.Should().NotBeNull();
            result!.ToolResponse.Should().NotBeNull();
            result.ToolResponse!.Plan.Should().Be("Simple plan");
            result.ToolResponse.IsAgent.Should().BeTrue();
            result.ToolResponse.FilePath.Should().BeNull();
            result.ToolResponse.HasTaskTool.Should().BeNull();
            result.ToolResponse.PlanWasEdited.Should().BeNull();
            result.ToolResponse.AwaitingLeaderApproval.Should().BeNull();
            result.ToolResponse.RequestId.Should().BeNull();
        }

        [TestMethod]
        public void DeserializeExitPlanMode_WithNullPlan_ShouldDeserializeCorrectly()
        {
            var json = """
                {
                    "tool_name": "ExitPlanMode",
                    "tool_input": {
                        "plan": ""
                    },
                    "tool_response": {
                        "plan": null,
                        "isAgent": false,
                        "planWasEdited": true
                    },
                    "tool_use_id": "toolu_01CCCCCCCCCCCCCCCCCCCCCC"
                }
                """;

            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.ExitPlanModePostToolUsePayload);

            result.Should().NotBeNull();
            result!.ToolResponse.Should().NotBeNull();
            result.ToolResponse!.Plan.Should().BeNull();
            result.ToolResponse.PlanWasEdited.Should().BeTrue();
        }

        #endregion

        #region Round-Trip Tests

        [TestMethod]
        public void ExitPlanModeToolResponse_RoundTrip_ShouldMaintainData()
        {
            var response = new ExitPlanModeToolResponse
            {
                Plan = "## Plan\n\n1. Step one\n2. Step two",
                IsAgent = false,
                FilePath = "/tmp/plan-test.md",
                HasTaskTool = true,
                PlanWasEdited = false,
                AwaitingLeaderApproval = true,
                RequestId = "req-test-456"
            };

            var json = JsonSerializer.Serialize(response, ClaudeHooksJsonContext.Default.ExitPlanModeToolResponse);
            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.ExitPlanModeToolResponse);

            result.Should().NotBeNull();
            result!.Plan.Should().Contain("Step one");
            result.IsAgent.Should().BeFalse();
            result.FilePath.Should().Be("/tmp/plan-test.md");
            result.HasTaskTool.Should().BeTrue();
            result.PlanWasEdited.Should().BeFalse();
            result.AwaitingLeaderApproval.Should().BeTrue();
            result.RequestId.Should().Be("req-test-456");
        }

        [TestMethod]
        public void ExitPlanModeToolResponse_RoundTrip_MinimalFields_ShouldOmitNulls()
        {
            var response = new ExitPlanModeToolResponse
            {
                Plan = "Quick plan",
                IsAgent = true
            };

            var json = JsonSerializer.Serialize(response, ClaudeHooksJsonContext.Default.ExitPlanModeToolResponse);
            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.ExitPlanModeToolResponse);

            result.Should().NotBeNull();
            result!.Plan.Should().Be("Quick plan");
            result.IsAgent.Should().BeTrue();
            result.FilePath.Should().BeNull();
            result.HasTaskTool.Should().BeNull();
            result.PlanWasEdited.Should().BeNull();
            result.AwaitingLeaderApproval.Should().BeNull();
            result.RequestId.Should().BeNull();
        }

        #endregion

    }

}
