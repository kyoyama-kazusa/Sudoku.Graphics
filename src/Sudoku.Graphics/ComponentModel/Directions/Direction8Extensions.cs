namespace Sudoku.ComponentModel.Arrows;

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
		/// Indicates the degrees of the arrow direction, in angle.
		/// </summary>
		/// <exception cref="ArgumentOutOfRangeException">
		/// Throws when the current instance is not defined or <see cref="Direction8.None"/>.
		/// </exception>
		/// <seealso cref="Direction8.None"/>
		public float AngleDegrees
			=> @this switch
			{
				Direction8.Up => 0,
				Direction8.RightUp => 45,
				Direction8.Right => 90,
				Direction8.RightDown => 135,
				Direction8.Down => 180,
				Direction8.LeftDown => 225,
				Direction8.Left => 270,
				Direction8.LeftUp => 315,
				_ => throw new ArgumentOutOfRangeException(nameof(@this))
			};
	}
}
