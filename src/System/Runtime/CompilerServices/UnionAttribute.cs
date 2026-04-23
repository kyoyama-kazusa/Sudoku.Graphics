namespace System.Runtime.CompilerServices;

/// <summary>
/// Represents a type that is a union or a union-like type.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, Inherited = false)]
public sealed class UnionAttribute : Attribute;
