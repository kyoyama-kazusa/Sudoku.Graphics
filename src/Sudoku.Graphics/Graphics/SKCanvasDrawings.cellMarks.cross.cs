namespace Sudoku.Graphics;

public partial class SKCanvasDrawings
{
	extension(SKCanvas @this)
	{
		/// <summary>
		/// Draws a cross symbol into the specified cell or candidate.
		/// </summary>
		/// <typeparam name="TLocator">The type of cell or candidate.</typeparam>
		/// <param name="locator">The cell or candidate.</param>
		/// <param name="sizeScale">The scale of size, related to cell size.</param>
		/// <param name="strokeWidthScale">The stroke width scale.</param>
		/// <param name="color">The color.</param>
		/// <param name="mapper">The mapper instance.</param>
		/// <param name="cap">The stroke cap. By default it's <see cref="SKStrokeCap.Round"/>.</param>
		/// <seealso cref="SKStrokeCap.Round"/>
		public void DrawCrossTo<TLocator>(
			TLocator locator,
			Scale sizeScale,
			Scale strokeWidthScale,
			SerializableColor color,
			PointMapper mapper,
			SKStrokeCap cap = SKStrokeCap.Round
		)
			where TLocator : unmanaged, ILocator<TLocator>
		{
			var cellSize = mapper.CellSize;
			var center = mapper.GetPoint(locator, Alignment.Center);
			var paddingOffset = sizeScale.Measure(cellSize / 2);
			var topLeft = new SKPoint(center.X - paddingOffset, center.Y - paddingOffset);
			var bottomRight = new SKPoint(center.X + paddingOffset, center.Y + paddingOffset);
			var topRight = new SKPoint(center.X + paddingOffset, center.Y - paddingOffset);
			var bottomLeft = new SKPoint(center.X - paddingOffset, center.Y + paddingOffset);

			// Stroke paint.
			var strokeWidth = strokeWidthScale.Measure(cellSize);
			using var paint = new SKPaint
			{
				Style = SKPaintStyle.Stroke,
				StrokeWidth = strokeWidth,
				Color = color,
				IsAntialias = true,
				StrokeCap = cap
			};
			@this.DrawLine(topLeft, bottomRight, paint);
			@this.DrawLine(topRight, bottomLeft, paint);
		}
	}
}
