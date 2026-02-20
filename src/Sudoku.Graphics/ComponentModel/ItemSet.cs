namespace Sudoku.ComponentModel;

/// <summary>
/// Represents a set of <see cref="Item"/> instances.
/// </summary>
/// <seealso cref="Item"/>
public sealed partial class ItemSet :
	IBitwiseOperators<ItemSet, ItemSet, ItemSet>,
	ICloneable,
	IEquatable<ItemSet>,
	IEqualityOperators<ItemSet, ItemSet, bool>,
	ISet<Item>,
	ISubtractionOperators<ItemSet, ItemSet, ItemSet>,
	IReadOnlySet<Item>
{
	/// <summary>
	/// Indicates the backing lookup dictionary of items, grouped by ites layer priority property, ordered.
	/// </summary>
	private readonly SortedDictionary<ItemType, HashSet<Item>> _itemsLookup = [];


	/// <summary>
	/// Indicates the number of items stored in this collection.
	/// </summary>
	public int Count => _itemsLookup.Values.Sum(Enumerable.Count);

	/// <inheritdoc/>
	bool ICollection<Item>.IsReadOnly => false;


	/// <summary>
	/// Creates a new <see cref="ItemSet"/> instance.
	/// </summary>
	public static ItemSet Empty => [];

	/// <summary>
	/// Returns an <see cref="IEqualityComparer{T}"/> of <see cref="HashSet{T}"/> of <see cref="Item"/> instance
	/// to be considered as equality comparison of items.
	/// </summary>
	private static IEqualityComparer<HashSet<Item>> HashSetItemsComparer => field ??= HashSet<Item>.CreateSetComparer();


	/// <summary>
	/// Returns an instance of items filtered, with the specified item type.
	/// If the target item type cannot be found in the collection, an empty sequence will be returned without throwing exceptions.
	/// </summary>
	/// <param name="type">The item type.</param>
	/// <returns>The filtered items that supports iteration and viewing.</returns>
	public FilteredItems this[ItemType type] => new(type, _itemsLookup.TryGetValue(type, out var items) ? items : []);


	/// <summary>
	/// Performs an iteration of the current collection, applying the specified operation over each element.
	/// </summary>
	/// <param name="action">An action to be applied to all elements.</param>
	public void ForEach(Action<Item> action)
	{
		foreach (var item in this)
		{
			action(item);
		}
	}

	/// <summary>
	/// Clears the whole collection, removing all stored elements.
	/// </summary>
	public void Clear() => _itemsLookup.Clear();

	/// <summary>
	/// Clears all elements of the specified item type.
	/// </summary>
	/// <param name="type">The item type to remove.</param>
	public void Clear(ItemType type) => _itemsLookup.Remove(type);

	/// <inheritdoc/>
	public override bool Equals([NotNullWhen(true)] object? obj) => Equals(obj as ItemSet);

	/// <inheritdoc/>
	public bool Equals([NotNullWhen(true)] ItemSet? other)
	{
		if (other is null)
		{
			return false;
		}

		if (_itemsLookup.Keys.Count != other._itemsLookup.Keys.Count)
		{
			return false;
		}

		foreach (var key in _itemsLookup.Keys)
		{
			if (!other._itemsLookup.TryGetValue(key, out var otherValues))
			{
				return false;
			}
			if (!HashSetItemsComparer.Equals(_itemsLookup[key], otherValues))
			{
				return false;
			}
		}

		return true;
	}

	/// <summary>
	/// Adds a new item into the current collection.
	/// </summary>
	/// <param name="item">The item to add.</param>
	/// <returns>A <see cref="bool"/> result indicating whether the item is successfully added.</returns>
	public bool Add(Item item)
	{
		var type = item.Type;
		return _itemsLookup.TryAdd(type, [item]) || _itemsLookup[type].Add(item);
	}

	/// <summary>
	/// Removes an item from the current collection.
	/// </summary>
	/// <param name="item">The item to remove.</param>
	/// <returns>A <see cref="bool"/> result indicating whether the item is successfully removed.</returns>
	public bool Remove(Item item)
	{
		var type = item.Type;
		return _itemsLookup.TryGetValue(type, out var items) && items.Remove(item);
	}

	/// <summary>
	/// Determines whether the specified item is stored in this collection, of value equality.
	/// </summary>
	/// <param name="item">The item to check.</param>
	/// <returns>A <see cref="bool"/> result indicating whether the item exists or not.</returns>
	public bool Contains(Item item) => _itemsLookup.TryGetValue(item.Type, out var items) && items.Contains(item);

	/// <inheritdoc/>
	public override int GetHashCode()
	{
		var hashCode = new HashCode();
		foreach (var key in _itemsLookup.Keys)
		{
			hashCode.Add(key);
			hashCode.Add(HashSetItemsComparer.GetHashCode(_itemsLookup[key]));
		}
		return hashCode.ToHashCode();
	}

	/// <summary>
	/// Adds a list of items into the current collection.
	/// </summary>
	/// <param name="items">A list of items to add.</param>
	/// <returns>An <see cref="int"/> value indicating how many items are successfully added.</returns>
	public int AddRange(params ReadOnlySpan<Item> items)
	{
		var result = 0;
		foreach (var item in items)
		{
			if (Add(item))
			{
				result++;
			}
		}
		return result;
	}

	/// <summary>
	/// Adds a list of items into the current collection.
	/// </summary>
	/// <typeparam name="TEnumerable">The type of enumerable sequence of <see cref="Item"/> instances.</typeparam>
	/// <param name="items">A list of items to add.</param>
	/// <returns>An <see cref="int"/> value indicating how many items are successfully added.</returns>
	public int AddRange<TEnumerable>(TEnumerable items) where TEnumerable : IEnumerable<Item>, allows ref struct
	{
		var result = 0;
		foreach (var item in items)
		{
			if (Add(item))
			{
				result++;
			}
		}
		return result;
	}

	/// <inheritdoc cref="IEnumerable{T}.GetEnumerator"/>
	public Enumerator GetEnumerator() => new(_itemsLookup);

	/// <summary>
	/// Returns an enumerable object that can iterate on each item of the specified item type.
	/// </summary>
	/// <param name="type">The item type.</param>
	/// <returns>An enumerator object.</returns>
	public FilteredItemsEnumerator EnumerateItemsOf(ItemType type) => new(_itemsLookup.TryGetValue(type, out var values) ? values : []);

	/// <summary>
	/// Converts the current instance into an array, and return that array.
	/// </summary>
	/// <returns>The array instance.</returns>
	public Item[] ToArray()
	{
		var result = new Item[Count];
		var i = 0;
		foreach (var item in this)
		{
			result[i++] = item;
		}
		return result;
	}

	/// <summary>
	/// Creates a new <see cref="ItemSet"/> instance, containing same values as the current instance, shallow-copied.
	/// </summary>
	/// <returns>A new <see cref="ItemSet"/> instance.</returns>
	public ItemSet Clone()
	{
		var result = Empty;
		result.AddRange(this);
		return result;
	}

	/// <summary>
	/// Flattens the current collection into a read-only set.
	/// </summary>
	/// <returns>The read-only set flattened.</returns>
	public IReadOnlySet<Item> Flatten()
	{
		var result = new HashSet<Item>();
		foreach (var element in this)
		{
			Add(element);
		}
		return result;
	}

	/// <inheritdoc/>
	void ICollection<Item>.Add(Item item) => Add(item);

	/// <inheritdoc/>
	void ICollection<Item>.CopyTo(Item[] array, int arrayIndex)
	{
		var span = array.AsSpan()[arrayIndex..];
		if (span.Length >= Count)
		{
			var i = 0;
			foreach (var element in this)
			{
				span[i++] = element;
			}
		}
		throw new ArgumentException("The space is not enough, or array index is thought to be too large that makes the last space not enough.");
	}

	/// <inheritdoc/>
	void ISet<Item>.ExceptWith(IEnumerable<Item> other) => throw new NotImplementedException();

	/// <inheritdoc/>
	void ISet<Item>.IntersectWith(IEnumerable<Item> other) => throw new NotImplementedException();

	/// <inheritdoc/>
	object ICloneable.Clone() => Clone();

	/// <inheritdoc/>
	bool IReadOnlySet<Item>.SetEquals(IEnumerable<Item> other) => Flatten().SetEquals(other);

	/// <inheritdoc/>
	bool IReadOnlySet<Item>.IsProperSubsetOf(IEnumerable<Item> other) => Flatten().IsProperSubsetOf(other);

	/// <inheritdoc/>
	bool IReadOnlySet<Item>.IsProperSupersetOf(IEnumerable<Item> other) => Flatten().IsProperSupersetOf(other);

	/// <inheritdoc/>
	bool IReadOnlySet<Item>.IsSubsetOf(IEnumerable<Item> other) => Flatten().IsSubsetOf(other);

	/// <inheritdoc/>
	bool IReadOnlySet<Item>.IsSupersetOf(IEnumerable<Item> other) => Flatten().IsSupersetOf(other);

	/// <inheritdoc/>
	bool IReadOnlySet<Item>.Overlaps(IEnumerable<Item> other) => Flatten().Overlaps(other);

	/// <inheritdoc/>
	bool ISet<Item>.SetEquals(IEnumerable<Item> other) => ((IReadOnlySet<Item>)this).SetEquals(other);

	/// <inheritdoc/>
	bool ISet<Item>.IsProperSubsetOf(IEnumerable<Item> other) => ((IReadOnlySet<Item>)this).IsProperSubsetOf(other);

	/// <inheritdoc/>
	bool ISet<Item>.IsProperSupersetOf(IEnumerable<Item> other) => ((IReadOnlySet<Item>)this).IsProperSupersetOf(other);

	/// <inheritdoc/>
	bool ISet<Item>.IsSubsetOf(IEnumerable<Item> other) => ((IReadOnlySet<Item>)this).IsSubsetOf(other);

	/// <inheritdoc/>
	bool ISet<Item>.IsSupersetOf(IEnumerable<Item> other) => ((IReadOnlySet<Item>)this).IsSupersetOf(other);

	/// <inheritdoc/>
	bool ISet<Item>.Overlaps(IEnumerable<Item> other) => ((IReadOnlySet<Item>)this).Overlaps(other);

	/// <inheritdoc/>
	void ISet<Item>.SymmetricExceptWith(IEnumerable<Item> other) => ((ISet<Item>)Flatten()).SymmetricExceptWith(other);

	/// <inheritdoc/>
	void ISet<Item>.UnionWith(IEnumerable<Item> other) => ((ISet<Item>)Flatten()).UnionWith(other);

	/// <inheritdoc/>
	IEnumerator IEnumerable.GetEnumerator() => ToArray().GetEnumerator();

	/// <inheritdoc/>
	IEnumerator<Item> IEnumerable<Item>.GetEnumerator() => ToArray().AsEnumerable().GetEnumerator();


	/// <summary>
	/// Performs subtraction and reassigns the result into the current instance.
	/// </summary>
	/// <param name="items">The items to check.</param>
	public void operator -=(ItemSet items)
	{
		foreach (var element in ToArray())
		{
			if (items.Contains(element))
			{
				Remove(element);
			}
		}
	}

	/// <summary>
	/// Performs intersection and reassigns the result into the current instance.
	/// </summary>
	/// <param name="items">The items to check.</param>
	public void operator &=(ItemSet items)
	{
		foreach (var element in ToArray())
		{
			if (!items.Contains(element))
			{
				Remove(element);
			}
		}
	}

	/// <summary>
	/// Performs union and reassigns the result into the current instance.
	/// </summary>
	/// <param name="items">The items to check.</param>
	public void operator |=(ItemSet items) => AddRange(items);

	/// <summary>
	/// Performs symmetric except operation and reassigns the result into the current instance.
	/// </summary>
	/// <param name="items">The items to check.</param>
	public void operator ^=(ItemSet items)
	{
		var target = this ^ items;
		Clear();
		AddRange(target);
	}


	/// <inheritdoc/>
	public static bool operator ==(ItemSet? left, ItemSet? right)
		=> (left, right) switch { (null, null) => true, (not null, not null) => left.Equals(right), _ => false };

	/// <inheritdoc/>
	public static bool operator !=(ItemSet? left, ItemSet? right) => !(left == right);

	/// <summary>
	/// Returns a list of <see cref="Item"/> instances that are stored in <paramref name="left"/> but not in <paramref name="right"/>.
	/// </summary>
	/// <param name="left">The first instance.</param>
	/// <param name="right">The second instance.</param>
	/// <returns>The result.</returns>
	public static ItemSet operator -(ItemSet left, ItemSet right)
	{
		var result = Empty;
		foreach (var element in left)
		{
			if (!right.Contains(element))
			{
				result.Add(element);
			}
		}
		return result;
	}

	/// <summary>
	/// Returns a list of <see cref="Item"/> instances that are both held by <paramref name="left"/> and <paramref name="right"/>.
	/// </summary>
	/// <param name="left">The first instance.</param>
	/// <param name="right">The second instance.</param>
	/// <returns>The result.</returns>
	public static ItemSet operator &(ItemSet left, ItemSet right)
	{
		var result = left.Clone();
		foreach (var element in left)
		{
			if (!right.Contains(element))
			{
				result.Remove(element);
			}
		}
		return result;
	}

	/// <summary>
	/// Returns a list of <see cref="Item"/> instances that are
	/// from both collections <paramref name="left"/> and <paramref name="right"/>.
	/// </summary>
	/// <param name="left">The first instance.</param>
	/// <param name="right">The second instance.</param>
	/// <returns>The result.</returns>
	public static ItemSet operator |(ItemSet left, ItemSet right)
	{
		var result = left.Clone();
		result.AddRange(right);
		return result;
	}

	/// <summary>
	/// Returns a list of <see cref="Item"/> instances that are
	/// only held by either collection <paramref name="left"/> or <paramref name="right"/>;
	/// both contains are not included in result.
	/// </summary>
	/// <param name="left">The first instance.</param>
	/// <param name="right">The second instance.</param>
	/// <returns>The result.</returns>
	public static ItemSet operator ^(ItemSet left, ItemSet right)
	{
		var result = Empty;
		result.AddRange(left - right);
		result.AddRange(right - left);
		return result;
	}

	/// <inheritdoc/>
	/// <exception cref="NotSupportedException">
	/// Always throws. The complement of <see cref="Item"/> set is not well-defined.
	/// </exception>
	[DoesNotReturn]
	static ItemSet IBitwiseOperators<ItemSet, ItemSet, ItemSet>.operator ~(ItemSet value) => throw new NotSupportedException();
}
