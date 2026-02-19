namespace Sudoku.Graphics.Primitives;

/// <summary>
/// Represents a canvas object that supports big-text-drawing methods.
/// </summary>
public interface ICanvasDrawBigText
{
	/// <summary>
	/// Draws the specified text onto the specified cell.
	/// </summary>
	/// <param name="template">The template that the text will be drawn.</param>
	/// <param name="text">The text.</param>
	/// <param name="cell">The relative cell.</param>
	/// <param name="color">The color.</param>
	void DrawBigText(GridTemplate template, string text, Relative cell, SKColor color);

	/// <summary>
	/// Draws the specified text onto the specified cell.
	/// </summary>
	/// <param name="template">The template that the text will be drawn.</param>
	/// <param name="text">The text.</param>
	/// <param name="cell">The absolute cell.</param>
	/// <param name="color">The color.</param>
	void DrawBigText(GridTemplate template, string text, Absolute cell, SKColor color);
}
