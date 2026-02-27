namespace Sudoku.Graphics;

public partial class SKCanvasDrawingExtensions
{
	extension(SKCanvas @this)
	{
		/// <summary>
		/// Draws a several triangles, surrounding with cell center point.
		/// </summary>
		/// <param name="cell">The cell.</param>
		/// <param name="trianglesCount">The number of triangles to generate. Recommend range [0, 6].</param>
		/// <param name="triangleSizeScale">The triangle size scale.</param>
		/// <param name="strokeColor">The stroke color.</param>
		/// <param name="triangleStrokeWidthScale">The triangle stroke width scale, relative to cell size.</param>
		/// <param name="fillColor">The fill color.</param>
		/// <param name="tipDistanceScale">The tip distance scale.</param>
		/// <param name="trianglesCornerRadiusScale">The triangles corner radius scale.</param>
		/// <param name="mapper">The mapper instance.</param>
		/// <exception cref="ArgumentException">
		/// Throws when <paramref name="trianglesCount"/> is below than 1.
		/// </exception>
		public void DrawTrianglesInCell(
			Absolute cell,
			int trianglesCount,
			Scale triangleSizeScale,
			SKColor strokeColor,
			Scale triangleStrokeWidthScale,
			SKColor fillColor,
			Scale tipDistanceScale,
			Scale trianglesCornerRadiusScale,
			PointMapper mapper
		)
		{
			if (trianglesCount < 1)
			{
				throw new ArgumentException($"Expected a value between 1 and 9.", nameof(trianglesCount));
			}

			var cellSize = mapper.CellSize;
			if (cellSize <= 0)
			{
				return;
			}

			var triangleHeight = triangleSizeScale.Measure(cellSize);
			if (triangleHeight <= 0F)
			{
				// Nothing to draw.
				return;
			}

			var s = 2F * triangleHeight / MathF.Sqrt(3); // Side length of equilateral triangle.
			var strokeWidth = Math.Max(0F, triangleStrokeWidthScale.Measure(cellSize));
			using var paintFill = new SKPaint { Style = SKPaintStyle.Fill, Color = fillColor, IsAntialias = true };
			using var paintStroke = new SKPaint
			{
				Style = SKPaintStyle.Stroke,
				Color = strokeColor,
				StrokeWidth = strokeWidth,
				PathEffect = SKPathEffect.CreateCorner(trianglesCornerRadiusScale.Measure(triangleHeight)),
				IsAntialias = true
			};

			var cellCenterPoint = mapper.GetPoint(cell, Alignment.Center);
			if (trianglesCount == 1)
			{
				// Single triangle: centered by centroid, pointing up.
				// For an equilateral triangle of height:
				// - Centroid is located at distance 2h/3 from the apex (tip), and at distance h/3 from the base midpoint.
				// We want centroid == center, and apex pointing up (negative Y).
				var tip = new SKPoint(cellCenterPoint.X, cellCenterPoint.Y - 2F * triangleHeight / 3F); // Apex (tip) up.
				var baseMid = new SKPoint(cellCenterPoint.X, cellCenterPoint.Y + triangleHeight / 3F); // Base midpoint down.
				var halfBase = s / 2F;

				// Base is horizontal: left/right.
				var baseLeft = new SKPoint(baseMid.X - halfBase, baseMid.Y);
				var baseRight = new SKPoint(baseMid.X + halfBase, baseMid.Y);

				using var path = new SKPath();
				path.MoveTo(tip);
				path.LineTo(baseLeft);
				path.LineTo(baseRight);
				path.Close();

				@this.DrawPath(path, paintFill);
				if (strokeWidth > 0)
				{
					@this.DrawPath(path, paintStroke);
				}
			}
			else
			{
				// Multiple triangles: arrange tips on circle around center,
				// tips point toward center. One triangle fixed at the top (i=0),
				// its tip is above center and points downwards to center.
				var maxRadius = cellSize / 2F; // Radius from center to cell edge.
				var tipRadius = tipDistanceScale.Measure(maxRadius); // Actual radius for tip positions.

				// Starting angle so that i=0 tip is at top (above center): angle = -pi/2.
				// Angle measured in radians, 0 = +X axis (right), positive = downwards rotation (screen y down).
				// Using standard math functions where sin/cos work with y-positive-down coordinate works fine.
				const float startAngle = -MathF.PI / 2;
				for (var i = 0; i < trianglesCount; i++)
				{
					var angle = startAngle + i * (MathF.Tau / trianglesCount);
					var unit = new SKPoint(MathF.Cos(angle), MathF.Sin(angle)); // From center -> tip direction.

					// Tip position on circle.
					var tipPos = new SKPoint(cellCenterPoint.X + unit.X * tipRadius, cellCenterPoint.Y + unit.Y * tipRadius);

					// Vector from tip to center = center - tipPos.
					var vToCenter = new SKPoint(cellCenterPoint.X - tipPos.X, cellCenterPoint.Y - tipPos.Y);
					var len = MathF.Sqrt(vToCenter.X * vToCenter.X + vToCenter.Y * vToCenter.Y);
					var axisDir = len - 0 < 1E-3
						// Degeneracy: tip coincides with center; choose arbitrary inward axis (down).
						? new SKPoint(0, 1)
						// Unit vector pointing to center.
						: new SKPoint(vToCenter.X / len, vToCenter.Y / len);

					// Base midpoint should be away from center: baseMid = tipPos - axisDir * triangleHeight.
					// (because axisDir points from tip toward center, so subtract to go away from center)
					var baseMid = new SKPoint(tipPos.X - axisDir.X * triangleHeight, tipPos.Y - axisDir.Y * triangleHeight);

					// Axis from tip->baseMid (direction away from center) is (-axisDir).
					var awayDir = new SKPoint(-axisDir.X, -axisDir.Y);

					// Perpendicular vector for base endpoints: rotate awayDir by 90 degrees.
					var perp = new SKPoint(-awayDir.Y, awayDir.X); // Normalized because awayDir normalized => perp normalized.

					var halfBase = s / 2F;
					var baseLeft = new SKPoint(baseMid.X + perp.X * halfBase, baseMid.Y + perp.Y * halfBase);
					var baseRight = new SKPoint(baseMid.X - perp.X * halfBase, baseMid.Y - perp.Y * halfBase);

					// Path: tip -> baseLeft -> baseRight.
					using var path = new SKPath();
					path.MoveTo(tipPos);
					path.LineTo(baseLeft);
					path.LineTo(baseRight);
					path.Close();

					@this.DrawPath(path, paintFill);
					if (strokeWidth > 0)
					{
						@this.DrawPath(path, paintStroke);
					}
				}
			}
		}
	}
}
