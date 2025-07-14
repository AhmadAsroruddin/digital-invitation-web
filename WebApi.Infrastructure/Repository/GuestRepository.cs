using WebApi.Application.Interfaces.Repository;
using WebApi.Domain.Entities;
using WebApi.Infrastructure.Data;

namespace WebApi.Infrastructure.Repository
{
    public class GuestRepository(AppDbContext dbContext) : Repository<Guest>(dbContext), IGuestRepository
    {

    }
}