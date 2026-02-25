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
	/// Indicates the background filling.
	/// </summary>
	BackgroundFill,

	/// <summary>
	/// Indicates cell filling.
	/// </summary>
	CellFill,

	/// <summary>
	/// Indicates candidate filling.
	/// </summary>
	CandidateFill,

	/// <summary>
	/// Indicates template line strokes.
	/// </summary>
	TemplateLineStroke,

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

	/// <summary>
	/// Indicates cell question mark.
	/// </summary>
	CellMark_Question,
}
