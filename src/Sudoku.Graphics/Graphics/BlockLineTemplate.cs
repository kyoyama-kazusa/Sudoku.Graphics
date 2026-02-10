namespace Sudoku.Graphics;

/// <summary>
/// Represents a block line template type that draws lines onto a canvas via the specified configuration.
/// </summary>
public abstract class BlockLineTemplate
{
	/// <summary>
	/// Try to draw lines onto the target canvas.
	/// </summary>
	/// <param name="mapper">The mapper instance.</param>
	/// <param name="canvas">The canvas object to receive drawn items.</param>
	/// <param name="options">The options.</param>
	public abstract void DrawLines(PointMapper mapper, SKCanvas canvas, CanvasDrawingOptions options);
}
