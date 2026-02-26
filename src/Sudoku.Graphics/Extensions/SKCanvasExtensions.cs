namespace SkiaSharp;

/// <summary>
/// Provides extension members on <see cref="SKCanvas"/>.
/// </summary>
/// <seealso cref="SKCanvas"/>
public static class SKCanvasExtensions
{
	/// <param name="this">The current instance.</param>
	extension(SKCanvas @this)
	{
		/// <summary>
		/// 
		/// </summary>
		public void DrawTextToCell(
			string text,
			Absolute cell,
			string fontName,
			Scale fontScale,
			SKFontStyleWeight fontWeight,
			SKFontStyleWidth fontWidth,
			SKFontStyleSlant fontSlant,
			SerializableColor fillColor,
			PointMapper mapper
		) => @this.DrawShadowedTextToCell(
			text,
			cell,
			fontName,
			fontScale,
			0M,
			fontWeight,
			fontWidth,
			fontSlant,
			SKColors.Transparent,
			fillColor,
			mapper
		);

		/// <summary>
		/// 
		/// </summary>
		public void DrawShadowedTextToCell(
			string text,
			Absolute cell,
			string fontName,
			Scale fontScale,
			Scale strokeWidthScale,
			SKFontStyleWeight fontWeight,
			SKFontStyleWidth fontWidth,
			SKFontStyleSlant fontSlant,
			SerializableColor strokeColor,
			SerializableColor fillColor,
			PointMapper mapper
		)
		{
			using var typeface = SKTypeface.FromFamilyName(fontName, fontWeight, fontWidth, fontSlant);
			var factSize = fontScale.Measure(mapper.CellSize);
			using var textFont = new SKFont(typeface, factSize) { Subpixel = true };
			using var textStrokePaint = new SKPaint
			{
				Style = SKPaintStyle.Stroke,
				Color = strokeColor,
				IsAntialias = true,
				StrokeWidth = strokeWidthScale.Measure(factSize),
				StrokeJoin = SKStrokeJoin.Round
			};
			using var textFillPaint = new SKPaint { Style = SKPaintStyle.Fill, Color = fillColor, IsAntialias = true };
			var textMetrics = textFont.Metrics;
			var targetPoint = mapper.GetPoint(cell, Alignment.Center)
				+ new SKPoint(0, (textMetrics.Ascent + textMetrics.Descent) / 2) // Baseline adjustment
				+ new SKPoint(0, textFont.Size / 2) // Centeralize
				+ new SKPoint(0, mapper.CellSize / 6); // Manual adjustment
			@this.DrawText(text, targetPoint, SKTextAlign.Center, textFont, textStrokePaint);
			@this.DrawText(text, targetPoint, SKTextAlign.Center, textFont, textFillPaint);
		}
	}
}
