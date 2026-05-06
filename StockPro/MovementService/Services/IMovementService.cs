public interface IMovementService
{
    Task RecordMovementAsync(CreateMovementDto dto);
    Task<IEnumerable<StockMovement>> GetByProductAsync(int productId);
}