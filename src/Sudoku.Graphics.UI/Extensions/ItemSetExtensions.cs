namespace Sudoku.Graphics;

/// <summary>
/// Provides extension members on <see cref="ItemSet"/>.
/// </summary>
/// <seealso cref="ItemSet"/>
public static class ItemSetExtensions
{
	/// <summary>
	/// Provides extension members on <see cref="ItemSet"/> instances.
	/// </summary>
	/// <param name="this">The current instance.</param>
	extension(ItemSet @this)
	{
		/// <summary>
		/// Adds a new <see cref="Item"/> into the collection;
		/// or do nothing if <paramref name="item"/> is <see langword="null"/>.
		/// </summary>
		/// <param name="item">The item to add.</param>
		public void AddNullable(Item? item)
		{
			if (item is not null)
			{
				@this.Add(item);
			}
		}

		/// <summary>
		/// Adds a list of nullable <see cref="Item"/> instances into the current collection;
		/// or ignore values if they are <see langword="null"/>.
		/// </summary>
		/// <typeparam name="T">The type of enumerable sequence.</typeparam>
		/// <param name="items">The items to add.</param>
		public void AddRangeNullable<T>(T items) where T : IEnumerable<Item?>
		{
			foreach (var item in items)
			{
				@this.AddNullable(item);
			}
		}
	}
}
