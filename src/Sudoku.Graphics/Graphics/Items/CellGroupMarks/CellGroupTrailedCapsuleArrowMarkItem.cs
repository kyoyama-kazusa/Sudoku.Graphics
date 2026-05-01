namespace Sudoku.Graphics.Items.CellGroupMarks;

/// <summary>
/// Represents cell group capsule with arrow line mark item (trailed capsule).
/// </summary>
public sealed record CellGroupTrailedCapsuleArrowMarkItem : CellGroupMarkItem
{
	/// <summary>
	/// Indicates rotation degrees of arrow caps, in angle.
	/// </summary>
	public required float HalfArrowCapRotationDegrees { get; init; }

	/// <inheritdoc/>
	public override ItemType Type => ItemType.CellGroup_CapsuleWithArrowLine;

	/// <summary>
	/// Indicates scale of capsule, related to cell size.
	/// </summary>
	public required Scale CapsuleSizeScale { get; init; }

	/// <summary>
	/// Indicates scale of arrow cap length, related to cell size.
	/// </summary>
	public required Scale ArrowCapLengthScale { get; init; }

	/// <inheritdoc/>
	public required override Scale StrokeWidthScale { get; init; }

	/// <inheritdoc/>
	public required override SerializableColor StrokeColor { get; init; }

	/// <inheritdoc/>
	public override SerializableColor FillColor { get; init; }

	/// <summary>
	/// Indicates arrow line cells.
	/// </summary>
	public required Absolute[] TrailCells { get; init; }


	/// <inheritdoc/>
	protected internal override void DrawTo(Canvas canvas)
	{
		var mapper = canvas.Mapper;
		var cellSize = mapper.CellSize;
		var strokeWidth = StrokeWidthScale.Measure(cellSize);
		using var fillPaint = new SKPaint { Style = SKPaintStyle.Fill, Color = FillColor, IsAntialias = true };
		using var strokePaint = new SKPaint
		{
			Style = SKPaintStyle.Stroke,
			Color = StrokeColor,
			StrokeWidth = strokeWidth,
			IsAntialias = true
		};
		using var linePaint = new SKPaint
		{
			Style = SKPaintStyle.Stroke,
			Color = StrokeColor,
			StrokeWidth = strokeWidth,
			IsAntialias = true,
			StrokeCap = SKStrokeCap.Round,
			StrokeJoin = SKStrokeJoin.Round
		};

		var backingCanvas = canvas.BackingCanvas;
		Debug.Assert(
			CapsuleTrailDrawer.DrawCapsule(
				backingCanvas,
				from cell in Cells select mapper.GetPoint(cell, Alignment.TopLeft),
				cellSize,
				CapsuleSizeScale,
				fillPaint,
				strokePaint,
				out var capsulePath
			)
		);

		var trailCellPoints = from cell in TrailCells select mapper.GetPoint(cell, Alignment.TopLeft);
		using var trailPath = CapsuleTrailDrawer.BuildTrailPath(capsulePath, trailCellPoints, cellSize);
		backingCanvas.DrawPath(trailPath, linePaint);

		var p0 = new SKPoint(trailCellPoints[^2].X + cellSize / 2, trailCellPoints[^2].Y + cellSize / 2);
		var p1 = new SKPoint(trailCellPoints[^1].X + cellSize / 2, trailCellPoints[^1].Y + cellSize / 2);
		var arrowLength = ArrowCapLengthScale.Measure(cellSize);
		backingCanvas.DrawArrowCaps(p0, p1, arrowLength, HalfArrowCapRotationDegrees, linePaint);

		capsulePath.Dispose();
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

			// Calculate for the margin of the capsule.
			var margin = (1 - (float)capsuleSizeScale) * cellSize / 2;

			// Calculate the factors of the outer circle.
			var outerLeft = minX;
			var outerTop = minY;
			var outerRight = maxX + cellSize;
			var outerBottom = maxY + cellSize;
			var rect = new SKRect(outerLeft + margin, outerTop + margin, outerRight - margin, outerBottom - margin);

			// Construct a path.
			var path = new SKPath();
			if (capsuleCells.Length == 1)
			{
				// Degenerate to a circle if length is equal to 1.
				path.AddOval(rect);
				return path;
			}

			// A normal rounded rectangle: radius = half size of the short side.
			var radius = horizontal ? rect.Height / 2 : rect.Width / 2;
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
}
