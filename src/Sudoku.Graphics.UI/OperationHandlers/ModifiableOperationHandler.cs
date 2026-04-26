namespace Sudoku.Graphics.UI.OperationHandlers;

/// <summary>
/// Provides an operation handler that produces <see cref="ModifiableTextItem"/> instances.
/// </summary>
/// <seealso cref="ModifiableTextItem"/>
[OperationHandler(ItemType.Text_Modifiable)]
public sealed class ModifiableOperationHandler() : GivenOrModifiableOperationHandler(false);
