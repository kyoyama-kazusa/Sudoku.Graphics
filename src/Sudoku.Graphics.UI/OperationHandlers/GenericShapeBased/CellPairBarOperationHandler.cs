namespace Sudoku.Graphics.UI.OperationHandlers.GenericShapeBased;

/// <summary>
/// Represents cell pair bar operation handler.
/// </summary>
[OperationHandler(ItemType.CellPair_Bar)]
public sealed class CellPairBarOperationHandler : CellPairGenericShapeOperationHandler
{
	/// <inheritdoc/>
	public override ItemType ItemType => ItemType.CellPair_Bar;

	/// <inheritdoc/>
	public override Func<Absolute, Absolute, PointMapper, IItem_CellPairProperty?> ItemFactory
		=> static (cell1, cell2, mapper) =>
			cell1.GetAdjacentAbsoluteIn(Direction4.Up, false, mapper) == cell2
			|| cell1.GetAdjacentAbsoluteIn(Direction4.Down, false, mapper) == cell2
			|| cell1.GetAdjacentAbsoluteIn(Direction4.Left, false, mapper) == cell2
			|| cell1.GetAdjacentAbsoluteIn(Direction4.Right, false, mapper) == cell2
				? ItemsFactory.CellPairBar(cell1, cell2)
				: null;
}
