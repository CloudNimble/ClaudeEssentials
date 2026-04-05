using System;
using CloudNimble.ClaudeEssentials.Hooks;
using CloudNimble.ClaudeEssentials.Hooks.Outputs;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Responses;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.Json;

namespace CloudNimble.ClaudeEssentials.Tests
{

    /// <summary>
    /// Tests for named convenience methods on <see cref="ClaudeHooksSerializer"/>.
    /// Each test uses realistic JSON payloads matching real Claude Code hook structures with PII removed.
    /// </summary>
    [TestClass]
    public class NamedSerializerMethodTests
    {

        #region Fields

        private const string PreToolUseWrapper = """
            {{
                "session_id": "aaaaaaaa-bbbb-cccc-dddd-eeeeeeeeeeee",
                "cwd": "D:\\Projects\\TestProject",
                "permission_mode": "default",
                "hook_event_name": "PreToolUse",
                "tool_name": "{0}",
                "tool_input": {1},
                "tool_use_id": "toolu_01AAAAAAAAAAAAAAAAAAAAAA"
            }}
            """;

        private const string PostToolUseWrapper = """
            {{
                "session_id": "aaaaaaaa-bbbb-cccc-dddd-eeeeeeeeeeee",
                "cwd": "D:\\Projects\\TestProject",
                "permission_mode": "default",
                "hook_event_name": "PostToolUse",
                "tool_name": "{0}",
                "tool_input": {1},
                "tool_response": {2},
                "tool_use_id": "toolu_01BBBBBBBBBBBBBBBBBBBBBB"
            }}
            """;

        #endregion

        #region PreToolUse Deserialization Tests

        [TestMethod]
        public void DeserializeAgentPreToolUse_ShouldReturnStronglyTypedPayload()
        {
            var toolInput = """{ "description": "Search codebase", "prompt": "Find all API endpoints", "subagent_type": "Explore" }""";
            var json = string.Format(PreToolUseWrapper, "Agent", toolInput);

            var result = ClaudeHooksSerializer.DeserializeAgentPreToolUse(json);

            result.Should().NotBeNull();
            result!.ToolName.Should().Be("Agent");
            result.ToolInput.Should().NotBeNull();
            result.ToolInput!.Description.Should().Be("Search codebase");
            result.ToolInput.Prompt.Should().Be("Find all API endpoints");
            result.ToolInput.SubagentType.Should().Be("Explore");
        }

        [TestMethod]
        public void DeserializeAskUserQuestionPreToolUse_ShouldReturnStronglyTypedPayload()
        {
            var toolInput = """{ "questions": [{ "question": "Which approach?", "header": "Design choice", "options": [{ "label": "Option A", "description": "First approach" }], "multiSelect": false }] }""";
            var json = string.Format(PreToolUseWrapper, "AskUserQuestion", toolInput);

            var result = ClaudeHooksSerializer.DeserializeAskUserQuestionPreToolUse(json);

            result.Should().NotBeNull();
            result!.ToolInput.Should().NotBeNull();
            result.ToolInput!.Questions.Should().HaveCount(1);
            result.ToolInput.Questions[0].Question.Should().Be("Which approach?");
            result.ToolInput.Questions[0].Header.Should().Be("Design choice");
            result.ToolInput.Questions[0].Options.Should().HaveCount(1);
            result.ToolInput.Questions[0].MultiSelect.Should().BeFalse();
        }

        [TestMethod]
        public void DeserializeBashPreToolUse_ShouldReturnStronglyTypedPayload()
        {
            var toolInput = """{ "command": "dotnet build -c Release", "description": "Build the project", "timeout": 120000 }""";
            var json = string.Format(PreToolUseWrapper, "Bash", toolInput);

            var result = ClaudeHooksSerializer.DeserializeBashPreToolUse(json);

            result.Should().NotBeNull();
            result!.ToolName.Should().Be("Bash");
            result.ToolInput.Should().NotBeNull();
            result.ToolInput!.Command.Should().Be("dotnet build -c Release");
            result.ToolInput.Description.Should().Be("Build the project");
            result.ToolInput.Timeout.Should().Be(120000);
        }

        [TestMethod]
        public void DeserializeConfigPreToolUse_ShouldReturnStronglyTypedPayload()
        {
            var toolInput = """{ "setting": "theme", "value": "dark" }""";
            var json = string.Format(PreToolUseWrapper, "Config", toolInput);

            var result = ClaudeHooksSerializer.DeserializeConfigPreToolUse(json);

            result.Should().NotBeNull();
            result!.ToolInput.Should().NotBeNull();
            result.ToolInput!.Setting.Should().Be("theme");
            result.ToolInput!.Value.Should().NotBeNull();
            result.ToolInput!.Value!.Value.GetString().Should().Be("dark");
        }

        [TestMethod]
        public void DeserializeEditPreToolUse_ShouldReturnStronglyTypedPayload()
        {
            var toolInput = """{ "file_path": "D:\\Projects\\TestProject\\src\\Program.cs", "old_string": "Console.WriteLine", "new_string": "Debug.WriteLine" }""";
            var json = string.Format(PreToolUseWrapper, "Edit", toolInput);

            var result = ClaudeHooksSerializer.DeserializeEditPreToolUse(json);

            result.Should().NotBeNull();
            result!.ToolInput.Should().NotBeNull();
            result.ToolInput!.FilePath.Should().Contain("Program.cs");
            result.ToolInput.OldString.Should().Be("Console.WriteLine");
            result.ToolInput.NewString.Should().Be("Debug.WriteLine");
        }

        [TestMethod]
        public void DeserializeEnterWorktreePreToolUse_ShouldReturnStronglyTypedPayload()
        {
            var toolInput = """{ "name": "feature-branch" }""";
            var json = string.Format(PreToolUseWrapper, "EnterWorktree", toolInput);

            var result = ClaudeHooksSerializer.DeserializeEnterWorktreePreToolUse(json);

            result.Should().NotBeNull();
            result!.ToolInput.Should().NotBeNull();
            result.ToolInput!.Name.Should().Be("feature-branch");
        }

