namespace Sudoku.Graphics;

/// <summary>
/// Represents a point mapper instance.
/// </summary>
/// <param name="cellSize"><inheritdoc cref="CellWidthAndHeight" path="/summary"/></param>
/// <param name="margin"><inheritdoc cref="Margin" path="/summary"/></param>
/// <param name="rowsCount"><inheritdoc cref="RowsCount" path="/summary"/></param>
/// <param name="columnsCount"><inheritdoc cref="ColumnsCount" path="/summary"/></param>
/// <param name="vector"><inheritdoc cref="Vector" path="/summary"/></param>
[method: SetsRequiredMembers]
public sealed class PointMapper(float cellSize, float margin, int rowsCount, int columnsCount, DirectionVector vector)
{
	/// <summary>
	/// Indicates cell width and height of pixels. By design, cell width is equal to cell height.
	/// </summary>
	public required float CellWidthAndHeight { get; init; } = cellSize;

	/// <summary>
	/// Indicates margin (pixel size of empty spaces between the fact sudoku grid and borders of the picture).
	/// </summary>
	public required float Margin { get; init; } = margin;

	/// <summary>
	/// Indicates the number of rows. The number of rows should be an absolute value,
	/// including reserved regions (used by drawing outside-like puzzles).
	/// </summary>
	public required int RowsCount { get; init; } = rowsCount;

	/// <summary>
	/// Indiactes the number of columns. The number of columns should be an absolute value,
	/// including reserved regions (used by drawing outside-like puzzles).
	/// </summary>
	public required int ColumnsCount { get; init; } = columnsCount;

	/// <summary>
	/// Indicates the number of rows in absolute grid.
	/// </summary>
	public int AbsoluteRowsCount => RowsCount + Vector.Left + Vector.Right;

	/// <summary>
	/// Indicates the number of columns in absolute grid.
	/// </summary>
	public int AbsoluteColumnsCount => ColumnsCount + Vector.Up + Vector.Down;

	/// <summary>
	/// Indicates empty cells count reserved to be empty.
	/// </summary>
	public required DirectionVector Vector { get; init; } = vector;

	/// <summary>
	/// Indicates grid size <see cref="SKRect"/> instance.
	/// </summary>
	public SKRect GridSize => SKRect.Create(CellWidthAndHeight * ColumnsCount, CellWidthAndHeight * RowsCount);

	/// <summary>
	/// Indicates full canvas size <see cref="SKRect"/> instance.
	/// </summary>
	public SKRect FullSize
		=> SKRect.Create(CellWidthAndHeight * AbsoluteColumnsCount + 2 * Margin, CellWidthAndHeight * AbsoluteRowsCount + 2 * Margin);

	/// <summary>
	/// Indicates full canvas size <see cref="SKRect"/> instance, in integer format.
	/// </summary>
	public SKRectI FullSizeInteger => SKRectI.Floor(FullSize);


	/// <summary>
	/// Projects the specified relative cell index into absolute one.
	/// </summary>
	/// <param name="relativeCellIndex">Relative cell index.</param>
	/// <returns>The result index.</returns>
	public int ToAbsoluteIndex(int relativeCellIndex)
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
	/// <returns>The result index.</returns>
	public int ToRelativeIndex(int absoluteCellIndex)
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
	/// <param name="isCyclic">Indicates whether the cell overflown in the relative grid will be included to be checked or not.</param>
	/// <returns>Target cell absolute index.</returns>
	public int GetAdjacentAbsoluteCellWith(int absoluteCellIndex, Direction direction, bool isCyclic)
	{
		var rowsCount = AbsoluteRowsCount;
		var columnsCount = AbsoluteColumnsCount;
		var row = absoluteCellIndex / columnsCount;
		var column = absoluteCellIndex % columnsCount;
		return direction switch
		{
			Direction.Up when row >= 1 => (row - 1) * columnsCount + column,
			Direction.Up when row == 0 && isCyclic => (rowsCount - 1) * columnsCount + column,
			Direction.Down when row < rowsCount => (row + 1) * columnsCount + column,
			Direction.Down when row == rowsCount && isCyclic => 0 * columnsCount + column,
			Direction.Left when column >= 1 => row * columnsCount + column - 1,
			Direction.Left when column == 0 && isCyclic => row * columnsCount + columnsCount - 1,
			Direction.Right when column < columnsCount => row * columnsCount + column + 1,
			Direction.Right when column == columnsCount && isCyclic => row + columnsCount + 0,
			_ => -1
		};
	}

