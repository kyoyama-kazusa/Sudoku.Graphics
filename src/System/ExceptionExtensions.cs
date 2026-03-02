namespace System;

/// <summary>
/// Provides extension members on <see cref="Exception"/>.
/// </summary>
/// <seealso cref="Exception"/>
public static class ExceptionExtensions
{
	extension(ArgumentOutOfRangeException)
	{
		/// <summary>
		/// Throws an <see cref="ArgumentOutOfRangeException"/> instance
		/// if the specified field of type <typeparamref name="TEnum"/> is undefined in its containing enumeration type.
		/// </summary>
		/// <typeparam name="TEnum">The type of enumeration.</typeparam>
		/// <param name="field">The field.</param>
		/// <param name="includesDefaultLiteral">
		/// Indicates whether <see langword="default"/>(<typeparamref name="TEnum"/>) is included to be checked,
		/// being treated as undefined. By default it's <see langword="false"/>.
		/// </param>
		/// <param name="expressionString">The expression string of <paramref name="field"/>.</param>
		/// <exception cref="ArgumentOutOfRangeException">Throws when <paramref name="field"/> is not defined.</exception>
		public static void ThrowIfUndefined<TEnum>(
			TEnum field,
			bool includesDefaultLiteral = false,
			[CallerArgumentExpression(nameof(field))] string expressionString = null!
		) where TEnum : unmanaged, Enum
		{
			if (!Enum.IsDefined(field) || includesDefaultLiteral && field.Equals(default(TEnum)))
			{
				throw new ArgumentOutOfRangeException(expressionString);
			}
		}
	}

	extension<TException>(TException) where TException : Exception
	{
		/// <summary>
		/// Asserts the condition. If failed, an exception of type <typeparamref name="TException"/> will be thrown,
		/// with the specified message shown.
		/// </summary>
		/// <param name="expression">The expression.</param>
		/// <param name="expressionString">The expression string.</param>
		public static void Assert(
			[DoesNotReturnIf(false)] bool expression,
			[CallerArgumentExpression(nameof(expression))] string expressionString = null!
		)
		{
			if (!expression)
			{
				throw (TException)Activator.CreateInstance(typeof(TException), $"The expression is failed: '{expressionString}'.")!;
			}
		}
	}
}
