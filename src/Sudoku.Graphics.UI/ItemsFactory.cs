using TetrisTetromino = Sudoku.Graphics.Items.CellMarks.Tetromino;

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

	public static CellTetrominoMarkItem Tetromino(Absolute cell, Tetromino piece, TetrominoRotationType rotationType)
	{
		var useSrsColors = ResolveProperty(() => App.UserPreferences.UseSrsPredefinedTetrominoFillColors);
		return new()
		{
			Cell = cell,
			Piece = piece,
			RotationType = rotationType,
			CornerRadiusScale = ResolveProperty(() => App.UserPreferences.CellTetrominoCornerRadiusScale),
			StrokeWidthScale = ResolveProperty(() => App.UserPreferences.CellTetrominoStrokeWidthScale),
			StrokeColor = useSrsColors ? SKColors.Transparent : Inherited.ResolveProperty<SerializableColor>(() => App.UserPreferences.CellTetrominoLineColor),
			FillColor = useSrsColors
				? piece switch
				{
					TetrisTetromino.I => SKColors.Tetrimino_I,
					TetrisTetromino.O => SKColors.Tetrimino_O,
					TetrisTetromino.T => SKColors.Tetrimino_T,
					TetrisTetromino.J => SKColors.Tetrimino_J,
					TetrisTetromino.L => SKColors.Tetrimino_L,
					TetrisTetromino.S => SKColors.Tetrimino_S,
					TetrisTetromino.Z => SKColors.Tetrimino_Z,
					_ => throw new ArgumentOutOfRangeException(nameof(piece))
				}
				: Inherited.ResolveProperty<SerializableColor>(() => App.UserPreferences.CellTetrominoFillColor),
			SizeScale = ResolveProperty(() => App.UserPreferences.CellTetrominoSmallBlockSizeScale)
		};
	}

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
			StrokeColor = ResolveProperty(() => App.UserPreferences.CellCrossStrokeColor)
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
			StrokeColor = ResolveProperty(() => App.UserPreferences.CandidateCrossStrokeColor)
		};

	public static CellQuestionMarkItem CellQuestion(Absolute cell)
		=> new()
		{
			Cell = cell,
			TextFontName = ResolveProperty(() => App.UserPreferences.QuestionFontName),
			SizeScale = ResolveProperty(() => App.UserPreferences.QuestionFontSizeScale),
			FillColor = ResolveProperty(() => App.UserPreferences.QuestionFontColor),
			FontWidth = ResolveProperty(() => App.UserPreferences.QuestionFontWidth),
			FontSlant = ResolveProperty(() => App.UserPreferences.QuestionFontSlant),
			FontWeight = ResolveProperty(() => App.UserPreferences.QuestionFontWeight)
		};

	public static CellExclamationMarkItem CellExclamation(Absolute cell)
		=> new()
		{
			Cell = cell,
			TextFontName = ResolveProperty(() => App.UserPreferences.ExclamationFontName),
			SizeScale = ResolveProperty(() => App.UserPreferences.ExclamationFontSizeScale),
			FillColor = ResolveProperty(() => App.UserPreferences.ExclamationFontColor),
			FontWidth = ResolveProperty(() => App.UserPreferences.ExclamationFontWidth),
			FontSlant = ResolveProperty(() => App.UserPreferences.ExclamationFontSlant),
			FontWeight = ResolveProperty(() => App.UserPreferences.ExclamationFontWeight)
		};

	public static CellSuitTextMarkItem CellSuit(Absolute cell, Suit suit)
	{
		var usePredefinedColor = ResolveProperty(() => App.UserPreferences.UsePredefinedSuitsFillColors);
		return new()
		{
			Cell = cell,
			Suit = suit,
			TextFontName = ResolveProperty(() => App.UserPreferences.SuitFontName),
			SizeScale = ResolveProperty(() => App.UserPreferences.SuitFontSizeScale),
			FillColor = usePredefinedColor ? suit.FillColor : ResolveProperty(() => App.UserPreferences.SuitFontColor),
			FontWidth = ResolveProperty(() => App.UserPreferences.SuitFontWidth),
			FontSlant = ResolveProperty(() => App.UserPreferences.SuitFontSlant),
			FontWeight = ResolveProperty(() => App.UserPreferences.SuitFontWeight)
		};
	}

	public static CellZodiacEmojiMarkItem CellZodiac(Absolute cell, Zodiac zodiac)
		=> new()
		{
			Cell = cell,
			Zodiac = zodiac,
			TextFontName = "Segoe UI Emoji",
			SizeScale = ResolveProperty(() => App.UserPreferences.ZodiacFontSizeScale),
			FillColor = SKColors.Black,
			FontWidth = SKFontStyleWidth.Normal,
			FontSlant = SKFontStyleSlant.Upright,
			FontWeight = SKFontStyleWeight.Medium
		};

	public static CellArrowTextMarkItem CellArrowText(Absolute cell, Direction8 direction)
		=> new()
		{
			Cell = cell,
			Direction = direction,
			TextFontName = ResolveProperty(() => App.UserPreferences.CellArrowTextFontName),
			SizeScale = ResolveProperty(() => App.UserPreferences.CellArrowTextFontSizeScale),
			FillColor = ResolveProperty(() => App.UserPreferences.CellArrowTextFontColor),
			FontWidth = ResolveProperty(() => App.UserPreferences.CellArrowTextFontWidth),
			FontSlant = ResolveProperty(() => App.UserPreferences.CellArrowTextFontSlant),
			FontWeight = ResolveProperty(() => App.UserPreferences.CellArrowTextFontWeight)
		};

	public static CellBorderAlignedDigitTextMarkItem CellBorderAlignedDigit(Absolute cell, int digit, Alignment alignment)
		=> new()
		{
			Cell = cell,
			Alignment = alignment,
			Digit = digit,
			TextFontName = ResolveProperty(() => App.UserPreferences.CellBorderAlignedDigitFontName),
			SizeScale = ResolveProperty(() => App.UserPreferences.CellBorderAlignedDigitFontSizeScale),
			FillColor = ResolveProperty(() => App.UserPreferences.CellBorderAlignedDigitFontColor),
			FontWidth = ResolveProperty(() => App.UserPreferences.CellBorderAlignedDigitFontWidth),
			FontSlant = ResolveProperty(() => App.UserPreferences.CellBorderAlignedDigitFontSlant),
			FontWeight = ResolveProperty(() => App.UserPreferences.CellBorderAlignedDigitFontWeight)
		};

	public static CellPairRomanNumeralTextMarkItem CellPairRomanNumeral(Absolute cell1, Absolute cell2, int value)
		=> new()
		{
			Cell1 = cell1,
			Cell2 = cell2,
			Value = value,
			FontName = ResolveProperty(() => App.UserPreferences.CellRomanNumeralFontName),
			FontSizeScale = ResolveProperty(() => App.UserPreferences.CellRomanNumeralFontSizeScale),
			FontColor = ResolveProperty(() => App.UserPreferences.CellRomanNumeralFontColor),
			FillColor = ResolveProperty(() => App.UserPreferences.CellRomanNumeralCoverColor),
			Padding = ResolveProperty(() => App.UserPreferences.CellRomanNumeralPadding),
			FontWidth = ResolveProperty(() => App.UserPreferences.CellRomanNumeralFontWidth),
			FontWeight = ResolveProperty(() => App.UserPreferences.CellRomanNumeralFontWeight),
			FontSlant = ResolveProperty(() => App.UserPreferences.CellRomanNumeralFontSlant)
		};

	public static CellPairNumberTextMarkItem CellPairNumber(Absolute cell1, Absolute cell2, int value)
		=> new()
		{
			Cell1 = cell1,
			Cell2 = cell2,
			Value = value,
			FontName = ResolveProperty(() => App.UserPreferences.CellNumberFontName),
			FontSizeScale = ResolveProperty(() => App.UserPreferences.CellNumberFontSizeScale),
			FontColor = ResolveProperty(() => App.UserPreferences.CellNumberFontColor),
			FillColor = ResolveProperty(() => App.UserPreferences.CellNumberCoverColor),
			Padding = ResolveProperty(() => App.UserPreferences.CellNumberPadding),
			FontWidth = ResolveProperty(() => App.UserPreferences.CellNumberFontWidth),
			FontWeight = ResolveProperty(() => App.UserPreferences.CellNumberFontWeight),
			FontSlant = ResolveProperty(() => App.UserPreferences.CellNumberFontSlant)
		};

	public static CellPairArrowTextMarkItem CellPairArrow(Absolute cell1, Absolute cell2, Direction8 direction)
		=> new()
		{
			Cell1 = cell1,
			Cell2 = cell2,
			Direction = direction,
			FontName = ResolveProperty(() => App.UserPreferences.CellPairArrowFontName),
			FontSizeScale = ResolveProperty(() => App.UserPreferences.CellPairArrowFontSizeScale),
			FontColor = ResolveProperty(() => App.UserPreferences.CellPairArrowFontColor),
			FillColor = ResolveProperty(() => App.UserPreferences.CellPairArrowCoverColor),
			Padding = ResolveProperty(() => App.UserPreferences.CellPairArrowPadding),
			FontWidth = ResolveProperty(() => App.UserPreferences.CellPairArrowFontWidth),
			FontWeight = ResolveProperty(() => App.UserPreferences.CellPairArrowFontWeight),
			FontSlant = ResolveProperty(() => App.UserPreferences.CellPairArrowFontSlant)
		};

	public static CellPairComparisonOperatorTextMarkItem CellPairComparisonOperator(Absolute cell1, Absolute cell2, ComparisonOperator @operator)
		=> new()
		{
			Cell1 = cell1,
			Cell2 = cell2,
			Operator = @operator,
			FontName = ResolveProperty(() => App.UserPreferences.CellPairComparisonOperatorFontName),
			FontSizeScale = ResolveProperty(() => App.UserPreferences.CellPairComparisonOperatorFontSizeScale),
			FontColor = ResolveProperty(() => App.UserPreferences.CellPairComparisonOperatorFontColor),
			FillColor = ResolveProperty(() => App.UserPreferences.CellPairComparisonOperatorCoverColor),
			Padding = ResolveProperty(() => App.UserPreferences.CellPairComparisonOperatorPadding),
			FontWidth = ResolveProperty(() => App.UserPreferences.CellPairComparisonOperatorFontWidth),
			FontWeight = ResolveProperty(() => App.UserPreferences.CellPairComparisonOperatorFontWeight),
			FontSlant = ResolveProperty(() => App.UserPreferences.CellPairComparisonOperatorFontSlant),
			RotationDegreesLookup = new Dictionary<Direction8, float>() { { Direction8.Up, 90 }, { Direction8.Down, 90 } }
		};

	public static CellPairRawTextMarkItem CellPairRawText(Absolute cell1, Absolute cell2, string text)
		=> new()
		{
			Cell1 = cell1,
			Cell2 = cell2,
			Text = text,
			FontName = ResolveProperty(() => App.UserPreferences.CellPairRawTextFontName),
			FontSizeScale = ResolveProperty(() => App.UserPreferences.CellPairRawTextFontSizeScale),
			FontColor = ResolveProperty(() => App.UserPreferences.CellPairRawTextFontColor),
			FillColor = ResolveProperty(() => App.UserPreferences.CellPairRawTextCoverColor),
			Padding = ResolveProperty(() => App.UserPreferences.CellPairRawTextPadding),
			FontWidth = ResolveProperty(() => App.UserPreferences.CellPairRawTextFontWidth),
			FontWeight = ResolveProperty(() => App.UserPreferences.CellPairRawTextFontWeight),
			FontSlant = ResolveProperty(() => App.UserPreferences.CellPairRawTextFontSlant)
		};

	public static CellPairBridgeLineMarkItem CellPairBridgeLine(Absolute cell1, Absolute cell2, int linesCount)
		=> new()
		{
			Cell1 = cell1,
			Cell2 = cell2,
			LinesCount = linesCount,
			FillColor = ResolveProperty(() => App.UserPreferences.CellPairBridgeLinesCircleFillColor),
			CircleDiameterScale = ResolveProperty(() => App.UserPreferences.CellPairBridgeLinesCircleDiameterScale),
			StrokeColor = ResolveProperty(() => App.UserPreferences.CellPairBridgeLinesStrokeColor),
			LinesMaxGapScale = ResolveProperty(() => App.UserPreferences.CellPairBridgeLinesMaxGapScale),
			StrokeWidthScale = ResolveProperty(() => App.UserPreferences.CellPairBridgeLinesStrokeWidthScale)
		};

	public static CellPairBarMarkItem CellPairBar(Absolute cell1, Absolute cell2)
		=> new()
		{
			Cell1 = cell1,
			Cell2 = cell2,
			CornerRadiusScale = ResolveProperty(() => App.UserPreferences.CellPairBarCornerRadiusScale),
			ShortSideScale = ResolveProperty(() => App.UserPreferences.CellPairBarShortSideScale),
			LongSideScale = ResolveProperty(() => App.UserPreferences.CellPairBarLongSideScale),
			FillColor = ResolveProperty(() => App.UserPreferences.CellPairBarFillColor),
			StrokeColor = ResolveProperty(() => App.UserPreferences.CellPairBarStrokeColor),
			StrokeWidthScale = ResolveProperty(() => App.UserPreferences.CellPairBarStrokeWidthScale)
		};

	public static CellPairConnectionLineMarkItem CellPairConnectionLine(Absolute cell1, Absolute cell2)
		=> new()
		{
			Cell1 = cell1,
			Cell2 = cell2,
			StrokeWidthScale = ResolveProperty(() => App.UserPreferences.CellPairConnectionLineStrokeWidthScale),
			StrokeColor = ResolveProperty(() => App.UserPreferences.CellPairConnectionLineStrokeColor),
			SizeScale = ResolveProperty(() => App.UserPreferences.CellPairConnectionLineSizeScale)
		};

	public static CellPairKropkiMarkItem CellPairKropki(Absolute cell1, Absolute cell2, bool isSolid)
		=> new()
		{
			Cell1 = cell1,
			Cell2 = cell2,
			IsSolid = isSolid,
			SizeScale = ResolveProperty(() => App.UserPreferences.CellPairKropkiSizeScale),
			StrokeWidthScale = ResolveProperty(() => App.UserPreferences.CellPairKropkiStrokeWidthScale),
			StrokeColor = ResolveProperty(() => App.UserPreferences.CellPairKropkiStrokeColor),
			FillColor = ResolveProperty(() => App.UserPreferences.CellPairKropkiFillColor)
		};

	public static CellPairKropkiSquareMarkItem CellPairKropkiSquare(Absolute cell1, Absolute cell2, bool isSolid)
		=> new()
		{
			Cell1 = cell1,
			Cell2 = cell2,
			IsSolid = isSolid,
			SizeScale = ResolveProperty(() => App.UserPreferences.CellPairKropkiSquareSizeScale),
			StrokeWidthScale = ResolveProperty(() => App.UserPreferences.CellPairKropkiSquareStrokeWidthScale),
			StrokeColor = ResolveProperty(() => App.UserPreferences.CellPairKropkiSquareStrokeColor),
			FillColor = ResolveProperty(() => App.UserPreferences.CellPairKropkiSquareFillColor),
			CornerRadiusScale = ResolveProperty(() => App.UserPreferences.CellPairKropkiSquareCornerRadiusScale)
		};

	public static CellPairKropkiTriangleMarkItem CellPairKropkiTriangle(Absolute cell1, Absolute cell2, bool isSolid)
		=> new()
		{
			Cell1 = cell1,
			Cell2 = cell2,
			IsSolid = isSolid,
			SizeScale = ResolveProperty(() => App.UserPreferences.CellPairKropkiTriangleSizeScale),
			StrokeWidthScale = ResolveProperty(() => App.UserPreferences.CellPairKropkiTriangleStrokeWidthScale),
			StrokeColor = ResolveProperty(() => App.UserPreferences.CellPairKropkiTriangleStrokeColor),
			FillColor = ResolveProperty(() => App.UserPreferences.CellPairKropkiTriangleFillColor)
		};

	public static CellPairBattenburgMarkItem CellPairBattenburg(Absolute cell1, Absolute cell2)
		=> new()
		{
			Cell1 = cell1,
			Cell2 = cell2,
			Color1 = ResolveProperty(() => App.UserPreferences.CellPairBattenburgColor1),
			Color2 = ResolveProperty(() => App.UserPreferences.CellPairBattenburgColor2),
			UniformCornerRadiusScale = ResolveProperty(() => App.UserPreferences.CellPairBattenburgUniformCornerRadiusScale),
			SizeScale = ResolveProperty(() => App.UserPreferences.CellPairBattenburgSizeScale),
			StrokeWidthScale = ResolveProperty(() => App.UserPreferences.CellPairBattenburgStrokeWidthScale),
			StrokeColor = ResolveProperty(() => App.UserPreferences.CellPairBattenburgStrokeColor)
		};
}
