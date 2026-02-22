namespace Sudoku.ComponentModel.Crossmath;

/// <summary>
/// Provides an easy way to create <see cref="GivenTextItem"/> instances that will be used by rendering a crossmath puzzle.
/// </summary>
public static class CrossmathPuzzleCellTextItemFactory
{
	/// <summary>
	/// Creates a list of <see cref="GivenTextItem"/> instances via the specified formulae.
	/// </summary>
	/// <param name="formulae">The formulae to be loaded.</param>
	/// <param name="mapper">The point mapper.</param>
	/// <param name="fontName">The font name.</param>
	/// <param name="fontSizeScale">The font size scale.</param>
	/// <param name="color">The color.</param>
	/// <returns>A list of <see cref="GivenTextItem"/> instances.</returns>
	public static GivenTextItem[] Create(CrossmathFormula[] formulae, PointMapper mapper, string fontName, Scale fontSizeScale, SerializableColor color)
	{
		var result = new HashSet<GivenTextItem>();
		foreach (var formula in formulae)
		{
			var startCell = formula.Cell;
			for (var i = 0; i < formula.CellsCount; i++)
			{
				var nextCell = i == 0 ? formula.Cell : mapper.GetAdjacentAbsoluteCellWith(startCell, formula.ExpandingDirection, false);
				if (formula.ValueSequence[i] is not { } textValue)
				{
					goto NextElement;
				}

				result.Add(
					new()
					{
						Cell = nextCell,
						FontName = fontName,
						FontSizeScale = fontSizeScale,
						TemplateIndex = 0,
						Text = textValue,
						Color = color
					}
				);

			NextElement:
				startCell = nextCell;
			}
		}
		return [.. result];
	}
}
