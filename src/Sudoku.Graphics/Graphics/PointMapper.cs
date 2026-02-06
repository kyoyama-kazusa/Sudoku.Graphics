namespace Sudoku.Graphics;

/// <summary>
/// Represents a point mapper instance.
/// </summary>
/// <param name="cellSize"><inheritdoc cref="CellSize" path="/summary"/></param>
/// <param name="margin"><inheritdoc cref="Margin" path="/summary"/></param>
/// <param name="rowsCount"><inheritdoc cref="RowsCount" path="/summary"/></param>
/// <param name="columnsCount"><inheritdoc cref="ColumnsCount" path="/summary"/></param>
[method: SetsRequiredMembers]
public sealed class PointMapper(float cellSize, float margin, int rowsCount, int columnsCount)
{
	/// <summary>
	/// Indicates cell size of pixels.
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
	public required int RowsCount { get; init; } = rowsCount;

	/// <summary>
	/// Indiactes the number of columns. The number of columns should be an absolute value,
	/// including reserved regions (used by drawing outside-like puzzles).
	/// </summary>
	public required int ColumnsCount { get; init; } = columnsCount;

	/// <summary>
	/// Indicates grid size <see cref="SKRect"/> instance.
	/// </summary>
	public SKRect GridSize => SKRect.Create(CellSize * ColumnsCount, CellSize * RowsCount);

	/// <summary>
	/// Indicates full canvas size <see cref="SKRect"/> instance.
	/// </summary>
	public SKRect FullSize => SKRect.Create(GridSize.Width + 2 * Margin, GridSize.Height + 2 * Margin);

	/// <summary>
	/// Indicates full canvas size <see cref="SKRect"/> instance, in integer format.
	/// </summary>
	public SKRectI FullSizeInteger
		=> SKRectI.Create(
			(int)Math.Round(GridSize.Width + 2 * Margin),
			(int)Math.Round(GridSize.Height + 2 * Margin)
		);


	/// <summary>
	/// Returns top-left position (point) of the specified absolute row and column absolute index.
	/// </summary>
	/// <param name="absoluteRowIndex">Absolute row index.</param>
	/// <param name="absoluteColumnIndex">Absolute column index.</param>
	/// <returns>The point instance that represents the target top-left position.</returns>
	public SKPoint GetTopLeftPoint(int absoluteRowIndex, int absoluteColumnIndex)
		=> new(CellSize * absoluteColumnIndex + Margin, CellSize * absoluteRowIndex + Margin);
}
