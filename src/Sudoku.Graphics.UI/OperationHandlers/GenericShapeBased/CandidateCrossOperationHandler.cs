namespace Sudoku.Graphics.UI.OperationHandlers.GenericShapeBased;

/// <summary>
/// Represents an operation handler that produces <see cref="CandidateCrossMarkItem"/> instances.
/// </summary>
/// <seealso cref="CandidateCrossMarkItem"/>
[OperationHandler(ItemType.Candidate_Cross)]
public sealed class CandidateCrossOperationHandler : CandidateGenericShapeOperationHandler
{
	/// <inheritdoc/>
	public override ItemType ItemType => ItemType.Candidate_Cross;

	/// <inheritdoc/>
	public override Func<CandidatePosition, IItem_CandidatePositionProperty> ItemFactory => ItemsFactory.CandidateCross;
}
