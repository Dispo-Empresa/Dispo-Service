using Dispo.API.ResponseBuilder;
using Dispo.Shared.Core.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dispo.API.Controllers
{
    [Route("api/v1/datatable")]
    [ApiController]
    [Authorize]
    public class DatatableController : ControllerBase
    {
        private readonly IDatatableRepository _datatableRepository;

        public DatatableController(IDatatableRepository datatableRepository)
        {
            _datatableRepository = datatableRepository;
        }

        [HttpGet("get-count")]
        public IActionResult GetCount()
        {
            var a = _datatableRepository.GetTotalRecords();

            return Ok(new ResponseModelBuilder().WithData(a)
                        .WithSuccess(true)
                        .WithAlert(AlertType.Success)
                        .Build());
        }

        [HttpGet("get-all")]
        public IActionResult Get([FromQuery] PaginationFilter paginationFilter)
        {
            dynamic datatableData = null;

            if (paginationFilter.Entity == "manufacturer")
            {
                datatableData = _datatableRepository.GetToDatatableManufacturer(paginationFilter.PageNumber, paginationFilter.PageSize).ToList();
            }
            else if (paginationFilter.Entity == "product")
            {
                datatableData = _datatableRepository.GetToDatatableProduct(paginationFilter.PageNumber, paginationFilter.PageSize).ToList();
            }
            else if (paginationFilter.Entity == "supplier")
            {
                datatableData = _datatableRepository.GetToDatatableSupplier(paginationFilter.PageNumber, paginationFilter.PageSize).ToList();
            }


            return Ok(new ResponseModelBuilder().WithData(datatableData)
                                                .WithSuccess(true)
                                                .WithAlert(AlertType.Success)
                                                .Build());
        }

        [HttpPost("get-by-filter")]
        public IActionResult GetByFilter([FromBody] FilterModel filterModel)
        {
            return Ok();
        }
    }

    public class PaginationFilter
    {
        public string Entity { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        //public PaginationFilter()
        //{
        //    this.PageNumber = 1;
        //    this.PageSize = 10;
        //}
        //public PaginationFilter(int pageNumber, int pageSize)
        //{
        //    this.PageNumber = pageNumber < 1 ? 1 : pageNumber;
        //    this.PageSize = pageSize > 10 ? 10 : pageSize;
        //}
    }

    public class FilterModel
    {
        public FilterPropertiesModel Properties { get; set; }
        public PaginationFilter PaginationConfig { get; set; }
    }

    public class FilterPropertiesModel
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }

    }
}
