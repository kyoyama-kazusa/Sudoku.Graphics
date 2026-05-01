namespace Sudoku.Graphics.Items.CellMarks;

/// <summary>
/// Provides cell diamond mark item.
/// </summary>
public sealed record CellDiamondMarkItem : CellMarkItem
{
	/// <inheritdoc/>
	public override ItemType Type => ItemType.Cell_Diamond;


	/// <inheritdoc/>
	protected internal override void DrawTo(Canvas canvas)
	{
		var mapper = canvas.Mapper;
		var cellSize = mapper.CellSize;
		var lineContainingBoxSize = SizeScale.Measure(cellSize);
		var halfPadding = (cellSize - lineContainingBoxSize) / 2;
		var topLeft = mapper.GetPoint(Cell, Alignment.TopLeft) + (+halfPadding, +halfPadding);
		var topRight = mapper.GetPoint(Cell, Alignment.TopRight) + (-halfPadding, +halfPadding);
		var bottomLeft = mapper.GetPoint(Cell, Alignment.BottomLeft) + (+halfPadding, -halfPadding);
		var top = topLeft + (lineContainingBoxSize / 2, 0);
		var bottom = bottomLeft + (lineContainingBoxSize / 2, 0);
		var left = topLeft + (0, lineContainingBoxSize / 2);
		var right = topRight + (0, lineContainingBoxSize / 2);
		using var path = new SKPath();
		path.MoveTo(top);
		path.LineTo(left);
		path.LineTo(bottom);
		path.LineTo(right);
		path.Close();

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
			using var fillPaint = new SKPaint { Style = SKPaintStyle.Fill, Color = FillColor, IsAntialias = true };
			canvas.BackingCanvas.DrawPath(path, fillPaint);
			canvas.BackingCanvas.DrawPath(path, strokePaint);
		}
	}
}
