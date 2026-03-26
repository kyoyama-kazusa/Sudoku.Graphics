using System.Runtime.InteropServices;

namespace System.Collections.Generic;

/// <summary>
/// Provides extension members on <see cref="List{T}"/>.
/// </summary>
/// <seealso cref="List{T}"/>
public static class ListExtensions
{
	/// <typeparam name="T">The type of each element.</typeparam>
	/// <param name="this">The current instance.</param>
	extension<T>(List<T> @this)
	{
		/// <summary>
		/// Converts the current instance into memory-storage-equivalent form of type <see cref="ReadOnlySpan{T}"/>.
		/// </summary>
		/// <returns>The equivalent <see cref="ReadOnlySpan{T}"/> instance.</returns>
		public ReadOnlySpan<T> AsSpan() => CollectionsMarshal.AsSpan(@this);
	}
}
