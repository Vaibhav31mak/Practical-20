namespace Practical20.Api.Endpoints.AuditLogEndpoints;

// Get all audit logs endpoint
public class GetAllAuditLogsEndpoint(IAuditLogService auditLogService) : BaseEndpoint
{
    [HttpGet("api/audit-logs")]
    public async Task<IActionResult> HandleAsync()
    {
        var result = await auditLogService.GetAllAsync();

        if (!result.IsSuccess)
            return BadRequest(result.ErrorMessage);

        return Ok(result.Value);
    }
}
