namespace Sudoku.Graphics;

public partial class SKCanvasDrawings
{
	extension(SKCanvas @this)
	{
		/// <summary>
		/// Draws a cell group trail.
		/// </summary>
		/// <param name="trailCells">The trail cells.</param>
		/// <param name="sizeScale">The scale of size, related to cell size.</param>
		/// <param name="fillColor">The fill color.</param>
		/// <param name="mapper">The point mapper instance.</param>
		public void DrawCellGroupTrail(
			ReadOnlySpan<Absolute> trailCells,
			Scale sizeScale,
			SerializableColor fillColor,
			PointMapper mapper
		)
		{
			var cellSize = mapper.CellSize;
			var squareSize = sizeScale.Measure(cellSize);
			var halfSquareSize = squareSize / 2;

			var rectangles = new List<SKRect>();
			var traversedCells = new HashSet<Absolute>();

			// Find for cell drawing rectagles.
			foreach (var cell in trailCells)
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
			for (var i = 0; i < trailCells.Length - 1; i++)
			{
				var cell1 = trailCells[i];
				var cell2 = trailCells[i + 1];
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
			using var fillPaint = new SKPaint { IsAntialias = true, Style = SKPaintStyle.Fill, Color = fillColor };
			foreach (ref readonly var rect in rectangles.AsSpan())
			{
				@this.DrawRect(rect, fillPaint);
			}
		}
	}
}
