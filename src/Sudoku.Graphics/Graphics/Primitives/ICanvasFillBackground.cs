namespace Sudoku.Graphics.Primitives;

/// <summary>
/// Represents a canvas object that supports filling background methods.
/// </summary>
public interface ICanvasFillBackground
{
	/// <summary>
	/// Clears the canvas via a color.
	/// </summary>
	void FillBackground();

	/// <summary>
	/// Clears the canvas via the specified color.
	/// </summary>
	/// <param name="color">The color.</param>
	void FillBackground(SKColor color);
}
