namespace Sudoku.Graphics;

public partial class ItemTypeOrdering
{
	/// <summary>
	/// Represents default item type enumeration.
	/// </summary>
	private static class DefaultEnumeration
	{
		/// <summary>
		/// Returns the ordering of default instance.
		/// </summary>
		/// <returns>The item types.</returns>
		public static IEnumerable<ItemType> DefaultEnumerateItemTypes()
		{
			// Fills
			yield return ItemType.Fill_Background;
			yield return ItemType.Fill_Cell;
			yield return ItemType.Fill_Candidate;

			// Cell marks
			yield return ItemType.CellMark_Tetris;
			yield return ItemType.CellMark_Dice;
			yield return ItemType.CellMark_PhantomDice;
			yield return ItemType.CellMark_SurroundingTriangles;
			yield return ItemType.CellMark_Square;
			yield return ItemType.CellMark_Circle;
			yield return ItemType.CellMark_Polygon;
			yield return ItemType.CellMark_Cross;
			yield return ItemType.CellMark_MoonPhase;
			yield return ItemType.CellMark_ArrowTriangle;
			yield return ItemType.CellMark_Arrow;
			yield return ItemType.CellMark_ApexCornerTriangle;
			yield return ItemType.CellMark_HalveLine;
			yield return ItemType.CellMark_Diamond;
			yield return ItemType.CellMark_Hexagon;
			yield return ItemType.CellMark_Triangle;
			yield return ItemType.CellMark_Battenburg;
			yield return ItemType.CellMark_SevenSegmentDisplay;
			yield return ItemType.CellMark_BorderAlignedTriangle;
			yield return ItemType.CellMark_BorderAlignedArrow;

			// Candidate marks
			yield return ItemType.CandidateMark_Circle;
			yield return ItemType.CandidateMark_Cross;

			// Text
			yield return ItemType.GivenText;
			yield return ItemType.ModifiableText;
			yield return ItemType.CandidateText;
			yield return ItemType.CellMark_QuestionText;
			yield return ItemType.CellMark_ExclamationText;
			yield return ItemType.CellMark_SuitText;
			yield return ItemType.CellMark_EmojiText;
			yield return ItemType.CellMark_ZodiacEmojiText;
			yield return ItemType.CellMark_ArrowText;
			yield return ItemType.CellMark_ArithmeticOperatorText;
			yield return ItemType.CellMark_BitwiseOperatorText;
			yield return ItemType.CellMark_ComparisonOperatorText;
			yield return ItemType.CellMark_BorderAlignedText;

			// Cell pair marks
			yield return ItemType.CellPairMark_BridgeLine;

			// Cell group marks
			yield return ItemType.CellGroupMark_Thermometer;
			yield return ItemType.CellGroupMark_CapsuleWithArrowLine;
			yield return ItemType.CellGroupMark_CellTrail;
			yield return ItemType.CellGroupMark_KillerCage;

			// Lines
			yield return ItemType.TemplateLine;
			yield return ItemType.VariantLine;

			yield return ItemType.CellPairMark_Bar;
		}
	}
}
