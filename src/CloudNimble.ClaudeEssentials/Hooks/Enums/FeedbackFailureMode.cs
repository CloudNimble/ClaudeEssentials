using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks
{

    /// <summary>
    /// Represents the closest failure mode for feedback about model behavior.
    /// </summary>
    public enum FeedbackFailureMode
    {

        /// <summary>
        /// The model did not follow instructions.
        /// </summary>
        [JsonStringEnumMemberName("instruction_following")]
        InstructionFollowing,

        /// <summary>
        /// The model took destructive actions.
        /// </summary>
        [JsonStringEnumMemberName("destructive_actions")]
        DestructiveActions,

        /// <summary>
        /// The model produced poor quality code.
        /// </summary>
        [JsonStringEnumMemberName("code_quality")]
        CodeQuality,

        /// <summary>
        /// The model repeated itself or looped.
        /// </summary>
        [JsonStringEnumMemberName("repetition_and_looping")]
        RepetitionAndLooping,

        /// <summary>
        /// The model regressed compared to earlier behavior.
        /// </summary>
        [JsonStringEnumMemberName("model_regression")]
        ModelRegression,

        /// <summary>
        /// The model was overconfident or hallucinated.
        /// </summary>
        [JsonStringEnumMemberName("overconfidence_and_hallucination")]
        OverconfidenceAndHallucination,

        /// <summary>
        /// The model mishandled context or memory.
        /// </summary>
        [JsonStringEnumMemberName("context_and_memory")]
        ContextAndMemory,

        /// <summary>
        /// The model was overeager.
        /// </summary>
        [JsonStringEnumMemberName("overeager")]
        Overeager,

        /// <summary>
        /// The model over-corrected.
        /// </summary>
        [JsonStringEnumMemberName("over_correction")]
        OverCorrection,

        /// <summary>
        /// The model stopped short of finishing.
        /// </summary>
        [JsonStringEnumMemberName("stopping_short")]
        StoppingShort,

        /// <summary>
        /// The model disputed or declined a reasonable request.
        /// </summary>
        [JsonStringEnumMemberName("dispute_or_decline")]
        DisputeOrDecline,

        /// <summary>
        /// The model spawned more subagents than warranted.
        /// </summary>
        [JsonStringEnumMemberName("subagent_overspawn")]
        SubagentOverspawn,

        /// <summary>
        /// The model's tone was off or preachy.
        /// </summary>
        [JsonStringEnumMemberName("tone_or_preachiness")]
        ToneOrPreachiness,

        /// <summary>
        /// The model asked too many questions.
        /// </summary>
        [JsonStringEnumMemberName("excessive_questions")]
        ExcessiveQuestions,

        /// <summary>
        /// The model expanded scope unprompted.
        /// </summary>
        [JsonStringEnumMemberName("unwanted_scope")]
        UnwantedScope,

        /// <summary>
        /// A model-behavior issue that fits no listed value.
        /// </summary>
        [JsonStringEnumMemberName("other")]
        Other

    }

}
