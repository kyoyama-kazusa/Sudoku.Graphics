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
	/// Indicates basic serializer options.
	/// </summary>
	private static readonly JsonSerializerOptions SerializerOptions = new()
	{
		WriteIndented = true,
		AllowTrailingCommas = false,
		IncludeFields = false,
		IgnoreReadOnlyFields = true,
		IgnoreReadOnlyProperties = true,
		AllowDuplicateProperties = false,
		RespectNullableAnnotations = true,
		RespectRequiredConstructorParameters = true,
		PropertyNameCaseInsensitive = false,
		AllowOutOfOrderMetadataProperties = true,
		IndentCharacter = ' ',
		IndentSize = 2,
		MaxDepth = 8,
		NewLine = "\r\n",
		Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
		PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
		DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
		PreferredObjectCreationHandling = JsonObjectCreationHandling.Populate,
		ReadCommentHandling = JsonCommentHandling.Skip,
		UnknownTypeHandling = JsonUnknownTypeHandling.JsonElement,
		DefaultIgnoreCondition = JsonIgnoreCondition.Never,
		UnmappedMemberHandling = JsonUnmappedMemberHandling.Skip,
		NumberHandling = JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.AllowNamedFloatingPointLiterals,
		TypeInfoResolver = JsonTypeInfoResolver.Combine(
			JsonTypeInfoResolver.Create<Template>(),
			JsonTypeInfoResolver.Create<Item>(),
			new DefaultJsonTypeInfoResolver()
		),
		Converters =
		{
			new JsonStringEnumConverter()
		}
	};


	private ItemSet _items = [];

	private Canvas? _canvas;


	public MainWindow()
	{
		InitializeComponent();

		DataContext = this;
	}


	[ObservableProperty]
	public partial string CurrentModeString { get; set; } = LocalizationResources.ResourceManager.GetString("ItemType_None")!;

	[ObservableProperty]
	public partial ItemType CurrentItemType { get; set; } = ItemType.None;

	[ObservableProperty]
	public partial ImageSource? GridImageSource { get; set; }

	public ICommand CreateCanvasCommand => new RelayCommand(OpenCreateCanvasWindowAndRenderPicture);

	public ICommand CloseCanvasCommand => new RelayCommand(ClosePicture);

	public ICommand SaveCanvasCommand => new RelayCommand(SaveAsPictureFile);

	public ICommand SaveAsJsonCommand => new AsyncRelayCommand(SaveAsJsonFileAsync);

	public ICommand LoadFromLocalCommand => new AsyncRelayCommand(LoadFromJsonFileAsync);

	public ICommand QuitCommand => new RelayCommand(Close);


	private void AboutMeMenuItem_Click(object sender, RoutedEventArgs e) => new AboutWindow { Owner = this }.ShowDialog();

	private void ToolItemButton_Click(object sender, RoutedEventArgs e)
	{
		if (sender is Button { Tag: ToolItem item })
		{
			CurrentItemType = item.ItemType;
		}
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

		InitializeItems();
		ReinitializeCanvasAndItems([window.CreateTemplate()]);
		RenderPicture();
	}

	[MemberNotNull(nameof(_items))]
	private void ReinitializeCanvasAndItems(ItemSet items) => _items = items;

	[MemberNotNull(nameof(_canvas))]
	private void ReinitializeCanvasAndItems(Template[] templates) => _canvas = new(templates);

	[MemberNotNull(nameof(_items), nameof(_canvas))]
	private void ReinitializeCanvasAndItems(Template[] templates, ItemSet items)
	{
		ReinitializeCanvasAndItems(templates);
		ReinitializeCanvasAndItems(items);
	}

	private void DrawItemsToCanvas() => _canvas?.DrawItems(_items);

	private void RenderPicture()
	{
		if (_canvas is null)
		{
			return;
		}

		DrawItemsToCanvas();

		using var image = _canvas.Surface.Snapshot();
		GridImageSource = image.ToWriteableBitmap();
	}

	private void ClosePicture()
	{
		GridImageSource = null;
		_canvas = null;
		_items.Clear();
	}

	private void InitializeItems()
	{
		_items.Add(new BackgroundFillItem { Color = R(() => App.UserPreferences.BackgroundFillColor) });
		_items.Add(new TemplateLineItem());
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
		var encoder = (BitmapEncoder)(
			extension switch
			{
				".jpg" or ".jpeg" => new JpegBitmapEncoder(),
				".bmp" => new BmpBitmapEncoder(),
				_ => new PngBitmapEncoder()
			}
		);

		encoder.Frames.Add(BitmapFrame.Create(GridImageSource));
		using var stream = File.Create(filePath);
		encoder.Save(stream);
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

		var canvasInfo = new CanvasInfo(_canvas?.Templates, _items);
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
		if (JsonSerializer.Deserialize<CanvasInfo>(json, SerializerOptions) is not ({ } templates, { } items))
		{
			return;
		}

		ReinitializeCanvasAndItems(templates, items);
		RenderPicture();
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

	partial void OnCurrentItemTypeChanged(ItemType value)
	{
		var modeString = LocalizationResources.ResourceManager.GetString($"ItemType_{value}");
		if (modeString is not null)
		{
			CurrentModeString = modeString;
		}
	}
}
