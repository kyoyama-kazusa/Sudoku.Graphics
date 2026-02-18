namespace Sudoku.Graphics.LineTemplates;

/// <summary>
/// Represents irregular (jigsaw) line template.
/// </summary>
/// <param name="cellIndexGroups"><inheritdoc cref="CellIndexGroups" path="/summary"/></param>
/// <param name="mapper"><inheritdoc cref="LineTemplate(PointMapper)" path="/param[@name='mapper']"/></param>
[method: JsonConstructor]
public sealed class JigsawLineTemplate(Relative[][] cellIndexGroups, PointMapper mapper) : IndividualBlockLineTemplate(mapper)
{
	/// <summary>
	/// Indicates the relative cell index groups.
	/// </summary>
	public Relative[][] CellIndexGroups { get; } = cellIndexGroups;

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
	protected override void GuardStatements(SKCanvas canvas, CanvasDrawingOptions options)
	{
	}

	/// <inheritdoc/>
	protected override void DrawBorderRectangle(SKCanvas canvas, CanvasDrawingOptions options)
	{
	}

	/// <inheritdoc/>
	protected override void DrawGridLines(SKCanvas canvas, CanvasDrawingOptions options)
	{
		using var thickLinePaint = CreateThickLinesPaint(options);
		using var thinLinePaint = CreateThinLinesPaint(options);

		// Iterate on each cell index group.
		var groupIndex = 0;
		foreach (var cellIndices in CellIndexGroups)
		{
			var lineSegmentsDictionary = LineSegmentFactory.GetLightupDirections(
				cellIndices,
				IsCyclicRuleChecked,
				Mapper,
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
				var topLeft = Mapper.GetPoint(cell, CellAlignment.TopLeft);
				var topRight = Mapper.GetPoint(cell, CellAlignment.TopRight);
				var bottomLeft = Mapper.GetPoint(cell, CellAlignment.BottomLeft);
				var bottomRight = Mapper.GetPoint(cell, CellAlignment.BottomRight);

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
