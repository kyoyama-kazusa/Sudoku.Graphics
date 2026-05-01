namespace Sudoku.Graphics.Items.CellMarks;

/// <summary>
/// Represents a cell mark item that renders a list of triangles, surrounding with cell center.
/// </summary>
public sealed record CellSurroundingTrianglesMarkItem : CellMarkItem
{
	/// <summary>
	/// Indicates the number of triangles.
	/// </summary>
	public required int TrianglesCount { get; init; }

	/// <summary>
	/// Indicates the tip distance with cell center point.
	/// </summary>
	public required Scale TipDistanceScale { get; init; }

	/// <inheritdoc/>
	public override ItemType Type => ItemType.Cell_SurroundingTriangles;


	/// <inheritdoc/>
	protected internal override void DrawTo(Canvas canvas)
	{
		if (TrianglesCount < 1)
		{
			throw new InvalidOperationException($"'{nameof(TrianglesCount)}' Expected a value between 1 and 9.");
		}

		var mapper = canvas.Mapper;
		var cellSize = mapper.CellSize;
		if (cellSize <= 0)
		{
			return;
		}

		var triangleHeight = SizeScale.Measure(cellSize);
		if (triangleHeight <= 0)
		{
			// Nothing to draw.
			return;
		}

		var backingCanvas = canvas.BackingCanvas;
		var s = 2F * triangleHeight / MathF.Sqrt(3); // Side length of equilateral triangle.
		var strokeWidth = Math.Max(0F, StrokeWidthScale.Measure(cellSize));
		using var paintFill = new SKPaint { Style = SKPaintStyle.Fill, Color = FillColor, IsAntialias = true };
		using var paintStroke = new SKPaint
		{
			Style = SKPaintStyle.Stroke,
			Color = StrokeColor,
			StrokeWidth = strokeWidth,
			PathEffect = SKPathEffect.CreateCorner(CornerRadiusScale.Measure(triangleHeight)),
			IsAntialias = true
		};

		var cellCenterPoint = mapper.GetPoint(Cell, Alignment.Center);
		if (TrianglesCount == 1)
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

			backingCanvas.DrawPath(path, paintFill);
			if (strokeWidth > 0)
			{
				backingCanvas.DrawPath(path, paintStroke);
			}
		}
		else
		{
			// Multiple triangles: arrange tips on circle around center,
			// tips point toward center. One triangle fixed at the top (i=0),
			// its tip is above center and points downwards to center.
			var maxRadius = cellSize / 2F; // Radius from center to cell edge.
			var tipRadius = TipDistanceScale.Measure(maxRadius); // Actual radius for tip positions.

			// Starting angle so that i=0 tip is at top (above center): angle = -pi/2.
			// Angle measured in radians, 0 = +X axis (right), positive = downwards rotation (screen y down).
			// Using standard math functions where sin/cos work with y-positive-down coordinate works fine.
			const float startAngle = -MathF.PI / 2;
			for (var i = 0; i < TrianglesCount; i++)
			{
				var angle = startAngle + i * (MathF.Tau / TrianglesCount);
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

				backingCanvas.DrawPath(path, paintFill);
				if (strokeWidth > 0)
				{
					backingCanvas.DrawPath(path, paintStroke);
				}
			}
		}
	}
}