        [TestMethod]
        public void DeserializeExitPlanModePreToolUse_ShouldReturnStronglyTypedPayload()
        {
            var toolInput = """{ "allowedPrompts": [{ "tool": "Bash", "prompt": "run tests" }] }""";
            var json = string.Format(PreToolUseWrapper, "ExitPlanMode", toolInput);

            var result = ClaudeHooksSerializer.DeserializeExitPlanModePreToolUse(json);

            result.Should().NotBeNull();
            result!.ToolInput.Should().NotBeNull();
            result.ToolInput!.AllowedPrompts.Should().HaveCount(1);
            result.ToolInput!.AllowedPrompts![0].Tool.Should().Be("Bash");
            result.ToolInput!.AllowedPrompts![0].Prompt.Should().Be("run tests");
        }

        [TestMethod]
        public void DeserializeExitWorktreePreToolUse_ShouldReturnStronglyTypedPayload()
        {
            var toolInput = """{ "action": "keep" }""";
            var json = string.Format(PreToolUseWrapper, "ExitWorktree", toolInput);

            var result = ClaudeHooksSerializer.DeserializeExitWorktreePreToolUse(json);

            result.Should().NotBeNull();
            result!.ToolInput.Should().NotBeNull();
            result.ToolInput!.Action.Should().Be(WorktreeAction.Keep);
        }

        [TestMethod]
        public void DeserializeGlobPreToolUse_ShouldReturnStronglyTypedPayload()
        {
            var toolInput = """{ "pattern": "**/*.cs", "path": "D:\\Projects\\TestProject\\src" }""";
            var json = string.Format(PreToolUseWrapper, "Glob", toolInput);

            var result = ClaudeHooksSerializer.DeserializeGlobPreToolUse(json);

            result.Should().NotBeNull();
            result!.ToolInput.Should().NotBeNull();
            result.ToolInput!.Pattern.Should().Be("**/*.cs");
            result.ToolInput.Path.Should().Contain("src");
        }

        [TestMethod]
        public void DeserializeGrepPreToolUse_ShouldReturnStronglyTypedPayload()
        {
            var toolInput = """{ "pattern": "TODO", "path": "D:\\Projects\\TestProject", "output_mode": "content", "-i": true }""";
            var json = string.Format(PreToolUseWrapper, "Grep", toolInput);

            var result = ClaudeHooksSerializer.DeserializeGrepPreToolUse(json);

            result.Should().NotBeNull();
            result!.ToolInput.Should().NotBeNull();
            result.ToolInput!.Pattern.Should().Be("TODO");
            result.ToolInput.OutputMode.Should().Be(GrepOutputMode.Content);
            result.ToolInput.CaseInsensitive.Should().BeTrue();
        }

        [TestMethod]
        public void DeserializeKillShellPreToolUse_ShouldReturnStronglyTypedPayload()
        {
            var toolInput = """{ "shell_id": "shell-abc-123" }""";
            var json = string.Format(PreToolUseWrapper, "KillShell", toolInput);

            var result = ClaudeHooksSerializer.DeserializeKillShellPreToolUse(json);

            result.Should().NotBeNull();
            result!.ToolInput.Should().NotBeNull();
            result.ToolInput!.ShellId.Should().Be("shell-abc-123");
        }

        [TestMethod]
        public void DeserializeListMcpResourcesPreToolUse_ShouldReturnStronglyTypedPayload()
        {
            var toolInput = """{ "server": "my-mcp-server" }""";
            var json = string.Format(PreToolUseWrapper, "ListMcpResources", toolInput);

            var result = ClaudeHooksSerializer.DeserializeListMcpResourcesPreToolUse(json);

            result.Should().NotBeNull();
            result!.ToolInput.Should().NotBeNull();
            result.ToolInput!.Server.Should().Be("my-mcp-server");
        }

        [TestMethod]
        public void DeserializeMcpPreToolUse_ShouldReturnStronglyTypedPayload()
        {
            var toolInput = """{ "server": "docs-server", "query": "authentication" }""";
            var json = string.Format(PreToolUseWrapper, "mcp__docs-server__search", toolInput);

            var result = ClaudeHooksSerializer.DeserializeMcpPreToolUse(json);

            result.Should().NotBeNull();
            result!.ToolInput.Should().NotBeNull();
            result.ToolInput!.ExtensionData.Should().ContainKey("server");
            result.ToolInput.ExtensionData!["query"].GetString().Should().Be("authentication");
        }

        [TestMethod]
        public void DeserializeNotebookEditPreToolUse_ShouldReturnStronglyTypedPayload()
        {
            var toolInput = """{ "notebook_path": "D:\\Projects\\analysis.ipynb", "new_source": "print('hello')", "cell_type": "code" }""";
            var json = string.Format(PreToolUseWrapper, "NotebookEdit", toolInput);

            var result = ClaudeHooksSerializer.DeserializeNotebookEditPreToolUse(json);

            result.Should().NotBeNull();
            result!.ToolInput.Should().NotBeNull();
            result.ToolInput!.NotebookPath.Should().Contain("analysis.ipynb");
            result.ToolInput.NewSource.Should().Be("print('hello')");
        }

        [TestMethod]
        public void DeserializeReadPreToolUse_ShouldReturnStronglyTypedPayload()
        {
            var toolInput = """{ "file_path": "D:\\Projects\\TestProject\\README.md", "offset": 0, "limit": 100 }""";
            var json = string.Format(PreToolUseWrapper, "Read", toolInput);

            var result = ClaudeHooksSerializer.DeserializeReadPreToolUse(json);

            result.Should().NotBeNull();
            result!.ToolInput.Should().NotBeNull();
            result.ToolInput!.FilePath.Should().Contain("README.md");
            result.ToolInput.Offset.Should().Be(0);
            result.ToolInput.Limit.Should().Be(100);
        }

        [TestMethod]
        public void DeserializeReadMcpResourcePreToolUse_ShouldReturnStronglyTypedPayload()
        {
            var toolInput = """{ "server": "my-mcp-server", "uri": "resource://config/settings" }""";
            var json = string.Format(PreToolUseWrapper, "ReadMcpResource", toolInput);

            var result = ClaudeHooksSerializer.DeserializeReadMcpResourcePreToolUse(json);

            result.Should().NotBeNull();
            result!.ToolInput.Should().NotBeNull();
            result.ToolInput!.Server.Should().Be("my-mcp-server");
            result.ToolInput.Uri.Should().Be("resource://config/settings");
        }

