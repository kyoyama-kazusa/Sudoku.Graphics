namespace Sudoku.Graphics.UI;

/// <summary>
/// Provides a factory type that creates <see cref="Item"/> instances onto canvas.
/// </summary>
/// <seealso cref="Item"/>
public static class ItemsFactory
{
	public static GivenTextItem Given(Absolute cell, int digit)
		=> new()
		{
			Cell = cell,
			Text = (digit + 1).ToString(),
			FontName = ResolveProperty(() => App.UserPreferences.GivenFontName),
			FontSizeScale = ResolveProperty(() => App.UserPreferences.GivenFontSizeScale),
			Color = ResolveProperty(() => App.UserPreferences.GivenFontColor),
			FontWidth = ResolveProperty(() => App.UserPreferences.GivenFontWidth),
			FontSlant = ResolveProperty(() => App.UserPreferences.GivenFontSlant),
			FontWeight = ResolveProperty(() => App.UserPreferences.GivenFontWeight)
		};

	public static ModifiableTextItem Modifiable(Absolute cell, int digit)
		=> new()
		{
			Cell = cell,
			Text = (digit + 1).ToString(),
			FontName = ResolveProperty(() => App.UserPreferences.ModifiableFontName),
			FontSizeScale = ResolveProperty(() => App.UserPreferences.ModifiableFontSizeScale),
			Color = ResolveProperty(() => App.UserPreferences.ModifiableFontColor),
			FontWidth = ResolveProperty(() => App.UserPreferences.ModifiableFontWidth),
			FontSlant = ResolveProperty(() => App.UserPreferences.ModifiableFontSlant),
			FontWeight = ResolveProperty(() => App.UserPreferences.ModifiableFontWeight)
		};

	public static IEnumerable<CandidateTextItem> Candidates<T>(Absolute cell, T digits, Relative subgridSize)
		where T : IEnumerable<int>
		=>
		from digit in digits
		select new CandidateTextItem
		{
			CandidatePosition = new(cell, subgridSize, digit),
			Text = (digit + 1).ToString(),
			FontName = ResolveProperty(() => App.UserPreferences.CandidateFontName),
			FontSizeScale = ResolveProperty(() => App.UserPreferences.CandidateFontSizeScale),
			Color = ResolveProperty(() => App.UserPreferences.CandidateFontColor),
			FontWidth = ResolveProperty(() => App.UserPreferences.CandidateFontWidth),
			FontSlant = ResolveProperty(() => App.UserPreferences.CandidateFontSlant),
			FontWeight = ResolveProperty(() => App.UserPreferences.CandidateFontWeight)
		};

	public static CellTetrisMarkItem Tetris(Absolute cell, Tetromino piece, TetrominoRotationType rotationType)
		=> new()
		{
			Cell = cell,
			Piece = piece,
			RotationType = rotationType,
			CornerRadiusScale = ResolveProperty(() => App.UserPreferences.CellTetrominoCornerRadiusScale),
			StrokeWidthScale = ResolveProperty(() => App.UserPreferences.CellTetrominoStrokeWidthScale),
			StrokeColor = ResolveProperty(() => App.UserPreferences.CellTetrominoLineColor),
			FillColor = ResolveProperty(() => App.UserPreferences.CellTetrominoFillColor),
			SizeScale = ResolveProperty(() => App.UserPreferences.CellTetrominoSmallBlockSizeScale)
		};

	public static CellDiceMarkItem Dice(Absolute cell, int value)
		=> new()
		{
			Cell = cell,
			Value = value,
			CornerRadiusScale = ResolveProperty(() => App.UserPreferences.CellDiceCornerRadiusScale),
			StrokeWidthScale = ResolveProperty(() => App.UserPreferences.CellDiceStrokeWidthScale),
			StrokeColor = ResolveProperty(() => App.UserPreferences.CellDiceLineColor),
			FillColor = ResolveProperty(() => App.UserPreferences.CellDiceFillColor),
			SizeScale = ResolveProperty(() => App.UserPreferences.CellDiceSizeScale)
		};

