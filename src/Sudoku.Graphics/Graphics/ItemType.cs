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
	BackgroundFill,

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
	// Cell marks
	//
	#region Cell marks
	/// <summary>
	/// Indicates cell tetris mark.
	/// </summary>
	CellMark_Tetris,

	/// <summary>
	/// Indicates cell dice mark.
	/// </summary>
	CellMark_Dice,

	/// <summary>
	/// Indicates cell phantom dice mark.
	/// </summary>
	CellMark_PhantomDice,

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
	/// Indicates cell battenburg mark.
	/// </summary>
	CellMark_Battenburg,

	/// <summary>
	/// Indicates cell seven segment display mark.
	/// </summary>
	CellMark_SevenSegmentDisplay,

	/// <summary>
	/// Indicates cell border-aligned triangle mark.
	/// </summary>
	CellMark_BorderAlignedTriangle,

	/// <summary>
	/// Indicates cell border-aligned arrow mark.
	/// </summary>
	CellMark_BorderAlignedArrow,
	#endregion

	//
	// Candidate marks
	//
	#region Candidate marks
	/// <summary>
	/// Indicates circle candidate mark.
	/// </summary>
	CandidateMark_Circle,

	/// <summary>
	/// Indicates candidate cross mark.
	/// </summary>
	CandidateMark_Cross,
	#endregion

	//
	// Text
	//
	#region Text
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
	#endregion

	//
	// Cell text marks
	//
	#region Cell text marks
	/// <summary>
	/// Indicates cell question mark.
	/// </summary>
	CellMark_QuestionText,

	/// <summary>
	/// Indicates cell exclamation mark.
	/// </summary>
	CellMark_ExclamationText,

	/// <summary>
	/// Indicates cell suit text mark.
	/// </summary>
	CellMark_SuitText,

	/// <summary>
	/// Indicates cell emoji mark.
	/// </summary>
	CellMark_EmojiText,

	/// <summary>
	/// Indicates cell zodiac emoji mark.
	/// </summary>
	CellMark_ZodiacEmojiText,

	/// <summary>
	/// Indicates cell arrow text mark.
	/// </summary>
	CellMark_ArrowText,

	/// <summary>
	/// Indicates arithmetic operator text mark.
	/// </summary>
	CellMark_ArithmeticOperatorText,

	/// <summary>
	/// Indicates bitwise operator text mark.
	/// </summary>
	CellMark_BitwiseOperatorText,

	/// <summary>
	/// Indicates comparison operator text mark.
	/// </summary>
	CellMark_ComparisonOperatorText,

	/// <summary>
	/// Indicates cell border-aligned text mark.
	/// </summary>
	CellMark_BorderAlignedText,
	#endregion

	//
	// Cell pair marks
	//
	#region Cell pair marks
	/// <summary>
	/// Indicates bridge line cell pair mark.
	/// </summary>
	CellPairMark_BridgeLine,
	#endregion

	//
	// Cell group marks
	//
	#region Cell group marks
	/// <summary>
	/// Indicates thermometer cell group mark.
	/// </summary>
	CellGroupMark_Thermometer,

	/// <summary>
	/// Indicates capsule with arrow line cell group mark.
	/// </summary>
	CellGroupMark_CapsuleWithArrowLine,

	/// <summary>
	/// Indicates cell trail cell group mark.
	/// </summary>
	CellGroupMark_CellTrail,

	/// <summary>
	/// Indicates killer cage cell group mark.
	/// </summary>
	CellGroupMark_KillerCage,
	#endregion

	//
	// Lines
	//
	#region Lines
	/// <summary>
	/// Indicates template line strokes.
	/// </summary>
	TemplateLine,

	/// <summary>
	/// Indicates variant line strokes.
	/// </summary>
	VariantLine,
	#endregion
}
