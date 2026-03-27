namespace Sudoku.Graphics.Items.CellMarks;

/// <summary>
/// Represents cell question mark item.
/// </summary>
public sealed record CellQuestionMarkItem : CellTextMarkItem
{
	/// <inheritdoc/>
	public override ItemType Type => ItemType.Cell_QuestionText;

	/// <inheritdoc/>
	protected override string PrintingText => "?";
}
