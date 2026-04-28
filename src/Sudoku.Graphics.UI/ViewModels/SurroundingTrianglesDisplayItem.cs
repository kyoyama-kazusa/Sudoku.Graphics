namespace Sudoku.Graphics.UI.ViewModels;

/// <summary>
/// Represents a type of surrounding triangles.
/// </summary>
public sealed class SurroundingTrianglesDisplayItem : IIconDisplayItem
{
	/// <summary>
	/// Indicates the value.
	/// </summary>
	public int Value { get; set; }

	/// <inheritdoc/>
	public ImageSource? Icon { get; set; }
}
