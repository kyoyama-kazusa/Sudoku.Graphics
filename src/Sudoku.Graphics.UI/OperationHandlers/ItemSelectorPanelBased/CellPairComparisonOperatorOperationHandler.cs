namespace Sudoku.Graphics.UI.OperationHandlers.ItemSelectorPanelBased;

/// <summary>
/// Represents an operation handler that produces <see cref="CellPairComparisonOperatorTextMarkItem"/> instances.
/// </summary>
/// <seealso cref="CellPairComparisonOperatorTextMarkItem"/>
[OperationHandler(ItemType.CellPairText_ComparisonOperator)]
public sealed class CellPairComparisonOperatorOperationHandler : CellPairBasedItemSelectorPanelOperationHandler<ITextDisplayItem>
{
	/// <inheritdoc/>
	public override ItemType ItemType => ItemType.CellPairText_ComparisonOperator;

	/// <inheritdoc/>
	public override ReadOnlySpan<Func<ITextDisplayItem>> DisplayItemFactory
		=> (Func<ComparisonOperatorDisplayItem>[])[
			static () => new() { Operator = ComparisonOperator.GreaterThan },
			static () => new() { Operator = ComparisonOperator.LessThan },
			static () => new() { Operator = ComparisonOperator.Equals },
			static () => new() { Operator = ComparisonOperator.GreaterThanOrEqual },
			static () => new() { Operator = ComparisonOperator.LessThanOrEqual },
			static () => new() { Operator = ComparisonOperator.Inequals }
		];

	/// <inheritdoc/>
	public override Func<MainWindow, ItemSelectorPanel> PanelSelector => static window => window.CellPairComparisonOperatorPanel;

	/// <inheritdoc/>
	public override Func<MainWindow, Popup> PopupSelector => static window => window.CellPairComparisonOperatorPopup;

	/// <inheritdoc/>
	public override Func<ITextDisplayItem?, Absolute, Absolute, Item?> ItemFactory
		=> static (item, cell1, cell2) =>
			item is null
				? null
				: ItemsFactory.CellPairComparisonOperator(cell1, cell2, ((ComparisonOperatorDisplayItem)item).Operator);
}
