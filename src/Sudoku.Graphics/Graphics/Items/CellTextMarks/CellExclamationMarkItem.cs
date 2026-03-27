namespace Sudoku.Graphics.Items.CellTextMarks;

/// <summary>
/// Represents cell exclamation mark item.
/// </summary>
public sealed record CellExclamationMarkItem : CellTextMarkItem
{
	/// <inheritdoc/>
	public override ItemType Type => ItemType.Cell_ExclamationText;

	/// <inheritdoc/>
	protected override string PrintingText => "!";
}
