namespace Sudoku.ComponentModel.Items.CellMarks;

/// <summary>
/// Represents cell exclamation mark item.
/// </summary>
public sealed class CellExclamationMarkItem : CellTextMarkItem
{
	/// <inheritdoc/>
	public override ItemType Type => ItemType.CellMark_Exclamation;

	/// <inheritdoc/>
	protected override float SlightScale => 1;

	/// <inheritdoc/>
	protected override string PrintingText => "!";

	/// <inheritdoc/>
	protected override Type EqualityContract => typeof(CellExclamationMarkItem);
}
