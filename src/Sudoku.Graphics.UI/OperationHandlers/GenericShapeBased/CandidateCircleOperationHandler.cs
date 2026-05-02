namespace Sudoku.Graphics.UI.OperationHandlers.GenericShapeBased;

/// <summary>
/// Represents an operation handler that produces <see cref="CandidateCircleMarkItem"/> instances.
/// </summary>
/// <seealso cref="CandidateCircleMarkItem"/>
[OperationHandler(ItemType.Candidate_Circle)]
public sealed class CandidateCircleOperationHandler : CandidateGenericShapeOperationHandler
{
	/// <inheritdoc/>
	public override ItemType ItemType => ItemType.Candidate_Circle;

	/// <inheritdoc/>
	public override Func<CandidatePosition, IItem_CandidatePositionProperty> ItemFactory => ItemsFactory.CandidateCircle;
}
