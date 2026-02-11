namespace Sudoku.Graphics;

/// <summary>
/// Represents irregular block line template.
/// </summary>
/// <param name="cellIndexGroups">The cell index groups.</param>
public sealed class IrregularBlockLineTemplate(params int[][] cellIndexGroups) : BlockLineTemplate
{
	/// <summary>
	/// Indicates the cell index groups.
	/// </summary>
	private readonly int[][] _cellIndexGroups = cellIndexGroups;


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
			Color = options.ThickLineColor,
			StrokeWidth = options.ThickLineWidth.Measure(mapper.CellWidthAndHeight),
			StrokeCap = SKStrokeCap.Round,
			IsAntialias = true
		};
		using var thinLinePaint = new SKPaint
		{
			Style = SKPaintStyle.Stroke,
			Color = options.ThinLineColor,
			StrokeWidth = options.ThinLineWidth.Measure(mapper.CellWidthAndHeight),
			StrokeCap = SKStrokeCap.Round,
			IsAntialias = true
		};

		// Iterate on each cell index group.
		foreach (var cellIndices in _cellIndexGroups)
		{
			var set = cellIndices.ToHashSet();
			var lineSegmentsDictionary = new Dictionary<int, Direction>(
				from cell in cellIndices
				let absoluteIndex = mapper.ToAbsoluteIndex(cell)
				select KeyValuePair.Create(absoluteIndex, Direction.Up | Direction.Down | Direction.Left | Direction.Right)
			);

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
				var topLeft = mapper.GetTopLeftPoint(cell);
				var topRight = mapper.GetTopRightPoint(cell);
				var bottomLeft = mapper.GetBottomLeftPoint(cell);
				var bottomRight = mapper.GetBottomRightPoint(cell);
				canvas.DrawLine(topLeft, topRight, (directions & Direction.Up) != Direction.None ? thickLinePaint : thinLinePaint);
				canvas.DrawLine(bottomLeft, bottomRight, (directions & Direction.Down) != Direction.None ? thickLinePaint : thinLinePaint);
				canvas.DrawLine(topLeft, bottomLeft, (directions & Direction.Left) != Direction.None ? thickLinePaint : thinLinePaint);
				canvas.DrawLine(topRight, bottomRight, (directions & Direction.Right) != Direction.None ? thickLinePaint : thinLinePaint);
			}
		}
	}
}
