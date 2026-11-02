using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks
{

    /// <summary>
    /// Represents the API error that ended a turn.
    /// </summary>
    public enum StopFailureErrorType
    {

        /// <summary>
        /// Authentication failed.
        /// </summary>
        [JsonStringEnumMemberName("authentication_failed")]
        AuthenticationFailed,

        /// <summary>
        /// The OAuth organization is not allowed.
        /// </summary>
        [JsonStringEnumMemberName("oauth_org_not_allowed")]
        OauthOrgNotAllowed,

        /// <summary>
        /// The account is on hold.
        /// </summary>
        [JsonStringEnumMemberName("account_on_hold")]
        AccountOnHold,

        /// <summary>
        /// Verification is required.
        /// </summary>
        [JsonStringEnumMemberName("verification_required")]
        VerificationRequired,

        /// <summary>
        /// A billing error occurred.
        /// </summary>
        [JsonStringEnumMemberName("billing_error")]
        BillingError,

        /// <summary>
        /// The request was rate limited.
        /// </summary>
        [JsonStringEnumMemberName("rate_limit")]
        RateLimit,

        /// <summary>
        /// The API is overloaded.
        /// </summary>
        [JsonStringEnumMemberName("overloaded")]
        Overloaded,

        /// <summary>
        /// The request was invalid.
        /// </summary>
        [JsonStringEnumMemberName("invalid_request")]
        InvalidRequest,

        /// <summary>
        /// The model was not found.
        /// </summary>
        [JsonStringEnumMemberName("model_not_found")]
        ModelNotFound,

        /// <summary>
        /// A server error occurred.
        /// </summary>
        [JsonStringEnumMemberName("server_error")]
        ServerError,

        /// <summary>
        /// An unknown error occurred.
        /// </summary>
        [JsonStringEnumMemberName("unknown")]
        Unknown,

        /// <summary>
        /// The response reached the maximum output tokens.
        /// </summary>
        [JsonStringEnumMemberName("max_output_tokens")]
        MaxOutputTokens,

        /// <summary>
        /// A cloud credential error occurred.
        /// </summary>
        [JsonStringEnumMemberName("cloud_credential_error")]
        CloudCredentialError

    }

}
