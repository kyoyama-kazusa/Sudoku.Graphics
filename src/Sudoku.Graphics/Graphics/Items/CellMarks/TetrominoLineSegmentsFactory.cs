namespace Sudoku.Graphics.Items.CellMarks;

/// <summary>
/// Provides extension members on <see cref="Tetromino"/>.
/// </summary>
/// <seealso cref="Tetromino"/>
public static class TetrominoLineSegmentsFactory
{
	/// <param name="this">The specified tetromino.</param>
	extension(Tetromino @this)
	{
		/// <summary>
		/// Try to get sequence of tetromino, after rotated.
		/// </summary>
		/// <param name="rotationType">The rotation type.</param>
		/// <returns>The boolean sequence.</returns>
		/// <exception cref="ArgumentOutOfRangeException">Throws when <paramref name="rotationType"/> is not defined.</exception>
		public bool[,] GetTetrominoBooleanSequence(TetrominoRotationType rotationType)
		{
			// Get table.
			var resultCoordinates = RotateViaCoordinateTable(
				(int)rotationType % 4,
				@this switch
				{
					Tetromino.I => TetrominoInfo.PiecesCoordinateTable[0],
					Tetromino.O => TetrominoInfo.PiecesCoordinateTable[1],
					Tetromino.T => TetrominoInfo.PiecesCoordinateTable[2],
					Tetromino.J => TetrominoInfo.PiecesCoordinateTable[3],
					Tetromino.L => TetrominoInfo.PiecesCoordinateTable[4],
					Tetromino.S => TetrominoInfo.PiecesCoordinateTable[5],
					Tetromino.Z => TetrominoInfo.PiecesCoordinateTable[6],
					_ => throw new ArgumentOutOfRangeException(nameof(@this))
				}
			);

			var rowsCount = resultCoordinates.Max(static c => c.RowIndex) + 1;
			var columnsCount = resultCoordinates.Max(static c => c.ColumnIndex) + 1;

			// Return the table after projected into a boolean array.
			var result = new bool[rowsCount, columnsCount];
			foreach (var (rowIndex, columnIndex) in resultCoordinates)
			{
				result[rowIndex, columnIndex] = true;
			}
			return result;
		}

		/// <summary>
		/// Creates a <see cref="LineSegment"/> array that describes outlines of a tetris piece.
		/// </summary>
		/// <param name="rotationType">The rotation type.</param>
		/// <param name="mapper">The mapper.</param>
		/// <param name="offsetRowsCount">The offset rows count.</param>
		/// <param name="offsetColumnsCount">The offset columns count.</param>
		/// <returns>An array of <see cref="LineSegment"/> instances.</returns>
		public LineSegment[] GetOutline(
			TetrominoRotationType rotationType,
			PointMapper mapper,
			Absolute offsetRowsCount,
			Absolute offsetColumnsCount
		) => LineSegmentFactory.GetOutline(
			@this.GetTetromino(mapper, rotationType).WithOffset(offsetRowsCount, offsetColumnsCount, mapper),
			mapper
		);

		/// <summary>
		/// Creates a list of absolute cell indices of the specified tetromino.
		/// </summary>
		/// <param name="mapper">The mapper instance that represents basic information of the number of rows and columns.</param>
		/// <param name="rotationType">The rotation type. By default it's <see cref="TetrominoRotationType.None"/>.</param>
		/// <returns>Absolute cell indices of that piece, aligned as global top-left position <c>(0, 0)</c> in canvas.</returns>
		/// <exception cref="ArgumentOutOfRangeException">
		/// Throws when <paramref name="this"/> or <paramref name="rotationType"/> is not defined.
		/// </exception>
		/// <seealso cref="TetrominoRotationType.None"/>
		public Absolute[] GetTetromino(PointMapper mapper, TetrominoRotationType rotationType = TetrominoRotationType.None)
		{
			var resultCoordinates = RotateViaCoordinateTable(
				(int)rotationType % 4,
				@this switch
				{
					Tetromino.I => TetrominoInfo.PiecesCoordinateTable[0],
					Tetromino.O => TetrominoInfo.PiecesCoordinateTable[1],
					Tetromino.T => TetrominoInfo.PiecesCoordinateTable[2],
					Tetromino.J => TetrominoInfo.PiecesCoordinateTable[3],
					Tetromino.L => TetrominoInfo.PiecesCoordinateTable[4],
					Tetromino.S => TetrominoInfo.PiecesCoordinateTable[5],
					Tetromino.Z => TetrominoInfo.PiecesCoordinateTable[6],
					_ => throw new ArgumentOutOfRangeException(nameof(@this))
				}
			);
			var columnsCount = mapper.AbsoluteColumnsCount;
			var result = new Absolute[resultCoordinates.Length];
			foreach (var (i, (r, c)) in resultCoordinates.Index())
			{
				result[i] = r * columnsCount + c;
			}
			return [.. result];
		}
	}

	/// <summary>
	/// Rotates tetris piece via coordinate table.
	/// </summary>
	/// <param name="times">The times to rotate.</param>
	/// <param name="coordinates">The coordinate table.</param>
	/// <returns>The target coordinate table.</returns>
	private static (Absolute RowIndex, Absolute ColumnIndex)[] RotateViaCoordinateTable(int times, (Absolute RowIndex, Absolute ColumnIndex)[] coordinates)
	{
		for (var i = 0; i < times; i++)
		{
			var target = new List<(Absolute RowIndex, Absolute ColumnIndex)>(coordinates.Length);
			foreach (var (r, c) in coordinates)
			{
				target.Add((+c, -r));
			}

			// Find minimal row index and column index; negate it.
			var minRowIndex = -target.Min(static coordinate => coordinate.RowIndex);
			var minColumnIndex = -target.Min(static coordinate => coordinate.ColumnIndex);

			// Shift all coordinates by add (minRowIndex, minColumnIndex).
			foreach (ref var coordinate in CollectionsMarshal.AsSpan(target))
			{
				coordinate.RowIndex += minRowIndex;
				coordinate.ColumnIndex += minColumnIndex;
			}

			// Reassign array.
			coordinates = [.. target];
		}

		return coordinates;
	}
}
