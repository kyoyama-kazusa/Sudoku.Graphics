namespace Sudoku.Graphics.Items.CandidateMarks;

/// <summary>
/// Represents a candidate mark item.
/// </summary>
public abstract record CandidateMarkItem :
	Item,
	IItem_CandidatePositionProperty,
	IItem_MarkRelatedProperties,
	IItem_TemplateIndexProperty
{
	/// <inheritdoc/>
	public required int TemplateIndex { get; init; }

	/// <inheritdoc/>
	public required CandidatePosition CandidatePosition { get; init; }

	/// <inheritdoc/>
	public virtual Scale SizeScale { get; init; }

	/// <inheritdoc/>
	public virtual SerializableColor StrokeColor { get; init; }

	/// <inheritdoc/>
	public virtual SerializableColor FillColor { get; init; }
}
