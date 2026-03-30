namespace Sudoku.Graphics.Items.CellPairMarks;

/// <summary>
/// Represents a cell pair bar mark item.
/// </summary>
public sealed record CellPairBarMarkItem : CellPairMarkItem
{
	/// <inheritdoc/>
	public override ItemType Type => ItemType.CellPair_Bar;

	/// <summary>
	/// Indicates the scale of short side, related to cell size.
	/// </summary>
	public required Scale ShortSideScale { get; init; }

	/// <summary>
	/// Indicates the scale of long side, related to cell size.
	/// </summary>
	public required Scale LongSideScale { get; init; }

	/// <inheritdoc/>
	public required override Scale StrokeWidthScale { get; init; }

	/// <inheritdoc/>
	public required override Scale CornerRadiusScale { get; init; }

	/// <inheritdoc/>
	public required override SerializableColor StrokeColor { get; init; }

	/// <inheritdoc/>
	public required override SerializableColor FillColor { get; init; }


	/// <inheritdoc/>
	protected internal override void DrawTo(Canvas canvas)
		=> canvas.BackingCanvas.DrawBar(
			Cell1,
			Cell2,
			ShortSideScale,
			LongSideScale,
			StrokeWidthScale,
			StrokeColor,
			FillColor,
			CornerRadiusScale,
			canvas.Templates[TemplateIndex].Mapper
		);
}
