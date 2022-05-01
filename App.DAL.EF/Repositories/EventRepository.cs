using App.Contracts.DAL;
using App.Domain;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class EventRepository : BaseEntityRepository<Event, AppDbContext>, IEventRepository
{
    public EventRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

    public override async Task<Event?> FirstOrDefaultAsync(int id, bool noTracking = true)
    {
        var query = RepoDbSet.AsQueryable();

        if (noTracking)
        {
            query = query.AsNoTracking();
        }

        return await query
            .Include(p => p.Persons)
            .Include(c => c.Companies)
            .FirstOrDefaultAsync(i => i.Id.Equals(id));
    }
}