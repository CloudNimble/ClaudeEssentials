using CloudNimble.ClaudeEssentials.Hooks;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Responses;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization.Metadata;

namespace CloudNimble.ClaudeEssentials.Tests.Hooks.Tools
{

    /// <summary>
    /// Verifies wire-format conformance for tool inputs and responses against shapes taken from the schema.
    /// </summary>
    /// <remarks>
    /// Every payload is deserialized and reserialized, and the resulting raw JSON is compared to the original. A
    /// misspelled wire name, dropped property, or wrong enum value changes the JSON, which a comparison of
    /// C# property values after a round trip would not detect. All values are synthetic.
    /// </remarks>
    [TestClass]
    public class ToolSchemaConformanceTests
    {

        #region Tool Input Tests

        [TestMethod]
        public void AgentToolInput_ShouldConform() => AssertConforms(Ctx.AgentToolInput, """
            {"description":"Find it","prompt":"Find the file","subagent_type":"Explore","model":"fable","effort":"xhigh","run_in_background":false,"name":"finder","team_name":"t","mode":"auto","isolation":"remote"}
            """);

        [TestMethod]
        public void ArtifactToolInput_ShouldConform() => AssertConforms(Ctx.ArtifactToolInput, """
            {"action":"upload_asset","file_path":"a.html","favicon":"f","icon":"chart","limit":25,"scope":"shared","title":"T","description":"D","label":"L","url":"https://example.test/a","prompt":"P","force":false,"pin":true,"out_dir":"o","file_paths":["a.png"],"asset_id":"abc","after":"n","capabilities":{"db":{}},"contract":"latest"}
            """);

        [TestMethod]
        public void AskUserQuestionToolInput_ShouldConform() => AssertConforms(Ctx.AskUserQuestionToolInput, """
            {"questions":[{"question":"Which?","header":"Pick","options":[{"label":"A","description":"First","preview":"p"},{"label":"B","description":"Second"}],"multiSelect":false}],"answers":{"Which?":"A"},"annotations":{"Which?":{"preview":"p","notes":"n"}},"metadata":{"source":"remember"}}
            """);

        [TestMethod]
        public void ClaudeDesignToolInput_ShouldConform() => AssertConforms(Ctx.ClaudeDesignToolInput, """{"operation":"list","arguments":{}}""");

        [TestMethod]
        public void CronToolInputs_ShouldConform()
        {
            AssertConforms(Ctx.CronCreateToolInput, """{"cron":"*/5 * * * *","prompt":"Check","recurring":false,"durable":true}""");
            AssertConforms(Ctx.CronDeleteToolInput, """{"id":"job1"}""");
            AssertConforms(Ctx.CronListToolInput, "{}");
        }

        [TestMethod]
        public void EmptyToolInputs_ShouldConform()
        {
            AssertConforms(Ctx.EnterPlanModeToolInput, "{}");
            AssertConforms(Ctx.ReadNotificationsToolInput, "{}");
            AssertConforms(Ctx.ShowOnboardingRolePickerToolInput, "{}");
            AssertConforms(Ctx.TaskListToolInput, "{}");
        }

        [TestMethod]
        public void ExitPlanModeToolInput_ShouldPreserveUnmodeledProperties() => AssertConforms(Ctx.ExitPlanModeToolInput, """
            {"allowedPrompts":[{"tool":"Bash","prompt":"run tests"}],"plan":"The plan"}
            """);

        [TestMethod]
        public void GrepToolInput_ShouldConform() => AssertConforms(Ctx.GrepToolInput, """
            {"pattern":"x","path":"p","glob":"*.cs","output_mode":"content","-B":1,"-A":2,"-C":3,"context":4,"-n":true,"-i":true,"-o":true,"type":"cs","head_limit":5,"offset":6,"multiline":true}
            """);

