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

	public static CellTetrisMarkItem Tetris(Absolute cell, Tetromino piece, TetrominoRotationType rotationType)
		=> new()
		{
			TemplateIndex = 0,
			Cell = cell,
			Piece = piece,
			RotationType = rotationType,
			CornerRadiusScale = ResolveProperty(() => App.UserPreferences.TetrominoCornerRadiusScale),
			StrokeWidthScale = ResolveProperty(() => App.UserPreferences.TetrominoStrokeWidthScale),
			StrokeColor = ResolveProperty(() => App.UserPreferences.TetrominoLineColor),
			FillColor = ResolveProperty(() => App.UserPreferences.TetrominoFillColor),
			SizeScale = ResolveProperty(() => App.UserPreferences.TetrominoSmallBlockSizeScale)
		};

	public static CellDiceMarkItem Dice(Absolute cell, int value)
		=> new()
		{
			TemplateIndex = 0,
			Cell = cell,
			Value = value,
			CornerRadiusScale = ResolveProperty(() => App.UserPreferences.DiceCornerRadiusScale),
			StrokeWidthScale = ResolveProperty(() => App.UserPreferences.DiceStrokeWidthScale),
			StrokeColor = ResolveProperty(() => App.UserPreferences.DiceLineColor),
			FillColor = ResolveProperty(() => App.UserPreferences.DiceFillColor),
			SizeScale = ResolveProperty(() => App.UserPreferences.DiceSizeScale)
		};
}
