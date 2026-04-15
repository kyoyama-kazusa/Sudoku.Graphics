namespace Sudoku.Graphics.UI.Views.OtherWindows;

/// <summary>
/// Interaction logic for CreateNewCanvasWindow.xaml.
/// </summary>
[INotifyPropertyChanged]
public partial class CreateNewCanvasWindow : Window
{
	public CreateNewCanvasWindow()
	{
		InitializeComponent();

		DataContext = this;
	}


	public bool IsStandardMode => CreateCanvasMode == CreateCanvasMode.StandardTemplate;

	public bool IsDefaultMode => CreateCanvasMode == CreateCanvasMode.DefaultTemplate;

	[ObservableProperty]
	public partial bool IsBorderRoundedRectangle { get; set; } = false;

	[ObservableProperty]
	public partial bool DrawBordersAsThickLines { get; set; } = true;

	[ObservableProperty]
	public partial int RenderedCellSize { get; set; } = 120;

	[ObservableProperty]
	public partial int RenderedGridMargin { get; set; } = 15;

	[ObservableProperty]
	public partial int RowsCount { get; set; } = 9;

	[ObservableProperty]
	public partial int ColumnsCount { get; set; } = 9;

	[ObservableProperty]
	public partial int BlockRowsCount { get; set; } = 3;

	[ObservableProperty]
	public partial int BlockColumnsCount { get; set; } = 3;

	[ObservableProperty]
	public partial int VectorTop { get; set; } = 0;

	[ObservableProperty]
	public partial int VectorBottom { get; set; } = 0;

	[ObservableProperty]
	public partial int VectorLeft { get; set; } = 0;

	[ObservableProperty]
	public partial int VectorRight { get; set; } = 0;

	[ObservableProperty]
	[NotifyPropertyChangedFor(nameof(IsStandardMode))]
	[NotifyPropertyChangedFor(nameof(IsDefaultMode))]
	public partial CreateCanvasMode CreateCanvasMode { get; set; } = CreateCanvasMode.StandardTemplate;


	public Template CreateTemplate()
	{
		switch (CreateCanvasMode)
		{
			case CreateCanvasMode.StandardTemplate:
			{
				var mapper = new PointMapper
				{
					CellSize = RenderedCellSize,
					Margin = RenderedGridMargin,
					TemplateSize = new()
					{
						RowsCount = RowsCount,
						ColumnsCount = ColumnsCount,
						Vector = new(VectorLeft, VectorTop, VectorRight, VectorBottom)
					}
				};
				return new StandardTemplate(BlockRowsCount, BlockColumnsCount, mapper)
				{
					IsBorderRoundedRectangle = IsBorderRoundedRectangle,
					BorderCornerRadius = 0.25M, // Config
					ThickLineColor = SKColors.Black, // Config
					ThickLineDashSequence = [], // Config
					ThickLineWidth = 0.06M, // Config
					ThinLineColor = SKColors.Black, // Config
					ThinLineDashSequence = [], // Config
					ThinLineWidth = 0.0225M // Config
				};
			}
			case CreateCanvasMode.DefaultTemplate:
			{
				return new DefaultTemplate
				{
					Mapper = new()
					{
						CellSize = RenderedCellSize,
						Margin = RenderedGridMargin,
						TemplateSize = new()
						{
							RowsCount = RowsCount,
							ColumnsCount = ColumnsCount,
							Vector = new(VectorLeft, VectorTop, VectorRight, VectorBottom)
						}
					},
					IsBorderRoundedRectangle = IsBorderRoundedRectangle,
					BorderCornerRadius = 0.25M, // Config
					DrawBordersAsThickLines = DrawBordersAsThickLines, // Config
					ThickLineColor = SKColors.Black, // Config
					ThickLineDashSequence = [], // Config
					ThickLineWidth = 0.06M, // Config
					ThinLineColor = SKColors.Black, // Config
					ThinLineDashSequence = [], // Config
					ThinLineWidth = 0.0225M // Config
				};
			}
			default:
			{
				throw new NotSupportedException();
			}
		}
	}

	private void CreateButton_Click(object sender, RoutedEventArgs e)
	{
		DialogResult = true;
		Close();
	}

	private void CancelButton_Click(object sender, RoutedEventArgs e)
	{
		DialogResult = false;
		Close();
	}

	private void StandardTemplateRadioButton_Checked(object sender, RoutedEventArgs e)
		=> CreateCanvasMode = CreateCanvasMode.StandardTemplate;

	private void DefaultTemplateRadioButton_Checked(object sender, RoutedEventArgs e)
		=> CreateCanvasMode = CreateCanvasMode.DefaultTemplate;
}

public enum CreateCanvasMode
{
	StandardTemplate,

	DefaultTemplate
}
