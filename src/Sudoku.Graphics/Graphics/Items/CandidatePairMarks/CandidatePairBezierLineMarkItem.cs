namespace Sudoku.Graphics.Items.CandidatePairMarks;

/// <summary>
/// Represents candidate pair Bezier line mark item.
/// </summary>
public sealed record CandidatePairBezierLineMarkItem : CandidatePairLineMarkItem
{
	/// <inheritdoc/>
	public override ItemType Type => ItemType.CandidatePair_BezierLine;


	/// <inheritdoc/>
	protected internal override void DrawTo(Canvas canvas)
	{
		// Define a list of variables to be used.
		var backingCanvas = canvas.BackingCanvas;
		var mapper = canvas.Templates[TemplateIndex].Mapper;
		var cellSize = mapper.CellSize;
		var candidateSizeMeasurer1 = CandidatePosition.GetLocatorMeasurer(CandidatePosition1, cellSize);
		var candidateSizeMeasurer2 = CandidatePosition.GetLocatorMeasurer(CandidatePosition2, cellSize);
		var center1 = mapper.GetPoint(CandidatePosition1, Alignment.Center);
		var center2 = mapper.GetPoint(CandidatePosition2, Alignment.Center);
		var radius1 = Candidate1SizeScale.Measure(candidateSizeMeasurer1) / 2;
		var radius2 = Candidate2SizeScale.Measure(candidateSizeMeasurer2) / 2;

		// Check for direction of trhe straight line (connected between two center points).
		var dx = center2.X - center1.X;
		var dy = center2.Y - center1.Y;
		var length = MathF.Sqrt(dx * dx + dy * dy);
		if (length < 1E-3F)
		{
			throw new InvalidOperationException("Two points are overlapped or too close.");
		}

		// Define pi / 4 (45 degrees) and -pi / 4 (-45 degrees).
		const float quarterPi = 45F * MathF.PI / 180F;
		const float minusQuarterPi = -45F * MathF.PI / 180F;

		// Define rotation degrees in order not to draw Bezier curves outside the grid canvas.
		var (startAngle, endAngle) = mapper.IsAlignedAs(LocatorGridAlignment.FirstRow, CandidatePosition1, CandidatePosition2)
			&& CandidatePosition1.IsSideWith(CandidatePosition2, Direction4.Right, mapper, true)
			|| mapper.IsAlignedAs(LocatorGridAlignment.LastRow, CandidatePosition1, CandidatePosition2)
			&& CandidatePosition1.IsSideWith(CandidatePosition2, Direction4.Left, mapper, true)
			|| mapper.IsAlignedAs(LocatorGridAlignment.FirstColumn, CandidatePosition1, CandidatePosition2)
			&& CandidatePosition1.IsSideWith(CandidatePosition2, Direction4.Up, mapper, true)
			|| mapper.IsAlignedAs(LocatorGridAlignment.LastColumn, CandidatePosition1, CandidatePosition2)
			&& CandidatePosition1.IsSideWith(CandidatePosition2, Direction4.Down, mapper, true)
			? (minusQuarterPi, quarterPi)
			: (quarterPi, minusQuarterPi);

		// Handle length. You can adjust '0.35' and '0.8' to make such curve better-looking.
		var handleLength = MathF.Min(length * 0.35F, cellSize * 0.8F);

		// Due to rotation of curve, the start and end terminal points won't point to center of candidates.
		// We should adjust slightly in order to point to center of candidates again.
		var baseDirection = new SKPoint(dx / length, dy / length);
		var startDirection = rotate(baseDirection, startAngle);
		var endDirection = rotate(baseDirection, endAngle);
		var p1 = new SKPoint(center1.X + startDirection.X * radius1, center1.Y + startDirection.Y * radius1);
		var p2 = new SKPoint(center2.X - endDirection.X * radius2, center2.Y - endDirection.Y * radius2);

		// Define two Bezier points.
		var cp1 = new SKPoint(p1.X + startDirection.X * handleLength, p1.Y + startDirection.Y * handleLength);
		var cp2 = new SKPoint(p2.X - endDirection.X * handleLength, p2.Y - endDirection.Y * handleLength);

		using var strokePaint = new SKPaint
		{
			Style = SKPaintStyle.Stroke,
			IsAntialias = true,
			Color = StrokeColor,
			StrokeWidth = StrokeWidthScale.Measure(cellSize),
			StrokeCap = SKStrokeCap.Round,
			PathEffect = DashSequence
		};

		using var path = new SKPath();
		path.MoveTo(p1);
		path.CubicTo(cp1, cp2, p2);
		backingCanvas.DrawPath(path, strokePaint);

		using var arrowStrokePaint = new SKPaint
		{
			Style = SKPaintStyle.Stroke,
			IsAntialias = true,
			Color = StrokeColor,
			StrokeWidth = StrokeWidthScale.Measure(cellSize),
			StrokeCap = SKStrokeCap.Round
		};

		// Make terminal arrow point to center of corresponding candidate.
		var arrowLength = ArrowCapLengthScale.Measure(cellSize);
		drawArrowTowardCenter(backingCanvas, p2, center2, arrowLength, HalfArrowCapRotationDegrees, arrowStrokePaint);


		static void drawArrowTowardCenter(
			SKCanvas canvas,
			SKPoint pointOnCircle,
			SKPoint center,
			float arrowCapLength,
			float arrowCapHalfAngleDegrees,
			SKPaint paint
		)
		{
			// Make arrow point to center of circle.
			var direction = normalize(new(center.X - pointOnCircle.X, center.Y - pointOnCircle.Y));
			if (direction.X is float.NaN || direction.Y is float.NaN)
			{
				return;
			}

			// Here I asked for ChatGPT and he told me here we should use formula 'arrowCapHalfAngleDegrees * MathF.PI / 180F'.
			// However this is not correct to draw - such arrow would point to its back side.
			var angle = (180F - arrowCapHalfAngleDegrees) * MathF.PI / 180F;
			var leftDirection = rotate(direction, angle);
			var rightDirection = rotate(direction, -angle);
			var left = new SKPoint(pointOnCircle.X + leftDirection.X * arrowCapLength, pointOnCircle.Y + leftDirection.Y * arrowCapLength);
			var right = new SKPoint(pointOnCircle.X + rightDirection.X * arrowCapLength, pointOnCircle.Y + rightDirection.Y * arrowCapLength);
			using var arrowPath = new SKPath();
			arrowPath.MoveTo(pointOnCircle);
			arrowPath.LineTo(left);
			arrowPath.MoveTo(pointOnCircle);
			arrowPath.LineTo(right);
			canvas.DrawPath(arrowPath, paint);
		}

		static SKPoint normalize(SKPoint v)
		{
			var length = MathF.Sqrt(v.X * v.X + v.Y * v.Y);
			return length < 1E-6F ? new(float.NaN, float.NaN) : new(v.X / length, v.Y / length);
		}

		static SKPoint rotate(SKPoint v, float radians)
		{
			var cosine = MathF.Cos(radians);
			var sine = MathF.Sin(radians);
			return new(v.X * cosine - v.Y * sine, v.X * sine + v.Y * cosine);
		}
	}
}
