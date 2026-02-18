namespace Sudoku.Graphics;

public partial class Canvas
{
	/// <inheritdoc/>
	public partial void DrawBigText(LineTemplate template, string text, Relative cell, SKColor color)
		=> DrawBigText(template, text, template.Mapper.GetAbsoluteIndex(cell), color);

	/// <inheritdoc/>
	public partial void DrawBigText(LineTemplate template, string text, Absolute cell, SKColor color)
	{
		var mapper = template.Mapper;
		using var typeface = SKTypeface.FromFamilyName(
			Options.BigTextFontName.Resolve(Options),
			Options.BigTextFontWeight.Resolve(Options),
			Options.BigTextFontWidth.Resolve(Options),
			Options.BigTextFontSlant.Resolve(Options)
		);
		var factSize = Options.BigTextFontSizeScale.Resolve(Options).Measure(mapper.CellSize);
		using var textFont = new SKFont(typeface, factSize) { Subpixel = true };
		using var textPaint = new SKPaint { Color = color };
		var offset = textFont.MeasureText(text, textPaint);
		BackingCanvas.DrawText(
			text,
			mapper.GetPoint(cell, CellAlignment.Center)
				+ new SKPoint(0, offset / (2 * text.Length)) // Offset adjustment
				+ new SKPoint(0, mapper.CellSize / 12), // Manual adjustment
			SKTextAlign.Center,
			textFont,
			textPaint
		);
	}
}
