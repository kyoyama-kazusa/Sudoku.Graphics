namespace Sudoku.Graphics.UI.OperationHandlers;

/// <summary>
/// The default operation handler.
/// </summary>
[OperationHandler(ItemType.None)]
public sealed class NoneOperationHandler : OperationHandler
{
	/// <inheritdoc/>
	protected internal override void OnMouseButtonPressed(OperationHandlerContext context)
	{
	}

	/// <inheritdoc/>
	protected internal override void OnMouseButtonReleased(OperationHandlerContext context)
	{
	}

	/// <inheritdoc/>
	protected internal override ReadOnlySpan<Item> CreateItem(OperationHandlerContext context) => [];
}
