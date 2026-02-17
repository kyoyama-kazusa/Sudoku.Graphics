namespace Sudoku.ComponentModel.Tetris;

/// <summary>
/// Provides extension members on <see cref="Piece"/>.
/// </summary>
/// <seealso cref="Piece"/>
public static class TetrisLineSegmentFactory
{
	/// <param name="this">The specified tetris piece.</param>
	extension(Piece @this)
	{
		/// <summary>
		/// Creates a <see cref="LineSegment"/> array that describes outlines of a tetris piece.
		/// </summary>
		/// <param name="rotationType">The rotation type.</param>
		/// <param name="mapper">The mapper.</param>
		/// <param name="offsetRowsCount">The offset rows count.</param>
		/// <param name="offsetColumnsCount">The offset columns count.</param>
		/// <returns>An array of <see cref="LineSegment"/> instances.</returns>
		public LineSegment[] GetOutline(
			RotationType rotationType,
			PointMapper mapper,
			Absolute offsetRowsCount,
			Absolute offsetColumnsCount
		) => LineSegmentFactory.GetOutline(
			@this.GetTetrisPiece(mapper, rotationType).Offset(offsetRowsCount, offsetColumnsCount, mapper),
			mapper
		);

		/// <summary>
		/// Creates a list of absolute cell indices of the specified piece, defined in tetris.
		/// </summary>
		/// <param name="mapper">The mapper instance that represents basic information of the number of rows and columns.</param>
		/// <param name="rotationType">The rotation type. By default it's <see cref="RotationType.None"/>.</param>
		/// <returns>Absolute cell indices of that piece, aligned as global top-left position <c>(0, 0)</c> in canvas.</returns>
		/// <exception cref="ArgumentOutOfRangeException">
		/// Throws when <paramref name="this"/> or <paramref name="rotationType"/> is not defined.
		/// </exception>
		/// <seealso cref="RotationType.None"/>
		public Absolute[] GetTetrisPiece(PointMapper mapper, RotationType rotationType = RotationType.None)
		{
			var resultCoordinates = rotate(
				(int)rotationType % 4,
				@this switch
				{
					Piece.I => PieceInfo.PiecesCoordinateTable[0],
					Piece.O => PieceInfo.PiecesCoordinateTable[1],
					Piece.T => PieceInfo.PiecesCoordinateTable[2],
					Piece.J => PieceInfo.PiecesCoordinateTable[3],
					Piece.L => PieceInfo.PiecesCoordinateTable[4],
					Piece.S => PieceInfo.PiecesCoordinateTable[5],
					Piece.Z => PieceInfo.PiecesCoordinateTable[6],
					_ => throw new ArgumentOutOfRangeException(nameof(@this))
				}
			);
			var columnsCount = mapper.AbsoluteColumnsCount;
			var result = new Absolute[resultCoordinates.Length];
			var i = 0;
			foreach (var (r, c) in resultCoordinates)
			{
				result[i++] = r * columnsCount + c;
			}
			return [.. result];


			static (Absolute RowIndex, Absolute ColumnIndex)[] rotate(int times, (Absolute RowIndex, Absolute ColumnIndex)[] coordinates)
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
	}
}
