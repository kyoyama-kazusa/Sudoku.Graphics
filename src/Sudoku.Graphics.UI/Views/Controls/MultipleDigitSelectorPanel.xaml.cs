namespace Sudoku.Graphics.UI.Views.Controls;

/// <summary>
/// Interaction logic for MultipleDigitSelectorPanel.xaml.
/// </summary>
public partial class MultipleDigitSelectorPanel : UserControl
{
	public static readonly DependencyProperty RowsCountProperty =
		DependencyProperty.Register(
			nameof(RowsCount),
			typeof(int),
			typeof(MultipleDigitSelectorPanel),
			new PropertyMetadata(0, OnBoardConfigChanged)
		);

	public static readonly DependencyProperty ColumnsCountProperty =
		DependencyProperty.Register(
			nameof(ColumnsCount),
			typeof(int),
			typeof(MultipleDigitSelectorPanel),
			new PropertyMetadata(0, OnBoardConfigChanged)
		);

	public static readonly DependencyProperty MaxDigitProperty =
		DependencyProperty.Register(
			nameof(MaxDigit),
			typeof(int),
			typeof(MultipleDigitSelectorPanel),
			new PropertyMetadata(0, OnBoardConfigChanged)
		);

	public static readonly DependencyProperty SelectedDigitsProperty =
		DependencyProperty.Register(
			nameof(SelectedDigits),
			typeof(int[]),
			typeof(MultipleDigitSelectorPanel),
			new FrameworkPropertyMetadata((int[])[], FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnSelectedDigitsChanged)
		);

	public static readonly DependencyProperty CellSizeProperty =
		DependencyProperty.Register(
			nameof(CellSize),
			typeof(double),
			typeof(MultipleDigitSelectorPanel),
			new PropertyMetadata(40D, OnBoardConfigChanged)
		);

	public static readonly DependencyProperty DigitEnabledMapProperty =
		DependencyProperty.Register(
			nameof(DigitEnabledMap),
			typeof(BitArray),
			typeof(MultipleDigitSelectorPanel),
			new PropertyMetadata(null, OnBoardConfigChanged)
		);


	private readonly HashSet<int> _selectedDigits = [];


	/// <summary>
	/// Initializes a <see cref="MultipleDigitSelectorPanel"/> instance.
	/// </summary>
	public MultipleDigitSelectorPanel()
	{
		InitializeComponent();

		Loaded += (_, _) => RefreshBoard();
	}

	public int RowsCount
	{
		get => (int)GetValue(RowsCountProperty);

		set => SetValue(RowsCountProperty, value);
	}

	public int ColumnsCount
	{
		get => (int)GetValue(ColumnsCountProperty);

		set => SetValue(ColumnsCountProperty, value);
	}

	public int MaxDigit
	{
		get => (int)GetValue(MaxDigitProperty);

		set => SetValue(MaxDigitProperty, value);
	}

	public int[]? SelectedDigits
	{
		get => (int[]?)GetValue(SelectedDigitsProperty);

		set => SetValue(SelectedDigitsProperty, value?[..]);
	}

	public double CellSize
	{
		get => (double)GetValue(CellSizeProperty);

		set => SetValue(CellSizeProperty, value);
	}

	public BitArray? DigitEnabledMap
	{
		get => (BitArray?)GetValue(DigitEnabledMapProperty);

		set => SetValue(DigitEnabledMapProperty, value);
	}

	public OperationHandlerContext? OperationHandlerContext { get; set; }


	public event EventHandler<MultipleDigitSelectorPanel, MultipleDigitSelectorPanelSelectedDigitsChangedEventArgs>? SelectedDigitsChanged;


	private void RefreshBoard()
	{
		if (PART_Root is null)
		{
			return;
		}

		PART_Root.Children.Clear();
		PART_Root.RowDefinitions.Clear();
		PART_Root.ColumnDefinitions.Clear();

		var rows = Math.Max(0, RowsCount);
		var columns = Math.Max(0, ColumnsCount);
		if (rows == 0 || columns == 0)
		{
			return;
		}

		for (var r = 0; r < rows; r++)
		{
			PART_Root.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
		}

		for (var c = 0; c < columns; c++)
		{
			PART_Root.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
		}

		var totalCells = rows * columns;
		var count = Math.Min(Math.Max(0, MaxDigit), totalCells);
		for (var digit = 0; digit < count; digit++)
		{
			var row = digit / columns;
			var column = digit % columns;
			var button = new ToggleButton
			{
				Content = (digit + 1).ToString(),
				Width = CellSize,
				Height = CellSize,
				Margin = new(2),
				Tag = digit,
				IsChecked = _selectedDigits.Contains(digit),
				IsEnabled = IsDigitEnabled(digit),
				HorizontalAlignment = HorizontalAlignment.Center,
				VerticalAlignment = VerticalAlignment.Center,
				FontSize = FontSize,
				FontFamily = FontFamily,
				FontWeight = FontWeight,
				Foreground = Foreground
			};
			button.Checked += DigitButtonStateChanged;
			button.Unchecked += DigitButtonStateChanged;

			Grid.SetRow(button, row);
			Grid.SetColumn(button, column);

			PART_Root.Children.Add(button);
		}
	}

	private void DigitButtonStateChanged(object sender, RoutedEventArgs e)
	{
		if (sender is not ToggleButton button || button.Tag is not int digit)
		{
			return;
		}

		if (button.IsChecked is true)
		{
			_selectedDigits.Add(digit);
		}
		else
		{
			_selectedDigits.Remove(digit);
		}
	}

	private void ConfirmButton_Click(object sender, RoutedEventArgs e)
		=> SelectedDigits = [.. from digit in _selectedDigits orderby digit select digit];

	private void SyncSelectionFromProperty()
	{
		_selectedDigits.Clear();

		var digits = SelectedDigits;
		if (digits is not null)
		{
			foreach (var digit in digits)
			{
				if (digit >= 1)
				{
					_selectedDigits.Add(digit);
				}
			}
		}

		RefreshBoard();
	}

	public void SetDigitEnabledMap(BitArray? map) => DigitEnabledMap = map is null ? null : (BitArray)map.Clone();

	public void SetDigitEnabled(int digit, bool enabled)
	{
		if (digit < 1)
		{
			return;
		}

		var targetLength = Math.Max(MaxDigit, digit);
		if (targetLength <= 0)
		{
			return;
		}

		BitArray next;
		if (DigitEnabledMap is null)
		{
			next = new(targetLength, true);
		}
		else
		{
			next = (BitArray)DigitEnabledMap.Clone();
			if (next.Length < targetLength)
			{
				var oldLength = next.Length;
				next.Length = targetLength;
				for (var i = oldLength; i < next.Length; i++)
				{
					next[i] = true;
				}
			}
		}

		next[digit] = enabled;
		DigitEnabledMap = next;
	}

	private bool IsDigitEnabled(int digit)
		=> digit >= 0 && (DigitEnabledMap is null || digit >= DigitEnabledMap.Length || DigitEnabledMap[digit]);


	private void ClearButton_Click(object sender, RoutedEventArgs e) => SelectedDigits = null;


	private static void OnBoardConfigChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		=> ((MultipleDigitSelectorPanel)d).RefreshBoard();

	private static void OnSelectedDigitsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		var panel = (MultipleDigitSelectorPanel)d;
		panel.SyncSelectionFromProperty();
		panel.SelectedDigitsChanged?.Invoke(
			panel,
			new(panel.SelectedDigits, panel.OperationHandlerContext ?? throw new InvalidOperationException("Expected non-null context."))
		);
	}
}
