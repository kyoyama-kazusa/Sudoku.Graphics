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
			{
				yield return ItemType.Fill_Background;
				yield return ItemType.Fill_Cell;
				yield return ItemType.Fill_Candidate;
			}

			// Cell marks
			{
				yield return ItemType.Cell_Tetris;
				yield return ItemType.Cell_Dice;
				yield return ItemType.Cell_PhantomDice;
				yield return ItemType.Cell_SurroundingTriangles;
				yield return ItemType.Cell_Square;
				yield return ItemType.Cell_Circle;
				yield return ItemType.Cell_Polygon;
				yield return ItemType.Cell_Cross;
				yield return ItemType.Cell_MoonPhase;
				yield return ItemType.Cell_ArrowTriangle;
				yield return ItemType.Cell_Arrow;
				yield return ItemType.Cell_ApexCornerTriangle;
				yield return ItemType.Cell_HalveLine;
				yield return ItemType.Cell_Diamond;
				yield return ItemType.Cell_Hexagon;
				yield return ItemType.Cell_Triangle;
				yield return ItemType.Cell_Battenburg;
				yield return ItemType.Cell_SevenSegmentDisplay;
				yield return ItemType.Cell_BorderAlignedTriangle;
				yield return ItemType.Cell_BorderAlignedArrow;
			}

			// Candidate marks
			{
				yield return ItemType.Candidate_Circle;
				yield return ItemType.Candidate_Cross;
			}

			// Text
			{
				yield return ItemType.Text_Given;
				yield return ItemType.Text_Modifiable;
				yield return ItemType.Text_Candidate;
			}

			// Cell text marks
			{
				yield return ItemType.Cell_QuestionText;
				yield return ItemType.Cell_ExclamationText;
				yield return ItemType.Cell_SuitText;
				yield return ItemType.Cell_EmojiText;
				yield return ItemType.Cell_ZodiacEmojiText;
				yield return ItemType.Cell_ArrowText;
				yield return ItemType.Cell_ArithmeticOperatorText;
				yield return ItemType.Cell_BitwiseOperatorText;
				yield return ItemType.Cell_ComparisonOperatorText;
				yield return ItemType.Cell_BorderAlignedText;
			}

			// Cell pair marks (1)
			{
				yield return ItemType.CellPair_BridgeLine;
			}

			// Cell group marks
			{
				yield return ItemType.CellGroup_Thermometer;
				yield return ItemType.CellGroup_CapsuleWithArrowLine;
				yield return ItemType.CellGroup_CellTrail;
				yield return ItemType.CellGroup_KillerCage;
			}

			// Lines
			{
				yield return ItemType.Line_Template;
				yield return ItemType.Line_Variant;
			}

			// Cell pair marks (2)
			{
				yield return ItemType.CellPair_Bar;
				yield return ItemType.CellPair_ConnectionLine;
				yield return ItemType.CellPair_Kropki;
				yield return ItemType.CellPair_KropkiSquare;
				yield return ItemType.CellPair_KropkiTriangle;
				yield return ItemType.CellPair_Battenburg;
			}

			// Cell pair text marks
			{
				yield return ItemType.CellPairText_RomanNumeral;
				yield return ItemType.CellPairText_Number;
				yield return ItemType.CellPairText_Arrow;
				yield return ItemType.CellPairText_Raw;
			}
		}
	}
}
