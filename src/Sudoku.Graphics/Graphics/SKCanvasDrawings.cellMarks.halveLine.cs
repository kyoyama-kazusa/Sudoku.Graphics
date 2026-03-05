namespace Sudoku.Graphics;

public partial class SKCanvasDrawings
{
	extension(SKCanvas @this)
	{
		/// <summary>
		/// Draws halve line to specified cell.
		/// </summary>
		/// <param name="cell">The cell.</param>
		/// <param name="orientation">The orientation.</param>
		/// <param name="sizeScale">The scale of padding, related to cell size.</param>
		/// <param name="strokeColor">The stroke color.</param>
		/// <param name="strokeWidthScale">The stroke width scale, related to cell size.</param>
		/// <param name="mapper">The mapper instance.</param>
		/// <exception cref="ArgumentOutOfRangeException">Throws when <paramref name="orientation"/> is not defined.</exception>
		public void DrawHalveLineToCell(
			Absolute cell,
			Orientation4 orientation,
			Scale sizeScale,
			SerializableColor strokeColor,
			Scale strokeWidthScale,
			PointMapper mapper
		)
		{
			var cellSize = mapper.CellSize;
			var lineContainingBoxSize = sizeScale.Measure(cellSize);
			var halfPadding = (cellSize - lineContainingBoxSize) / 2;
			var topLeft = mapper.GetPoint(cell, Alignment.TopLeft) + new SKPoint(+halfPadding, +halfPadding);
			var topRight = mapper.GetPoint(cell, Alignment.TopRight) + new SKPoint(-halfPadding, +halfPadding);
			var bottomLeft = mapper.GetPoint(cell, Alignment.BottomLeft) + new SKPoint(+halfPadding, -halfPadding);
			var bottomRight = mapper.GetPoint(cell, Alignment.BottomRight) + new SKPoint(-halfPadding, -halfPadding);
			var top = topLeft + new SKPoint(lineContainingBoxSize / 2, 0);
			var bottom = bottomLeft + new SKPoint(lineContainingBoxSize / 2, 0);
			var left = topLeft + new SKPoint(0, lineContainingBoxSize / 2);
			var right = topRight + new SKPoint(0, lineContainingBoxSize / 2);
			var (start, end) = orientation switch
			{
				Orientation4.Horizontal => (left, right),
				Orientation4.Vertical => (top, bottom),
				Orientation4.Slash => (topRight, bottomLeft),
				Orientation4.Backslash => (topLeft, bottomRight),
				_ => throw new ArgumentOutOfRangeException(nameof(orientation))
			};

			var strokeWidth = strokeWidthScale.Measure(cellSize);
			if (strokeWidth != 0 && strokeColor.Alpha != 0)
			{
				using var strokePaint = new SKPaint
				{
					Style = SKPaintStyle.Stroke,
					Color = strokeColor,
					StrokeWidth = strokeWidth,
					IsAntialias = true
				};
				@this.DrawLine(start, end, strokePaint);
			}
		}
	}
}
