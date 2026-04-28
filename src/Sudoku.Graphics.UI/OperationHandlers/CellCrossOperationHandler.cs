namespace Sudoku.Graphics.UI.OperationHandlers;

/// <summary>
/// Represents an operation handler that produces <see cref="CellCrossMarkItem"/>.
/// </summary>
/// <seealso cref="CellCrossMarkItem"/>
[OperationHandler(ItemType.Cell_Cross)]
public sealed class CellCrossOperationHandler : CellShapeOperationHandler
{
	/// <inheritdoc/>
	public override ItemType ItemType => ItemType.Cell_Cross;

	/// <inheritdoc/>
	public override Func<Absolute, CellMarkItem> ItemFactory => ItemsFactory.Cross;
}
