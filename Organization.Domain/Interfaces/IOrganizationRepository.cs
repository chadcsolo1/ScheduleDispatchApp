using System;
using System.Collections.Generic;
using System.Text;
using Organizations.Domain.Entities;

namespace Organizations.Domain.Interfaces
{
    public interface IOrganizationRepository
    {
        Task<Organization?> GetByIdAsync(Guid id);
        Task AddAsync(Organization organization);
        Task UpdateAsync(Organization organization);

        Task DeleteAsync(Organization organization);
    }
}
