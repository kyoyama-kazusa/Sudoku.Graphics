namespace Sudoku.Graphics;

/// <summary>
/// Represents a default grid line template.
/// </summary>
/// <param name="rowsCount"><inheritdoc cref="RowsCount" path="/summary"/></param>
/// <param name="columnsCount"><inheritdoc cref="ColumnsCount" path="/summary"/></param>
/// <param name="vector"><inheritdoc cref="Vector" path="/summary"/></param>
/// <param name="mapper"><inheritdoc cref="GridLineTemplate.Mapper" path="/summary"/></param>
public sealed class DefaultGridLineTemplate(PointMapper mapper, int rowsCount, int columnsCount, DirectionVector vector) :
	GridLineTemplate(mapper)
{
	/// <summary>
	/// Indicates the number of rows.
	/// </summary>
	public int RowsCount { get; } = rowsCount;

	/// <summary>
	/// Indicates the number of columns.
	/// </summary>
	public int ColumnsCount { get; } = columnsCount;

	/// <summary>
	/// Indicates the vector as outside blank.
	/// </summary>
	public DirectionVector Vector { get; } = vector;


	/// <inheritdoc/>
	public override void DrawLines(SKCanvas canvas)
	{
		throw new NotImplementedException();
	}
}
