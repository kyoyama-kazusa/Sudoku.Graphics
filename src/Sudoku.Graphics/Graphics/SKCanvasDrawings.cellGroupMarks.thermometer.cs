namespace Sudoku.Graphics;

public partial class SKCanvasDrawings
{
	extension(SKCanvas @this)
	{
		/// <summary>
		/// Draws a thermometer into the grid.
		/// </summary>
		/// <param name="cells">The cells.</param>
		/// <param name="strokeWidthScale">The scale of stroke width.</param>
		/// <param name="strokeColor">The stroke color.</param>
		/// <param name="circleScale">The scale of circle.</param>
		/// <param name="circleFillColor">The circle filling color.</param>
		/// <param name="mapper">The mapper instance.</param>
		public void DrawThermometer(
			ReadOnlySpan<Absolute> cells,
			Scale strokeWidthScale,
			SKColor strokeColor,
			Scale circleScale,
			SKColor circleFillColor,
			PointMapper mapper
		)
		{
			var points = from cell in cells select mapper.GetPoint(cell, Alignment.Center);
			using var path = new SKPath();
			path.MoveTo(points[0]);
			for (var i = 1; i < points.Length; i++)
			{
				path.LineTo(points[i]);
			}

			var cellSize = mapper.CellSize;
			var strokeWidth = strokeWidthScale.Measure(cellSize);
			using var strokePaint = new SKPaint
			{
				IsAntialias = true,
				Style = SKPaintStyle.Stroke,
				StrokeWidth = strokeWidth,
				Color = strokeColor
			};
			@this.DrawPath(path, strokePaint);

			var diameter = circleScale.Measure(cellSize);
			var radius = diameter / 2;
			using var fillPaint = new SKPaint { IsAntialias = true, Style = SKPaintStyle.Fill, Color = circleFillColor };
			@this.DrawCircle(mapper.GetPoint(cells[0], Alignment.Center), radius, fillPaint);
		}
	}
}
