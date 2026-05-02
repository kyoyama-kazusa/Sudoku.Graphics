namespace Sudoku.Graphics.UI.OperationHandlers.GenericShapeBased;

/// <summary>
/// Represents an operation handler that produces <see cref="CellSquareMarkItem"/>.
/// </summary>
/// <seealso cref="CellSquareMarkItem"/>
[OperationHandler(ItemType.Cell_Square)]
public sealed class CellSquareOperationHandler : CellGenericShapeOperationHandler
{
	/// <inheritdoc/>
	public override ItemType ItemType => ItemType.Cell_Square;

	/// <inheritdoc/>
	public override Func<Absolute, IItem_CellProperty> ItemFactory => ItemsFactory.Square;
}
