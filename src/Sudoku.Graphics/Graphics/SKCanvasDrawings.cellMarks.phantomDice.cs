namespace Sudoku.Graphics;

public partial class SKCanvasDrawings
{
	extension(SKCanvas @this)
	{
		/// <summary>
		/// Draws phantom dice to the specified cell.
		/// </summary>
		/// <param name="cell">The cell.</param>
		/// <param name="subgridSize">The subgrid szie.</param>
		/// <param name="sizeScale">The scale of size, related to cell.</param>
		/// <param name="strokeColor">The stroke color.</param>
		/// <param name="strokeWidthScale">The scale of stroke width, related to cell size.</param>
		/// <param name="phantomStrokeWidthScale">The scale of phantom stroke width, related to cell size.</param>
		/// <param name="fillColor">The fill color.</param>
		/// <param name="states">The on/off states table.</param>
		/// <param name="mapper">The mapper instance.</param>
		public void DrawPhantomDiceToCell(
			Absolute cell,
			Relative subgridSize,
			Scale sizeScale,
			SerializableColor strokeColor,
			Scale strokeWidthScale,
			Scale phantomStrokeWidthScale,
			SerializableColor fillColor,
			BitArray states,
			PointMapper mapper
		)
		{
			var cellSize = mapper.CellSize;
			for (var i = 0; i < subgridSize * subgridSize; i++)
			{
				var center = mapper.GetPoint(new CandidatePosition(cell, subgridSize, i), Alignment.Center);
				var radius = sizeScale.Measure(cellSize) / 2;
				if (states[i])
				{
					// Fill paint.
					if (fillColor.Alpha != 0)
					{
						using var fillPaint = new SKPaint { Style = SKPaintStyle.Fill, IsAntialias = true, Color = fillColor };
						@this.DrawCircle(center, radius, fillPaint);
					}

					// Stroke paint.
					var strokeWidth = strokeWidthScale.Measure(cellSize);
					if (strokeWidth != 0 && strokeColor.Alpha != 0)
					{
						using var strokePaint = new SKPaint
						{
							Style = SKPaintStyle.Stroke,
							IsAntialias = true,
							Color = strokeColor,
							StrokeWidth = strokeWidth
						};
						@this.DrawCircle(center, radius, strokePaint);
					}
					continue;
				}

				var phantomStrokeWidth = phantomStrokeWidthScale.Measure(cellSize);
				if (phantomStrokeWidth != 0 && strokeColor.Alpha != 0)
				{
					using var phantomStrokePaint = new SKPaint
					{
						Style = SKPaintStyle.Stroke,
						IsAntialias = true,
						Color = strokeColor,
						StrokeWidth = phantomStrokeWidth
					};
					@this.DrawCircle(center, radius, phantomStrokePaint);
				}
			}
		}
	}
}
