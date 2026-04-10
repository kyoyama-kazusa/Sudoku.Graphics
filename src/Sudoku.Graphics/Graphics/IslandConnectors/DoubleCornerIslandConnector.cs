namespace Sudoku.Graphics.IslandConnectors;

/// <summary>
/// Represents an island connector that will make double corners.
/// </summary>
public sealed class DoubleCornerIslandConnector : IslandConnector
{
	/// <summary>
	/// Indicates the offset.
	/// </summary>
	public required Relative Offset { get; init; }

	/// <summary>
	/// Indicates the start connected direction.
	/// </summary>
	public required Direction4 StartConnectedDirection { get; init; }

	/// <summary>
	/// Indicates the end connection direction.
	/// </summary>
	public required Direction4 EndConnectedDirection { get; init; }

	/// <inheritdoc/>
	protected override Type EqualityContract => typeof(DoubleCornerIslandConnector);


	/// <inheritdoc/>
	public override bool Equals([NotNullWhen(true)] IslandConnector? other)
		=> other is DoubleCornerIslandConnector comparer
		&& Offset == comparer.Offset
		&& StartConnectedDirection == comparer.StartConnectedDirection && EndConnectedDirection == comparer.StartConnectedDirection;

	/// <inheritdoc/>
	public override int GetHashCode()
		=> HashCode.Combine(EqualityContract, Offset, StartConnectedDirection, EndConnectedDirection);

	/// <inheritdoc/>
	public override DoubleCornerIslandConnector Clone()
		=> new()
		{
			Offset = Offset,
			StartConnectedDirection = StartConnectedDirection,
			EndConnectedDirection = EndConnectedDirection
		};

	/// <inheritdoc/>
	protected override void PrintMembers(StringBuilder builder)
	{
		AppendMemberString(builder, Offset);
		AppendMemberString(builder, StartConnectedDirection);
		AppendMemberString(builder, EndConnectedDirection);
	}
}
