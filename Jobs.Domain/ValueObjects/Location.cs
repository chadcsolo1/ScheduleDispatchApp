using System;
using System.Collections.Generic;
using System.Text;

namespace Jobs.Domain.ValueObjects
{
    public record Location(
        string AddressLine1,
        string AddressLine2,
        string City,
        string State,
        int ZipCode
    );
}
