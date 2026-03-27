namespace Sudoku.Graphics;

public partial class SKCanvasDrawings
{
	extension(SKCanvas @this)
	{
		/// <summary>
		/// Draws a bar between two adjacent cells.
		/// </summary>
		/// <param name="cell1">The cell 1.</param>
		/// <param name="cell2">The cell 2.</param>
		/// <param name="shortSideScale">The scale of short side, related to cell size.</param>
		/// <param name="longSideScale">The scale of long side, related to cell size.</param>
		/// <param name="fillColor">The fill color.</param>
		/// <param name="cornerRadiusScale">The scale of corner radius of bar, related to short side length.</param>
		/// <param name="mapper">The point mapper instance.</param>
		/// <exception cref="ArgumentException">Throws when cells are not adjacent with each other.</exception>
		public void DrawBar(
			Absolute cell1,
			Absolute cell2,
			Scale shortSideScale,
			Scale longSideScale,
			SerializableColor fillColor,
			Scale cornerRadiusScale,
			PointMapper mapper
		)
		{
			if (!Absolute.IsAdjacent(cell1, cell2, mapper, out var houseType))
			{
				throw new ArgumentException($"Cells '{cell1}' and '{cell2}' must be adjacent with each other.");
			}

			var cellSize = mapper.CellSize;
			var shortSide = shortSideScale.Measure(cellSize);
			var longSide = longSideScale.Measure(cellSize);

			// Keeps 'cell1' is less than 'cell2'.
			if (cell1 > cell2)
			{
				// Here we may want to do a swap, but this is not necessary because 'cell2' is not necessary to be used.
				(cell1, _) = (cell2, cell1);
			}

			var cell1Center = mapper.GetPoint(cell1, Alignment.Center);
			var cellPairCenter = cell1Center + (houseType == HouseType.Row ? new SKPoint(cellSize / 2, 0) : new SKPoint(0, cellSize / 2));
			var offsetPoint = houseType == HouseType.Row ? new SKPoint(shortSide / 2, longSide / 2) : new SKPoint(longSide / 2, shortSide / 2);
			var topLeft = cellPairCenter - offsetPoint;
			var bottomRight = cellPairCenter + offsetPoint;
			var rect = SKRect.Create(topLeft, bottomRight);
			using var fillPaint = new SKPaint { IsAntialias = true, Style = SKPaintStyle.Fill, Color = fillColor };
			@this.DrawRoundRect(new(rect, cornerRadiusScale.Measure(shortSide)), fillPaint);
		}
	}
}
