namespace Sudoku.Graphics.Directions;

/// <summary>
/// Provides extension methods on <see cref="Direction8"/>.
/// </summary>
/// <seealso cref="Direction8"/>
public static class Direction8Extensions
{
	/// <param name="this">The current instance.</param>
	extension(Direction8 @this)
	{
		/// <summary>
		/// Indicates whether the direction points to diagonal direction.
		/// </summary>
		public bool IsDiagonal => @this is Direction8.LeftUp or Direction8.RightUp or Direction8.LeftDown or Direction8.RightDown;

		/// <summary>
		/// Indicates arrow string of this direction pointing to.
		/// </summary>
		/// <exception cref="ArgumentOutOfRangeException">
		/// Throws when <paramref name="this"/> is not defined or <see cref="Direction8.None"/>.
		/// </exception>
		/// <seealso cref="Direction8.None"/>
		public string ArrowString
			=> @this switch
			{
				Direction8.Up => "\u2191",
				Direction8.Down => "\u2193",
				Direction8.Left => "\u2190",
				Direction8.Right => "\u2192",
				Direction8.LeftUp => "\u2196",
				Direction8.RightUp => "\u2197",
				Direction8.LeftDown => "\u2199",
				Direction8.RightDown => "\u2198",
				_ => throw new ArgumentOutOfRangeException(nameof(@this))
			};

		/// <summary>
		/// Indicates the rotation degree of the specified direction, in angle.
		/// </summary>
		/// <exception cref="ArgumentOutOfRangeException">Throws when the current instance is not defined.</exception>
		/// <seealso cref="Direction8.None"/>
		public float RotationDegree
			=> @this switch
			{
				Direction8.Up or Direction8.None => 0,
				Direction8.RightUp => 45,
				Direction8.Right => 90,
				Direction8.RightDown => 135,
				Direction8.Down => 180,
				Direction8.LeftDown => 225,
				Direction8.Left => 270,
				Direction8.LeftUp => 315,
				_ => throw new ArgumentOutOfRangeException(nameof(@this))
			};


		/// <summary>
		/// Cast the current instance into <see cref="Direction4"/> instance.
		/// </summary>
		/// <returns>A <see cref="Direction4"/> instance.</returns>
		/// <exception cref="InvalidCastException">Throws when the direction is diagonal.</exception>
		/// <exception cref="ArgumentOutOfRangeException">Throws when the value is not defined.</exception>
		public Direction4 AsDirection4()
			=> @this switch
			{
				Direction8.Up => Direction4.Up,
				Direction8.Down => Direction4.Down,
				Direction8.Left => Direction4.Left,
				Direction8.Right => Direction4.Right,
				{ IsDiagonal: true } => throw new InvalidCastException("Cannot cast diagonal direction."),
				_ => throw new ArgumentOutOfRangeException(nameof(@this))
			};
	}
}
