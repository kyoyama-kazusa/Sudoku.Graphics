namespace Sudoku.Graphics.Items.CellMarks;

/// <summary>
/// Provides cell apex corner triangle mark item.
/// </summary>
public sealed record CellApexCornerTriangleMarkItem : CellMarkItem
{
	/// <summary>
	/// Indicates corner alignment. The value cannot be <see cref="Alignment.Center"/> due to design.
	/// </summary>
	/// <seealso cref="Alignment.Center"/>
	public required Alignment CornerAlignment { get; init; }

	/// <inheritdoc/>
	public override ItemType Type => ItemType.Cell_ApexCornerTriangle;

	/// <summary>
	/// Indicates padding scale (distance to border of cells).
	/// </summary>
	public required Scale PaddingScale { get; init; }


	/// <inheritdoc/>
	protected internal override void DrawTo(Canvas canvas)
	{
		var mapper = canvas.Mapper;
		var cellSize = mapper.CellSize;
		var offset = PaddingScale.Measure(cellSize);
		var (x, y) = mapper.GetPoint(Cell, Alignment.TopLeft);
		var (rx, ry) = CornerAlignment switch
		{
			Alignment.TopLeft => (x + offset, y + offset),
			Alignment.TopRight => (x + cellSize - offset, y + offset),
			Alignment.BottomLeft => (x + offset, y + cellSize - offset),
			Alignment.BottomRight => (x + cellSize - offset, y + cellSize - offset),
			_ => throw new InvalidOperationException($"{nameof(CornerAlignment)} is not defined or invalid.")
		};
		var len = SizeScale.Measure(cellSize);
		var (x1, y1, x2, y2) = CornerAlignment switch
		{
			Alignment.TopLeft => (rx + len, ry, rx, ry + len),
			Alignment.TopRight => (rx - len, ry, rx, ry + len),
			Alignment.BottomLeft => (rx + len, ry, rx, ry - len),
			Alignment.BottomRight => (rx - len, ry, rx, ry - len),
			_ => throw new InvalidOperationException($"{nameof(CornerAlignment)} is not defined or invalid.")
		};

		using var path = new SKPath();
		path.MoveTo(rx, ry);
		path.LineTo(x1, y1);
		path.LineTo(x2, y2);
		path.Close();

		using var fillPaint = new SKPaint { Style = SKPaintStyle.Fill, Color = FillColor, IsAntialias = true };
		canvas.BackingCanvas.DrawPath(path, fillPaint);
		var strokeWidth = StrokeWidthScale.Measure(cellSize);
		if (strokeWidth != 0 && StrokeColor.Alpha != 0)
		{
			using var strokePaint = new SKPaint
			{
				Style = SKPaintStyle.Stroke,
				Color = StrokeColor,
				StrokeWidth = strokeWidth,
				IsAntialias = true
			};
			canvas.BackingCanvas.DrawPath(path, strokePaint);
		}
	}
}
