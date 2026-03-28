namespace SkiaSharp;

/// <summary>
/// Provides basic encapsulation on drawing items.
/// </summary>
public static class SKCanvasExtensions
{
	/// <summary>
	/// Provides extension members on <see cref="SKCanvas"/> instances.
	/// </summary>
	/// <param name="this">The current instance.</param>
	extension(SKCanvas @this)
	{
		/// <summary>
		/// Draws the specified text with background cover.
		/// </summary>
		/// <param name="point">The point.</param>
		/// <param name="text">The text.</param>
		/// <param name="textAlign">The text align.</param>
		/// <param name="font">The font.</param>
		/// <param name="paint">The paint.</param>
		/// <param name="coverPaint">The paint of cover background.</param>
		/// <param name="paddingTop">The padding top of the boundary of text drawn.</param>
		/// <param name="paddingBottom">The padding bottom of the boundary of text drawn.</param>
		/// <param name="paddingLeft">The padding left of the boundary of text drawn.</param>
		/// <param name="paddingRight">The padding right of the boundary of text drawn.</param>
		/// <param name="offset">The offset to the text to be drawn.</param>
		/// <exception cref="ArgumentOutOfRangeException">Throws when <paramref name="textAlign"/> is not defined.</exception>
		public void DrawTextWithCover(
			SKPoint point,
			string text,
			SKTextAlign textAlign,
			SKFont font,
			SKPaint paint,
			SKPaint coverPaint,
			float paddingTop,
			float paddingBottom,
			float paddingLeft,
			float paddingRight,
			SKPoint offset
		)
		{
			if (string.IsNullOrWhiteSpace(text))
			{
				// Nothing to draw.
				return;
			}

			var drawPoint = new SKPoint(point.X + offset.X, point.Y + offset.Y);
			var textWidth = font.MeasureText(text, paint);
			font.MeasureText(text, out var bounds, paint);
			var alignedX = textAlign switch
			{
				SKTextAlign.Left => drawPoint.X,
				SKTextAlign.Center => drawPoint.X - textWidth / 2,
				SKTextAlign.Right => drawPoint.X - textWidth,
				_ => throw new ArgumentOutOfRangeException(nameof(textAlign))
			};
			bounds.Offset(alignedX, drawPoint.Y);
			var coverBounds = new SKRect(
				bounds.Left - paddingLeft,
				bounds.Top - paddingTop,
				bounds.Right + paddingRight,
				bounds.Bottom + paddingBottom
			);

			@this.DrawRect(coverBounds, coverPaint);
			@this.DrawText(text, drawPoint, textAlign, font, paint);
		}
	}
}
