using CloudNimble.ClaudeEssentials.Hooks;
using CloudNimble.ClaudeEssentials.Hooks.Inputs;
using CloudNimble.ClaudeEssentials.Hooks.Outputs;
using CloudNimble.ClaudeEssentials.Hooks.Permissions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization.Metadata;

namespace CloudNimble.ClaudeEssentials.Tests.Hooks
{

    /// <summary>
    /// Verifies wire-format conformance for hook inputs and outputs against shapes taken from the Agent SDK schema.
    /// </summary>
    /// <remarks>
    /// Every payload is deserialized and reserialized, and the resulting raw JSON is compared to the original. A
    /// misspelled wire name, dropped property, or wrong enum value changes the JSON, which a comparison of
    /// C# property values after a round trip would not detect. All values are synthetic.
    /// </remarks>
    [TestClass]
    public class HookSchemaConformanceTests
    {

        #region Input Tests

        [TestMethod]
        public void HookInputBase_ShouldConformWithEveryCommonField() => AssertConforms(Ctx.SetupHookInput, """
            {"session_id":"s","transcript_path":"t","cwd":"c","prompt_id":"p","permission_mode":"dontAsk","agent_id":"a","agent_type":"Explore","effort":{"level":"xhigh"},"hook_event_name":"Setup","trigger":"maintenance"}
            """);

        [TestMethod]
        public void HookInputBase_WithoutPermissionMode_ShouldOmitIt()
        {
            var value = JsonSerializer.Deserialize(Input("Setup", """ "trigger":"init" """), Ctx.SetupHookInput)!;

            value.PermissionMode.Should().BeNull();
            JsonSerializer.Serialize(value, Ctx.SetupHookInput).Should().NotContain("permission_mode");
        }

        [TestMethod]
        public void ToolHookInputs_ShouldConform()
        {
            const string Tool = """ "tool_name":"Bash","tool_input":{"command":"ls"},"mcp_server":{"name":"n","source":"plugin"} """;

            AssertConforms(Ctx.PreToolUseHookInputObject, Input("PreToolUse", Tool + """,  "tool_use_id":"t" """));
            AssertConforms(Ctx.PostToolUseHookInputObject, Input("PostToolUse", Tool + """, "tool_use_id":"t","tool_response":{"a":1},"duration_ms":5 """));
            AssertConforms(Ctx.PostToolUseFailureHookInput, Input("PostToolUseFailure", Tool + """, "tool_use_id":"t","error":"e","is_interrupt":true,"duration_ms":5 """));
            AssertConforms(Ctx.PermissionDeniedHookInput, Input("PermissionDenied", Tool + """, "tool_use_id":"t","reason":"r" """));
        }

        [TestMethod]
        public void PermissionRequestHookInput_ShouldConformForEveryPermissionUpdate() => AssertConforms(Ctx.PermissionRequestHookInputObject, Input("PermissionRequest", """
            "tool_name":"Bash","tool_input":{"command":"ls"},"tool_use_id":"t","mcp_server":{"name":"n","source":"user"},"permission_suggestions":[
            {"type":"addRules","rules":[{"toolName":"Bash","ruleContent":"git *"}],"behavior":"allow","destination":"userSettings"},
            {"type":"replaceRules","rules":[{"toolName":"Read"}],"behavior":"deny","destination":"projectSettings"},
            {"type":"removeRules","rules":[{"toolName":"Edit"}],"behavior":"ask","destination":"localSettings"},
            {"type":"setMode","mode":"dontAsk","destination":"session"},
            {"type":"addDirectories","directories":["d"],"destination":"cliArg"},
            {"type":"removeDirectories","directories":["d"],"destination":"session"}]
            """));

        [TestMethod]
        public void PermissionUpdate_ShouldSelectTheDerivedTypeFromItsDiscriminator()
        {
            var value = JsonSerializer.Deserialize("""{"destination":"session","mode":"plan","type":"setMode"}""", Ctx.PermissionUpdate);

            value.Should().BeOfType<SetModePermissionUpdate>().Which.Mode.Should().Be(PermissionMode.Plan);
        }

