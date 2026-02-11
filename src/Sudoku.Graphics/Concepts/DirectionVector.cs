namespace Sudoku.Concepts;

/// <summary>
/// Represents an encapsulated type of a quadruple of <see cref="int"/> values, indicating direction offsets.
/// </summary>
/// <param name="Up">Indicates upside offsets.</param>
/// <param name="Down">Indicates downside offsets.</param>
/// <param name="Left">Indicates leftside offsets.</param>
/// <param name="Right">Indicates rightside offsets.</param>
[JsonConverter(typeof(Converter))]
public readonly record struct DirectionVector(int Up, int Down, int Left, int Right) :
	IAdditionOperators<DirectionVector, DirectionVector, DirectionVector>,
	IEqualityOperators<DirectionVector, DirectionVector, bool>,
	ISubtractionOperators<DirectionVector, DirectionVector, DirectionVector>
{
	/// <summary>
	/// Indicates the instance whose members are initialized zero.
	/// </summary>
	public static readonly DirectionVector Zero = new(0, 0, 0, 0);


	/// <summary>
	/// Initializes a <see cref="DirectionVector"/> instance via the uniform value.
	/// </summary>
	/// <param name="uniform">The uniform value.</param>
	public DirectionVector(int uniform) : this(uniform, uniform, uniform, uniform)
	{
	}


	/// <summary>
	/// Returns the value at the specified direction.
	/// </summary>
	/// <param name="direction">The direction.</param>
	/// <returns>Result value.</returns>
	/// <exception cref="ArgumentOutOfRangeException">
	/// Throws when <paramref name="direction"/> is not defined or <see cref="Direction.None"/>.
	/// </exception>
	/// <seealso cref="Direction.None"/>
	public int GetValue(Direction direction)
		=> direction switch
		{
			Direction.Up => Up,
			Direction.Down => Down,
			Direction.Left => Left,
			Direction.Right => Right,
			_ => throw new ArgumentOutOfRangeException(nameof(direction))
		};

	/// <inheritdoc cref="IEquatable{T}.Equals(T)"/>
	[OverloadResolutionPriority(1)]
	public bool Equals(in DirectionVector vector) => Up == vector.Up && Down == vector.Down && Left == vector.Left && Right == vector.Right;

	/// <inheritdoc cref="object.GetHashCode"/>
	public override int GetHashCode() => HashCode.Combine(Up, Down, Left, Right);


	/// <inheritdoc cref="IAdditionOperators{TSelf, TOther, TResult}.op_Addition(TSelf, TOther)"/>
	public static DirectionVector operator +(in DirectionVector left, in DirectionVector right)
		=> new(left.Up + right.Up, left.Down + right.Down, left.Left + right.Left, left.Right + right.Right);

	/// <inheritdoc cref="IAdditionOperators{TSelf, TOther, TResult}.op_CheckedAddition(TSelf, TOther)"/>
	public static DirectionVector operator checked +(in DirectionVector left, in DirectionVector right)
		=> new(
			checked(left.Up + right.Up),
			checked(left.Down + right.Down),
			checked(left.Left + right.Left),
			checked(left.Right + right.Right)
		);

	/// <inheritdoc cref="ISubtractionOperators{TSelf, TOther, TResult}.op_Subtraction(TSelf, TOther)"/>
	public static DirectionVector operator -(in DirectionVector left, in DirectionVector right)
		=> new(left.Up - right.Up, left.Down - right.Down, left.Left - right.Left, left.Right - right.Right);

	/// <inheritdoc cref="ISubtractionOperators{TSelf, TOther, TResult}.op_CheckedSubtraction(TSelf, TOther)"/>
	public static DirectionVector operator checked -(in DirectionVector left, in DirectionVector right)
		=> new(
			checked(left.Up - right.Up),
			checked(left.Down - right.Down),
			checked(left.Left - right.Left),
			checked(left.Right - right.Right)
		);

	/// <inheritdoc/>
	static DirectionVector IAdditionOperators<DirectionVector, DirectionVector, DirectionVector>.operator +(DirectionVector left, DirectionVector right)
		=> left + right;

	/// <inheritdoc/>
	static DirectionVector IAdditionOperators<DirectionVector, DirectionVector, DirectionVector>.operator checked +(DirectionVector left, DirectionVector right)
		=> checked(left + right);

	/// <inheritdoc/>
	static DirectionVector ISubtractionOperators<DirectionVector, DirectionVector, DirectionVector>.operator -(DirectionVector left, DirectionVector right)
		=> left - right;

	/// <inheritdoc/>
	static DirectionVector ISubtractionOperators<DirectionVector, DirectionVector, DirectionVector>.operator checked -(DirectionVector left, DirectionVector right)
		=> checked(left - right);
}

/// <summary>
/// Represents a JSON converter object that converts <see cref="DirectionVector"/> instances.
/// </summary>
/// <seealso cref="DirectionVector"/>
file sealed class Converter : JsonConverter<DirectionVector>
{
	/// <inheritdoc/>
	public override DirectionVector Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		var array = JsonSerializer.Deserialize<int[]>(ref reader, options);
		return array is [var up, var down, var left, var right] ? new(up, down, left, right) : throw new JsonException();
	}

	/// <inheritdoc/>
	public override void Write(Utf8JsonWriter writer, DirectionVector value, JsonSerializerOptions options)
	{
		var (up, down, left, right) = value;
		JsonSerializer.Serialize(writer, (int[])[up, down, left, right], options);
	}
}
