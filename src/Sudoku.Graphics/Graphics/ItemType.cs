namespace Sudoku.Graphics;

/// <summary>
/// Represents a type of item. The value of field represents ordering of the items of this type to draw.
/// The greater the value is, the lower priority the item will be drawn.
/// </summary>
public enum ItemType
{
	//
	// Placeholder
	//
	#region Placeholder
	/// <summary>
	/// Indicates the placeholder of this type.
	/// </summary>
	None = 0,
	#endregion

	//
	// Background
	//
	#region Background
	/// <summary>
	/// Indicates the background filling.
	/// </summary>
	BackgroundFill = 1,

	/// <summary>
	/// Indicates cell filling.
	/// </summary>
	CellFill,

	/// <summary>
	/// Indicates candidate filling.
	/// </summary>
	CandidateFill,
	#endregion

	//
	// Text
	//
	#region Text
	/// <summary>
	/// Indicates given text.
	/// </summary>
	GivenText = 101,

	/// <summary>
	/// Indicates modifiable text.
	/// </summary>
	ModifiableText,

	/// <summary>
	/// Indicates candidate text.
	/// </summary>
	CandidateText,
	#endregion

	//
	// Cell marks
	//
	#region Cell marks
	/// <summary>
	/// Indicates cell question mark.
	/// </summary>
	CellMark_Question = 201,

	/// <summary>
	/// Indicates cell exclamation mark.
	/// </summary>
	CellMark_Exclamation,

	/// <summary>
	/// Indicates cell tetris mark.
	/// </summary>
	CellMark_Tetris,

	/// <summary>
	/// Indicates cell dice mark.
	/// </summary>
	CellMark_Dice,

	/// <summary>
	/// Indicates cell surrounding triangles mark.
	/// </summary>
	CellMark_SurroundingTriangles,

	/// <summary>
	/// Indicates cell square mark.
	/// </summary>
	CellMark_Square,

	/// <summary>
	/// Indicates cell circle mark.
	/// </summary>
	CellMark_Circle,

	/// <summary>
	/// Indicates cell polygon mark.
	/// </summary>
	CellMark_Polygon,

	/// <summary>
	/// Indicates cell cross mark.
	/// </summary>
	CellMark_Cross,

	/// <summary>
	/// Indicates cell moon phase mark.
	/// </summary>
	CellMark_MoonPhase,

	/// <summary>
	/// Indicates cell arrow triangle mark.
	/// </summary>
	CellMark_ArrowTriangle,

	/// <summary>
	/// Indicates cell arrow mark.
	/// </summary>
	CellMark_Arrow,

	/// <summary>
	/// Indicates cell suit mark.
	/// </summary>
	CellMark_Suit,

	/// <summary>
	/// Indicates cell emoji mark.
	/// </summary>
	CellMark_Emoji,

	/// <summary>
	/// Indicates cell zodiac emoji mark.
	/// </summary>
	CellMark_ZodiacEmoji,

	/// <summary>
	/// Indicates cell apex corner triangle mark.
	/// </summary>
	CellMark_ApexCornerTriangle,

	/// <summary>
	/// Indicates cell halve line mark.
	/// </summary>
	CellMark_HalveLine,

	/// <summary>
	/// Indicates cell diamond mark.
	/// </summary>
	CellMark_Diamond,

	/// <summary>
	/// Indicates cell hexagon mark.
	/// </summary>
	CellMark_Hexagon,

	/// <summary>
	/// Indicates cell triangle mark.
	/// </summary>
	CellMark_Triangle,

	/// <summary>
	/// Indicates cell arrow text mark.
	/// </summary>
	CellMark_ArrowText,

	/// <summary>
	/// Indicates arithmetic operator text mark.
	/// </summary>
	CellMark_ArithmeticOperator,

	/// <summary>
	/// Indicates bitwise operator text mark.
	/// </summary>
	CellMark_BitwiseOperator,

	/// <summary>
	/// Indicates comparison operator text mark.
	/// </summary>
	CellMark_ComparisonOperator,

	/// <summary>
	/// Indicates cell battenburg mark.
	/// </summary>
	CellMark_Battenburg,
	#endregion

	//
	// Template lines
	//
	#region Template lines
	/// <summary>
	/// Indicates template line strokes.
	/// </summary>
	TemplateLineStroke = 1000,
	#endregion
}
