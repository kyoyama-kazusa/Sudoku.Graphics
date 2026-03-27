namespace Sudoku.Graphics;

/// <summary>
/// Represents ordering of item type to be rendered.
/// </summary>
public sealed partial class ItemTypeOrdering : ICloneable, IReadOnlyDictionary<ItemType, int>
{
	/// <summary>
	/// Indicates the default ordering.
	/// </summary>
	public static readonly ItemTypeOrdering Default;


	/// <summary>
	/// The backing dictionary.
	/// </summary>
	private readonly SortedDictionary<ItemType, int> _orderingDictionary = [];

	/// <summary>
	/// Indicates the keys.
	/// </summary>
	private readonly SortedSet<ItemType> _keys = [];


	/// <summary>
	/// The static constructor of this type.
	/// </summary>
	static ItemTypeOrdering()
	{
		var defaultInstance = new ItemTypeOrdering();
		var priorityValue = 0;
		foreach (var field in DefaultEnumeration.DefaultEnumerateItemTypes())
		{
			defaultInstance.Add(field, priorityValue++);
		}
		Default = defaultInstance;
	}


	/// <inheritdoc/>
	public int Count => _orderingDictionary.Count;

	/// <summary>
	/// Indicates the keys in the collection.
	/// </summary>
	public ReadOnlySpan<ItemType> Keys => _keys.ToArray();

	/// <summary>
	/// Indicates the values in the collection.
	/// </summary>
	public ReadOnlySpan<int> Values => from key in Keys select _orderingDictionary[key];

	/// <inheritdoc/>
	IEnumerable<int> IReadOnlyDictionary<ItemType, int>.Values => _orderingDictionary.Values;

	/// <inheritdoc/>
	IEnumerable<ItemType> IReadOnlyDictionary<ItemType, int>.Keys => _orderingDictionary.Keys;


	/// <summary>
	/// Gets the priority value of the specified item type.
	/// </summary>
	/// <param name="type">The type.</param>
	/// <returns>The priority value.</returns>
	public int this[ItemType type] => _orderingDictionary[type];


	/// <summary>
	/// Adds a new item type and ordering value into the current collection.
	/// </summary>
	/// <param name="key">The type.</param>
	/// <param name="value">The ordering value.</param>
	public void Add(ItemType key, int value)
	{
		_orderingDictionary.Add(key, value);
		_keys.Add(key);
	}

	/// <inheritdoc/>
	public bool ContainsKey(ItemType key) => _keys.Contains(key);

	/// <inheritdoc/>
	public bool TryGetValue(ItemType key, out int value)
	{
		if (_keys.Contains(key))
		{
			value = _orderingDictionary[key];
			return true;
		}
		value = default;
		return false;
	}

	/// <inheritdoc/>
	public override string ToString() => _orderingDictionary.ToDictionaryString();

	/// <inheritdoc cref="IEnumerable{T}.GetEnumerator"/>
	public Enumerator GetEnumerator() => new(_orderingDictionary.GetEnumerator());

	/// <inheritdoc cref="ICloneable.Clone"/>
	public ItemTypeOrdering Clone()
	{
		var result = new ItemTypeOrdering();
		foreach (var (k, v) in this)
		{
			result.Add(k, v);
		}
		return result;
	}

	/// <inheritdoc/>
	object ICloneable.Clone() => Clone();

	/// <inheritdoc/>
	IEnumerator IEnumerable.GetEnumerator() => _orderingDictionary.GetEnumerator();

	/// <inheritdoc/>
	IEnumerator<KeyValuePair<ItemType, int>> IEnumerable<KeyValuePair<ItemType, int>>.GetEnumerator()
		=> _orderingDictionary.GetEnumerator();
}
