public class MovementServiceImpl : IMovementService
{
    private readonly IMovementRepository _repo;

    public MovementServiceImpl(IMovementRepository repo)
    {
        _repo = repo;
    }

    public async Task RecordMovementAsync(CreateMovementDto dto)
    {
        var lastBalance = await _repo.GetLatestBalance(dto.ProductId, dto.WarehouseId);

        int newBalance = dto.MovementType switch
        {
            MovementType.STOCK_IN => lastBalance + dto.Quantity,
            MovementType.TRANSFER_IN => lastBalance + dto.Quantity,
            MovementType.RETURN => lastBalance + dto.Quantity,

            MovementType.STOCK_OUT => lastBalance - dto.Quantity,
            MovementType.TRANSFER_OUT => lastBalance - dto.Quantity,
            MovementType.WRITE_OFF => lastBalance - dto.Quantity,
            MovementType.ADJUSTMENT => dto.Quantity,

            _ => lastBalance
        };

        var movement = new StockMovement
        {
            ProductId = dto.ProductId,
            WarehouseId = dto.WarehouseId,
            MovementType = dto.MovementType,
            Quantity = dto.Quantity,
            ReferenceType = dto.ReferenceType,
            ReferenceId = dto.ReferenceId,
            UnitCost = dto.UnitCost,
            PerformedBy = dto.PerformedBy,
            Notes = dto.Notes,
            BalanceAfter = newBalance
        };

        await _repo.AddAsync(movement);
    }

    public async Task<IEnumerable<StockMovement>> GetByProductAsync(int productId)
    {
        return await _repo.GetByProductAsync(productId);
    }
}