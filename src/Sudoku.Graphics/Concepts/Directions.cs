namespace Sudoku.Concepts;

/// <summary>
/// Provides extension members on <see cref="Direction"/>.
/// </summary>
/// <seealso cref="Direction"/>
public static class DirectionExtensions
{
	extension(Direction)
	{
		/// <summary>
		/// Indicates all directions.
		/// </summary>
		public static ReadOnlySpan<Direction> AllDirections => [Direction.Up, Direction.Down, Direction.Left, Direction.Right];
	}
}
