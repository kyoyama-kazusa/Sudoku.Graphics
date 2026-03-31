namespace Sudoku.Graphics.Items.CellPairMarks;

/// <summary>
/// Represents a cell pair bar mark item.
/// </summary>
public sealed record CellPairBarMarkItem : CellPairMarkItem
{
	/// <inheritdoc/>
	public override ItemType Type => ItemType.CellPair_Bar;

	/// <summary>
	/// Indicates the scale of short side, related to cell size.
	/// </summary>
	public required Scale ShortSideScale { get; init; }

	/// <summary>
	/// Indicates the scale of long side, related to cell size.
	/// </summary>
	public required Scale LongSideScale { get; init; }

	/// <inheritdoc/>
	public required override Scale StrokeWidthScale { get; init; }

	/// <inheritdoc/>
	public required override Scale CornerRadiusScale { get; init; }

	/// <inheritdoc/>
	public required override SerializableColor StrokeColor { get; init; }

	/// <inheritdoc/>
	public required override SerializableColor FillColor { get; init; }


	/// <inheritdoc/>
	protected internal override void DrawTo(Canvas canvas)
	{
		// Keeps 'cell1' is less than 'cell2'.
		var (cell1, cell2) = (Cell1, Cell2);
		if (cell1 > cell2)
		{
			// Here we may want to do a swap, but this is not necessary because 'cell2' is not necessary to be used.
			(cell1, _) = (cell2, cell1);
		}

		var mapper = canvas.Templates[TemplateIndex].Mapper;
		if (!Absolute.IsAdjacent(cell1, cell2, mapper, out var houseType))
		{
			throw new ArgumentException($"Cells '{cell1}' and '{cell2}' must be adjacent with each other.");
		}

		var cellSize = mapper.CellSize;
		var shortSide = ShortSideScale.Measure(cellSize);
		var longSide = LongSideScale.Measure(cellSize);
		var cell1Center = mapper.GetPoint(cell1, Alignment.Center);
		var cellPairCenter = cell1Center + (houseType == HouseType.Row ? new SKPoint(cellSize / 2, 0) : new SKPoint(0, cellSize / 2));
		var offsetPoint = houseType == HouseType.Row ? new SKPoint(shortSide / 2, longSide / 2) : new SKPoint(longSide / 2, shortSide / 2);
		var topLeft = cellPairCenter - offsetPoint;
		var bottomRight = cellPairCenter + offsetPoint;
		var rect = SKRect.Create(topLeft, bottomRight);
		using var fillPaint = new SKPaint { IsAntialias = true, Style = SKPaintStyle.Fill, Color = FillColor };
		var roundRect = new SKRoundRect(rect, CornerRadiusScale.Measure(shortSide));
		canvas.BackingCanvas.DrawRoundRect(roundRect, fillPaint);

		using var strokePaint = new SKPaint
		{
			IsAntialias = true,
			Style = SKPaintStyle.Stroke,
			Color = StrokeColor,
			StrokeWidth = StrokeWidthScale.Measure(cellSize),
			StrokeJoin = SKStrokeJoin.Round,
			StrokeCap = SKStrokeCap.Round
		};
		canvas.BackingCanvas.DrawRoundRect(roundRect, strokePaint);
	}
}