        [TestMethod]
        public void MiscellaneousToolInputs_ShouldConform()
        {
            AssertConforms(Ctx.MonitorToolInput, """{"description":"Watch","timeout_ms":300000,"command":"tail","ws":{"url":"wss://example.test","protocols":["p"]}}""");
            AssertConforms(Ctx.OfferChromeSetupToolInput, """{"reason":"Sign in"}""");
            AssertConforms(Ctx.ProposeGoalToolInput, """{"condition":"Tests pass","ask_user":false}""");
            AssertConforms(Ctx.ProposeSkillsToolInput, """{"proposals":[{"name":"s","kind":"improvement","target":"t","description":"d","evidence":["e"],"skillMd":"m"}]}""");
            AssertConforms(Ctx.PushNotificationToolInput, """{"message":"Done","status":"proactive"}""");
            AssertConforms(Ctx.ReadMcpResourceDirToolInput, """{"server":"s","uri":"u"}""");
            AssertConforms(Ctx.ReadToolInput, """{"file_path":"f","offset":1,"limit":2,"pages":"1-5","allow_large":true}""");
            AssertConforms(Ctx.RefreshMcpToolsToolInput, """{"server":"s"}""");
            AssertConforms(Ctx.RemoteTriggerToolInput, """{"action":"create_webhook_trigger","trigger_id":"t","session_id":"s","cursor":"c","body":{"a":1}}""");
            AssertConforms(Ctx.ScheduleWakeupToolInput, """{"delaySeconds":90.5,"reason":"r","prompt":"p","stop":false,"noop":true}""");
            AssertConforms(Ctx.WebFetchToolInput, """{"url":"u","prompt":"p","offset":10}""");
            AssertConforms(Ctx.WorkflowToolInput, """{"script":"s","name":"n","description":"d","title":"t","args":["a"],"scriptPath":"p","resumeFromRunId":"r"}""");
        }

        [TestMethod]
        public void ProjectsToolInput_ShouldConform() => AssertConforms(Ctx.ProjectsToolInput, """
            {"method":"project_memory_read","path":"p","content":"c","local_path":"l","present_to_user":true,"query":"q","n":5}
            """);

        [TestMethod]
        public void ReportFindingsToolInput_ShouldConform() => AssertConforms(Ctx.ReportFindingsToolInput, """
            {"level":"max","findings":[{"file":"f","line":1,"summary":"s","short_summary":"ss","failure_scenario":"fs","category":"correctness","verdict":"CONFIRMED","outcome":"no_change_needed"}]}
            """);

        [TestMethod]
        public void SendFeedbackToolInput_ShouldConform() => AssertConforms(Ctx.SendFeedbackToolInput, """
            {"type":"missing_capability","title":"T","details":"D","area":"hooks","failure_mode":"overconfidence_and_hallucination","task_category":"code_edit"}
            """);

        [TestMethod]
        public void TaskToolInputs_ShouldConform()
        {
            AssertConforms(Ctx.TaskCreateToolInput, """{"subject":"S","description":"D","activeForm":"A","metadata":{"k":1}}""");
            AssertConforms(Ctx.TaskGetToolInput, """{"taskId":"1"}""");
            AssertConforms(Ctx.TaskUpdateToolInput, """{"taskId":"1","subject":"S","description":"D","activeForm":"A","status":"deleted","addBlocks":["2"],"addBlockedBy":["3"],"owner":"o","metadata":{"k":null}}""");
        }

        #endregion

        #region Agent Response Tests

