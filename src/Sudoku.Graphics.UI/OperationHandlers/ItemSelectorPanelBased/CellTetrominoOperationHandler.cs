namespace Sudoku.Graphics.UI.OperationHandlers.ItemSelectorPanelBased;

/// <summary>
/// Represents an operation handler type that creates for <see cref="CellTetrominoMarkItem"/> instances.
/// </summary>
/// <seealso cref="CellTetrominoMarkItem"/>
[OperationHandler(ItemType.Cell_Tetromino)]
public sealed class CellTetrominoOperationHandler : CellBasedItemSelectorPanelOperationHandler
{
	/// <inheritdoc/>
	public override ItemType ItemType => ItemType.Cell_Tetromino;

	/// <inheritdoc/>
	protected override ReadOnlySpan<Func<IIconDisplayItem>> IconsFactory
	{
		get
		{
			var result = new List<Func<IIconDisplayItem>>();
			foreach (var piece in (Tetromino[])[
				Tetromino.I,
				Tetromino.O,
				Tetromino.T,
				Tetromino.J,
				Tetromino.L,
				Tetromino.S,
				Tetromino.Z
			])
			{
				foreach (var rotationType in (TetrominoRotationType[])[
					TetrominoRotationType.None,
					TetrominoRotationType.Single,
					TetrominoRotationType.Double,
					TetrominoRotationType.Triple
				])
				{
					var targetName = $"Tetromino_{piece}_{rotationType}";
					result.Add(() => new TetrominoDisplayItem { RotationType = rotationType, Type = piece });
				}
			}
			return result.AsSpan();
		}
	}

	/// <inheritdoc/>
	protected override Func<object?, Absolute, Item?> ItemFactory
		=> static (item, cell) =>
			item is TetrominoDisplayItem { Type: var piece, RotationType: var rotationType }
				? ItemsFactory.Tetromino(cell, piece, rotationType)
				: null;

	/// <inheritdoc/>
	protected override Func<MainWindow, Popup> PopupSelector => static window => window.TetrominoSelectorPopup;

	/// <inheritdoc/>
	protected override Func<MainWindow, ItemSelectorPanel> PanelSelector => static window => window.TetrominoSelectorPanel;
}
