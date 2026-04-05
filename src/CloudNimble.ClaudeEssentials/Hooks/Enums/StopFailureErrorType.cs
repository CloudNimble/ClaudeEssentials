using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks
{

    /// <summary>
    /// Represents the type of error that caused a stop failure in Claude Code.
    /// </summary>
    public enum StopFailureErrorType
    {

        /// <summary>
        /// The request was rate limited by the API.
        /// </summary>
        [JsonStringEnumMemberName("rate_limit")]
        RateLimit,

        /// <summary>
        /// Authentication with the API failed.
        /// </summary>
        [JsonStringEnumMemberName("authentication_failed")]
        AuthenticationFailed,

        /// <summary>
        /// A billing error occurred with the API account.
        /// </summary>
        [JsonStringEnumMemberName("billing_error")]
        BillingError,

        /// <summary>
        /// The API request was invalid.
        /// </summary>
        [JsonStringEnumMemberName("invalid_request")]
        InvalidRequest,

        /// <summary>
        /// An internal server error occurred on the API side.
        /// </summary>
        [JsonStringEnumMemberName("server_error")]
        ServerError,

        /// <summary>
        /// The maximum output token limit was reached.
        /// </summary>
        [JsonStringEnumMemberName("max_output_tokens")]
        MaxOutputTokens,

        /// <summary>
        /// An unknown error type occurred.
        /// </summary>
        [JsonStringEnumMemberName("unknown")]
        Unknown

    }

}
