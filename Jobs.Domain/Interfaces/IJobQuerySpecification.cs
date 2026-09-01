using System;
using System.Collections.Generic;
using System.Text;

namespace Jobs.Domain.Interfaces
{
    public interface IJobQuerySpecification
    {
        // Search
        string? SearchTerm { get; }
        
        // Job Type Filters
        string? JobTypeName { get; }
        string? JobTypeCategory { get; }
        
        // Location Filters
        string? City { get; }
        string? State { get; }
        int? ZipCode { get; }
        
        // Sorting
        string? SortBy { get; }
        string? SortDirection { get; }

        //Data shaping
        string? Fields { get; }
        
        // Pagination
        int Page { get; }
        int PageSize { get; }

  
    }
}
