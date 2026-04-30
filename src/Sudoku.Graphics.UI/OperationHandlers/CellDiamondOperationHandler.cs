namespace Sudoku.Graphics.UI.OperationHandlers;

/// <summary>
/// Represents an operation handler that produces <see cref="CellDiamondMarkItem"/> instances.
/// </summary>
/// <seealso cref="CellDiamondMarkItem"/>
[OperationHandler(ItemType.Cell_Diamond)]
public sealed class CellDiamondOperationHandler : CellShapeOperationHandler
{
	/// <inheritdoc/>
	public override ItemType ItemType => ItemType.Cell_Diamond;

	/// <inheritdoc/>
	public override Func<Absolute, CellMarkItem> ItemFactory => ItemsFactory.Diamond;
}