        [TestMethod]
        public void CompletedAgentResponse_WithDiscriminatorInSchemaPosition_ShouldConform()
        {
            var json = """
                {"agentId":"a","agentType":"Explore","content":[{"type":"text","text":"t","citations":[{"c":1}]}],"resolvedModel":"m","modelsUsed":["m"],"totalToolUseCount":1,"totalDurationMs":2,"totalTokens":3,"usage":{"input_tokens":1,"output_tokens":2,"cache_creation_input_tokens":null,"cache_read_input_tokens":null,"server_tool_use":null,"service_tier":null,"cache_creation":null,"inference_geo":"us","speed":"fast","iterations":[1],"output_tokens_details":{"thinking_tokens":4},"fallback_credit":{"x":1}},"toolsUsed":["Read"],"toolStats":{"readCount":1,"searchCount":2,"bashCount":3,"editFileCount":4,"linesAdded":5,"linesRemoved":6,"otherToolCount":7,"frameCount":8},"status":"completed","prompt":"p","worktreePath":"w","worktreeBranch":"b"}
                """;

            AssertConforms(Ctx.AgentToolResponse, json);
            JsonSerializer.Deserialize(json, Ctx.AgentToolResponse).Should().BeOfType<CompletedAgentToolResponse>();
        }

        [TestMethod]
        public void AsyncLaunchedAgentResponse_ShouldConform() => AssertConforms(Ctx.AgentToolResponse, """
            {"status":"async_launched","isAsync":true,"agentId":"a","description":"d","resolvedModel":"m","modelsUsed":["m"],"prompt":"p","outputFile":"o","canReadOutputFile":true}
            """);

        [TestMethod]
        public void RemoteLaunchedAgentResponse_ShouldConform()
        {
            var json = """{"status":"remote_launched","taskId":"t","sessionUrl":"s","description":"d","prompt":"p","outputFile":"o"}""";

            AssertConforms(Ctx.AgentToolResponse, json);
            JsonSerializer.Deserialize(json, Ctx.AgentToolResponse).Should().BeOfType<RemoteLaunchedAgentToolResponse>();
        }

        [TestMethod]
        public void AgentResponse_WithUnknownStatus_ShouldThrow()
        {
            var act = () => JsonSerializer.Deserialize("""{"status":"unheard_of"}""", Ctx.AgentToolResponse);

            act.Should().Throw<System.Exception>();
        }

        #endregion

        #region File Tool Response Tests

        [TestMethod]
        public void BashToolResponse_ShouldConform() => AssertConforms(Ctx.BashToolResponse, """
            {"stdout":"o","stderr":"e","rawOutputPath":"r","interrupted":false,"isImage":false,"backgroundTaskId":"b","backgroundedByUser":true,"timedOutAfterMs":5,"backgroundCwdHint":"h","backgroundEndsWithFinalResponse":true,"backgroundWakesOwner":true,"dangerouslyDisableSandbox":false,"returnCodeInterpretation":"i","noOutputExpected":true,"structuredContent":[{"a":1}],"persistedOutputPath":"p","persistedOutputSize":9,"staleReadFileStateHint":"s","ghRateLimitHint":"g","gitOperation":{"commit":{"sha":"abc","kind":"cherry-picked","branch":"b"},"push":{"branch":"main"},"branch":{"ref":"r","action":"rebased"},"pr":{"number":1,"url":"u","action":"auto-merge-disabled"}}}
            """);

        [TestMethod]
        public void BashToolResponse_WithoutIsImage_ShouldOmitIt()
        {
            var value = JsonSerializer.Deserialize("""{"stdout":"","stderr":"","interrupted":false}""", Ctx.BashToolResponse);

            JsonSerializer.Serialize(value!, Ctx.BashToolResponse).Should().NotContain("isImage");
        }

        [TestMethod]
        public void EditToolResponse_ShouldConform() => AssertConforms(Ctx.EditToolResponse, """
            {"filePath":"f","oldString":"o","newString":"n","originalFile":null,"structuredPatch":[{"oldStart":1,"oldLines":2,"newStart":3,"newLines":4,"lines":["l"]}],"userModified":false,"replaceAll":true,"gitDiff":{"filename":"f","status":"modified","additions":1,"deletions":2,"changes":3,"patch":"p","repository":"o/r"},"staged":true}
            """);

