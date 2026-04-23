namespace Sudoku.Graphics.UI.Views.Controls;

/// <summary>
/// Interaction logic for DigitSelectorPanel.xaml.
/// </summary>
public partial class DigitSelectorPanel : UserControl
{
	public static readonly DependencyProperty RowsCountProperty =
		DependencyProperty.Register(
			nameof(RowsCount),
			typeof(int),
			typeof(DigitSelectorPanel),
			new PropertyMetadata(0, OnBoardConfigChanged)
		);

	public static readonly DependencyProperty ColumnsCountProperty =
		DependencyProperty.Register(
			nameof(ColumnsCount),
			typeof(int),
			typeof(DigitSelectorPanel),
			new PropertyMetadata(0, OnBoardConfigChanged)
		);

	public static readonly DependencyProperty MaxDigitProperty =
		DependencyProperty.Register(
			nameof(MaxDigit),
			typeof(int),
			typeof(DigitSelectorPanel),
			new PropertyMetadata(0, OnBoardConfigChanged)
		);

	public static readonly DependencyProperty SelectedDigitProperty =
		DependencyProperty.Register(
			nameof(SelectedDigit),
			typeof(int),
			typeof(DigitSelectorPanel),
			new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault)
		);

	public static readonly DependencyProperty CellSizeProperty =
		DependencyProperty.Register(
			nameof(CellSize),
			typeof(double),
			typeof(DigitSelectorPanel),
			new PropertyMetadata(40D, OnBoardConfigChanged)
		);

	public static readonly DependencyProperty DigitEnabledMapProperty =
		DependencyProperty.Register(
			nameof(DigitEnabledMap),
			typeof(BitArray),
			typeof(DigitSelectorPanel),
			new PropertyMetadata(null, OnBoardConfigChanged)
		);


	/// <summary>
	/// Initializes a <see cref="DigitSelectorPanel"/> instance.
	/// </summary>
	public DigitSelectorPanel()
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

	public int SelectedDigit
	{
		get => (int)GetValue(SelectedDigitProperty);

		set => SetValue(SelectedDigitProperty, value);
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


	public event EventHandler<DigitSelectorPanel, DigitSelectorPanelSelectedDigitChangedEventArgs>? SelectedDigitChanged;


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
		for (var digit = 1; digit <= count; digit++)
		{
			var row = (digit - 1) / columns;
			var column = (digit - 1) % columns;
			var button = new Button
			{
				Content = digit.ToString(),
				Width = CellSize,
				Height = CellSize,
				Margin = new(2),
				Tag = digit,
				HorizontalAlignment = HorizontalAlignment.Center,
				VerticalAlignment = VerticalAlignment.Center,
				FontSize = FontSize,
				FontFamily = FontFamily,
				FontWeight = FontWeight,
				Foreground = Foreground
			};
			button.Click += DigitButton_Click;

			Grid.SetRow(button, row);
			Grid.SetColumn(button, column);

			PART_Root.Children.Add(button);
		}
	}

	private void DigitButton_Click(object sender, RoutedEventArgs e)
	{
		if (sender is not Button button || button.Tag is not int digit)
		{
			return;
		}

		SelectedDigit = digit;
		SelectedDigitChanged?.Invoke(
			this,
			new(digit, OperationHandlerContext ?? throw new InvalidOperationException("Expect non-null context."))
		);
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

		next[digit - 1] = enabled;
		DigitEnabledMap = next;
	}

	private bool IsDigitEnabled(int digit)
		=> digit >= 1 && (DigitEnabledMap is null || digit - 1 is var index && (index >= DigitEnabledMap.Length || DigitEnabledMap[index]));


	private static void OnBoardConfigChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		=> ((DigitSelectorPanel)d).RefreshBoard();
}
