namespace Sudoku.Graphics.UI.OperationHandlers;

/// <summary>
/// Represents an operation handler that produces exclamation marks.
/// </summary>
[OperationHandler(ItemType.CellText_Exclamation)]
public sealed class CellTextExclamationOperationHandler : CellTextOperationHandler
{
	/// <inheritdoc/>
	public override ItemType ItemType => ItemType.CellText_Exclamation;

	/// <inheritdoc/>
	public override Func<Absolute, CellTextMarkItem> ItemFactory => ItemsFactory.CellExclamation;
}