        [TestMethod]
        public void WriteToolResponse_ShouldConform() => AssertConforms(Ctx.WriteToolResponse, """
            {"type":"create","filePath":"f","content":"c","structuredPatch":[],"originalFile":null,"gitDiff":{"filename":"f","status":"added","additions":1,"deletions":0,"changes":1,"patch":"p"},"userModified":false,"staged":true}
            """);

        [TestMethod]
        public void ExitPlanModeToolResponse_ShouldKeepRequiredNullPlan() => AssertConforms(Ctx.ExitPlanModeToolResponse, """
            {"plan":null,"isAgent":false,"filePath":"f","hasTaskTool":true,"planWasEdited":true,"awaitingLeaderApproval":true,"requestId":"r"}
            """);

        [TestMethod]
        public void GlobAndGrepToolResponses_ShouldConform()
        {
            AssertConforms(Ctx.GlobToolResponse, """{"durationMs":1,"numFiles":2,"filenames":["f"],"truncated":false,"totalMatches":3,"countIsComplete":true}""");
            AssertConforms(Ctx.GrepToolResponse, """{"mode":"count","numFiles":1,"filenames":[],"content":"c","numLines":2,"numMatches":3,"totalFiles":4,"totalLines":5,"appliedLimit":6,"appliedOffset":7}""");
            AssertConforms(Ctx.GrepToolResponse, """{"numFiles":0,"filenames":[]}""");
        }

        [TestMethod]
        public void NotebookEditToolResponse_ShouldConform() => AssertConforms(Ctx.NotebookEditToolResponse, """
            {"new_source":"n","old_source":"o","cell_id":"c","cell_type":"markdown","language":"python","edit_mode":"replace","error":"e","notebook_path":"p","original_file":"of","updated_file":"uf"}
            """);

        [TestMethod]
        public void ReadToolResponses_ShouldConformForEveryVariant()
        {
            AssertConforms(Ctx.ReadToolResponse, """{"type":"text","file":{"filePath":"f","content":"c","numLines":1,"startLine":1,"totalLines":1,"truncatedByTokenCap":true},"artifactRead":{"slug":"s","ver":"v"}}""");
            AssertConforms(Ctx.ReadToolResponse, """{"type":"image","file":{"base64":"b","type":"image/webp","originalSize":1,"dimensions":{"originalWidth":1,"originalHeight":2,"displayWidth":3,"displayHeight":4}}}""");
            AssertConforms(Ctx.ReadToolResponse, """{"type":"notebook","file":{"filePath":"f","cells":[{"a":1}]}}""");
            AssertConforms(Ctx.ReadToolResponse, """{"type":"pdf","file":{"filePath":"f","base64":"b","originalSize":1}}""");
            AssertConforms(Ctx.ReadToolResponse, """{"type":"parts","file":{"filePath":"f","originalSize":1,"count":2,"outputDir":"o"},"firstPage":1,"pages":[{"base64":"","mediaType":"image/png","error":"e"}]}""");
            AssertConforms(Ctx.ReadToolResponse, """{"type":"file_unchanged","file":{"filePath":"f"},"source":"seeded"}""");
        }

        #endregion

        #region MCP Response Tests

        [TestMethod]
        public void McpToolResponse_ShouldConformForEveryShape()
        {
            AssertConforms(Ctx.McpToolResponse, "\"plain text\"");
            AssertConforms(Ctx.McpToolResponse, """[{"type":"text","text":"t"},{"type":"image","data":"d","mimeType":"image/png"}]""");
            AssertConforms(Ctx.McpToolResponse, """{"anything":{"nested":[1,2]}}""");
        }

