public class StockMovement
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public int WarehouseId { get; set; }
    public int Quantity { get; set; }
    public string Type { get; set; } // IN / OUT
    public DateTime Date { get; set; } = DateTime.UtcNow;
}