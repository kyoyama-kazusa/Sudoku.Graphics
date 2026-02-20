namespace Sudoku.ComponentModel;

public partial class ItemSet
{
	/// <summary>
	/// Represents an enumerator object that can iterate items of the specified item type.
	/// </summary>
	/// <param name="_items">The items.</param>
	public ref struct FilteredItemsEnumerator(HashSet<Item> _items) : IEnumerable<Item>, IEnumerator<Item>
	{
		/// <summary>
		/// Indicates the backing enumerator.
		/// </summary>
		private HashSet<Item>.Enumerator _enumerator = _items.GetEnumerator();


		/// <inheritdoc/>
		public readonly Item Current => _enumerator.Current;

		/// <inheritdoc/>
		readonly object IEnumerator.Current => Current;


		/// <inheritdoc cref="IEnumerable{T}.GetEnumerator"/>
		public readonly FilteredItemsEnumerator GetEnumerator() => this;

		/// <inheritdoc/>
		public bool MoveNext() => _enumerator.MoveNext();

		/// <inheritdoc/>
		readonly void IDisposable.Dispose()
		{
		}

		/// <inheritdoc/>
		[DoesNotReturn]
		readonly void IEnumerator.Reset() => throw new NotImplementedException();

		/// <inheritdoc/>
		readonly IEnumerator IEnumerable.GetEnumerator() => _enumerator;

		/// <inheritdoc/>
		readonly IEnumerator<Item> IEnumerable<Item>.GetEnumerator() => _enumerator;
	}
}
