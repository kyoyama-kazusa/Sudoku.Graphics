namespace Sudoku.Graphics.UI.OperationHandlers.GenericShapeBased;

/// <summary>
/// Represents an operation handler that produces exclamation marks.
/// </summary>
[OperationHandler(ItemType.CellText_Exclamation)]
public sealed class CellTextExclamationOperationHandler : CellGenericShapeOperationHandler
{
	/// <inheritdoc/>
	public override ItemType ItemType => ItemType.CellText_Exclamation;

	/// <inheritdoc/>
	public override Func<Absolute, IItem_CellProperty> ItemFactory => ItemsFactory.CellExclamation;
}