        [TestMethod]
        public void LifecycleHookInputs_ShouldConform()
        {
            AssertConforms(Ctx.SessionStartHookInput, Input("SessionStart", """ "source":"fork","agent_type":"a","model":"m","session_title":"t","seconds_since_last_response":12.5,"context_tokens":100,"prompt_cache_likely_expired":true,"estimated_cache_write_usd":0.25 """));
            AssertConforms(Ctx.SessionEndHookInput, Input("SessionEnd", """ "reason":"prompt_input_exit" """));
            AssertConforms(Ctx.SetupHookInput, Input("Setup", """ "trigger":"init" """));
            AssertConforms(Ctx.ConfigChangeHookInput, Input("ConfigChange", """ "source":"skills","file_path":"f" """));
            AssertConforms(Ctx.InstructionsLoadedHookInput, Input("InstructionsLoaded", """ "file_path":"f","memory_type":"Managed","load_reason":"path_glob_match","globs":["g"],"trigger_file_path":"t","parent_file_path":"p" """));
            AssertConforms(Ctx.CwdChangedHookInput, Input("CwdChanged", """ "old_cwd":"o","new_cwd":"n" """));
            AssertConforms(Ctx.FileChangedHookInput, Input("FileChanged", """ "file_path":"f","event":"unlink" """));
            AssertConforms(Ctx.DirectoryAddedHookInput, Input("DirectoryAdded", """ "directory":"d","source":"register_repo_root" """));
            AssertConforms(Ctx.WorktreeCreateHookInput, Input("WorktreeCreate", """ "name":"n" """));
            AssertConforms(Ctx.WorktreeRemoveHookInput, Input("WorktreeRemove", """ "worktree_path":"p" """));
        }

        [TestMethod]
        public void PromptAndMessageHookInputs_ShouldConform()
        {
            AssertConforms(Ctx.UserPromptSubmitHookInput, Input("UserPromptSubmit", """ "prompt":"p","source":"loop_wakeup","session_title":"t" """));
            AssertConforms(Ctx.UserPromptExpansionHookInput, Input("UserPromptExpansion", """ "expansion_type":"mcp_prompt","command_name":"n","command_args":"a","command_source":"s","prompt":"p" """));
            AssertConforms(Ctx.NotificationHookInput, Input("Notification", """ "message":"m","title":"t","notification_type":"a_type_added_later" """));
            AssertConforms(Ctx.MessageDisplayHookInput, Input("MessageDisplay", """ "turn_id":"t","message_id":"m","index":0,"final":true,"delta":"" """));
            AssertConforms(Ctx.PostToolBatchHookInput, Input("PostToolBatch", """ "tool_calls":[{"tool_name":"Read","tool_input":{"a":1},"tool_use_id":"t","tool_response":"r"},{"tool_name":"Bash","tool_input":{},"tool_use_id":"u"}] """));
        }

        [TestMethod]
        public void StopHookInputs_ShouldConform()
        {
            const string Work = """ "last_assistant_message":"m","background_tasks":[{"id":"1","type":"shell","status":"running","description":"d","command":"c","agent_type":"a","server":"s","tool":"t","name":"n"}],"session_crons":[{"id":"1","schedule":"0 9 * * *","recurring":true,"prompt":"p"}] """;

            AssertConforms(Ctx.StopHookInput, Input("Stop", """ "stop_hook_active":false, """ + Work));
            AssertConforms(Ctx.SubagentStopHookInput, Input("SubagentStop", """ "stop_hook_active":true,"agent_id":"a","agent_transcript_path":"p","agent_type":"t", """ + Work));
            AssertConforms(Ctx.SubagentStartHookInput, Input("SubagentStart", """ "agent_id":"a","agent_type":"t" """));
            AssertConforms(Ctx.StopFailureHookInput, Input("StopFailure", """ "error":"cloud_credential_error","error_details":"d","last_assistant_message":"m" """));
        }

        [TestMethod]
        public void CompactionHookInputs_ShouldConform()
        {
            AssertConforms(Ctx.PreCompactHookInput, Input("PreCompact", """ "trigger":"manual","custom_instructions":null """));
            AssertConforms(Ctx.PostCompactHookInput, Input("PostCompact", """ "trigger":"auto","compact_summary":"s" """));
        }

        [TestMethod]
        public void ModelSwitchHookInputs_ShouldConform()
        {
            const string Switch = """ "from_model":"a","to_model":"b","requested_model":null,"context_tokens":100,"prompt_cache_warm":true,"estimated_cache_write_usd":0.5 """;

            AssertConforms(Ctx.PreModelSwitchHookInput, Input("PreModelSwitch", Switch + """, "source":"picker","cache_ttl":"5m","pricing":"default" """));
            AssertConforms(Ctx.PostModelSwitchHookInput, Input("PostModelSwitch", Switch + """, "source":"resume","cache_ttl":"1h","pricing":"configured" """));
        }

