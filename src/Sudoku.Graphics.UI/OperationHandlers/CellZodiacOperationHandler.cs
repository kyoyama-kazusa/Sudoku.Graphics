namespace Sudoku.Graphics.UI.OperationHandlers;

/// <summary>
/// Represents an operation handler that produces zodiac emoji text mark items.
/// </summary>
[OperationHandler(ItemType.CellText_ZodiacEmoji)]
public sealed class CellZodiacOperationHandler : CellBasedItemSelectorPanelOperationHandler
{
	/// <inheritdoc/>
	public override ItemType ItemType { get; }

	/// <inheritdoc/>
	protected override ReadOnlySpan<Func<IIconDisplayItem>> IconsFactory
		=> (Func<IIconDisplayItem>[])[
			static () => new CellZodiacDisplayItem { Zodiac = Zodiac.Rat },
			static () => new CellZodiacDisplayItem { Zodiac = Zodiac.Ox },
			static () => new CellZodiacDisplayItem { Zodiac = Zodiac.Tiger },
			static () => new CellZodiacDisplayItem { Zodiac = Zodiac.Rabbit },
			static () => new CellZodiacDisplayItem { Zodiac = Zodiac.Dragon },
			static () => new CellZodiacDisplayItem { Zodiac = Zodiac.Snake },
			static () => new CellZodiacDisplayItem { Zodiac = Zodiac.Horse },
			static () => new CellZodiacDisplayItem { Zodiac = Zodiac.Sheep },
			static () => new CellZodiacDisplayItem { Zodiac = Zodiac.Monkey },
			static () => new CellZodiacDisplayItem { Zodiac = Zodiac.Rooster },
			static () => new CellZodiacDisplayItem { Zodiac = Zodiac.Dog },
			static () => new CellZodiacDisplayItem { Zodiac = Zodiac.Pig }
		];

	/// <inheritdoc/>
	protected override Func<object?, Absolute, Item?> ItemFactory
		=> static (item, cell) => item is CellZodiacDisplayItem { Zodiac: var zodiac } ? ItemsFactory.CellZodiac(cell, zodiac) : null;

	/// <inheritdoc/>
	protected override Func<MainWindow, ItemSelectorPanel> PanelSelector => static window => window.CellZodiacPanel;

	/// <inheritdoc/>
	protected override Func<MainWindow, Popup> PopupSelector => static window => window.CellZodiacPopup;
}
