namespace Sudoku.ComponentModel;

public partial class Template
{
	/// <summary>
	/// Represents an enumerator instance that can iterate on each cell index as <see cref="Absolute"/> instance.
	/// </summary>
	/// <param name="_template">The template.</param>
	/// <seealso cref="Absolute"/>
	public ref struct CellEnumerator(Template _template) : IEnumerable<Absolute>, IEnumerator<Absolute>
	{
		/// <summary>
		/// Indicates the relative index.
		/// </summary>
		private Relative _index = -1;

		/// <inheritdoc/>
		public readonly Absolute Current => _template.Mapper.GetAbsoluteIndex(_index);

		/// <inheritdoc/>
		readonly object IEnumerator.Current => Current;

		/// <summary>
		/// Indicates the number of cells.
		/// </summary>
		private readonly Absolute CellsCount => _template.Mapper.TemplateSize.RowsCount * _template.Mapper.TemplateSize.ColumnsCount;


		/// <inheritdoc/>
		public bool MoveNext() => ++_index < CellsCount;

		/// <inheritdoc cref="IEnumerable{T}.GetEnumerator"/>
		public readonly CellEnumerator GetEnumerator() => this;

		/// <inheritdoc/>
		readonly void IDisposable.Dispose()
		{
		}

		/// <inheritdoc/>
		[DoesNotReturn]
		readonly void IEnumerator.Reset() => throw new NotImplementedException();

		/// <inheritdoc/>
		readonly IEnumerator IEnumerable.GetEnumerator() => GetValuesFallback().GetEnumerator();

		/// <inheritdoc/>
		readonly IEnumerator<Absolute> IEnumerable<Absolute>.GetEnumerator() => GetValuesFallback().GetEnumerator();

		/// <summary>
		/// The fallback implementation of calculating all values.
		/// </summary>
		/// <returns>A list of result values.</returns>
		private readonly List<Absolute> GetValuesFallback()
		{
			var result = new List<Absolute>();
			for (var i = 0; i < CellsCount; i++)
			{
				result.Add(_template.Mapper.GetAbsoluteIndex(i));
			}
			return result;
		}
	}
}
