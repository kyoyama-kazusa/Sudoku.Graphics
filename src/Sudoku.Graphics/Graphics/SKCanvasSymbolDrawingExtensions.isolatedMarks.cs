namespace Sudoku.Graphics;

public partial class SKCanvasSymbolDrawingExtensions
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
		/// <param name="cornerRadiusScale">The scale of corner radiuse, related to drawn square size.</param>
		/// <param name="mapper">The mapper instance.</param>
		public void DrawSquareToCell(
			Absolute cell,
			Scale sizeScale,
			SKColor strokeColor,
			Scale strokeWidthScale,
			SKColor fillColor,
			Scale cornerRadiusScale,
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
			var offset = (cellSize - outerSide) / 2 + strokeWidth / 2;
			var topLeft = mapper.GetPoint(cell, Alignment.TopLeft);
			var left = topLeft.X + offset;
			var top = topLeft.Y + offset;
			var right = left + innerSide;
			var bottom = top + innerSide;
			var maxCorner = innerSide / 2;
			var cornerRadius = cornerRadiusScale.Measure(innerSide);
			var radius = Math.Max(0F, Math.Min(cornerRadius, maxCorner));
			var rect = new SKRect(left, top, right, bottom);

			// Fill paint.
			if (fillColor.Alpha != 0 && innerSide != 0)
			{
				using var fillPaint = new SKPaint { Style = SKPaintStyle.Fill, IsAntialias = true, Color = fillColor };
				if (radius != 0F)
				{
					@this.DrawRoundRect(rect, radius, radius, fillPaint);
				}
				else
				{
					@this.DrawRect(rect, fillPaint);
				}
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
				if (radius > 0F)
				{
					@this.DrawRoundRect(rect, radius, radius, strokePaint);
				}
				else
				{
					@this.DrawRect(rect, strokePaint);
				}
			}
		}

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
			SKColor strokeColor,
			Scale strokeWidthScale,
			SKColor fillColor,
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

		/// <summary>
		/// Draws a cross symbol into the specified cell.
		/// </summary>
		/// <param name="cell">The cell.</param>
		/// <param name="sizeScale">The scale of size, related to cell size.</param>
		/// <param name="strokeWidthScale">The stroke width scale.</param>
		/// <param name="color">The color.</param>
		/// <param name="mapper">The mapper instance.</param>
		/// <param name="cap">The stroke cap. By default it's <see cref="SKStrokeCap.Round"/>.</param>
		/// <seealso cref="SKStrokeCap.Round"/>
		public void DrawCrossInCell(
			Absolute cell,
			Scale sizeScale,
			Scale strokeWidthScale,
			SKColor color,
			PointMapper mapper,
			SKStrokeCap cap = SKStrokeCap.Round
		)
		{
			var cellSize = mapper.CellSize;
			var center = mapper.GetPoint(cell, Alignment.Center);
			var paddingOffset = sizeScale.Measure(cellSize / 2);
			var topLeft = new SKPoint(center.X - paddingOffset, center.Y - paddingOffset);
			var bottomRight = new SKPoint(center.X + paddingOffset, center.Y + paddingOffset);
			var topRight = new SKPoint(center.X + paddingOffset, center.Y - paddingOffset);
			var bottomLeft = new SKPoint(center.X - paddingOffset, center.Y + paddingOffset);

			// Stroke paint.
			var strokeWidth = strokeWidthScale.Measure(cellSize);
			using var paint = new SKPaint
			{
				Style = SKPaintStyle.Stroke,
				StrokeWidth = strokeWidth,
				Color = color,
				IsAntialias = true,
				StrokeCap = cap
			};
			@this.DrawLine(topLeft, bottomRight, paint);
			@this.DrawLine(topRight, bottomLeft, paint);
		}

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
			SKColor strokeColor,
			SKColor fillColor,
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
			SKColor strokeColor,
			SKColor fillColor,
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

		/// <summary>
		/// Draws a moon to the specified cell, using the specified phase.
		/// </summary>
		/// <param name="cell">The cell.</param>
		/// <param name="phase">The moon phase.</param>
		/// <param name="strokeWidthScale">The scale of stroke width, related to cell size.</param>
		/// <param name="strokeColor">The stroke color.</param>
		/// <param name="fillColor">The fill color.</param>
		/// <param name="radiusScale">The radius scale, related to half cell size.</param>
		/// <param name="mapper">The point mapper.</param>
		public void DrawMoonToCell(
			Absolute cell,
			MoonPhase phase,
			Scale strokeWidthScale,
			SKColor strokeColor,
			SKColor fillColor,
			Scale radiusScale,
			PointMapper mapper
		)
		{
			var cellSize = mapper.CellSize;
			var (x, y) = mapper.GetPoint(cell, Alignment.TopLeft);
			var cx = x + cellSize / 2;
			var cy = y + cellSize / 2;
			var radiusOfMainCircle = radiusScale.Measure(cellSize / 2);
			var strokeWidth = strokeWidthScale.Measure(cellSize);

			using var fillPaint = new SKPaint { Style = SKPaintStyle.Fill, Color = fillColor, IsAntialias = true };
			using var strokePaint = new SKPaint
			{
				Style = SKPaintStyle.Stroke,
				Color = strokeColor,
				StrokeWidth = strokeWidth,
				IsAntialias = true
			};

			const float chordAngleDegrees = 45;
			switch (phase)
			{
				default:
				case MoonPhase.Full:
				{
					@this.DrawCircle(cx, cy, radiusOfMainCircle, fillPaint);
					if (strokeWidth != 0)
					{
						@this.DrawCircle(cx, cy, radiusOfMainCircle, strokePaint);
					}
					break;
				}
				case MoonPhase.UpperHalf_Line or MoonPhase.LowerHalf_Line:
				{
					var startDegrees = chordAngleDegrees + 90;
					var rect = new SKRect(cx - radiusOfMainCircle, cy - radiusOfMainCircle, cx + radiusOfMainCircle, cy + radiusOfMainCircle);
					using var path = new SKPath();
					path.ArcTo(rect, startDegrees, 180, false);
					path.Close();
					if (phase == MoonPhase.LowerHalf_Line)
					{
						path.Reset();
						path.ArcTo(rect, startDegrees + 180, 180, false);
						path.Close();
					}

					@this.DrawPath(path, fillPaint);
					if (strokeWidth != 0)
					{
						@this.DrawPath(path, strokePaint);
					}
					break;
				}
				case MoonPhase.UpperHalf_Curve or MoonPhase.LowerHalf_Curve:
				{
					var innerScale = .8F;
					var offsetFactor = .5F;
					var sign = phase == MoonPhase.UpperHalf_Curve ? +1F : -1F;
					var chordAngleDeg = 45F;
					var chordAngleRad = chordAngleDeg * MathF.PI / 180;
					var nx = MathF.Cos(chordAngleRad);
					var ny = MathF.Sin(chordAngleRad);
					var dx = sign * nx * radiusOfMainCircle * offsetFactor;
					var dy = sign * ny * radiusOfMainCircle * offsetFactor;
					var c2x = cx + dx;
					var c2y = cy + dy;
					var r2 = radiusOfMainCircle * innerScale;
					var crescentPath = MoonPainterHelper.BuildCrescentPath(cx, cy, radiusOfMainCircle, c2x, c2y, r2);
					if (crescentPath is not null)
					{
						// Using type Even-Odd is unnecessary here.
						crescentPath.FillType = SKPathFillType.Winding;
						@this.DrawPath(crescentPath, fillPaint);
						if (strokeWidth != 0)
						{
							@this.DrawPath(crescentPath, strokePaint);
						}
					}
					else
					{
						// Fallback: If no intersection here, using EvenOdd here.
						using var fallback = new SKPath { FillType = SKPathFillType.EvenOdd };
						fallback.AddCircle(cx, cy, radiusOfMainCircle);
						fallback.AddCircle(c2x, c2y, r2);
						@this.DrawPath(fallback, fillPaint);
						if (strokeWidth != 0)
						{
							@this.DrawCircle(cx, cy, radiusOfMainCircle, strokePaint);
						}
					}
					break;
				}
			}
		}

		/// <summary>
		/// Draws an arrow triangle into the specifie cell.
		/// </summary>
		/// <param name="cell">The cell.</param>
		/// <param name="direction">The direction.</param>
		/// <param name="sizeScale">The scale of size, related to cell size.</param>
		/// <param name="baseScale">The scale of base line, related to cell size.</param>
		/// <param name="strokeWidthScale">The stroke width scale, related to cell size.</param>
		/// <param name="strokeColor">The stroke color.</param>
		/// <param name="fillColor">The fill color.</param>
		/// <param name="mapper">The mapper instance.</param>
		public void DrawArrowTriangleToCell(
			Absolute cell,
			ArrowDirection direction,
			Scale sizeScale,
			Scale baseScale,
			Scale strokeWidthScale,
			SKColor strokeColor,
			SKColor fillColor,
			PointMapper mapper
		)
		{
			var cellSize = mapper.CellSize;
			var (x, y) = mapper.GetPoint(cell, Alignment.TopLeft);
			var cellRect = new SKRect(x, y, x + cellSize, y + cellSize);
			var strokeWidth = strokeWidthScale.Measure(cellSize);
			using var fillPaint = new SKPaint { Style = SKPaintStyle.Fill, IsAntialias = true, Color = fillColor };
			using var strokePaint = new SKPaint
			{
				Style = SKPaintStyle.Stroke,
				IsAntialias = true,
				StrokeWidth = strokeWidth,
				StrokeJoin = SKStrokeJoin.Round,
				StrokeCap = SKStrokeCap.Round,
				Color = strokeColor
			};

			using var arrowPath = ArrowPainterHelper.CreateArrowTrianglePath(cellRect, sizeScale, strokeWidth, direction, baseScale);
			@this.DrawPath(arrowPath, fillPaint);
			@this.DrawPath(arrowPath, strokePaint);
		}

		/// <summary>
		/// Draws an arrow symbol into the specified cell.
		/// </summary>
		/// <param name="cell">The cell.</param>
		/// <param name="direction">The direction.</param>
		/// <param name="triangleWidthScale">The triangle width scale, related to cell size.</param>
		/// <param name="triangleHeightScale">The triangle height scale, related to cell size.</param>
		/// <param name="shaftWidthScale">The shaft width scale, related to cell size.</param>
		/// <param name="shaftHeightScale">The shaft height scale, related to cell size.</param>
		/// <param name="strokeWidthScale">The stroke width scale, related to cell size.</param>
		/// <param name="strokeColor">The stroke coloor.</param>
		/// <param name="fillColor">The fill color.</param>
		/// <param name="mapper">The mapper instance.</param>
		public void DrawArrowToCell(
			Absolute cell,
			ArrowDirection direction,
			Scale triangleWidthScale,
			Scale triangleHeightScale,
			Scale shaftWidthScale,
			Scale shaftHeightScale,
			Scale strokeWidthScale,
			SKColor strokeColor,
			SKColor fillColor,
			PointMapper mapper
		)
		{
			var cellSize = mapper.CellSize;
			var (x, y) = mapper.GetPoint(cell, Alignment.TopLeft);
			var cellRect = new SKRect(x, y, x + cellSize, y + cellSize);
			var strokeWidth = strokeWidthScale.Measure(cellSize);
			using var fillPaint = new SKPaint { Style = SKPaintStyle.Fill, IsAntialias = true, Color = fillColor };
			using var strokePaint = new SKPaint
			{
				Style = SKPaintStyle.Stroke,
				IsAntialias = true,
				StrokeWidth = strokeWidth,
				StrokeJoin = SKStrokeJoin.Round,
				StrokeCap = SKStrokeCap.Round,
				Color = strokeColor
			};

			using var arrowPath = ArrowPainterHelper.CreateArrowPath(
				cellRect,
				triangleWidthScale,
				triangleHeightScale,
				shaftWidthScale,
				shaftHeightScale,
				strokeWidth,
				direction
			);
			@this.DrawPath(arrowPath, fillPaint);
			@this.DrawPath(arrowPath, strokePaint);
		}
	}
}

