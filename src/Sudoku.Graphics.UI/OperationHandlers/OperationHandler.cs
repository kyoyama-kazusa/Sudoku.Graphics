#pragma warning disable CS0809

namespace Sudoku.Graphics.UI.OperationHandlers;

/// <summary>
/// Provides base operation on an <see cref="Item"/>.
/// </summary>
/// <seealso cref="Item"/>
public abstract class OperationHandler
{
	/// <inheritdoc/>
	[Obsolete($"This type does not support '{nameof(Equals)}' method.", true)]
	public sealed override bool Equals(object? obj) => throw new NotSupportedException();

	/// <inheritdoc/>
	[Obsolete($"This type does not support '{nameof(GetHashCode)}' method.", true)]
	public sealed override int GetHashCode() => throw new NotSupportedException();

	/// <inheritdoc/>
	[Obsolete($"This type does not support '{nameof(ToString)}' method.", true)]
	public sealed override string ToString() => throw new NotSupportedException();

	/// <summary>
	/// Process when mouse button is pressed (mouse down).
	/// </summary>
	/// <param name="button">The button pressed.</param>
	/// <param name="context">The context.</param>
	protected internal abstract void OnMouseButtonPressed(OperationHandlerContext context);

	/// <summary>
	/// Process when mouse button is released (mouse up).
	/// </summary>
	/// <param name="button">The button released.</param>
	/// <param name="context">The context.</param>
	protected internal abstract void OnMouseButtonReleased(OperationHandlerContext context);

	/// <summary>
	/// Craeates a new <see cref="Item"/> instance.
	/// </summary>
	/// <param name="context">The context.</param>
	/// <returns>The item created, or <see langword="null"/> if failed to create.</returns>
	protected internal abstract Item? CreateItem(OperationHandlerContext context);
}
