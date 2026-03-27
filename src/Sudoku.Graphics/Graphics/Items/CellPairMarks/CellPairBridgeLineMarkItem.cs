namespace Sudoku.Graphics.Items.CellPairMarks;

/// <summary>
/// Represents a cell pair bridge line mark item.
/// </summary>
public sealed record CellPairBridgeLineMarkItem : CellPairMarkItem
{
	/// <summary>
	/// Indicates whether the circles should be drawn. By default it's <see langword="true"/>.
	/// </summary>
	public bool DrawCircles { get; init; } = true;

	/// <summary>
	/// Indicates whether lines should be drawn. By default it's <see langword="true"/>.
	/// </summary>
	public bool DrawLines { get; init; } = true;

	/// <summary>
	/// Indicates the number of lines.
	/// </summary>
	public required int LinesCount { get; init; }

	/// <inheritdoc/>
	public override ItemType Type => ItemType.CellPair_BridgeLine;

	/// <summary>
	/// Indicates the scale of maximum gap among lines, related to circle diameter.
	/// </summary>
	public required Scale LinesMaxGapScale { get; init; }

	/// <summary>
	/// Indicates the scale of circle, related to cell size.
	/// </summary>
	public required Scale CircleScale { get; init; }

	/// <inheritdoc/>
	public override required Scale StrokeWidthScale { get; init; }

	/// <inheritdoc/>
	public override required SerializableColor FillColor { get; init; }

	/// <inheritdoc/>
	public override required SerializableColor StrokeColor { get; init; }


	/// <inheritdoc/>
	protected internal override void DrawTo(Canvas canvas)
		=> canvas.BackingCanvas.DrawBridgeAndConnectedCircles(
			Cell1,
			Cell2,
			CircleScale,
			LinesCount,
			LinesMaxGapScale,
			StrokeWidthScale,
			StrokeColor,
			FillColor,
			StrokeWidthScale,
			StrokeColor,
			DrawCircles,
			DrawLines,
			canvas.Templates[TemplateIndex].Mapper
		);
}
