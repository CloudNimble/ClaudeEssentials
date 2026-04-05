using CloudNimble.ClaudeEssentials.Hooks;
using CloudNimble.ClaudeEssentials.Hooks.Inputs;
using CloudNimble.ClaudeEssentials.Hooks.Outputs;
using CloudNimble.ClaudeEssentials.Hooks.Tools;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Responses;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;

namespace CloudNimble.ClaudeEssentials.Tests
{

    /// <summary>
    /// Tests for <see cref="ClaudeHooksSerializer"/> methods not covered by other test classes.
    /// Focuses on generic overloads and remaining convenience methods.
    /// </summary>
    [TestClass]
    public class ClaudeHooksSerializerTests
    {

        #region Generic Overload Tests

        [TestMethod]
        public void DeserializePreToolUseInput_GenericOverload_WithJsonTypeInfo_ShouldDeserializeCorrectly()
        {
            var json = """
                {
                    "session_id": "aaaaaaaa-bbbb-cccc-dddd-eeeeeeeeeeee",
                    "cwd": "D:\\Projects\\TestProject",
                    "hook_event_name": "PreToolUse",
                    "tool_name": "Bash",
                    "tool_input": {
                        "command": "echo hello",
                        "description": "Print hello"
                    },
                    "tool_use_id": "toolu_01AAAAAAAAAAAAAAAAAAAAAA"
                }
                """;

            var result = ClaudeHooksSerializer.DeserializePreToolUseInput(
                json,
                ClaudeHooksJsonContext.Default.BashPreToolUsePayload);

            result.Should().NotBeNull();
            result!.ToolName.Should().Be("Bash");
            result.ToolInput.Should().NotBeNull();
            result.ToolInput!.Command.Should().Be("echo hello");
            result.ToolInput.Description.Should().Be("Print hello");
        }

        [TestMethod]
        public void DeserializePostToolUseInput_GenericOverload_WithJsonTypeInfo_ShouldDeserializeCorrectly()
        {
            var json = """
                {
                    "session_id": "aaaaaaaa-bbbb-cccc-dddd-eeeeeeeeeeee",
                    "cwd": "D:\\Projects\\TestProject",
                    "hook_event_name": "PostToolUse",
                    "tool_name": "Bash",
                    "tool_input": {
                        "command": "echo hello"
                    },
                    "tool_response": {
                        "stdout": "hello\n",
                        "stderr": "",
                        "interrupted": false,
                        "isImage": false
                    },
                    "tool_use_id": "toolu_01BBBBBBBBBBBBBBBBBBBBBB"
                }
                """;

            var result = ClaudeHooksSerializer.DeserializePostToolUseInput(
                json,
                ClaudeHooksJsonContext.Default.BashPostToolUsePayload);

            result.Should().NotBeNull();
            result!.ToolName.Should().Be("Bash");
            result.ToolInput.Should().NotBeNull();
            result.ToolInput!.Command.Should().Be("echo hello");
            result.ToolResponse.Should().NotBeNull();
            result.ToolResponse!.Stdout.Should().Be("hello\n");
            result.ToolResponse.Interrupted.Should().BeFalse();
        }

        [TestMethod]
        public void SerializePreToolUseOutput_GenericOverload_WithJsonTypeInfo_ShouldSerializeCorrectly()
        {
            var output = new PreToolUseHookOutput<BashToolInput>
            {
                Continue = true,
                HookSpecificOutput = new PreToolUseSpecificOutput<BashToolInput>
                {
                    PermissionDecision = PermissionDecision.Allow,
                    PermissionDecisionReason = "Approved by policy",
                    UpdatedInput = new BashToolInput
                    {
                        Command = "echo safe",
                        Description = "Safe command"
                    }
                }
            };

            var json = ClaudeHooksSerializer.SerializePreToolUseOutput(
                output,
                ClaudeHooksJsonContext.Default.BashPreToolUseHookOutput);

            var doc = JsonDocument.Parse(json);
            doc.RootElement.GetProperty("continue").GetBoolean().Should().BeTrue();
            doc.RootElement.GetProperty("hookSpecificOutput")
                .GetProperty("permissionDecision").GetString().Should().Be("allow");
            doc.RootElement.GetProperty("hookSpecificOutput")
                .GetProperty("updatedInput").GetProperty("command").GetString().Should().Be("echo safe");
        }

