namespace System.Windows.Input;

/// <summary>
/// Provides extension members on <see cref="MouseEventArgs"/> instances.
/// </summary>
/// <seealso cref="MouseEventArgs"/>
public static class MouseEventArgsExtensions
{
	/// <summary>
	/// Provides extension members on <see cref="MouseEventArgs"/> instances.
	/// </summary>
	/// <param name="this">The current instance.</param>
	extension(MouseEventArgs @this)
	{
		/// <summary>
		/// Indicates position clicked, related to <see cref="RoutedEventArgs.OriginalSource"/>.
		/// </summary>
		/// <exception cref="InvalidCastException">
		/// Throws when <see cref="RoutedEventArgs.OriginalSource"/> is not <see cref="IInputElement"/>.
		/// </exception>
		/// <seealso cref="RoutedEventArgs.OriginalSource"/>
		/// <seealso cref="IInputElement"/>
		public Point Position => @this.GetPosition((IInputElement)@this.OriginalSource);
	}
}
