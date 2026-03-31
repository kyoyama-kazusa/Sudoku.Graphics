namespace Sudoku.Graphics;

public partial class SKCanvasDrawings
{
	extension(SKCanvas @this)
	{
		/// <summary>
		/// Draw a (convex) polygon into the specified cell.
		/// </summary>
		/// <param name="cell">The cell.</param>
		/// <param name="sidesCount">The number of sides.</param>
		/// <param name="sizeScale">The scale of polygon size, related to cell size.</param>
		/// <param name="strokeWidthScale">The scale of stroke width, related to cell size.</param>
		/// <param name="strokeColor">The stroke color.</param>
		/// <param name="fillColor">The fill color.</param>
		/// <param name="mapper">The mapper instance.</param>
		/// <param name="rotationDegrees">The initial rotation degrees, in angle.</param>
		/// <exception cref="ArgumentException">Throws when the number of sides is invalid (below 3).</exception>
		/// <exception cref="InvalidOperationException">Throws when the number of sides is too large (above 16).</exception>
		public void DrawPolygonToCell(
			Absolute cell,
			int sidesCount,
			Scale sizeScale,
			Scale strokeWidthScale,
			SerializableColor strokeColor,
			SerializableColor fillColor,
			PointMapper mapper,
			float rotationDegrees = 0
		)
		{
			if (sizeScale.IsNegative)
			{
				// Nothing to draw.
				return;
			}

			if (sidesCount < 3)
			{
				throw new ArgumentException("The number of sides must be at least 3.", nameof(sidesCount));
			}
			if (sidesCount > 16)
			{
				throw new InvalidOperationException("The sides is too large to draw. The maximum value expected is 16.");
			}

			var (x, y) = mapper.GetPoint(cell, Alignment.TopLeft);
			var cellSize = mapper.CellSize;
			var outerSide = sizeScale.Measure(cellSize);
			var outerR = outerSide / 2;
			var strokeWidth = strokeWidthScale.Measure(cellSize);
			var innerR = Math.Max(0, outerR - strokeWidth / 2);
			if (innerR <= 0)
			{
				return;
			}

			var cx = x + cellSize / 2;
			var cy = y + cellSize / 2;
			var startAngle = rotationDegrees * MathF.PI / 180 - MathF.PI / 2;
			var delta = MathF.Tau / sidesCount;

			// Construct path.
			using var path = new SKPath();
			for (var i = 0; i < sidesCount; i++)
			{
				var angle = startAngle + i * delta;
				var px = cx + innerR * MathF.Cos(angle);
				var py = cy + innerR * MathF.Sin(angle);
				if (i == 0)
				{
					path.MoveTo(px, py);
				}
				else
				{
					path.LineTo(px, py);
				}
			}
			path.Close();

			// Fill paint.
			if (fillColor.Alpha != 0)
			{
				using var fillPaint = new SKPaint { Style = SKPaintStyle.Fill, IsAntialias = true, Color = fillColor };
				@this.DrawPath(path, fillPaint);
			}

			// Stroke paint.
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

		/// <summary>
		/// Draw a concave polygon into the specified cell.
		/// </summary>
		/// <param name="cell">The cell.</param>
		/// <param name="sidesCount">The number of sides.</param>
		/// <param name="sizeScale">The scale of polygon size, related to cell size.</param>
		/// <param name="innerScale">The scale of inner corner point to center of cell, related to shape size.</param>
		/// <param name="strokeWidthScale">The scale of stroke width, related to cell size.</param>
		/// <param name="strokeColor">The stroke color.</param>
		/// <param name="fillColor">The fill color.</param>
		/// <param name="mapper">The mapper instance.</param>
		/// <param name="rotationDegrees">The initial rotation degrees, in angle.</param>
		/// <exception cref="ArgumentException">Throws when the number of sides is invalid (below 3).</exception>
		/// <exception cref="InvalidOperationException">Throws when the number of sides is too large (above 16).</exception>
		public void DrawConcavePolygonToCell(
			Absolute cell,
			int sidesCount,
			Scale sizeScale,
			Scale innerScale,
			Scale strokeWidthScale,
			SerializableColor strokeColor,
			SerializableColor fillColor,
			PointMapper mapper,
			float rotationDegrees = 0
		)
		{
			if (sizeScale.IsNegative)
			{
				// Nothing to draw.
				return;
			}

			if (sidesCount < 3)
			{
				throw new ArgumentException("The number of sides must be at least 3.", nameof(sidesCount));
			}
			if (sidesCount > 16)
			{
				throw new InvalidOperationException("The sides is too large to draw. The maximum value expected is 16.");
			}

			var cellSize = mapper.CellSize;
			var outerSide = sizeScale.Measure(cellSize);
			var outerR = outerSide / 2;
			var strokeWidth = strokeWidthScale.Measure(cellSize);
			var usableOuterR = Math.Max(0F, outerR - strokeWidth / 2);
			if (usableOuterR <= 0)
			{
				return;
			}

			var usableInnerR = innerScale.Measure(usableOuterR);
			if (usableInnerR <= 0)
			{
				usableInnerR = Math.Max(1E-3F, usableOuterR * .05F);
			}

			var topLeft = mapper.GetPoint(cell, Alignment.TopLeft);
			var cx = topLeft.X + cellSize / 2;
			var cy = topLeft.Y + cellSize / 2;
			var startAngle = rotationDegrees * MathF.PI / 180 - MathF.PI / 2;
			var delta = MathF.Tau / sidesCount;
			var halfDelta = delta / 2;
			using var path = new SKPath { FillType = SKPathFillType.EvenOdd };
			for (var i = 0; i < sidesCount; i++)
			{
				var aOuter = startAngle + i * delta;
				var ox = cx + usableOuterR * MathF.Cos(aOuter);
				var oy = cy + usableOuterR * MathF.Sin(aOuter);
				var aInner = aOuter + halfDelta;
				var ix = cx + usableInnerR * MathF.Cos(aInner);
				var iy = cy + usableInnerR * MathF.Sin(aInner);
				if (i == 0)
				{
					path.MoveTo(ox, oy);
					path.LineTo(ix, iy);
				}
				else
				{
					path.LineTo(ox, oy);
					path.LineTo(ix, iy);
				}
			}
			path.Close();

			// Fill paint.
			if (fillColor.Alpha != 0)
			{
				using var fillPaint = new SKPaint { Style = SKPaintStyle.Fill, IsAntialias = true, Color = fillColor };
				@this.DrawPath(path, fillPaint);
			}

			// Stroke paint.
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
