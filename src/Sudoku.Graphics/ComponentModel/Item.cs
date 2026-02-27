namespace Sudoku.ComponentModel;

/// <summary>
/// Represents an item to be drawn. The item can be anything the canvas can draw - cell background, canvas background,
/// candidate highlight, grid lines, and other basic items to draw.
/// </summary>
[JsonPolymorphic(UnknownDerivedTypeHandling = JsonUnknownDerivedTypeHandling.FailSerialization)]
[JsonDerivedType(typeof(BackgroundFillItem), nameof(BackgroundFillItem))]
[JsonDerivedType(typeof(CandidateFillItem), nameof(CandidateFillItem))]
[JsonDerivedType(typeof(CandidateTextItem), nameof(CandidateTextItem))]
[JsonDerivedType(typeof(CellDiceMarkItem), nameof(CellDiceMarkItem))]
[JsonDerivedType(typeof(CellFillItem), nameof(CellFillItem))]
[JsonDerivedType(typeof(CellExclamationMarkItem), nameof(CellExclamationMarkItem))]
[JsonDerivedType(typeof(CellQuestionMarkItem), nameof(CellQuestionMarkItem))]
[JsonDerivedType(typeof(CellSurroundingTrianglesMarkItem), nameof(CellSurroundingTrianglesMarkItem))]
[JsonDerivedType(typeof(CellTetrisMarkItem), nameof(CellTetrisMarkItem))]
[JsonDerivedType(typeof(GivenTextItem), nameof(GivenTextItem))]
[JsonDerivedType(typeof(ModifiableTextItem), nameof(ModifiableTextItem))]
[JsonDerivedType(typeof(TemplateLineStrokeItem), nameof(TemplateLineStrokeItem))]
public abstract class Item : IEquatable<Item>, IEqualityOperators<Item, Item, bool>
{
	/// <summary>
	/// Indicates the type of item.
	/// </summary>
	public abstract ItemType Type { get; }

	/// <summary>
	/// Gets the equality contract for this instance.
	/// </summary>
	/// <remarks>
	/// <para>
	/// The equality contract represents the runtime type identity used when evaluating value-based equality.
	/// It allows derived types to participate in equality comparisons while preserving type consistency
	/// across an inheritance hierarchy.
	/// </para>
	/// <para>
	/// By default, this property returns the concrete runtime type of the current instance.
	/// Derived types may override this property to customize how type identity is considered during equality comparisons.
	/// </para>
	/// <para>
	/// This mechanism enables polymorphic equality semantics without relying directly on <see cref="object.GetType"/>.
	/// </para>
	/// </remarks>
	/// <seealso cref="object.GetType"/>
	protected abstract Type EqualityContract { get; }


	/// <inheritdoc/>
	public sealed override bool Equals([NotNullWhen(true)] object? obj) => Equals(obj as Item);

	/// <inheritdoc/>
	public abstract bool Equals([NotNullWhen(true)] Item? other);

	/// <inheritdoc/>
	public abstract override int GetHashCode();

	/// <inheritdoc/>
	public sealed override string ToString()
	{
		var sb = new StringBuilder();
		PrintMembers(sb);
		return sb.Length == 0 ? $$"""{{EqualityContract.Name}} {}""" : $$"""{{EqualityContract.Name}} { {{sb}} }""";
	}

	/// <summary>
	/// Try to draw the current item onto the specified canvas.
	/// </summary>
	/// <param name="canvas">The canvas to draw.</param>
	protected internal abstract void DrawTo(Canvas canvas);

	/// <summary>
	/// Provides a way to concatenate string values, in order to write out string text to be shown.
	/// </summary>
	/// <param name="builder">The string builder instance.</param>
	protected abstract void PrintMembers(StringBuilder builder);


	/// <inheritdoc/>
	public static bool operator ==(Item? left, Item? right)
		=> (left, right) switch { (null, null) => true, (not null, not null) => left.Equals(right), _ => false };

	/// <inheritdoc/>
	public static bool operator !=(Item? left, Item? right) => !(left == right);
}
