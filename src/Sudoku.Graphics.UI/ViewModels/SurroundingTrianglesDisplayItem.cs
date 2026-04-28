namespace Sudoku.Graphics.UI.ViewModels;

/// <summary>
/// Represents a type of surrounding triangles.
/// </summary>
public sealed class SurroundingTrianglesDisplayItem
{
	/// <summary>
	/// Indicates the value.
	/// </summary>
	public int Value { get; set; }

	/// <summary>
	/// Indicates the source.
	/// </summary>
	public ImageSource? Source { get; set; }
}
