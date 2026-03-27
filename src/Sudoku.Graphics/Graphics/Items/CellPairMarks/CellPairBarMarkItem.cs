namespace Sudoku.Graphics.Items.CellPairMarks;

/// <summary>
/// Represents a cell pair bar mark item.
/// </summary>
public sealed record CellPairBarMarkItem : CellPairMarkItem
{
	/// <inheritdoc/>
	public override ItemType Type => ItemType.CellPairMark_Bar;

	/// <summary>
	/// Indicates the scale of short side, related to cell size.
	/// </summary>
	public required Scale ShortSideScale { get; init; }

	/// <summary>
	/// Indicates the scale of long side, related to cell size.
	/// </summary>
	public required Scale LongSideScale { get; init; }


	/// <inheritdoc/>
	protected internal override void DrawTo(Canvas canvas)
		=> canvas.BackingCanvas.DrawBar(
			Cell1,
			Cell2,
			ShortSideScale,
			LongSideScale,
			FillColor,
			CornerRadiusScale,
			canvas.Templates[TemplateIndex].Mapper
		);
}
