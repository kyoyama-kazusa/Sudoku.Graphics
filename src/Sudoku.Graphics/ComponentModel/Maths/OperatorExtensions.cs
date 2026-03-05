namespace Sudoku.ComponentModel.Maths;

/// <summary>
/// Provides extension members on <see cref="ArithmeticOperator"/>, <see cref="BitwiseOperator"/>
/// and <see cref="ComparisonOperator"/>.
/// </summary>
/// <seealso cref="ArithmeticOperator"/>
/// <seealso cref="BitwiseOperator"/>
/// <seealso cref="ComparisonOperator"/>
public static class OperatorExtensions
{
	/// <typeparam name="TOperatorEnum">The type of this enumeration field.</typeparam>
	/// <param name="this">The current instance.</param>
	extension<TOperatorEnum>(TOperatorEnum @this) where TOperatorEnum : unmanaged, Enum
	{
		/// <inheritdoc cref="extension(ArithmeticOperator).Text"/>
		public string Text
			=> @this switch
			{
				ArithmeticOperator a => a.Text,
				BitwiseOperator b => b.Text,
				ComparisonOperator c => c.Text,
				_ => throw new NotSupportedException("The type of this enumeration field is not supported.")
			};
	}

	/// <param name="this">The current instance.</param>
	extension(ArithmeticOperator @this)
	{
		/// <summary>
		/// Indicates text of this operator.
		/// </summary>
		/// <exception cref="ArgumentOutOfRangeException">
		/// Throws when <paramref name="this"/> is not defined, or equals <see langword="default"/> value
		/// of this type (placeholder).
		/// </exception>
		public string Text
			=> @this switch
			{
				ArithmeticOperator.Add => "+",
				ArithmeticOperator.Subtract => "-",
				ArithmeticOperator.Multiply_Cross => "\u00d7",
				ArithmeticOperator.Multiply_Dot => "\u00b7",
				ArithmeticOperator.Multiply_Asterisk => "*",
				ArithmeticOperator.Division_DivisionSign => "\u00f7",
				ArithmeticOperator.Division_Slash => "/",
				ArithmeticOperator.Division_Ratio => ":",
				ArithmeticOperator.Modulo => "%",
				_ => throw new ArgumentOutOfRangeException(nameof(@this))
			};
	}

	/// <param name="this">The current instance.</param>
	extension(BitwiseOperator @this)
	{
		/// <inheritdoc cref="extension(ArithmeticOperator).Text"/>
		public string Text
			=> @this switch
			{
				BitwiseOperator.And => "&",
				BitwiseOperator.Or => "|",
				BitwiseOperator.Not => "~",
				BitwiseOperator.ExclusiveOr => "^",
				_ => throw new ArgumentOutOfRangeException(nameof(@this))
			};
	}

	/// <param name="this">The current instance.</param>
	extension(ComparisonOperator @this)
	{
		/// <inheritdoc cref="extension(ArithmeticOperator).Text"/>
		public string Text
			=> @this switch
			{
				ComparisonOperator.Equals => "=",
				ComparisonOperator.Inequals => "\u2260",
				ComparisonOperator.GreaterThan => ">",
				ComparisonOperator.GreaterThanOrEqual => "\u2265",
				ComparisonOperator.LessThan => "<",
				ComparisonOperator.LessThanOrEqual => "\u2264",
				_ => throw new ArgumentOutOfRangeException(nameof(@this))
			};
	}
}
