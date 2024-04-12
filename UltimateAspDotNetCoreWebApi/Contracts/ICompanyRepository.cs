using Entities.Models;

namespace Contracts;

public interface ICompanyRepository
{
    IEnumerable<Company> GetAll(bool trackChanges);
}
