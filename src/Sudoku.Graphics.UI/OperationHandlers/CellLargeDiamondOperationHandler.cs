namespace Sudoku.Graphics.UI.OperationHandlers;

/// <summary>
/// Represents an operation handler that produces <see cref="CellLargeDiamondMarkItem"/>.
/// </summary>
/// <seealso cref="CellLargeDiamondMarkItem"/>
[OperationHandler(ItemType.Cell_LargeDiamond)]
public sealed class CellLargeDiamondOperationHandler : CellShapeOperationHandler
{
	/// <inheritdoc/>
	public override ItemType ItemType => ItemType.Cell_LargeDiamond;

	/// <inheritdoc/>
	public override Func<Absolute, CellMarkItem> ItemFactory => ItemsFactory.CellLargeDiamond;
}
