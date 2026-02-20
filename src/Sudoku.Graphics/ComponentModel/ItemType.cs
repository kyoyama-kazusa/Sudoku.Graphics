namespace Sudoku.ComponentModel;

/// <summary>
/// Represents a type of item.
/// </summary>
public enum ItemType
{
	/// <summary>
	/// Indicates the placeholder of this type.
	/// </summary>
	None = 0,

	/// <summary>
	/// Indicates the canvas background.
	/// </summary>
	CanvasBackground,

	/// <summary>
	/// Indicates template lines.
	/// </summary>
	TemplateLines,

	/// <summary>
	/// Indicates big text.
	/// </summary>
	BigText,

	/// <summary>
	/// Indicates small text.
	/// </summary>
	SmallText,
}
