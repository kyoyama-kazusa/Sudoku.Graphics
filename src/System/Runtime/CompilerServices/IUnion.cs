namespace System.Runtime.CompilerServices;

/// <summary>
/// Represents a type that behaves like a union.
/// </summary>
public interface IUnion
{
	/// <summary>
	/// Indicates the backing value of the union.
	/// </summary>
	object? Value { get; }
}
