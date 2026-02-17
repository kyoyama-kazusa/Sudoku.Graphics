namespace Sudoku.Graphics;

public partial class Canvas
{
	/// <inheritdoc/>
	public partial void DrawBigText(string text, Relative cell, SKColor color, SKFontStyleSlant slant)
		=> DrawBigText(text, Mapper.ToAbsoluteIndex(cell), color, slant);

	/// <inheritdoc/>
	public partial void DrawBigText(string text, Absolute cell, SKColor color, SKFontStyleSlant slant)
	{
		using var typeface = SKTypeface.FromFamilyName(
			DrawingOptions.BigTextFontName.Resolve(DrawingOptions),
			DrawingOptions.BigTextFontWeight.Resolve(DrawingOptions),
			DrawingOptions.BigTextFontWidth.Resolve(DrawingOptions),
			slant
		);
		var factSize = DrawingOptions.BigTextFontSizeScale.Resolve(DrawingOptions).Measure(Mapper.CellSize);
		using var textFont = new SKFont(typeface, factSize) { Subpixel = true };
		using var textPaint = new SKPaint { Color = color };
		var offset = textFont.MeasureText(text, textPaint);
		BackingCanvas.DrawText(
			text,
			Mapper.GetPoint(cell, CellCornerType.Center)
				+ new SKPoint(0, offset / (2 * text.Length)) // Offset adjustment
				+ new SKPoint(0, Mapper.CellSize / 12), // Manual adjustment
			SKTextAlign.Center,
			textFont,
			textPaint
		);
	}
}
