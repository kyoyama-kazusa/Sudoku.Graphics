namespace Sudoku.Graphics;

public partial class Canvas
{
	/// <inheritdoc/>
	public partial void DrawBigText(string text, Relative cell, SKColor color)
		=> DrawBigText(text, Mapper.GetAbsoluteIndex(cell), color);

	/// <inheritdoc/>
	public partial void DrawBigText(string text, Absolute cell, SKColor color)
	{
		using var typeface = SKTypeface.FromFamilyName(
			Options.BigTextFontName.Resolve(Options),
			Options.BigTextFontWeight.Resolve(Options),
			Options.BigTextFontWidth.Resolve(Options),
			Options.BigTextFontSlant.Resolve(Options)
		);
		var factSize = Options.BigTextFontSizeScale.Resolve(Options).Measure(Mapper.CellSize);
		using var textFont = new SKFont(typeface, factSize) { Subpixel = true };
		using var textPaint = new SKPaint { Color = color };
		var offset = textFont.MeasureText(text, textPaint);
		BackingCanvas.DrawText(
			text,
			Mapper.GetPoint(cell, CellAlignment.Center)
				+ new SKPoint(0, offset / (2 * text.Length)) // Offset adjustment
				+ new SKPoint(0, Mapper.CellSize / 12), // Manual adjustment
			SKTextAlign.Center,
			textFont,
			textPaint
		);
	}
}
