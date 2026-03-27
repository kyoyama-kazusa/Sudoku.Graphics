namespace Sudoku.Graphics;

public partial class ItemTypeOrdering
{
	/// <summary>
	/// Represents an enumerator instance that can iterate one each values in the current instance of the containing type.
	/// </summary>
	/// <param name="_enumerator">The backing enumerator.</param>
	public ref struct Enumerator(SortedDictionary<ItemType, int>.Enumerator _enumerator) : IEnumerator<KeyValuePair<ItemType, int>>
	{
		/// <inheritdoc cref="IEnumerator{T}.Current"/>
		public readonly (ItemType Key, int Value) Current
		{
			get
			{
				var (k, v) = _enumerator.Current;
				return (k, v);
			}
		}

		/// <inheritdoc/>
		readonly object IEnumerator.Current => _enumerator.Current;

		/// <inheritdoc/>
		readonly KeyValuePair<ItemType, int> IEnumerator<KeyValuePair<ItemType, int>>.Current => _enumerator.Current;


		/// <inheritdoc/>
		public bool MoveNext() => _enumerator.MoveNext();

		/// <inheritdoc/>
		readonly void IEnumerator.Reset() => ((IEnumerator)_enumerator).Reset();

		/// <inheritdoc/>
		void IDisposable.Dispose() => _enumerator.Dispose();
	}
}
