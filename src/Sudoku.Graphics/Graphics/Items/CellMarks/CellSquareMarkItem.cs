namespace Sudoku.Graphics.Items.CellMarks;

/// <summary>
/// Represents a cell square mark item.
/// </summary>
public sealed record CellSquareMarkItem : CellMarkItem
{
	/// <inheritdoc/>
	public override ItemType Type => ItemType.Cell_Square;


	/// <inheritdoc/>
	protected internal override void DrawTo(Canvas canvas)
	{
		var mapper = canvas.Templates[TemplateIndex].Mapper;
		if (SizeScale.IsNegative)
		{
			// Nothing to draw.
			return;
		}

		var backingCanvas = canvas.BackingCanvas;
		var cellSize = mapper.CellSize;
		var outerSide = SizeScale.Measure(cellSize);
		var strokeWidth = StrokeWidthScale.Measure(cellSize);
		var innerSide = Math.Max(0F, outerSide - strokeWidth);
		var offset = (cellSize - outerSide) / 2 + strokeWidth / 2;
		var topLeft = mapper.GetPoint(Cell, Alignment.TopLeft);
		var left = topLeft.X + offset;
		var top = topLeft.Y + offset;
		var right = left + innerSide;
		var bottom = top + innerSide;
		var maxCorner = innerSide / 2;
		var cornerRadius = CornerRadiusScale.Measure(innerSide);
		var radius = Math.Max(0F, Math.Min(cornerRadius, maxCorner));
		var rect = new SKRect(left, top, right, bottom);

		// Fill paint.
		if (FillColor.Alpha != 0 && innerSide != 0)
		{
			using var fillPaint = new SKPaint { Style = SKPaintStyle.Fill, IsAntialias = true, Color = FillColor };
			if (radius != 0F)
			{
				backingCanvas.DrawRoundRect(rect, radius, radius, fillPaint);
			}
			else
			{
				backingCanvas.DrawRect(rect, fillPaint);
			}
		}

		// Stroke paint.
		if (strokeWidth != 0 && StrokeColor.Alpha != 0 && innerSide != 0)
		{
			using var strokePaint = new SKPaint
			{
				Style = SKPaintStyle.Stroke,
				IsAntialias = true,
				Color = StrokeColor,
				StrokeWidth = strokeWidth
			};
			if (radius > 0F)
			{
				backingCanvas.DrawRoundRect(rect, radius, radius, strokePaint);
			}
			else
			{
				backingCanvas.DrawRect(rect, strokePaint);
			}
		}
	}
}
