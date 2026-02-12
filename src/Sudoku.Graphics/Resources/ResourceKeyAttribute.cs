namespace Sudoku.Resources;

/// <summary>
/// Indicates the property resource key.
/// </summary>
/// <param name="key">The key.</param>
[AttributeUsage(AttributeTargets.Property)]
public sealed class ResourceKeyAttribute(string key) : Attribute
{
	/// <summary>
	/// Indicates the key of resource entry.
	/// </summary>
	public string Key { get; } = key;
}
