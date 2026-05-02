namespace Sudoku.Graphics.UI.OperationHandlers;

/// <summary>
/// Represents an operation handler that produces candidate shape items.
/// </summary>
public abstract class CandidateGenericShapeOperationHandler() : LocatorGenericShapeOperationHandler(false)
{
	/// <summary>
	/// Indicates the item factory.
	/// </summary>
	public abstract Func<CandidatePosition, IItem_CandidatePositionProperty> ItemFactory { get; }

	/// <inheritdoc/>
	protected sealed override Func<Locator, Item> ItemFactoryBase
		=> locator => locator is CandidatePosition c && ItemFactory(c) is Item i ? i : throw new UnreachableException();
}
