namespace Sudoku.Graphics.UI.OperationHandlers.GenericShapeBased;

/// <summary>
/// Represents an operation handler that produces <see cref="CellDiamondMarkItem"/> instances.
/// </summary>
/// <seealso cref="CellDiamondMarkItem"/>
[OperationHandler(ItemType.Cell_Diamond)]
public sealed class CellDiamondOperationHandler : CellGenericShapeOperationHandler
{
	/// <inheritdoc/>
	public override ItemType ItemType => ItemType.Cell_Diamond;

	/// <inheritdoc/>
	public override Func<Absolute, IItem_CellProperty> ItemFactory => ItemsFactory.Diamond;
}