        [TestMethod]
        public void DeserializeTaskPreToolUse_ShouldReturnStronglyTypedPayload()
        {
            var toolInput = """{ "description": "Run tests", "prompt": "Execute the test suite", "subagent_type": "general-purpose" }""";
            var json = string.Format(PreToolUseWrapper, "Task", toolInput);

            var result = ClaudeHooksSerializer.DeserializeTaskPreToolUse(json);

            result.Should().NotBeNull();
            result!.ToolInput.Should().NotBeNull();
            result.ToolInput!.Description.Should().Be("Run tests");
            result.ToolInput.Prompt.Should().Be("Execute the test suite");
            result.ToolInput.SubagentType.Should().Be("general-purpose");
        }

        [TestMethod]
        public void DeserializeTaskOutputPreToolUse_ShouldReturnStronglyTypedPayload()
        {
            var toolInput = """{ "task_id": "task-001", "block": true, "timeout": 30000 }""";
            var json = string.Format(PreToolUseWrapper, "TaskOutput", toolInput);

            var result = ClaudeHooksSerializer.DeserializeTaskOutputPreToolUse(json);

            result.Should().NotBeNull();
            result!.ToolInput.Should().NotBeNull();
            result.ToolInput!.TaskId.Should().Be("task-001");
            result.ToolInput.Block.Should().BeTrue();
            result.ToolInput.Timeout.Should().Be(30000);
        }

        [TestMethod]
        public void DeserializeTaskStopPreToolUse_ShouldReturnStronglyTypedPayload()
        {
            var toolInput = """{ "task_id": "task-002" }""";
            var json = string.Format(PreToolUseWrapper, "TaskStop", toolInput);

            var result = ClaudeHooksSerializer.DeserializeTaskStopPreToolUse(json);

            result.Should().NotBeNull();
            result!.ToolInput.Should().NotBeNull();
            result.ToolInput!.TaskId.Should().Be("task-002");
        }

        [TestMethod]
        public void DeserializeTodoWritePreToolUse_ShouldReturnStronglyTypedPayload()
        {
            var toolInput = """{ "todos": [{ "content": "Fix bug", "activeForm": "Fixing bug", "status": "in_progress" }] }""";
            var json = string.Format(PreToolUseWrapper, "TodoWrite", toolInput);

            var result = ClaudeHooksSerializer.DeserializeTodoWritePreToolUse(json);

            result.Should().NotBeNull();
            result!.ToolInput.Should().NotBeNull();
            result.ToolInput!.Todos.Should().HaveCount(1);
            result.ToolInput.Todos[0].Content.Should().Be("Fix bug");
            result.ToolInput.Todos[0].ActiveForm.Should().Be("Fixing bug");
            result.ToolInput.Todos[0].Status.Should().Be(TodoItemStatus.InProgress);
        }

        [TestMethod]
        public void DeserializeWebFetchPreToolUse_ShouldReturnStronglyTypedPayload()
        {
            var toolInput = """{ "url": "https://example.com/api/docs", "prompt": "Extract API documentation" }""";
            var json = string.Format(PreToolUseWrapper, "WebFetch", toolInput);

            var result = ClaudeHooksSerializer.DeserializeWebFetchPreToolUse(json);

            result.Should().NotBeNull();
            result!.ToolInput.Should().NotBeNull();
            result.ToolInput!.Url.Should().Be("https://example.com/api/docs");
            result.ToolInput.Prompt.Should().Be("Extract API documentation");
        }

        [TestMethod]
        public void DeserializeWebSearchPreToolUse_ShouldReturnStronglyTypedPayload()
        {
            var toolInput = """{ "query": "System.Text.Json source generation", "allowed_domains": ["learn.microsoft.com"] }""";
            var json = string.Format(PreToolUseWrapper, "WebSearch", toolInput);

            var result = ClaudeHooksSerializer.DeserializeWebSearchPreToolUse(json);

            result.Should().NotBeNull();
            result!.ToolInput.Should().NotBeNull();
            result.ToolInput!.Query.Should().Be("System.Text.Json source generation");
            result.ToolInput.AllowedDomains.Should().Contain("learn.microsoft.com");
        }

        [TestMethod]
        public void DeserializeWritePreToolUse_ShouldReturnStronglyTypedPayload()
        {
            var toolInput = """{ "file_path": "D:\\Projects\\TestProject\\src\\NewFile.cs", "content": "namespace Test { }" }""";
            var json = string.Format(PreToolUseWrapper, "Write", toolInput);

            var result = ClaudeHooksSerializer.DeserializeWritePreToolUse(json);

            result.Should().NotBeNull();
            result!.ToolInput.Should().NotBeNull();
            result.ToolInput!.FilePath.Should().Contain("NewFile.cs");
            result.ToolInput.Content.Should().Be("namespace Test { }");
        }

        #endregion

        #region PostToolUse Deserialization Tests

        [TestMethod]
        public void DeserializeAgentPostToolUse_ShouldReturnStronglyTypedPayload()
        {
            var toolInput = """{ "description": "Search codebase", "prompt": "Find endpoints" }""";
            var toolResponse = """{ "status": "completed", "result": "Found 3 endpoints", "content": [{ "type": "text", "text": "Found 3 endpoints" }], "usage": { "input_tokens": 100, "output_tokens": 50 } }""";
            var json = string.Format(PostToolUseWrapper, "Agent", toolInput, toolResponse);

            var result = ClaudeHooksSerializer.DeserializeAgentPostToolUse(json);

            result.Should().NotBeNull();
            result!.ToolInput.Should().NotBeNull();
            result.ToolResponse.Should().NotBeNull();
            result.ToolResponse.Should().BeOfType<CompletedAgentToolResponse>();
        }

        [TestMethod]
        public void DeserializeAskUserQuestionPostToolUse_ShouldReturnStronglyTypedPayload()
        {
            var toolInput = """{ "questions": [{ "question": "Which?", "header": "Choice", "options": [{ "label": "A", "description": "First" }], "multiSelect": false }] }""";
            var toolResponse = """{ "questions": [{ "question": "Which?", "header": "Choice", "options": [{ "label": "A", "description": "First" }], "multiSelect": false }], "answers": { "0": "A" } }""";
            var json = string.Format(PostToolUseWrapper, "AskUserQuestion", toolInput, toolResponse);

            var result = ClaudeHooksSerializer.DeserializeAskUserQuestionPostToolUse(json);

            result.Should().NotBeNull();
            result!.ToolResponse.Should().NotBeNull();
            result.ToolResponse!.Answers.Should().ContainKey("0");
        }

