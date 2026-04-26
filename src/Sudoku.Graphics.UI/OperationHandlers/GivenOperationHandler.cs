namespace Sudoku.Graphics.UI.OperationHandlers;

/// <summary>
/// Provides an operation handler that produces <see cref="GivenTextItem"/> instances.
/// </summary>
/// <seealso cref="GivenTextItem"/>
[OperationHandler(ItemType.Text_Given)]
public sealed class GivenOperationHandler() : GivenOrModifiableOperationHandler(true);
