namespace Sudoku.Graphics.Items.CellPairMarks;

/// <summary>
/// Represents a cell pair bridge line mark item.
/// </summary>
public sealed record CellPairBridgeLineMarkItem : CellPairMarkItem
{
	/// <summary>
	/// Indicates whether the circles should be drawn. By default it's <see langword="true"/>.
	/// </summary>
	public bool DrawCircles { get; init; } = true;

	/// <summary>
	/// Indicates whether lines should be drawn. By default it's <see langword="true"/>.
	/// </summary>
	public bool DrawLines { get; init; } = true;

	/// <summary>
	/// Indicates the number of lines.
	/// </summary>
	public required int LinesCount { get; init; }

	/// <inheritdoc/>
	public override ItemType Type => ItemType.CellPair_BridgeLine;

	/// <summary>
	/// Indicates the scale of maximum gap among lines, related to circle diameter.
	/// </summary>
	public required Scale LinesMaxGapScale { get; init; }

	/// <summary>
	/// Indicates the scale of circle, related to cell size.
	/// </summary>
	public required Scale CircleDiameterScale { get; init; }

	/// <inheritdoc/>
	public required override Scale StrokeWidthScale { get; init; }

	/// <inheritdoc/>
	public required override SerializableColor FillColor { get; init; }

	/// <inheritdoc/>
	public required override SerializableColor StrokeColor { get; init; }


	/// <inheritdoc/>
	protected internal override void DrawTo(Canvas canvas)
	{
		var backingCanvas = canvas.BackingCanvas;
		var mapper = canvas.Mapper;
		if (LinesCount is < 1 or > 8)
		{
			throw new NotSupportedException("The specified number of lines is not supported due to complexity or invalidity.");
		}

		var cellSize = mapper.CellSize;
		var (center1, center2) = (mapper.GetPoint(Cell1, Alignment.Center), mapper.GetPoint(Cell2, Alignment.Center));

		// Radius.
		var diameter = CircleDiameterScale.Measure(cellSize);
		var r = diameter / 2;
		if (r <= 0)
		{
			// Nothing to draw.
			return;
		}

		// Direction from <c>c1</c> to <c>c2</c>.
		var direction = new SKPoint(center2.X - center1.X, center2.Y - center1.Y);
		var distance = MathF.Sqrt(direction.X * direction.X + direction.Y * direction.Y);
		var circleStrokeWidth = StrokeWidthScale.Measure(cellSize);
		if (distance < 1E-6F)
		{
			if (DrawCircles)
			{
				// Centers are too close.
				drawCircle(center1, r, circleStrokeWidth, StrokeColor, FillColor);
			}
			return;
		}

		if (DrawCircles)
		{
			// Draw two circles.
			drawCircle(center1, r, circleStrokeWidth, StrokeColor, FillColor);
			drawCircle(center2, r, circleStrokeWidth, StrokeColor, FillColor);
		}

		// Draw lines.
		if (DrawLines)
		{
			var connectorStrokeWidth = StrokeWidthScale.Measure(cellSize);
			using var strokePaint = new SKPaint
			{
				IsAntialias = true,
				Style = SKPaintStyle.Stroke,
				StrokeWidth = connectorStrokeWidth,
				Color = StrokeColor
			};
			foreach (var (start, end) in getConnectorPairs(center1, center2, LinesCount, LinesMaxGapScale))
			{
				backingCanvas.DrawLine(start, end, strokePaint);
			}
		}


		void drawCircle(SKPoint center, float r, float strokeWidth, SKColor strokeColor, SKColor fillColor)
		{
			using var fillPaint = new SKPaint { IsAntialias = true, Style = SKPaintStyle.Fill, Color = fillColor };
			backingCanvas.DrawCircle(center, r, fillPaint);

			using var storkePaint = new SKPaint
			{
				IsAntialias = true,
				Style = SKPaintStyle.Stroke,
				StrokeWidth = strokeWidth,
				Color = strokeColor
			};
			backingCanvas.DrawCircle(center, r, storkePaint);
		}

		IEnumerable<(SKPoint, SKPoint)> getConnectorPairs(
			SKPoint c1,
			SKPoint c2,
			int linesCount,
			Scale linesMaxGapScale
		)
		{
			var u = new SKPoint(direction.X / distance, direction.Y / distance); // Center axis from <c>c1</c> to <c>c2</c>.
			var v = new SKPoint(-u.Y, u.X); // Vertical direction.
			if (linesCount == 1)
			{
				// Calcluate baseline (i.e. the line drawn when linesCount == 1), and two points in baseline:
				//   * p1Base = c1 + u * r
				//   * p2Base = c2 - u * r
				var p1Base = new SKPoint(c1.X + u.X * r, c1.Y + u.Y * r);
				var p2Base = new SKPoint(c2.X - u.X * r, c2.Y - u.Y * r);
				yield return (p1Base, p2Base);
				yield break;
			}

			float n = linesCount;

			// n >= 2: Make gap of two farthest lines = linesMaxGapScale * diameter.
			var s = linesMaxGapScale.Measure(diameter) / (n - 1); // n == 2 => s = linesMaxGapScale * diameter
			var centerIndex = (n - 1) / 2;
			for (var i = 0; i < linesCount; i++)
			{
				// Offset to vertical direction related to baseline.
				var offset = (i - centerIndex) * s;

				// If offset exceeds radius of circle, we can skip or clamp the value.
				// Here we do a clamp with range [-r, r].
				if (Math.Abs(offset) > r)
				{
					// You can also do 'continue' here.
					offset = Math.Sign(offset) * r;
				}

				// Calculate shadowing. For each circle, when we get offset <c>v * offset</c>,
				// we can calculate length of projection to center axis <c>t = sqrt(r ^ 2 - offset ^ 2)</c>.
				var under = r * r - offset * offset;
				var t = under <= 0 ? 0 : MathF.Sqrt(under);

				// p1 = c1 + v * offset + u * (+t)
				var p1 = new SKPoint(c1.X + v.X * offset + u.X * t, c1.Y + v.Y * offset + u.Y * t);

				// p2 = c2 + v * offset + u * (-t)
				var p2 = new SKPoint(c2.X + v.X * offset - u.X * t, c2.Y + v.Y * offset - u.Y * t);
				yield return (p1, p2);
			}
		}
	}
}
