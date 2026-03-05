using System;
using System.Collections.Generic;
using System.Text;

namespace Sudoku.Graphics;

public partial class SKCanvasDrawings
{
	extension(SKCanvas @this)
	{
		/// <summary>
		/// Draws a diamond symbol into the specified cell.
		/// </summary>
		/// <param name="cell">The cell.</param>
		/// <param name="sizeScale">The scale of size.</param>
		/// <param name="strokeColor">The stroke color.</param>
		/// <param name="strokeWidthScale">The stroke width scale.</param>
		/// <param name="fillColor">The fill color.</param>
		/// <param name="mapper">The mapper.</param>
		public void DrawDiamondToCell(
			Absolute cell,
			Scale sizeScale,
			SerializableColor strokeColor,
			Scale strokeWidthScale,
			SerializableColor fillColor,
			PointMapper mapper
		)
		{
			var cellSize = mapper.CellSize;
			var lineContainingBoxSize = sizeScale.Measure(cellSize);
			var halfPadding = (cellSize - lineContainingBoxSize) / 2;
			var topLeft = mapper.GetPoint(cell, Alignment.TopLeft) + new SKPoint(+halfPadding, +halfPadding);
			var topRight = mapper.GetPoint(cell, Alignment.TopRight) + new SKPoint(-halfPadding, +halfPadding);
			var bottomLeft = mapper.GetPoint(cell, Alignment.BottomLeft) + new SKPoint(+halfPadding, -halfPadding);
			var top = topLeft + new SKPoint(lineContainingBoxSize / 2, 0);
			var bottom = bottomLeft + new SKPoint(lineContainingBoxSize / 2, 0);
			var left = topLeft + new SKPoint(0, lineContainingBoxSize / 2);
			var right = topRight + new SKPoint(0, lineContainingBoxSize / 2);
			using var path = new SKPath();
			path.MoveTo(top);
			path.LineTo(left);
			path.LineTo(bottom);
			path.LineTo(right);
			path.Close();

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
				using var fillPaint = new SKPaint { Style = SKPaintStyle.Fill, Color = fillColor, IsAntialias = true };
				@this.DrawPath(path, fillPaint);
				@this.DrawPath(path, strokePaint);
			}
		}
	}
}
