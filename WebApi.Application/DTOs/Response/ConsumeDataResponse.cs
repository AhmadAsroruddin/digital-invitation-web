namespace WebApi.Application.DTOs.Response;

public class ConsumeDataResponse
{
    public Guid Id { get; set; }
    public string? Name { get; set; } = default!;
    public string? Group { get; set; } = default!;

    public int? Event1Quota { get; set; }
    public int? Event2Quota { get; set; }

    public int? Event1RSVP { get; set; }
    public int? Event2RSVP { get; set; }

    public int? Event1Attend { get; set; }
    public int? Event2Attend { get; set; }

    public int? Event2AngpaoCount { get; set; }
    public int? Event2GiftCount { get; set; }
    public int? Event2Souvenir { get; set; }

    public string? Notes { get; set; }
}
