namespace Sudoku.ComponentModel;

/// <summary>
/// Represents logical size of a <see cref="LineTemplate"/> instance.
/// </summary>
/// <param name="rowsCount"><inheritdoc cref="RowsCount" path="/summary"/></param>
/// <param name="columnsCount"><inheritdoc cref="ColumnsCount" path="/summary"/></param>
/// <param name="vector"><inheritdoc cref="Vector" path="/summary"/></param>
/// <seealso cref="LineTemplate"/>
[method: SetsRequiredMembers]
public sealed class LineTemplateSize(Absolute rowsCount, Absolute columnsCount, DirectionVector vector)
{
	/// <summary>
	/// Indicates the number of rows in main sudoku grid.
	/// </summary>
	public required Absolute RowsCount { get; init; } = rowsCount;

	/// <summary>
	/// Indicates the number of columns in main sudoku grid.
	/// </summary>
	public required Absolute ColumnsCount { get; init; } = columnsCount;

	/// <summary>
	/// Indicates the number of rows. The number of rows should be an absolute value,
	/// including reserved regions (used by drawing outside-like puzzles).
	/// </summary>
	public Absolute AbsoluteRowsCount => RowsCount + Vector.Up + Vector.Down;

	/// <summary>
	/// Indiactes the number of columns. The number of columns should be an absolute value,
	/// including reserved regions (used by drawing outside-like puzzles).
	/// </summary>
	public Absolute AbsoluteColumnsCount => ColumnsCount + Vector.Left + Vector.Right;

	/// <summary>
	/// Indicates empty cells count reserved to be empty.
	/// </summary>
	public required DirectionVector Vector { get; init; } = vector;


	/// <summary>
	/// Creates a new <see cref="LineTemplateSize"/> instance via the specified offset of the current instance.
	/// </summary>
	/// <param name="rowsCount">The number of rows.</param>
	/// <param name="columnsCount">The number of columns.</param>
	/// <returns>A new <see cref="LineTemplateSize"/> instance.</returns>
	public LineTemplateSize WithOffset(Relative rowsCount, Relative columnsCount)
		=> new(RowsCount, ColumnsCount, Vector + new DirectionVector(columnsCount, 0, rowsCount, 0));
}