/// <summary>
/// The helper type that draws for moon.
/// </summary>
file static class MoonPainterHelper
{
	/// <summary>
	/// Calculates intersection of two circles, finding out two points intersected. If found, return <see langword="true"/>.
	/// </summary>
	/// <param name="x0">X value of point, in circle 1.</param>
	/// <param name="y0">Y value of point, in circle 1.</param>
	/// <param name="r0">Radius of circle 1.</param>
	/// <param name="x1">X value of point, in circle 2.</param>
	/// <param name="y1">Y value of point, in circle 2.</param>
	/// <param name="r1">Radius of circle 2.</param>
	/// <param name="p1">Intersection point 1.</param>
	/// <param name="p2">Intersection point 2.</param>
	/// <returns>A <see cref="bool"/> result.</returns>
	public static bool CircleCircleIntersection(
		float x0,
		float y0,
		float r0,
		float x1,
		float y1,
		float r1,
		out SKPoint p1,
		out SKPoint p2
	)
	{
		p1 = default;
		p2 = default;
		var dx = x1 - x0;
		var dy = y1 - y0;
		var d = MathF.Sqrt(dx * dx + dy * dy);

		// No solutions: separate or one contains the other or coincident circles.
		if (d > r0 + r1 || d < MathF.Abs(r0 - r1) || d == 0 && r0 == r1)
		{
			return false;
		}

		// Distance from 0-center to line joining intersection points.
		var a = (r0 * r0 - r1 * r1 + d * d) / (2 * d);
		var h = MathF.Sqrt(MathF.Max(0F, r0 * r0 - a * a));
		var xm = x0 + a * dx / d;
		var ym = y0 + a * dy / d;
		var rx = -dy * (h / d);
		var ry = dx * (h / d);
		p1 = new(xm + rx, ym + ry);
		p2 = new(xm - rx, ym - ry);
		return true;
	}

	/// <summary>
	/// Build crescent path if can, from main circle <c>(<paramref name="cx"/>, <paramref name="cy"/>, <paramref name="r"/>)</c>
	/// and side circle <c>(<paramref name="c2x"/>, <paramref name="c2y"/>, <paramref name="r2"/>)</c>.
	/// If failed to build, <see langword="null"/> will be returned.
	/// </summary>
	/// <param name="cx">X value of center point of main circle.</param>
	/// <param name="cy">Y value of center point of main circle.</param>
	/// <param name="r">Radius of main circle.</param>
	/// <param name="c2x">X value of center point of side circle.</param>
	/// <param name="c2y">Y value of center point of side circle.</param>
	/// <param name="r2">Radius of side circle.</param>
	/// <returns>The path built. If failed to be built, <see langword="null"/>.</returns>
	public static SKPath? BuildCrescentPath(float cx, float cy, float r, float c2x, float c2y, float r2)
	{
		if (!CircleCircleIntersection(cx, cy, r, c2x, c2y, r2, out var p1, out var p2))
		{
			// No intersection.
			return null;
		}

		var a1 = angleDegreeOf(cx, cy, p1);
		var a2 = angleDegreeOf(cx, cy, p2);
		var b1 = angleDegreeOf(c2x, c2y, p1);
		var b2 = angleDegreeOf(c2x, c2y, p2);
		var sweepMainCCW = angleDiffCCW(a1, a2); // 0..360
		var midMainAngle = a1 + sweepMainCCW / 2;
		var midMainPoint = pointOnCircle(cx, cy, r, midMainAngle);
		var distMidToC2 = Math.Sqrt((midMainPoint.X - c2x) * (midMainPoint.X - c2x) + (midMainPoint.Y - c2y) * (midMainPoint.Y - c2y));
		var midMainInsideC2 = distMidToC2 < r2 - 1E-6;
		var finalMainSweepDeg = !midMainInsideC2 ? sweepMainCCW : sweepMainCCW - 360;
		var sweepMaskCCW = angleDiffCCW(b2, b1);
		var midMaskAngle = b2 + sweepMaskCCW / 2;
		var midMaskPoint = pointOnCircle(c2x, c2y, r2, midMaskAngle);
		var distMidMaskToC = Math.Sqrt((midMaskPoint.X - cx) * (midMaskPoint.X - cx) + (midMaskPoint.Y - cy) * (midMaskPoint.Y - cy));
		var midMaskInsideMain = distMidMaskToC < r - 1E-6;
		var finalMaskSweepDeg = midMaskInsideMain ? sweepMaskCCW : sweepMaskCCW - 360;

		// Build path: move P1 -> arc on main (a1, finalMainSweepDeg) -> arc on mask (b2, finalMaskSweepDeg) back to P1.
		var path = new SKPath();
		path.MoveTo(p1);
		path.ArcTo(new(cx - r, cy - r, cx + r, cy + r), a1, finalMainSweepDeg, false);
		path.ArcTo(new(c2x - r2, c2y - r2, c2x + r2, c2y + r2), b2, finalMaskSweepDeg, false);
		path.Close();
		return path;


		static float angleDiffCCW(float fromDegree, float toDegree)
		{
			var df = normalize360(toDegree) - normalize360(fromDegree);
			return df < 0 ? df + 360 : df;


			static float normalize360(float deg) => (deg % 360 + 360) % 360;
		}

		static float angleDegreeOf(float cx, float cy, SKPoint p) => MathF.Atan2(p.Y - cy, p.X - cx) * 180 / MathF.PI;

		static SKPoint pointOnCircle(float cx, float cy, float r, float deg)
		{
			var rad = deg * MathF.PI / 180;
			return new(cx + r * MathF.Cos(rad), cy + r * MathF.Sin(rad));
		}
	}
}

