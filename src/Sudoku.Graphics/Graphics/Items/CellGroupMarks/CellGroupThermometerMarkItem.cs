namespace Sudoku.Graphics.Items.CellGroupMarks;

/// <summary>
/// Represents cell group thermometer mark item.
/// </summary>
public sealed record CellGroupThermometerMarkItem : CellGroupMarkItem
{
	/// <inheritdoc/>
	public override ItemType Type => ItemType.CellGroup_Thermometer;

	/// <summary>
	/// Indicates scale of circle.
	/// </summary>
	public required Scale CircleScale { get; init; }

	/// <inheritdoc/>
	public required override Scale StrokeWidthScale { get; init; }

	/// <inheritdoc/>
	public required override SerializableColor StrokeColor { get; init; }

	/// <inheritdoc/>
	public required override SerializableColor FillColor { get; init; }


	/// <inheritdoc/>
	protected internal override void DrawTo(Canvas canvas)
	{
		var backingCanvas = canvas.BackingCanvas;
		var mapper = canvas.Mapper;
		var points = from cell in Cells select mapper.GetPoint(cell, Alignment.Center);
		using var path = new SKPath();
		path.MoveTo(points[0]);
		for (var i = 1; i < points.Length; i++)
		{
			path.LineTo(points[i]);
		}

		var cellSize = mapper.CellSize;
		var strokeWidth = StrokeWidthScale.Measure(cellSize);
		using var strokePaint = new SKPaint
		{
			IsAntialias = true,
			Style = SKPaintStyle.Stroke,
			StrokeWidth = strokeWidth,
			Color = StrokeColor
		};
		backingCanvas.DrawPath(path, strokePaint);

		var diameter = CircleScale.Measure(cellSize);
		var radius = diameter / 2;
		using var fillPaint = new SKPaint { IsAntialias = true, Style = SKPaintStyle.Fill, Color = FillColor };
		backingCanvas.DrawCircle(mapper.GetPoint(Cells[0], Alignment.Center), radius, fillPaint);
	}
}
