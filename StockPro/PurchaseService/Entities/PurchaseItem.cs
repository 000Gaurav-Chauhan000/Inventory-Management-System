public class PurchaseOrder
{
    public int Id { get; set; }
    public string SupplierName { get; set; }
    public DateTime OrderDate { get; set; } = DateTime.UtcNow;
    public string Status { get; set; } = "Pending";

    public List<PurchaseOrderItem> Items { get; set; }
}