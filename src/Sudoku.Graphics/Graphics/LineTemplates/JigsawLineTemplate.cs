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


	/// <inheritdoc/>
	public override void DrawLines(PointMapper mapper, SKCanvas canvas, CanvasDrawingOptions options)
	{
		using var thickLinePaint = new SKPaint
		{
			Style = SKPaintStyle.Stroke,
			Color = options.ThickLineColor.Resolve(options),
			StrokeWidth = options.ThickLineWidth.Resolve(options).Measure(mapper.CellSize),
			StrokeCap = SKStrokeCap.Round,
			IsAntialias = true
		};
		using var thinLinePaint = new SKPaint
		{
			Style = SKPaintStyle.Stroke,
			Color = options.ThinLineColor.Resolve(options),
			StrokeWidth = options.ThinLineWidth.Resolve(options).Measure(mapper.CellSize),
			StrokeCap = SKStrokeCap.Round,
			IsAntialias = true
		};

		// Iterate on each cell index group.
		foreach (var cellIndices in CellIndexGroups)
		{
			var lineSegmentsDictionary = new Dictionary<Absolute, Direction>(
				from cell in cellIndices
				let absoluteIndex = mapper.ToAbsoluteIndex(cell)
				select KeyValuePair.Create(absoluteIndex, Direction.Up | Direction.Down | Direction.Left | Direction.Right)
			);
			var set = lineSegmentsDictionary.Keys.ToHashSet();

			// Iterate on each cell (absolute), to find for adjacent cells.
			foreach (var cell in lineSegmentsDictionary.Keys)
			{
				foreach (var direction in Direction.AllDirections)
				{
					if (set.Contains(mapper.GetAdjacentAbsoluteCellWith(cell, direction, IsCyclicRuleChecked)))
					{
						// This direction contains that cell - we should remove this direction.
						lineSegmentsDictionary[cell] &= ~direction;
					}
				}
			}

			// Then draw lines onto it.
			foreach (var (cell, directions) in lineSegmentsDictionary)
			{
				var topLeft = mapper.GetPoint(cell, CellCornerType.TopLeft);
				var topRight = mapper.GetPoint(cell, CellCornerType.TopRight);
				var bottomLeft = mapper.GetPoint(cell, CellCornerType.BottomLeft);
				var bottomRight = mapper.GetPoint(cell, CellCornerType.BottomRight);
				canvas.DrawLine(topLeft, topRight, (directions & Direction.Up) != Direction.None ? thickLinePaint : thinLinePaint);
				canvas.DrawLine(bottomLeft, bottomRight, (directions & Direction.Down) != Direction.None ? thickLinePaint : thinLinePaint);
				canvas.DrawLine(topLeft, bottomLeft, (directions & Direction.Left) != Direction.None ? thickLinePaint : thinLinePaint);
				canvas.DrawLine(topRight, bottomRight, (directions & Direction.Right) != Direction.None ? thickLinePaint : thinLinePaint);
			}
		}
	}
}
