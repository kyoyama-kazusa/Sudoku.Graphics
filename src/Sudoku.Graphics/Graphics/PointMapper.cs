namespace Sudoku.Graphics;

/// <summary>
/// Represents a point mapper instance.
/// </summary>
/// <param name="cellSize"><inheritdoc cref="CellSize" path="/summary"/></param>
/// <param name="margin"><inheritdoc cref="Margin" path="/summary"/></param>
/// <param name="rowsCount"><inheritdoc cref="RowsCount" path="/summary"/></param>
/// <param name="columnsCount"><inheritdoc cref="ColumnsCount" path="/summary"/></param>
/// <param name="vector"><inheritdoc cref="Vector" path="/summary"/></param>
[method: SetsRequiredMembers]
public sealed class PointMapper(
	float cellSize,
	float margin,
	Absolute rowsCount,
	Absolute columnsCount,
	DirectionVector vector
)
{
	/// <summary>
	/// Indicates cell width and height of pixels. By design, cell width is equal to cell height.
	/// </summary>
	public required float CellSize { get; init; } = cellSize;

	/// <summary>
	/// Indicates margin (pixel size of empty spaces between the fact sudoku grid and borders of the picture).
	/// </summary>
	public required float Margin { get; init; } = margin;

	/// <summary>
	/// Indicates the number of rows. The number of rows should be an absolute value,
	/// including reserved regions (used by drawing outside-like puzzles).
	/// </summary>
	public required Relative RowsCount { get; init; } = rowsCount;

	/// <summary>
	/// Indiactes the number of columns. The number of columns should be an absolute value,
	/// including reserved regions (used by drawing outside-like puzzles).
	/// </summary>
	public required Relative ColumnsCount { get; init; } = columnsCount;

	/// <summary>
	/// Indicates the number of rows in absolute grid.
	/// </summary>
	public Absolute AbsoluteRowsCount => RowsCount + Vector.Left + Vector.Right;

	/// <summary>
	/// Indicates the number of columns in absolute grid.
	/// </summary>
	public Absolute AbsoluteColumnsCount => ColumnsCount + Vector.Up + Vector.Down;

	/// <summary>
	/// Indicates empty cells count reserved to be empty.
	/// </summary>
	public required DirectionVector Vector { get; init; } = vector;

	/// <summary>
	/// Indicates grid size <see cref="SKSize"/> instance.
	/// </summary>
	public SKSize GridSize => new(CellSize * ColumnsCount, CellSize * RowsCount);

	/// <summary>
	/// Indicates full canvas size <see cref="SKSize"/> instance, in integer format.
	/// </summary>
	public SKSizeI FullCanvasSize
		=> new((int)(CellSize * AbsoluteColumnsCount + 2 * Margin), (int)(CellSize * AbsoluteRowsCount + 2 * Margin));


	/// <summary>
	/// Projects the specified relative cell index into absolute one.
	/// </summary>
	/// <param name="relativeCellIndex">Relative cell index.</param>
	/// <returns>The result absolute index.</returns>
	public Absolute ToAbsoluteIndex(Relative relativeCellIndex)
	{
		var row = relativeCellIndex / ColumnsCount;
		var column = relativeCellIndex % ColumnsCount;
		var resultRow = row + Vector.Up;
		var resultColumn = column + Vector.Left;
		return resultRow * AbsoluteColumnsCount + resultColumn;
	}

	/// <summary>
	/// Projects the specified absolute cell index into relative one.
	/// </summary>
	/// <param name="absoluteCellIndex">Absolute cell index.</param>
	/// <returns>The result relative index.</returns>
	public Relative ToRelativeIndex(Absolute absoluteCellIndex)
	{
		var absoluteColumnsCount = AbsoluteColumnsCount;
		var row = absoluteCellIndex / absoluteColumnsCount;
		var column = absoluteCellIndex % absoluteColumnsCount;
		var resultRow = row - Vector.Up;
		var resultColumn = column - Vector.Left;
		return resultRow * ColumnsCount + resultColumn;
	}

	/// <summary>
	/// Gets the adjacent cell at the specified direction of the specified absolute cell index.
	/// </summary>
	/// <param name="absoluteCellIndex">Absolute cell index.</param>
	/// <param name="direction">The direction.</param>
	/// <param name="isCyclicChecking">Indicates whether the cell overflown in the relative grid will be included to be checked or not.</param>
	/// <returns>Target cell absolute index.</returns>
	public Absolute GetAdjacentAbsoluteCellWith(Absolute absoluteCellIndex, Direction direction, bool isCyclicChecking)
	{
		var rowsCount = AbsoluteRowsCount;
		var columnsCount = AbsoluteColumnsCount;
		var row = absoluteCellIndex / columnsCount;
		var column = absoluteCellIndex % columnsCount;
		return direction switch
		{
			Direction.Up when row >= 1 => (row - 1) * columnsCount + column,
			Direction.Up when row == 0 && isCyclicChecking => (rowsCount - 1) * columnsCount + column,
			Direction.Down when row < rowsCount => (row + 1) * columnsCount + column,
			Direction.Down when row == rowsCount && isCyclicChecking => 0 * columnsCount + column,
			Direction.Left when column >= 1 => row * columnsCount + column - 1,
			Direction.Left when column == 0 && isCyclicChecking => row * columnsCount + columnsCount - 1,
			Direction.Right when column < columnsCount => row * columnsCount + column + 1,
			Direction.Right when column == columnsCount && isCyclicChecking => row + columnsCount + 0,
			_ => -1
		};
	}

	/// <summary>
	/// Returns the position (point) of the specified corner type of the specified cell.
	/// </summary>
	/// <param name="absoluteCellIndex">Absolute cell index.</param>
	/// <param name="type">The type.</param>
	/// <returns>The point instance that represents the target center position.</returns>
	/// <exception cref="ArgumentOutOfRangeException">
	/// Throws when <paramref name="type"/> is not defined or <see cref="CellCornerType.None"/>.
	/// </exception>
	/// <seealso cref="CellCornerType.None"/>
	public SKPoint GetPoint(Absolute absoluteCellIndex, CellCornerType type)
	{
		var columnsCount = AbsoluteColumnsCount;
		return GetPoint(absoluteCellIndex / columnsCount, absoluteCellIndex % columnsCount, type);
	}

	/// <summary>
	/// Returns the position (point) of the specified corner type of the specified cell.
	/// </summary>
	/// <param name="absoluteRowIndex">Absolute row index.</param>
	/// <param name="absoluteColumnIndex">Absolute column index.</param>
	/// <param name="type">The type.</param>
	/// <returns>The point instance that represents the target center position.</returns>
	/// <exception cref="ArgumentOutOfRangeException">
	/// Throws when <paramref name="type"/> is not defined or <see cref="CellCornerType.None"/>.
	/// </exception>
	/// <seealso cref="CellCornerType.None"/>
	public SKPoint GetPoint(Absolute absoluteRowIndex, Absolute absoluteColumnIndex, CellCornerType type)
	{
		var topLeft = new SKPoint(CellSize * absoluteColumnIndex + Margin, CellSize * absoluteRowIndex + Margin);
		return type switch
		{
			CellCornerType.Center => topLeft - new SKPoint(CellSize / 2, CellSize / 2),
			CellCornerType.TopLeft => topLeft,
			CellCornerType.TopRight => topLeft + new SKPoint(CellSize, 0),
			CellCornerType.BottomLeft => topLeft + new SKPoint(0, CellSize),
			CellCornerType.BottomRight => topLeft + new SKPoint(CellSize, CellSize),
			_ => throw new ArgumentOutOfRangeException(nameof(type))
		};
	}
}
