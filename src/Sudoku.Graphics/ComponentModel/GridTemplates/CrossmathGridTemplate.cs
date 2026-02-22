namespace Sudoku.ComponentModel.GridTemplates;

/// <summary>
/// Represents crossmath grid template.
/// </summary>
public sealed class CrossmathGridTemplate : SpecifiedGridTemplate
{
	/// <summary>
	/// Represents a list of formulae. Property <see cref="CrossmathFormula.ValueSequence"/> won't be used here;
	/// this property requires property <see cref="GridTemplate.Mapper"/> having been already initialized.
	/// </summary>
	/// <exception cref="InvalidOperationException">
	/// Throws when property <see cref="GridTemplate.Mapper"/> isn't initialized.
	/// </exception>
	/// <seealso cref="CrossmathFormula.ValueSequence"/>
	/// <seealso cref="GridTemplate.Mapper"/>
	public required CrossmathFormula[] Formulae
	{
		get;

		init
		{
			if (Mapper is null)
			{
				throw new InvalidOperationException($"You should firstly initialize for property '{nameof(Mapper)}'.");
			}

			field = value;

			var thinBorders = new List<LineSegment>();
			foreach (var formula in value)
			{
				var startCell = formula.Cell;
				for (var i = 0; i < formula.CellsCount; i++)
				{
					var nextCell = i == 0 ? formula.Cell : Mapper.GetAdjacentAbsoluteCellWith(startCell, formula.ExpandingDirection, false);
					thinBorders.Add(new(nextCell, Direction.Up | Direction.Down | Direction.Left | Direction.Right));
					startCell = nextCell;
				}
			}
			ThinLineSegments = [.. thinBorders];
		}
	}
}
