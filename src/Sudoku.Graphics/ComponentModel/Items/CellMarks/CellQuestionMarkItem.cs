namespace Sudoku.ComponentModel.Items.CellMarks;

/// <summary>
/// Represents cell question mark item.
/// </summary>
public sealed class CellQuestionMarkItem : CellTextMarkItem
{
	/// <inheritdoc/>
	public override ItemType Type => ItemType.CellMark_Question;

	/// <inheritdoc/>
	protected override string PrintingText => "?";

	/// <inheritdoc/>
	protected override Type EqualityContract => typeof(CellQuestionMarkItem);
}
