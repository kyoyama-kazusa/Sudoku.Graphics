namespace Sudoku.Graphics.UI.OperationHandlers;

/// <summary>
/// Represents cell generic shape operation handler.
/// </summary>
public abstract class CellGenericShapeOperationHandler() : LocatorGenericShapeOperationHandler(true)
{
	/// <summary>
	/// Indicates the item factory.
	/// </summary>
	public abstract Func<Absolute, IItem_CellProperty> ItemFactory { get; }

	/// <inheritdoc/>
	protected sealed override Func<Locator, Item> ItemFactoryBase
		=> locator => locator is Absolute a && ItemFactory(a) is Item i ? i : throw new UnreachableException();
}
