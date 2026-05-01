namespace Sudoku.Graphics.Items.CellGroupMarks;

/// <summary>
/// Represents a cell group trail mark item.
/// </summary>
public sealed record CellGroupTrailMarkItem : CellGroupMarkItem
{
	/// <inheritdoc/>
	public override ItemType Type => ItemType.CellGroup_CellTrail;

	/// <summary>
	/// Indicates scale of size, related to cell size.
	/// </summary>
	public required Scale SizeScale { get; init; }

	/// <inheritdoc/>
	public required override SerializableColor FillColor { get; init; }


	/// <inheritdoc/>
	protected internal override void DrawTo(Canvas canvas)
	{
		var mapper = canvas.Mapper;
		var cellSize = mapper.CellSize;
		var squareSize = SizeScale.Measure(cellSize);
		var halfSquareSize = squareSize / 2;

		var rectangles = new List<SKRect>();
		var traversedCells = new HashSet<Absolute>();

		// Find for cell drawing rectagles.
		foreach (var cell in Cells)
		{
			if (!traversedCells.Add(cell))
			{
				// The cell has been already traversed.
				continue;
			}

			var center = mapper.GetPoint(cell, Alignment.Center);
			var p1 = new SKPoint(center.X - halfSquareSize, center.Y - halfSquareSize);
			var p2 = new SKPoint(center.X + halfSquareSize, center.Y + halfSquareSize);
			rectangles.Add(SKRect.Create(p1, p2));
		}

		// Then we should check for adjacent cell pairs, in order to drawing gap rectangle between each pair.
		for (var i = 0; i < Cells.Length - 1; i++)
		{
			var cell1 = Cells[i];
			var cell2 = Cells[i + 1];
			if (!Absolute.IsAdjacent(cell1, cell2, mapper, out var houseType))
			{
				// They are not adjacent.
				continue;
			}

			// Keeps 'cell1' is less than 'cell2'.
			if (cell1 > cell2)
			{
				(cell1, cell2) = (cell2, cell1);
			}

			var p1 = mapper.GetPoint(cell1, Alignment.Center);
			var p2 = mapper.GetPoint(cell2, Alignment.Center);
			if (houseType == HouseType.Row)
			{
				p1.X += halfSquareSize;
				p1.Y -= halfSquareSize;
				p2.X -= halfSquareSize;
				p2.Y += halfSquareSize;
			}
			else
			{
				p1.X -= halfSquareSize;
				p1.Y += halfSquareSize;
				p2.X += halfSquareSize;
				p2.Y -= halfSquareSize;
			}
			rectangles.Add(SKRect.Create(p1, p2));
		}

		// Draw such rectangles.
		using var fillPaint = new SKPaint { IsAntialias = true, Style = SKPaintStyle.Fill, Color = FillColor };
		foreach (ref readonly var rect in rectangles.AsSpan())
		{
			canvas.BackingCanvas.DrawRect(rect, fillPaint);
		}
	}
}
