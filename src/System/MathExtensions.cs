namespace System;

/// <summary>
/// Provides extension members on <see cref="Math"/>.
/// </summary>
/// <seealso cref="Math"/>
public static class MathExtensions
{
	/// <summary>
	/// Provides extension members on <see cref="Math"/> type.
	/// </summary>
	extension(Math)
	{
		/// <summary>
		/// Returns the absolute value of the current number.
		/// </summary>
		/// <typeparam name="T">The type of number.</typeparam>
		/// <param name="value">The value.</param>
		/// <returns>The result value.</returns>
		public static T Abs<T>(T value) where T : INumber<T> => value < T.Zero ? -value : value;
	}
}
