namespace Sudoku.ComponentModel.Items;

/// <summary>
/// Represents a type that includes <see cref="Color"/> property.
/// </summary>
public interface IItem_ColorProperty
{
	/// <summary>
	/// Indicates the color to fill.
	/// </summary>
	SerializableColor Color { get; init; }
}
