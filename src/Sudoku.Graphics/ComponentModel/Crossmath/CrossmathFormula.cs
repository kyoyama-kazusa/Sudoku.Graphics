namespace Sudoku.ComponentModel.Crossmath;

/// <summary>
/// Represents a crossmath formula.
/// </summary>
public sealed record CrossmathFormula : IEqualityOperators<CrossmathFormula, CrossmathFormula, bool>
{
	/// <summary>
	/// Indicates the value sequence that will be filled into the cells.
	/// </summary>
	public required string?[] ValueSequence { get; init; }

	/// <summary>
	/// Indicates the cell as start.
	/// </summary>
	public required Absolute Cell { get; init; }

	/// <summary>
	/// Indicates the number of cells occupied.
	/// </summary>
	public Relative CellsCount => ValueSequence.Length;

	/// <summary>
	/// Indicates expanding direction of formula.
	/// </summary>
	public required Direction ExpandingDirection { get; init; }


	/// <inheritdoc/>
	public override string ToString()
	{
		var formulaString = string.Concat(from value in ValueSequence select value ?? "?");
		return $"@{Cell}, {formulaString}, {ExpandingDirection}";
	}


	/// <summary>
	/// Creates a <see cref="CrossmathTemplate"/> instance via the specified formulae.
	/// </summary>
	/// <param name="formulae">The formulae to be loaded.</param>
	/// <param name="mapper">The point mapper.</param>
	/// <param name="thickLineWidth">Thick line width.</param>
	/// <param name="thinLineWidth">Thin line width.</param>
	/// <param name="thickLineColor">Thick line color.</param>
	/// <param name="thinLineColor">Thin line color.</param>
	/// <returns>The grid template.</returns>
	public static CrossmathTemplate CreateTemplate(
		CrossmathFormula[] formulae,
		PointMapper mapper,
		Scale thickLineWidth,
		Scale thinLineWidth,
		SerializableColor thickLineColor,
		SerializableColor thinLineColor
	) => new(formulae, mapper)
	{
		ThickLineWidth = thickLineWidth,
		ThinLineWidth = thinLineWidth,
		ThickLineColor = thickLineColor,
		ThinLineColor = thinLineColor
	};

	/// <summary>
	/// Creates a list of <see cref="GivenTextItem"/> instances via the specified formulae.
	/// </summary>
	/// <param name="formulae">The formulae to be loaded.</param>
	/// <param name="mapper">The point mapper.</param>
	/// <param name="fontName">The font name.</param>
	/// <param name="fontSizeScale">The font size scale.</param>
	/// <param name="color">The color.</param>
	/// <returns>A list of <see cref="GivenTextItem"/> instances.</returns>
	public static GivenTextItem[] CreateItems(
		CrossmathFormula[] formulae,
		PointMapper mapper,
		string fontName,
		Scale fontSizeScale,
		SerializableColor color
	)
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
