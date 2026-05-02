namespace Sudoku.Graphics.Items;

/// <summary>
/// Represents a type that includes <see cref="CandidatePosition"/> property.
/// </summary>
public interface IItem_CandidatePositionProperty : IItem_LocatorProperty
{
	/// <summary>
	/// Indicates candidate position to be set.
	/// </summary>
	CandidatePosition CandidatePosition { get; init; }

	/// <inheritdoc/>
	Locator IItem_LocatorProperty.Locator
	{
		get => CandidatePosition;

		init
			=> CandidatePosition = value is CandidatePosition a
				? a
				: throw new ArgumentException($"Type mismatches - expected '{nameof(CandidatePosition)}'.", nameof(value));
	}
}