        [TestMethod]
        public void ElicitationHookInputs_ShouldConform()
        {
            AssertConforms(Ctx.ElicitationHookInput, Input("Elicitation", """ "mcp_server_name":"s","message":"m","mode":"url","url":"u","elicitation_id":"e","requested_schema":{"type":"object"} """));
            AssertConforms(Ctx.ElicitationResultHookInput, Input("ElicitationResult", """ "mcp_server_name":"s","elicitation_id":"e","mode":"form","action":"decline","content":{"a":1} """));
        }

        [TestMethod]
        public void TeamHookInputs_ShouldConform()
        {
            const string Task = """ "task_id":"1","task_subject":"s","task_description":"d","teammate_name":"t","team_name":"n" """;

            AssertConforms(Ctx.TaskCreatedHookInput, Input("TaskCreated", Task));
            AssertConforms(Ctx.TaskCompletedHookInput, Input("TaskCompleted", Task));
            AssertConforms(Ctx.TeammateIdleHookInput, Input("TeammateIdle", """ "teammate_name":"t","team_name":"n" """));
        }

        #endregion

        #region Output Tests

        [TestMethod]
        public void HookOutputBase_ShouldConformWithEveryCommonField() => AssertConforms(Ctx.StopHookOutput, """
            {"continue":false,"suppressOutput":true,"stopReason":"r","decision":"approve","systemMessage":"m","terminalSequence":"\u0007","reason":"why"}
            """);

        [TestMethod]
        public void HookDecision_ShouldWriteTheSchemaValues()
        {
            JsonSerializer.Serialize(new StopHookOutput { Decision = HookDecision.Approve }, Ctx.StopHookOutput).Should().Contain("\"decision\":\"approve\"");
            JsonSerializer.Serialize(new StopHookOutput { Decision = HookDecision.Block }, Ctx.StopHookOutput).Should().Contain("\"decision\":\"block\"");
        }

        [TestMethod]
        public void AsyncHookOutput_ShouldWriteTrueByDefault()
        {
            var json = ClaudeHooksSerializer.SerializeAsyncOutput(new AsyncHookOutput { AsyncTimeout = 30 });

            JsonNode.DeepEquals(JsonNode.Parse(json), JsonNode.Parse("""{"async":true,"asyncTimeout":30}""")).Should().BeTrue(json);
        }

        [TestMethod]
        public void ToolSpecificOutputs_ShouldConform()
        {
            AssertConforms(Ctx.PreToolUseHookOutputObject, Output("PreToolUse", """ "permissionDecision":"defer","permissionDecisionReason":"r","updatedInput":{"a":1},"additionalContext":"c" """));
            AssertConforms(Ctx.PostToolUseHookOutput, Output("PostToolUse", """ "additionalContext":"c","classifierContext":"k","updatedToolOutput":{"a":1},"updatedMCPToolOutput":"s" """));
            AssertConforms(Ctx.PostToolUseFailureHookOutput, Output("PostToolUseFailure", """ "additionalContext":"c" """));
            AssertConforms(Ctx.PostToolBatchHookOutput, Output("PostToolBatch", """ "additionalContext":"c" """));
            AssertConforms(Ctx.PermissionDeniedHookOutput, Output("PermissionDenied", """ "retry":true """));
        }

        [TestMethod]
        public void PermissionRequestSpecificOutput_ShouldConformForBothBehaviors()
        {
            AssertConforms(Ctx.PermissionRequestHookOutputObject, Output("PermissionRequest", """ "decision":{"behavior":"allow","updatedInput":{"a":1},"updatedPermissions":[{"type":"setMode","mode":"plan","destination":"session"}]} """));
            AssertConforms(Ctx.PermissionRequestHookOutputObject, Output("PermissionRequest", """ "decision":{"behavior":"deny","message":"m","interrupt":false} """));
            AssertConforms(Ctx.PermissionRequestHookOutputObject, Output("PermissionRequest", """ "decision":{"behavior":"allow"} """));
        }

