namespace Sudoku.Graphics.UI.OperationHandlers;

/// <summary>
/// Represents an operation handler that produces question marks.
/// </summary>
[OperationHandler(ItemType.CellText_Question)]
public sealed class CellTextQuestionOperationHandler : CellTextOperationHandler
{
	/// <inheritdoc/>
	public override ItemType ItemType => ItemType.CellText_Question;

	/// <inheritdoc/>
	public override Func<Absolute, CellTextMarkItem> ItemFactory => ItemsFactory.CellQuestion;
}
