using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Skyfri.BL.IServices;
using Skyfri.Models;
using Skyfri.ViewModels;
using Swashbuckle.AspNetCore.Annotations;

namespace Skyfri.Controllers
{
    /// <summary>
    /// Managed operations related to plant
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PlantController : ControllerBase
    {
        private readonly IPortfolioService _portfolioService;
        private readonly IPlantService _plantService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public PlantController(IPortfolioService portfolioService,IPlantService plantService,
            IMapper mapper, ILogger<PlantController> logger)
        {
            _portfolioService = portfolioService;
            _plantService = plantService;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Get all plants for a portfolio
        /// </summary>
        /// <param name="portfolioId">UID of the portfolio</param>
        /// <returns>list of all the plants for a portfolio</returns>
        [HttpGet]
        [SwaggerOperation(OperationId = "GetAllPlants")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(IEnumerable<PlantViewModel>))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<PlantViewModel>>> GetAllPlantsByPortfolio(Guid portfolioId)
        {
            try
            {
                var plants = await _plantService.GetPlantsByPortfolioIdAsync(portfolioId);
                if(plants == default||plants.Count()<=0) {
                    return Problem(
                        statusCode: StatusCodes.Status404NotFound,
                        title: "Not found",
                        detail: $"No plant was found with portfolioId '{portfolioId}'");
                }
                return Ok(_mapper.Map<IEnumerable<PlantViewModel>>(plants));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Problem(statusCode: StatusCodes.Status500InternalServerError,
                    title: "Internal Server Error",
                    detail: ex.Message);
            }
        }

        /// <summary>
        /// Add a new plant for a portfolio
        /// </summary>
        /// <param name="portfolioId">UID of the portfolio</param>
        /// <param name="plantModel">data for the plant</param>
        /// <returns>newly created plant data</returns>
        [HttpPost]
        [Consumes("application/json")]
        [SwaggerOperation(OperationId = "AddPlant")]
        [SwaggerResponse(StatusCodes.Status201Created)]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult> AddPlant(Guid portfolioId, PlantUpdateModel plantModel)
        {
            try
            {
                var portfolio = await _portfolioService.GetPortfolioByIdAsync(portfolioId);
                if(portfolio==default)
                {
                    return Problem(
                        statusCode: StatusCodes.Status404NotFound,
                        title: "Not found",
                        detail: $"Portfolio with portfolioId '{portfolioId}' was not found");
                }
                var plant = _mapper.Map<Plant>(plantModel);
                plant.PortfolioId = portfolioId;
                var createdPlant = await _plantService.AddPlantAsync(plant);
                return StatusCode(StatusCodes.Status201Created,_mapper.Map<PlantViewModel>(createdPlant));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Problem(statusCode: StatusCodes.Status500InternalServerError,
                    title: "Internal Server Error",
                    detail: ex.Message);
            }
        }

        /// <summary>
        /// Delete a plant for a portfolio
        /// </summary>
        /// <param name="plantId">UID of the plant</param>
        /// <param name="portfolioId">UID of the portfolio</param>
        /// <returns>NA</returns>
        [HttpDelete]
        [SwaggerOperation(OperationId = "DeletePlant")]
        [SwaggerResponse(StatusCodes.Status204NoContent)]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult> DeletePlant(Guid plantId, Guid portfolioId)
        {
            try
            {
                var portfolio = await _plantService.GetPlantsByPortfolioIdAndPlantAsync(portfolioId, plantId);
                if(portfolio == null)
                {
                    return Problem(
                       statusCode: StatusCodes.Status404NotFound,
                       title: "Not found",
                       detail: $"Plant with plantId '{plantId}' and portfolioId '{portfolioId}' was not found");
                }
                await _plantService.DeletePlantAsync(plantId);
                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Problem(statusCode: StatusCodes.Status500InternalServerError,
                    title: "Internal Server Error",
                    detail: ex.Message);
            }
        }
    }
}
