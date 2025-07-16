using WebApi.Application.Interfaces.Repository;
using WebApi.Domain.Entities;
using WebApi.Infrastructure.Data;

namespace WebApi.Infrastructure.Repository
{
    public class GuestListConfigRepository(AppDbContext dbContext) : Repository<GuestlistConfig>(dbContext), IGuestlistConfigRespository
    {
        
    }
}