namespace Sudoku.ComponentModel.GridTemplates;

/// <summary>
/// Represents crossmath grid template.
/// </summary>
public sealed class CrossmathTemplate : SpecifiedTemplate
{
	/// <summary>
	/// Initializes a <see cref="CrossmathTemplate"/> instance, via the formulae.
	/// </summary>
	/// <param name="formulae">Indicates the formulae.</param>
	/// <param name="mapper">The mapper.</param>
	[SetsRequiredMembers]
	public CrossmathTemplate(CrossmathFormula[] formulae, PointMapper mapper) : base(mapper)
	{
		var thinBorders = new List<LineSegment>();
		foreach (var formula in formulae)
		{
			var startCell = formula.Cell;
			for (var i = 0; i < formula.CellsCount; i++)
			{
				var nextCell = i == 0 ? formula.Cell : Mapper.GetAdjacentAbsoluteCellWith(startCell, formula.ExpandingDirection, false);
				thinBorders.Add(new(nextCell, Direction.Up | Direction.Down | Direction.Left | Direction.Right));
				startCell = nextCell;
			}
		}

		Formulae = formulae;
		ThinLineSegments = [.. thinBorders];
	}


	/// <summary>
	/// Represents a list of formulae.
	/// </summary>
	public CrossmathFormula[] Formulae { get; }
}
