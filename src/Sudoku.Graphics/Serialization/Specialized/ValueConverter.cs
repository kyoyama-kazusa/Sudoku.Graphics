namespace Sudoku.Serialization.Specialized;

/// <summary>
/// Represents a value converter object.
/// </summary>
/// <typeparam name="TValue">The type of value.</typeparam>
public sealed class ValueConverter<TValue> : JsonConverter<TValue> where TValue : struct, IInteger<TValue>
{
	/// <inheritdoc/>
	public override TValue Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		=> reader.GetInt32();

	/// <inheritdoc/>
	public override void Write(Utf8JsonWriter writer, TValue value, JsonSerializerOptions options)
		=> writer.WriteNumberValue(value);
}
