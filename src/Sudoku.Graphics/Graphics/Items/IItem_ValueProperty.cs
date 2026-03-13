namespace Sudoku.Graphics.Items;

/// <summary>
/// Represents an item type that supports value property.
/// </summary>
/// <typeparam name="T">The type of value.</typeparam>
public interface IItem_ValueProperty<T>
{
	/// <summary>
	/// Indicates the value.
	/// </summary>
	T Value { get; init; }
}
