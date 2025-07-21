using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using WebApi.Application.DTOs.Request.CheckIn;
using WebApi.Application.DTOs.Request.Guest;
using WebApi.Application.DTOs.Request.GuestSubEvent;
using WebApi.Application.DTOs.Request.RSVP;
using WebApi.Application.DTOs.Response;
using WebApi.Application.Exceptions;
using WebApi.Application.Interfaces.Repository;
using WebApi.Application.Interfaces.Service;
using WebApi.Infrastructure.Data;

public class ConsumeDataService : IConsumeDataService
{
    private readonly HttpClient _httpClient;
    private readonly IGuestService _guestService;
    private readonly ISubEventRepository _subEventRepository;
    private readonly IGuestSubEventService _guestSubEventService;
    private readonly IRSVPService _rsvpService;
    private readonly ICheckInService _checkinService;
    private readonly AppDbContext _dbContext;
    private readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions
    {
        PropertyNameCaseInsensitive = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };


    public ConsumeDataService(
        IHttpClientFactory httpClientFactory,
        IGuestService guestService,
        ISubEventRepository subEventRepository,
        IGuestSubEventService guestSubEventService,
        IRSVPService rsvpService,
        ICheckInService checkinService,
        AppDbContext dbContext
    )
    {
        _httpClient = httpClientFactory.CreateClient("ConsumeDataClient");
        _guestService = guestService;
        _subEventRepository = subEventRepository;
        _guestSubEventService = guestSubEventService;
        _rsvpService = rsvpService;
        _checkinService = checkinService;
        _dbContext = dbContext;
    }

    public async Task<bool> PullData(int eventId)
    {
        using var transaction = await _dbContext.Database.BeginTransactionAsync();

        try
        {
            var response = await _httpClient.GetAsync("c/0e23-17c0-49ad-a57e");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            var results = JsonSerializer.Deserialize<List<ConsumeDataResponse>>(json, _jsonOptions);

            var subEvents = (await _subEventRepository.GetAllAsync(
                e => e.EventId == eventId,
                includeProperties: ["Event"]
            ))?.ToList() ?? throw new NotFoundException("Event doesn't have sub event yet");

            foreach (var result in results ?? [])
            {
                var guest = await _guestService.CreateAsync(eventId, new SaveGuestRequest
                {
                    Name = result.Name,
                    Phone = "",
                    InvitedBy = "",
                    GuestGroup = result.Group
                });

                if (result.Event1RSVP != null || result.Event1RSVP > 0)
                {
                    Console.WriteLine(subEvents[0]);
                    var guestSubEvent1 = await _guestSubEventService.CreateAsync(subEvents[0].Id, new SaveGuestSubEventRequest
                    {
                        GuestId = guest.Id,
                    });

                    await _rsvpService.CreateAsync(guestSubEvent1.Id, new SaveRSVPRequest
                    {
                        Status = "attending",
                        PaxConfirmed = (int)result.Event1RSVP
                    });

                    if (result.Event1Attend != null || result.Event1Attend > 0)
                    {    
                        await _checkinService.CreateAsync(new SaveCheckInRequest
                            {
                                GuestSubEventId = guestSubEvent1.Id,
                                PaxActual = (int)result.Event1Attend!,
                                GiftQty = 0,
                                SouvenirQty = 0,
                            });
                    }
                }

                if (result.Event2RSVP != null || result.Event2RSVP > 0 || subEvents.Count > 1)
                {
                    var guestSubEvent2 = await _guestSubEventService.CreateAsync(subEvents[1].Id, new SaveGuestSubEventRequest
                    {
                        GuestId = guest.Id,
                    });

                    await _rsvpService.CreateAsync(guestSubEvent2.Id, new SaveRSVPRequest
                    {
                        Status = "attending",
                        PaxConfirmed = (int)result.Event2RSVP!
                    });

                    if (result.Event2Attend != null || result.Event2Attend > 0)
                    {
                        await _checkinService.CreateAsync(new SaveCheckInRequest
                            {
                                GuestSubEventId = guestSubEvent2.Id,
                                PaxActual = (int)result.Event2Attend!,
                                GiftQty = result.Event2GiftCount ?? 0,
                                SouvenirQty = result.Event2Souvenir ?? 0,
                            });
                    }
                }
            }

            await transaction.CommitAsync();
            return true;
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            Console.WriteLine($"❌ Rollback: {ex.Message}");
            throw;
        }
    }
}
