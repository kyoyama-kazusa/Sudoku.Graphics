namespace Sudoku.Graphics;

/// <summary>
/// Provides extension members on <see cref="SKCanvas"/>.
/// </summary>
/// <seealso cref="SKCanvas"/>
public static class SKCanvasDrawingExtensions
{
	/// <param name="this">The current instance.</param>
	extension(SKCanvas @this)
	{
		/// <summary>
		/// Draws the specified text into the target cell.
		/// </summary>
		/// <inheritdoc cref="DrawOutlinedTextToCell"/>
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
		) => @this.DrawOutlinedTextToCell(
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
		/// Draws the specified text into the target cell, with outlined.
		/// </summary>
		/// <param name="text">The text.</param>
		/// <param name="cell">The cell.</param>
		/// <param name="fontName">The font name.</param>
		/// <param name="fontScale">The font scale, relative to cell size.</param>
		/// <param name="outlineThicknessScale">
		/// The outline width scale, relative to text fact size calculated from <paramref name="fontScale"/>.
		/// </param>
		/// <param name="fontWeight">The font weight.</param>
		/// <param name="fontWidth">The font width.</param>
		/// <param name="fontSlant">The font slant.</param>
		/// <param name="outlineColor">The outline color.</param>
		/// <param name="fillColor">The fill color of text.</param>
		/// <param name="mapper">The mapper.</param>
		public void DrawOutlinedTextToCell(
			string text,
			Absolute cell,
			string fontName,
			Scale fontScale,
			Scale outlineThicknessScale,
			SKFontStyleWeight fontWeight,
			SKFontStyleWidth fontWidth,
			SKFontStyleSlant fontSlant,
			SerializableColor outlineColor,
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
				Color = outlineColor,
				IsAntialias = true,
				StrokeWidth = outlineThicknessScale.Measure(factSize),
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