        [TestMethod]
        public void DeserializeBashPostToolUse_ShouldReturnStronglyTypedPayload()
        {
            var toolInput = """{ "command": "dotnet build -c Release" }""";
            var toolResponse = """{ "stdout": "Build succeeded.\r\n", "stderr": "", "interrupted": false, "isImage": false }""";
            var json = string.Format(PostToolUseWrapper, "Bash", toolInput, toolResponse);

            var result = ClaudeHooksSerializer.DeserializeBashPostToolUse(json);

            result.Should().NotBeNull();
            result!.ToolInput.Should().NotBeNull();
            result.ToolInput!.Command.Should().Be("dotnet build -c Release");
            result.ToolResponse.Should().NotBeNull();
            result.ToolResponse!.Stdout.Should().Contain("Build succeeded");
            result.ToolResponse.Interrupted.Should().BeFalse();
        }

        [TestMethod]
        public void DeserializeConfigPostToolUse_ShouldReturnStronglyTypedPayload()
        {
            var toolInput = """{ "setting": "theme" }""";
            var toolResponse = """{ "success": true, "operation": "get", "setting": "theme" }""";
            var json = string.Format(PostToolUseWrapper, "Config", toolInput, toolResponse);

            var result = ClaudeHooksSerializer.DeserializeConfigPostToolUse(json);

            result.Should().NotBeNull();
            result!.ToolResponse.Should().NotBeNull();
            result.ToolResponse!.Success.Should().BeTrue();
        }

        [TestMethod]
        public void DeserializeEditPostToolUse_ShouldReturnStronglyTypedPayload()
        {
            var toolInput = """{ "file_path": "D:\\Projects\\test.cs", "old_string": "old", "new_string": "new" }""";
            var toolResponse = """{ "filePath": "D:\\Projects\\test.cs", "oldString": "old", "newString": "new", "originalFile": "old content", "structuredPatch": [], "userModified": false, "replaceAll": false }""";
            var json = string.Format(PostToolUseWrapper, "Edit", toolInput, toolResponse);

            var result = ClaudeHooksSerializer.DeserializeEditPostToolUse(json);

            result.Should().NotBeNull();
            result!.ToolResponse.Should().NotBeNull();
            result.ToolResponse!.FilePath.Should().Contain("test.cs");
            result.ToolResponse.UserModified.Should().BeFalse();
        }

        [TestMethod]
        public void DeserializeEnterWorktreePostToolUse_ShouldReturnStronglyTypedPayload()
        {
            var toolInput = """{ "name": "feature" }""";
            var toolResponse = """{ "worktreePath": "/tmp/worktree", "message": "Created worktree" }""";
            var json = string.Format(PostToolUseWrapper, "EnterWorktree", toolInput, toolResponse);

            var result = ClaudeHooksSerializer.DeserializeEnterWorktreePostToolUse(json);

            result.Should().NotBeNull();
            result!.ToolResponse.Should().NotBeNull();
            result.ToolResponse!.WorktreePath.Should().Be("/tmp/worktree");
        }

        [TestMethod]
        public void DeserializeExitPlanModePostToolUse_ShouldReturnStronglyTypedPayload()
        {
            var toolInput = """{ "allowedPrompts": [] }""";
            var toolResponse = """{ "isAgent": false }""";
            var json = string.Format(PostToolUseWrapper, "ExitPlanMode", toolInput, toolResponse);

            var result = ClaudeHooksSerializer.DeserializeExitPlanModePostToolUse(json);

            result.Should().NotBeNull();
            result!.ToolResponse.Should().NotBeNull();
            result.ToolResponse!.IsAgent.Should().BeFalse();
        }

        [TestMethod]
        public void DeserializeExitWorktreePostToolUse_ShouldReturnStronglyTypedPayload()
        {
            var toolInput = """{ "action": "keep" }""";
            var toolResponse = """{ "action": "keep", "originalCwd": "/home/user/project", "worktreePath": "/tmp/wt", "message": "Kept worktree" }""";
            var json = string.Format(PostToolUseWrapper, "ExitWorktree", toolInput, toolResponse);

            var result = ClaudeHooksSerializer.DeserializeExitWorktreePostToolUse(json);

            result.Should().NotBeNull();
            result!.ToolResponse.Should().NotBeNull();
            result.ToolResponse!.Action.Should().Be(WorktreeAction.Keep);
        }

        [TestMethod]
        public void DeserializeGlobPostToolUse_ShouldReturnStronglyTypedPayload()
        {
            var toolInput = """{ "pattern": "**/*.cs" }""";
            var toolResponse = """{ "filenames": ["src/Program.cs", "src/Lib.cs"], "durationMs": 15, "numFiles": 2, "truncated": false }""";
            var json = string.Format(PostToolUseWrapper, "Glob", toolInput, toolResponse);

            var result = ClaudeHooksSerializer.DeserializeGlobPostToolUse(json);

            result.Should().NotBeNull();
            result!.ToolResponse.Should().NotBeNull();
            result.ToolResponse!.Filenames.Should().HaveCount(2);
            result.ToolResponse.Truncated.Should().BeFalse();
        }

        [TestMethod]
        public void DeserializeGrepPostToolUse_ShouldReturnStronglyTypedPayload()
        {
            var toolInput = """{ "pattern": "TODO" }""";
            var toolResponse = """{ "mode": "files_with_matches", "filenames": ["src/Program.cs"], "numFiles": 1 }""";
            var json = string.Format(PostToolUseWrapper, "Grep", toolInput, toolResponse);

            var result = ClaudeHooksSerializer.DeserializeGrepPostToolUse(json);

            result.Should().NotBeNull();
            result!.ToolResponse.Should().NotBeNull();
            result.ToolResponse!.Mode.Should().Be(GrepOutputMode.FilesWithMatches);
            result.ToolResponse.NumFiles.Should().Be(1);
        }

        [TestMethod]
        public void DeserializeKillShellPostToolUse_ShouldReturnStronglyTypedPayload()
        {
            var toolInput = """{ "shell_id": "shell-abc" }""";
            var toolResponse = """{ "shellId": "shell-abc", "success": true }""";
            var json = string.Format(PostToolUseWrapper, "KillShell", toolInput, toolResponse);

            var result = ClaudeHooksSerializer.DeserializeKillShellPostToolUse(json);

            result.Should().NotBeNull();
            result!.ToolResponse.Should().NotBeNull();
            result.ToolResponse!.Success.Should().BeTrue();
        }

