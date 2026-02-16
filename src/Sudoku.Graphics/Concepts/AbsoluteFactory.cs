namespace Sudoku.Concepts;

/// <summary>
/// Provides an easy way to create <see cref="Absolute"/> instances.
/// Generally it will be used by creating <see cref="LineSegment"/> instances.
/// </summary>
/// <seealso cref="Absolute"/>
/// <seealso cref="LineSegment"/>
public static class AbsoluteFactory
{
	/// <param name="this">The current instance.</param>
	extension(Absolute[] @this)
	{
		/// <summary>
		/// Get offset of the specified array of cell indices.
		/// </summary>
		/// <param name="rowsCount">The number of rows as offset.</param>
		/// <param name="columnsCount">The number of columns as offset.</param>
		/// <param name="mapper">The mapper instance.</param>
		/// <returns>The result.</returns>
		public Absolute[] Offset(Absolute rowsCount, Absolute columnsCount, PointMapper mapper)
		{
			var absoluteColumnsCount = mapper.AbsoluteColumnsCount;
			var result = new Absolute[@this.Length];
			var i = 0;
			foreach (var cell in @this)
			{
				var r = cell / absoluteColumnsCount;
				var c = cell % absoluteColumnsCount;
				result[i++] = (r + rowsCount) * absoluteColumnsCount + c + columnsCount;
			}
			return result;
		}
	}
}
