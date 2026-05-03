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
	/// Indicates cell bisector line mark.
	/// </summary>
	Cell_BisectorLine,

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

	/// <summary>
	/// Indicates cell large diamond mark.
	/// </summary>
	Cell_LargeDiamond,

	/// <summary>
	/// Indicates loop segment lines.
	/// </summary>
	Cell_LoopSegmentLine,
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
	CellText_Question,

	/// <summary>
	/// Indicates cell exclamation mark.
	/// </summary>
	CellText_Exclamation,

	/// <summary>
	/// Indicates cell suit text mark.
	/// </summary>
	CellText_Suit,

	/// <summary>
	/// Indicates cell emoji mark.
	/// </summary>
	CellText_Emoji,

	/// <summary>
	/// Indicates cell zodiac emoji mark.
	/// </summary>
	CellText_ZodiacEmoji,

	/// <summary>
	/// Indicates cell arrow text mark.
	/// </summary>
	CellText_Arrow,

	/// <summary>
	/// Indicates arithmetic operator text mark.
	/// </summary>
	CellText_ArithmeticOperator,

	/// <summary>
	/// Indicates bitwise operator text mark.
	/// </summary>
	CellText_BitwiseOperator,

	/// <summary>
	/// Indicates comparison operator text mark.
	/// </summary>
	CellText_ComparisonOperator,

	/// <summary>
	/// Indicates cell border-aligned text mark.
	/// </summary>
	CellText_BorderAligned,

	/// <summary>
	/// Indicates cell border-aligned digit text mark.
	/// </summary>
	CellText_BorderAlignedDigit,
	#endregion

	//
	// Cell pair text marks
	//
	#region Cell pair text marks
	/// <summary>
	/// Indicates cell pair roman numeral text mark.
	/// </summary>
	CellPairText_RomanNumeral,

	/// <summary>
	/// Indicates cell pair number text mark.
	/// </summary>
	CellPairText_Number,

	/// <summary>
	/// Indicates cell pair arrow text mark.
	/// </summary>
	CellPairText_Arrow,

	/// <summary>
	/// Indicates cell pair comparison operator mark.
	/// </summary>
	CellPairText_ComparisonOperator,

	/// <summary>
	/// Indicates cell pair raw text mark.
	/// </summary>
	CellPairText_Raw,
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

	/// <summary>
	/// Indicates cell pair island connector mark.
	/// </summary>
	CellPair_IslandConnector,
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
	/// Indicates satellite cell group mark.
	/// </summary>
	CellGroup_CapsuleWithSatellite,

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
	// Candidate pair marks
	//
	#region Candidate pair marks
	/// <summary>
	/// Indicates candidate pair line mark.
	/// </summary>
	CandidatePair_Line,

	/// <summary>
	/// Indicates candidate pair Bezier line mark.
	/// </summary>
	CandidatePair_BezierLine,
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
