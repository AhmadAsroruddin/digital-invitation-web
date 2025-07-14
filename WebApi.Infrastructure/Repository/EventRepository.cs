using WebApi.Application.Interfaces.Repository;
using WebApi.Domain.Entities;
using WebApi.Infrastructure.Data;

namespace WebApi.Infrastructure.Repository
{
    public class EventRepository(AppDbContext dbContext) : Repository<Event>(dbContext), IEventRepository
    {

    }
}