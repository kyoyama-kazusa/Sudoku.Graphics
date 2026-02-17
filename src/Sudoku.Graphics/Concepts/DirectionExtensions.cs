namespace Sudoku.Concepts;

/// <summary>
/// Provides extension members on <see cref="Direction"/>.
/// </summary>
/// <seealso cref="Direction"/>
public static class DirectionExtensions
{
	/// <param name="this">The current direction.</param>
	extension(Direction @this)
	{
		/// <summary>
		/// Indicates the negated direction of the current direction.
		/// If the current instance contains multiple directions as flags, it'll negate all directions contained.
		/// </summary>
		public Direction Negated
		{
			get
			{
				var result = @this;
				if ((@this & Direction.Up) == Direction.Up)
				{
					result &= ~Direction.Up;
					result |= Direction.Down;
				}
				if ((@this & Direction.Down) == Direction.Down)
				{
					result &= ~Direction.Down;
					result |= Direction.Up;
				}
				if ((@this & Direction.Left) == Direction.Left)
				{
					result &= ~Direction.Left;
					result |= Direction.Right;
				}
				if ((@this & Direction.Right) == Direction.Right)
				{
					result &= ~Direction.Right;
					result |= Direction.Left;
				}
				return result;
			}
		}


		/// <summary>
		/// Indicates all directions.
		/// </summary>
		public static ReadOnlySpan<Direction> AllDirections => [Direction.Up, Direction.Down, Direction.Left, Direction.Right];
	}
}
