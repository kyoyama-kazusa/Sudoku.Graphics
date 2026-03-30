namespace Sudoku.Graphics;

public partial class ItemTypeOrdering
{
	/// <summary>
	/// Represents a JSON converter for this type.
	/// </summary>
	private sealed class Converter : JsonConverter<ItemTypeOrdering>
	{
		/// <inheritdoc/>
		public override ItemTypeOrdering? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			var dictionary = JsonSerializer.Deserialize<Dictionary<ItemType, int>>(ref reader, options);
			if (dictionary is null)
			{
				return null;
			}

			var result = new ItemTypeOrdering();
			foreach (var (key, value) in dictionary)
			{
				result.Add(key, value);
			}
			return result;
		}

		/// <inheritdoc/>
		public override void Write(Utf8JsonWriter writer, ItemTypeOrdering value, JsonSerializerOptions options)
			=> JsonSerializer.Serialize(writer, value._orderingDictionary, options);
	}
}
