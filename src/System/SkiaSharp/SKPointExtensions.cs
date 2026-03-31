namespace SkiaSharp;

/// <summary>
/// Provdies extension members on <see cref="SKPoint"/>.
/// </summary>
/// <seealso cref="SKPoint"/>
public static class SKPointExtensions
{
	/// <summary>
	/// Provides immutable extension members on <see cref="SKPoint"/> instances.
	/// </summary>
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
		public SKPoint AlignYAsBaseline(SKFont font)
		{
			var result = @this;
			var textMetrics = font.Metrics;
			result.Y -= (textMetrics.Ascent + textMetrics.Descent) / 2;
			return result;
		}


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

		/// <inheritdoc cref="IAdditionOperators{TSelf, TOther, TResult}.op_Addition(TSelf, TOther)"/>
		public static SKPoint operator +(in SKPoint left, (float X, float Y) right) => new(left.X + right.X, left.Y + right.Y);

		/// <inheritdoc cref="ISubtractionOperators{TSelf, TOther, TResult}.op_Subtraction(TSelf, TOther)"/>
		public static SKPoint operator -(in SKPoint left, (float X, float Y) right) => new(left.X - right.X, left.Y - right.Y);

		/// <inheritdoc cref="IComparisonOperators{TSelf, TOther, TResult}.op_GreaterThan(TSelf, TOther)"/>
		public static bool operator >(in SKPoint left, in SKPoint right) => left.CompareTo(right) > 0;

		/// <inheritdoc cref="IComparisonOperators{TSelf, TOther, TResult}.op_LessThan(TSelf, TOther)"/>
		public static bool operator <(in SKPoint left, in SKPoint right) => left.CompareTo(right) < 0;

		/// <inheritdoc cref="IComparisonOperators{TSelf, TOther, TResult}.op_GreaterThanOrEqual(TSelf, TOther)"/>
		public static bool operator >=(in SKPoint left, in SKPoint right) => left.CompareTo(right) >= 0;

		/// <inheritdoc cref="IComparisonOperators{TSelf, TOther, TResult}.op_LessThanOrEqual(TSelf, TOther)"/>
		public static bool operator <=(in SKPoint left, in SKPoint right) => left.CompareTo(right) <= 0;
	}

	/// <summary>
	/// Provides some extension members on <see cref="SKPoint"/> instances, that may change the containing states of those instances.
	/// </summary>
	/// <param name="this">The current instance.</param>
	extension(ref SKPoint @this)
	{
		/// <summary>
		/// Adds the specified value into the current instance.
		/// </summary>
		/// <param name="value">The value that is added to the current instance.</param>
		public void operator +=((float X, float Y) value)
		{
			@this.X += value.X;
			@this.Y += value.Y;
		}

		/// <summary>
		/// Adds the negated value of the specified value into the current instance.
		/// </summary>
		/// <param name="value">The value that is subtracted from the current instance.</param>
		public void operator -=((float X, float Y) value)
		{
			@this.X -= value.X;
			@this.Y -= value.Y;
		}
	}
}
