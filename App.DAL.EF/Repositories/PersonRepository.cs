using App.Contracts.DAL;
using App.Domain;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class PersonRepository : BaseEntityRepository<Person, AppDbContext>, IPersonRepository
{
    public PersonRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
}