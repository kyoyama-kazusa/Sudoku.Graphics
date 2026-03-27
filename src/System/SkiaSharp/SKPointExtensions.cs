namespace SkiaSharp;

/// <summary>
/// Provdies extension members on <see cref="SKPoint"/>.
/// </summary>
/// <seealso cref="SKPoint"/>
public static class SKPointExtensions
{
	/// <param name="this">The current instance.</param>
	extension(SKPoint @this)
	{
		/// <summary>
		/// Deconstruct instance into multiple values.
		/// </summary>
		public void Deconstruct(out float x, out float y) => (x, y) = (@this.X, @this.Y);

		/// <inheritdoc cref="IComparable{T}.CompareTo(T)"/>
		public int CompareTo(SKPoint other) => @this.X.CompareTo(other.X) is var r1 and not 0 ? r1 : @this.Y.CompareTo(other.Y);


		/// <inheritdoc cref="IComparisonOperators{TSelf, TOther, TResult}.op_GreaterThan(TSelf, TOther)"/>
		public static bool operator >(SKPoint left, SKPoint right) => left.CompareTo(right) > 0;

		/// <inheritdoc cref="IComparisonOperators{TSelf, TOther, TResult}.op_LessThan(TSelf, TOther)"/>
		public static bool operator <(SKPoint left, SKPoint right) => left.CompareTo(right) < 0;

		/// <inheritdoc cref="IComparisonOperators{TSelf, TOther, TResult}.op_GreaterThanOrEqual(TSelf, TOther)"/>
		public static bool operator >=(SKPoint left, SKPoint right) => left.CompareTo(right) >= 0;

		/// <inheritdoc cref="IComparisonOperators{TSelf, TOther, TResult}.op_LessThanOrEqual(TSelf, TOther)"/>
		public static bool operator <=(SKPoint left, SKPoint right) => left.CompareTo(right) <= 0;

		/// <summary>
		/// Returns the current instance directly; just a symmetry definition
		/// with operator <see cref="extension(SKPoint).op_UnaryNegation(SKPoint)"/>.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns>The result.</returns>
		/// <seealso cref="extension(SKPoint).op_UnaryNegation(SKPoint)"/>
		public static SKPoint operator +(SKPoint value) => value;

		/// <summary>
		/// Negates all factors of the current instance.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns>The result negated.</returns>
		public static SKPoint operator -(SKPoint value) => new(-value.X, -value.Y);
	}
}
