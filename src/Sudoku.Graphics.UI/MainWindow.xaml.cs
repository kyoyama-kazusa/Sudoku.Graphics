namespace Sudoku.Graphics.UI;

/// <summary>
/// Interaction logic for MainWindow.xaml.
/// </summary>
[INotifyPropertyChanged]
public partial class MainWindow : Window
{
	/// <summary>
	/// Indicates the threshold of file size that can be loaded.
	/// </summary>
	private const long FileSizeThreshold = 10 * 1024 * 1024;


	/// <summary>
	/// Indicates operation handler factory dictionary.
	/// </summary>
	private static readonly Dictionary<ItemType, Func<OperationHandler>> ItemOperationHandlerFactory;

	/// <summary>
	/// Indicates bitmap encoder factory dictionary.
	/// </summary>
	private static readonly Dictionary<string, Func<BitmapEncoder>> BitmapEncoderFactory;

	/// <summary>
	/// Indicates basic serializer options.
	/// </summary>
	private static readonly JsonSerializerOptions SerializerOptions;


	/// <summary>
	/// The backing items used.
	/// </summary>
	private ItemSet _items = [];

	/// <summary>
	/// The backing snapshot. The value can only be initialized in methods
	/// <list type="bullet">
	/// <item><see cref="OpenCreateCanvasWindowAndRenderPicture"/></item>
	/// <item><see cref="ClosePicture"/></item>
	/// </list>
	/// </summary>
	private Snapshot? _snapshot;

	/// <summary>
	/// The operation handler context.
	/// </summary>
	private OperationHandlerContext? _operationHandlerContext;

	/// <summary>
	/// The previous operation handler.
	/// </summary>
	private OperationHandler? _previousOperationHandler;


	public MainWindow()
	{
		InitializeComponent();

		DataContext = this;

		InitializeValues();
	}


	[ObservableProperty]
	public partial string CurrentModeString { get; set; } = LocalizationResources.ItemType_None;

	[ObservableProperty]
	public partial ItemType CurrentItemType { get; set; } = ItemType.None;

	[ObservableProperty]
	public partial ImageSource? GridImageSource { get; set; }

	internal bool IsDirectIslandMode => CurrentIslandConnectorMode == IslandConnectorMode.Direct;

	internal bool IsSingleCornerIslandMode => CurrentIslandConnectorMode == IslandConnectorMode.SingleCorner;

	internal bool IsDoubleCornersIslandMode => CurrentIslandConnectorMode == IslandConnectorMode.DoubleCorners;

	internal bool IsDirection1Up => IslandCorner1Direction == Direction4.Up;

	internal bool IsDirection1Down => IslandCorner1Direction == Direction4.Down;

	internal bool IsDirection1Left => IslandCorner1Direction == Direction4.Left;

	internal bool IsDirection1Right => IslandCorner1Direction == Direction4.Right;

	internal bool IsDirection2Up => IslandCorner2Direction == Direction4.Up;

	internal bool IsDirection2Down => IslandCorner2Direction == Direction4.Down;

	internal bool IsDirection2Left => IslandCorner2Direction == Direction4.Left;

	internal bool IsDirection2Right => IslandCorner2Direction == Direction4.Right;

	[ObservableProperty]
	[NotifyPropertyChangedFor(nameof(IsDirection1Up))]
	[NotifyPropertyChangedFor(nameof(IsDirection1Down))]
	[NotifyPropertyChangedFor(nameof(IsDirection1Left))]
	[NotifyPropertyChangedFor(nameof(IsDirection1Right))]
	internal partial Direction4 IslandCorner1Direction { get; set; }

	[ObservableProperty]
	[NotifyPropertyChangedFor(nameof(IsDirection2Up))]
	[NotifyPropertyChangedFor(nameof(IsDirection2Down))]
	[NotifyPropertyChangedFor(nameof(IsDirection2Left))]
	[NotifyPropertyChangedFor(nameof(IsDirection2Right))]
	internal partial Direction4 IslandCorner2Direction { get; set; }

	[ObservableProperty]
	[NotifyPropertyChangedFor(nameof(IsDirectIslandMode))]
	[NotifyPropertyChangedFor(nameof(IsSingleCornerIslandMode))]
	[NotifyPropertyChangedFor(nameof(IsDoubleCornersIslandMode))]
	internal partial IslandConnectorMode CurrentIslandConnectorMode { get; set; }

