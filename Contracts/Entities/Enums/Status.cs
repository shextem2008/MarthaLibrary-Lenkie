using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Entities.Enums
{
    public enum Status
    {
        Available = 1,
        CheckOut = 2,
        CheckIn = 3,
        Reserve = 4,
        Borrow = 5,
        OnHold = 6,     
        NotAvailable = 7,

    }
}
