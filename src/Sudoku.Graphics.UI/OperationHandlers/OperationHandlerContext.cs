namespace Sudoku.Graphics.UI.OperationHandlers;

/// <summary>
/// Provides context on processing an operation.
/// </summary>
public sealed class OperationHandlerContext
{
	/// <summary>
	/// Indicates the mouse point pressed.
	/// </summary>
	public required Point PointPressed { get; set; }

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
	/// When a user clicked a border, this method will calculate which border of two adjacent cells he clicked.
	/// </summary>
	/// <returns>Two cells.</returns>
	public (Absolute Cell1, Absolute Cell2) GetBorder()
	{
		unsafe
		{
			return GetBorderOrCornerCore(&ImageSourcePointMapper.TryGetBorder, (Absolute)(-1), (Absolute)(-1));
		}
	}

	/// <summary>
	/// When a user clicked a border, this method will calculate which border he clicked.
	/// </summary>
	/// <returns>The cell and the direction of border clicked.</returns>
	public (Absolute Cell, Direction4 Direction) GetBorderWithDirection()
	{
		unsafe
		{
			return GetBorderOrCornerCore(&ImageSourcePointMapper.TryGetBorderRaw, (Absolute)(-1), Direction4.None);
		}
	}

	/// <summary>
	/// When a user clicked a border or corner, this method will calculate which border or corner he clicked.
	/// This method returns a pair of cells indicating the corner or border shared.
	/// </summary>
	/// <returns>Two cells.</returns>
	public (Absolute Cell1, Absolute Cell2) GetBorderOrCorner()
	{
		unsafe
		{
			return GetBorderOrCornerCore(&ImageSourcePointMapper.TryGetBorderOrCorner, (Absolute)(-1), (Absolute)(-1));
		}
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

	/// <summary>
	/// The backing method of <see cref="GetBorderOrCorner"/> and <see cref="GetBorder"/>.
	/// </summary>
	/// <typeparam name="T1">The type of result value 1.</typeparam>
	/// <typeparam name="T2">The type of result value 2.</typeparam>
	/// <param name="checker">The checker method.</param>
	/// <param name="defaultValue1">The default value of first value of return.</param>
	/// <param name="defaultValue2">The default value of second value of return.</param>
	/// <returns>A pair of cells.</returns>
	/// <seealso cref="GetBorder"/>
	/// <seealso cref="GetBorderOrCorner"/>
	private unsafe (T1, T2) GetBorderOrCornerCore<T1, T2>(
		[DisallowNull, NotNull] delegate*<Point, PointMapper, out T1, out T2, bool> checker,
		T1 defaultValue1,
		T2 defaultValue2
	)
		where T1 : notnull
		where T2 : notnull
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
			return (defaultValue1, defaultValue2);
		}

		var scaleX = actualWidth / sourceWidth;
		var scaleY = actualHeight / sourceHeight;
		var scale = Math.Min(scaleX, scaleY);
		var offsetX = (actualWidth - sourceWidth * scale) / 2;
		var offsetY = (actualHeight - sourceHeight * scale) / 2;
		var originalX = (x - offsetX) / scale;
		var originalY = (y - offsetY) / scale;
		return checker(new(originalX, originalY), mapper, out var resultValue1, out var resultValue2)
			? (resultValue1, resultValue2)
			: (defaultValue1, defaultValue2);
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