	/// <summary>
	/// Returns center position (point) of the specified absolute row and column absolute index.
	/// </summary>
	/// <param name="absoluteRowIndex">Absolute row index.</param>
	/// <param name="absoluteColumnIndex">Absolute column index.</param>
	/// <returns>The point instance that represents the target center position.</returns>
	public SKPoint GetCenterPoint(int absoluteRowIndex, int absoluteColumnIndex)
	{
		var topLeft = GetTopLeftPoint(absoluteRowIndex, absoluteColumnIndex);
		return topLeft - new SKPoint(CellWidthAndHeight / 2, CellWidthAndHeight / 2);
	}

	/// <summary>
	/// Returns top-left position (point) of the specified absolute cell index.
	/// </summary>
	/// <param name="absoluteCellIndex">Absolute cell index.</param>
	/// <returns>The point instance that represents the target top-left position.</returns>
	public SKPoint GetTopLeftPoint(int absoluteCellIndex)
	{
		var columnsCount = AbsoluteColumnsCount;
		return GetTopLeftPoint(absoluteCellIndex / columnsCount, absoluteCellIndex % columnsCount);
	}

	/// <summary>
	/// Returns top-left position (point) of the specified absolute row and column index.
	/// </summary>
	/// <param name="absoluteRowIndex">Absolute row index.</param>
	/// <param name="absoluteColumnIndex">Absolute column index.</param>
	/// <returns>The point instance that represents the target top-left position.</returns>
	public SKPoint GetTopLeftPoint(int absoluteRowIndex, int absoluteColumnIndex)
		=> new(CellWidthAndHeight * absoluteColumnIndex + Margin, CellWidthAndHeight * absoluteRowIndex + Margin);

	/// <summary>
	/// Returns top-right position (point) of the specified absolute cell index.
	/// </summary>
	/// <param name="absoluteCellIndex">Absolute cell index.</param>
	/// <returns>The point instance that represents the target top-right position.</returns>
	public SKPoint GetTopRightPoint(int absoluteCellIndex)
	{
		var columnsCount = AbsoluteColumnsCount;
		return GetTopRightPoint(absoluteCellIndex / columnsCount, absoluteCellIndex % columnsCount);
	}

	/// <summary>
	/// Returns top-right position (point) of the specified absolute row and column index.
	/// </summary>
	/// <param name="absoluteRowIndex">Absolute row index.</param>
	/// <param name="absoluteColumnIndex">Absolute column index.</param>
	/// <returns>The point instance that represents the target top-right position.</returns>
	public SKPoint GetTopRightPoint(int absoluteRowIndex, int absoluteColumnIndex)
	{
		var topLeft = GetTopLeftPoint(absoluteRowIndex, absoluteColumnIndex);
		return topLeft + new SKPoint(CellWidthAndHeight, 0);
	}

	/// <summary>
	/// Returns bottom-left position (point) of the specified absolute cell index.
	/// </summary>
	/// <param name="absoluteCellIndex">Absolute cell index.</param>
	/// <returns>The point instance that represents the target bottom-left position.</returns>
	public SKPoint GetBottomLeftPoint(int absoluteCellIndex)
	{
		var columnsCount = AbsoluteColumnsCount;
		return GetBottomLeftPoint(absoluteCellIndex / columnsCount, absoluteCellIndex % columnsCount);
	}

	/// <summary>
	/// Returns bottom-left position (point) of the specified absolute row and column index.
	/// </summary>
	/// <param name="absoluteRowIndex">Absolute row index.</param>
	/// <param name="absoluteColumnIndex">Absolute column index.</param>
	/// <returns>The point instance that represents the target bottom-left position.</returns>
	public SKPoint GetBottomLeftPoint(int absoluteRowIndex, int absoluteColumnIndex)
	{
		var topLeft = GetTopLeftPoint(absoluteRowIndex, absoluteColumnIndex);
		return topLeft + new SKPoint(0, CellWidthAndHeight);
	}

	/// <summary>
	/// Returns bottom-right position (point) of the specified absolute cell index.
	/// </summary>
	/// <param name="absoluteCellIndex">Absolute cell index.</param>
	/// <returns>The point instance that represents the target bottom-right position.</returns>
	public SKPoint GetBottomRightPoint(int absoluteCellIndex)
	{
		var columnsCount = AbsoluteColumnsCount;
		return GetBottomRightPoint(absoluteCellIndex / columnsCount, absoluteCellIndex % columnsCount);
	}

	/// <summary>
	/// Returns bottom-right position (point) of the specified absolute row and column index.
	/// </summary>
	/// <param name="absoluteRowIndex">Absolute row index.</param>
	/// <param name="absoluteColumnIndex">Absolute column index.</param>
	/// <returns>The point instance that represents the target bottom-right position.</returns>
	public SKPoint GetBottomRightPoint(int absoluteRowIndex, int absoluteColumnIndex)
	{
		var topLeft = GetTopLeftPoint(absoluteRowIndex, absoluteColumnIndex);
		return topLeft + new SKPoint(CellWidthAndHeight, CellWidthAndHeight);
	}
}
