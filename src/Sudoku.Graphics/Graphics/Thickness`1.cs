namespace Sudoku.Graphics;

/// <summary>
/// Represents a thickness of generic type of values.
/// </summary>
/// <typeparam name="T">The type of values.</typeparam>
/// <param name="Left">Indicates the left offset size.</param>
/// <param name="Top">Indicates the top offset size.</param>
/// <param name="Right">Indicates the right offset size.</param>
/// <param name="Bottom">Indicates the bottom size.</param>
[JsonConverter(typeof(Converter<>))]
public readonly record struct Thickness<T>(T? Left, T? Top, T? Right, T? Bottom) where T : notnull
{
	/// <summary>
	/// Initializes a <see cref="Thickness"/> instance.
	/// </summary>
	/// <param name="uniform">The uniform value.</param>
	public Thickness(T? uniform) : this(uniform, uniform, uniform, uniform)
	{
	}

	/// <summary>
	/// Initializes a <see cref="Thickness"/> instance.
	/// </summary>
	/// <param name="topLeft">The top and left values.</param>
	/// <param name="bottomRight">The bottom and right values.</param>
	public Thickness(T? topLeft, T? bottomRight) : this(topLeft, topLeft, bottomRight, bottomRight)
	{
	}


	/// <summary>
	/// Indicates the value whose factors are initialized <see langword="default"/>(<typeparamref name="T"/>).
	/// </summary>
	public static Thickness<T> Zero => new(default);
}

/// <summary>
/// Represents a JSON converter object that converts <see cref="Thickness{T}"/> instances.
/// </summary>
/// <seealso cref="Thickness{T}"/>
file sealed class Converter<T> : JsonConverter<Thickness<T>> where T : notnull
{
	/// <inheritdoc/>
	public override Thickness<T> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		var array = JsonSerializer.Deserialize<T?[]>(ref reader, options);
		return array is [var up, var down, var left, var right] ? new(up, down, left, right) : throw new JsonException();
	}

	/// <inheritdoc/>
	public override void Write(Utf8JsonWriter writer, Thickness<T> value, JsonSerializerOptions options)
	{
		var (up, down, left, right) = value;
		JsonSerializer.Serialize(writer, (T?[])[up, down, left, right], options);
	}
}
