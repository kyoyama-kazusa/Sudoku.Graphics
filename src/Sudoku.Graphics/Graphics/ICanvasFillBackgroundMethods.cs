namespace Sudoku.Graphics;

/// <summary>
/// Represents a canvas object that supports filling background methods.
/// </summary>
public interface ICanvasFillBackgroundMethods
{
	/// <summary>
	/// Clears the canvas via the specified color.
	/// </summary>
	/// <param name="color">The color.</param>
	void FillBackground(SKColor color);

	/// <summary>
	/// Clears the canvas via a color, specified by property <see cref="CanvasDrawingOptions.BackgroundColor"/>.
	/// </summary>
	/// <param name="options">The options provider.</param>
	/// <seealso cref="CanvasDrawingOptions.BackgroundColor"/>
	void FillBackground(CanvasDrawingOptions? options = null);
}
