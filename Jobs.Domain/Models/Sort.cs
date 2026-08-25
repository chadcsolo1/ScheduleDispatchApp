using System;
using System.Collections.Generic;
using System.Text;

namespace Jobs.Domain.Models
{
    public class Sort
    {
        public string? SortBy
        {
            get; set;
        }
        public string? SortDirection { get; set; }
    }
}
