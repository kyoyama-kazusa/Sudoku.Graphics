namespace Sudoku.Concepts;

public partial class AbsoluteFactory
{
	/// <summary>
	/// Provides tetris information.
	/// </summary>
	private static class Tetris
	{
		/// <summary>
		/// Represents a table of coordinates of all pieces (tetriminoes).
		/// </summary>
		public static readonly (Absolute RowIndex, Absolute ColumnIndex)[][] PiecesCoordinateTable = [
			[(0, 0), (0, 1), (0, 2), (0, 3)], // I
			[(0, 0), (0, 1), (1, 0), (1, 1)], // O
			[(0, 0), (0, 1), (0, 2), (1, 1)], // T
			[(0, 0), (1, 0), (1, 1), (1, 2)], // J
			[(0, 2), (1, 0), (1, 1), (1, 2)], // L
			[(0, 1), (0, 2), (1, 0), (1, 1)], // S
			[(0, 0), (0, 1), (1, 1), (1, 2)] // Z
		];


		/// <summary>
		/// Get size of the specified piece.
		/// </summary>
		/// <param name="piece">The piece.</param>
		public static (Absolute RowsCount, Absolute ColumnsCount) GetPiecesSize(TetrisPiece piece)
		{
			var coordinates = PiecesCoordinateTable[(int)piece];
			var maxRowIndex = coordinates.Max(static coordinate => coordinate.RowIndex);
			var maxColumnIndex = coordinates.Max(static coordinate => coordinate.ColumnIndex);
			return (maxRowIndex + 1, maxColumnIndex + 1);
		}
	}
}
