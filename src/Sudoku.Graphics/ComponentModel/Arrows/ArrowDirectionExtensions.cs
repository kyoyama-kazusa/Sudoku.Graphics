namespace Sudoku.ComponentModel.Arrows;

/// <summary>
/// Provides extension methods on <see cref="ArrowDirection"/>.
/// </summary>
/// <seealso cref="ArrowDirection"/>
public static class ArrowDirectionExtensions
{
	/// <param name="this">The current instance.</param>
	extension(ArrowDirection @this)
	{
		/// <summary>
		/// Indicates the degrees of the arrow direction, in angle.
		/// </summary>
		/// <exception cref="ArgumentOutOfRangeException">
		/// Throws when the current instance is not defined or <see cref="ArrowDirection.None"/>.
		/// </exception>
		/// <seealso cref="ArrowDirection.None"/>
		public float AngleDegrees
			=> @this switch
			{
				ArrowDirection.N => 0,
				ArrowDirection.NE => 45,
				ArrowDirection.E => 90,
				ArrowDirection.SE => 135,
				ArrowDirection.S => 180,
				ArrowDirection.SW => 225,
				ArrowDirection.W => 270,
				ArrowDirection.NW => 315,
				_ => throw new ArgumentOutOfRangeException(nameof(@this))
			};
	}
}