        #endregion

        #region Remaining Serialize Methods

        [TestMethod]
        public void SerializeNotificationOutput_ShouldSerializeCorrectly()
        {
            var output = new NotificationHookOutput
            {
                Continue = true
            };

            var json = ClaudeHooksSerializer.SerializeNotificationOutput(output);
            var doc = JsonDocument.Parse(json);

            doc.RootElement.GetProperty("continue").GetBoolean().Should().BeTrue();
        }

        [TestMethod]
        public void SerializePreCompactOutput_ShouldSerializeCorrectly()
        {
            var output = new PreCompactHookOutput
            {
                Continue = true,
                SystemMessage = "Preserve important context"
            };

            var json = ClaudeHooksSerializer.SerializePreCompactOutput(output);
            var doc = JsonDocument.Parse(json);

            doc.RootElement.GetProperty("continue").GetBoolean().Should().BeTrue();
            doc.RootElement.GetProperty("systemMessage").GetString().Should().Be("Preserve important context");
        }

        [TestMethod]
        public void SerializeSessionEndOutput_ShouldSerializeCorrectly()
        {
            var output = new SessionEndHookOutput
            {
                Continue = true
            };

            var json = ClaudeHooksSerializer.SerializeSessionEndOutput(output);
            var doc = JsonDocument.Parse(json);

            doc.RootElement.GetProperty("continue").GetBoolean().Should().BeTrue();
        }

        #endregion

        #region DefaultOptions Tests

        [TestMethod]
        public void DefaultOptions_ShouldReturnConfiguredOptions()
        {
            var options = ClaudeHooksSerializer.DefaultOptions;

            options.Should().NotBeNull();
        }

        #endregion

        #region Malformed Payload Tests

        [TestMethod]
        public void DeserializePreToolUseInput_GenericOverload_WithGarbageInput_ShouldThrowJsonException()
        {
            var json = "this is not valid json at all!!!";

            var action = () => ClaudeHooksSerializer.DeserializePreToolUseInput(
                json,
                ClaudeHooksJsonContext.Default.BashPreToolUsePayload);

            action.Should().Throw<JsonException>();
        }

        [TestMethod]
        public void DeserializePreToolUseInput_GenericOverload_WithNull_ShouldReturnNull()
        {
            var json = "null";

            var result = ClaudeHooksSerializer.DeserializePreToolUseInput(
                json,
                ClaudeHooksJsonContext.Default.BashPreToolUsePayload);

            result.Should().BeNull();
        }

        [TestMethod]
        public void DeserializePreToolUseInput_GenericOverload_WithEmptyObject_ShouldReturnObjectWithDefaults()
        {
            var json = "{}";

            var result = ClaudeHooksSerializer.DeserializePreToolUseInput(
                json,
                ClaudeHooksJsonContext.Default.BashPreToolUsePayload);

            result.Should().NotBeNull();
            result!.ToolName.Should().BeEmpty();
            result.ToolInput.Should().BeNull();
        }

        [TestMethod]
        public void DeserializePostToolUseInput_GenericOverload_WithGarbageInput_ShouldThrowJsonException()
        {
            var json = "this is not valid json at all!!!";

            var action = () => ClaudeHooksSerializer.DeserializePostToolUseInput(
                json,
                ClaudeHooksJsonContext.Default.BashPostToolUsePayload);

            action.Should().Throw<JsonException>();
        }

        [TestMethod]
        public void DeserializePostToolUseInput_GenericOverload_WithNull_ShouldReturnNull()
        {
            var json = "null";

            var result = ClaudeHooksSerializer.DeserializePostToolUseInput(
                json,
                ClaudeHooksJsonContext.Default.BashPostToolUsePayload);

            result.Should().BeNull();
        }

        [TestMethod]
        public void SerializePreToolUseOutput_GenericOverload_WithNullOutput_ShouldSerializeNullString()
        {
            var json = ClaudeHooksSerializer.SerializePreToolUseOutput<PreToolUseHookOutput<BashToolInput>>(
                null!,
                ClaudeHooksJsonContext.Default.BashPreToolUseHookOutput);

            json.Should().Be("null");
        }

        #endregion

    }

}
