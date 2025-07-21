using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace WebApi.Domain.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum RSVPStatus
    {
        [EnumMember(Value = "attending")]
        Attending = 1,

        [EnumMember(Value = "not_attending")]
        NotAttending = 2
    }
}
