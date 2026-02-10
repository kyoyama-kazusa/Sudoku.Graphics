namespace Sudoku.Graphics;

/// <summary>
/// Represents a canvas object that supports line-drawing methods.
/// </summary>
public interface ICanvasDrawLinesMethods
{
	/// <summary>
	/// Try to draw lines using the specified template.
	/// </summary>
	/// <param name="options">The options that provides template to draw.</param>
	void DrawLines(CanvasDrawingOptions? options = null);
}