        [TestMethod]
        public void DeserializeListMcpResourcesPostToolUse_ShouldReturnStronglyTypedPayload()
        {
            var toolInput = """{ "server": "my-server" }""";
            var toolResponse = """[{ "uri": "resource://test", "name": "test", "server": "my-server" }]""";
            var json = string.Format(PostToolUseWrapper, "ListMcpResources", toolInput, toolResponse);

            var result = ClaudeHooksSerializer.DeserializeListMcpResourcesPostToolUse(json);

            result.Should().NotBeNull();
            result!.ToolResponse.Should().NotBeNull();
            result.ToolResponse.Should().HaveCount(1);
        }

        [TestMethod]
        public void DeserializeMcpPostToolUse_ShouldReturnStronglyTypedPayload()
        {
            var toolInput = """{ "server": "docs-server", "query": "search term" }""";
            var toolResponse = """{ "result": "some data" }""";
            var json = string.Format(PostToolUseWrapper, "mcp__docs-server__search", toolInput, toolResponse);

            var result = ClaudeHooksSerializer.DeserializeMcpPostToolUse(json);

            result.Should().NotBeNull();
            result!.ToolInput.Should().NotBeNull();
        }

        [TestMethod]
        public void DeserializeNotebookEditPostToolUse_ShouldReturnStronglyTypedPayload()
        {
            var toolInput = """{ "notebook_path": "analysis.ipynb", "new_source": "print('hello')" }""";
            var toolResponse = """{ "notebook_path": "analysis.ipynb" }""";
            var json = string.Format(PostToolUseWrapper, "NotebookEdit", toolInput, toolResponse);

            var result = ClaudeHooksSerializer.DeserializeNotebookEditPostToolUse(json);

            result.Should().NotBeNull();
            result!.ToolResponse.Should().NotBeNull();
            result.ToolResponse!.NotebookPath.Should().Be("analysis.ipynb");
        }

        [TestMethod]
        public void DeserializeReadPostToolUse_ShouldReturnStronglyTypedPayload()
        {
            var toolInput = """{ "file_path": "D:\\Projects\\README.md" }""";
            var toolResponse = """{ "type": "text", "file": { "filePath": "D:\\Projects\\README.md", "content": "# Hello", "numLines": 1, "startLine": 1, "totalLines": 1 } }""";
            var json = string.Format(PostToolUseWrapper, "Read", toolInput, toolResponse);

            var result = ClaudeHooksSerializer.DeserializeReadPostToolUse(json);

            result.Should().NotBeNull();
            result!.ToolInput.Should().NotBeNull();
            result.ToolInput!.FilePath.Should().Contain("README.md");
            result.ToolResponse.Should().NotBeNull();
        }

        [TestMethod]
        public void DeserializeReadMcpResourcePostToolUse_ShouldReturnStronglyTypedPayload()
        {
            var toolInput = """{ "server": "my-server", "uri": "resource://config" }""";
            var toolResponse = """{ "contents": [{ "uri": "resource://config", "text": "config data" }] }""";
            var json = string.Format(PostToolUseWrapper, "ReadMcpResource", toolInput, toolResponse);

            var result = ClaudeHooksSerializer.DeserializeReadMcpResourcePostToolUse(json);

            result.Should().NotBeNull();
            result!.ToolResponse.Should().NotBeNull();
            result.ToolResponse!.Contents.Should().HaveCount(1);
        }

        [TestMethod]
        public void DeserializeTaskPostToolUse_ShouldReturnStronglyTypedPayload()
        {
            var toolInput = """{ "description": "Run tests", "prompt": "Execute tests", "subagent_type": "general-purpose" }""";
            var toolResponse = """{ "result": "All tests passed", "status": "completed" }""";
            var json = string.Format(PostToolUseWrapper, "Task", toolInput, toolResponse);

            var result = ClaudeHooksSerializer.DeserializeTaskPostToolUse(json);

            result.Should().NotBeNull();
            result!.ToolResponse.Should().NotBeNull();
            result.ToolResponse!.Result.Should().Be("All tests passed");
        }

        [TestMethod]
        public void DeserializeTaskOutputPostToolUse_ShouldReturnStronglyTypedPayload()
        {
            var toolInput = """{ "task_id": "task-001", "block": true, "timeout": 30000 }""";
            var toolResponse = """{ "result": "Task output data", "status": "completed" }""";
            var json = string.Format(PostToolUseWrapper, "TaskOutput", toolInput, toolResponse);

            var result = ClaudeHooksSerializer.DeserializeTaskOutputPostToolUse(json);

            result.Should().NotBeNull();
            result!.ToolResponse.Should().NotBeNull();
            result.ToolResponse!.Result.Should().Be("Task output data");
        }

        [TestMethod]
        public void DeserializeTaskStopPostToolUse_ShouldReturnStronglyTypedPayload()
        {
            var toolInput = """{ "task_id": "task-002" }""";
            var toolResponse = """{ "message": "Task stopped", "task_id": "task-002", "task_type": "agent" }""";
            var json = string.Format(PostToolUseWrapper, "TaskStop", toolInput, toolResponse);

            var result = ClaudeHooksSerializer.DeserializeTaskStopPostToolUse(json);

            result.Should().NotBeNull();
            result!.ToolResponse.Should().NotBeNull();
            result.ToolResponse!.TaskId.Should().Be("task-002");
        }

        [TestMethod]
        public void DeserializeTodoWritePostToolUse_ShouldReturnStronglyTypedPayload()
        {
            var toolInput = """{ "todos": [{ "content": "Fix bug", "activeForm": "Fixing bug", "status": "completed" }] }""";
            var toolResponse = """{ "oldTodos": [], "newTodos": [{ "content": "Fix bug", "activeForm": "Fixing bug", "status": "completed" }] }""";
            var json = string.Format(PostToolUseWrapper, "TodoWrite", toolInput, toolResponse);

            var result = ClaudeHooksSerializer.DeserializeTodoWritePostToolUse(json);

            result.Should().NotBeNull();
            result!.ToolResponse.Should().NotBeNull();
            result.ToolResponse!.NewTodos.Should().HaveCount(1);
        }