	/// <summary>
	/// Indicates the current canvas.
	/// </summary>
	[ObservableProperty]
	internal partial Canvas? CurrentCanvas { get; set; }

	/// <summary>
	/// Indicates the target grid to operate.
	/// </summary>
	[ObservableProperty]
	internal partial SudokuGrid? CurrentGrid { get; set; }

	public ICommand CreateCanvasCommand => new RelayCommand(OpenCreateCanvasWindowAndRenderPicture);

	public ICommand CloseCanvasCommand => new RelayCommand(ClosePicture);

	public ICommand SaveCanvasCommand => new RelayCommand(SaveAsPictureFile);

	public ICommand SaveAsJsonCommand => new AsyncRelayCommand(SaveAsJsonFileAsync);

	public ICommand LoadFromLocalCommand => new AsyncRelayCommand(LoadFromJsonFileAsync);

	public ICommand ClearAllItemsCommand => new RelayCommand(ClearAllItemsExceptGridTemplateLines);

	public ICommand QuitCommand => new RelayCommand(Close);


	private void InitializeValues()
	{
		CurrentIslandConnectorMode = IslandConnectorMode.Direct;
		IslandCorner1Direction = Direction4.Up;
		IslandCorner2Direction = Direction4.Up;
		IslandConnectorDirectModeRadioButton.IsChecked = true;
		CellPairIslandConnectorDirection1UpRadioButton.IsChecked = true;
		CellPairIslandConnectorDirection2UpRadioButton.IsChecked = true;
		CellPairIslandConnectorOffsetInputBox.Value = 1;
	}

	private void OpenCreateCanvasWindowAndRenderPicture()
	{
		if (GridImageSource is not null)
		{
			ClosePicture();
		}

		var window = new CreateNewCanvasWindow { Owner = this };
		var result = window.ShowDialog();
		if (result is not true)
		{
			return;
		}

		var backgroundFill = new BackgroundFillItem { Color = ResolveProperty(() => App.UserPreferences.BackgroundFillColor) };
		var templateLine = new TemplateLineItem();
		_items.Add(backgroundFill);
		_items.Add(templateLine);
		var defaultTemplate = window.CreateTemplate(out var grid);
		CurrentCanvas = new(defaultTemplate);
		CurrentGrid = grid;
		_snapshot = new(backgroundFill, templateLine, defaultTemplate, grid);

		RenderPicture();
	}

	private void RenderPicture()
	{
		if (CurrentCanvas is not null)
		{
			CurrentCanvas.DrawItems(_items);

			using var image = CurrentCanvas.Surface.Snapshot();
			GridImageSource = image.ToWriteableBitmap();
		}
	}

	private void ClosePicture()
	{
		GridImageSource = null;
		CurrentCanvas = null;
		CurrentGrid = null;
		_snapshot = null;
		_items.Clear();
	}

	private void SaveAsPictureFile()
	{
		if (GridImageSource is null)
		{
			return;
		}

		var dialog = new SaveFileDialog
		{
			Title = LocalizationResources.SaveFileDialog_Title,
			Filter = LocalizationResources.SaveFileDialog_Filters,
			DefaultExt = ".png",
			AddExtension = true
		};
		var filePath = dialog.ShowDialog() is true ? dialog.FileName : null;
		if (filePath is null)
		{
			return;
		}

		var extension = Path.GetExtension(filePath);
		var encoder = BitmapEncoderFactory[extension]();
		encoder.Frames.Add(BitmapFrame.Create(GridImageSource));
		using var stream = File.Create(filePath);
		encoder.Save(stream);
	}

	private void UpdateGridRelatedItems()
	{
		// Clear old items.
		_items.Clear(ItemType.Text_Given);
		_items.Clear(ItemType.Text_Modifiable);
		_items.Clear(ItemType.Text_Candidate);

		// Render picture.
		if (CurrentGrid is null)
		{
			goto OnRenderingPicture;
		}

		var givens = CurrentGrid.Givens;
		var modifiables = CurrentGrid.Modifiables;
		var candidates = CurrentGrid.Candidates;
		for (var cell = 0; cell < CurrentGrid.CellsCount; cell++)
		{
			if (givens[cell] is var givenDigit and not -1)
			{
				_items.Add(ItemsFactory.Given(cell, givenDigit));
			}
			if (modifiables[cell] is var modifiableDigit and not -1)
			{
				_items.Add(ItemsFactory.Modifiable(cell, modifiableDigit));
			}
			if (candidates[cell] is { } candidateDigits)
			{
				var subgridSize = CurrentGrid.RowsCount.GetCandidatesCountInEachRow();
				var digits = new List<int>();
				for (var digit = 0; digit < CurrentGrid.DigitsCount; digit++)
				{
					if (candidateDigits[digit])
					{
						digits.Add(digit);
					}
				}
				_items.AddRange(ItemsFactory.Candidates(cell, digits, subgridSize));
			}
		}

	OnRenderingPicture:
		RenderPicture();
	}

