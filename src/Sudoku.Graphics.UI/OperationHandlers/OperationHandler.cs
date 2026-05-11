namespace Sudoku.Graphics.UI.OperationHandlers;

/// <summary>
/// Provides base operation on an <see cref="Item"/>.
/// </summary>
/// <seealso cref="Item"/>
public abstract class OperationHandler
{
	/// <summary>
	/// Indicates whether the operation handler can be reused in different events, mouse down and up.
	/// By default it's <see langword="true"/>.
	/// </summary>
	public virtual bool UseDifferentInstancesBetweenEvents => true;

	/// <summary>
	/// Indicates whether the operation handler will differ events, mouse down and up.
	/// By default it's <see langword="false"/>. If <see langword="false"/>, clicked point on mouse down event is applied in both events.
	/// </summary>
	public virtual bool DiffersMousePositionsOnEvents => false;


#pragma warning disable CS0809
	/// <inheritdoc/>
	[Obsolete($"This type does not support '{nameof(Equals)}' method.", true)]
	public sealed override bool Equals(object? obj) => throw new NotSupportedException();

	/// <inheritdoc/>
	[Obsolete($"This type does not support '{nameof(GetHashCode)}' method.", true)]
	public sealed override int GetHashCode() => throw new NotSupportedException();

	/// <inheritdoc/>
	[Obsolete($"This type does not support '{nameof(ToString)}' method.", true)]
	public sealed override string ToString() => throw new NotSupportedException();
#pragma warning restore CS0809

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
	/// Indicates whether the current operation is available.
	/// </summary>
	/// <param name="context">The context.</param>
	/// <returns>A <see cref="bool"/> result indicating whether the operation is available or not.</returns>
	protected internal virtual bool IsAvailable(OperationHandlerContext context) => true;


	/// <summary>
	/// Update items.
	/// </summary>
	/// <param name="window">The window.</param>
	/// <param name="itemSetHandler">The handler of item set.</param>
	protected static void UpdateItems(MainWindow window, Action<ItemSet> itemSetHandler)
	{
		if (window.CurrentCanvas is { } canvas)
		{
			ref var items = ref getItems(window);
			itemSetHandler(items);
			canvas.DrawItems(items);
			renderPicture(window);
		}


		[UnsafeAccessor(UnsafeAccessorKind.Method, Name = "RenderPicture")]
		static extern void renderPicture(MainWindow window);

		[UnsafeAccessor(UnsafeAccessorKind.Field, Name = "_items")]
		static extern ref ItemSet getItems(MainWindow window);
	}
}
