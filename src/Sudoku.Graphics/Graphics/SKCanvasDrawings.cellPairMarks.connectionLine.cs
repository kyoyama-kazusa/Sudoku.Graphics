namespace Sudoku.Graphics;

public partial class SKCanvasDrawings
{
	extension(SKCanvas @this)
	{
		/// <summary>
		/// Draws a connection line between two adjacent cells, including diagonally-adjacent cases.
		/// </summary>
		/// <param name="cell1">The cell 1.</param>
		/// <param name="cell2">The cell 2.</param>
		/// <param name="strokeWidthScale">The scale of stroke width, related to cell size.</param>
		/// <param name="strokeColor">The color of stroke.</param>
		/// <param name="strokeSizeScale">The scale of stroke size, related to cell size.</param>
		/// <param name="mapper">The point mapper instance.</param>
		public void DrawConnectionLine(
			Absolute cell1,
			Absolute cell2,
			Scale strokeWidthScale,
			SerializableColor strokeColor,
			Scale strokeSizeScale,
			PointMapper mapper
		)
		{
			if (Absolute.GetAdjacentRelation(cell1, cell2, mapper) is not (var adjacentRelation and not Direction8.None))
			{
				return;
			}

			var cellSize = mapper.CellSize;
			var cellPadding = strokeSizeScale.Measure(cellSize);
			var p1 = mapper.GetPoint(cell1, Alignment.Center);
			var p2 = mapper.GetPoint(cell2, Alignment.Center);
			_ = adjacentRelation switch
			{
				Direction8.Up => (p1 += new SKPoint(0, cellPadding), p2 -= new SKPoint(0, cellPadding)),
				Direction8.Down => (p1 -= new SKPoint(0, cellPadding), p2 += new SKPoint(0, cellPadding)),
				Direction8.Left => (p1 += new SKPoint(cellPadding, 0), p2 -= new SKPoint(cellPadding, 0)),
				Direction8.Right => (p1 -= new SKPoint(cellPadding, 0), p2 += new SKPoint(cellPadding, 0)),
				Direction8.LeftUp => (p1 += new SKPoint(+cellPadding, +cellPadding), p2 -= new SKPoint(+cellPadding, +cellPadding)),
				Direction8.RightUp => (p1 += new SKPoint(-cellPadding, +cellPadding), p2 -= new SKPoint(-cellPadding, +cellPadding)),
				Direction8.LeftDown => (p1 += new SKPoint(+cellPadding, -cellPadding), p2 -= new SKPoint(+cellPadding, -cellPadding)),
				Direction8.RightDown => (p1 += new SKPoint(-cellPadding, -cellPadding), p2 -= new SKPoint(-cellPadding, -cellPadding)),
				_ => throw new UnreachableException()
			};

			using var strokePaint = new SKPaint
			{
				Style = SKPaintStyle.Stroke,
				IsAntialias = true,
				StrokeWidth = strokeWidthScale.Measure(cellSize),
				Color = strokeColor,
				StrokeCap = SKStrokeCap.Round,
				StrokeJoin = SKStrokeJoin.Round
			};
			@this.DrawLine(p1, p2, strokePaint);
		}
	}
}