	/// <summary>
	/// Try to get border clicked. This method will find for the nearest border to user-clicked point.
	/// </summary>
	/// <param name="point">The point.</param>
	/// <param name="mapper">The mapper instance.</param>
	/// <param name="cell1">The cell 1 that provides the half of the border.</param>
	/// <param name="cell2">The cell 2 that provides the other half of the border.</param>
	/// <returns>A <see cref="bool"/> result indicating whether the border is correctly projected.</returns>
	public static bool TryGetBorder(Point point, PointMapper mapper, out Absolute cell1, out Absolute cell2)
	{
		// Find cell clicked.
		if (!TryGetPoint(point, mapper, out var cell))
		{
			goto ReturnFalse;
		}

		// Check distance to four borders.
		var distanceUp = Math.Abs(point.Y - mapper.GetPoint(cell, Alignment.TopLeft).Y);
		var distanceDown = Math.Abs(point.Y - mapper.GetPoint(cell, Alignment.BottomLeft).Y);
		var distanceLeft = Math.Abs(point.X - mapper.GetPoint(cell, Alignment.TopLeft).X);
		var distanceRight = Math.Abs(point.X - mapper.GetPoint(cell, Alignment.TopRight).X);
		var min = Enumerable.Min([distanceUp, distanceDown, distanceLeft, distanceRight]);
		if (min == distanceUp)
		{
			(cell1, cell2) = (cell - mapper.AbsoluteColumnsCount, cell);
		}
		else if (min == distanceDown)
		{
			(cell1, cell2) = (cell, cell + mapper.AbsoluteColumnsCount);
		}
		else if (min == distanceLeft)
		{
			(cell1, cell2) = (cell - 1, cell);
		}
		else if (min == distanceRight)
		{
			(cell1, cell2) = (cell, cell + 1);
		}
		else
		{
			goto ReturnFalse;
		}

		// Check whether border overflows.
		var (cellRow, cellColumn) = (cell / mapper.AbsoluteColumnsCount, cell % mapper.AbsoluteColumnsCount);
		if (min == distanceUp && cellRow == 0
			|| min == distanceDown && cellRow == mapper.AbsoluteRowsCount - 1
			|| min == distanceLeft && cellColumn == 0
			|| min == distanceRight && cellColumn == mapper.AbsoluteColumnsCount - 1)
		{
			goto ReturnFalse;
		}
		return true;

	ReturnFalse:
		(cell1, cell2) = (-1, -1);
		return false;
	}

	/// <summary>
	/// Try to get border clicked. This method will find for the nearest border to user-clicked point.
	/// </summary>
	/// <param name="point">The point.</param>
	/// <param name="mapper">The mapper instance.</param>
	/// <param name="cell">The cell that provides the half of the border.</param>
	/// <param name="direction">The direction clicked.</param>
	/// <returns>A <see cref="bool"/> result indicating whether the border is correctly projected.</returns>
	public static bool TryGetBorderRaw(Point point, PointMapper mapper, out Absolute cell, out Direction4 direction)
	{
		// Find cell clicked.
		if (!TryGetPoint(point, mapper, out cell))
		{
			goto ReturnFalse;
		}

		// Check distance to four borders.
		var distanceUp = Math.Abs(point.Y - mapper.GetPoint(cell, Alignment.TopLeft).Y);
		var distanceDown = Math.Abs(point.Y - mapper.GetPoint(cell, Alignment.BottomLeft).Y);
		var distanceLeft = Math.Abs(point.X - mapper.GetPoint(cell, Alignment.TopLeft).X);
		var distanceRight = Math.Abs(point.X - mapper.GetPoint(cell, Alignment.TopRight).X);
		var min = Enumerable.Min([distanceUp, distanceDown, distanceLeft, distanceRight]);
		if (min == distanceUp)
		{
			direction = Direction4.Up;
		}
		else if (min == distanceDown)
		{
			direction = Direction4.Down;
		}
		else if (min == distanceLeft)
		{
			direction = Direction4.Left;
		}
		else if (min == distanceRight)
		{
			direction = Direction4.Right;
		}
		else
		{
			goto ReturnFalse;
		}

		return true;

	ReturnFalse:
		(cell, direction) = (-1, Direction4.None);
		return false;
	}