	public static CellPhantomDiceMarkItem PhantomDice(Absolute cell, Relative subgridSize, BitArray states)
		=> new()
		{
			Cell = cell,
			SubgridSize = subgridSize,
			States = states,
			PhantomStrokeWidthScale = ResolveProperty(() => App.UserPreferences.CellPhantomDiceStrokeWidthScale),
			CornerRadiusScale = ResolveProperty(() => App.UserPreferences.CellPhantomDiceCornerRadiusScale),
			StrokeWidthScale = ResolveProperty(() => App.UserPreferences.CellPhantomDiceStrokeWidthScale),
			StrokeColor = ResolveProperty(() => App.UserPreferences.CellPhantomDiceLineColor),
			FillColor = ResolveProperty(() => App.UserPreferences.CellPhantomDiceFillColor),
			SizeScale = ResolveProperty(() => App.UserPreferences.CellPhantomDiceSizeScale)
		};

	public static CellSurroundingTrianglesMarkItem SurroundingTriangles(Absolute cell, int value)
		=> new()
		{
			Cell = cell,
			TrianglesCount = value,
			TipDistanceScale = ResolveProperty(() => App.UserPreferences.SurroundingTrianglesTipDistanceScale),
			CornerRadiusScale = ResolveProperty(() => App.UserPreferences.SurroundingTrianglesCornerRadiusScale),
			StrokeWidthScale = ResolveProperty(() => App.UserPreferences.SurroundingTrianglesStrokeWidthScale),
			StrokeColor = ResolveProperty(() => App.UserPreferences.SurroundingTrianglesStrokeColor),
			FillColor = ResolveProperty(() => App.UserPreferences.SurroundingTrianglesFillColor),
			SizeScale = ResolveProperty(() => App.UserPreferences.SurroundingTrianglesSizeScale)
		};

	public static CellCircleMarkItem Circle(Absolute cell)
		=> new()
		{
			Cell = cell,
			SizeScale = ResolveProperty(() => App.UserPreferences.CellCircleSizeScale),
			StrokeWidthScale = ResolveProperty(() => App.UserPreferences.CellCircleStrokeWidthScale),
			StrokeColor = ResolveProperty(() => App.UserPreferences.CellCircleStrokeColor),
			FillColor = ResolveProperty(() => App.UserPreferences.CellCircleFillColor)
		};

	public static CellSquareMarkItem Square(Absolute cell)
		=> new()
		{
			Cell = cell,
			SizeScale = ResolveProperty(() => App.UserPreferences.CellCircleSizeScale),
			StrokeWidthScale = ResolveProperty(() => App.UserPreferences.CellCircleStrokeWidthScale),
			StrokeColor = ResolveProperty(() => App.UserPreferences.CellCircleStrokeColor),
			FillColor = ResolveProperty(() => App.UserPreferences.CellCircleFillColor)
		};

	public static CellCrossMarkItem Cross(Absolute cell)
		=> new()
		{
			Cell = cell,
			SizeScale = ResolveProperty(() => App.UserPreferences.CellCrossSizeScale),
			StrokeWidthScale = ResolveProperty(() => App.UserPreferences.CellCrossStrokeWidthScale),
			StrokeColor = ResolveProperty(() => App.UserPreferences.CellCrossStrokeColor),
			FillColor = ResolveProperty(() => App.UserPreferences.CellCrossFillColor)
		};

	public static CellDiamondMarkItem Diamond(Absolute cell)
		=> new()
		{
			Cell = cell,
			SizeScale = ResolveProperty(() => App.UserPreferences.CellDiamondSizeScale),
			StrokeWidthScale = ResolveProperty(() => App.UserPreferences.CellDiamondStrokeWidthScale),
			StrokeColor = ResolveProperty(() => App.UserPreferences.CellDiamondStrokeColor),
			FillColor = ResolveProperty(() => App.UserPreferences.CellDiamondFillColor)
		};

