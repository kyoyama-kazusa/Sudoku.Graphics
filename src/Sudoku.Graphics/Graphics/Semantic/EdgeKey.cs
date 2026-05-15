namespace Sudoku.Graphics.Semantic;

/// <summary>
/// Represents a key of an edge (line segment).
/// </summary>
/// <param name="Cell1Row">Indicates the value Y1.</param>
/// <param name="Cell1Column">Indicates the value X1.</param>
/// <param name="Cell2Row">Indicates the value Y2.</param>
/// <param name="Cell2Column">Indicates the value X2.</param>
public readonly record struct EdgeKey(Absolute Cell1Row, Absolute Cell1Column, Absolute Cell2Row, Absolute Cell2Column) :
	IEqualityOperators<EdgeKey, EdgeKey, bool>
{
	/// <summary>
	/// Creates an <see cref="EdgeKey"/> instance via the specified cell and direction.
	/// </summary>
	/// <param name="cell">The cell.</param>
	/// <param name="direction">The direction.</param>
	/// <param name="mapper">The point mapper instance.</param>
	/// <returns>An <see cref="EdgeKey"/> instance.</returns>
	/// <exception cref="ArgumentOutOfRangeException">Throws when <paramref name="direction"/> is not defined.</exception>
	public static EdgeKey Create(Absolute cell, Direction4 direction, PointMapper mapper)
	{
		var row = cell / mapper.AbsoluteColumnsCount;
		var column = cell % mapper.AbsoluteColumnsCount;
#pragma warning disable format
		var (row1, column1, row2, column2) = direction switch
		{
			Direction4.Up    => (row    , column    , row    , column + 1),
			Direction4.Right => (row    , column + 1, row + 1, column + 1),
			Direction4.Down  => (row + 1, column    , row + 1, column + 1),
			Direction4.Left  => (row    , column    , row + 1, column    ),
			_                => throw new ArgumentOutOfRangeException(nameof(direction))
		};
#pragma warning restore format
		if (column1 > column2 || column1 == column2 && row1 > row2)
		{
			(column1, row1, column2, row2) = (column2, row2, column1, row1);
		}
		return new(row1, column1, row2, column2);
	}
}