	private async Task SaveAsJsonFileAsync()
	{
		if (GridImageSource is null)
		{
			return;
		}

		var dialog = new SaveFileDialog
		{
			Title = LocalizationResources.SaveFileDialog_Title_Json,
			Filter = LocalizationResources.SaveFileDialog_Filters_Json,
			DefaultExt = ".json",
			AddExtension = true
		};
		var filePath = dialog.ShowDialog() is true ? dialog.FileName : null;
		if (filePath is null)
		{
			return;
		}

		var canvasInfo = new SerializableCanvasInfo(CurrentCanvas?.Template, _items);
		var json = JsonSerializer.Serialize(canvasInfo, SerializerOptions);
		await File.WriteAllTextAsync(filePath, json);
	}

	private async Task LoadFromJsonFileAsync()
	{
		var dialog = new OpenFileDialog
		{
			Title = LocalizationResources.OpenFileDialog_Title_Json,
			Filter = LocalizationResources.OpenFileDialog_Filters_Json,
			DefaultExt = ".json",
			AddExtension = true
		};
		var filePath = dialog.ShowDialog() is true ? dialog.FileName : null;
		if (filePath is null)
		{
			return;
		}

		if (new FileInfo(filePath).Length > FileSizeThreshold)
		{
			var message = string.Format(LocalizationResources.MessageBox_FileLengthExceeded, FileSizeThreshold.ToFileLengthString());
			MessageBox.Show(this, message);
			return;
		}

		var json = await File.ReadAllTextAsync(filePath);
		if (JsonSerializer.Deserialize<SerializableCanvasInfo>(json, SerializerOptions) is not ({ } templates, { } items))
		{
			return;
		}

		CurrentCanvas = new(templates);
		_items = items;

		RenderPicture();
	}

	private void ClearAllItemsExceptGridTemplateLines()
	{
		if (_snapshot is not var (backgroundFill, templateLine, defaultTemplate, grid))
		{
			return;
		}

		ClosePicture();

		_items.Add(backgroundFill);
		_items.Add(templateLine);
		CurrentCanvas = new(defaultTemplate);
		CurrentGrid = grid;
		RenderPicture();
	}


	partial void OnCurrentItemTypeChanged(ItemType value)
	{
		var modeString = LocalizationResources.ResourceManager.GetString($"ItemType_{value}");
		if (modeString is not null)
		{
			CurrentModeString = modeString;
		}
	}

	partial void OnCurrentGridChanging(SudokuGrid? value)
	{
		if (value is not null)
		{
			value.DigitAdded -= CurrentGrid_DigitsAdded;
			value.CandidateAdded -= CurrentGrid_CandidateAdded;
			value.DigitRemoved -= CurrentGrid_DigitRemoved;
			value.CandidateRemoved -= CurrentGrid_CandidateRemoved;
			value.Cleared -= CurrentGrid_Cleared;
			value.CellCleared -= CurrentGrid_CellCleared;
		}
	}

	partial void OnCurrentGridChanged(SudokuGrid? value)
	{
		if (value is not null)
		{
			value.DigitAdded += CurrentGrid_DigitsAdded;
			value.CandidateAdded += CurrentGrid_CandidateAdded;
			value.DigitRemoved += CurrentGrid_DigitRemoved;
			value.CandidateRemoved += CurrentGrid_CandidateRemoved;
			value.Cleared += CurrentGrid_Cleared;
			value.CellCleared += CurrentGrid_CellCleared;
		}
	}

	private void CurrentGrid_CandidateRemoved(SudokuGrid sender, SudokuGridCandidateRemovedEventArgs e) => UpdateGridRelatedItems();

