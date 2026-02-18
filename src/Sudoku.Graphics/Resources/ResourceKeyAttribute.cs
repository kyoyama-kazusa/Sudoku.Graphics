namespace Sudoku.Resources;

/// <summary>
/// Indicates the property resource key.
/// Such key configured will be called in UI projects (reflection) to get target information on I18N-related scenarios.
/// </summary>
/// <param name="key">The key.</param>
[AttributeUsage(AttributeTargets.Property, Inherited = false)]
public sealed class ResourceKeyAttribute(string key) : Attribute
{
	/// <summary>
	/// Indicates the key of resource entry.
	/// </summary>
	public string Key { get; } = key;
}
