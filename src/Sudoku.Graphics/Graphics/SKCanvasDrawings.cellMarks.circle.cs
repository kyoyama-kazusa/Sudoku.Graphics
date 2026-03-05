namespace Sudoku.Graphics;

public partial class SKCanvasDrawings
{
	extension(SKCanvas @this)
	{
		/// <summary>
		/// Draws a square into the specified cell.
		/// </summary>
		/// <param name="cell">The cell.</param>
		/// <param name="sizeScale">The scale of size, related to cell.</param>
		/// <param name="strokeColor">The stroke color.</param>
		/// <param name="strokeWidthScale">The scale of stroke width, related to cell.</param>
		/// <param name="fillColor">The fill color.</param>
		/// <param name="mapper">The mapper instance.</param>
		public void DrawCircleToCell(
			Absolute cell,
			Scale sizeScale,
			SerializableColor strokeColor,
			Scale strokeWidthScale,
			SerializableColor fillColor,
			PointMapper mapper
		)
		{
			if (sizeScale.IsNegative)
			{
				// Nothing to draw.
				return;
			}

			var cellSize = mapper.CellSize;
			var outerSide = sizeScale.Measure(cellSize);
			var strokeWidth = strokeWidthScale.Measure(cellSize);
			var innerSide = Math.Max(0F, outerSide - strokeWidth);
			var topLeft = mapper.GetPoint(cell, Alignment.Center);
			var radius = innerSide / 2;

			// Fill paint.
			if (fillColor.Alpha != 0 && innerSide != 0)
			{
				using var fillPaint = new SKPaint { Style = SKPaintStyle.Fill, IsAntialias = true, Color = fillColor };
				@this.DrawCircle(topLeft.X, topLeft.Y, radius, fillPaint);
			}

			// Stroke paint.
			if (strokeWidth != 0 && strokeColor.Alpha != 0 && innerSide != 0)
			{
				using var strokePaint = new SKPaint
				{
					Style = SKPaintStyle.Stroke,
					IsAntialias = true,
					Color = strokeColor,
					StrokeWidth = strokeWidth
				};
				@this.DrawCircle(topLeft.X, topLeft.Y, radius, strokePaint);
			}
		}
	}
}
