using System.Text;
using System.Text.Json;

public class PurchaseServiceImpl : IPurchaseService
{
    private readonly IPurchaseRepository _repo;
    private readonly HttpClient _httpClient;

    public PurchaseServiceImpl(IPurchaseRepository repo, HttpClient httpClient)
    {
        _repo = repo;
        _httpClient = httpClient;
    }

    public async Task<PurchaseOrder> CreatePurchaseOrderAlgo(PurchaseOrder order)
    {
        await _repo.AddAsync(order);
        return order;
    }

    public async Task<IEnumerable<PurchaseOrder>> GetAllAlgo()
        => await _repo.GetAllAsync();

    public async Task<PurchaseOrder?> GetByIdAlgo(int id)
        => await _repo.GetByIdAsync(id);

    public async Task ApprovePurchaseOrderAlgo(int id)
    {
        var po = await _repo.GetByIdAsync(id);
        if (po == null) throw new Exception("PO not found");

        po.Status = "Approved";
        await _repo.UpdateAsync(po);
    }

    public async Task ReceivePurchaseOrderAlgo(int id)
    {
        var po = await _repo.GetByIdAsync(id);
        if (po == null) throw new Exception("PO not found");

        if (po.Status != "Approved")
            throw new Exception("PO must be approved first");

        foreach (var item in po.Items)
        {
            var url = $"http://localhost:xxxx/api/inventory/add?productId={item.ProductId}&warehouseId=1&quantity={item.Quantity}";
            await _httpClient.PostAsync(url, null);
        }

        po.Status = "Received";
        await _repo.UpdateAsync(po);
    }
}