	/// <summary>
	/// Try to get border or corner clicked. This method will find for the nearest border to user-clicked point.
	/// </summary>
	/// <param name="point">The point.</param>
	/// <param name="mapper">The mapper instance.</param>
	/// <param name="cell1">The cell 1 that provides the half of the border.</param>
	/// <param name="cell2">The cell 2 that provides the other half of the border.</param>
	/// <returns>A <see cref="bool"/> result indicating whether the border is correctly projected.</returns>
	public static bool TryGetBorderOrCorner(Point point, PointMapper mapper, out Absolute cell1, out Absolute cell2)
	{
		// Find cell clicked.
		if (!TryGetPoint(point, mapper, out var cell))
		{
			goto ReturnFalse;
		}

		var columnsCount = mapper.AbsoluteColumnsCount;
		var rowsCount = mapper.AbsoluteRowsCount;
		var cellRow = cell / columnsCount;
		var cellColumn = cell % columnsCount;

		// Corner points of the current cell.
		var topLeft = mapper.GetPoint(cell, Alignment.TopLeft);
		var topRight = mapper.GetPoint(cell, Alignment.TopRight);
		var bottomLeft = mapper.GetPoint(cell, Alignment.BottomLeft);
		var bottomRight = mapper.GetPoint(cell, Alignment.BottomRight);

		// Distances to four borders.
		var distanceUp = Math.Abs(point.Y - topLeft.Y);
		var distanceDown = Math.Abs(point.Y - bottomLeft.Y);
		var distanceLeft = Math.Abs(point.X - topLeft.X);
		var distanceRight = Math.Abs(point.X - topRight.X);

		// Decide whether this click looks like a corner click. Tune this threshold if needed.
		const float cellSizeEpsilon = 0.2F;
		var cornerEpsilon = mapper.CellSize * cellSizeEpsilon;
		var verticalDistance = Math.Min(distanceUp, distanceDown);
		var horizontalDistance = Math.Min(distanceLeft, distanceRight);
		if (Math.Abs(verticalDistance - horizontalDistance) <= cornerEpsilon)
		{
			// Find the nearest corner of the current cell.
			var dTopLeft = squaredDistance(point, topLeft);
			var dTopRight = squaredDistance(point, topRight);
			var dBottomLeft = squaredDistance(point, bottomLeft);
			var dBottomRight = squaredDistance(point, bottomRight);

			var minCornerDistance = Math.Min(Math.Min(dTopLeft, dTopRight), Math.Min(dBottomLeft, dBottomRight));

			var (vertexRow, vertexCol) = minCornerDistance == dTopLeft ? (cellRow, cellColumn)
				: minCornerDistance == dTopRight ? (cellRow, cellColumn + 1)
				: minCornerDistance == dBottomLeft ? (cellRow + 1, cellColumn)
				: (cellRow + 1, cellColumn + 1);

			// A real corner must be shared by 4 cells.
			if (vertexRow <= 0 || vertexRow >= rowsCount || vertexCol <= 0 || vertexCol >= columnsCount)
			{
				goto ReturnFalse;
			}

			// Four cells around this vertex:
			// (vertexRow - 1, vertexCol - 1), (vertexRow - 1, vertexCol)
			// (vertexRow,     vertexCol - 1), (vertexRow,     vertexCol)
			cell1 = (vertexRow - 1) * columnsCount + (vertexCol - 1);
			cell2 = vertexRow * columnsCount + vertexCol;
			return true;
		}

		// Border mode: same logic as TryGetBorder.
		var min = Math.Min(Math.Min(distanceUp, distanceDown), Math.Min(distanceLeft, distanceRight));
		if (min == distanceUp)
		{
			(cell1, cell2) = (cell - columnsCount, cell);
		}
		else if (min == distanceDown)
		{
			(cell1, cell2) = (cell, cell + columnsCount);
		}
		else if (min == distanceLeft)
		{
			(cell1, cell2) = (cell - 1, cell);
		}
		else if (min == distanceRight)
		{
			(cell1, cell2) = (cell, cell + 1);
		}
		else
		{
			goto ReturnFalse;
		}

		// Check whether border overflows.
		if (min == distanceUp && cellRow == 0
			|| min == distanceDown && cellRow == rowsCount - 1
			|| min == distanceLeft && cellColumn == 0
			|| min == distanceRight && cellColumn == columnsCount - 1)
		{
			goto ReturnFalse;
		}

		return true;

	ReturnFalse:
		(cell1, cell2) = (-1, -1);
		return false;


		static double squaredDistance(GenericPoint a, GenericPoint b)
		{
			var dx = a.X - b.X;
			var dy = a.Y - b.Y;
			return dx * dx + dy * dy;
		}
	}
}

/// <summary>
/// Represents a generic-typed point.
/// </summary>
file readonly union GenericPoint(Point, SKPoint)
{
	/// <summary>
	/// Indicates the point X value.
	/// </summary>
	public double X => Value switch { Point p => p.X, SKPoint p => p.X, _ => throw new InvalidOperationException() };

	/// <summary>
	/// Indicates the point Y value.
	/// </summary>
	public double Y => Value switch { Point p => p.Y, SKPoint p => p.Y, _ => throw new InvalidOperationException() };
}
