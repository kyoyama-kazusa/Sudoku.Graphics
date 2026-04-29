namespace Sudoku.Graphics.UI.OperationHandlers;

/// <summary>
/// Represents cell moon phase operation handler.
/// </summary>
[OperationHandler(ItemType.Cell_MoonPhase)]
public sealed class CellMoonPhaseOperatonHandler : CellBasedItemSelectorPanelOperationHandler
{
	/// <inheritdoc/>
	public override ItemType ItemType => ItemType.Cell_MoonPhase;

	/// <inheritdoc/>
	protected override ReadOnlySpan<Func<IIconDisplayItem>> IconsFactory
		=> (Func<IIconDisplayItem>[])[
			static () => new MoonPhaseDisplayItem { Phase = MoonPhase.LowerHalf_Curve },
			static () => new MoonPhaseDisplayItem { Phase = MoonPhase.LowerHalf_Line },
			static () => new MoonPhaseDisplayItem { Phase = MoonPhase.UpperHalf_Curve },
			static () => new MoonPhaseDisplayItem { Phase = MoonPhase.UpperHalf_Line },
			static () => new MoonPhaseDisplayItem { Phase = MoonPhase.Full }
		];

	/// <inheritdoc/>
	protected override Func<object?, Absolute, Item?> ItemFactory
		=> static (item, cell) => item is MoonPhaseDisplayItem { Phase: var phase } ? ItemsFactory.MoonPhase(cell, phase) : null;

	/// <inheritdoc/>
	protected override Func<MainWindow, ItemSelectorPanel> PanelSelector => static window => window.MoonPhasePanel;

	/// <inheritdoc/>
	protected override Func<MainWindow, Popup> PopupSelector => static window => window.MoonPhasePopup;
}
