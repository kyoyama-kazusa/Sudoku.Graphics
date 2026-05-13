namespace Sudoku.Graphics.UI.OperationHandlers.GenericShapeBased;

/// <summary>
/// Represents an operation handler that produces <see cref="CellPairBattenburgMarkItem"/>.
/// </summary>
/// <seealso cref="CellPairBattenburgMarkItem"/>
[OperationHandler(ItemType.CellPair_Battenburg)]
public sealed class CellPairBattenburgOperationHandler : CellPairGenericShapeOperationHandler
{
	/// <inheritdoc/>
	public override ItemType ItemType => ItemType.CellPair_Battenburg;

	/// <inheritdoc/>
	public override Func<Absolute, Absolute, IItem_CellPairProperty> ItemFactory => ItemsFactory.CellPairBattenburg;
}