        [TestMethod]
        public void McpToolResponse_ShouldPopulateOnlyTheMatchingMember()
        {
            JsonSerializer.Deserialize("\"t\"", Ctx.McpToolResponse)!.Text.Should().Be("t");

            var blocks = JsonSerializer.Deserialize("""[{"type":"text","text":"t"}]""", Ctx.McpToolResponse)!;
            blocks.Text.Should().BeNull();
            blocks.StructuredContent.Should().BeNull();
            blocks.ContentBlocks![0].Type.Should().Be("text");
            blocks.ContentBlocks![0].ExtensionData!["text"].GetString().Should().Be("t");

            JsonSerializer.Deserialize("""{"a":1}""", Ctx.McpToolResponse)!.StructuredContent!["a"].GetInt32().Should().Be(1);
        }

        [TestMethod]
        public void McpToolResponse_WithUnsupportedToken_ShouldThrow()
        {
            var act = () => JsonSerializer.Deserialize("42", Ctx.McpToolResponse);

            act.Should().Throw<JsonException>();
        }

        [TestMethod]
        public void McpPostToolUsePayload_ShouldDeserializeAStringResult()
        {
            var payload = ClaudeHooksSerializer.DeserializeMcpPostToolUse("""{"tool_name":"mcp__s__t","tool_input":{"a":1},"tool_response":"done"}""");

            payload!.ToolResponse!.Text.Should().Be("done");
        }

        [TestMethod]
        public void McpResourceToolResponses_ShouldConform()
        {
            AssertConforms(Ctx.ReadMcpResourceToolResponse, """{"contents":[{"uri":"u","mimeType":"m","text":"t","blobSavedTo":"b"}],"error":"e"}""");
            AssertConforms(Ctx.ReadMcpResourceDirToolResponse, """{"resources":[{"uri":"u","name":"n","mimeType":"inode/directory"}],"error":"e"}""");
            AssertConforms(Ctx.ListRefreshMcpToolsServerResult, """[{"server":"s","status":"not_connected","toolCount":1,"added":["a"],"removed":["r"],"error":"e"}]""");
        }

        #endregion

        #region Web Response Tests

        [TestMethod]
        public void WebFetchToolResponse_ShouldConform() => AssertConforms(Ctx.WebFetchToolResponse, """
            {"bytes":1,"code":200,"codeText":"OK","result":"r","durationMs":2,"url":"u","artifactRead":{"slug":"s","ver":"v","seeded":false}}
            """);

        [TestMethod]
        public void WebSearchToolResponse_WithHitsAndCommentary_ShouldConform()
        {
            var json = """
                {"query":"q","results":[{"tool_use_id":"t","content":[{"title":"T","url":"u","snippet":"s"}]},"Some commentary"],"durationSeconds":1.5,"searchCount":1}
                """;

            AssertConforms(Ctx.WebSearchToolResponse, json);

            var value = JsonSerializer.Deserialize(json, Ctx.WebSearchToolResponse)!;
            value.Results[0].Content[0].Snippet.Should().Be("s");
            value.Results[0].Commentary.Should().BeNull();
            value.Results[1].Commentary.Should().Be("Some commentary");
        }

        [TestMethod]
        public void WebSearchToolResponse_WithUnsupportedResultToken_ShouldThrow()
        {
            var act = () => JsonSerializer.Deserialize("""{"query":"q","results":[42],"durationSeconds":1}""", Ctx.WebSearchToolResponse);

            act.Should().Throw<JsonException>();
        }

        #endregion

        #region AskUserQuestion Response Tests

        [TestMethod]
        public void AskUserQuestionToolResponse_ShouldConform() => AssertConforms(Ctx.AskUserQuestionToolResponse, """
            {"questions":[{"question":"Q?","header":"H","options":[{"label":"A","description":"a"},{"label":"B","description":"b"}],"multiSelect":true}],"answers":{"Q?":"A"},"response":"r","annotations":{"Q?":{"preview":"p","notes":"n","unanswered":"replied_in_chat"}},"afkTimeoutMs":60000}
            """);

