namespace Sudoku.Graphics;

public partial class SKCanvasDrawings
{
	extension(SKCanvas @this)
	{
		/// <summary>
		/// Draws a battenburg mark.
		/// </summary>
		/// <param name="cell1">The cell 1.</param>
		/// <param name="cell2">The cell 2.</param>
		/// <param name="sizeScale">The scale of size, related to cell size.</param>
		/// <param name="colorA">Indicates color 1 (top-left and bottom-right).</param>
		/// <param name="colorB">Indicates color 2 (top-right and bottom-left).</param>
		/// <param name="strokeColor">The stroke line color.</param>
		/// <param name="strokeWidthScale">The scale of stroke width, related to cell size.</param>
		/// <param name="cornerRadiiScale">The scale of corner radii.</param>
		/// <param name="mapper">The mapper instance.</param>
		public void DrawBattenburg(
			Absolute cell1,
			Absolute cell2,
			Scale sizeScale,
			SerializableColor colorA,
			SerializableColor colorB,
			SerializableColor strokeColor,
			Scale strokeWidthScale,
			Scale[]? cornerRadiiScale,
			PointMapper mapper
		)
		{
			if (cell1 > cell2)
			{
				(cell1, _) = (cell2, cell1);
			}

			@this.DrawBattenburg_Generic(
				mapper.GetPointBetweenWithAdjacentRelation(cell1, cell2, out _),
				sizeScale,
				colorA,
				colorB,
				strokeColor,
				strokeWidthScale,
				cornerRadiiScale,
				mapper
			);
		}

		/// <summary>
		/// Draws a battenburg mark to the specified point.
		/// </summary>
		/// <param name="centerPoint">The center point to draw.</param>
		/// <param name="sizeScale">The scale of size, related to cell size.</param>
		/// <param name="colorA">Indicates color 1 (top-left and bottom-right).</param>
		/// <param name="colorB">Indicates color 2 (top-right and bottom-left).</param>
		/// <param name="strokeColor">The stroke line color.</param>
		/// <param name="strokeWidthScale">The scale of stroke width, related to cell size.</param>
		/// <param name="cornerRadiiScale">The scale of corner radii.</param>
		/// <param name="mapper">The mapper instance.</param>
		public void DrawBattenburg_Generic(
			SKPoint centerPoint,
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
			var size = sizeScale.Measure(cellSize);
			var small = size / 2;
			var left = centerPoint.X - small;
			var top = centerPoint.Y - small;
			var cornerRadii = (
				stackalloc[]
				{
					cornerRadiiScale[0].Measure(small),
					cornerRadiiScale[1].Measure(small),
					cornerRadiiScale[2].Measure(small),
					cornerRadiiScale[3].Measure(small)
				}
			);

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
