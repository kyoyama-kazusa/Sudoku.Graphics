namespace Sudoku.Graphics;

/// <summary>
/// Represents an island connector.
/// </summary>
[JsonPolymorphic(UnknownDerivedTypeHandling = JsonUnknownDerivedTypeHandling.FallBackToBaseType)]
[JsonDerivedType(typeof(DirectIslandConnector), nameof(DirectIslandConnector))]
[JsonDerivedType(typeof(SingleCornerIslandConnector), nameof(SingleCornerIslandConnector))]
[JsonDerivedType(typeof(DoubleCornerIslandConnector), nameof(DoubleCornerIslandConnector))]
public abstract class IslandConnector :
	ICloneable,
	IEquatable<IslandConnector>,
	IEqualityOperators<IslandConnector, IslandConnector, bool>
{
	/// <summary>
	/// Represents equality contract.
	/// </summary>
	protected abstract Type EqualityContract { get; }


	/// <inheritdoc/>
	public sealed override bool Equals([NotNullWhen(true)] object? obj) => Equals(obj as IslandConnector);

	/// <inheritdoc/>
	public abstract bool Equals([NotNullWhen(true)] IslandConnector? other);

	/// <inheritdoc/>
	public abstract override int GetHashCode();

	/// <inheritdoc/>
	public sealed override string ToString()
	{
		var sb = new StringBuilder();
		sb.Append(EqualityContract.Name);
		sb.Append(" { ");

		PrintMembers(sb);

		sb.RemoveFromEnd(", ".Length);
		sb.Append(" }");
		return sb.ToString();
	}

	/// <inheritdoc cref="ICloneable.Clone"/>
	public abstract IslandConnector Clone();

	/// <summary>
	/// Print members.
	/// </summary>
	/// <param name="builder">The string builder instance.</param>
	protected abstract void PrintMembers(StringBuilder builder);

	/// <inheritdoc/>
	object ICloneable.Clone() => Clone();


	/// <summary>
	/// Appends the string for the current member.
	/// </summary>
	/// <typeparam name="T">The type of value.</typeparam>
	/// <param name="builder">The string builder.</param>
	/// <param name="value">The value.</param>
	/// <param name="parameterName">The parameter name to <paramref name="value"/>.</param>
	protected static void AppendMemberString<T>(StringBuilder builder, T value, [CallerArgumentExpression(nameof(value))] string parameterName = null!)
		=> builder.Append($"{parameterName} = {value}, ");


	/// <inheritdoc/>
	public static bool operator ==(IslandConnector? left, IslandConnector? right)
		=> (left, right) switch { (null, null) => true, (not null, not null) => left.Equals(right), _ => false };

	/// <inheritdoc/>
	public static bool operator !=(IslandConnector? left, IslandConnector? right) => !(left == right);
}
