namespace Sudoku.Graphics.Primitives;

/// <summary>
/// Represents a canvas object that supports small-text-drawing methods.
/// </summary>
public interface ICanvasDrawSmallText
{
	/// <summary>
	/// Draws the specified text onto the specified cell, with the specified offset values as inner offsets in a cell;
	/// such offsets are used by drawing candidates.
	/// </summary>
	/// <param name="text">The text.</param>
	/// <param name="cell">The relative cell.</param>
	/// <param name="innerPosition">The inner position.</param>
	/// <param name="splitSize">The number of inner rows and columns.</param>
	/// <param name="color">The color.</param>
	/// <param name="slant">The text slant.</param>
	void DrawSmallText(string text, Relative cell, int innerPosition, int splitSize, SKColor color, SKFontStyleSlant slant);

	/// <summary>
	/// Draws the specified text onto the specified cell, with the specified offset values as inner offsets in a cell;
	/// such offsets are used by drawing candidates.
	/// </summary>
	/// <param name="text">The text.</param>
	/// <param name="cell">The absolute cell.</param>
	/// <param name="innerPosition">The inner position.</param>
	/// <param name="splitSize">The number of inner rows and columns.</param>
	/// <param name="color">The color.</param>
	/// <param name="slant">The text slant.</param>
	void DrawSmallText(string text, Absolute cell, int innerPosition, int splitSize, SKColor color, SKFontStyleSlant slant);
}