	public static CellHexagonMarkItem Hexagon(Absolute cell, Orientation2 orientation)
		=> new()
		{
			Cell = cell,
			Orientation = orientation,
			SizeScale = ResolveProperty(() => App.UserPreferences.CellDiamondSizeScale),
			StrokeWidthScale = ResolveProperty(() => App.UserPreferences.CellDiamondStrokeWidthScale),
			StrokeColor = ResolveProperty(() => App.UserPreferences.CellDiamondStrokeColor),
			FillColor = ResolveProperty(() => App.UserPreferences.CellDiamondFillColor)
		};

	public static CellBattenburgMarkItem Battenburg(Absolute cell)
		=> new()
		{
			Cell = cell,
			Color1 = ResolveProperty(() => App.UserPreferences.CellBattenburgColor1),
			Color2 = ResolveProperty(() => App.UserPreferences.CellBattenburgColor2),
			UniformCornerRadiusScale = ResolveProperty(() => App.UserPreferences.CellBattenburgUniformCornerRadiusScale),
			SizeScale = ResolveProperty(() => App.UserPreferences.CellBattenburgSizeScale),
			StrokeWidthScale = ResolveProperty(() => App.UserPreferences.CellBattenburgStrokeWidthScale),
			StrokeColor = ResolveProperty(() => App.UserPreferences.CellBattenburgStrokeColor)
		};

	public static CellPolygonMarkItem Polygon(Absolute cell, int sidesCount, bool isConcave)
		=> new()
		{
			Cell = cell,
			SidesCount = sidesCount,
			DrawConcavePolygon = isConcave,
			ConcaveInnerScale = ResolveProperty(() => App.UserPreferences.CellPolygonConcaveInnerScale),
			SizeScale = ResolveProperty(() => App.UserPreferences.CellPolygonSizeScale),
			StrokeWidthScale = ResolveProperty(() => App.UserPreferences.CellPolygonStrokeWidthScale),
			StrokeColor = ResolveProperty(() => App.UserPreferences.CellPolygonStrokeColor),
			FillColor = ResolveProperty(() => App.UserPreferences.CellPolygonFillColor),
			RotationDegree = 0
		};

	public static CellMoonPhaseMarkItem MoonPhase(Absolute cell, MoonPhase phase)
		=> new()
		{
			Cell = cell,
			Phase = phase,
			SizeScale = ResolveProperty(() => App.UserPreferences.CellMoonPhaseSizeScale),
			StrokeWidthScale = ResolveProperty(() => App.UserPreferences.CellMoonPhaseStrokeWidthScale),
			StrokeColor = ResolveProperty(() => App.UserPreferences.CellMoonPhaseStrokeColor),
			FillColor = ResolveProperty(() => App.UserPreferences.CellMoonPhaseFillColor)
		};

	public static CellTriangleMarkItem CellTriangle(Absolute cell, Direction8 direction)
		=> new()
		{
			Cell = cell,
			Direction = direction,
			SizeScale = ResolveProperty(() => App.UserPreferences.CellTriangleSizeScale),
			StrokeWidthScale = ResolveProperty(() => App.UserPreferences.CellTriangleStrokeWidthScale),
			StrokeColor = ResolveProperty(() => App.UserPreferences.CellTriangleStrokeColor),
			FillColor = ResolveProperty(() => App.UserPreferences.CellTriangleFillColor)
		};

	public static CellArrowTriangleMarkItem CellArrowTriangle(Absolute cell, Direction8 direction)
		=> new()
		{
			Cell = cell,
			Direction = direction,
			BaseScale = ResolveProperty(() => App.UserPreferences.CellArrowTriangleBaseScale),
			SizeScale = ResolveProperty(() => App.UserPreferences.CellArrowTriangleSizeScale),
			StrokeWidthScale = ResolveProperty(() => App.UserPreferences.CellArrowTriangleStrokeWidthScale),
			StrokeColor = ResolveProperty(() => App.UserPreferences.CellArrowTriangleStrokeColor),
			FillColor = ResolveProperty(() => App.UserPreferences.CellArrowTriangleFillColor)
		};

