namespace Sudoku.Graphics.Items.CellMarks;

/// <summary>
/// Represents a cell seven-segment display mark item.
/// </summary>
public sealed record CellSevenSegmentDisplayMarkItem : CellMarkItem, IItem_ValueProperty<int>
{
	/// <summary>
	/// Indicates whether phantom segments (segments not shown in specified value) are also shown, but not filled.
	/// </summary>
	public required bool ShowPhantomSegments { get; init; }

	/// <inheritdoc/>
	public required int Value { get; init; }

	/// <summary>
	/// Indicates the scale of width of segment rectangles, related to cell size.
	/// </summary>
	public required Scale SegmentRectWidthScale { get; init; }

	/// <summary>
	/// Indicates the scale of height of segment rectangles, related to cell size.
	/// </summary>
	public required Scale SegmentRectHeightScale { get; init; }

	/// <summary>
	/// Indicates scale of stroke width of phantom segments, related to cell size.
	/// </summary>
	public required Scale PhantomStrokeWidthScale { get; init; }

	/// <inheritdoc/>
	public override ItemType Type => ItemType.CellMark_SevenSegmentDisplay;


	/// <inheritdoc/>
	protected internal override void DrawTo(Canvas canvas)
		=> canvas.BackingCanvas.DrawSevenSegmentsDisplayToCell(
			Cell,
			Value,
			ShowPhantomSegments,
			SizeScale,
			StrokeWidthScale,
			StrokeColor,
			FillColor,
			SegmentRectWidthScale,
			SegmentRectHeightScale,
			PhantomStrokeWidthScale,
			canvas.Templates[TemplateIndex].Mapper
		);
}
