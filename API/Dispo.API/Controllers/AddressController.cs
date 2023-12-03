using Dispo.API.ResponseBuilder;
using Dispo.Product.Core.Application.Interfaces;
using Dispo.Shared.Core.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dispo.API.Controllers
{
    [Route("/api/v1/address")]
    [ApiController]
    [Authorize(Roles = RolesManager.AllRoles)]
    public class AddressController : ControllerBase
    {
        private readonly IAddressService addressService;

        public AddressController(IAddressService addressRepository)
        {
            addressService = addressRepository;
        }

        [HttpGet("get-formatted-addresses")]
        public IActionResult GetAll()
        {
            try
            {
                var addresses = addressService.GetFormattedAddresses();

                return Ok(new ResponseModelBuilder().WithData(addresses)
                                                    .WithSuccess(true)
                                                    .Build());
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseModelBuilder().WithMessage(ex.Message)
                                                            .WithSuccess(false)
                                                            .Build());
            }
        }
    }
}