/// <summary>
/// The helper type that draws for arrows.
/// </summary>
file static class ArrowPainterHelper
{
	/// <summary>
	/// Creates a path of arrow, pointing to the specified direction.
	/// </summary>
	/// <param name="cell">The cell rectangle.</param>
	/// <param name="sizeScale">The scale of size.</param>
	/// <param name="strokeWidth">The stroke width.</param>
	/// <param name="direction">The direction.</param>
	/// <param name="baseScale">The base scale.</param>
	/// <returns>
	/// A <see cref="SKPath"/> instance.
	/// Note: You must call <see cref="SKNativeObject.Dispose()"/> method manually
	/// or use <see langword="using"/> statements to release resources.
	/// </returns>
	/// <seealso cref="SKPath"/>
	/// <seealso cref="SKNativeObject.Dispose()"/>
	public static SKPath CreateArrowTrianglePath(SKRect cell, Scale sizeScale, float strokeWidth, ArrowDirection direction, Scale baseScale)
	{
		var cellSize = Math.Min(cell.Width, cell.Height);
		var halfStroke = strokeWidth / 2;
		var insetRect = new SKRect(cell.Left + halfStroke, cell.Top + halfStroke, cell.Right - halfStroke, cell.Bottom - halfStroke);
		var insetCellSize = Math.Min(insetRect.Width, insetRect.Height);
		var desiredLength = sizeScale.Measure(cellSize);
		var effectiveLength = Math.Min(desiredLength, insetCellSize);
		if (effectiveLength <= 0)
		{
			effectiveLength = 1F;
		}

		var cx = (insetRect.Left + insetRect.Right) / 2;
		var cy = (insetRect.Top + insetRect.Bottom) / 2;
		var halfLength = effectiveLength / 2;
		var baseWidth = baseScale.Measure(effectiveLength);
		var tip = new SKPoint(cx, cy - halfLength);
		var baseLeft = new SKPoint(cx - baseWidth / 2, cy + halfLength);
		var baseRight = new SKPoint(cx + baseWidth / 2, cy + halfLength);
		var angleDeg = direction.AngleDegrees;
		var rtTip = rotateAround(tip, new(cx, cy), angleDeg);
		var rtBL = rotateAround(baseLeft, new(cx, cy), angleDeg);
		var rtBR = rotateAround(baseRight, new(cx, cy), angleDeg);
		var path = new SKPath { FillType = SKPathFillType.EvenOdd };
		path.MoveTo(rtTip);
		path.LineTo(rtBL);
		path.LineTo(rtBR);
		path.Close();
		return path;


		static SKPoint rotateAround(SKPoint p, SKPoint center, float degreesClockwise)
		{
			var rad = degreesClockwise * MathF.PI / 180;
			var cosine = MathF.Cos(rad);
			var sine = MathF.Sin(rad);
			var dx = p.X - center.X;
			var dy = p.Y - center.Y;
			var rx = dx * cosine + dy * sine;
			var ry = -dx * sine + dy * cosine;
			return new(center.X + rx, center.Y + ry);
		}
	}

	/// <summary>
	/// Draws arrow path.
	/// </summary>
	/// <param name="cellRect">The cell rectangle.</param>
	/// <param name="triangleWidthScale">The triangle width scale, related to cell size.</param>
	/// <param name="triangleHeightScale">The triangle height scale, related to cell size.</param>
	/// <param name="shaftWidthScale">The shaft width scale, related to cell size.</param>
	/// <param name="shaftLengthScale">The shaft height scale, related to cell size.</param>
	/// <param name="strokeWidth">The stroke width.</param>
	/// <param name="direction">The direction.</param>
	/// <returns><inheritdoc cref="CreateArrowTrianglePath(SKRect, Scale, float, ArrowDirection, Scale)" path="/returns"/></returns>
	/// <exception cref="InvalidOperationException">Throws when shaft width is greater than triangle width.</exception>
	public static SKPath CreateArrowPath(
		SKRect cellRect,
		Scale triangleWidthScale,
		Scale triangleHeightScale,
		Scale shaftWidthScale,
		Scale shaftLengthScale,
		float strokeWidth,
		ArrowDirection direction
	)
	{
		var cellSize = Math.Min(cellRect.Width, cellRect.Height);
		var halfStroke = strokeWidth / 2;
		var insetRect = new SKRect(cellRect.Left + halfStroke, cellRect.Top + halfStroke, cellRect.Right - halfStroke, cellRect.Bottom - halfStroke);
		var insetCellSize = Math.Min(insetRect.Width, insetRect.Height);
		if (insetCellSize <= 0)
		{
			insetCellSize = 1;
		}

		var triangleWidth = triangleWidthScale.Measure(cellSize);
		var triangleHeight = triangleHeightScale.Measure(cellSize);
		var shaftWidth = shaftWidthScale.Measure(cellSize);
		var shaftHeight = shaftLengthScale.Measure(cellSize);
		var totalLength = triangleHeight + shaftHeight;
		if (totalLength > insetCellSize)
		{
			var scale = insetCellSize / totalLength;
			triangleWidth *= scale;
			triangleHeight *= scale;
			shaftWidth *= scale;
			shaftHeight *= scale;
			totalLength = triangleHeight + shaftHeight;
		}

		triangleHeight = Math.Max(triangleHeight, 1F);
		triangleWidth = Math.Max(triangleWidth, 1F);
		shaftWidth = Math.Max(shaftWidth, .5F);

		var cx = (insetRect.Left + insetRect.Right) / 2;
		var cy = (insetRect.Top + insetRect.Bottom) / 2;
		var halfTotal = totalLength / 2;
		var tipY = -halfTotal;
		var baseY = tipY + triangleHeight;
		var shaftBottomY = baseY + shaftHeight;
		var halfTriangleWidth = triangleWidth / 2;
		var halfShaftWidth = shaftWidth / 2;
		if (halfShaftWidth > halfTriangleWidth)
		{
			throw new InvalidOperationException("Invalid size of shaft size - it is greater than triangle!");
		}

		var tipPoint = new SKPoint(0, tipY);
		var baseLeftPoint = new SKPoint(-halfTriangleWidth, baseY);
		var shaftBottomLeftPoint = new SKPoint(-halfShaftWidth, shaftBottomY);
		var shaftBottomRightPoint = new SKPoint(halfShaftWidth, shaftBottomY);
		var baseRightPoint = new SKPoint(halfTriangleWidth, baseY);
		var shaftTopLeftPoint = new SKPoint(-halfShaftWidth, baseY);
		var shaftTopRightPoint = new SKPoint(halfShaftWidth, baseY);
		var angleDegree = direction.AngleDegrees;

		var path = new SKPath { FillType = SKPathFillType.EvenOdd };
		path.MoveTo(RotateAndTranslate(tipPoint, angleDegree, cx, cy));
		path.LineTo(RotateAndTranslate(baseLeftPoint, angleDegree, cx, cy));
		path.LineTo(RotateAndTranslate(shaftTopLeftPoint, angleDegree, cx, cy));
		path.LineTo(RotateAndTranslate(shaftBottomLeftPoint, angleDegree, cx, cy));
		path.LineTo(RotateAndTranslate(shaftBottomRightPoint, angleDegree, cx, cy));
		path.LineTo(RotateAndTranslate(shaftTopRightPoint, angleDegree, cx, cy));
		path.LineTo(RotateAndTranslate(baseRightPoint, angleDegree, cx, cy));
		path.Close();
		return path;


		static SKPoint RotateAndTranslate(SKPoint p, float degreesClockwise, float tx, float ty)
		{
			var rad = degreesClockwise * MathF.PI / 180;
			var cosine = MathF.Cos(rad);
			var sine = MathF.Sin(rad);
			var rx = p.X * cosine + p.Y * sine;
			var ry = -p.X * sine + p.Y * cosine;
			return new(tx + rx, ty + ry);
		}
	}
}
