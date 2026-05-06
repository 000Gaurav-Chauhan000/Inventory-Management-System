using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/purchase")]
public class PurchaseController : ControllerBase
{
    private readonly IPurchaseService _service;

    public PurchaseController(IPurchaseService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Create(PurchaseOrder order)
        => Ok(await _service.CreatePurchaseOrderAlgo(order));

    [HttpGet]
    public async Task<IActionResult> GetAll()
        => Ok(await _service.GetAllAlgo());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
        => Ok(await _service.GetByIdAlgo(id));

    [HttpPost("{id}/approve")]
    public async Task<IActionResult> Approve(int id)
    {
        await _service.ApprovePurchaseOrderAlgo(id);
        return Ok("Approved");
    }

    [HttpPost("{id}/receive")]
    public async Task<IActionResult> Receive(int id)
    {
        await _service.ReceivePurchaseOrderAlgo(id);
        return Ok("Received & Stock Updated");
    }
}