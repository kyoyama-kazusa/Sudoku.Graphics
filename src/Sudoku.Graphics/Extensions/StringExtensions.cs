namespace System;

/// <summary>
/// Provides extension members on <see cref="string"/>.
/// </summary>
/// <seealso cref="string"/>
public static class StringExtensions
{
	extension(string)
	{
		/// <summary>
		/// Splits the current instance into the multiple parts.
		/// </summary>
		/// <param name="original">The original string.</param>
		/// <param name="separator">The separator character.</param>
		/// <returns>A list of <see cref="string"/> instances.</returns>
		public static string[] operator /(string original, char separator) => original.Split(separator);

		/// <inheritdoc cref="extension(string).op_Division(string, char)"/>
		public static string[] operator /(string original, string separator) => original.Split(separator);
	}
}
