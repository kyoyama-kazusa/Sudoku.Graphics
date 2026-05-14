namespace Sudoku.Graphics.UI.OperationHandlers;

/// <summary>
/// Represents an operation handler that produces <see cref="ThickLineSegmentItem"/> instances.
/// </summary>
/// <seealso cref="ThickLineSegmentItem"/>
[OperationHandler(ItemType.LineSegment_Thick)]
public sealed class ThickLineSegmentOperationHandler() : LineSegmentOperationHandler(true);
