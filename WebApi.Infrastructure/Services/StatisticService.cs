using AutoMapper;
using WebApi.Application.DTOs.Response;
using WebApi.Application.Exceptions;
using WebApi.Application.Interfaces.Repository;
using WebApi.Application.Interfaces.Service;
using WebApi.Domain.Entities;
using WebApi.Domain.Enums;

namespace WebApi.Infrastructure.Services
{
    public class StatisticService(IMapper mapper, ISubEventRepository subEventRepository, IGuestSubEventRepository guestSubEventRepository, IRSPVRepository rSPVRepository, ICheckInRepository checkInRepository, IEventRepository eventRepository) : IStatisticService
    {
        private readonly IMapper mapper = mapper;
        private readonly ISubEventRepository subEventRepository = subEventRepository;
        private readonly IGuestSubEventRepository guestSubEventRepository = guestSubEventRepository;
        private readonly IRSPVRepository rSPVRepository = rSPVRepository;
        private readonly ICheckInRepository checkInRepository = checkInRepository;
        private readonly IEventRepository eventRepository = eventRepository;

        public async Task<SubEventStatisticResponse> GetSubEventStatistic(int subEventId)
        {
            var subEvent = await subEventRepository.GetOneAsync(e => e.Id == subEventId, includeProperties: ["Event"]) ?? throw new NotFoundException("SubEvent");
            var guestSubEvents = await guestSubEventRepository.GetAllAsync(e => e.SubEventId == subEventId);
            var allRSVP = await rSPVRepository.GetAllAsync(e => e.GuestSubEvent
            !.SubEventId == subEventId, includeProperties: ["GuestSubEvent.SubEvent"]);
            var checkIns = await checkInRepository.GetAllAsync(includeProperties: ["GuestSubEvent", "GuestSubEvent.Guest"]);

            var totalRSVPAttending = 0;
            var totalRSVPNotAttending = 0;

            var totalCheckins = 0;
            var totalPeopleCheckin = 0;
            var totalGift = 0;
            var totalSouvenir = 0;

            foreach (var guestSubEvent in guestSubEvents)
            {
                var rsvp = allRSVP.FirstOrDefault(e => e.GuestSubEventId == guestSubEvent.Id);
                var checkin = checkIns.FirstOrDefault(e => e.GuestSubEventId == guestSubEvent.Id);

                if (rsvp?.Status == RSVPStatus.Attending)
                {
                    totalRSVPAttending += 1;
                }

                if (rsvp?.Status == RSVPStatus.NotAttending)
                {
                    totalRSVPNotAttending += 1;
                }

                if (checkin != null)
                {
                    totalCheckins += 1;
                    totalPeopleCheckin += checkin?.PaxActual ?? 0;
                    totalGift += checkin?.GiftQty ?? 0;
                    totalSouvenir += checkin?.SouvenirQty ?? 0;
                }
            }

            return new SubEventStatisticResponse
            {
                Event = mapper.Map<EventResponse>(subEvent.Event),
                EventId = subEvent.EventId,
                SubEvent = mapper.Map<SubEventResponse>(subEvent),
                TotalInvitedGuest = guestSubEvents.Count(),
                TotalRSVPGuest = totalRSVPAttending + totalRSVPNotAttending,
                TotalAttendingRSVPGuest = totalRSVPAttending,
                TotalNotAttendingRSVPGuest = totalRSVPNotAttending,
                TotalCheckins = totalCheckins,
                TotalPeopleCheckin = totalPeopleCheckin,
                TotalGift = totalGift,
                TotalSouvenir = totalSouvenir
            };
        }

        public async Task<EventStatisticResponse> GetEventStatistic(int eventId)
        {
            var subEvents = await subEventRepository.GetAllAsync(e => e.EventId == eventId, includeProperties:["Event"]) ?? throw new NotFoundException("Sub Event");

            var totalInvitedGuest = 0;
            var totalRSVPGuest = 0;
            var totalAttendingRSVPGuest = 0;
            var totalNotAttendingRSVPGuest = 0;
            var totalCheckins = 0;
            var totalPeopleCheckin = 0;
            var totalGift = 0;
            var totalSouvenir = 0;

            foreach (var subEvent in subEvents)
            {
                Console.WriteLine(subEvent.Id);
                var subEventData = await GetSubEventStatistic(subEvent.Id);
                totalInvitedGuest += subEventData.TotalInvitedGuest;
                totalRSVPGuest += subEventData.TotalRSVPGuest;
                totalAttendingRSVPGuest += subEventData.TotalAttendingRSVPGuest;
                totalNotAttendingRSVPGuest += subEventData.TotalNotAttendingRSVPGuest;
                totalCheckins += subEventData.TotalCheckins;
                totalPeopleCheckin += subEventData.TotalPeopleCheckin;
                totalGift += subEventData.TotalGift;
                totalSouvenir += subEventData.TotalSouvenir;
            }

            return new EventStatisticResponse
            {
                Event = mapper.Map<EventResponse>(subEvents.FirstOrDefault()!.Event),
                EventId = eventId,
                TotalInvitedGuest = totalInvitedGuest,
                TotalRSVPGuest = totalAttendingRSVPGuest + totalNotAttendingRSVPGuest,
                TotalAttendingRSVPGuest = totalAttendingRSVPGuest,
                TotalNotAttendingRSVPGuest = totalNotAttendingRSVPGuest,
                TotalCheckins = totalCheckins,
                TotalPeopleCheckin = totalPeopleCheckin,
                TotalGift = totalGift,
                TotalSouvenir = totalSouvenir
            };
        }
    }
}