using System;
using System.Collections.Generic;
using System.Text;
using Organization.Domain.Entities;

namespace Organization.Domain.Interfaces
{
    public interface IOrganizationRepository
    {
        Task<Organization?> GetByIdAsync(Guid id);
        Task<List<Organization>> GetAllAsync();
        Task AddAsync(Organization organization);
        Task UpdateAsync(Organization organization);

        Task DeleteAsync(Organization organization);
    }
}
