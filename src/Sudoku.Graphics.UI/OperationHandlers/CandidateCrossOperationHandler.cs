namespace Sudoku.Graphics.UI.OperationHandlers;

/// <summary>
/// Represents an operation handler that produces <see cref="CandidateCrossMarkItem"/> instances.
/// </summary>
/// <seealso cref="CandidateCrossMarkItem"/>
[OperationHandler(ItemType.Candidate_Cross)]
public sealed class CandidateCrossOperationHandler : CandidateShapeOperationHandler
{
	/// <inheritdoc/>
	public override ItemType ItemType => ItemType.Candidate_Cross;

	/// <inheritdoc/>
	public override Func<CandidatePosition, CandidateMarkItem> ItemFactory => ItemsFactory.CandidateCross;
}
