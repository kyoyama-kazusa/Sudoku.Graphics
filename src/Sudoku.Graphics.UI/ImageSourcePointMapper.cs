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
		// 从 PointMapper 获取关键参数
		var cellSize = pointMapper.CellSize;
		var margin = pointMapper.Margin;
		int absoluteRows = pointMapper.AbsoluteRowsCount;      // 总行数（含预留）
		int absoluteColumns = pointMapper.AbsoluteColumnsCount; // 总列数（含预留）

		// 计算网格绘制区域的左上角坐标和尺寸
		var gridStartX = margin;
		var gridStartY = margin;
		var gridWidth = absoluteColumns * cellSize;
		var gridHeight = absoluteRows * cellSize;

		// 判断点击点是否位于网格区域内（允许浮点误差，使用 < 而不是 <= 以处理右/下边界）
		if (point.X < gridStartX || point.X >= gridStartX + gridWidth || point.Y < gridStartY || point.Y >= gridStartY + gridHeight)
		{
			result = default;
			return false;
		}

		// 计算列索引和行索引
		var column = (int)((point.X - gridStartX) / cellSize);
		var row = (int)((point.Y - gridStartY) / cellSize);

		// 边界保护（防止浮点误差导致索引越界）
		column = Math.Clamp(column, 0, absoluteColumns - 1);
		row = Math.Clamp(row, 0, absoluteRows - 1);

		// 绝对索引 = row * 总列数 + column
		result = row * absoluteColumns + column;
		return true;
	}
}
