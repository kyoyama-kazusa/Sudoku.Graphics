namespace System.Collections.Generic;

/// <summary>
/// Provides extension members on <see cref="IReadOnlyDictionary{TKey, TValue}"/>,
/// or its related concrete types.
/// </summary>
/// <seealso cref="IReadOnlyDictionary{TKey, TValue}"/>
public static class DictionaryExtensions
{
	/// <summary>
	/// Provides extension members of type <see cref="IReadOnlyDictionary{TKey, TValue}"/>.
	/// </summary>
	/// <typeparam name="TKey">The type of key.</typeparam>
	/// <typeparam name="TValue">The type of value.</typeparam>
	/// <param name="this">The current instance.</param>
	extension<TKey, TValue>(IReadOnlyDictionary<TKey, TValue> @this) where TKey : notnull
	{
		/// <summary>
		/// Returns the equivalent string representation of the current dictionary.
		/// </summary>
		/// <returns>The equivalent string representation.</returns>
		public string ToDictionaryString()
		{
			var str = string.Join(", ", from kvp in @this select $"{kvp.Key}: {kvp.Value}");
			return $"[{str}]";
		}

		/// <summary>
		/// Returns the equivalent string representation of the current dictionary;
		/// using the specified converters to format keys and values.
		/// </summary>
		/// <param name="keyConverter">The key converter.</param>
		/// <param name="valueConverter">The value converter.</param>
		/// <returns>The equivalent string representation.</returns>
		public string ToDictionaryString(Converter<TKey, string> keyConverter, Converter<TValue, string> valueConverter)
		{
			var str = string.Join(", ", from kvp in @this select $"{keyConverter(kvp.Key)}: {valueConverter(kvp.Value)}");
			return $"[{str}]";
		}
	}
}
