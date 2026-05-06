public class CreateMovementDto
{
    public int ProductId { get; set; }
    public int WarehouseId { get; set; }
    public MovementType MovementType { get; set; }
    public int Quantity { get; set; }
    public string ReferenceType { get; set; }
    public int ReferenceId { get; set; }
    public decimal UnitCost { get; set; }
    public string PerformedBy { get; set; }
    public string Notes { get; set; }
}