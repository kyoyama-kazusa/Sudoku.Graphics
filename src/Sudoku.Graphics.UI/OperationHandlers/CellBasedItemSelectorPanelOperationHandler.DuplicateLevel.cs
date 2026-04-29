namespace Sudoku.Graphics.UI.OperationHandlers;

public partial class CellBasedItemSelectorPanelOperationHandler
{
	/// <summary>
	/// Represents duplicate item level.
	/// </summary>
	protected enum DuplicateLevel
	{
		/// <summary>
		/// Indicates duplicate level is item.
		/// </summary>
		Item,

		/// <summary>
		/// Indicates duplicate level is only for the current type in the same cell.
		/// </summary>
		CellOnlyCurrentItemType,

		/// <summary>
		/// Indicates duplicate level is all types in the same cell.
		/// </summary>
		CellAllTypes
	}
}
