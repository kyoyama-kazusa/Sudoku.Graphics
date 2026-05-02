namespace Sudoku.Graphics.UI.OperationHandlers.GenericShapeBased;

/// <summary>
/// Represents an operation handler that produces <see cref="CellBattenburgMarkItem"/>.
/// </summary>
/// <seealso cref="CellBattenburgMarkItem"/>
[OperationHandler(ItemType.Cell_Battenburg)]
public sealed class CellBattenburgOperationHandler : CellGenericShapeOperationHandler
{
	/// <inheritdoc/>
	public override ItemType ItemType => ItemType.Cell_Battenburg;

	/// <inheritdoc/>
	public override Func<Absolute, IItem_CellProperty> ItemFactory => ItemsFactory.Battenburg;
}
