namespace Sudoku.Graphics;

/// <summary>
/// Represents grid line template type that draws lines onto a canvas via the specified configuration.
/// </summary>
/// <param name="mapper"><inheritdoc cref="Mapper" path="/summary"/></param>
public abstract class GridLineTemplate(PointMapper mapper)
{
	/// <summary>
	/// Indicates the mapper instance.
	/// </summary>
	protected PointMapper Mapper { get; } = mapper;


	/// <summary>
	/// Try to draw lines onto the target canvas.
	/// </summary>
	/// <param name="canvas">The canvas object to receive drawn items.</param>
	public abstract void DrawLines(SKCanvas canvas);
}
