namespace Sudoku.ComponentModel;

public partial class ItemSet
{
	/// <summary>
	/// Represents an enumerator type that iterates on each <see cref="Item"/> instances, grouped and sorted by its key (item type).
	/// </summary>
	/// <param name="_items">The items.</param>
	/// <seealso cref="Item"/>
	public ref struct Enumerator(SortedDictionary<ItemType, HashSet<Item>> _items) : IEnumerable<Item>, IEnumerator<Item>
	{
		/// <summary>
		/// Indicates whether the enumerator is started or not.
		/// </summary>
		private bool _isStarted = false;

		/// <summary>
		/// Indicates the backing enumerator.
		/// </summary>
		private SortedDictionary<ItemType, HashSet<Item>>.Enumerator _enumerator = _items.GetEnumerator();

		/// <summary>
		/// The item type enumerator instance.
		/// </summary>
		private FilteredItemsEnumerator _itemTypeEnumerator;


		/// <summary>
		/// Indicates the current item type to be iterated.
		/// </summary>
		public ItemType CurrentItemType { get; private set; }

		/// <inheritdoc/>
		public Item Current { get; private set; } = null!;

		/// <inheritdoc/>
		readonly object IEnumerator.Current => Current;


		/// <inheritdoc cref="IEnumerable{T}.GetEnumerator"/>
		public readonly Enumerator GetEnumerator() => this;

		/// <inheritdoc/>
		public unsafe bool MoveNext()
		{
			if (!_isStarted)
			{
				// If not started, set the flag to true and start iteration.
				_isStarted = true;

				// Advance state.
				return advanceState(ref this, ref _enumerator, ref _itemTypeEnumerator, &itemTypeSetter, &currentValueSetter);
			}

			// If not, the operation is started. Now check item type enumerator directly.
			if (_itemTypeEnumerator.MoveNext())
			{
				Current = _itemTypeEnumerator.Current;
				return true;
			}

			// Advance state.
			return advanceState(ref this, ref _enumerator, ref _itemTypeEnumerator, &itemTypeSetter, &currentValueSetter);


			static void itemTypeSetter(ItemType type, ref Enumerator instance) => instance.CurrentItemType = type;

			static void currentValueSetter(Item value, ref Enumerator instance) => instance.Current = value;

			static bool advanceState(
				ref Enumerator instance,
				ref SortedDictionary<ItemType, HashSet<Item>>.Enumerator enumerator,
				ref FilteredItemsEnumerator itemTypeEnumerator,
				delegate*<ItemType, ref Enumerator, void> itemTypeUpdater,
				delegate*<Item, ref Enumerator, void> currentPropertyUpdater
			)
			{
				// Do a while loop to find for the first enumerated item type has any elements to be iterated.
				while (enumerator.MoveNext())
				{
					var (currentItemType, currentHashSet) = enumerator.Current;
					itemTypeUpdater(currentItemType, ref instance);

					// Check whether the collection is empty or not.
					if (currentHashSet.Count == 0)
					{
						continue;
					}

					// The collection has an arbitrary element to be iterated (non-empty collection) => valid collection.
					// Set the enumerator to the current hash set, and break this searching loop.
					itemTypeEnumerator = new(currentHashSet);
					if (itemTypeEnumerator.MoveNext())
					{
						currentPropertyUpdater(itemTypeEnumerator.Current, ref instance);
						return true;
					}
					throw new UnreachableException("Thought to be unreachable.");
				}

				// Failed to be iterated.
				return false;
			}
		}

		/// <inheritdoc/>
		readonly IEnumerator IEnumerable.GetEnumerator() => GetItemsFallback().GetEnumerator();

		/// <inheritdoc/>
		readonly IEnumerator<Item> IEnumerable<Item>.GetEnumerator() => GetItemsFallback().GetEnumerator();

		/// <inheritdoc/>
		void IEnumerator.Reset()
		{
			_isStarted = false;
			_enumerator = _items.GetEnumerator();
			_itemTypeEnumerator = default;
			Current = default!;
			CurrentItemType = default;
		}

		/// <inheritdoc/>
		void IDisposable.Dispose()
		{
			_isStarted = false;
			_enumerator.Dispose();
			_itemTypeEnumerator = default;
			Current = default!;
			CurrentItemType = default;
		}

		/// <summary>
		/// Returns a <see cref="List{T}"/> of <see cref="Item"/> instances to be iterated.
		/// </summary>
		/// <returns>A <see cref="List{T}"/> of <see cref="Item"/> instances to be iterated.</returns>
		private readonly List<Item> GetItemsFallback()
		{
			var list = new List<Item>();
			foreach (var key in _items.Keys)
			{
				foreach (var element in _items[key])
				{
					list.Add(element);
				}
			}
			return list;
		}
	}
}
