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
	/// Indicates given text.
	/// </summary>
	GivenText,

	/// <summary>
	/// Indicates modifiable text.
	/// </summary>
	ModifiableText,

	/// <summary>
	/// Indicates candidate text.
	/// </summary>
	CandidateText,
}
