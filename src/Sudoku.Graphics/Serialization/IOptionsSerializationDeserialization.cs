namespace Sudoku.Serialization;

/// <summary>
/// Represents an instance of type <typeparamref name="TSelf"/> that supports serialization and deserialization.
/// </summary>
/// <typeparam name="TSelf">The type itself.</typeparam>
public interface IOptionsSerializationDeserialization<out TSelf>
	where TSelf : IOptionsSerializationDeserialization<TSelf>
{
	/// <summary>
	/// Writes the configuration of this instance into target path (serialize the instance).
	/// </summary>
	/// <param name="path">The path.</param>
	/// <param name="options">The options.</param>
	void WriteTo(string path, JsonSerializerOptions? options = null);


	/// <summary>
	/// Reads the configuration from the specified path, and load it, converting it into <typeparamref name="TSelf"/> instance.
	/// </summary>
	/// <param name="path">The path.</param>
	/// <param name="options">The options.</param>
	/// <returns>The instance loaded.</returns>
	static abstract TSelf ReadFrom(string path, JsonSerializerOptions? options = null);
}
