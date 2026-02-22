namespace Sudoku.ComponentModel.GridTemplates;

/// <summary>
/// Represents crossmath grid template.
/// </summary>
public sealed class CrossmathGridTemplate : SpecifiedGridTemplate
{
	/// <summary>
	/// Represents a list of formulae.
	/// </summary>
	public required CrossmathFormula[] Formulae
	{
		get;

		init
		{
			field = value;

			var thinBorders = new List<LineSegment>();
			foreach (var formula in value)
			{
				var startCell = formula.Cell;
				for (var i = 0; i < formula.CellsCount; i++)
				{
					thinBorders.Add(new(startCell + i, Direction.Up | Direction.Down | Direction.Left | Direction.Right));
				}
			}
			ThinLineSegments = [.. thinBorders];
		}
	}
}
