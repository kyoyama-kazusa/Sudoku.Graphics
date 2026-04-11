namespace System;

/// <summary>
/// Provides extension members on <see cref="IFloatingPoint{TSelf}"/>.
/// </summary>
/// <seealso cref="IFloatingPoint{TSelf}"/>
public static class FloatingPointExtensions
{
	/// <summary>
	/// Provides methods on <see cref="IFloatingPoint{TSelf}"/> instances.
	/// </summary>
	/// <typeparam name="T">The type that implements <see cref="IFloatingPoint{TSelf}"/>.</typeparam>
	extension<T>(T) where T : IFloatingPoint<T>
	{
		/// <summary>
		/// Determine whether two <typeparamref name="T"/> instances are nearly equal.
		/// </summary>
		/// <param name="left">The left instance.</param>
		/// <param name="right">The right instance.</param>
		/// <param name="epsilon">Epsilon.</param>
		/// <returns>A <see cref="bool"/> result.</returns>
		public static bool NearlyEquals(T left, T right, T epsilon) => Math.Abs(left - right) <= epsilon;
	}
}
