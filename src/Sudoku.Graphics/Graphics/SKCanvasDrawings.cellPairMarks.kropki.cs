namespace Sudoku.Graphics;

public partial class SKCanvasDrawings
{
	extension(SKCanvas @this)
	{
		/// <summary>
		/// Draws a kropki circle (solid or hollow) into the grid line between two adjacent cells.
		/// </summary>
		/// <param name="cell1">The cell 1.</param>
		/// <param name="cell2">The cell 2.</param>
		/// <param name="isSolid">Indicates whether the mark is solid or not.</param>
		/// <param name="sizeScale">The scale of size, related to cell size.</param>
		/// <param name="strokeWidthScale">The scale of stroke width, related to cell size.</param>
		/// <param name="strokeColor">The stroke color.</param>
		/// <param name="fillColor">The fill color. The value works only if <paramref name="isSolid"/> is <see langword="false"/>.</param>
		/// <param name="mapper">The point mapper instance.</param>
		public void DrawKropki(
			Absolute cell1,
			Absolute cell2,
			bool isSolid,
			Scale sizeScale,
			Scale strokeWidthScale,
			SerializableColor strokeColor,
			SerializableColor fillColor,
			PointMapper mapper
		)
		{
			var cellSize = mapper.CellSize;
			var radius = sizeScale.Measure(cellSize) / 2;
			var center = mapper.GetPointBetween(cell1, cell2);
			using var strokePaint = new SKPaint
			{
				IsAntialias = true,
				Style = SKPaintStyle.Stroke,
				StrokeWidth = strokeWidthScale.Measure(cellSize),
				StrokeCap = SKStrokeCap.Round,
				StrokeJoin = SKStrokeJoin.Round,
				Color = strokeColor
			};
			using var fillPaint = new SKPaint
			{
				IsAntialias = true,
				Style = SKPaintStyle.Fill,
				Color = isSolid ? strokeColor : fillColor
			};

			@this.DrawCircle(center, radius, strokePaint);
			@this.DrawCircle(center, radius, fillPaint);
		}

		/// <summary>
		/// Draws a kropki square (solid or hollow) into the grid line between two adjacent cells.
		/// </summary>
		/// <param name="cell1">The cell 1.</param>
		/// <param name="cell2">The cell 2.</param>
		/// <param name="isSolid">Indicates whether the mark is solid or not.</param>
		/// <param name="sizeScale">The scale of size, related to cell size.</param>
		/// <param name="strokeWidthScale">The scale of stroke width, related to cell size.</param>
		/// <param name="strokeColor">The stroke color.</param>
		/// <param name="fillColor">The fill color. The value works only if <paramref name="isSolid"/> is <see langword="false"/>.</param>
		/// <param name="cornerRadiusScale">The scale of corner radius, related to half size of the mark drawn.</param>
		/// <param name="mapper">The point mapper instance.</param>
		public void DrawKropkiSquare(
			Absolute cell1,
			Absolute cell2,
			bool isSolid,
			Scale sizeScale,
			Scale strokeWidthScale,
			SerializableColor strokeColor,
			SerializableColor fillColor,
			Scale cornerRadiusScale,
			PointMapper mapper
		)
		{
			var cellSize = mapper.CellSize;
			var halfSize = sizeScale.Measure(cellSize) / 2;
			var center = mapper.GetPointBetween(cell1, cell2);
			var topLeft = center - new SKPoint(halfSize, halfSize);
			var bottomRight = center + new SKPoint(halfSize, halfSize);
			var rect = SKRect.Create(topLeft, bottomRight);
			using var strokePaint = new SKPaint
			{
				IsAntialias = true,
				Style = SKPaintStyle.Stroke,
				StrokeWidth = strokeWidthScale.Measure(cellSize),
				StrokeCap = SKStrokeCap.Round,
				StrokeJoin = SKStrokeJoin.Round,
				Color = strokeColor
			};
			using var fillPaint = new SKPaint
			{
				IsAntialias = true,
				Style = SKPaintStyle.Fill,
				Color = isSolid ? strokeColor : fillColor
			};

			var cornerRadius = cornerRadiusScale.Measure(halfSize);
			var roundRect = new SKRoundRect(rect, cornerRadius);
			@this.DrawRoundRect(roundRect, strokePaint);
			@this.DrawRoundRect(roundRect, fillPaint);
		}

		/// <summary>
		/// Draws a kropki triangle (solid or hollow) into the grid line between two adjacent cells.
		/// </summary>
		/// <param name="cell1">The cell 1.</param>
		/// <param name="cell2">The cell 2.</param>
		/// <param name="isSolid">Indicates whether the mark is solid or not.</param>
		/// <param name="sizeScale">The scale of size, related to cell size.</param>
		/// <param name="strokeWidthScale">The scale of stroke width, related to cell size.</param>
		/// <param name="strokeColor">The stroke color.</param>
		/// <param name="fillColor">The fill color. The value works only if <paramref name="isSolid"/> is <see langword="false"/>.</param>
		/// <param name="mapper">The point mapper instance.</param>
		public void DrawKropkiTriangle(
			Absolute cell1,
			Absolute cell2,
			bool isSolid,
			Scale sizeScale,
			Scale strokeWidthScale,
			SerializableColor strokeColor,
			SerializableColor fillColor,
			PointMapper mapper
		)
		{
			var cellSize = mapper.CellSize;
			var halfSize = sizeScale.Measure(cellSize) / 2;
			var (x, y) = mapper.GetPointBetween(cell1, cell2);
			using var strokePaint = new SKPaint
			{
				IsAntialias = true,
				Style = SKPaintStyle.Stroke,
				StrokeWidth = strokeWidthScale.Measure(cellSize),
				StrokeCap = SKStrokeCap.Round,
				StrokeJoin = SKStrokeJoin.Round,
				Color = strokeColor
			};
			using var fillPaint = new SKPaint
			{
				IsAntialias = true,
				Style = SKPaintStyle.Fill,
				Color = isSolid ? strokeColor : fillColor
			};

			using var path = new SKPath();
			var squareRootOf3 = MathF.Sqrt(3);
			var height = squareRootOf3 * halfSize;
			var p1 = new SKPoint(x, y - 2 * height / 3);
			var p2 = new SKPoint(x - halfSize, y + height / 3);
			var p3 = new SKPoint(x + halfSize, y + height / 3);
			path.MoveTo(p1);
			path.LineTo(p2);
			path.LineTo(p3);
			path.Close();

			@this.DrawPath(path, strokePaint);
			@this.DrawPath(path, fillPaint);
		}
	}
}
