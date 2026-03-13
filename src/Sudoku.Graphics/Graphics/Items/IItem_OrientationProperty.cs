namespace Sudoku.Graphics.Items;

/// <summary>
/// Represents an item type that supports orientation property.
/// </summary>
public interface IItem_OrientationProperty<TOrientation> where TOrientation : Enum
{
	/// <summary>
	/// Indicates the orientation.
	/// </summary>
	TOrientation Orientation { get; init; }
}
