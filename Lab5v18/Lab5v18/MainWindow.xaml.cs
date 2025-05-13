using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Lab5v18
{
    public partial class MainWindow : Window
    {
        private LibraryDBEntities context;

        public MainWindow()
        {
            InitializeComponent();
            context = new LibraryDBEntities();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var yearTab = new TabItem { Header = "Books before 2000" };
            var yearContainer = new Grid();
            yearContainer.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            yearContainer.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });

            var yearPanel = new StackPanel { Orientation = Orientation.Horizontal, Margin = new Thickness(5) };
            var yearBox = new TextBox { Width = 100, Margin = new Thickness(0, 0, 5, 0), ToolTip = "Enter year" };
            var filterBtn = new Button { Content = "Filter", Width = 75 };

            yearPanel.Children.Add(new TextBlock { Text = "Year <", VerticalAlignment = VerticalAlignment.Center, Margin = new Thickness(0, 0, 5, 0) });
            yearPanel.Children.Add(yearBox);
            yearPanel.Children.Add(filterBtn);

            Grid.SetRow(yearPanel, 0);
            yearContainer.Children.Add(yearPanel);

            var yearGrid = new DataGrid { Name = "YearGrid", AutoGenerateColumns = true, Margin = new Thickness(5) };
            Grid.SetRow(yearGrid, 1);
            yearContainer.Children.Add(yearGrid);

            filterBtn.Click += (_, __) =>
            {
                if (int.TryParse(yearBox.Text.Trim(), out var yr))
                {
                    yearGrid.ItemsSource = context.Books
                                                  .Where(b => b.PublicationYear < yr)
                                                  .Select(b => new
                                                  {
                                                      b.ISBN,
                                                      b.Authors,
                                                      b.PublisherId,
                                                      b.PublicationYear
                                                  })
                                                  .ToList();
                }
                else
                {
                    yearGrid.ItemsSource = null;
                }
            };

            // LINQ Select
            yearTab.Content = yearContainer;
            mainTabControl.Items.Add(yearTab);

            var publishersTab = new TabItem { Header = "Publishers" };
            var publishersGrid = new DataGrid
            {
                Name = "PublishersGrid",
                AutoGenerateColumns = true,
                ItemsSource = context.Publishers
                                     .Select(p => new
                                     {
                                         p.PublisherId,
                                         p.PublisherName,
                                         BookCount = p.Books.Count()
                                     })
                                     .ToList()
            };
            publishersTab.Content = publishersGrid;
            mainTabControl.Items.Add(publishersTab);

            // LINQ Select
            var booksTab = new TabItem { Header = "Books" };
            var booksGrid = new DataGrid
            {
                Name = "BooksGrid",
                AutoGenerateColumns = true,
                ItemsSource = context.Books
                                     .Select(b => new
                                     {
                                         b.ISBN,
                                         b.Authors,
                                         b.PublisherId,
                                         PublisherName = b.Publishers.PublisherName,
                                         b.PublicationYear
                                     })
                                     .ToList()
            };
            booksTab.Content = booksGrid;
            mainTabControl.Items.Add(booksTab);

            // SQL Query
            var booksSqlTab = new TabItem { Header = "Books (SQL)" };
            var booksSqlGrid = new DataGrid
            {
                Name = "BooksSqlGrid",
                AutoGenerateColumns = true,
                ItemsSource = context.Database
                                     .SqlQuery<Books>(
                                         "SELECT ISBN, Authors, PublisherId, PublicationYear FROM Books")
                                     .Select(b => new
                                     {
                                         b.ISBN,
                                         b.Authors,
                                         b.PublisherId,
                                         b.PublicationYear
                                     })
                                     .ToList()
            };
            booksSqlTab.Content = booksSqlGrid;
            mainTabControl.Items.Add(booksSqlTab);

            // Join Books
            var joinTab = new TabItem { Header = "Books & Publishers" };
            var joinGrid = new DataGrid
            {
                Name = "JoinGrid",
                AutoGenerateColumns = true,
                ItemsSource = context.Books
                                     .Join(context.Publishers,
                                           b => b.PublisherId,
                                           p => p.PublisherId,
                                           (b, p) => new
                                           {
                                               b.ISBN,
                                               b.Authors,
                                               b.PublisherId,
                                               PublisherName = p.PublisherName,
                                               b.PublicationYear
                                           })
                                     .ToList()
            };
            joinTab.Content = joinGrid;
            mainTabControl.Items.Add(joinTab);

            // GroupBy Count
            var groupTab = new TabItem { Header = "Count by Publisher" };
            var groupGrid = new DataGrid
            {
                Name = "GroupGrid",
                AutoGenerateColumns = true,
                ItemsSource = context.Books
                                     .Join(context.Publishers,
                                           b => b.PublisherId,
                                           p => p.PublisherId,
                                           (b, p) => new { p.PublisherName, b.ISBN })
                                     .GroupBy(x => x.PublisherName)
                                     .Select(g => new
                                     {
                                         Publisher = g.Key,
                                         Count = g.Count()
                                     })
                                     .ToList()
            };
            groupTab.Content = groupGrid;
            mainTabControl.Items.Add(groupTab);

            // Search Tab
            var searchTab = new TabItem { Header = "Search" };
            var searchContainer = new Grid();
            searchContainer.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            searchContainer.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            searchContainer.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });

            var hint = new TextBlock
            {
                Text = "Select field and enter value to search:",
                Margin = new Thickness(5),
                FontStyle = FontStyles.Italic
            };
            Grid.SetRow(hint, 0);
            searchContainer.Children.Add(hint);

            var searchPanel = new StackPanel { Orientation = Orientation.Horizontal, Margin = new Thickness(5) };
            var combo = new ComboBox
            {
                Name = "SearchFieldComboBox",
                Width = 120,
                Margin = new Thickness(0, 0, 5, 0),
                ItemsSource = new[] { "ISBN", "Authors", "PublicationYear" },
                SelectedIndex = 1
            };
            var tb = new TextBox { Name = "SearchTextBox", Width = 200, Margin = new Thickness(0, 0, 5, 0) };
            var btnSearch = new Button { Content = "Search", Width = 75 };
            searchPanel.Children.Add(new TextBlock { Text = "Field:", VerticalAlignment = VerticalAlignment.Center, Margin = new Thickness(0, 0, 5, 0) });
            searchPanel.Children.Add(combo);
            searchPanel.Children.Add(tb);
            searchPanel.Children.Add(btnSearch);
            Grid.SetRow(searchPanel, 1);
            searchContainer.Children.Add(searchPanel);

            var resultGrid = new DataGrid { Name = "SearchResultsGrid", AutoGenerateColumns = true, Margin = new Thickness(5) };
            Grid.SetRow(resultGrid, 2);
            searchContainer.Children.Add(resultGrid);

            btnSearch.Click += (_, __) =>
            {
                var field = combo.SelectedItem as string;
                var term = tb.Text?.Trim() ?? "";
                var query = context.Books.AsQueryable();

                switch (field)
                {
                    case "ISBN":
                        query = query.Where(b => b.ISBN.Contains(term));
                        break;
                    case "Authors":
                        query = query.Where(b => b.Authors.Contains(term));
                        break;
                    case "PublicationYear":
                        if (int.TryParse(term, out var year))
                            query = query.Where(b => b.PublicationYear == year);
                        else
                            query = query.Take(0);
                        break;
                }

                resultGrid.ItemsSource = query
                    .Select(b => new
                    {
                        b.ISBN,
                        b.Authors,
                        b.PublisherId,
                        b.PublicationYear
                    })
                    .ToList();
            };

            searchTab.Content = searchContainer;
            mainTabControl.Items.Add(searchTab);
        }
    }
}
