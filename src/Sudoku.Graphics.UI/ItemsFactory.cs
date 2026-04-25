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
			FontName = ResolveProperty(() => App.UserPreferences.GivenFontName),
			FontSizeScale = ResolveProperty(() => App.UserPreferences.GivenFontSizeScale),
			Text = digit.ToString(),
			Color = ResolveProperty(() => App.UserPreferences.GivenTextColor),
			FontWidth = ResolveProperty(() => App.UserPreferences.GivenFontWidth),
			FontSlant = ResolveProperty(() => App.UserPreferences.GivenFontSlant),
			FontWeight = ResolveProperty(() => App.UserPreferences.GivenFontWeight)
		};

	public static ModifiableTextItem Modifiable(Absolute cell, int digit)
		=> new()
		{
			TemplateIndex = 0,
			Cell = cell,
			FontName = ResolveProperty(() => App.UserPreferences.ModifiableFontName),
			FontSizeScale = ResolveProperty(() => App.UserPreferences.ModifiableFontSizeScale),
			Text = digit.ToString(),
			Color = ResolveProperty(() => App.UserPreferences.ModifiableTextColor),
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
			CandidatePosition = new(cell, subgridSize, digit - 1),
			FontName = ResolveProperty(() => App.UserPreferences.GivenFontName),
			FontSizeScale = ResolveProperty(() => App.UserPreferences.GivenFontSizeScale),
			Text = digit.ToString(),
			Color = ResolveProperty(() => App.UserPreferences.GivenTextColor),
			FontWidth = ResolveProperty(() => App.UserPreferences.GivenFontWidth),
			FontSlant = ResolveProperty(() => App.UserPreferences.GivenFontSlant),
			FontWeight = ResolveProperty(() => App.UserPreferences.GivenFontWeight)
		};
}