        [TestMethod]
        public void AskUserQuestionAnnotation_WithUnknownUnansweredValue_ShouldTreatItAsAbsent()
        {
            var value = JsonSerializer.Deserialize("""{"unanswered":"something_new"}""", Ctx.AskUserQuestionAnnotation)!;

            value.Unanswered.Should().BeNull();
            JsonSerializer.Serialize(value, Ctx.AskUserQuestionAnnotation).Should().NotContain("unanswered");
        }

        #endregion

        #region Task, Schedule, and Miscellaneous Response Tests

        [TestMethod]
        public void TaskToolResponses_ShouldConform()
        {
            AssertConforms(Ctx.TaskCreateToolResponse, """{"task":{"id":"1","subject":"s"}}""");
            AssertConforms(Ctx.TaskGetToolResponse, """{"task":{"id":"1","subject":"s","description":"d","status":"in_progress","blocks":["2"],"blockedBy":["3"]}}""");
            AssertConforms(Ctx.TaskGetToolResponse, """{"task":null}""");
            AssertConforms(Ctx.TaskUpdateToolResponse, """{"success":true,"taskId":"1","updatedFields":["status"],"error":"e","statusChange":{"from":"pending","to":"completed"}}""");
            AssertConforms(Ctx.TaskListToolResponse, """{"tasks":[{"id":"1","subject":"s","status":"completed","owner":"o","blockedBy":[]}]}""");
            AssertConforms(Ctx.TaskStopToolResponse, """{"message":"m","task_id":"t","task_type":"local","command":"c"}""");
        }

        [TestMethod]
        public void ScheduleToolResponses_ShouldConform()
        {
            AssertConforms(Ctx.CronCreateToolResponse, """{"id":"1","humanSchedule":"h","recurring":true,"durable":false,"nextFireAt":1790000000000}""");
            AssertConforms(Ctx.CronDeleteToolResponse, """{"id":"1"}""");
            AssertConforms(Ctx.CronListToolResponse, """{"jobs":[{"id":"1","cron":"c","humanSchedule":"h","prompt":"p","recurring":true,"durable":false,"nextFireAt":1790000000000}]}""");
            AssertConforms(Ctx.ScheduleWakeupToolResponse, """{"scheduledFor":1790000000000,"clampedDelaySeconds":60,"wasClamped":true,"stopped":false,"cancelledWakeups":0}""");
            AssertConforms(Ctx.MonitorToolResponse, """{"taskId":"t","timeoutMs":0,"persistent":true}""");
            AssertConforms(Ctx.RemoteTriggerToolResponse, """{"status":200,"json":"{}","summary":"s"}""");
        }

        [TestMethod]
        public void MiscellaneousToolResponses_ShouldConform()
        {
            AssertConforms(Ctx.ClaudeDesignToolResponse, """{"operation":"o","content":[{"a":1}],"isError":true}""");
            AssertConforms(Ctx.EnterPlanModeToolResponse, """{"message":"m"}""");
            AssertConforms(Ctx.OfferChromeSetupToolResponse, """{"outcome":"no_attempt_yet"}""");
            AssertConforms(Ctx.ProposeGoalToolResponse, """{"condition":"c","askUser":false}""");
            AssertConforms(Ctx.ProposeSkillsToolResponse, """{"proposalCount":2}""");
            AssertConforms(Ctx.PushNotificationToolResponse, """{"message":"m","pushSent":true,"localSent":false,"disabledReason":"user_present","sentAt":"2026-01-01T00:00:00Z"}""");
            AssertConforms(Ctx.ReadNotificationsToolResponse, """{"notifications":[{"notification_id":"n","origin":"github_webhook","queued_at":"q","content":"c","arrived_at":"a"}],"remaining":0,"read_at":"r"}""");
            AssertConforms(Ctx.ReportFindingsToolResponse, """{"count":1,"level":"low","findings":[{"file":"f","summary":"s","failure_scenario":"fs","verdict":"PLAUSIBLE","outcome":"fixed"}]}""");
            AssertConforms(Ctx.SendFeedbackToolResponse, """{"success":true,"message":"m"}""");
            AssertConforms(Ctx.ShowOnboardingRolePickerToolResponse, """{"role":"r","dismissed":false}""");
            AssertConforms(Ctx.WorkflowToolResponse, """{"status":"remote_launched","taskId":"t","taskType":"remote_agent","workflowName":"w","runId":"r","summary":"s","transcriptDir":"d","scriptPath":"p","sessionUrl":"u","warning":"x","error":"e"}""");
        }

