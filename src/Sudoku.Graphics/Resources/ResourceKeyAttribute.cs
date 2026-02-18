namespace Sudoku.Resources;

/// <summary>
/// Indicates the property resource key.
/// Such key configured will be called in UI projects (reflection) to get target information on I18N-related scenarios.
/// </summary>
/// <param name="key">
/// <para><inheritdoc cref="Key" path="/summary"/></para>
/// <para><b>Do not use <see langword="nameof"/> expressions here in order to prevent renaming and mismatching cases.</b></para>
/// </param>
[AttributeUsage(AttributeTargets.Property, Inherited = false)]
public sealed class ResourceKeyAttribute(string key) : Attribute
{
	/// <summary>
	/// Indicates the key of resource entry.
	/// </summary>
	public string Key { get; } = key;
}
