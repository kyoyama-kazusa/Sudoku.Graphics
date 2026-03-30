namespace SkiaSharp;

/// <summary>
/// Provdies extension members on <see cref="SKPoint"/>.
/// </summary>
/// <seealso cref="SKPoint"/>
public static class SKPointExtensions
{
	/// <param name="this">The current instance.</param>
	extension(in SKPoint @this)
	{
		/// <summary>
		/// Deconstruct instance into multiple values.
		/// </summary>
		public void Deconstruct(out float x, out float y) => (x, y) = (@this.X, @this.Y);

		/// <inheritdoc cref="IComparable{T}.CompareTo(T)"/>
		public int CompareTo(in SKPoint other) => @this.X.CompareTo(other.X) is var r1 and not 0 ? r1 : @this.Y.CompareTo(other.Y);

		/// <summary>
		/// Centralize the point using the specified font, in order to draw text aligning with center in vertical orientation.
		/// </summary>
		/// <param name="font">The font.</param>
		/// <returns>The changed result.</returns>
		public SKPoint CentralizeAsFont(SKFont font)
		{
			var result = @this;
			var textMetrics = font.Metrics;
			result.Y += (textMetrics.Ascent + textMetrics.Descent) / 2; // Baseline adjustment.
			result.Y += font.Size / 2; // Move to center of Y.
			return result;
		}


		/// <inheritdoc cref="IComparisonOperators{TSelf, TOther, TResult}.op_GreaterThan(TSelf, TOther)"/>
		public static bool operator >(in SKPoint left, in SKPoint right) => left.CompareTo(right) > 0;

		/// <inheritdoc cref="IComparisonOperators{TSelf, TOther, TResult}.op_LessThan(TSelf, TOther)"/>
		public static bool operator <(in SKPoint left, in SKPoint right) => left.CompareTo(right) < 0;

		/// <inheritdoc cref="IComparisonOperators{TSelf, TOther, TResult}.op_GreaterThanOrEqual(TSelf, TOther)"/>
		public static bool operator >=(in SKPoint left, in SKPoint right) => left.CompareTo(right) >= 0;

		/// <inheritdoc cref="IComparisonOperators{TSelf, TOther, TResult}.op_LessThanOrEqual(TSelf, TOther)"/>
		public static bool operator <=(in SKPoint left, in SKPoint right) => left.CompareTo(right) <= 0;

		/// <summary>
		/// Returns the current instance directly; just a symmetry definition
		/// with operator <see cref="extension(in SKPoint).op_UnaryNegation(in SKPoint)"/>.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns>The result.</returns>
		/// <seealso cref="extension(in SKPoint).op_UnaryNegation(in SKPoint)"/>
		public static SKPoint operator +(in SKPoint value) => value;

		/// <summary>
		/// Negates all factors of the current instance.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns>The result negated.</returns>
		public static SKPoint operator -(in SKPoint value) => new(-value.X, -value.Y);
	}
}
