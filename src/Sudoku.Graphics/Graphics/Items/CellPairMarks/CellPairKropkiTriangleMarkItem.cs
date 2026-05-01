namespace Sudoku.Graphics.Items.CellPairMarks;

/// <summary>
/// Represents a cell pair kropki (triangle) mark item.
/// </summary>
public sealed record CellPairKropkiTriangleMarkItem : CellPairMarkItem
{
	/// <summary>
	/// Indicates whether the mark should be drawn solid.
	/// </summary>
	public required bool IsSolid { get; init; }

	/// <inheritdoc/>
	public override ItemType Type => ItemType.CellPair_KropkiTriangle;

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
		var mapper = canvas.Mapper;
		var cellSize = mapper.CellSize;
		var halfSize = SizeScale.Measure(cellSize) / 2;
		var (x, y) = mapper.GetPointBetween(Cell1, Cell2);
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

		using var path = new SKPath();
		var squareRootOf3 = MathF.Sqrt(3);
		var height = squareRootOf3 * halfSize;
		var p1 = new SKPoint(x, y - 2 * height / 3);
		var p2 = new SKPoint(x - halfSize, y + height / 3);
		var p3 = new SKPoint(x + halfSize, y + height / 3);
		path.MoveTo(p1);
		path.LineTo(p2);
		path.LineTo(p3);
		path.Close();

		canvas.BackingCanvas.DrawPath(path, strokePaint);
		canvas.BackingCanvas.DrawPath(path, fillPaint);
	}
}
