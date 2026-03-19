namespace Sudoku.Graphics;

/// <summary>
/// Represents an item to be drawn. The item can be anything the canvas can draw - cell background, canvas background,
/// candidate highlight, grid lines, and other basic items to draw.
/// </summary>
[JsonPolymorphic(UnknownDerivedTypeHandling = JsonUnknownDerivedTypeHandling.FailSerialization)]
[JsonDerivedType(typeof(BackgroundFillItem), nameof(BackgroundFillItem))]
[JsonDerivedType(typeof(CandidateCircleMarkItem), nameof(CandidateCircleMarkItem))]
[JsonDerivedType(typeof(CandidateCrossMarkItem), nameof(CandidateCrossMarkItem))]
[JsonDerivedType(typeof(CandidateFillItem), nameof(CandidateFillItem))]
[JsonDerivedType(typeof(CandidateTextItem), nameof(CandidateTextItem))]
[JsonDerivedType(typeof(CellApexCornerTriangleMarkItem), nameof(CellApexCornerTriangleMarkItem))]
[JsonDerivedType(typeof(CellArithmeticOperatorTextMarkItem), nameof(CellArithmeticOperatorTextMarkItem))]
[JsonDerivedType(typeof(CellArrowMarkItem), nameof(CellArrowMarkItem))]
[JsonDerivedType(typeof(CellArrowTextMarkItem), nameof(CellArrowTextMarkItem))]
[JsonDerivedType(typeof(CellArrowTriangleMarkItem), nameof(CellArrowTriangleMarkItem))]
[JsonDerivedType(typeof(CellBattenburgMarkItem), nameof(CellBattenburgMarkItem))]
[JsonDerivedType(typeof(CellBitwiseOperatorTextMarkItem), nameof(CellBitwiseOperatorTextMarkItem))]
[JsonDerivedType(typeof(CellBorderAlignedArrowMarkItem), nameof(CellBorderAlignedArrowMarkItem))]
[JsonDerivedType(typeof(CellBorderAlignedTextMarkItem), nameof(CellBorderAlignedArrowMarkItem))]
[JsonDerivedType(typeof(CellBorderAlignedTriangleMarkItem), nameof(CellBorderAlignedTriangleMarkItem))]
[JsonDerivedType(typeof(CellCircleMarkItem), nameof(CellCircleMarkItem))]
[JsonDerivedType(typeof(CellComparisonOperatorTextMarkItem), nameof(CellComparisonOperatorTextMarkItem))]
[JsonDerivedType(typeof(CellCrossMarkItem), nameof(CellCrossMarkItem))]
[JsonDerivedType(typeof(CellDiamondMarkItem), nameof(CellDiamondMarkItem))]
[JsonDerivedType(typeof(CellDiceMarkItem), nameof(CellDiceMarkItem))]
[JsonDerivedType(typeof(CellEmojiMarkItem), nameof(CellEmojiMarkItem))]
[JsonDerivedType(typeof(CellExclamationMarkItem), nameof(CellExclamationMarkItem))]
[JsonDerivedType(typeof(CellFillItem), nameof(CellFillItem))]
[JsonDerivedType(typeof(CellGroupThermometerMarkItem), nameof(CellGroupThermometerMarkItem))]
[JsonDerivedType(typeof(CellHalveLineMarkItem), nameof(CellHalveLineMarkItem))]
[JsonDerivedType(typeof(CellHexagonMarkItem), nameof(CellHexagonMarkItem))]
[JsonDerivedType(typeof(CellMoonPhaseMarkItem), nameof(CellMoonPhaseMarkItem))]
[JsonDerivedType(typeof(CellPairBridgeLineMarkItem), nameof(CellPairBridgeLineMarkItem))]
[JsonDerivedType(typeof(CellPhantomDiceMarkItem), nameof(CellPhantomDiceMarkItem))]
[JsonDerivedType(typeof(CellPolygonMarkItem), nameof(CellPolygonMarkItem))]
[JsonDerivedType(typeof(CellQuestionMarkItem), nameof(CellQuestionMarkItem))]
[JsonDerivedType(typeof(CellSevenSegmentDisplayMarkItem), nameof(CellSevenSegmentDisplayMarkItem))]
[JsonDerivedType(typeof(CellSquareMarkItem), nameof(CellSquareMarkItem))]
[JsonDerivedType(typeof(CellSuitTextMarkItem), nameof(CellSuitTextMarkItem))]
[JsonDerivedType(typeof(CellSurroundingTrianglesMarkItem), nameof(CellSurroundingTrianglesMarkItem))]
[JsonDerivedType(typeof(CellTetrisMarkItem), nameof(CellTetrisMarkItem))]
[JsonDerivedType(typeof(CellTriangleMarkItem), nameof(CellTriangleMarkItem))]
[JsonDerivedType(typeof(CellZodiacEmojiMarkItem), nameof(CellZodiacEmojiMarkItem))]
[JsonDerivedType(typeof(GivenTextItem), nameof(GivenTextItem))]
[JsonDerivedType(typeof(ModifiableTextItem), nameof(ModifiableTextItem))]
[JsonDerivedType(typeof(TemplateLineItem), nameof(TemplateLineItem))]
[JsonDerivedType(typeof(VariantLineItem), nameof(VariantLineItem))]
public abstract record Item : IEqualityOperators<Item, Item, bool>
{
	/// <summary>
	/// Indicates the type of item.
	/// </summary>
	public abstract ItemType Type { get; }


	/// <summary>
	/// Try to draw the current item onto the specified canvas.
	/// </summary>
	/// <param name="canvas">The canvas to draw.</param>
	protected internal abstract void DrawTo(Canvas canvas);
}
