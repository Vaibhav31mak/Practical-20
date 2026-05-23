namespace Practical20.Api.Endpoints.StudentEndpoints;

// Get all students endpoint
public class GetAllStudentsEndpoint(IStudentService studentService) : BaseEndpoint
{
    [HttpGet("api/students")]
    public async Task<IActionResult> HandleAsync()
    {
        var result = await studentService.GetAllAsync();

        if (!result.IsSuccess)
            return BadRequest(result.ErrorMessage);

        return Ok(result.Value);
    }
}
