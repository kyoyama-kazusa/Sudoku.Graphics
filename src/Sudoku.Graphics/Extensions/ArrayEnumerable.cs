namespace System.Linq;

/// <summary>
/// Provides equivalent implementation of <see cref="Enumerable"/>, of type <see cref="Array"/>.
/// </summary>
/// <seealso cref="Array"/>
public static class ArrayEnumerable
{
	/// <param name="source">
	/// <inheritdoc cref="Enumerable.Index{TSource}(IEnumerable{TSource})" path="/param[@name='source']"/>
	/// </param>
	extension<TSource>(TSource[] source)
	{
		/// <inheritdoc cref="Enumerable.Index{TSource}(IEnumerable{TSource})"/>
		public (int Index, TSource Value)[] Index()
		{
			var result = new (int, TSource)[source.Length];
			var i = 0;
			foreach (var element in source)
			{
				result[i] = (i++, element);
			}
			return result;
		}
	}
}
