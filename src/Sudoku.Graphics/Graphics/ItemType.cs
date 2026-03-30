namespace Sudoku.Graphics;

/// <summary>
/// Represents a type of item.
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
	Fill_Background,

	/// <summary>
	/// Indicates cell filling.
	/// </summary>
	Fill_Cell,

	/// <summary>
	/// Indicates candidate filling.
	/// </summary>
	Fill_Candidate,
	#endregion

	//
	// Cell marks
	//
	#region Cell marks
	/// <summary>
	/// Indicates cell tetris mark.
	/// </summary>
	Cell_Tetris,

	/// <summary>
	/// Indicates cell dice mark.
	/// </summary>
	Cell_Dice,

	/// <summary>
	/// Indicates cell phantom dice mark.
	/// </summary>
	Cell_PhantomDice,

	/// <summary>
	/// Indicates cell surrounding triangles mark.
	/// </summary>
	Cell_SurroundingTriangles,

	/// <summary>
	/// Indicates cell square mark.
	/// </summary>
	Cell_Square,

	/// <summary>
	/// Indicates cell circle mark.
	/// </summary>
	Cell_Circle,

	/// <summary>
	/// Indicates cell polygon mark.
	/// </summary>
	Cell_Polygon,

	/// <summary>
	/// Indicates cell cross mark.
	/// </summary>
	Cell_Cross,

	/// <summary>
	/// Indicates cell moon phase mark.
	/// </summary>
	Cell_MoonPhase,

	/// <summary>
	/// Indicates cell arrow triangle mark.
	/// </summary>
	Cell_ArrowTriangle,

	/// <summary>
	/// Indicates cell arrow mark.
	/// </summary>
	Cell_Arrow,

	/// <summary>
	/// Indicates cell apex corner triangle mark.
	/// </summary>
	Cell_ApexCornerTriangle,

	/// <summary>
	/// Indicates cell halve line mark.
	/// </summary>
	Cell_HalveLine,

	/// <summary>
	/// Indicates cell diamond mark.
	/// </summary>
	Cell_Diamond,

	/// <summary>
	/// Indicates cell hexagon mark.
	/// </summary>
	Cell_Hexagon,

	/// <summary>
	/// Indicates cell triangle mark.
	/// </summary>
	Cell_Triangle,

	/// <summary>
	/// Indicates cell battenburg mark.
	/// </summary>
	Cell_Battenburg,

	/// <summary>
	/// Indicates cell seven segment display mark.
	/// </summary>
	Cell_SevenSegmentDisplay,

	/// <summary>
	/// Indicates cell border-aligned triangle mark.
	/// </summary>
	Cell_BorderAlignedTriangle,

	/// <summary>
	/// Indicates cell border-aligned arrow mark.
	/// </summary>
	Cell_BorderAlignedArrow,
	#endregion

	//
	// Candidate marks
	//
	#region Candidate marks
	/// <summary>
	/// Indicates circle candidate mark.
	/// </summary>
	Candidate_Circle,

	/// <summary>
	/// Indicates candidate cross mark.
	/// </summary>
	Candidate_Cross,
	#endregion

	//
	// Text
	//
	#region Text
	/// <summary>
	/// Indicates given text.
	/// </summary>
	Text_Given,

	/// <summary>
	/// Indicates modifiable text.
	/// </summary>
	Text_Modifiable,

	/// <summary>
	/// Indicates candidate text.
	/// </summary>
	Text_Candidate,
	#endregion

	//
	// Cell text marks
	//
	#region Cell text marks
	/// <summary>
	/// Indicates cell question mark.
	/// </summary>
	Cell_QuestionText,

	/// <summary>
	/// Indicates cell exclamation mark.
	/// </summary>
	Cell_ExclamationText,

	/// <summary>
	/// Indicates cell suit text mark.
	/// </summary>
	Cell_SuitText,

	/// <summary>
	/// Indicates cell emoji mark.
	/// </summary>
	Cell_EmojiText,

	/// <summary>
	/// Indicates cell zodiac emoji mark.
	/// </summary>
	Cell_ZodiacEmojiText,

	/// <summary>
	/// Indicates cell arrow text mark.
	/// </summary>
	Cell_ArrowText,

	/// <summary>
	/// Indicates arithmetic operator text mark.
	/// </summary>
	Cell_ArithmeticOperatorText,

	/// <summary>
	/// Indicates bitwise operator text mark.
	/// </summary>
	Cell_BitwiseOperatorText,

	/// <summary>
	/// Indicates comparison operator text mark.
	/// </summary>
	Cell_ComparisonOperatorText,

	/// <summary>
	/// Indicates cell border-aligned text mark.
	/// </summary>
	Cell_BorderAlignedText,
	#endregion

	//
	// Cell pair marks
	//
	#region Cell pair marks
	/// <summary>
	/// Indicates bridge line cell pair mark.
	/// </summary>
	CellPair_BridgeLine,

	/// <summary>
	/// Indicates adjacent cell pair bar mark.
	/// </summary>
	CellPair_Bar,

	/// <summary>
	/// Indicates cell connection line mark.
	/// </summary>
	CellPair_ConnectionLine,

	/// <summary>
	/// Indicates cell pair kropki (circle) mark.
	/// </summary>
	CellPair_Kropki,

	/// <summary>
	/// Indicates cell pair kropki square mark.
	/// </summary>
	CellPair_KropkiSquare,

	/// <summary>
	/// Indicates cell pair kropki triangle mark.
	/// </summary>
	CellPair_KropkiTriangle,

	/// <summary>
	/// Indicates cell pair batternburg mark.
	/// </summary>
	CellPair_Battenburg,
	#endregion

	//
	// Cell group marks
	//
	#region Cell group marks
	/// <summary>
	/// Indicates thermometer cell group mark.
	/// </summary>
	CellGroup_Thermometer,

	/// <summary>
	/// Indicates capsule with arrow line cell group mark.
	/// </summary>
	CellGroup_CapsuleWithArrowLine,

	/// <summary>
	/// Indicates cell trail cell group mark.
	/// </summary>
	CellGroup_CellTrail,

	/// <summary>
	/// Indicates killer cage cell group mark.
	/// </summary>
	CellGroup_KillerCage,
	#endregion

	//
	// Lines
	//
	#region Lines
	/// <summary>
	/// Indicates template line strokes.
	/// </summary>
	Line_Template,

	/// <summary>
	/// Indicates variant line strokes.
	/// </summary>
	Line_Variant,
	#endregion
}
