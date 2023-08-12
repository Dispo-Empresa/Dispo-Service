using Dispo.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dispo.Service.Services.Interfaces
{
    public interface IAddressService
    {
        IList<WarehouseAddressDto> GetFormattedAddresses();
    }
}
