namespace Sudoku.Graphics.UI.OperationHandlers;

/// <summary>
/// Represents an operation handler that produces <see cref="CellSquareMarkItem"/>.
/// </summary>
/// <seealso cref="CellSquareMarkItem"/>
[OperationHandler(ItemType.Cell_Square)]
public sealed class CellSquareOperationHandler : CellShapeOperationHandler
{
	/// <inheritdoc/>
	public override ItemType ItemType => ItemType.Cell_Square;

	/// <inheritdoc/>
	public override Func<Absolute, CellMarkItem> ItemFactory => ItemsFactory.Square;
}
