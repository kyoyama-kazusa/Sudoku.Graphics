namespace Sudoku.Graphics.Items.CellPairMarks;

/// <summary>
/// Represents a cell pair kropki (circle) mark item.
/// </summary>
public sealed record CellPairKropkiMarkItem : CellPairMarkItem
{
	/// <summary>
	/// Indicates whether the mark should be drawn solid.
	/// </summary>
	public required bool IsSolid { get; init; }

	/// <inheritdoc/>
	public override ItemType Type => ItemType.CellPair_Kropki;

	/// <summary>
	/// Indicates the scale of size of circle drawn, related to cell size.
	/// </summary>
	public required Scale SizeScale { get; init; }

	/// <inheritdoc/>
	public required override Scale StrokeWidthScale { get; init; }

	/// <inheritdoc/>
	public required override SerializableColor StrokeColor { get; init; }

	/// <inheritdoc/>
	public required override SerializableColor FillColor { get; init; }


	/// <inheritdoc/>
	protected internal override void DrawTo(Canvas canvas)
	{
		var mapper = canvas.Templates[TemplateIndex].Mapper;
		var cellSize = mapper.CellSize;
		var radius = SizeScale.Measure(cellSize) / 2;
		var center = mapper.GetPointBetween(Cell1, Cell2);
		using var strokePaint = new SKPaint
		{
			IsAntialias = true,
			Style = SKPaintStyle.Stroke,
			StrokeWidth = StrokeWidthScale.Measure(cellSize),
			StrokeCap = SKStrokeCap.Round,
			StrokeJoin = SKStrokeJoin.Round,
			Color = StrokeColor
		};
		using var fillPaint = new SKPaint
		{
			IsAntialias = true,
			Style = SKPaintStyle.Fill,
			Color = IsSolid ? StrokeColor : FillColor
		};

		canvas.BackingCanvas.DrawCircle(center, radius, strokePaint);
		canvas.BackingCanvas.DrawCircle(center, radius, fillPaint);
	}
}
