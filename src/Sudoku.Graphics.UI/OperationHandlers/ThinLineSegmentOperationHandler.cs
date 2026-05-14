namespace Sudoku.Graphics.UI.OperationHandlers;

/// <summary>
/// Represents an operation handler that produces <see cref="ThinLineSegmentItem"/> instances.
/// </summary>
/// <seealso cref="ThinLineSegmentItem"/>
[OperationHandler(ItemType.LineSegment_Thin)]
public sealed class ThinLineSegmentOperationHandler() : LineSegmentOperationHandler(false);
