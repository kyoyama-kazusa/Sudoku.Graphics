namespace Sudoku.Graphics.Items.CandidatePairMarks;

/// <summary>
/// Represents a candidate pair mark item.
/// </summary>
public abstract record CandidatePairMarkItem : Item, IItem_MarkRelatedProperties
{
	/// <summary>
	/// Indicates the candidate position 1.
	/// </summary>
	public required CandidatePosition CandidatePosition1 { get; init; }

	/// <summary>
	/// Indicates the candidate position 2.
	/// </summary>
	public required CandidatePosition CandidatePosition2 { get; init; }

	/// <summary>
	/// Indicates stroke width scale.
	/// </summary>
	public virtual Scale StrokeWidthScale { get; init; }

	/// <inheritdoc/>
	public virtual SerializableColor StrokeColor { get; init; }

	/// <inheritdoc/>
	public virtual SerializableColor FillColor { get; init; }

	/// <inheritdoc/>
	Scale IItem_MarkRelatedProperties.SizeScale { get; init; }
}
