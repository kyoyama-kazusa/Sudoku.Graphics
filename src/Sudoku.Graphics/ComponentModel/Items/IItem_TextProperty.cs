namespace Sudoku.ComponentModel.Items;

/// <summary>
/// Represents a type that includes <see cref="Text"/> property.
/// </summary>
public interface IItem_TextProperty
{
	/// <summary>
	/// Indicates the text to draw.
	/// </summary>
	string Text { get; init; }
}
