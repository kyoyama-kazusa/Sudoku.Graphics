namespace Sudoku.Graphics;

public partial interface IInteger<TSelf>
{
	/// <summary>
	/// Represents a value converter object.
	/// </summary>
	public sealed class Converter : JsonConverter<TSelf>
	{
		/// <inheritdoc/>
		public override TSelf Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
			=> reader.GetInt32();

		/// <inheritdoc/>
		public override void Write(Utf8JsonWriter writer, TSelf value, JsonSerializerOptions options)
			=> writer.WriteNumberValue(value);
	}
}
