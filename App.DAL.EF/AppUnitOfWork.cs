using App.Contracts.DAL;
using App.DAL.EF.Repositories;

namespace App.DAL.EF;

public class AppUnitOfWork : IAppUnitOfWork
{
    protected readonly AppDbContext UowDbContext;


    public AppUnitOfWork(AppDbContext uowDbContext)
    {
        UowDbContext = uowDbContext;
    }

    public async Task<int> SaveChangesAsync()
    {
        return await UowDbContext.SaveChangesAsync();
    }

    public int SaveChanges()
    {
        return UowDbContext.SaveChanges();
    }

    public ICompanyRepository Companies => new CompanyRepository(UowDbContext);
    public IEventRepository Events => new EventRepository(UowDbContext);
    public IPersonRepository Persons => new PersonRepository(UowDbContext);
}