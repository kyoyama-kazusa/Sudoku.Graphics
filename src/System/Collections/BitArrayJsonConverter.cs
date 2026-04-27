namespace System.Collections;

/// <summary>
/// Represents a JSON converter for type <see cref="BitArray"/>.
/// </summary>
/// <seealso cref="BitArray"/>
public sealed class BitArrayJsonConverter : JsonConverter<BitArray>
{
	/// <inheritdoc/>
	public override BitArray Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		var str = reader.GetString() ?? throw new JsonException();
		var result = new BitArray(str.Length);
		for (var i = 0; i < str.Length; i++)
		{
			result[i] = str[i] switch { '1' => true, '0' => false, _ => throw new JsonException() };
		}
		return result;
	}

	/// <inheritdoc/>
	public override void Write(Utf8JsonWriter writer, BitArray value, JsonSerializerOptions options)
	{
		var result = (stackalloc char[value.Length]);
		for (var i = 0; i < value.Length; i++)
		{
			result[i] = value[i] ? '1' : '0';
		}
		writer.WriteStringValue(result);
	}
}
