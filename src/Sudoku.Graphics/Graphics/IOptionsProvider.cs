namespace Sudoku.Graphics;

/// <summary>
/// Defines an options type.
/// </summary>
/// <typeparam name="TSelf">The type itself.</typeparam>
public interface IOptionsProvider<out TSelf> : IOptionsSerializationDeserialization<TSelf>
	where TSelf : notnull, IOptionsProvider<TSelf>, new()
{
	/// <summary>
	/// Indicates the default options instance.
	/// </summary>
	static virtual TSelf DefaultInstance { get; } = new();
}
