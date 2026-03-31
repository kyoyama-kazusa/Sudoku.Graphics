namespace Sudoku.Graphics.Items.CellPairMarks;

/// <summary>
/// Represents a cell pair connection line mark item.
/// </summary>
public sealed record CellPairConnectionLineMarkItem : CellPairMarkItem
{
	/// <inheritdoc/>
	public override ItemType Type => ItemType.CellPair_ConnectionLine;

	/// <summary>
	/// Indicates the size scale.
	/// </summary>
	public required Scale SizeScale { get; init; }

	/// <inheritdoc/>
	public required override Scale StrokeWidthScale { get; init; }

	/// <inheritdoc/>
	public required override SerializableColor StrokeColor { get; init; }


	/// <inheritdoc/>
	protected internal override void DrawTo(Canvas canvas)
	{
		var mapper = canvas.Templates[TemplateIndex].Mapper;
		if (Absolute.GetAdjacentRelation(Cell1, Cell2, mapper) is not (var adjacentRelation and not Direction8.None))
		{
			return;
		}

		var cellSize = mapper.CellSize;
		var cellPadding = SizeScale.Measure(cellSize);
		var p1 = mapper.GetPoint(Cell1, Alignment.Center);
		var p2 = mapper.GetPoint(Cell2, Alignment.Center);
		_ = adjacentRelation switch
		{
			Direction8.Up => (p1 += new SKPoint(0, cellPadding), p2 -= new SKPoint(0, cellPadding)),
			Direction8.Down => (p1 -= new SKPoint(0, cellPadding), p2 += new SKPoint(0, cellPadding)),
			Direction8.Left => (p1 += new SKPoint(cellPadding, 0), p2 -= new SKPoint(cellPadding, 0)),
			Direction8.Right => (p1 -= new SKPoint(cellPadding, 0), p2 += new SKPoint(cellPadding, 0)),
			Direction8.LeftUp => (p1 += new SKPoint(+cellPadding, +cellPadding), p2 -= new SKPoint(+cellPadding, +cellPadding)),
			Direction8.RightUp => (p1 += new SKPoint(-cellPadding, +cellPadding), p2 -= new SKPoint(-cellPadding, +cellPadding)),
			Direction8.LeftDown => (p1 += new SKPoint(+cellPadding, -cellPadding), p2 -= new SKPoint(+cellPadding, -cellPadding)),
			Direction8.RightDown => (p1 += new SKPoint(-cellPadding, -cellPadding), p2 -= new SKPoint(-cellPadding, -cellPadding)),
			_ => throw new UnreachableException()
		};

		using var strokePaint = new SKPaint
		{
			Style = SKPaintStyle.Stroke,
			IsAntialias = true,
			StrokeWidth = StrokeWidthScale.Measure(cellSize),
			Color = StrokeColor,
			StrokeCap = SKStrokeCap.Round,
			StrokeJoin = SKStrokeJoin.Round
		};
		canvas.BackingCanvas.DrawLine(p1, p2, strokePaint);
	}
}