        #endregion

        #region Projects Response Tests

        [TestMethod]
        public void ProjectsToolResponse_ShouldConformForEveryMethod()
        {
            AssertConforms(Ctx.ProjectsToolResponse, """{"method":"project_info","notice":"n","local_file":"l","name":"N","description":"D","instructions":"I","docs":[{"path":"p","created_at":null}],"files":[{"path":"p","file_kind":"k","created_at":"c"}],"sync_sources":[{"type":null,"config":{"a":1}}],"knowledge":{"knowledge_size":1,"max_knowledge_size":2}}""");
            AssertConforms(Ctx.ProjectsToolResponse, """{"method":"project_read","notice":"n","path":"p","file_kind":"k","content":"c","local_file":"l","created_at":null}""");
            AssertConforms(Ctx.ProjectsToolResponse, """{"method":"project_search","notice":"n","rag":true,"hits":[{"name":"N","doc_uuid":"d","text":"t"}],"docs":["x"]}""");
            AssertConforms(Ctx.ProjectsToolResponse, """{"method":"project_write","notice":"n","path":"p","doc_uuid":"d","replaced":false,"present_to_user":true,"local_path":"l"}""");
            AssertConforms(Ctx.ProjectsToolResponse, """{"method":"project_delete","notice":"n","path":"p","deleted":true}""");
            AssertConforms(Ctx.ProjectsToolResponse, """{"method":"project_memory_list","notice":"n","files":[{"path":"p","size_bytes":1,"updated_at":null,"truncated":false}],"truncated":true}""");
            AssertConforms(Ctx.ProjectsToolResponse, """{"method":"project_memory_read","notice":"n","path":"p","content":"c","local_file":"l","size_bytes":1,"updated_at":null,"truncated":false}""");
        }

        [TestMethod]
        public void ProjectsToolResponse_WithDiscriminatorLast_ShouldSelectTheDerivedType()
        {
            var value = JsonSerializer.Deserialize("""{"path":"p","deleted":true,"method":"project_delete"}""", Ctx.ProjectsToolResponse);

            value.Should().BeOfType<DeleteProjectsToolResponse>().Which.Deleted.Should().BeTrue();
        }

        #endregion

        #region Artifact Response Tests

        [TestMethod]
        public void ArtifactToolResponse_ShouldConformForEveryShape()
        {
            AssertConforms(Ctx.ArtifactToolResponse, """{"url":"u","path":"p","artifact_id":"a","title":"t","version":"v","capabilities":{"db":{}},"stored":{"contract":"c","preferredContract":"pc","capabilities":{"x":1},"carried":true,"read":"r"},"warnings":["w"],"publishesRemaining":1,"publishesResetAt":1790000000,"contract":"c","updated":true,"owned":true,"icon":"i","faviconSent":true,"iconDropped":true,"audience":"au","seq":3,"unchanged":true,"liveSubscription":"l","pinned":false}""");
            AssertConforms(Ctx.ArtifactToolResponse, """{"artifacts":[{"title":"t","url":"u","favicon":"f","updatedAt":"d","rel":"shared","external":true,"role":"commenter","outsidersMayHaveWritten":true,"pinned":false}],"truncated":false,"total":1,"total_at_least":true,"pins_enabled":true,"scope":"all","external_listed":true}""");
            AssertConforms(Ctx.ArtifactToolResponse, """{"read":{"url":"u","bytes":1,"code":200,"codeText":"OK","result":"r","durationMs":2,"title":"t"},"owned":true,"artifactRead":{"slug":"s","ver":"v","seeded":false}}""");
            AssertConforms(Ctx.ArtifactToolResponse, """{"watch":{"url":"u","watching":true,"outcome":"o","reason":"r","durable_skip_reason":"d","task_id":"t","since":1,"token_expires_at":2,"rail":"ra","trigger_id":"ti","durable_since":"ds","status":200,"detail":"de","note":"n","events":["e"]}}""");
            AssertConforms(Ctx.ArtifactToolResponse, """{"unwatch":{"url":"u","was_watching":true}}""");
            AssertConforms(Ctx.ArtifactToolResponse, """{"pin":{"action":"unpin","url":"u","pinned":false,"title":"t"}}""");
        }

