using System.ComponentModel;

namespace WebApi.Domain.Enums
{
    public enum GuestGroup
    {
        [Description("Bride Family")] BrideFamily = 1,
        [Description("Groom Family")] GroomFamily = 2
    }
}