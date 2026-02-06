using SkiaSharp;

namespace Sudoku.Graphics;

/// <summary>
/// Represents grid line template type that draws lines onto a canvas via the specified configuration.
/// </summary>
public abstract class GridLineTemplate
{
	/// <summary>
	/// Try to draw lines onto the target canvas.
	/// </summary>
	/// <param name="canvas">The canvas object to receive drawn items.</param>
	public abstract void DrawLines(SKCanvas canvas);
}
