namespace Sudoku.Graphics;

public partial class SKCanvasDrawings
{
	extension(SKCanvas @this)
	{
		/// <summary>
		/// Draws a battenburg mark into the specified cell.
		/// </summary>
		/// <param name="cell">The cell.</param>
		/// <param name="sizeScale">The scale of size, related to cell size.</param>
		/// <param name="colorA">Indicates color 1 (top-left and bottom-right).</param>
		/// <param name="colorB">Indicates color 2 (top-right and bottom-left).</param>
		/// <param name="strokeColor">The stroke line color.</param>
		/// <param name="strokeWidthScale">The scale of stroke width, related to cell size.</param>
		/// <param name="cornerRadiiScale">The scale of corner radii.</param>
		/// <param name="mapper">The mapper instance.</param>
		public void DrawBattenburgToCell(
			Absolute cell,
			Scale sizeScale,
			SerializableColor colorA,
			SerializableColor colorB,
			SerializableColor strokeColor,
			Scale strokeWidthScale,
			Scale[]? cornerRadiiScale,
			PointMapper mapper
		)
		{
			cornerRadiiScale ??= [0M, 0M, 0M, 0M];

			if (cornerRadiiScale.Length != 4)
			{
				throw new ArgumentException("Expects an array of length 4.", nameof(cornerRadiiScale));
			}

			var cellSize = mapper.CellSize;
			var (x, y) = mapper.GetPoint(cell, Alignment.TopLeft);
			var size = sizeScale.Measure(cellSize);
			var offset = (cellSize - size) / 2;
			var left = x + offset;
			var top = y + offset;
			var small = size / 2;
			var cornerRadii = (stackalloc[]
			{
				cornerRadiiScale[0].Measure(small),
				cornerRadiiScale[1].Measure(small),
				cornerRadiiScale[2].Measure(small),
				cornerRadiiScale[3].Measure(small)
			});

			var strokeWidth = strokeWidthScale.Measure(cellSize);
			using var fillPaint = new SKPaint { Style = SKPaintStyle.Fill, IsAntialias = true };
			using var strokePaint = new SKPaint
			{
				Style = SKPaintStyle.Stroke,
				StrokeWidth = strokeWidth,
				IsAntialias = true,
				StrokeCap = SKStrokeCap.Butt,
				Color = strokeColor
			};

			// Top left
			{
				var rect = new SKRect(left, top, left + small, top + small);
				var rr = new SKRoundRect();
				rr.SetRectRadii(rect, makeCornerRadii(cornerRadii[0], 0, 0, 0));
				fillPaint.Color = colorA;
				@this.DrawRoundRect(rr, fillPaint);
				@this.DrawRoundRect(rr, strokePaint);
			}

			// Top-right
			{
				var rect = new SKRect(left + small, top, left + size, top + small);
				var rr = new SKRoundRect();
				rr.SetRectRadii(rect, makeCornerRadii(0, cornerRadii[1], 0, 0));
				fillPaint.Color = colorB;
				@this.DrawRoundRect(rr, fillPaint);
				@this.DrawRoundRect(rr, strokePaint);
			}

			// Bottom-right
			{
				var rect = new SKRect(left + small, top + small, left + size, top + size);
				var rr = new SKRoundRect();
				rr.SetRectRadii(rect, makeCornerRadii(0, 0, cornerRadii[2], 0));
				fillPaint.Color = colorA;
				@this.DrawRoundRect(rr, fillPaint);
				@this.DrawRoundRect(rr, strokePaint);
			}

			// Bottom-left
			{
				var rect = new SKRect(left, top + small, left + small, top + size);
				var rr = new SKRoundRect();
				rr.SetRectRadii(rect, makeCornerRadii(0, 0, 0, cornerRadii[3]));
				fillPaint.Color = colorB;
				@this.DrawRoundRect(rr, fillPaint);
				@this.DrawRoundRect(rr, strokePaint);
			}


			static SKPoint[] makeCornerRadii(float tl, float tr, float br, float bl)
				=> [new(tl, tl), new(tr, tr), new(br, br), new(bl, bl)];
		}
	}
}
