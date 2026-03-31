namespace Sudoku.Graphics;

/// <summary>
/// Provides extension members on both <see cref="Thickness"/> and <see cref="Thickness{T}"/>.
/// </summary>
/// <seealso cref="Thickness"/>
/// <seealso cref="Thickness{T}"/>
public static class ThicknessExtensions
{
	/// <summary>
	/// Provides extension operators on <see cref="Thickness{T}"/>,
	/// where <typeparamref name="T"/> implements <see cref="IInteger{TSelf}"/>.
	/// </summary>
	/// <typeparam name="T">The type of each factor defined in type <see cref="Thickness{T}"/>.</typeparam>
	extension<T>(Thickness<T>) where T : struct, IInteger<T>
	{
		/// <inheritdoc cref="IAdditionOperators{TSelf, TOther, TResult}.op_Addition(TSelf, TOther)"/>
		public static Thickness<T> operator +(Thickness<T> left, Thickness<T> right)
			=> new(left.Left + right.Left, left.Top + right.Top, left.Right + right.Right, left.Bottom + right.Bottom);

		/// <inheritdoc cref="IAdditionOperators{TSelf, TOther, TResult}.op_CheckedAddition(TSelf, TOther)"/>
		public static Thickness<T> operator checked +(Thickness<T> left, Thickness<T> right)
			=> new(
				checked(left.Left + right.Left),
				checked(left.Top + right.Top),
				checked(left.Right + right.Right),
				checked(left.Bottom + right.Bottom)
			);

		/// <inheritdoc cref="ISubtractionOperators{TSelf, TOther, TResult}.op_Subtraction(TSelf, TOther)"/>
		public static Thickness<T> operator -(Thickness<T> left, Thickness<T> right)
			=> new(left.Left - right.Left, left.Top - right.Top, left.Right - right.Right, left.Bottom - right.Bottom);

		/// <inheritdoc cref="ISubtractionOperators{TSelf, TOther, TResult}.op_CheckedSubtraction(TSelf, TOther)"/>
		public static Thickness<T> operator checked -(Thickness<T> left, Thickness<T> right)
			=> new(
				checked(left.Left - right.Left),
				checked(left.Top - right.Top),
				checked(left.Right - right.Right),
				checked(left.Bottom - right.Bottom)
			);
	}
}
