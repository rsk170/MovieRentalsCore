using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VidlyCore.Services.Dto
{
    public class NewRentalDto
    {
        public int CustomerId { get; set; }
        public List<int> MovieIds { get; set; }
    }
}
