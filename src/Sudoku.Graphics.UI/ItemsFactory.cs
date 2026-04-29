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
			TemplateIndex = 0,
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
			TemplateIndex = 0,
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
			TemplateIndex = 0,
			CandidatePosition = new(cell, subgridSize, digit),
			Text = (digit + 1).ToString(),
			FontName = ResolveProperty(() => App.UserPreferences.CandidateFontName),
			FontSizeScale = ResolveProperty(() => App.UserPreferences.CandidateFontSizeScale),
			Color = ResolveProperty(() => App.UserPreferences.CandidateFontColor),
			FontWidth = ResolveProperty(() => App.UserPreferences.CandidateFontWidth),
			FontSlant = ResolveProperty(() => App.UserPreferences.CandidateFontSlant),
			FontWeight = ResolveProperty(() => App.UserPreferences.CandidateFontWeight)
		};

	public static CellTetrisMarkItem Tetris(Absolute cell, Tetromino piece, TetrominoRotationType rotationType, bool isSample = false)
		=> new()
		{
			TemplateIndex = 0,
			Cell = cell,
			Piece = piece,
			RotationType = rotationType,
			CornerRadiusScale = ResolveProperty(() => App.UserPreferences.CellTetrominoCornerRadiusScale),
			StrokeWidthScale = ResolveProperty(() => App.UserPreferences.CellTetrominoStrokeWidthScale),
			StrokeColor = ResolveProperty(() => App.UserPreferences.CellTetrominoLineColor),
			FillColor = ResolveProperty(() => App.UserPreferences.CellTetrominoFillColor),
			SizeScale = isSample ? .3M : ResolveProperty(() => App.UserPreferences.CellTetrominoSmallBlockSizeScale)
		};

	public static CellDiceMarkItem Dice(Absolute cell, int value, bool isSample = false)
		=> new()
		{
			TemplateIndex = 0,
			Cell = cell,
			Value = value,
			CornerRadiusScale = ResolveProperty(() => App.UserPreferences.CellDiceCornerRadiusScale),
			StrokeWidthScale = ResolveProperty(() => App.UserPreferences.CellDiceStrokeWidthScale),
			StrokeColor = ResolveProperty(() => App.UserPreferences.CellDiceLineColor),
			FillColor = ResolveProperty(() => App.UserPreferences.CellDiceFillColor),
			SizeScale = isSample ? 0.45M : ResolveProperty(() => App.UserPreferences.CellDiceSizeScale)
		};

	public static CellPhantomDiceMarkItem PhantomDice(Absolute cell, Relative subgridSize, BitArray states)
		=> new()
		{
			TemplateIndex = 0,
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

	public static CellSurroundingTrianglesMarkItem SurroundingTriangles(Absolute cell, int value, bool isSample = false)
		=> new()
		{
			TemplateIndex = 0,
			Cell = cell,
			TrianglesCount = value,
			TipDistanceScale = ResolveProperty(() => App.UserPreferences.SurroundingTrianglesTipDistanceScale),
			CornerRadiusScale = ResolveProperty(() => App.UserPreferences.SurroundingTrianglesCornerRadiusScale),
			StrokeWidthScale = ResolveProperty(() => App.UserPreferences.SurroundingTrianglesStrokeWidthScale),
			StrokeColor = ResolveProperty(() => App.UserPreferences.SurroundingTrianglesStrokeColor),
			FillColor = ResolveProperty(() => App.UserPreferences.SurroundingTrianglesFillColor),
			SizeScale = isSample ? 0.5M : ResolveProperty(() => App.UserPreferences.SurroundingTrianglesSizeScale)
		};

	public static CellCircleMarkItem Circle(Absolute cell)
		=> new()
		{
			TemplateIndex = 0,
			Cell = cell,
			SizeScale = ResolveProperty(() => App.UserPreferences.CellCircleSizeScale),
			StrokeWidthScale = ResolveProperty(() => App.UserPreferences.CellCircleStrokeWidthScale),
			StrokeColor = ResolveProperty(() => App.UserPreferences.CellCircleStrokeColor),
			FillColor = ResolveProperty(() => App.UserPreferences.CellCircleFillColor)
		};

	public static CellSquareMarkItem Square(Absolute cell)
		=> new()
		{
			TemplateIndex = 0,
			Cell = cell,
			SizeScale = ResolveProperty(() => App.UserPreferences.CellCircleSizeScale),
			StrokeWidthScale = ResolveProperty(() => App.UserPreferences.CellCircleStrokeWidthScale),
			StrokeColor = ResolveProperty(() => App.UserPreferences.CellCircleStrokeColor),
			FillColor = ResolveProperty(() => App.UserPreferences.CellCircleFillColor)
		};

	public static CellCrossMarkItem Cross(Absolute cell)
		=> new()
		{
			TemplateIndex = 0,
			Cell = cell,
			SizeScale = ResolveProperty(() => App.UserPreferences.CellCrossSizeScale),
			StrokeWidthScale = ResolveProperty(() => App.UserPreferences.CellCrossStrokeWidthScale),
			StrokeColor = ResolveProperty(() => App.UserPreferences.CellCrossStrokeColor),
			FillColor = ResolveProperty(() => App.UserPreferences.CellCrossFillColor)
		};

	public static CellPolygonMarkItem Polygon(Absolute cell, int sidesCount, bool isConcave, bool isSample = false)
		=> new()
		{
			TemplateIndex = 0,
			Cell = cell,
			SidesCount = sidesCount,
			DrawConcavePolygon = isConcave,
			ConcaveInnerScale = ResolveProperty(() => App.UserPreferences.CellPolygonConcaveInnerScale),
			SizeScale = isSample ? 1M : ResolveProperty(() => App.UserPreferences.CellPolygonSizeScale),
			StrokeWidthScale = ResolveProperty(() => App.UserPreferences.CellPolygonStrokeWidthScale),
			StrokeColor = ResolveProperty(() => App.UserPreferences.CellPolygonStrokeColor),
			FillColor = ResolveProperty(() => App.UserPreferences.CellPolygonFillColor),
			RotationDegree = 0
		};

	public static CellMoonPhaseMarkItem MoonPhase(Absolute cell, MoonPhase phase, bool isSample = false)
		=> new()
		{
			TemplateIndex = 0,
			Cell = cell,
			Phase = phase,
			SizeScale = isSample ? 1M : ResolveProperty(() => App.UserPreferences.CellMoonPhaseSizeScale),
			StrokeWidthScale = ResolveProperty(() => App.UserPreferences.CellMoonPhaseStrokeWidthScale),
			StrokeColor = ResolveProperty(() => App.UserPreferences.CellMoonPhaseStrokeColor),
			FillColor = ResolveProperty(() => App.UserPreferences.CellMoonPhaseFillColor),
		};

	public static CellTriangleMarkItem CellTriangle(Absolute cell, Direction8 direction, bool isSample = false)
		=> new()
		{
			TemplateIndex = 0,
			Cell = cell,
			Direction = direction,
			SizeScale = isSample ? 1M : ResolveProperty(() => App.UserPreferences.CellTriangleSizeScale),
			StrokeWidthScale = ResolveProperty(() => App.UserPreferences.CellTriangleStrokeWidthScale),
			StrokeColor = ResolveProperty(() => App.UserPreferences.CellTriangleStrokeColor),
			FillColor = ResolveProperty(() => App.UserPreferences.CellTriangleFillColor)
		};
}
