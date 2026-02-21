namespace Sudoku.ComponentModel.Items;

/// <summary>
/// Represents canvas background fill item.
/// </summary>
public sealed class BackgroundFillItem : FillItem
{
	/// <inheritdoc/>
	public override ItemType Type => ItemType.BackgroundFill;

	/// <summary>
	/// Indicates the color to be filled.
	/// </summary>
	public required SerializableColor Color { get; init; }

	/// <inheritdoc/>
	protected override Type EqualityContract => typeof(BackgroundFillItem);


	/// <inheritdoc/>
	/// <remarks>
	/// <b>By design, the global item sequence can only contain one <see cref="BackgroundFillItem"/> instance.</b>
	/// </remarks>
	public override bool Equals([NotNullWhen(true)] Item? other) => other is BackgroundFillItem;

	/// <inheritdoc/>
	/// <remarks>
	/// <b>By design, the global item sequence can only contain one <see cref="BackgroundFillItem"/> instance.</b>
	/// </remarks>
	public override int GetHashCode() => HashCode.Combine(EqualityContract);

	/// <inheritdoc/>
	protected internal override void DrawTo(Canvas canvas) => canvas.BackingCanvas.Clear(Color);

	/// <inheritdoc/>
	protected override void PrintMembers(StringBuilder builder) => builder.Append($"{nameof(Color)} = {Color}");
}
