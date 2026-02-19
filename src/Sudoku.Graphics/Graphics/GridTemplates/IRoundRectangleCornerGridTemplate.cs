namespace Sudoku.Graphics.GridTemplates;

/// <summary>
/// Represents a grid template that supports drawing corners with rounded rectangle.
/// </summary>
public interface IRoundRectangleCornerGridTemplate
{
	/// <summary>
	/// Indicates whether the border should be drawn as rounded rectangle. By default it's <see langword="true"/>.
	/// </summary>
	bool IsRoundedRectangle { get; init; }
}
