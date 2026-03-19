namespace Sudoku.Graphics;

public partial class SKCanvasDrawings
{
	extension(SKCanvas @this)
	{
		/// <summary>
		/// Draws capsule in cells.
		/// </summary>
		/// <param name="capsuleCells">The capsule cells.</param>
		/// <param name="trailCells">The arrow line cells.</param>
		/// <param name="capsuleSizeScale">The scale of capsule size, related to cell size.</param>
		/// <param name="strokeColor">The stroke color.</param>
		/// <param name="strokeWidthScale">The scale of stroke width, related to cell size.</param>
		/// <param name="capsuleFillColor">The fill color of capsule.</param>
		/// <param name="arrowCapLengthScale">The scale of arrow cap length.</param>
		/// <param name="arrowHalfAngleDegrees">The half of rotation degrees of arrow cap, in angle.</param>
		/// <param name="mapper">The point mapper instance.</param>
		public void DrawCapsuleWithArrowLine(
			ReadOnlySpan<Absolute> capsuleCells,
			ReadOnlySpan<Absolute> trailCells,
			Scale capsuleSizeScale,
			SKColor strokeColor,
			Scale strokeWidthScale,
			SKColor capsuleFillColor,
			Scale arrowCapLengthScale,
			float arrowHalfAngleDegrees,
			PointMapper mapper
		)
		{
			var cellSize = mapper.CellSize;
			var strokeWidth = strokeWidthScale.Measure(cellSize);
			using var fillPaint = new SKPaint { Style = SKPaintStyle.Fill, Color = capsuleFillColor, IsAntialias = true };
			using var strokePaint = new SKPaint
			{
				Style = SKPaintStyle.Stroke,
				Color = strokeColor,
				StrokeWidth = strokeWidth,
				IsAntialias = true
			};
			using var linePaint = new SKPaint
			{
				Style = SKPaintStyle.Stroke,
				Color = strokeColor,
				StrokeWidth = strokeWidth,
				IsAntialias = true,
				StrokeCap = SKStrokeCap.Round,
				StrokeJoin = SKStrokeJoin.Round
			};

			Debug.Assert(
				CapsuleTrailDrawer.DrawCapsule(
					@this,
					from cell in capsuleCells select mapper.GetPoint(cell, Alignment.TopLeft),
					cellSize,
					capsuleSizeScale,
					fillPaint,
					strokePaint,
					out var capsulePath
				)
			);

			var trailCellPoints = from cell in trailCells select mapper.GetPoint(cell, Alignment.TopLeft);
			using var trailPath = CapsuleTrailDrawer.BuildTrailPath(capsulePath, trailCellPoints, cellSize);
			@this.DrawPath(trailPath, linePaint);

			var p0 = new SKPoint(trailCellPoints[^2].X + cellSize / 2, trailCellPoints[^2].Y + cellSize / 2);
			var p1 = new SKPoint(trailCellPoints[^1].X + cellSize / 2, trailCellPoints[^1].Y + cellSize / 2);
			var arrowLength = arrowCapLengthScale.Measure(cellSize);
			CapsuleTrailDrawer.DrawArrowCaps(@this, p0, p1, arrowLength, arrowHalfAngleDegrees, linePaint);

			capsulePath.Dispose();
		}
	}
}

