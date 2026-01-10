using BL.Contract;
using BL.Dtos;
using DAL.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShippingPackgingController : ControllerBase
    {
        IPackgingTypes _packgingTypes;
        public ShippingPackgingController(IPackgingTypes packgingTypes)
        {
            _packgingTypes = packgingTypes;
        }
        // GET: api/<ShippingTypesController>
        [HttpGet]
        public async Task<ActionResult<ApiResponse<List<ShippingPackgingDto>>>> Get()
        {
            try
            {
                var data = await _packgingTypes.GetAllAsync();

                return Ok(ApiResponse<List<ShippingPackgingDto>>.SuccessResponse(data));
            }
            catch(DataAccessException daEx)
            {
                return StatusCode(500, ApiResponse<List<ShippingPackgingDto>>.FailResponse
                    ("data access exception", new List<string>() { daEx.Message }));
            }
            catch(Exception ex)
            {
                return StatusCode(500, ApiResponse<List<ShippingPackgingDto>>.FailResponse
                    ("general exception", new List<string>() { ex.Message }));
            }

        }

        // GET api/<ShippingTypesController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<ShippingPackgingDto>>> Get(Guid id)
        {
            try
            {
                var data =await _packgingTypes.GetByIdAsync(id);

                return Ok(ApiResponse<ShippingPackgingDto>.SuccessResponse(data));
            }
            catch (DataAccessException daEx)
            {
                return StatusCode(500, ApiResponse<ShippingPackgingDto>.FailResponse
                    ("data access exception", new List<string>() { daEx.Message }));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<ShippingPackgingDto>.FailResponse
                    ("general exception", new List<string>() { ex.Message }));
            }
        }
    }
}
