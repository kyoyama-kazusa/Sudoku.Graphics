namespace Sudoku.Graphics.Items;

/// <summary>
/// Represents an item type that supports direction property.
/// </summary>
public interface IItem_DirectionProperty<TDirection> where TDirection : Enum
{
	/// <summary>
	/// Indicates the direction.
	/// </summary>
	TDirection Direction { get; init; }
}
