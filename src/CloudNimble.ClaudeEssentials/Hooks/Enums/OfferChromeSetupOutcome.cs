using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks
{

    /// <summary>
    /// Represents the outcome of offering Chrome setup to the user.
    /// </summary>
    public enum OfferChromeSetupOutcome
    {

        /// <summary>
        /// The browser was connected.
        /// </summary>
        [JsonStringEnumMemberName("connected")]
        Connected,

        /// <summary>
        /// The user declined for now.
        /// </summary>
        [JsonStringEnumMemberName("not_now")]
        NotNow,

        /// <summary>
        /// No connection attempt has been made yet.
        /// </summary>
        [JsonStringEnumMemberName("no_attempt_yet")]
        NoAttemptYet

    }

}
