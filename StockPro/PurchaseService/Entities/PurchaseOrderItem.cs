using System.Text.Json.Serialization;

public class PurchaseOrderItem
{
    public int Id { get; set; }
    public int PurchaseOrderId { get; set; }

    public int ProductId { get; set; }
    public int Quantity { get; set; }

    [JsonIgnore] 
    public PurchaseOrder? PurchaseOrder { get; set; }
}