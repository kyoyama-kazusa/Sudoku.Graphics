namespace Sudoku.Graphics.Items.CellMarks;

/// <summary>
/// Represents a cell seven-segment display mark item.
/// </summary>
public sealed record CellSevenSegmentDisplayMarkItem : CellMarkItem, IItem_ValueProperty<int>
{
	/// <summary>
	/// Indicates whether phantom segments (segments not shown in specified value) are also shown, but not filled.
	/// </summary>
	public required bool ShowPhantomSegments { get; init; }

	/// <inheritdoc/>
	public required int Value { get; init; }

	/// <summary>
	/// Indicates the scale of width of segment rectangles, related to cell size.
	/// </summary>
	public required Scale SegmentRectWidthScale { get; init; }

	/// <summary>
	/// Indicates the scale of height of segment rectangles, related to cell size.
	/// </summary>
	public required Scale SegmentRectHeightScale { get; init; }

	/// <summary>
	/// Indicates scale of stroke width of phantom segments, related to cell size.
	/// </summary>
	public required Scale PhantomStrokeWidthScale { get; init; }

	/// <inheritdoc/>
	public override ItemType Type => ItemType.Cell_SevenSegmentDisplay;


	/// <inheritdoc/>
	protected internal override void DrawTo(Canvas canvas)
	{
		var backingCanvas = canvas.BackingCanvas;
		var mapper = canvas.Templates[TemplateIndex].Mapper;
		var cellSize = mapper.CellSize;
		var center = mapper.GetPoint(Cell, Alignment.Center);
		var size = SizeScale.Measure(cellSize);
		var segmentRectWidth = SegmentRectWidthScale.Measure(cellSize);
		var segmentRectHeight = SegmentRectHeightScale.Measure(cellSize);
		var twoBoxesSize = size - segmentRectHeight;
		var boxSize = twoBoxesSize / 2;
		var gapSize = (boxSize - segmentRectWidth) / 2;
		var rectSize = segmentRectWidth - segmentRectHeight;
		var anchors = (SKPoint[])[
			new(center.X - boxSize / 2, center.Y - boxSize),
			new(center.X + boxSize / 2, center.Y - boxSize),
			new(center.X - boxSize / 2, center.Y),
			new(center.X + boxSize / 2, center.Y),
			new(center.X - boxSize / 2, center.Y + boxSize),
			new(center.X + boxSize / 2, center.Y + boxSize)
		];
		var points = (SKPoint[][])[
			[
				new(anchors[0].X + gapSize, anchors[0].Y),
				new(anchors[0].X + gapSize + segmentRectHeight / 2, anchors[0].Y - segmentRectHeight / 2),
				new(anchors[0].X + gapSize + segmentRectHeight / 2 + rectSize, anchors[0].Y - segmentRectHeight / 2),
				new(anchors[0].X + gapSize + segmentRectHeight + rectSize, anchors[0].Y),
				new(anchors[0].X + gapSize + segmentRectHeight / 2 + rectSize, anchors[0].Y + segmentRectHeight / 2),
				new(anchors[0].X + gapSize + segmentRectHeight / 2, anchors[0].Y + segmentRectHeight / 2)
			],
			[
				new(anchors[0].X, anchors[0].Y + gapSize),
				new(anchors[0].X + segmentRectHeight / 2, anchors[0].Y + gapSize + segmentRectHeight / 2),
				new(anchors[0].X + segmentRectHeight / 2, anchors[0].Y + gapSize + segmentRectHeight / 2 + rectSize),
				new(anchors[0].X, anchors[0].Y + gapSize + segmentRectHeight + rectSize),
				new(anchors[0].X - segmentRectHeight / 2, anchors[0].Y + gapSize + segmentRectHeight / 2 + rectSize),
				new(anchors[0].X - segmentRectHeight / 2, anchors[0].Y + gapSize + segmentRectHeight / 2)
			],
			[
				new(anchors[1].X, anchors[1].Y + gapSize),
				new(anchors[1].X + segmentRectHeight / 2, anchors[1].Y + gapSize + segmentRectHeight / 2),
				new(anchors[1].X + segmentRectHeight / 2, anchors[1].Y + gapSize + segmentRectHeight / 2 + rectSize),
				new(anchors[1].X, anchors[1].Y + gapSize + segmentRectHeight + rectSize),
				new(anchors[1].X - segmentRectHeight / 2, anchors[1].Y + gapSize + segmentRectHeight / 2 + rectSize),
				new(anchors[1].X - segmentRectHeight / 2, anchors[1].Y + gapSize + segmentRectHeight / 2)
			],
			[
				new(anchors[2].X + gapSize, anchors[2].Y),
				new(anchors[2].X + gapSize + segmentRectHeight / 2, anchors[2].Y - segmentRectHeight / 2),
				new(anchors[2].X + gapSize + segmentRectHeight / 2 + rectSize, anchors[2].Y - segmentRectHeight / 2),
				new(anchors[2].X + gapSize + segmentRectHeight + rectSize, anchors[2].Y),
				new(anchors[2].X + gapSize + segmentRectHeight / 2 + rectSize, anchors[2].Y + segmentRectHeight / 2),
				new(anchors[2].X + gapSize + segmentRectHeight / 2, anchors[2].Y + segmentRectHeight / 2)
			],
			[
				new(anchors[2].X, anchors[2].Y + gapSize),
				new(anchors[2].X + segmentRectHeight / 2, anchors[2].Y + gapSize + segmentRectHeight / 2),
				new(anchors[2].X + segmentRectHeight / 2, anchors[2].Y + gapSize + segmentRectHeight / 2 + rectSize),
				new(anchors[2].X, anchors[2].Y + gapSize + segmentRectHeight + rectSize),
				new(anchors[2].X - segmentRectHeight / 2, anchors[2].Y + gapSize + segmentRectHeight / 2 + rectSize),
				new(anchors[2].X - segmentRectHeight / 2, anchors[2].Y + gapSize + segmentRectHeight / 2)
			],
			[
				new(anchors[3].X, anchors[3].Y + gapSize),
				new(anchors[3].X + segmentRectHeight / 2, anchors[3].Y + gapSize + segmentRectHeight / 2),
				new(anchors[3].X + segmentRectHeight / 2, anchors[3].Y + gapSize + segmentRectHeight / 2 + rectSize),
				new(anchors[3].X, anchors[3].Y + gapSize + segmentRectHeight + rectSize),
				new(anchors[3].X - segmentRectHeight / 2, anchors[3].Y + gapSize + segmentRectHeight / 2 + rectSize),
				new(anchors[3].X - segmentRectHeight / 2, anchors[3].Y + gapSize + segmentRectHeight / 2)
			],
			[
				new(anchors[4].X + gapSize, anchors[4].Y),
				new(anchors[4].X + gapSize + segmentRectHeight / 2, anchors[4].Y - segmentRectHeight / 2),
				new(anchors[4].X + gapSize + segmentRectHeight / 2 + rectSize, anchors[4].Y - segmentRectHeight / 2),
				new(anchors[4].X + gapSize + segmentRectHeight + rectSize, anchors[4].Y),
				new(anchors[4].X + gapSize + segmentRectHeight / 2 + rectSize, anchors[4].Y + segmentRectHeight / 2),
				new(anchors[4].X + gapSize + segmentRectHeight / 2, anchors[4].Y + segmentRectHeight / 2)
			]
		];

		using var strokePaint = new SKPaint
		{
			Style = SKPaintStyle.Stroke,
			IsAntialias = true,
			Color = StrokeColor,
			StrokeWidth = StrokeWidthScale.Measure(cellSize)
		};
		using var phantomStrokePaint = new SKPaint
		{
			Style = SKPaintStyle.Stroke,
			IsAntialias = true,
			Color = StrokeColor,
			StrokeWidth = PhantomStrokeWidthScale.Measure(cellSize)
		};
		using var fillPaint = new SKPaint { Style = SKPaintStyle.Fill, IsAntialias = true, Color = FillColor };

		var segmentsLightup = LedStatesTable.Value[/*digit*/Value][/*style*/0];
		for (var i = 0; i < segmentsLightup.Length; i++)
		{
			using var path = new SKPath();
			path.MoveTo(points[i][0]);
			for (var j = 1; j < 6; j++)
			{
				path.LineTo(points[i][j]);
			}
			path.Close();

			if (segmentsLightup[i])
			{
				backingCanvas.DrawPath(path, fillPaint);
				backingCanvas.DrawPath(path, strokePaint);
			}
			else if (ShowPhantomSegments)
			{
				backingCanvas.DrawPath(path, phantomStrokePaint);
			}
		}
	}
}