        [TestMethod]
        public void ArtifactToolResponse_ShouldConformForEveryWatchRail()
        {
            var json = """
                {"watches":[{"url":"u","task_id":"t","since":1,"explicit":true,"connected":true,"connecting":false,"token_expires_at":2,"armed_via":"a"},{"url":"u","rail":"durable_wake","trigger_id":"ti","since":"2026-01-01T00:00:00Z","events":["e"],"restored":true},{"url":"u","rail":"live_stopped","since":1,"explicit":false,"armed_via":"a","stop_kind":"k"}],"filter_url":"f","arms":[{"url":"u","rail":"r","state":"s","reconnect":true,"failures":1,"max_failures":2,"next_in_s":1.5,"last_failure":"l","reason":"re","detail":"d","server_message":"sm","at":3}]}
                """;

            AssertConforms(Ctx.ArtifactToolResponse, json);

            var watches = JsonSerializer.Deserialize(json, Ctx.ArtifactToolResponse)!.Watches!;
            watches[0].Rail.Should().BeNull();
            watches[0].Since!.Value.GetInt64().Should().Be(1);
            watches[1].Rail.Should().Be(ArtifactWatchRail.DurableWake);
            watches[1].Since!.Value.GetString().Should().Be("2026-01-01T00:00:00Z");
        }

        [TestMethod]
        public void ArtifactToolResponse_ShouldConformForEveryAssetShape()
        {
            AssertConforms(Ctx.ArtifactToolResponse, """{"written":{"url":"u"},"owned":true,"asset_upload":{"id":"i","url":"u","size_bytes":1,"content_type":"c","sha256":"s","file_name":"f"}}""");
            AssertConforms(Ctx.ArtifactToolResponse, """{"written":{"url":"u"},"owned":true,"asset_uploads":{"url":"u","results":[{"file_path":"a","status":"uploaded","id":"i","url":"u","size_bytes":1,"content_type":"c","sha256":"s","file_name":"f"},{"file_path":"b","status":"not_attempted","reason":"r","message":"m","may_be_stored":true}]}}""");
            AssertConforms(Ctx.ArtifactToolResponse, """{"asset_list":{"url":"u","assets":[{"id":"i","url":"u","content_type":"c","size_bytes":1,"sha256":"s","created_at":"d"}],"usage":{"files":1,"bytes":2,"max_files":3,"max_bytes":4},"next":"n","cowritten":true,"outside_writer":true}}""");
            AssertConforms(Ctx.ArtifactToolResponse, """{"asset_read":{"id":"i","path":"p","size_bytes":1,"content_type":"c","sha256":"s","cowritten":true,"outside_writer":true,"public_read":true,"foreign":true}}""");
            AssertConforms(Ctx.ArtifactToolResponse, """{"written":{"url":"u"},"owned":true,"asset_delete":{"id":"i","deleted":true}}""");
        }

        #endregion

        #region Private Members

        private static ClaudeHooksJsonContext Ctx => ClaudeHooksJsonContext.Default;

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
