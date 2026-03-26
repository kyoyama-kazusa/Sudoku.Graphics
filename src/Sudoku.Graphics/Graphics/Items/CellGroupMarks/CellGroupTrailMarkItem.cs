namespace Sudoku.Graphics.Items.CellGroupMarks;

/// <summary>
/// Represents a cell group trail mark item.
/// </summary>
public sealed record CellGroupTrailMarkItem : CellGroupMarkItem
{
	/// <inheritdoc/>
	public override ItemType Type => ItemType.CellGroupMark_CellTrail;

	/// <summary>
	/// Indicates scale of size, related to cell size.
	/// </summary>
	public required Scale SizeScale { get; init; }


	/// <inheritdoc/>
	protected internal override void DrawTo(Canvas canvas)
		=> canvas.BackingCanvas.DrawCellGroupTrail(Cells, SizeScale, FillColor, canvas.Templates[TemplateIndex].Mapper);
}
