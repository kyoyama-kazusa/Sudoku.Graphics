namespace Sudoku.Graphics.Items.CellPairMarks;

/// <summary>
/// Represents a cell pair kropki (square) mark item.
/// </summary>
public sealed record CellPairKropkiSquareMarkItem : CellPairMarkItem
{
	/// <summary>
	/// Indicates whether the mark should be drawn solid.
	/// </summary>
	public required bool IsSolid { get; init; }

	/// <inheritdoc/>
	public override ItemType Type => ItemType.CellPair_KropkiSquare;

	/// <summary>
	/// Indicates the scale of size of square drawn, related to cell size.
	/// </summary>
	public required Scale SizeScale { get; init; }

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
	{
		var mapper = canvas.Templates[TemplateIndex].Mapper;
		var cellSize = mapper.CellSize;
		var halfSize = SizeScale.Measure(cellSize) / 2;
		var center = mapper.GetPointBetween(Cell1, Cell2);
		var topLeft = center - new SKPoint(halfSize, halfSize);
		var bottomRight = center + new SKPoint(halfSize, halfSize);
		var rect = SKRect.Create(topLeft, bottomRight);
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

		var cornerRadius = CornerRadiusScale.Measure(halfSize);
		var roundRect = new SKRoundRect(rect, cornerRadius);
		canvas.BackingCanvas.DrawRoundRect(roundRect, strokePaint);
		canvas.BackingCanvas.DrawRoundRect(roundRect, fillPaint);
	}
}
