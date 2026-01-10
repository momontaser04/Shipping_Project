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
    public class CitiesController : ControllerBase
    {
        ICities _city;
        public CitiesController(ICities city)
        {
            _city = city;
        }
        // GET: api/<cityController>
        [HttpGet]
        public async Task<ActionResult<ApiResponse<List<CityDto>>>> Get()
        {
            try
            {
                var data = await _city.GetAllAsync();

                return Ok(ApiResponse<List<CityDto>>.SuccessResponse(data));
            }
            catch(DataAccessException daEx)
            {
                return StatusCode(500, ApiResponse<List<CityDto>>.FailResponse
                    ("data access exception", new List<string>() { daEx.Message }));
            }
            catch(Exception ex)
            {
                return StatusCode(500, ApiResponse<List<CityDto>>.FailResponse
                    ("general exception", new List<string>() { ex.Message }));
            }

        }

        // GET api/<cityController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<CityDto>>> Get(Guid id)
        {
            try
            {
                var data =await _city.GetByIdAsync(id);

                return Ok(ApiResponse<CityDto>.SuccessResponse(data));
            }
            catch (DataAccessException daEx)
            {
                return StatusCode(500, ApiResponse<CityDto>.FailResponse
                    ("data access exception", new List<string>() { daEx.Message }));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<CityDto>.FailResponse
                    ("general exception", new List<string>() { ex.Message }));
            }
        }

        [HttpGet("GetByCountry/{id}")]
        public ActionResult<ApiResponse<CityDto>> GetByCountry(Guid id)
        {
            try
            {
                var data = _city.GetByCountry(id);

                return Ok(ApiResponse<List<CityDto>>.SuccessResponse(data));
            }
            catch (DataAccessException daEx)
            {
                return StatusCode(500, ApiResponse<List<CityDto>>.FailResponse
                    ("data access exception", new List<string>() { daEx.Message }));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<List<CityDto>>.FailResponse
                    ("general exception", new List<string>() { ex.Message }));
            }
        }
    }
}
