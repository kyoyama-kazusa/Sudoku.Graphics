namespace Sudoku.Graphics.UI.OperationHandlers;

/// <summary>
/// Represents an operation handler type that creates for <see cref="CellTetrisMarkItem"/> instances.
/// </summary>
/// <seealso cref="CellTetrisMarkItem"/>
[OperationHandler(ItemType.Cell_Tetris)]
public sealed class TetrisOperationHandler : CellBasedItemSelectorPanelOperationHandler
{
	/// <inheritdoc/>
	public override ItemType ItemType => ItemType.Cell_Tetris;

	/// <inheritdoc/>
	public override Func<object?, Absolute, Item?> ItemFactory
		=> static (item, cell) => item switch
		{
			TetrominoDisplayItem { Type: var piece, RotationType: var rotationType } => ItemsFactory.Tetris(cell, piece, rotationType),
			_ => null
		};

	/// <inheritdoc/>
	public override Func<MainWindow, Popup> PopupSelector => static window => window.TetrisSelectorPopup;

	/// <inheritdoc/>
	public override Func<MainWindow, ItemSelectorPanel> PanelSelector => static window => window.TetrisSelectorPanel;

	/// <inheritdoc/>
	public override IReadOnlyDictionary<string, Func<IIconDisplayItem>> IconDisplayItemFactory
	{
		get
		{
			var result = new Dictionary<string, Func<IIconDisplayItem>>();
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
					result.Add(
						targetName,
						() => new TetrominoDisplayItem { RotationType = rotationType, Type = piece }
					);
				}
			}
			return result;
		}
	}
}
