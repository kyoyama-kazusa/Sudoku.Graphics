namespace Sudoku.Graphics.Items.CellMarks;

/// <summary>
/// Represents a cell border-aligned triangle mark item.
/// </summary>
public sealed record CellBorderAlignedTriangleMarkItem : CellMarkItem, IItem_DirectionProperty<Direction4>
{
	/// <inheritdoc/>
	public required Direction4 Direction { get; init; }

	/// <inheritdoc/>
	public required override Scale SizeScale { get; init; }

	/// <inheritdoc/>
	public required override Scale StrokeWidthScale { get; init; }

	/// <inheritdoc/>
	public required override SerializableColor StrokeColor { get; init; }

	/// <inheritdoc/>
	public required override SerializableColor FillColor { get; init; }

	/// <inheritdoc/>
	public override ItemType Type => ItemType.Cell_BorderAlignedTriangle;


	/// <inheritdoc/>
	protected internal override void DrawTo(Canvas canvas)
	{
		var mapper = canvas.Templates[TemplateIndex].Mapper;
		var cellSize = mapper.CellSize;
		var center = mapper.GetPoint(Cell, Alignment.Center);
		var top = new SKPoint(center.X, center.Y - cellSize / 2);
		var bottom = new SKPoint(center.X, center.Y + cellSize / 2);
		var left = new SKPoint(center.X - cellSize / 2, center.Y);
		var right = new SKPoint(center.X + cellSize / 2, center.Y);
		var triangleBaseSize = SizeScale.Measure(cellSize);
		var (p1, p2, p3) = Direction switch
		{
			Direction4.Up => (
				new SKPoint(top.X - triangleBaseSize / 2, top.Y),
				new SKPoint(top.X + triangleBaseSize / 2, top.Y),
				new SKPoint(top.X, top.Y + triangleBaseSize / 2)
			),
			Direction4.Down => (
				new(bottom.X - triangleBaseSize / 2, bottom.Y),
				new(bottom.X + triangleBaseSize / 2, bottom.Y),
				new(bottom.X, bottom.Y - triangleBaseSize / 2)
			),
			Direction4.Left => (
				new(left.X, left.Y - triangleBaseSize / 2),
				new(left.X, left.Y + triangleBaseSize / 2),
				new(left.X + triangleBaseSize / 2, left.Y)
			),
			Direction4.Right => (
				new(right.X, right.Y - triangleBaseSize / 2),
				new(right.X, right.Y + triangleBaseSize / 2),
				new(right.X - triangleBaseSize / 2, right.Y)
			),
			_ => throw new InvalidOperationException($"{nameof(Direction)} is not defined or '{Direction4.None}'.")
		};
		using var path = new SKPath();
		path.MoveTo(p1);
		path.LineTo(p2);
		path.LineTo(p3);
		path.Close();

		// Fill paint.
		if (FillColor.Alpha != 0)
		{
			using var fillPaint = new SKPaint { Style = SKPaintStyle.Fill, IsAntialias = true, Color = FillColor };
			canvas.BackingCanvas.DrawPath(path, fillPaint);
		}

		// Stroke paint.
		var strokeWidth = StrokeWidthScale.Measure(cellSize);
		if (strokeWidth != 0 && StrokeColor.Alpha != 0)
		{
			using var strokePaint = new SKPaint
			{
				Style = SKPaintStyle.Stroke,
				IsAntialias = true,
				Color = StrokeColor,
				StrokeWidth = strokeWidth
			};
			canvas.BackingCanvas.DrawPath(path, strokePaint);
		}
	}
}
