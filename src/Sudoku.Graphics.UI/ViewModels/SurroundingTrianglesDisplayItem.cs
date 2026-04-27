namespace Sudoku.Graphics.UI.ViewModels;

/// <summary>
/// Represents a type of surrounding triangles.
/// </summary>
public sealed partial class SurroundingTrianglesDisplayItem : ObservableObject
{
	/// <summary>
	/// Indicates the value.
	/// </summary>
	[ObservableProperty]
	public partial int Value { get; set; }

	/// <summary>
	/// Indicates the source.
	/// </summary>
	[ObservableProperty]
	public partial ImageSource? Source { get; set; }
}
