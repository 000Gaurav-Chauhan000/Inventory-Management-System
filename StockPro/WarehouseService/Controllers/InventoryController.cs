using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/inventory")]
public class InventoryController : ControllerBase
{
    private readonly IInventoryService _service;

    public InventoryController(IInventoryService service)
    {
        _service = service;
    }

    [HttpPost("add")]
    public async Task<IActionResult> AddStock(int productId, int warehouseId, int quantity)
    {
        await _service.AddStockAlgo(productId, warehouseId, quantity);
        return Ok("Stock added");
    }

    [HttpPost("remove")]
    public async Task<IActionResult> RemoveStock(int productId, int warehouseId, int quantity)
    {
        await _service.RemoveStockAlgo(productId, warehouseId, quantity);
        return Ok("Stock removed");
    }

    [HttpGet("product/{productId}")]
    public async Task<IActionResult> GetByProduct(int productId)
    {
        return Ok(await _service.GetByProductAlgo(productId));
    }

    [HttpGet("warehouse/{warehouseId}")]
    public async Task<IActionResult> GetByWarehouse(int warehouseId)
    {
        return Ok(await _service.GetByWarehouseAlgo(warehouseId));
    }
}