	private void CurrentGrid_CandidateAdded(SudokuGrid sender, SudokuGridCandidateAddedEventArgs e) => UpdateGridRelatedItems();

	private void CurrentGrid_CellCleared(SudokuGrid sender, SudokuGridCellRefreshedEventArgs e) => UpdateGridRelatedItems();

	private void CurrentGrid_Cleared(SudokuGrid sender, SudokuGridClearedEventArgs e) => UpdateGridRelatedItems();

	private void CurrentGrid_DigitsAdded(SudokuGrid sender, SudokuGridDigitAddedEventArgs e) => UpdateGridRelatedItems();

	private void CurrentGrid_DigitRemoved(SudokuGrid sender, SudokuGridDigitRemovedEventArgs e) => UpdateGridRelatedItems();

	private void AboutMeMenuItem_Click(object sender, RoutedEventArgs e) => new AboutWindow { Owner = this }.ShowDialog();

	private void ToolItemButton_Click(object sender, RoutedEventArgs e)
	{
		if (sender is Button { Tag: ToolItem item })
		{
			CurrentItemType = item.ItemType;
		}
	}

	private void Window_Closing(object sender, CancelEventArgs e)
	{
		var configFolder = ReservedPaths.ConfigFolderPath;
		if (!Directory.Exists(configFolder))
		{
			Directory.CreateDirectory(configFolder);
		}

		var json = JsonSerializer.Serialize(App.UserPreferences, SerializerOptions);
		File.WriteAllText(ReservedPaths.ConfigFilePath, json);
	}

	private void Window_Initialized(object sender, EventArgs e)
	{
		var configFolder = ReservedPaths.ConfigFolderPath;
		if (!Directory.Exists(configFolder))
		{
			Directory.CreateDirectory(configFolder);
		}

		// Load config file if exists.
		if (!File.Exists(ReservedPaths.ConfigFilePath))
		{
			return;
		}

		var fileInfo = new FileInfo(ReservedPaths.ConfigFilePath);
		if (fileInfo.Length > FileSizeThreshold)
		{
			// Config file is too large - don't load.
			return;
		}

		var json = File.ReadAllText(ReservedPaths.ConfigFilePath);
		var instance = JsonSerializer.Deserialize<Preferences>(json, SerializerOptions);
		if (instance is null)
		{
			return;
		}

		App.UserPreferences = instance;
	}

	[MemberNotNull(nameof(_operationHandlerContext))]
	[MemberNotNull(nameof(_previousOperationHandler))]
	private void Image_MouseDown(object sender, MouseButtonEventArgs e)
	{
		_operationHandlerContext = new() { Items = _items, OwnerWindow = this, MouseEventArgs = e, PointPressed = e.Position };

		var handler = ItemOperationHandlerFactory[CurrentItemType]();
		if (handler.IsAvailable(_operationHandlerContext))
		{
			handler.OnMouseButtonPressed(_operationHandlerContext);
		}

		_previousOperationHandler = handler;
	}

	private void Image_MouseUp(object sender, MouseButtonEventArgs e)
	{
		if (_operationHandlerContext is null || _previousOperationHandler is null)
		{
			return;
		}

		var newInstance = ItemOperationHandlerFactory[CurrentItemType]();
		var handler = newInstance.UsesDifferentInstancesBetweenEvents ? newInstance : _previousOperationHandler;
		_operationHandlerContext.MouseEventArgs = e;
		if (handler.DiffersMousePositionsBetweenEvents)
		{
			_operationHandlerContext.PointPressed = e.Position;
		}

		if (handler.IsAvailable(_operationHandlerContext))
		{
			handler.OnMouseButtonReleased(_operationHandlerContext);
		}

		_operationHandlerContext = null;
		_previousOperationHandler = null;
	}

	private void IslandConnectorDirectionRadioButton_Click(object sender, RoutedEventArgs e)
		=> IslandCorner1Direction = (Direction4)((RadioButton)sender).Tag;

	private void IslandConnectorDirectionRadioButton2_Click(object sender, RoutedEventArgs e)
		=> IslandCorner2Direction = (Direction4)((RadioButton)sender).Tag;

	private void IslandConnectorModeRadioButton_Checked(object sender, RoutedEventArgs e)
		=> CurrentIslandConnectorMode = (IslandConnectorMode)((RadioButton)sender).Tag;
}
