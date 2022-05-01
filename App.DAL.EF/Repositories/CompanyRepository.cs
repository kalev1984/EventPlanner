using App.Contracts.DAL;
using App.Domain;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class CompanyRepository : BaseEntityRepository<Company, AppDbContext>, ICompanyRepository
{
    public CompanyRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
}