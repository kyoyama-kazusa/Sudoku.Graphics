namespace Sudoku.Graphics.UI.OperationHandlers.GenericShapeBased;

/// <summary>
/// Represents an operation handler that produces <see cref="CellCrossMarkItem"/>.
/// </summary>
/// <seealso cref="CellCrossMarkItem"/>
[OperationHandler(ItemType.Cell_Cross)]
public sealed class CellCrossOperationHandler : CellGenericShapeOperationHandler
{
	/// <inheritdoc/>
	public override ItemType ItemType => ItemType.Cell_Cross;

	/// <inheritdoc/>
	public override Func<Absolute, IItem_CellProperty> ItemFactory => ItemsFactory.Cross;
}