        [TestMethod]
        public void DeserializeWebFetchPostToolUse_ShouldReturnStronglyTypedPayload()
        {
            var toolInput = """{ "url": "https://example.com", "prompt": "Get docs" }""";
            var toolResponse = """{ "url": "https://example.com", "result": "Page content here" }""";
            var json = string.Format(PostToolUseWrapper, "WebFetch", toolInput, toolResponse);

            var result = ClaudeHooksSerializer.DeserializeWebFetchPostToolUse(json);

            result.Should().NotBeNull();
            result!.ToolResponse.Should().NotBeNull();
            result.ToolResponse!.Result.Should().Be("Page content here");
        }

        [TestMethod]
        public void DeserializeWebSearchPostToolUse_ShouldReturnStronglyTypedPayload()
        {
            var toolInput = """{ "query": "dotnet AOT" }""";
            var toolResponse = """{ "query": "dotnet AOT", "results": [], "durationSeconds": 0.5 }""";
            var json = string.Format(PostToolUseWrapper, "WebSearch", toolInput, toolResponse);

            var result = ClaudeHooksSerializer.DeserializeWebSearchPostToolUse(json);

            result.Should().NotBeNull();
            result!.ToolResponse.Should().NotBeNull();
            result.ToolResponse!.Query.Should().Be("dotnet AOT");
        }

        [TestMethod]
        public void DeserializeWritePostToolUse_ShouldReturnStronglyTypedPayload()
        {
            var toolInput = """{ "file_path": "D:\\Projects\\test.cs", "content": "namespace Test { }" }""";
            var toolResponse = """{ "type": "create", "filePath": "D:\\Projects\\test.cs", "content": "namespace Test { }", "structuredPatch": [] }""";
            var json = string.Format(PostToolUseWrapper, "Write", toolInput, toolResponse);

            var result = ClaudeHooksSerializer.DeserializeWritePostToolUse(json);

            result.Should().NotBeNull();
            result!.ToolResponse.Should().NotBeNull();
            result.ToolResponse!.Type.Should().Be(WriteOperationType.Create);
        }

        #endregion

        #region PreToolUse Output Serialization Tests

        [TestMethod]
        public void SerializeAgentPreToolUseOutput_ShouldSerializeCorrectly()
        {
            var output = new PreToolUseHookOutput<AgentToolInput> { Continue = true };
            var json = ClaudeHooksSerializer.SerializeAgentPreToolUseOutput(output);
            JsonDocument.Parse(json).RootElement.GetProperty("continue").GetBoolean().Should().BeTrue();
        }

        [TestMethod]
        public void SerializeAskUserQuestionPreToolUseOutput_ShouldSerializeCorrectly()
        {
            var output = new PreToolUseHookOutput<AskUserQuestionToolInput> { Continue = true };
            var json = ClaudeHooksSerializer.SerializeAskUserQuestionPreToolUseOutput(output);
            JsonDocument.Parse(json).RootElement.GetProperty("continue").GetBoolean().Should().BeTrue();
        }

        [TestMethod]
        public void SerializeBashPreToolUseOutput_WithUpdatedInput_ShouldSerializeCorrectly()
        {
            var output = new PreToolUseHookOutput<BashToolInput>
            {
                Continue = true,
                HookSpecificOutput = new PreToolUseSpecificOutput<BashToolInput>
                {
                    PermissionDecision = PermissionDecision.Allow,
                    UpdatedInput = new BashToolInput { Command = "echo safe", Description = "Sanitized command" }
                }
            };

            var json = ClaudeHooksSerializer.SerializeBashPreToolUseOutput(output);
            var doc = JsonDocument.Parse(json);

            doc.RootElement.GetProperty("hookSpecificOutput")
                .GetProperty("updatedInput").GetProperty("command").GetString().Should().Be("echo safe");
        }

        [TestMethod]
        public void SerializeConfigPreToolUseOutput_ShouldSerializeCorrectly()
        {
            var output = new PreToolUseHookOutput<ConfigToolInput> { Continue = true };
            var json = ClaudeHooksSerializer.SerializeConfigPreToolUseOutput(output);
            JsonDocument.Parse(json).RootElement.GetProperty("continue").GetBoolean().Should().BeTrue();
        }

        [TestMethod]
        public void SerializeEditPreToolUseOutput_ShouldSerializeCorrectly()
        {
            var output = new PreToolUseHookOutput<EditToolInput>
            {
                Continue = true,
                HookSpecificOutput = new PreToolUseSpecificOutput<EditToolInput>
                {
                    PermissionDecision = PermissionDecision.Deny,
                    PermissionDecisionReason = "Protected file"
                }
            };

            var json = ClaudeHooksSerializer.SerializeEditPreToolUseOutput(output);
            var doc = JsonDocument.Parse(json);

            doc.RootElement.GetProperty("hookSpecificOutput")
                .GetProperty("permissionDecision").GetString().Should().Be("deny");
        }

        [TestMethod]
        public void SerializeEnterWorktreePreToolUseOutput_ShouldSerializeCorrectly()
        {
            var output = new PreToolUseHookOutput<EnterWorktreeToolInput> { Continue = true };
            var json = ClaudeHooksSerializer.SerializeEnterWorktreePreToolUseOutput(output);
            JsonDocument.Parse(json).RootElement.GetProperty("continue").GetBoolean().Should().BeTrue();
        }

        [TestMethod]
        public void SerializeExitPlanModePreToolUseOutput_ShouldSerializeCorrectly()
        {
            var output = new PreToolUseHookOutput<ExitPlanModeToolInput> { Continue = true };
            var json = ClaudeHooksSerializer.SerializeExitPlanModePreToolUseOutput(output);
            JsonDocument.Parse(json).RootElement.GetProperty("continue").GetBoolean().Should().BeTrue();
        }

        [TestMethod]
        public void SerializeExitWorktreePreToolUseOutput_ShouldSerializeCorrectly()
        {
            var output = new PreToolUseHookOutput<ExitWorktreeToolInput> { Continue = true };
            var json = ClaudeHooksSerializer.SerializeExitWorktreePreToolUseOutput(output);
            JsonDocument.Parse(json).RootElement.GetProperty("continue").GetBoolean().Should().BeTrue();
        }

        [TestMethod]
        public void SerializeGlobPreToolUseOutput_ShouldSerializeCorrectly()
        {
            var output = new PreToolUseHookOutput<GlobToolInput> { Continue = true };
            var json = ClaudeHooksSerializer.SerializeGlobPreToolUseOutput(output);
            JsonDocument.Parse(json).RootElement.GetProperty("continue").GetBoolean().Should().BeTrue();
        }

