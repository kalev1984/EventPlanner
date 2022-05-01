using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface IAppUnitOfWork : IUnitOfWork
{
    ICompanyRepository Companies { get; }
    IEventRepository Events { get; }
    IPersonRepository Persons { get; }
}