namespace Sudoku.Graphics.UI.OperationHandlers;

/// <summary>
/// Represents an operation handler that produces <see cref="CandidateCircleMarkItem"/> instances.
/// </summary>
/// <seealso cref="CandidateCircleMarkItem"/>
[OperationHandler(ItemType.Candidate_Circle)]
public sealed class CandidateCircleOperationHandler : CandidateShapeOperationHandler
{
	/// <inheritdoc/>
	public override ItemType ItemType => ItemType.Candidate_Circle;

	/// <inheritdoc/>
	public override Func<CandidatePosition, CandidateMarkItem> ItemFactory => ItemsFactory.CandidateCircle;
}
