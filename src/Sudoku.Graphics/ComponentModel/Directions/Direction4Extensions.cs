namespace Sudoku.ComponentModel.Directions;

/// <summary>
/// Provides extension members on <see cref="Direction4"/>.
/// </summary>
/// <seealso cref="Direction4"/>
public static class Direction4Extensions
{
	/// <param name="this">The current direction.</param>
	extension(Direction4 @this)
	{
		/// <summary>
		/// Indicates the reversed direction of the current direction.
		/// </summary>
		public Direction4 Reversed
			=> @this switch
			{
				Direction4.Up => Direction4.Down,
				Direction4.Down => Direction4.Up,
				Direction4.Left => Direction4.Right,
				Direction4.Right => Direction4.Left,
				_ => throw new ArgumentOutOfRangeException(nameof(@this))
			};

		/// <summary>
		/// Indicates the negated direction of the current direction.
		/// If the current instance contains multiple directions as flags, it'll negate all directions contained.
		/// </summary>
		public Direction4 Negated
		{
			get
			{
				var result = @this;
				if (@this.HasFlag(Direction4.Up))
				{
					result &= ~Direction4.Up;
					result |= Direction4.Down;
				}
				if (@this.HasFlag(Direction4.Down))
				{
					result &= ~Direction4.Down;
					result |= Direction4.Up;
				}
				if (@this.HasFlag(Direction4.Left))
				{
					result &= ~Direction4.Left;
					result |= Direction4.Right;
				}
				if (@this.HasFlag(Direction4.Right))
				{
					result &= ~Direction4.Right;
					result |= Direction4.Left;
				}
				return result;
			}
		}


		/// <summary>
		/// Returns an equivalent <see cref="Direction8"/> instance (8-direction enumeration type).
		/// </summary>
		/// <returns>An equivalent <see cref="Direction8"/> instance.</returns>
		public Direction8 AsDirection8()
		{
			var result = Direction8.None;
			if (@this.HasFlag(Direction4.Up))
			{
				result |= Direction8.Up;
			}
			if (@this.HasFlag(Direction4.Down))
			{
				result |= Direction8.Down;
			}
			if (@this.HasFlag(Direction4.Left))
			{
				result |= Direction8.Left;
			}
			if (@this.HasFlag(Direction4.Right))
			{
				result |= Direction8.Right;
			}
			return result;
		}


		/// <summary>
		/// Indicates all directions.
		/// </summary>
		public static ReadOnlySpan<Direction4> AllDirections => [Direction4.Up, Direction4.Down, Direction4.Left, Direction4.Right];
	}
}
