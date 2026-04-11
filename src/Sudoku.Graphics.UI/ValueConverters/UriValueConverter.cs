namespace Sudoku.Graphics.UI.ValueConverters;

/// <summary>
/// Represents a value converter on <see cref="Uri"/> type.
/// </summary>
/// <seealso cref="Uri"/>
public sealed class UriValueConverter : IValueConverter
{
	/// <inheritdoc/>
	public object? Convert(object? value, Type? targetType, object? parameter, CultureInfo? culture)
		=> value is string s ? new Uri(s) : throw new NotSupportedException();

	/// <inheritdoc/>
	public object? ConvertBack(object? value, Type? targetType, object? parameter, CultureInfo? culture)
		=> value is Uri uri ? uri.ToString() : throw new NotSupportedException();
}
