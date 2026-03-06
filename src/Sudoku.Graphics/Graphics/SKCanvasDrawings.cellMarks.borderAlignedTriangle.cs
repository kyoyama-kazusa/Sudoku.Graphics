namespace Sudoku.Graphics;

public partial class SKCanvasDrawings
{
	extension(SKCanvas @this)
	{
		/// <summary>
		/// Draws a border-aligned triangle into the specified cell.
		/// </summary>
		/// <param name="cell">The cell.</param>
		/// <param name="direction">The direction.</param>
		/// <param name="triangleBaseSizeScale">The scale of triangle base size, related to cell size.</param>
		/// <param name="strokeColor">The stroke color.</param>
		/// <param name="strokeWidthScale">The scale of stroke width, related to cell size.</param>
		/// <param name="fillColor">The fill color.</param>
		/// <param name="mapper">The point mapper instance.</param>
		/// <exception cref="ArgumentOutOfRangeException">
		/// Throws when <paramref name="direction"/> is not defined or <see cref="Direction4.None"/>.
		/// </exception>
		/// <seealso cref="Direction4.None"/>
		public void DrawBorderAlignedTriangleToCell(
			Absolute cell,
			Direction4 direction,
			Scale triangleBaseSizeScale,
			SerializableColor strokeColor,
			Scale strokeWidthScale,
			SerializableColor fillColor,
			PointMapper mapper
		)
		{
			var cellSize = mapper.CellSize;
			var center = mapper.GetPoint(cell, Alignment.Center);
			var top = new SKPoint(center.X, center.Y - cellSize / 2);
			var bottom = new SKPoint(center.X, center.Y + cellSize / 2);
			var left = new SKPoint(center.X - cellSize / 2, center.Y);
			var right = new SKPoint(center.X + cellSize / 2, center.Y);
			var triangleBaseSize = triangleBaseSizeScale.Measure(cellSize);
			SKPoint[] points = direction switch
			{
				Direction4.Up => [
					new(top.X - triangleBaseSize / 2, top.Y),
					new(top.X + triangleBaseSize / 2, top.Y),
					new(top.X, top.Y + triangleBaseSize / 2)
				],
				Direction4.Down => [
					new(bottom.X - triangleBaseSize / 2, bottom.Y),
					new(bottom.X + triangleBaseSize / 2, bottom.Y),
					new(bottom.X, bottom.Y - triangleBaseSize / 2)
				],
				Direction4.Left => [
					new(left.X, left.Y - triangleBaseSize / 2),
					new(left.X, left.Y + triangleBaseSize / 2),
					new(left.X + triangleBaseSize / 2, left.Y)
				],
				Direction4.Right => [
					new(right.X, right.Y - triangleBaseSize / 2),
					new(right.X, right.Y + triangleBaseSize / 2),
					new(right.X - triangleBaseSize / 2, right.Y)
				],
				_ => throw new ArgumentOutOfRangeException(nameof(direction))
			};
			using var path = new SKPath();
			path.MoveTo(points[0]);
			path.LineTo(points[1]);
			path.LineTo(points[2]);
			path.Close();

			// Fill paint.
			if (fillColor.Alpha != 0)
			{
				using var fillPaint = new SKPaint { Style = SKPaintStyle.Fill, IsAntialias = true, Color = fillColor };
				@this.DrawPath(path, fillPaint);
			}

			// Stroke paint.
			var strokeWidth = strokeWidthScale.Measure(cellSize);
			if (strokeWidth != 0 && strokeColor.Alpha != 0)
			{
				using var strokePaint = new SKPaint
				{
					Style = SKPaintStyle.Stroke,
					IsAntialias = true,
					Color = strokeColor,
					StrokeWidth = strokeWidth
				};
				@this.DrawPath(path, strokePaint);
			}
		}
	}
}
