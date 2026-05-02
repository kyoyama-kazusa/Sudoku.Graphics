namespace Sudoku.Graphics.UI.OperationHandlers.GenericShapeBased;

/// <summary>
/// Represents an operation handler that produces <see cref="CellCircleMarkItem"/>.
/// </summary>
/// <seealso cref="CellCircleMarkItem"/>
[OperationHandler(ItemType.Cell_Circle)]
public sealed class CellCircleOperationHandler : CellGenericShapeOperationHandler
{
	/// <inheritdoc/>
	public override ItemType ItemType => ItemType.Cell_Circle;

	/// <inheritdoc/>
	public override Func<Absolute, IItem_CellProperty> ItemFactory => ItemsFactory.Circle;
}
