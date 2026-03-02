namespace System.Text;

/// <summary>
/// Provides extension members on <see cref="StringBuilder"/>.
/// </summary>
/// <seealso cref="StringBuilder"/>
public static class StringBuilderExtensions
{
	/// <param name="this">The current string builder.</param>
	extension(StringBuilder @this)
	{
		/// <summary>
		/// Removes the specified number of characters from the end of the current instance.
		/// </summary>
		/// <param name="count">The number of characters to remove.</param>
		/// <returns>The reference same as the current instance.</returns>
		public StringBuilder RemoveFromEnd(int count) => @this.Remove(@this.Length - count, count);
	}
}
