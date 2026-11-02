using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks
{

    /// <summary>
    /// Represents why a push notification was not delivered.
    /// </summary>
    public enum PushNotificationDisabledReason
    {

        /// <summary>
        /// Push notifications are turned off in configuration.
        /// </summary>
        [JsonStringEnumMemberName("config_off")]
        ConfigOff,

        /// <summary>
        /// The user is present at the terminal.
        /// </summary>
        [JsonStringEnumMemberName("user_present")]
        UserPresent,

        /// <summary>
        /// No delivery transport is available.
        /// </summary>
        [JsonStringEnumMemberName("no_transport")]
        NoTransport

    }

}
