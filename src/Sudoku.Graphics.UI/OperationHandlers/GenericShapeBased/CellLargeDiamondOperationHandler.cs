namespace Sudoku.Graphics.UI.OperationHandlers.GenericShapeBased;

/// <summary>
/// Represents an operation handler that produces <see cref="CellLargeDiamondMarkItem"/>.
/// </summary>
/// <seealso cref="CellLargeDiamondMarkItem"/>
[OperationHandler(ItemType.Cell_LargeDiamond)]
public sealed class CellLargeDiamondOperationHandler : CellGenericShapeOperationHandler
{
	/// <inheritdoc/>
	public override ItemType ItemType => ItemType.Cell_LargeDiamond;

	/// <inheritdoc/>
	public override Func<Absolute, IItem_CellProperty> ItemFactory => ItemsFactory.CellLargeDiamond;
}
