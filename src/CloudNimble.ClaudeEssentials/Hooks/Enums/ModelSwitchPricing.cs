using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks
{

    /// <summary>
    /// Represents where the cost estimate for a model switch came from.
    /// </summary>
    public enum ModelSwitchPricing
    {

        /// <summary>
        /// The managed <c>modelPricing</c> setting.
        /// </summary>
        [JsonStringEnumMemberName("configured")]
        Configured,

        /// <summary>
        /// List price.
        /// </summary>
        [JsonStringEnumMemberName("catalog")]
        Catalog,

        /// <summary>
        /// The target model was unknown, so the default tier was assumed.
        /// </summary>
        [JsonStringEnumMemberName("default")]
        Default

    }

}
