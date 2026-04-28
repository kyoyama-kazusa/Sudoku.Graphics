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
	public override Func<MainWindow, ItemSelectorPanel> PanelSelector => static window => window.MoonPhasePanel;

	/// <inheritdoc/>
	public override Func<MainWindow, Popup> PopupSelector => static window => window.MoonPhasePopup;

	/// <inheritdoc/>
	public override Func<object?, Absolute, Item?> ItemFactory
		=> static (item, cell) => item switch
		{
			MoonPhaseDisplayItem { Phase: var phase } => ItemsFactory.MoonPhase(cell, phase, true),
			_ => null
		};

	/// <inheritdoc/>
	public override IReadOnlyDictionary<string, Func<IIconDisplayItem>> IconDisplayItemFactory
		=> new Dictionary<string, Func<IIconDisplayItem>>
		{
			{ "MoonPhase_LowerHalf_Curve", static () => new MoonPhaseDisplayItem { Phase = MoonPhase.LowerHalf_Curve } },
			{ "MoonPhase_LowerHalf_Line", static () => new MoonPhaseDisplayItem { Phase = MoonPhase.LowerHalf_Line } },
			{ "MoonPhase_UpperHalf_Curve", static () => new MoonPhaseDisplayItem { Phase = MoonPhase.UpperHalf_Curve } },
			{ "MoonPhase_UpperHalf_Line", static () => new MoonPhaseDisplayItem { Phase = MoonPhase.UpperHalf_Line } },
			{ "MoonPhase_Full", static () => new MoonPhaseDisplayItem { Phase = MoonPhase.Full } },
		};
}