/// <summary>
/// Provides a way to draw capsules and trails.
/// </summary>
file static class CapsuleTrailDrawer
{
	/// <summary>
	/// Draws a capsule.
	/// </summary>
	/// <param name="canvas">The canvas instance.</param>
	/// <param name="capsuleCells">
	/// The cells of capsule, specified by <see cref="SKPoint"/> instances (top-left point of that cells).
	/// </param>
	/// <param name="cellSize">The size of each cell.</param>
	/// <param name="capsuleSizeScale">The capsule size scale, related to cell size.</param>
	/// <param name="fillPaint">The fill paint.</param>
	/// <param name="strokePaint">The stroke paint.</param>
	/// <param name="capsulePath">
	/// Capsule path generated.
	/// You should use <see langword="using"/> statements or manually call method <c>Dispose</c> to release memory.
	/// </param>
	/// <returns>A <see cref="bool"/> result indicating whether the path is generated successfully.</returns>
	public static bool DrawCapsule(
		SKCanvas canvas,
		ReadOnlySpan<SKPoint> capsuleCells,
		float cellSize,
		Scale capsuleSizeScale,
		SKPaint fillPaint,
		SKPaint strokePaint,
		[NotNullWhen(true)] out SKPath? capsulePath
	)
	{
		if (capsuleCells.Length == 0)
		{
			capsulePath = null;
			return false;
		}

		capsulePath = buildCapsulePath(capsuleCells, cellSize, capsuleSizeScale);
		if (fillPaint is not null)
		{
			canvas.DrawPath(capsulePath, fillPaint);
		}
		if (strokePaint is not null)
		{
			canvas.DrawPath(capsulePath, strokePaint);
		}
		return true;


		static SKPath buildCapsulePath(ReadOnlySpan<SKPoint> capsuleCells, float cellSize, Scale capsuleSizeScale)
		{
			var horizontal = isHorizontal(capsuleCells);
			var minX = capsuleCells.Min(static c => c.X);
			var minY = capsuleCells.Min(static c => c.Y);
			var maxX = capsuleCells.Max(static c => c.X);
			var maxY = capsuleCells.Max(static c => c.Y);
			var shortSide = capsuleSizeScale.Measure(cellSize);

			SKRect rect;
			float radius;
			if (horizontal)
			{
				var width = maxX - minX + cellSize;
				var height = shortSide;
				var top = minY + (cellSize - height) / 2;
				rect = new(minX, top, minX + width, top + height);
				radius = height / 2;
			}
			else
			{
				var width = shortSide;
				var height = maxY - minY + cellSize;
				var left = minX + (cellSize - width) / 2;
				rect = new(left, minY, left + width, minY + height);
				radius = width / 2;
			}

			var path = new SKPath();
			path.AddRoundRect(new SKRoundRect(rect, radius, radius));
			return path;
		}

		static bool isHorizontal(ReadOnlySpan<SKPoint> cells)
			=> cells[0] is var first && (cells.Length < 2 || cells.All(c => MathF.Abs(c.Y - first.Y) < 1E-3F));
	}

	/// <summary>
	/// Build a path of trail lines.
	/// </summary>
	/// <param name="capsulePath">The capsule path.</param>
	/// <param name="trailCells">The trail cells.</param>
	/// <param name="cellSize">The size of each cell.</param>
	/// <returns>A path generated.</returns>
	public static SKPath BuildTrailPath(SKPath capsulePath, ReadOnlySpan<SKPoint> trailCells, float cellSize)
	{
		var path = new SKPath();
		if (trailCells.Length < 2)
		{
			return path;
		}

		// Find for boundary point leaving this capsule, from the first segment of trail line (from cell 1 to cell 2).
		// Generally we know that the first cell is inside the capsule, and the second is not.
		var p0 = ToCenter(trailCells[0], cellSize);
		var p1 = ToCenter(trailCells[1], cellSize);
		var startOnBorder = findBorderPointByBinarySearch(capsulePath, p0, p1);

		path.MoveTo(startOnBorder);
		path.LineTo(p1);
		for (var i = 2; i < trailCells.Length; i++)
		{
			path.LineTo(ToCenter(trailCells[i], cellSize));
		}
		return path;


		static SKPoint ToCenter(SKPoint cell, float cellSize)
			=> new(cell.X + cellSize / 2, cell.Y + cellSize / 2);

		static SKPoint findBorderPointByBinarySearch(SKPath capsulePath, SKPoint inside, SKPoint outside)
		{
			// Here we use binary search to find intersection point of that boundary.
			// We suppose that <paramref name="inside"/> is inside capsule and <paramref name="outside"/> is not.
			var (low, high) = (0F, 1F);
			for (var i = 0; i < 28; i++)
			{
				var mid = (low + high) / 2;
				var p = lerp(inside, outside, mid);
				if (capsulePath.Contains(p.X, p.Y))
				{
					low = mid;
				}
				else
				{
					high = mid;
				}
			}
			return lerp(inside, outside, low);


			static SKPoint lerp(SKPoint a, SKPoint b, float t) => new(a.X + (b.X - a.X) * t, a.Y + (b.Y - a.Y) * t);
		}
	}

	/// <summary>
	/// Draw arrow caps.
	/// </summary>
	/// <param name="canvas">The canvas instance.</param>
	/// <param name="penultimatePoint">The penuleimate point (i.e. <c>points[^2]</c>).</param>
	/// <param name="endPoint">The end point.</param>
	/// <param name="arrowCapLength">The arrow length.</param>
	/// <param name="arrowCapHalfAngleDegrees">The arrow half angle degrees.</param>
	/// <param name="paint">The stroke paint.</param>
	public static void DrawArrowCaps(
		SKCanvas canvas,
		SKPoint penultimatePoint,
		SKPoint endPoint,
		float arrowCapLength,
		float arrowCapHalfAngleDegrees,
		SKPaint paint
	)
	{
		var dir = normalize(new(endPoint.X - penultimatePoint.X, endPoint.Y - penultimatePoint.Y));
		if (float.IsNaN(dir.X) || float.IsNaN(dir.Y))
		{
			return;
		}

		var angle = arrowCapHalfAngleDegrees * MathF.PI / 180;

		// In reversed direction, drawing arrow caps.
		var back = new SKPoint(-dir.X, -dir.Y);
		var leftDir = rotate(back, angle);
		var rightDir = rotate(back, -angle);
		var left = new SKPoint(endPoint.X + leftDir.X * arrowCapLength, endPoint.Y + leftDir.Y * arrowCapLength);
		var right = new SKPoint(endPoint.X + rightDir.X * arrowCapLength, endPoint.Y + rightDir.Y * arrowCapLength);
		using var arrowPath = new SKPath();
		arrowPath.MoveTo(endPoint);
		arrowPath.LineTo(left);
		arrowPath.MoveTo(endPoint);
		arrowPath.LineTo(right);
		canvas.DrawPath(arrowPath, paint);


		static SKPoint normalize(SKPoint v)
		{
			var len = MathF.Sqrt(v.X * v.X + v.Y * v.Y);
			return len < 1E-6F ? new(float.NaN, float.NaN) : new(v.X / len, v.Y / len);
		}

		static SKPoint rotate(SKPoint v, float radians)
		{
			var cosine = MathF.Cos(radians);
			var sine = MathF.Sin(radians);
			return new(v.X * cosine - v.Y * sine, v.X * sine + v.Y * cosine);
		}
	}
}
