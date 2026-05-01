namespace Sudoku.Graphics.Items.CellMarks;

/// <summary>
/// Provides with a cell phantom dice mark item.
/// </summary>
public sealed record CellPhantomDiceMarkItem : CellMarkItem
{
	/// <summary>
	/// Indicates subgrid size.
	/// </summary>
	public required Relative SubgridSize { get; init; }

	/// <summary>
	/// Indicates scale of phantom stroke width, related to cell size.
	/// </summary>
	public required Scale PhantomStrokeWidthScale { get; init; }

	/// <inheritdoc/>
	public override ItemType Type => ItemType.Cell_PhantomDice;

	/// <summary>
	/// Indicates the states.
	/// </summary>
	public required BitArray States { get; init; }


	/// <inheritdoc/>
	protected internal override void DrawTo(Canvas canvas)
	{
		var backingCanvas = canvas.BackingCanvas;
		var mapper = canvas.Mapper;
		var cellSize = mapper.CellSize;
		for (var i = 0; i < SubgridSize * SubgridSize; i++)
		{
			var center = mapper.GetPoint(new CandidatePosition(Cell, SubgridSize, i), Alignment.Center);
			var radius = SizeScale.Measure(cellSize) / 2;
			if (States[i])
			{
				// Fill paint.
				if (FillColor.Alpha != 0)
				{
					using var fillPaint = new SKPaint { Style = SKPaintStyle.Fill, IsAntialias = true, Color = FillColor };
					backingCanvas.DrawCircle(center, radius, fillPaint);
				}

				// Stroke paint.
				var strokeWidth = StrokeWidthScale.Measure(cellSize);
				if (strokeWidth != 0 && StrokeColor.Alpha != 0)
				{
					using var strokePaint = new SKPaint
					{
						Style = SKPaintStyle.Stroke,
						IsAntialias = true,
						Color = StrokeColor,
						StrokeWidth = strokeWidth
					};
					backingCanvas.DrawCircle(center, radius, strokePaint);
				}
				continue;
			}

			var phantomStrokeWidth = PhantomStrokeWidthScale.Measure(cellSize);
			if (phantomStrokeWidth != 0 && StrokeColor.Alpha != 0)
			{
				using var phantomStrokePaint = new SKPaint
				{
					Style = SKPaintStyle.Stroke,
					IsAntialias = true,
					Color = StrokeColor,
					StrokeWidth = phantomStrokeWidth
				};
				backingCanvas.DrawCircle(center, radius, phantomStrokePaint);
			}
		}
	}
}
