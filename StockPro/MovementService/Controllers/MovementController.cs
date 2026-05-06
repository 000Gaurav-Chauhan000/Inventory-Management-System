using Microsoft.AspNetCore.Mvc; 
[ApiController]
[Route("api/movements")]
public class MovementController : ControllerBase
{
    private readonly IMovementService _service;

    public MovementController(IMovementService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> RecordMovement(CreateMovementDto dto)
    {
        await _service.RecordMovementAsync(dto);
        return Ok("Movement recorded");
    }

    [HttpGet("product/{productId}")]
    public async Task<IActionResult> GetByProduct(int productId)
    {
        var data = await _service.GetByProductAsync(productId);
        return Ok(data);
    }
}