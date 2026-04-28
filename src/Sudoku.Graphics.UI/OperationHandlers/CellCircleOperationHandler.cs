namespace Sudoku.Graphics.UI.OperationHandlers;

/// <summary>
/// Represents an operation handler that produces <see cref="CellCircleMarkItem"/>.
/// </summary>
/// <seealso cref="CellCircleMarkItem"/>
[OperationHandler(ItemType.Cell_Circle)]
public sealed class CellCircleOperationHandler : CellShapeOperationHandler
{
	/// <inheritdoc/>
	public override ItemType ItemType => ItemType.Cell_Circle;

	/// <inheritdoc/>
	public override Func<Absolute, CellMarkItem> ItemFactory => ItemsFactory.Circle;
}
