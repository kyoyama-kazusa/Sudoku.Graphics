namespace Sudoku.ComponentModel;

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
				if (@this.HasFlag(Direction.Up))
				{
					result &= ~Direction.Up;
					result |= Direction.Down;
				}
				if (@this.HasFlag(Direction.Down))
				{
					result &= ~Direction.Down;
					result |= Direction.Up;
				}
				if (@this.HasFlag(Direction.Left))
				{
					result &= ~Direction.Left;
					result |= Direction.Right;
				}
				if (@this.HasFlag(Direction.Right))
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
