namespace Sudoku.Graphics.UI;

/// <summary>
/// Provides event handler arguments with context. 
/// </summary>
/// <param name="context">The context.</param>
public abstract class ContextBasedEventArgs(OperationHandlerContext context) : EventArgs
{
	/// <summary>
	/// Indicates the context used.
	/// </summary>
	public OperationHandlerContext Context { get; } = context;
}
