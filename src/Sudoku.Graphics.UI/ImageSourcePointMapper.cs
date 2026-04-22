namespace Sudoku.Graphics.UI;

/// <summary>
/// Provides a way that maps points user clicked in user interface and drawing points.
/// </summary>
internal static class ImageSourcePointMapper
{
	/// <summary>
	/// Try to map user-clicked point <see cref="Point"/> to drawing cell <see cref="Absolute"/> instance.
	/// </summary>
	/// <param name="source">The source instance.</param>
	/// <param name="point">The point that user clicked.</param>
	/// <param name="pointMapper">The point mapper instance.</param>
	/// <param name="result">The result cell index.</param>
	/// <returns>A <see cref="bool"/> result indicating whether the mapping operation is succeeded.</returns>
	public static bool TryGetPoint(ImageSource source, Point point, PointMapper pointMapper, out Absolute result)
	{
		var cellSize = pointMapper.CellSize;
		var margin = pointMapper.Margin;
		var absoluteRows = pointMapper.AbsoluteRowsCount;
		var absoluteColumns = pointMapper.AbsoluteColumnsCount;
		var gridStartX = margin;
		var gridStartY = margin;
		var gridWidth = absoluteColumns * cellSize;
		var gridHeight = absoluteRows * cellSize;
		if (point.X < gridStartX || point.X >= gridStartX + gridWidth || point.Y < gridStartY || point.Y >= gridStartY + gridHeight)
		{
			result = default;
			return false;
		}

		var row = (int)((point.Y - gridStartY) / cellSize);
		var column = (int)((point.X - gridStartX) / cellSize);
		if (row < 0 || row >= absoluteRows || column < 0 || column >= absoluteColumns)
		{
			result = default;
			return false;
		}
		result = row * absoluteColumns + column;
		return true;
	}
}