        [TestMethod]
        public void SerializeGrepPreToolUseOutput_ShouldSerializeCorrectly()
        {
            var output = new PreToolUseHookOutput<GrepToolInput> { Continue = true };
            var json = ClaudeHooksSerializer.SerializeGrepPreToolUseOutput(output);
            JsonDocument.Parse(json).RootElement.GetProperty("continue").GetBoolean().Should().BeTrue();
        }

        [TestMethod]
        public void SerializeKillShellPreToolUseOutput_ShouldSerializeCorrectly()
        {
            var output = new PreToolUseHookOutput<KillShellToolInput> { Continue = true };
            var json = ClaudeHooksSerializer.SerializeKillShellPreToolUseOutput(output);
            JsonDocument.Parse(json).RootElement.GetProperty("continue").GetBoolean().Should().BeTrue();
        }

        [TestMethod]
        public void SerializeListMcpResourcesPreToolUseOutput_ShouldSerializeCorrectly()
        {
            var output = new PreToolUseHookOutput<ListMcpResourcesToolInput> { Continue = true };
            var json = ClaudeHooksSerializer.SerializeListMcpResourcesPreToolUseOutput(output);
            JsonDocument.Parse(json).RootElement.GetProperty("continue").GetBoolean().Should().BeTrue();
        }

        [TestMethod]
        public void SerializeMcpPreToolUseOutput_ShouldSerializeCorrectly()
        {
            var output = new PreToolUseHookOutput<McpToolInput> { Continue = true };
            var json = ClaudeHooksSerializer.SerializeMcpPreToolUseOutput(output);
            JsonDocument.Parse(json).RootElement.GetProperty("continue").GetBoolean().Should().BeTrue();
        }

        [TestMethod]
        public void SerializeNotebookEditPreToolUseOutput_ShouldSerializeCorrectly()
        {
            var output = new PreToolUseHookOutput<NotebookEditToolInput> { Continue = true };
            var json = ClaudeHooksSerializer.SerializeNotebookEditPreToolUseOutput(output);
            JsonDocument.Parse(json).RootElement.GetProperty("continue").GetBoolean().Should().BeTrue();
        }

        [TestMethod]
        public void SerializeReadPreToolUseOutput_ShouldSerializeCorrectly()
        {
            var output = new PreToolUseHookOutput<ReadToolInput> { Continue = true };
            var json = ClaudeHooksSerializer.SerializeReadPreToolUseOutput(output);
            JsonDocument.Parse(json).RootElement.GetProperty("continue").GetBoolean().Should().BeTrue();
        }

        [TestMethod]
        public void SerializeReadMcpResourcePreToolUseOutput_ShouldSerializeCorrectly()
        {
            var output = new PreToolUseHookOutput<ReadMcpResourceToolInput> { Continue = true };
            var json = ClaudeHooksSerializer.SerializeReadMcpResourcePreToolUseOutput(output);
            JsonDocument.Parse(json).RootElement.GetProperty("continue").GetBoolean().Should().BeTrue();
        }

        [TestMethod]
        public void SerializeTaskPreToolUseOutput_ShouldSerializeCorrectly()
        {
            var output = new PreToolUseHookOutput<TaskToolInput> { Continue = true };
            var json = ClaudeHooksSerializer.SerializeTaskPreToolUseOutput(output);
            JsonDocument.Parse(json).RootElement.GetProperty("continue").GetBoolean().Should().BeTrue();
        }

        [TestMethod]
        public void SerializeTaskOutputPreToolUseOutput_ShouldSerializeCorrectly()
        {
            var output = new PreToolUseHookOutput<TaskOutputToolInput> { Continue = true };
            var json = ClaudeHooksSerializer.SerializeTaskOutputPreToolUseOutput(output);
            JsonDocument.Parse(json).RootElement.GetProperty("continue").GetBoolean().Should().BeTrue();
        }

        [TestMethod]
        public void SerializeTaskStopPreToolUseOutput_ShouldSerializeCorrectly()
        {
            var output = new PreToolUseHookOutput<TaskStopToolInput> { Continue = true };
            var json = ClaudeHooksSerializer.SerializeTaskStopPreToolUseOutput(output);
            JsonDocument.Parse(json).RootElement.GetProperty("continue").GetBoolean().Should().BeTrue();
        }

        [TestMethod]
        public void SerializeTodoWritePreToolUseOutput_ShouldSerializeCorrectly()
        {
            var output = new PreToolUseHookOutput<TodoWriteToolInput> { Continue = true };
            var json = ClaudeHooksSerializer.SerializeTodoWritePreToolUseOutput(output);
            JsonDocument.Parse(json).RootElement.GetProperty("continue").GetBoolean().Should().BeTrue();
        }

        [TestMethod]
        public void SerializeWebFetchPreToolUseOutput_ShouldSerializeCorrectly()
        {
            var output = new PreToolUseHookOutput<WebFetchToolInput> { Continue = true };
            var json = ClaudeHooksSerializer.SerializeWebFetchPreToolUseOutput(output);
            JsonDocument.Parse(json).RootElement.GetProperty("continue").GetBoolean().Should().BeTrue();
        }

        [TestMethod]
        public void SerializeWebSearchPreToolUseOutput_ShouldSerializeCorrectly()
        {
            var output = new PreToolUseHookOutput<WebSearchToolInput> { Continue = true };
            var json = ClaudeHooksSerializer.SerializeWebSearchPreToolUseOutput(output);
            JsonDocument.Parse(json).RootElement.GetProperty("continue").GetBoolean().Should().BeTrue();
        }

        [TestMethod]
        public void SerializeWritePreToolUseOutput_ShouldSerializeCorrectly()
        {
            var output = new PreToolUseHookOutput<WriteToolInput> { Continue = true };
            var json = ClaudeHooksSerializer.SerializeWritePreToolUseOutput(output);
            JsonDocument.Parse(json).RootElement.GetProperty("continue").GetBoolean().Should().BeTrue();
        }

        #endregion

        #region Malformed Payload Tests

        [TestMethod]
        public void DeserializeBashPreToolUse_WithGarbageInput_ShouldThrowJsonException()
        {
            var action = () => ClaudeHooksSerializer.DeserializeBashPreToolUse("not valid json at all");

            action.Should().Throw<JsonException>();
        }

