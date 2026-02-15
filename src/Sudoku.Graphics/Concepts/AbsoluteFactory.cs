namespace Sudoku.Concepts;

/// <summary>
/// Provides an easy way to create <see cref="Absolute"/> instances.
/// Generally it will be used by creating <see cref="LineSegment"/> instances.
/// </summary>
/// <seealso cref="Absolute"/>
/// <seealso cref="LineSegment"/>
public static partial class AbsoluteFactory
{
	extension(Absolute[] @this)
	{
		/// <summary>
		/// Get offset of the specified array of cell indices.
		/// </summary>
		/// <param name="rowsCount">The number of rows as offset.</param>
		/// <param name="columnsCount">The number of columns as offset.</param>
		/// <param name="mapper">The mapper instance.</param>
		/// <returns>The result.</returns>
		public Absolute[] Offset(Absolute rowsCount, Absolute columnsCount, PointMapper mapper)
		{
			var absoluteColumnsCount = mapper.AbsoluteColumnsCount;
			var result = new Absolute[@this.Length];
			var i = 0;
			foreach (var cell in @this)
			{
				var r = cell / absoluteColumnsCount;
				var c = cell % absoluteColumnsCount;
				result[i++] = (r + rowsCount) * absoluteColumnsCount + c + columnsCount;
			}
			return result;
		}
	}


	/// <summary>
	/// Creates a list of absolute cell indices of the specified piece, defined in tetris.
	/// </summary>
	/// <param name="piece">The piece.</param>
	/// <param name="mapper">The mapper instance that represents basic information of the number of rows and columns.</param>
	/// <param name="rotationType">The rotation type. By default it's <see cref="RotationType.None"/>.</param>
	/// <returns>Absolute cell indices of that piece, aligned as global top-left position <c>(0, 0)</c> in canvas.</returns>
	/// <exception cref="ArgumentOutOfRangeException">
	/// Throws when <paramref name="piece"/> or <paramref name="rotationType"/> is not defined.
	/// </exception>
	/// <seealso cref="RotationType.None"/>
	public static Absolute[] GetTetrisPiece(TetrisPiece piece, PointMapper mapper, RotationType rotationType = RotationType.None)
	{
		var resultCoordinates = rotate(
			(int)rotationType % 4,
			piece switch
			{
				TetrisPiece.I => Tetris.PiecesCoordinateTable[0],
				TetrisPiece.O => Tetris.PiecesCoordinateTable[1],
				TetrisPiece.T => Tetris.PiecesCoordinateTable[2],
				TetrisPiece.J => Tetris.PiecesCoordinateTable[3],
				TetrisPiece.L => Tetris.PiecesCoordinateTable[4],
				TetrisPiece.S => Tetris.PiecesCoordinateTable[5],
				TetrisPiece.Z => Tetris.PiecesCoordinateTable[6],
				_ => throw new ArgumentOutOfRangeException(nameof(piece))
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
