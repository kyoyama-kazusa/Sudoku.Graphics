namespace System.Pipelines;

/// <summary>
/// Provides a way to use pipeline.
/// </summary>
public static class Pipeline
{
	/// <summary>
	/// Provides extension members on type <typeparamref name="T"/>, and return instances of type <typeparamref name="TResult"/>.
	/// </summary>
	/// <typeparam name="T">The type of current instance.</typeparam>
	/// <typeparam name="TResult">The type of result instance.</typeparam>
	extension<T, TResult>(T) where T : allows ref struct where TResult : allows ref struct
	{
		/// <summary>
		/// Do operation <c>left |&gt; right</c> (i.e. <c>right(left)</c>).
		/// </summary>
		/// <param name="instance">The instance.</param>
		/// <param name="converter">The converter.</param>
		/// <returns>The result.</returns>
		public static TResult operator >>(T instance, Converter<T, TResult> converter) => converter(instance);
	}
}