        [TestMethod]
        public void DeserializeBashPreToolUse_WithNullLiteral_ShouldReturnNull()
        {
            var result = ClaudeHooksSerializer.DeserializeBashPreToolUse("null");

            result.Should().BeNull();
        }

        [TestMethod]
        public void DeserializeBashPreToolUse_WithEmptyObject_ShouldReturnObjectWithNullToolInput()
        {
            var result = ClaudeHooksSerializer.DeserializeBashPreToolUse("{}");

            result.Should().NotBeNull();
            result!.ToolInput.Should().BeNull();
        }

        [TestMethod]
        public void DeserializeBashPreToolUse_WithTruncatedJson_ShouldThrowJsonException()
        {
            var json = string.Format(PreToolUseWrapper, "Bash", """{ "command": "dotnet build""");

            var action = () => ClaudeHooksSerializer.DeserializeBashPreToolUse(json);

            action.Should().Throw<JsonException>();
        }

        [TestMethod]
        public void DeserializeBashPreToolUse_WithToolInputAsString_ShouldThrowJsonException()
        {
            var json = """
                {
                    "session_id": "aaaaaaaa-bbbb-cccc-dddd-eeeeeeeeeeee",
                    "tool_name": "Bash",
                    "tool_input": "this is a string not an object"
                }
                """;

            var action = () => ClaudeHooksSerializer.DeserializeBashPreToolUse(json);

            action.Should().Throw<JsonException>();
        }

        [TestMethod]
        public void DeserializeBashPreToolUse_WithWrongToolPayload_ShouldDeserializeWithDefaults()
        {
            var writeToolInput = """{ "file_path": "D:\\Projects\\test.cs", "content": "namespace Test { }" }""";
            var json = string.Format(PreToolUseWrapper, "Bash", writeToolInput);

            var result = ClaudeHooksSerializer.DeserializeBashPreToolUse(json);

            result.Should().NotBeNull();
            result!.ToolInput.Should().NotBeNull();
            result.ToolInput!.Command.Should().BeNullOrEmpty();
        }

        [TestMethod]
        public void DeserializeReadPreToolUse_WithGarbageInput_ShouldThrowJsonException()
        {
            var action = () => ClaudeHooksSerializer.DeserializeReadPreToolUse("{{{{garbage}}}}");

            action.Should().Throw<JsonException>();
        }

        [TestMethod]
        public void DeserializeEditPreToolUse_WithExtraUnknownFields_ShouldIgnoreExtras()
        {
            var toolInput = """{ "file_path": "D:\\Projects\\test.cs", "old_string": "old", "new_string": "new", "unknown_field": 42, "another_extra": "ignored" }""";
            var json = string.Format(PreToolUseWrapper, "Edit", toolInput);

            var result = ClaudeHooksSerializer.DeserializeEditPreToolUse(json);

            result.Should().NotBeNull();
            result!.ToolInput.Should().NotBeNull();
            result.ToolInput!.FilePath.Should().Contain("test.cs");
            result.ToolInput.OldString.Should().Be("old");
            result.ToolInput.NewString.Should().Be("new");
        }

        [TestMethod]
        public void DeserializeBashPostToolUse_WithGarbageInput_ShouldThrowJsonException()
        {
            var action = () => ClaudeHooksSerializer.DeserializeBashPostToolUse("not valid json");

            action.Should().Throw<JsonException>();
        }

        [TestMethod]
        public void DeserializeBashPostToolUse_WithNullLiteral_ShouldReturnNull()
        {
            var result = ClaudeHooksSerializer.DeserializeBashPostToolUse("null");

            result.Should().BeNull();
        }

        [TestMethod]
        public void DeserializeBashPostToolUse_WithEmptyObject_ShouldReturnObjectWithNullResponse()
        {
            var result = ClaudeHooksSerializer.DeserializeBashPostToolUse("{}");

            result.Should().NotBeNull();
            result!.ToolResponse.Should().BeNull();
        }

        [TestMethod]
        public void DeserializeBashPostToolUse_WithToolResponseAsString_ShouldThrowJsonException()
        {
            var json = """
                {
                    "session_id": "aaaaaaaa-bbbb-cccc-dddd-eeeeeeeeeeee",
                    "tool_name": "Bash",
                    "tool_input": { "command": "echo hi" },
                    "tool_response": "this is a string not an object"
                }
                """;

            var action = () => ClaudeHooksSerializer.DeserializeBashPostToolUse(json);

            action.Should().Throw<JsonException>();
        }

        [TestMethod]
        public void DeserializeReadPostToolUse_WithMissingTypeDiscriminator_ShouldThrowNotSupportedException()
        {
            var toolInput = """{ "file_path": "D:\\Projects\\README.md" }""";
            var toolResponse = """{ "file": { "filePath": "D:\\Projects\\README.md", "content": "# Hello", "numLines": 1, "startLine": 1, "totalLines": 1 } }""";
            var json = string.Format(PostToolUseWrapper, "Read", toolInput, toolResponse);

            var action = () => ClaudeHooksSerializer.DeserializeReadPostToolUse(json);

            action.Should().Throw<NotSupportedException>();
        }

        [TestMethod]
        public void DeserializeReadPostToolUse_WithUnknownTypeDiscriminator_ShouldThrowJsonException()
        {
            var toolInput = """{ "file_path": "D:\\Projects\\README.md" }""";
            var toolResponse = """{ "type": "unknown_format", "data": "something" }""";
            var json = string.Format(PostToolUseWrapper, "Read", toolInput, toolResponse);

            var action = () => ClaudeHooksSerializer.DeserializeReadPostToolUse(json);

            action.Should().Throw<JsonException>();
        }

        [TestMethod]
        public void DeserializeEditPostToolUse_WithMissingToolResponse_ShouldReturnNullResponse()
        {
            var json = """
                {
                    "session_id": "aaaaaaaa-bbbb-cccc-dddd-eeeeeeeeeeee",
                    "tool_name": "Edit",
                    "tool_input": { "file_path": "D:\\Projects\\test.cs", "old_string": "old", "new_string": "new" }
                }
                """;

            var result = ClaudeHooksSerializer.DeserializeEditPostToolUse(json);

            result.Should().NotBeNull();
            result!.ToolResponse.Should().BeNull();
        }

        #endregion

    }

}
