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
	public override Func<Absolute, Absolute, IItem_CellPairProperty> ItemFactory => ItemsFactory.CellPairBar;
}
