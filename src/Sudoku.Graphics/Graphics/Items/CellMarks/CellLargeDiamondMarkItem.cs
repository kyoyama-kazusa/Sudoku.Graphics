namespace Sudoku.Graphics.Items.CellMarks;

/// <summary>
/// Represents cell large diamond mark item.
/// </summary>
public sealed record CellLargeDiamondMarkItem : CellMarkItem
{
	/// <inheritdoc/>
	public override ItemType Type => ItemType.Cell_LargeDiamond;

	/// <inheritdoc/>
	public override Scale StrokeWidthScale { get; init; }

	/// <inheritdoc/>
	public override SerializableColor StrokeColor { get; init; }

	/// <inheritdoc/>
	public required override SerializableColor FillColor { get; init; }


	/// <inheritdoc/>
	protected internal override void DrawTo(Canvas canvas)
	{
		var mapper = canvas.Templates[TemplateIndex].Mapper;
		var cellSize = mapper.CellSize;

		// Find for the points above, below, left-side and right-side the current cell.
		var center = mapper.GetPoint(Cell, Alignment.Center);
		var aboveCellCenter = center - new SKPoint(0, cellSize);
		var belowCellCenter = center + new SKPoint(0, cellSize);
		var leftSideCellCenter = center - new SKPoint(cellSize, 0);
		var rightSideCellCenter = center + new SKPoint(cellSize, 0);

		using var path = new SKPath();
		path.MoveTo(aboveCellCenter);
		path.LineTo(leftSideCellCenter);
		path.LineTo(belowCellCenter);
		path.LineTo(rightSideCellCenter);
		path.Close();

		using var strokePaint = new SKPaint
		{
			Style = SKPaintStyle.Stroke,
			IsAntialias = true,
			StrokeWidth = StrokeWidthScale.Measure(cellSize),
			Color = StrokeColor,
			StrokeJoin = SKStrokeJoin.Round,
			StrokeCap = SKStrokeCap.Round
		};
		using var fillPaint = new SKPaint { Style = SKPaintStyle.Fill, IsAntialias = true, Color = FillColor };

		var backingCanvas = canvas.BackingCanvas;
		backingCanvas.DrawPath(path, strokePaint);
		backingCanvas.DrawPath(path, fillPaint);
	}
}
