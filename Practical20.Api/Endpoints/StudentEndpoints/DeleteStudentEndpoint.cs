namespace Practical20.Api.Endpoints.StudentEndpoints;

// Delete endpoint for Student entity
public class DeleteStudentEndpoint(IStudentService studentService) : BaseEndpoint
{
    [HttpDelete("api/students/{id:guid}")]
    public async Task<IActionResult> HandleAsync(Guid id)
    {
        var result = await studentService.DeleteAsync(id);

        if (!result.IsSuccess)
            return NotFound(result.ErrorMessage);

        return Ok(result);
    }
}
