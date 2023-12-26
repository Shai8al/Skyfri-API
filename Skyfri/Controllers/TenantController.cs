using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Skyfri.BL.IServices;
using Skyfri.Models;
using Skyfri.ViewModels;
using Swashbuckle.AspNetCore.Annotations;

namespace Skyfri.Controllers
{
    /// <summary>
    /// Manage operations related to tenant
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class TenantController : ControllerBase
    {
        private readonly ITenantService _tenantService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public TenantController(ITenantService tenantService, IMapper mapper, ILogger<TenantController> logger)
        {
            _tenantService = tenantService;
            _mapper = mapper;
            _logger = logger;

        }

        /// <summary>
        /// Get all the tenants listed
        /// </summary>
        /// <returns>List of tenants</returns>
        [HttpGet]
        [SwaggerOperation(OperationId = "GetAllTenants")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(IEnumerable<TenantViewModel>))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<TenantViewModel>>> GetAllTenants()
        {
            try
            {
                var tenants = await _tenantService.GetAllTenantsAsync();
                return Ok(_mapper.Map<IEnumerable<TenantViewModel>>(tenants));
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
        /// Get a specific tenant data
        /// </summary>
        /// <param name="tenantId">UID of the tenant</param>
        /// <returns>Data of the specific tenant</returns>
        [HttpGet("{tenantId}")]
        [SwaggerOperation(OperationId = "GetTenantData")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(TenantViewModel))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<TenantViewModel>> GetTenantById(Guid tenantId)
        {
            try
            {
                var tenant = await _tenantService.GetTenantByIdAsync(tenantId);
                if (tenant == default)
                {
                    return Problem(
                        statusCode: StatusCodes.Status404NotFound,
                        title: "Not found",
                        detail: $"Tenant with tenantId '{tenantId}' not found");
                }
                return Ok(_mapper.Map<TenantViewModel>(tenant));
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
        /// Add a tenant
        /// </summary>
        /// <param name="tenantModel">Details of the tenant</param>
        /// <returns>newly created tenant data</returns>
        [HttpPost]
        [Consumes("application/json")]
        [SwaggerOperation(OperationId = "AddTenant")]
        [SwaggerResponse(StatusCodes.Status201Created, Type = typeof(TenantViewModel))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<TenantViewModel>> AddTenant(TenantUpdateModel tenantModel)
        {
            try
            {
                var tenantEntity = _mapper.Map<Tenant>(tenantModel);
                var createdTenant = await _tenantService.AddTenantAsync(tenantEntity);
                return StatusCode(StatusCodes.Status201Created, _mapper.Map<TenantViewModel>(createdTenant));
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
        /// Edit an existing tenant data
        /// </summary>
        /// <param name="tenantId">UID of the tenant</param>
        /// <param name="tenantViewModel">Updated data for the tenant</param>
        /// <returns>updated tenant data</returns>
        [HttpPut("{tenantId}")]
        [Consumes("application/json")]
        [SwaggerOperation(OperationId = "UpdateTenant")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(TenantViewModel))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<TenantViewModel>> EditTenant(Guid tenantId, TenantUpdateModel tenantViewModel)
        {
            try
            {
                var tenantEntity = await _tenantService.GetTenantByIdAsync(tenantId);
                if (tenantEntity == default)
                {
                    return Problem(
                       statusCode: StatusCodes.Status404NotFound,
                       title: "Not found",
                       detail: $"Tenant with tenantId '{tenantId}' not found");
                }
                var updatedTenant = _mapper.Map<Tenant>(tenantViewModel);
                updatedTenant = await _tenantService.UpdateTenantAsync(updatedTenant, tenantEntity);
                return Ok(_mapper.Map<TenantViewModel>(updatedTenant));
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
        /// Deletes a tenant
        /// </summary>
        /// <param name="tenantId">UID of the tenant to be deleted</param>
        /// <returns>NA</returns>
        [HttpDelete("{tenantId}")]
        [SwaggerOperation(OperationId = "DeleteTenant")]
        [SwaggerResponse(StatusCodes.Status204NoContent)]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult> DeleteTenant(Guid tenantId)
        {
            try
            {
                var tenantEntity = await _tenantService.GetTenantByIdAsync(tenantId);
                if (tenantEntity == default)
                {
                    return Problem(
                       statusCode: StatusCodes.Status404NotFound,
                       title: "Not found",
                       detail: $"Tenant with tenantId '{tenantId}' not found");
                }
                await _tenantService.DeleteTenantAsync(tenantId);
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
