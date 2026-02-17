namespace Sudoku.Graphics;

public partial class Canvas
{
	/// <inheritdoc/>
	public partial void DrawSmallText(string text, Relative cell, int innerPosition, int splitSize, SKColor color)
		=> DrawSmallText(text, Mapper.GetAbsoluteIndex(cell), innerPosition, splitSize, color);

	/// <inheritdoc/>
	public partial void DrawSmallText(string text, Absolute cell, int innerPosition, int splitSize, SKColor color)
	{
		// The main idea on drawing candidates is to find for the number of rows and columns in a cell should be drawn,
		// accommodating all possible candidate values.
		// The general way is to divide a cell into <c>n * n</c> subcells, in order to fill with each candidate value.
		// Here variable <c>splitSize</c> represents the variable <c>n</c> (for <c>n * n</c> subcells).

		using var typeface = SKTypeface.FromFamilyName(
			Options.SmallTextFontName.Resolve(Options),
			Options.SmallTextFontWeight.Resolve(Options),
			Options.SmallTextFontWidth.Resolve(Options),
			Options.SmallTextFontSlant.Resolve(Options)
		);
		var cellTopLeft = Mapper.GetPoint(cell, CellAlignment.TopLeft);
		var candidateSize = Mapper.CellSize / splitSize;
		var candidateRowIndex = innerPosition / splitSize;
		var candidateColumnIndex = innerPosition % splitSize;
		var factSize = Options.SmallTextFontSizeScale.Resolve(Options).Measure(Mapper.CellSize) / splitSize;
		using var textFont = new SKFont(typeface, factSize) { Subpixel = true };
		using var textPaint = new SKPaint { Color = color };
		var offset = textFont.MeasureText(text, textPaint);
		BackingCanvas.DrawText(
			text,
			cellTopLeft
				+ new SKPoint(candidateColumnIndex * candidateSize, candidateRowIndex * candidateSize) // Adjust to candidate position
				+ new SKPoint(candidateSize / 2, candidateSize / 2) // Adjust to candidate center
				+ new SKPoint(0, offset / (2 * text.Length)) // Offset adjustment
				+ new SKPoint(0, candidateSize / 12), // Manual adjustment
			SKTextAlign.Center,
			textFont,
			textPaint
		);
	}
}
