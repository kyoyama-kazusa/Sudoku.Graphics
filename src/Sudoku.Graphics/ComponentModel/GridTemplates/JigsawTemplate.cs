namespace Sudoku.ComponentModel.GridTemplates;

/// <summary>
/// Represents irregular (jigsaw) grid template, containing irregular-shaped blocks.
/// </summary>
public sealed class JigsawTemplate : Template
{
	/// <summary>
	/// Indicates the relative cell index groups. By default it's an empty array.
	/// </summary>
	public Relative[][] CellIndexGroups { get; init; } = [];

	/// <summary>
	/// Indicates whether cyclic rule will be checked.
	/// </summary>
	public bool IsCyclicRuleChecked { get; init; } = true;

	/// <summary>
	/// Indicates whether this template will also fill cell groups with colors
	/// by using <see cref="CanvasDrawingOptions.JSudokuColorSet"/>.
	/// </summary>
	/// <seealso cref="CanvasDrawingOptions.JSudokuColorSet"/>
	[MemberNotNullWhen(true, nameof(GroupColorSet))]
	public bool AlsoFillGroups { get; init; } = false;

	/// <summary>
	/// Indicates the color set that will be used for colorization of cells.
	/// </summary>
	public required SerializableColorSet? GroupColorSet { get; init; }


	/// <inheritdoc/>
	protected override void DrawBorderRectangle(SKCanvas canvas)
	{
	}

	/// <inheritdoc/>
	protected override void DrawGridLines(SKCanvas canvas)
	{
		using var thickLinePaint = CreateThickLinesPaint();
		using var thinLinePaint = CreateThinLinesPaint();

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

			using var fillPaint = AlsoFillGroups && GroupColorSet is { } resolvedColorSet
				? new SKPaint
				{
					Style = SKPaintStyle.Fill,
					Color = resolvedColorSet[groupIndex % resolvedColorSet.Count]
				}
				: null;

			// Then draw lines onto it, and also fill with cells if worth.
			foreach (var (cell, directions) in lineSegmentsDictionary)
			{
				var topLeft = Mapper.GetPoint(cell, Alignment.TopLeft);
				var topRight = Mapper.GetPoint(cell, Alignment.TopRight);
				var bottomLeft = Mapper.GetPoint(cell, Alignment.BottomLeft);
				var bottomRight = Mapper.GetPoint(cell, Alignment.BottomRight);

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
