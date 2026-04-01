namespace Sudoku.Graphics.Items.CandidatePairMarks;

/// <summary>
/// Represents candidate pair link mark item.
/// </summary>
public sealed record CandidatePairLinkMarkItem : CandidatePairMarkItem
{
	/// <summary>
	/// Indicates the half arrow cap rotation degrees, in angles.
	/// </summary>
	public required float HalfArrowCapRotationDegrees { get; init; }

	/// <inheritdoc/>
	public override ItemType Type => ItemType.CandidatePair_Link;

	/// <summary>
	/// Indicates dash sequence.
	/// </summary>
	public LineDashSequence DashSequence { get; init; } = [];

	/// <summary>
	/// Indicates the scale of candidate 1 size, related to candidate size.
	/// </summary>
	public required Scale Candidate1SizeScale { get; init; }

	/// <summary>
	/// Indicates the scale of candidate 2 size, related to candidate size.
	/// </summary>
	public required Scale Candidate2SizeScale { get; init; }

	/// <inheritdoc/>
	public required override Scale StrokeWidthScale { get; init; }

	/// <summary>
	/// Indicates arrow cap length scale.
	/// </summary>
	public required Scale ArrowCapLengthScale { get; init; }

	/// <inheritdoc/>
	public required override SerializableColor StrokeColor { get; init; }


	/// <inheritdoc/>
	protected internal override void DrawTo(Canvas canvas)
	{
		var backingCanvas = canvas.BackingCanvas;
		var mapper = canvas.Templates[TemplateIndex].Mapper;
		var cellSize = mapper.CellSize;
		var ((_, candidateSize1, _), (_, candidateSize2, _)) = (CandidatePosition1, CandidatePosition2);
		var candidateSizeMeasurer1 = CandidatePosition.GetLocatorMeasurer(CandidatePosition1, cellSize);
		var candidateSizeMeasurer2 = CandidatePosition.GetLocatorMeasurer(CandidatePosition2, cellSize);
		var center1 = mapper.GetPoint(CandidatePosition1, Alignment.Center);
		var center2 = mapper.GetPoint(CandidatePosition2, Alignment.Center);
		var radius1 = Candidate1SizeScale.Measure(candidateSizeMeasurer1) / 2;
		var radius2 = Candidate2SizeScale.Measure(candidateSizeMeasurer2) / 2;
		var (p1, p2) = getPoints(center1.X, center1.Y, radius1, center2.X, center2.Y, radius2);
		using var strokePaint = new SKPaint
		{
			Style = SKPaintStyle.Stroke,
			IsAntialias = true,
			Color = StrokeColor,
			StrokeWidth = StrokeWidthScale.Measure(cellSize),
			StrokeCap = SKStrokeCap.Round,
			PathEffect = DashSequence
		};
		backingCanvas.DrawLine(p1, p2, strokePaint);

		var first = new SKPoint(p1.X + candidateSize1 / 2, p1.Y + candidateSize1 / 2);
		var second = new SKPoint(p2.X + candidateSize2 / 2, p2.Y + candidateSize2 / 2);
		var arrowLength = ArrowCapLengthScale.Measure(cellSize);

		using var arrowStrokePaint = new SKPaint
		{
			Style = SKPaintStyle.Stroke,
			IsAntialias = true,
			Color = StrokeColor,
			StrokeWidth = StrokeWidthScale.Measure(cellSize),
			StrokeCap = SKStrokeCap.Round
		};
		backingCanvas.DrawArrowCaps(first, second, arrowLength, HalfArrowCapRotationDegrees, arrowStrokePaint);


		static (SKPoint, SKPoint) getPoints(float x1, float y1, float r1, float x2, float y2, float r2)
		{
			var dx = x2 - x1;
			var dy = y2 - y1;
			var length = MathF.Sqrt(dx * dx + dy * dy);
			if (length < 1E-3F)
			{
				throw new InvalidOperationException("Two points are overlapped or too close.");
			}

			var ux = dx / length;
			var uy = dy / length;
			return (new(x1 + r1 * ux, y1 + r1 * uy), new(x2 - r2 * ux, y2 - r2 * uy));
		}
	}
}
