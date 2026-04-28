namespace Sudoku.Graphics.UI.ViewModels;

/// <summary>
/// Represents a polygon display item.
/// </summary>
public sealed class PolygonDisplayItem
{
	/// <summary>
	/// Indicates the number of sides.
	/// </summary>
	public int SidesCount { get; set; }

	/// <summary>
	/// Indicates whether the display item is concave polygon.
	/// </summary>
	public bool IsConcave { get; set; }

	/// <summary>
	/// Indicates image source.
	/// </summary>
	public ImageSource? Source { get; set; }
}
