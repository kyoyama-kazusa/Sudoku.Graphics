#pragma warning disable CS0809

namespace Sudoku.Graphics.UI.OperationHandlers;

/// <summary>
/// Represents an attribute type that describes what item type the opertaion handler will support.
/// </summary>
/// <param name="type">The supported item type.</param>
[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public sealed class OperationHandlerAttribute(ItemType type) : Attribute
{
	/// <summary>
	/// Indicates supported item type.
	/// </summary>
	public ItemType SupportedItemType { get; } = type;
}
