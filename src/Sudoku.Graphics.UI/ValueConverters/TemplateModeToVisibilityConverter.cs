namespace Sudoku.Graphics.UI.ValueConverters;

/// <summary>
/// Represents a converter that checks for template type and returns a <see cref="Visibility"/> value
/// indicating the showing state of a control.
/// </summary>
/// <seealso cref="Visibility"/>
public sealed class TemplateModeToVisibilityConverter : IValueConverter
{
	/// <summary>
	/// Indicates whether the condition is reverted (not equal).
	/// </summary>
	public bool IsReverted { get; set; }


	/// <inheritdoc/>
	public object? Convert(object? value, Type? targetType, object? parameter, CultureInfo? culture)
		=> (value, parameter) is (CurrentCreateTemplateType left, CurrentCreateTemplateType right)
			? (IsReverted ? left != right : left == right) ? Visibility.Visible : Visibility.Collapsed
			: throw new NotSupportedException();

	/// <inheritdoc/>
	public object? ConvertBack(object? value, Type? targetType, object? parameter, CultureInfo? culture)
		=> throw new NotImplementedException();
}
