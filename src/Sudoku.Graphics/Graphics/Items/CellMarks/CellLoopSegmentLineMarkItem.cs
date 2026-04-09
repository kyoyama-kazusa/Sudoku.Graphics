namespace Sudoku.Graphics.Items.CellMarks;

/// <summary>
/// Represents a cell loop segment line mark item.
/// </summary>
public sealed record CellLoopSegmentLineMarkItem : CellMarkItem
{
	/// <summary>
	/// Indicates all directions that the segment line will occupy.
	/// </summary>
	public required Direction4 OccupiedDirections { get; init; }

	/// <inheritdoc/>
	public override ItemType Type => ItemType.Cell_LoopSegmentLine;

	/// <inheritdoc/>
	public required override Scale StrokeWidthScale { get; init; }

	/// <inheritdoc/>
	public required override SerializableColor StrokeColor { get; init; }


	/// <inheritdoc/>
	protected internal override void DrawTo(Canvas canvas)
	{
		var mapper = canvas.Templates[TemplateIndex].Mapper;
		var cellSize = mapper.CellSize;
		using var strokePaint = new SKPaint
		{
			IsAntialias = true,
			Style = SKPaintStyle.Stroke,
			StrokeWidth = StrokeWidthScale.Measure(cellSize),
			Color = StrokeColor,
			StrokeJoin = SKStrokeJoin.Round,
			StrokeCap = SKStrokeCap.Round
		};

		var backingCanvas = canvas.BackingCanvas;
		var center = mapper.GetPoint(Cell, Alignment.Center);
		if (OccupiedDirections.HasFlag(Direction4.Up))
		{
			backingCanvas.DrawLine(center, center with { Y = center.Y - cellSize / 2 }, strokePaint);
		}
		if (OccupiedDirections.HasFlag(Direction4.Down))
		{
			backingCanvas.DrawLine(center, center with { Y = center.Y + cellSize / 2 }, strokePaint);
		}
		if (OccupiedDirections.HasFlag(Direction4.Left))
		{
			backingCanvas.DrawLine(center, center with { X = center.X - cellSize / 2 }, strokePaint);
		}
		if (OccupiedDirections.HasFlag(Direction4.Right))
		{
			backingCanvas.DrawLine(center, center with { X = center.X + cellSize / 2 }, strokePaint);
		}
	}
}