	public static CellArrowMarkItem CellArrow(Absolute cell, Direction8 direction)
		=> new()
		{
			Cell = cell,
			Direction = direction,
			TriangleWidthScale = ResolveProperty(() => App.UserPreferences.CellArrowTriangleWidthScale),
			TriangleHeightScale = ResolveProperty(() => App.UserPreferences.CellArrowTriangleHeightScale),
			ShaftWidthScale = ResolveProperty(() => App.UserPreferences.CellArrowShaftWidthScale),
			ShaftHeightScale = ResolveProperty(() => App.UserPreferences.CellArrowShaftHeightScale),
			SizeScale = ResolveProperty(() => App.UserPreferences.CellArrowSizeScale),
			StrokeWidthScale = ResolveProperty(() => App.UserPreferences.CellArrowStrokeWidthScale),
			StrokeColor = ResolveProperty(() => App.UserPreferences.CellArrowStrokeColor),
			FillColor = ResolveProperty(() => App.UserPreferences.CellArrowFillColor)
		};

	public static CellApexCornerTriangleMarkItem CellApexCornerTriangle(Absolute cell, Alignment cornerAlignement)
		=> new()
		{
			Cell = cell,
			CornerAlignment = cornerAlignement,
			PaddingScale = ResolveProperty(() => App.UserPreferences.CellApexCornerTrianglePaddingScale),
			SizeScale = ResolveProperty(() => App.UserPreferences.CellApexCornerTriangleSizeScale),
			StrokeWidthScale = ResolveProperty(() => App.UserPreferences.CellApexCornerTriangleStrokeWidthScale),
			StrokeColor = ResolveProperty(() => App.UserPreferences.CellApexCornerTriangleStrokeColor),
			FillColor = ResolveProperty(() => App.UserPreferences.CellApexCornerTriangleFillColor)
		};

	public static CellBisectorLineMarkItem CellBisectorLine(Absolute cell, Orientation4 orientation)
		=> new()
		{
			Cell = cell,
			Orientation = orientation,
			SizeScale = ResolveProperty(() => App.UserPreferences.CellBisectorLineSizeScale),
			StrokeWidthScale = ResolveProperty(() => App.UserPreferences.CellBisectorLineStrokeWidthScale),
			StrokeColor = ResolveProperty(() => App.UserPreferences.CellBisectorLineStrokeColor)
		};

	public static CellSevenSegmentDisplayMarkItem CellSevenSegmentDisplay(Absolute cell, int value)
		=> new()
		{
			Cell = cell,
			Value = value,
			UseSecondaryStyle = ResolveProperty(() => App.UserPreferences.CellSevenSegmentDisplayUseSecondaryDigitStyle),
			ShowPhantomSegments = ResolveProperty(() => App.UserPreferences.CellSevenSegmentDisplayShowPhantomSegments),
			SizeScale = ResolveProperty(() => App.UserPreferences.CellSevenSegmentDisplaySizeScale),
			SegmentRectWidthScale = ResolveProperty(() => App.UserPreferences.CellSevenSegmentDisplaySegmentRectWidthScale),
			SegmentRectHeightScale = ResolveProperty(() => App.UserPreferences.CellSevenSegmentDisplaySegmentRectHeightScale),
			PhantomStrokeWidthScale = ResolveProperty(() => App.UserPreferences.CellSevenSegmentDisplayPhantomStrokeWidthScale),
			PhantomStrokeColor = ResolveProperty(() => App.UserPreferences.CellSevenSegmentDisplayPhantomStrokeColor),
			StrokeWidthScale = ResolveProperty(() => App.UserPreferences.CellSevenSegmentDisplayStrokeWidthScale),
			StrokeColor = ResolveProperty(() => App.UserPreferences.CellSevenSegmentDisplayStrokeColor),
			FillColor = ResolveProperty(() => App.UserPreferences.CellSevenSegmentDisplayFillColor)
		};


