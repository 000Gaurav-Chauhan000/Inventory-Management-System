public interface IPurchaseService
{
    Task<PurchaseOrder> CreatePurchaseOrderAlgo(PurchaseOrder order);
    Task<PurchaseOrder?> GetByIdAlgo(int id);
    Task<IEnumerable<PurchaseOrder>> GetAllAlgo();

    Task ApprovePurchaseOrderAlgo(int id);
    Task ReceivePurchaseOrderAlgo(int id);
}