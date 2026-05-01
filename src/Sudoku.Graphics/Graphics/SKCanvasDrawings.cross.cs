namespace Sudoku.Graphics;

public partial class SKCanvasDrawings
{
	extension(SKCanvas @this)
	{
		/// <summary>
		/// Draws a cross symbol into the specified cell or candidate.
		/// </summary>
		/// <param name="locator">The cell or candidate.</param>
		/// <param name="sizeScale">The scale of size, related to its locator size (cell or candidate size).</param>
		/// <param name="strokeWidthScale">The stroke width scale.</param>
		/// <param name="color">The color.</param>
		/// <param name="mapper">The mapper instance.</param>
		/// <param name="cap">The stroke cap. By default it's <see cref="SKStrokeCap.Round"/>.</param>
		/// <seealso cref="SKStrokeCap.Round"/>
		public void DrawCrossTo(
			Locator locator,
			Scale sizeScale,
			Scale strokeWidthScale,
			SerializableColor color,
			PointMapper mapper,
			SKStrokeCap cap = SKStrokeCap.Round
		)
		{
			var cellSize = mapper.CellSize;
			var measurer = locator.GetLocatorMeasurer(cellSize);
			var center = mapper.GetPoint(locator, Alignment.Center);
			var paddingOffset = sizeScale.Measure(measurer / 2);
			var topLeft = new SKPoint(center.X - paddingOffset, center.Y - paddingOffset);
			var bottomRight = new SKPoint(center.X + paddingOffset, center.Y + paddingOffset);
			var topRight = new SKPoint(center.X + paddingOffset, center.Y - paddingOffset);
			var bottomLeft = new SKPoint(center.X - paddingOffset, center.Y + paddingOffset);

			// Stroke paint.
			var strokeWidth = strokeWidthScale.Measure(measurer);
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
