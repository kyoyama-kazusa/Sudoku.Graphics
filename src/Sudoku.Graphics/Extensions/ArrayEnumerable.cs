namespace System.Linq;

/// <summary>
/// Provides equivalent implementation of <see cref="Enumerable"/>, of type <see cref="Array"/>.
/// </summary>
/// <seealso cref="Array"/>
public static class ArrayEnumerable
{
	extension<TSource>(TSource[] source)
	{
		/// <inheritdoc cref="Enumerable.Index{TSource}(IEnumerable{TSource})"/>
		public (int Index, TSource Value)[] Index()
		{
			var result = new (int, TSource)[source.Length];
			var i = 0;
			foreach (var element in source)
			{
				result[i++] = (i, element);
			}
			return result;
		}
	}
}