        [TestMethod]
        public void SessionSpecificOutputs_ShouldConform()
        {
            AssertConforms(Ctx.SessionStartHookOutput, Output("SessionStart", """ "additionalContext":"c","initialUserMessage":"m","sessionTitle":"t","watchPaths":["p"],"reloadSkills":true """));
            AssertConforms(Ctx.SetupHookOutput, Output("Setup", """ "additionalContext":"c" """));
            AssertConforms(Ctx.NotificationHookOutput, Output("Notification", """ "additionalContext":"c" """));
            AssertConforms(Ctx.CwdChangedHookOutput, Output("CwdChanged", """ "watchPaths":["p"] """));
            AssertConforms(Ctx.FileChangedHookOutput, Output("FileChanged", """ "watchPaths":["p"] """));
            AssertConforms(Ctx.WorktreeCreateHookOutput, Output("WorktreeCreate", """ "worktreePath":"p" """));
            AssertConforms(Ctx.MessageDisplayHookOutput, Output("MessageDisplay", """ "displayContent":"d" """));
        }

        [TestMethod]
        public void PromptAndStopSpecificOutputs_ShouldConform()
        {
            AssertConforms(Ctx.UserPromptSubmitHookOutput, Output("UserPromptSubmit", """ "additionalContext":"c","sessionTitle":"t","suppressOriginalPrompt":true """));
            AssertConforms(Ctx.UserPromptExpansionHookOutput, Output("UserPromptExpansion", """ "additionalContext":"c","suppressOriginalPrompt":false """));
            AssertConforms(Ctx.StopHookOutput, Output("Stop", """ "additionalContext":"c" """));
            AssertConforms(Ctx.SubagentStartHookOutput, Output("SubagentStart", """ "additionalContext":"c" """));
            AssertConforms(Ctx.SubagentStopHookOutput, Output("SubagentStop", """ "additionalContext":"c" """));
        }

        [TestMethod]
        public void ModelSwitchSpecificOutputs_ShouldConform()
        {
            AssertConforms(Ctx.PreModelSwitchHookOutput, Output("PreModelSwitch", """ "permissionDecision":"ask","permissionDecisionReason":"r" """));
            AssertConforms(Ctx.PostModelSwitchHookOutput, Output("PostModelSwitch", """ "additionalContext":"c" """));
        }

        [TestMethod]
        public void ElicitationSpecificOutputs_ShouldConform()
        {
            AssertConforms(Ctx.ElicitationHookOutput, Output("Elicitation", """ "action":"accept","content":{"a":1} """));
            AssertConforms(Ctx.ElicitationHookOutput, Output("Elicitation", ""));
            AssertConforms(Ctx.ElicitationResultHookOutput, Output("ElicitationResult", """ "action":"cancel" """));
        }

        [TestMethod]
        public void OutputsWithoutSpecificData_ShouldConform()
        {
            const string Plain = """{"continue":true}""";

            AssertConforms(Ctx.DirectoryAddedHookOutput, Plain);
            AssertConforms(Ctx.ConfigChangeHookOutput, Plain);
            AssertConforms(Ctx.TeammateIdleHookOutput, Plain);
        }

        #endregion

        #region Malformed Payload Tests

        [TestMethod]
        public void HookInput_WithUnknownEnumValue_ShouldThrow()
        {
            var act = () => JsonSerializer.Deserialize(Input("Setup", """ "trigger":"something_new" """), Ctx.SetupHookInput);

            act.Should().Throw<JsonException>();
        }

        [TestMethod]
        public void PermissionUpdate_WithUnknownType_ShouldThrow()
        {
            var act = () => JsonSerializer.Deserialize("""{"type":"somethingNew"}""", Ctx.PermissionUpdate);

            act.Should().Throw<System.Exception>();
        }

        #endregion

        #region Private Members

        private static ClaudeHooksJsonContext Ctx => ClaudeHooksJsonContext.Default;

        private static string Input(string hookEvent, string body) => """{"session_id":"s","transcript_path":"t","cwd":"c","hook_event_name":""" + "\"" + hookEvent + "\"," + body + "}";

        private static string Output(string hookEvent, string body)
        {
            var separator = body.Length > 0 ? "," : "";
            return """{"continue":true,"hookSpecificOutput":{"hookEventName":""" + "\"" + hookEvent + "\"" + separator + body + "}}";
        }

        private static void AssertConforms<T>(JsonTypeInfo<T> typeInfo, string json)
        {
            var value = JsonSerializer.Deserialize(json, typeInfo);
            value.Should().NotBeNull();

            var output = JsonSerializer.Serialize(value!, typeInfo);

            JsonNode.DeepEquals(JsonNode.Parse(json), JsonNode.Parse(output)).Should().BeTrue($"the JSON should survive unchanged.\nExpected: {json}\nActual:   {output}");
        }

        #endregion

    }

}
