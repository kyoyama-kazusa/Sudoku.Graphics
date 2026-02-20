namespace Sudoku.ComponentModel;

public partial class ItemSet
{
	/// <summary>
	/// Returns a subview under the specified item type.
	/// </summary>
	/// <param name="type">The type.</param>
	/// <param name="_items">The items.</param>
	public readonly ref struct FilteredItems(ItemType type, HashSet<Item> _items) : IReadOnlySet<Item>
	{
		/// <inheritdoc/>
		public int Count => _items.Count;

		/// <summary>
		/// Indicates the target type.
		/// </summary>
		public ItemType ItemType { get; } = type;

		/// <summary>
		/// Indicates the items.
		/// </summary>
		public IReadOnlySet<Item> Items => _items;


		/// <inheritdoc cref="IEnumerable{T}.GetEnumerator"/>
		public FilteredItemsEnumerator GetEnumerator() => new(_items);

		/// <inheritdoc/>
		bool IReadOnlySet<Item>.Contains(Item item) => _items.Contains(item);

		/// <inheritdoc/>
		bool IReadOnlySet<Item>.IsProperSubsetOf(IEnumerable<Item> other) => _items.IsProperSubsetOf(other);

		/// <inheritdoc/>
		bool IReadOnlySet<Item>.IsProperSupersetOf(IEnumerable<Item> other) => _items.IsProperSupersetOf(other);

		/// <inheritdoc/>
		bool IReadOnlySet<Item>.IsSubsetOf(IEnumerable<Item> other) => _items.IsSubsetOf(other);

		/// <inheritdoc/>
		bool IReadOnlySet<Item>.IsSupersetOf(IEnumerable<Item> other) => _items.IsSupersetOf(other);

		/// <inheritdoc/>
		bool IReadOnlySet<Item>.Overlaps(IEnumerable<Item> other) => _items.Overlaps(other);

		/// <inheritdoc/>
		bool IReadOnlySet<Item>.SetEquals(IEnumerable<Item> other) => _items.SetEquals(other);

		/// <inheritdoc/>
		IEnumerator IEnumerable.GetEnumerator() => _items.GetEnumerator();

		/// <inheritdoc/>
		IEnumerator<Item> IEnumerable<Item>.GetEnumerator() => _items.GetEnumerator();
	}
}
