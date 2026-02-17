namespace Sudoku.Graphics.LineTemplates;

/// <summary>
/// Represents irregular (jigsaw) line template.
/// </summary>
public sealed class JigsawLineTemplate : LineTemplate
{
	/// <summary>
	/// Indicates the relative cell index groups.
	/// </summary>
	public required Relative[][] CellIndexGroups { get; init; }

	/// <summary>
	/// Indicates whether cyclic rule will be checked.
	/// </summary>
	public bool IsCyclicRuleChecked { get; init; } = true;

	/// <summary>
	/// Indicates whether this template will also fill cell groups with colors
	/// by using <see cref="CanvasDrawingOptions.JSudokuColorSet"/>.
	/// </summary>
	/// <seealso cref="CanvasDrawingOptions.JSudokuColorSet"/>
	public bool AlsoFillGroups { get; init; } = false;


	/// <inheritdoc/>
	public override void DrawLines(PointMapper mapper, SKCanvas canvas, CanvasDrawingOptions options)
	{
		using var thickLinePaint = new SKPaint
		{
			Style = SKPaintStyle.Stroke,
			Color = options.ThickLineColor.Resolve(options),
			StrokeWidth = options.ThickLineWidth.Resolve(options).Measure(mapper.CellSize),
			StrokeCap = SKStrokeCap.Round,
			IsAntialias = false,
			PathEffect = options.ThickLineDashSequence.Resolve(options) switch { { IsEmpty: false } sequence => sequence, _ => null }
		};
		using var thinLinePaint = new SKPaint
		{
			Style = SKPaintStyle.Stroke,
			Color = options.ThinLineColor.Resolve(options),
			StrokeWidth = options.ThinLineWidth.Resolve(options).Measure(mapper.CellSize),
			StrokeCap = SKStrokeCap.Round,
			IsAntialias = false,
			PathEffect = options.ThinLineDashSequence.Resolve(options) switch { { IsEmpty: false } sequence => sequence, _ => null }
		};

		// Iterate on each cell index group.
		var groupIndex = 0;
		foreach (var cellIndices in CellIndexGroups)
		{
			var lineSegmentsDictionary = LineSegmentFactory.GetLightupDirections(
				cellIndices,
				IsCyclicRuleChecked,
				mapper,
				out var absoluteCellIndices
			);

			using var fillPaint = AlsoFillGroups && options.JSudokuColorSet.Resolve(options) is var resolvedColorSet
				? new SKPaint
				{
					Style = SKPaintStyle.Fill,
					Color = resolvedColorSet[groupIndex % resolvedColorSet.Count]
				}
				: null;

			// Then draw lines onto it, and also fill with cells if worth.
			foreach (var (cell, directions) in lineSegmentsDictionary)
			{
				var topLeft = mapper.GetPoint(cell, CellCornerType.TopLeft);
				var topRight = mapper.GetPoint(cell, CellCornerType.TopRight);
				var bottomLeft = mapper.GetPoint(cell, CellCornerType.BottomLeft);
				var bottomRight = mapper.GetPoint(cell, CellCornerType.BottomRight);

				if (AlsoFillGroups)
				{
					var rect = SKRect.Create(topLeft, bottomRight);
					canvas.DrawRect(rect, fillPaint);
				}

				canvas.DrawLine(topLeft, topRight, (directions & Direction.Up) != Direction.None ? thickLinePaint : thinLinePaint);
				canvas.DrawLine(bottomLeft, bottomRight, (directions & Direction.Down) != Direction.None ? thickLinePaint : thinLinePaint);
				canvas.DrawLine(topLeft, bottomLeft, (directions & Direction.Left) != Direction.None ? thickLinePaint : thinLinePaint);
				canvas.DrawLine(topRight, bottomRight, (directions & Direction.Right) != Direction.None ? thickLinePaint : thinLinePaint);
			}

			// Increment group index.
			groupIndex++;
		}
	}
}
