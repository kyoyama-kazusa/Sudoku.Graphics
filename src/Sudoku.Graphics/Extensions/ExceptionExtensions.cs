namespace System;

/// <summary>
/// Provides extension members on <see cref="Exception"/>.
/// </summary>
/// <seealso cref="Exception"/>
public static class ExceptionExtensions
{
	extension<TException>(TException) where TException : Exception
	{
		/// <summary>
		/// Asserts the condition. If failed, an exception of type <typeparamref name="TException"/> will be thrown,
		/// with the specified message shown.
		/// </summary>
		/// <param name="expression">The expression.</param>
		/// <param name="expressionString">The expression string.</param>
		public static void Assert(bool expression, [CallerArgumentExpression(nameof(expression))] string expressionString = null!)
		{
			if (!expression)
			{
				throw (TException)Activator.CreateInstance(typeof(TException), $"The expression is failed: '{expressionString}'.")!;
			}
		}
	}
}
