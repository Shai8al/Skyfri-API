using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Skyfri.BL.IServices;
using Skyfri.Models;
using Skyfri.ViewModels;
using Swashbuckle.AspNetCore.Annotations;


namespace Skyfri.Controllers
{
    /// <summary>
    /// Managed operations related to portfolio
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PortfolioController : ControllerBase
    {
        private readonly IPortfolioService _portfolioService;
        private readonly ITenantService _tenantService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public PortfolioController(IPortfolioService portfolioService, ITenantService tenantService, IMapper mapper, ILogger<PortfolioController> logger)
        {
            _logger = logger;
            _portfolioService = portfolioService;
            _tenantService = tenantService;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all the portfolios for a tenant
        /// </summary>
        /// <param name="tenantId"></param>
        /// <returns>List of portfolios</returns>
        [HttpGet]
        [SwaggerOperation(OperationId = "GetAllPortfolios")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(IEnumerable<PortfolioViewModel>))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<PortfolioViewModel>>> GetAllPortfoliosForTenant(Guid tenantId)
        {
            try
            {
                var portfolios = await _portfolioService.GetPortfolioByTenantIdAsync(tenantId);
                if (portfolios == default || portfolios.Count() <= 0)
                {
                    return Problem(
                        statusCode: StatusCodes.Status404NotFound,
                        title: "Not found",
                        detail: $"No Portfolio found with tenantId '{tenantId}'");
                }
                return Ok(_mapper.Map<IEnumerable<PortfolioViewModel>>(portfolios));
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
        /// Add a new portfolio for a tenant
        /// </summary>
        /// <param name="portfolioModel">Details of the portfolio</param>
        /// <param name="tenantId"></param>
        /// <returns>newly created portfolio data</returns>
        [HttpPost]
        [Consumes("application/json")]
        [SwaggerOperation(OperationId = "AddPortfolio")]
        [SwaggerResponse(StatusCodes.Status201Created, Type = typeof(PortfolioViewModel))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult> AddPortfolio(PortfolioUpdateModel portfolioModel, Guid tenantId)
        {
            try
            {
                var tenant = await _tenantService.GetTenantByIdAsync(tenantId);
                if (tenant == default)
                {
                    return Problem(
                        statusCode: StatusCodes.Status404NotFound,
                        title: "Not found",
                        detail: $"Tenant with tenantId '{tenantId}' was not found");
                }
                var portfolio = _mapper.Map<Portfolio>(portfolioModel);
                portfolio.TenantId = tenantId;
                var createdPortfolio = await _portfolioService.AddPortfolioAsync(portfolio);
                return StatusCode(StatusCodes.Status201Created, _mapper.Map<PortfolioViewModel>(createdPortfolio));
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
        /// Delete a portfolio for a tenant
        /// </summary>
        /// <param name="tenantId">UID of the tenant</param>
        /// <param name="portfolioId">UID of the portfolio</param>
        /// <returns></returns>
        [HttpDelete]
        [SwaggerOperation(OperationId = "DeletePortfolio")]
        [SwaggerResponse(StatusCodes.Status204NoContent)]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult> DeletePortfolio(Guid tenantId, Guid portfolioId)
        {
            try
            {
                var portfolio = await _portfolioService.GetByTenantAndPortfolioIdAsync(tenantId, portfolioId);
                if (portfolio == default)
                {
                    return Problem(
                        statusCode: StatusCodes.Status404NotFound,
                        title: "Not found",
                        detail: $"Portfolio with tenantId '{tenantId}' and portfolioId '{portfolioId}' was not found");
                }
                await _portfolioService.DeletePortfolioAsync(portfolioId);
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
