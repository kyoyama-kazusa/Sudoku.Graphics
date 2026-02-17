namespace Sudoku.Graphics.Primitives;

/// <summary>
/// Represents a canvas object that supports filling background methods.
/// </summary>
public interface ICanvasFillBackground
{
	/// <summary>
	/// Fill background using the color specified in drawing options <see cref="CanvasDrawingOptions.BackgroundColor"/>.
	/// </summary>
	/// <seealso cref="CanvasDrawingOptions.BackgroundColor"/>
	void FillBackground();

	/// <summary>
	/// Fill background using the specified color.
	/// </summary>
	/// <param name="color">The color.</param>
	void FillBackground(SKColor color);
}
