namespace Sudoku.Graphics.Primitives;

/// <summary>
/// Defines an options type.
/// </summary>
/// <typeparam name="TSelf">The type itself.</typeparam>
public interface IOptionsProvider<out TSelf> : IOptionsSupportsSerialization<TSelf>
	where TSelf : notnull, IOptionsProvider<TSelf>, new()
{
	/// <summary>
	/// Represents serializer options.
	/// </summary>
	protected static readonly JsonSerializerOptions SerializerOptions = new()
	{
		WriteIndented = true,
		IndentCharacter = ' ',
		IndentSize = 2,
		AllowTrailingCommas = false,
		Converters = { new JsonStringEnumConverter() }
	};


	/// <summary>
	/// Indicates the default options instance.
	/// </summary>
	static virtual TSelf DefaultInstance { get; } = new();
}
