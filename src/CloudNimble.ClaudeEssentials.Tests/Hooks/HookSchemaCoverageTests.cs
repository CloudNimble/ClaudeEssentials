using CloudNimble.ClaudeEssentials.Hooks;
using CloudNimble.ClaudeEssentials.Hooks.Inputs;
using CloudNimble.ClaudeEssentials.Hooks.Outputs;
using CloudNimble.ClaudeEssentials.Hooks.Permissions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace CloudNimble.ClaudeEssentials.Tests.Hooks
{

    /// <summary>
    /// Verifies the hook surface against the <c>claude-agent-sdk-schema.d.ts</c> file at the repository root, which is the
    /// <c>sdk.d.ts</c> file published in the <c>@anthropic-ai/claude-agent-sdk</c> package.
    /// </summary>
    /// <remarks>
    /// These tests read the schema itself rather than a copy of it. Replacing the file with a newer release makes
    /// them fail for every event, wire name, or enum value that the C# types do not yet represent.
    /// </remarks>
    [TestClass]
    public class HookSchemaCoverageTests
    {

        #region Fields

        /// <summary>
        /// Wire names that a C# type intentionally has beyond the schema, keyed by schema type name.
        /// </summary>
        /// <remarks>
        /// <see cref="PermissionRequestHookInput{TToolInput}"/> shares <see cref="ToolHookInputBase{TToolInput}"/> with the other tool hooks, and the schema omits <c>tool_use_id</c> from this one event only.
        /// </remarks>
        private static readonly Dictionary<string, string[]> AllowedExtras = new()
        {
            ["PermissionRequestHookInput"] = ["tool_use_id"]
        };

        /// <summary>
        /// Enum properties that share a C# enum with a wider set of values than this one schema property allows.
        /// </summary>
        private static readonly HashSet<string> SharedEnumProperties =
        [
            "PreModelSwitchHookInput.source",
            "PreModelSwitchHookSpecificOutput.permissionDecision"
        ];

        private static readonly Assembly Library = typeof(ClaudeHooksSerializer).Assembly;

        private static readonly Lazy<string[]> SchemaLines = new(() =>
        {
            var directory = new DirectoryInfo(AppContext.BaseDirectory);
            while (directory is not null && !File.Exists(Path.Combine(directory.FullName, "claude-agent-sdk-schema.d.ts")))
            {
                directory = directory.Parent;
            }

            directory.Should().NotBeNull("claude-agent-sdk-schema.d.ts should exist at the repository root");
            return File.ReadAllLines(Path.Combine(directory!.FullName, "claude-agent-sdk-schema.d.ts"));
        });

        #endregion

        #region Test Methods

        [TestMethod]
        public void HookEventName_ShouldMatchTheSchemaEventsExactly()
        {
            EnumWireNames(typeof(HookEventName)).Should().BeEquivalentTo(Events());
        }

        [TestMethod]
        public void HookInputUnion_ShouldContainAnInputForEveryEvent()
        {
            var union = Regex.Matches(string.Join(" ", Block("HookInput")), @"(\w+)HookInput\b").Select(m => m.Groups[1].Value).Where(n => n != "").Distinct();

            union.Should().BeEquivalentTo(Events());
        }

        [TestMethod]
        public void HookInputs_ShouldDeclareExactlyTheWireNamesInTheSchema()
        {
            var problems = Events().SelectMany(e => Differences($"{e}HookInput", Resolve("Inputs", $"{e}HookInput"))).ToList();

            problems.Should().BeEmpty();
        }

        [TestMethod]
        public void HookSpecificOutputs_ShouldDeclareExactlyTheWireNamesInTheSchema()
        {
            var problems = SpecificOutputEvents().SelectMany(e => Differences($"{e}HookSpecificOutput", Resolve("Outputs", $"{e}SpecificOutput"))).ToList();

            problems.Should().BeEmpty();
        }

        [TestMethod]
        public void HookOutputBase_ShouldDeclareExactlyTheWireNamesInTheSchema()
        {
            var expected = OwnKeys("SyncHookJSONOutput");
            expected.Remove("hookSpecificOutput");

            WireNames(typeof(HookOutputBase)).Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public void AsyncHookOutput_ShouldDeclareExactlyTheWireNamesInTheSchema()
        {
            WireNames(typeof(AsyncHookOutput)).Should().BeEquivalentTo(OwnKeys("AsyncHookJSONOutput"));
        }

        [TestMethod]
        public void HookOutputs_ShouldExposeSpecificOutputOnlyForEventsInTheSchema()
        {
            var specific = SpecificOutputEvents().ToHashSet();

            foreach (var e in Events())
            {
                var property = Resolve("Outputs", $"{e}HookOutput").GetProperty(nameof(PostToolUseHookOutput.HookSpecificOutput));

                if (specific.Contains(e))
                {
                    property.Should().NotBeNull(e);
                    property!.PropertyType.Name.Should().StartWith($"{e}SpecificOutput", e);
                }
                else
                {
                    property.Should().BeNull(e);
                }
            }
        }

        [TestMethod]
        public void HookEnums_ShouldMatchTheLiteralUnionsInTheSchema()
        {
            var problems = new List<string>();

            foreach (var e in Events())
            {
                problems.AddRange(EnumDifferences($"{e}HookInput", Resolve("Inputs", $"{e}HookInput")));
            }

            foreach (var e in SpecificOutputEvents())
            {
                problems.AddRange(EnumDifferences($"{e}HookSpecificOutput", Resolve("Outputs", $"{e}SpecificOutput")));
            }

            problems.AddRange(EnumDifferences("SyncHookJSONOutput", typeof(HookOutputBase)));

            problems.Should().BeEmpty();
        }

        [TestMethod]
        public void SharedEnums_ShouldMatchTheirSchemaAliases()
        {
            EnumWireNames(typeof(PermissionMode)).Should().BeEquivalentTo(AliasValues("PermissionMode"));
            EnumWireNames(typeof(PermissionBehavior)).Should().BeEquivalentTo(AliasValues("PermissionBehavior"));
            EnumWireNames(typeof(PermissionUpdateDestination)).Should().BeEquivalentTo(AliasValues("PermissionUpdateDestination"));
        }

        [TestMethod]
        public void Serializer_ShouldExposeNamedMethodsForEveryEvent()
        {
            var serializer = typeof(ClaudeHooksSerializer);

            foreach (var e in Events().Where(e => e is not ("PreToolUse" or "PostToolUse" or "PermissionRequest")))
            {
                var input = serializer.GetMethod($"Deserialize{e}Input", [typeof(string)]);
                input.Should().NotBeNull(e);
                input!.Invoke(null, ["{}"]).Should().BeOfType(Resolve("Inputs", $"{e}HookInput"), e);

                var outputType = Resolve("Outputs", $"{e}HookOutput");
                var output = serializer.GetMethod($"Serialize{e}Output", [outputType]);
                output.Should().NotBeNull(e);
                ((string)output!.Invoke(null, [Activator.CreateInstance(outputType)])!).Should().Contain("\"continue\":true", e);
            }
        }

        #endregion

        #region Private Methods

        private static List<string> Events()
        {
            var line = SchemaLines.Value.First(l => l.StartsWith("export declare const HOOK_EVENTS:", StringComparison.Ordinal));
            return Regex.Matches(line, "'([^']+)'").Select(m => m.Groups[1].Value).ToList();
        }

        private static List<string> SpecificOutputEvents()
        {
            var line = SchemaLines.Value.First(l => l.TrimStart().StartsWith("hookSpecificOutput?:", StringComparison.Ordinal));
            return Regex.Matches(line, @"(\w+)HookSpecificOutput\b").Select(m => m.Groups[1].Value).ToList();
        }

        /// <summary>
        /// Finds a C# type by name, closing any generic type over <see cref="object"/>.
        /// </summary>
        private static Type Resolve(string area, string name)
        {
            var type = Library.GetTypes().SingleOrDefault(t => t.Namespace == $"CloudNimble.ClaudeEssentials.Hooks.{area}" && (t.Name == name || t.Name.StartsWith($"{name}`", StringComparison.Ordinal)));
            type.Should().NotBeNull($"{name} should exist");

            return type!.IsGenericTypeDefinition
                ? type.MakeGenericType(type.GetGenericArguments().Select(_ => typeof(object)).ToArray())
                : type;
        }

        private static IEnumerable<string> Differences(string schemaType, Type type)
        {
            var expected = Keys(schemaType);
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

        private static IEnumerable<string> EnumDifferences(string schemaType, Type type)
        {
            foreach (var line in Block(schemaType).Select(l => l.Trim()))
            {
                var property = Regex.Match(line, @"^(\w+)\??:\s*(.+);$");
                if (!property.Success)
                {
                    continue;
                }

                var key = property.Groups[1].Value;
                var value = property.Groups[2].Value;
                var literals = Regex.IsMatch(value, @"^\w+$") ? AliasValues(value) : Regex.Matches(value, "'([^']+)'").Select(m => m.Groups[1].Value).ToList();

                // A single literal is a discriminator such as hook_event_name, not a set of choices.
                if (literals.Count < 2)
                {
                    continue;
                }

                var member = type.GetProperties().FirstOrDefault(p => p.GetCustomAttribute<JsonPropertyNameAttribute>()?.Name == key);
                var enumType = member is null ? null : Nullable.GetUnderlyingType(member.PropertyType) ?? member.PropertyType;
                if (enumType?.IsEnum != true)
                {
                    yield return $"{schemaType}.{key}: the schema has a literal union, but {type.Name} does not use an enum";
                    continue;
                }

                var actual = EnumWireNames(enumType);
                if (literals.Except(actual).Any() || (!SharedEnumProperties.Contains($"{schemaType}.{key}") && actual.Except(literals).Any()))
                {
                    yield return $"{schemaType}.{key}: {enumType.Name} has [{string.Join(", ", actual)}], but the schema has [{string.Join(", ", literals)}]";
                }
            }
        }

        private static List<string> AliasValues(string alias)
        {
            var line = SchemaLines.Value.FirstOrDefault(l => l.StartsWith($"export declare type {alias} = '", StringComparison.Ordinal) || l.StartsWith($"declare type {alias} = '", StringComparison.Ordinal));
            return line is null ? [] : Regex.Matches(line, "'([^']+)'").Select(m => m.Groups[1].Value).ToList();
        }

        private static List<string> EnumWireNames(Type enumType)
        {
            return enumType.GetFields(BindingFlags.Public | BindingFlags.Static)
                .Select(f => f.GetCustomAttribute<JsonStringEnumMemberNameAttribute>()?.Name ?? f.Name)
                .ToList();
        }

        /// <summary>
        /// Returns the source lines of a schema type, without comments.
        /// </summary>
        private static List<string> Block(string name)
        {
            var lines = SchemaLines.Value;
            var start = Array.FindIndex(lines, l => Regex.IsMatch(l, $@"^(export )?declare type {name}\b"));
            start.Should().BeGreaterThanOrEqualTo(0, $"{name} should be declared in the schema");

            var block = new List<string> { lines[start] };
            for (var i = start + 1; i < lines.Length; i++)
            {
                var line = lines[i];
                if (line.StartsWith("export ", StringComparison.Ordinal) || line.StartsWith("declare ", StringComparison.Ordinal) || line.StartsWith("/**", StringComparison.Ordinal))
                {
                    break;
                }

                var trimmed = line.TrimStart();
                if (!trimmed.StartsWith('*') && !trimmed.StartsWith("/*", StringComparison.Ordinal))
                {
                    block.Add(line);
                }
            }

            return block;
        }

        private static HashSet<string> OwnKeys(string name)
        {
            return Block(name).Skip(1)
                .Select(l => Regex.Match(l.TrimStart(), @"^(\w+)\??:"))
                .Where(m => m.Success)
                .Select(m => m.Groups[1].Value)
                .ToHashSet();
        }

        /// <summary>
        /// Returns the keys of a schema type and of every named type that it references.
        /// </summary>
        private static HashSet<string> Keys(string root)
        {
            var keys = new HashSet<string>();
            var visited = new HashSet<string>();
            Visit(root);
            return keys;

            void Visit(string name)
            {
                if (!visited.Add(name))
                {
                    return;
                }

                var block = Block(name);
                keys.UnionWith(OwnKeys(name));

                foreach (Match reference in Regex.Matches(Regex.Replace(string.Join("\n", block.Skip(1).Prepend(block[0][(block[0].IndexOf('=') + 1)..])), "'[^']*'", ""), @"\b[A-Z]\w+\b"))
                {
                    if (SchemaLines.Value.Any(l => Regex.IsMatch(l, $@"^(export )?declare type {reference.Value}\b")))
                    {
                        Visit(reference.Value);
                    }
                }
            }
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

                if (type.Assembly != Library || type.IsEnum)
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
