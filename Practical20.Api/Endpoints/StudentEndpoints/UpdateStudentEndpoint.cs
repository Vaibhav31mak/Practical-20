namespace Practical20.Api.Endpoints.StudentEndpoints;

// Update endpoint for Student entity
public class UpdateStudentEndpoint(IStudentService studentService) : BaseEndpoint
{
    [HttpPut("api/students/{id:guid}")]
    public async Task<IActionResult> HandleAsync(Guid id, UpdateStudentDto dto)
    {
        var result = await studentService.UpdateAsync(id, dto);

        if (!result.IsSuccess)
            return BadRequest(result.ErrorMessage);

        return Ok(result);
    }
}
