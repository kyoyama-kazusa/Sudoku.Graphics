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
				throw new ArgumentException("cornerRatios must be null or an array of length 4 (tl,tr,br,bl).");
			}

			var cellSize = mapper.CellSize;
			var (x, y) = mapper.GetPoint(cell, Alignment.TopLeft);
			var iconSize = sizeScale.Measure(cellSize);
			var offset = (cellSize - iconSize) / 2;
			var iconLeft = x + offset;
			var iconTop = y + offset;
			var small = iconSize / 2;
			var cornerRadii = (stackalloc float[]
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
				var rect = new SKRect(iconLeft, iconTop, iconLeft + small, iconTop + small);
				var rr = new SKRoundRect();
				rr.SetRectRadii(rect, makeCornerRadii(cornerRadii[0], 0, 0, 0));
				fillPaint.Color = colorA;
				@this.DrawRoundRect(rr, fillPaint);
				@this.DrawRoundRect(rr, strokePaint);
			}

			// Top-right
			{
				var rect = new SKRect(iconLeft + small, iconTop, iconLeft + iconSize, iconTop + small);
				var rr = new SKRoundRect();
				rr.SetRectRadii(rect, makeCornerRadii(0, cornerRadii[1], 0, 0));
				fillPaint.Color = colorB;
				@this.DrawRoundRect(rr, fillPaint);
				@this.DrawRoundRect(rr, strokePaint);
			}

			// Bottom-right
			{
				var rect = new SKRect(iconLeft + small, iconTop + small, iconLeft + iconSize, iconTop + iconSize);
				var rr = new SKRoundRect();
				rr.SetRectRadii(rect, makeCornerRadii(0, 0, cornerRadii[2], 0));
				fillPaint.Color = colorA;
				@this.DrawRoundRect(rr, fillPaint);
				@this.DrawRoundRect(rr, strokePaint);
			}

			// Bottom-left
			{
				var rect = new SKRect(iconLeft, iconTop + small, iconLeft + small, iconTop + iconSize);
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
