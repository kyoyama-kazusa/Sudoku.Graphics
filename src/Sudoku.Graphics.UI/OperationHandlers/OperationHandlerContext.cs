namespace Sudoku.Graphics.UI.OperationHandlers;

/// <summary>
/// Provides context on processing an operation.
/// </summary>
public sealed class OperationHandlerContext
{
	/// <summary>
	/// Indicates the mouse point pressed.
	/// </summary>
	public required Point PointPressed { get; init; }

	/// <summary>
	/// Indicates the owner window.
	/// </summary>
	public required MainWindow OwnerWindow { get; init; }

	/// <summary>
	/// Indicates mouse button event arguments provided. It may be changed during different phase (mouse down and up events).
	/// </summary>
	public required MouseButtonEventArgs MouseEventArgs { get; set; }

	/// <summary>
	/// Indicates the items.
	/// </summary>
	public required ItemSet Items { get; init; }


	/// <summary>
	/// Try to project user-clicked point into the target cell clicked; or return -1 if failed to calculate.
	/// </summary>
	/// <returns>The target cell clicked.</returns>
	public Absolute GetCell()
	{
		if (this is not
			{
				PointPressed: var (x, y),
				OwnerWindow:
				{
					MainGrid:
					{
						Source: { Width: var sourceWidth, Height: var sourceHeight },
						ActualWidth: var actualWidth,
						ActualHeight: var actualHeight
					},
					CurrentCanvas.Mapper: var mapper
				},
			})
		{
			return -1;
		}

		var scaleX = actualWidth / sourceWidth;
		var scaleY = actualHeight / sourceHeight;
		var scale = Math.Min(scaleX, scaleY);
		var offsetX = (actualWidth - sourceWidth * scale) / 2;
		var offsetY = (actualHeight - sourceHeight * scale) / 2;
		var originalX = (x - offsetX) / scale;
		var originalY = (y - offsetY) / scale;
		var point = new Point(originalX, originalY);
		return ImageSourcePointMapper.TryGetPoint(point, mapper, out var cell) ? cell : -1;
	}

	/// <summary>
	/// Try to project user-clicked point into the target candidate clicked;
	/// or return <see cref="CandidatePosition.Invalid"/> if failed to calculate.
	/// </summary>
	/// <returns>The target cell clicked.</returns>
	/// <seealso cref="CandidatePosition.Invalid"/>
	public CandidatePosition GetCandidate()
	{
		if (this is not
			{
				PointPressed: var (x, y),
				OwnerWindow:
				{
					MainGrid:
					{
						Source: { Width: var sourceWidth, Height: var sourceHeight },
						ActualWidth: var actualWidth,
						ActualHeight: var actualHeight
					},
					CurrentCanvas.Mapper: { TemplateSize: { RowsCount: var r, ColumnsCount: var c } } mapper
				},
			})
		{
			return CandidatePosition.Invalid;
		}
		if (r != c)
		{
			return CandidatePosition.Invalid;
		}

		var scaleX = actualWidth / sourceWidth;
		var scaleY = actualHeight / sourceHeight;
		var scale = Math.Min(scaleX, scaleY);
		var offsetX = (actualWidth - sourceWidth * scale) / 2;
		var offsetY = (actualHeight - sourceHeight * scale) / 2;
		var originalX = (x - offsetX) / scale;
		var originalY = (y - offsetY) / scale;
		var cellSize = mapper.CellSize;
		var margin = mapper.Margin;
		var absoluteRowsCount = mapper.AbsoluteRowsCount;
		var absoluteColumnsCount = mapper.AbsoluteColumnsCount;
		var gridStartX = margin;
		var gridStartY = margin;
		var gridWidth = absoluteColumnsCount * cellSize;
		var gridHeight = absoluteRowsCount * cellSize;
		if (originalX < gridStartX || originalX >= gridStartX + gridWidth
			|| originalY < gridStartY || originalY >= gridStartY + gridHeight)
		{
			return CandidatePosition.Invalid;
		}

		var rowFloat = (originalY - gridStartY) / cellSize;
		var colFloat = (originalX - gridStartX) / cellSize;
		var cellRow = (int)rowFloat;
		var cellColumn = (int)colFloat;
		if (cellRow < 0 || cellRow >= absoluteRowsCount || cellColumn < 0 || cellColumn >= absoluteColumnsCount)
		{
			return CandidatePosition.Invalid;
		}

		var localX = Math.Clamp(colFloat - cellColumn, 0, 1 - 1E-6);
		var localY = Math.Clamp(rowFloat - cellRow, 0, 1 - 1E-6);
		var subgridSize = r.GetCandidatesCountInEachRow();
		var subCellWidth = 1.0 / subgridSize;
		var subCellHeight = 1.0 / subgridSize;
		var subRow = Math.Clamp((int)(localY / subCellHeight), 0, subgridSize - 1);
		var subCol = Math.Clamp((int)(localX / subCellWidth), 0, subgridSize - 1);
		var innerIndex = subRow * subgridSize + subCol;
		var cellIndex = cellRow * absoluteColumnsCount + cellColumn;
		return new(cellIndex, subgridSize, innerIndex);
	}
}

/// <summary>
/// Provides a way that maps points user clicked in user interface and drawing points.
/// </summary>
file static class ImageSourcePointMapper
{
	/// <summary>
	/// Try to map user-clicked point <see cref="Point"/> to drawing cell <see cref="Absolute"/> instance.
	/// </summary>
	/// <param name="point">The point that user clicked.</param>
	/// <param name="pointMapper">The point mapper instance.</param>
	/// <param name="result">The result cell index.</param>
	/// <returns>A <see cref="bool"/> result indicating whether the mapping operation is succeeded.</returns>
	public static bool TryGetPoint(Point point, PointMapper pointMapper, out Absolute result)
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