	public static CellBorderAlignedTriangleMarkItem CellBorderAlignedTriangle(Absolute cell, Direction4 direction)
		=> new()
		{
			Cell = cell,
			Direction = direction,
			SizeScale = ResolveProperty(() => App.UserPreferences.CellBorderAlignedTriangleSizeScale),
			StrokeWidthScale = ResolveProperty(() => App.UserPreferences.CellBorderAlignedTriangleStrokeWidthScale),
			StrokeColor = ResolveProperty(() => App.UserPreferences.CellBorderAlignedTriangleStrokeColor),
			FillColor = ResolveProperty(() => App.UserPreferences.CellBorderAlignedTriangleFillColor)
		};

	public static CellBorderAlignedArrowMarkItem CellBorderAlignedArrow(Absolute cell, Direction4 direction, RotationDirection rotationDirection)
		=> new()
		{
			Cell = cell,
			Direction = direction,
			RotationDirection = rotationDirection,
			PaddingScale = ResolveProperty(() => App.UserPreferences.CellBorderAlignedArrowPaddingScale),
			SizeScale = ResolveProperty(() => App.UserPreferences.CellBorderAlignedArrowSizeScale),
			StrokeWidthScale = ResolveProperty(() => App.UserPreferences.CellBorderAlignedArrowStrokeWidthScale),
			StrokeColor = ResolveProperty(() => App.UserPreferences.CellBorderAlignedArrowStrokeColor),
			FillColor = ResolveProperty(() => App.UserPreferences.CellBorderAlignedArrowFillColor)
		};

	public static CellLargeDiamondMarkItem CellLargeDiamond(Absolute cell)
		=> new()
		{
			Cell = cell,
			SizeScale = ResolveProperty(() => App.UserPreferences.CellLargeDiamondSizeScale),
			StrokeWidthScale = ResolveProperty(() => App.UserPreferences.CellLargeDiamondStrokeWidthScale),
			StrokeColor = ResolveProperty(() => App.UserPreferences.CellLargeDiamondStrokeColor),
			FillColor = ResolveProperty(() => App.UserPreferences.CellLargeDiamondFillColor)
		};

	public static CellLoopSegmentLineMarkItem CellLoopSegmentLine(Absolute cell, Direction4 directions)
		=> new()
		{
			Cell = cell,
			OccupiedDirections = directions,
			SizeScale = ResolveProperty(() => App.UserPreferences.CellLoopSegmentLineSizeScale),
			StrokeWidthScale = ResolveProperty(() => App.UserPreferences.CellLoopSegmentLineStrokeWidthScale),
			StrokeColor = ResolveProperty(() => App.UserPreferences.CellLoopSegmentLineStrokeColor),
			CornerRadiusScale = ResolveProperty(() => App.UserPreferences.CellLoopSegmentLineCornerRadiusScale)
		};

	public static CandidateCircleMarkItem CandidateCircle(CandidatePosition candidate)
		=> new()
		{
			CandidatePosition = candidate,
			SizeScale = ResolveProperty(() => App.UserPreferences.CandidateCircleSizeScale),
			StrokeWidthScale = ResolveProperty(() => App.UserPreferences.CandidateCircleStrokeWidthScale),
			StrokeColor = ResolveProperty(() => App.UserPreferences.CandidateCircleStrokeColor),
			FillColor = ResolveProperty(() => App.UserPreferences.CandidateCircleFillColor)
		};

	public static CandidateCrossMarkItem CandidateCross(CandidatePosition candidate)
		=> new()
		{
			CandidatePosition = candidate,
			SizeScale = ResolveProperty(() => App.UserPreferences.CandidateCrossSizeScale),
			StrokeWidthScale = ResolveProperty(() => App.UserPreferences.CandidateCrossStrokeWidthScale),
			StrokeColor = ResolveProperty(() => App.UserPreferences.CandidateCrossStrokeColor),
			FillColor = ResolveProperty(() => App.UserPreferences.CandidateCrossFillColor)
		};
}
