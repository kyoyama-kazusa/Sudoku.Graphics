namespace Sudoku.Graphics.UI.ValueConverters;

/// <summary>
/// Represents a converter that converts <see cref="bool"/> values into <see cref="Visibility"/> values.
/// </summary>
/// <seealso cref="Visibility"/>
public sealed class BooleanToVisibilityConverter : IValueConverter
{
	/// <summary>
	/// Indicates the condition is reverted.
	/// </summary>
	public bool IsReverted { get; set; }


	/// <inheritdoc/>
	public object? Convert(object? value, Type? targetType, object? parameter, CultureInfo? culture)
		=> (value is bool b ? b ^ IsReverted : throw new NotSupportedException()) ? Visibility.Visible : Visibility.Collapsed;

	/// <inheritdoc/>
	public object? ConvertBack(object? value, Type? targetType, object? parameter, CultureInfo? culture)
		=> throw new NotImplementedException();
}
