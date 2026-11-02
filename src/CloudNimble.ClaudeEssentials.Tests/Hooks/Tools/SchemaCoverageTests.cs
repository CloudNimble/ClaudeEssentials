using CloudNimble.ClaudeEssentials.Hooks;
using CloudNimble.ClaudeEssentials.Hooks.Outputs;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Responses;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace CloudNimble.ClaudeEssentials.Tests.Hooks.Tools
{

    /// <summary>
    /// Verifies the C# surface against the <c>claude-code-schema.d.ts</c> file at the repository root, which is the
    /// <c>sdk-tools.d.ts</c> file published in the <c>@anthropic-ai/claude-code</c> package.
    /// </summary>
    /// <remarks>
    /// These tests read the schema itself rather than a copy of it. Replacing the file with a newer release
    /// makes them fail for every tool, wire name, or union member that the C# types do not yet represent.
    /// </remarks>
    [TestClass]
    public class SchemaCoverageTests
    {

        #region Fields

        /// <summary>
        /// Wire names that a C# type intentionally has beyond one side of the schema, keyed by schema type name.
        /// </summary>
        /// <remarks>
        /// The schema gives AskUserQuestion annotations an <c>unanswered</c> key on the output only, and both sides share <see cref="AskUserQuestionAnnotation"/>.
        /// The schema also omits <c>seeded</c> from the Read tool's <c>artifactRead</c> value only, and every tool shares <see cref="ArtifactReadInfo"/>.
        /// </remarks>
        private static readonly Dictionary<string, string[]> AllowedExtras = new()
        {
            ["AskUserQuestionInput"] = ["unanswered"],
            ["FileReadOutput"] = ["seeded"]
        };

        /// <summary>
        /// Maps each tool name to the schema type prefix and the C# types representing it.
        /// </summary>
        private static readonly (string Tool, string Schema, Type Input, Type Response)[] Tools =
        [
            ("Agent", "Agent", typeof(AgentToolInput), typeof(AgentToolResponse)),
            ("Artifact", "Artifact", typeof(ArtifactToolInput), typeof(ArtifactToolResponse)),
            ("AskUserQuestion", "AskUserQuestion", typeof(AskUserQuestionToolInput), typeof(AskUserQuestionToolResponse)),
            ("Bash", "Bash", typeof(BashToolInput), typeof(BashToolResponse)),
            ("ClaudeDesign", "ClaudeDesign", typeof(ClaudeDesignToolInput), typeof(ClaudeDesignToolResponse)),
            ("CronCreate", "CronCreate", typeof(CronCreateToolInput), typeof(CronCreateToolResponse)),
            ("CronDelete", "CronDelete", typeof(CronDeleteToolInput), typeof(CronDeleteToolResponse)),
            ("CronList", "CronList", typeof(CronListToolInput), typeof(CronListToolResponse)),
            ("Edit", "FileEdit", typeof(EditToolInput), typeof(EditToolResponse)),
            ("EnterPlanMode", "EnterPlanMode", typeof(EnterPlanModeToolInput), typeof(EnterPlanModeToolResponse)),
            ("EnterWorktree", "EnterWorktree", typeof(EnterWorktreeToolInput), typeof(EnterWorktreeToolResponse)),
            ("ExitPlanMode", "ExitPlanMode", typeof(ExitPlanModeToolInput), typeof(ExitPlanModeToolResponse)),
            ("ExitWorktree", "ExitWorktree", typeof(ExitWorktreeToolInput), typeof(ExitWorktreeToolResponse)),
            ("Glob", "Glob", typeof(GlobToolInput), typeof(GlobToolResponse)),
            ("Grep", "Grep", typeof(GrepToolInput), typeof(GrepToolResponse)),
            ("ListMcpResources", "ListMcpResources", typeof(ListMcpResourcesToolInput), typeof(List<McpResourceInfo>)),
            ("Mcp", "Mcp", typeof(McpToolInput), typeof(McpToolResponse)),
            ("Monitor", "Monitor", typeof(MonitorToolInput), typeof(MonitorToolResponse)),
            ("NotebookEdit", "NotebookEdit", typeof(NotebookEditToolInput), typeof(NotebookEditToolResponse)),
            ("OfferChromeSetup", "OfferChromeSetup", typeof(OfferChromeSetupToolInput), typeof(OfferChromeSetupToolResponse)),
            ("Projects", "Projects", typeof(ProjectsToolInput), typeof(ProjectsToolResponse)),
            ("ProposeGoal", "ProposeGoal", typeof(ProposeGoalToolInput), typeof(ProposeGoalToolResponse)),
            ("ProposeSkills", "ProposeSkills", typeof(ProposeSkillsToolInput), typeof(ProposeSkillsToolResponse)),
            ("PushNotification", "PushNotification", typeof(PushNotificationToolInput), typeof(PushNotificationToolResponse)),
            ("Read", "FileRead", typeof(ReadToolInput), typeof(ReadToolResponse)),
            ("ReadMcpResource", "ReadMcpResource", typeof(ReadMcpResourceToolInput), typeof(ReadMcpResourceToolResponse)),
            ("ReadMcpResourceDir", "ReadMcpResourceDir", typeof(ReadMcpResourceDirToolInput), typeof(ReadMcpResourceDirToolResponse)),
            ("ReadNotifications", "ReadNotifications", typeof(ReadNotificationsToolInput), typeof(ReadNotificationsToolResponse)),
            ("RefreshMcpTools", "RefreshMcpTools", typeof(RefreshMcpToolsToolInput), typeof(List<RefreshMcpToolsServerResult>)),
            ("RemoteTrigger", "RemoteTrigger", typeof(RemoteTriggerToolInput), typeof(RemoteTriggerToolResponse)),
            ("ReportFindings", "ReportFindings", typeof(ReportFindingsToolInput), typeof(ReportFindingsToolResponse)),
            ("ScheduleWakeup", "ScheduleWakeup", typeof(ScheduleWakeupToolInput), typeof(ScheduleWakeupToolResponse)),
            ("SendFeedback", "SendFeedback", typeof(SendFeedbackToolInput), typeof(SendFeedbackToolResponse)),
            ("ShowOnboardingRolePicker", "ShowOnboardingRolePicker", typeof(ShowOnboardingRolePickerToolInput), typeof(ShowOnboardingRolePickerToolResponse)),
            ("TaskCreate", "TaskCreate", typeof(TaskCreateToolInput), typeof(TaskCreateToolResponse)),
            ("TaskGet", "TaskGet", typeof(TaskGetToolInput), typeof(TaskGetToolResponse)),
            ("TaskList", "TaskList", typeof(TaskListToolInput), typeof(TaskListToolResponse)),
            ("TaskStop", "TaskStop", typeof(TaskStopToolInput), typeof(TaskStopToolResponse)),
            ("TaskUpdate", "TaskUpdate", typeof(TaskUpdateToolInput), typeof(TaskUpdateToolResponse)),
            ("TodoWrite", "TodoWrite", typeof(TodoWriteToolInput), typeof(TodoWriteToolResponse)),
            ("WebFetch", "WebFetch", typeof(WebFetchToolInput), typeof(WebFetchToolResponse)),
            ("WebSearch", "WebSearch", typeof(WebSearchToolInput), typeof(WebSearchToolResponse)),
            ("Workflow", "Workflow", typeof(WorkflowToolInput), typeof(WorkflowToolResponse)),
            ("Write", "FileWrite", typeof(WriteToolInput), typeof(WriteToolResponse))
        ];

        private static readonly Lazy<string[]> SchemaLines = new(() =>
        {
            var directory = new DirectoryInfo(AppContext.BaseDirectory);
            while (directory is not null && !File.Exists(Path.Combine(directory.FullName, "claude-code-schema.d.ts")))
            {
                directory = directory.Parent;
            }

            directory.Should().NotBeNull("claude-code-schema.d.ts should exist at the repository root");
            return File.ReadAllLines(Path.Combine(directory!.FullName, "claude-code-schema.d.ts"));
        });

        #endregion

        #region Test Methods

        [TestMethod]
        public void ToolTable_ShouldMatchEveryMemberOfTheSchemaUnions()
        {
            var inputs = UnionMembers("ToolInputSchemas").Where(m => m != "ToolOutputSchemas");
            var outputs = UnionMembers("ToolOutputSchemas");

            inputs.Should().BeEquivalentTo(Tools.Select(t => t.Schema + "Input"));
            outputs.Should().BeEquivalentTo(Tools.Select(t => t.Schema + "Output"));
        }

        [TestMethod]
        public void ToolInputs_ShouldDeclareExactlyTheWireNamesInTheSchema()
        {
            var problems = Tools.SelectMany(t => Differences(t.Schema + "Input", t.Input)).ToList();

            problems.Should().BeEmpty();
        }

        [TestMethod]
        public void ToolResponses_ShouldDeclareExactlyTheWireNamesInTheSchema()
        {
            var problems = Tools.SelectMany(t => Differences(t.Schema + "Output", t.Response)).ToList();

            problems.Should().BeEmpty();
        }

        [TestMethod]
        public void Serializer_ShouldExposeTypedMethodsForEveryTool()
        {
            var serializer = typeof(ClaudeHooksSerializer);

            foreach (var (tool, _, input, response) in Tools)
            {
                var pre = serializer.GetMethod($"Deserialize{tool}PreToolUse", [typeof(string)]);
                pre.Should().NotBeNull(tool);
                var prePayload = pre!.Invoke(null, [Envelope(tool)]);
                prePayload.Should().NotBeNull(tool);
                pre.ReturnType.BaseType!.GetGenericArguments().Should().Equal([input], tool);

                var post = serializer.GetMethod($"Deserialize{tool}PostToolUse", [typeof(string)]);
                post.Should().NotBeNull(tool);
                var postPayload = post!.Invoke(null, [Envelope(tool)]);
                postPayload.Should().NotBeNull(tool);
                post.ReturnType.BaseType!.GetGenericArguments().Should().Equal([input, response], tool);

                var outputType = typeof(PreToolUseHookOutput<>).MakeGenericType(input);
                var serialize = serializer.GetMethod($"Serialize{tool}PreToolUseOutput", [outputType]);
                serialize.Should().NotBeNull(tool);
                var json = (string)serialize!.Invoke(null, [Activator.CreateInstance(outputType)])!;
                json.Should().NotBeNullOrWhiteSpace(tool);
            }
        }

        #endregion

        #region Private Methods

        private static string Envelope(string tool) => "{\"tool_name\":\"" + tool + "\",\"tool_input\":{}}";

        private static IEnumerable<string> Differences(string schemaType, Type type)
        {
            var expected = SchemaKeys(schemaType);
            var actual = WireNames(type);
            var allowed = AllowedExtras.TryGetValue(schemaType, out var extras) ? extras : [];

            foreach (var missing in expected.Except(actual))
            {
                yield return $"{schemaType}: '{missing}' is in the schema but not on {type.Name}";
            }

            foreach (var extra in actual.Except(expected).Except(allowed))
            {
                yield return $"{schemaType}: '{extra}' is on {type.Name} but not in the schema";
            }
        }

        private static HashSet<string> SchemaKeys(string name)
        {
            var lines = SchemaLines.Value;
            var start = Array.FindIndex(lines, l => Regex.IsMatch(l, $@"^export (type|interface) {name}\b"));
            start.Should().BeGreaterThanOrEqualTo(0, $"{name} should be declared in the schema");

            var keys = new HashSet<string>();
            for (var i = start + 1; i < lines.Length && !lines[i].StartsWith("export ", StringComparison.Ordinal); i++)
            {
                var trimmed = lines[i].TrimStart();
                if (trimmed.StartsWith('*') || trimmed.StartsWith("/*", StringComparison.Ordinal))
                {
                    continue;
                }

                var match = Regex.Match(trimmed, @"^""?(-?[A-Za-z_][A-Za-z0-9_]*)""?\??:");
                if (match.Success)
                {
                    keys.Add(match.Groups[1].Value);
                }
            }

            return keys;
        }

        private static List<string> UnionMembers(string name)
        {
            var lines = SchemaLines.Value;
            var start = Array.FindIndex(lines, l => l.StartsWith($"export type {name} =", StringComparison.Ordinal));
            var members = new List<string>();
            for (var i = start + 1; lines[i].StartsWith("  | ", StringComparison.Ordinal); i++)
            {
                members.Add(lines[i][4..].TrimEnd(';'));
            }

            return members;
        }

        private static HashSet<string> WireNames(Type root)
        {
            var names = new HashSet<string>();
            var visited = new HashSet<Type>();
            Visit(root);
            return names;

            void Visit(Type type)
            {
                type = type.GetElementType() ?? Nullable.GetUnderlyingType(type) ?? type;
                if (!visited.Add(type))
                {
                    return;
                }

                foreach (var argument in type.GetGenericArguments())
                {
                    Visit(argument);
                }

                if (type.Assembly != typeof(ClaudeHooksSerializer).Assembly || type.IsEnum)
                {
                    return;
                }

                if (type.GetCustomAttribute<JsonPolymorphicAttribute>(false) is { } polymorphic)
                {
                    names.Add(polymorphic.TypeDiscriminatorPropertyName!);
                }

                foreach (var derived in type.GetCustomAttributes<JsonDerivedTypeAttribute>(false))
                {
                    Visit(derived.DerivedType);
                }

                foreach (var property in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
                {
                    if (property.GetCustomAttribute<JsonPropertyNameAttribute>() is { } wire)
                    {
                        names.Add(wire.Name);
                    }

                    Visit(property.PropertyType);
                }
            }
        }

        #endregion

    }

}
