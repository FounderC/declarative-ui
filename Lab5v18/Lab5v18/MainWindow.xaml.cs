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
            // LINQ Where
            var whereTab = new TabItem
            {
                Header = "Books before 2000",
                Content = new DataGrid
                {
                    Name = "WhereGrid",
                    AutoGenerateColumns = true,
                    ItemsSource = context.Books
                                         .Where(b => b.PublicationYear < 2000)
                                         .Select(b => new
                                         {
                                             b.ISBN,
                                             b.Authors,
                                             b.PublisherId,
                                             b.PublicationYear
                                         })
                                         .ToList()
                }
            };
            mainTabControl.Items.Add(whereTab);

            // LINQ Select
            var publishersTab = new TabItem
            {
                Header = "Publishers",
                Content = new DataGrid
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
                }
            };
            mainTabControl.Items.Add(publishersTab);

            // LINQ Select
            var booksTab = new TabItem
            {
                Header = "Books",
                Content = new DataGrid
                {
                    Name = "BooksGrid",
                    AutoGenerateColumns = true,
                    ItemsSource = context.Books
                                         .Select(b => new
                                         {
                                             b.ISBN,
                                             b.Authors,
                                             b.PublisherId,
                                             b.Publishers.PublisherName,
                                             b.PublicationYear
                                         })
                                         .ToList()
                }
            };
            mainTabControl.Items.Add(booksTab);

            // SQL-запит через SqlQuery
            var booksSqlTab = new TabItem
            {
                Header = "Books (SQL)",
                Content = new DataGrid
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
                }
            };
            mainTabControl.Items.Add(booksSqlTab);

            // LINQ Join
            var joinTab = new TabItem
            {
                Header = "Books & Publishers",
                Content = new DataGrid
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
                                                   p.PublisherName,
                                                   b.PublicationYear
                                               })
                                         .ToList()
                }
            };
            mainTabControl.Items.Add(joinTab);

            // LINQ GroupBy
            var groupTab = new TabItem
            {
                Header = "Count by Publisher",
                Content = new DataGrid
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
                }
            };
            mainTabControl.Items.Add(groupTab);

            // Search-tab
            var searchTab = new TabItem { Header = "Search" };
            var container = new Grid();
            container.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            container.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            container.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });

            var hint = new TextBlock
            {
                Text = "Виберіть поле та введіть текст для пошуку:",
                Margin = new Thickness(5),
                FontStyle = FontStyles.Italic
            };
            Grid.SetRow(hint, 0);
            container.Children.Add(hint);

            var panel = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                Margin = new Thickness(5)
            };
            var combo = new ComboBox
            {
                Name = "SearchFieldComboBox",
                Width = 120,
                Margin = new Thickness(0, 0, 5, 0),
                ItemsSource = new[] { "ISBN", "Authors", "PublicationYear" },
                SelectedIndex = 1
            };
            var tb = new TextBox
            {
                Name = "SearchTextBox",
                Width = 200,
                Margin = new Thickness(0, 0, 5, 0),
                ToolTip = "Введіть значення для вибраного поля"
            };
            var btn = new Button
            {
                Content = "Search",
                Width = 75
            };
            panel.Children.Add(new TextBlock { Text = "Field:", VerticalAlignment = VerticalAlignment.Center, Margin = new Thickness(0, 0, 5, 0) });
            panel.Children.Add(combo);
            panel.Children.Add(tb);
            panel.Children.Add(btn);
            Grid.SetRow(panel, 1);
            container.Children.Add(panel);

            // DataGrid
            var resultGrid = new DataGrid
            {
                Name = "SearchResultsGrid",
                AutoGenerateColumns = true,
                Margin = new Thickness(5)
            };
            Grid.SetRow(resultGrid, 2);
            container.Children.Add(resultGrid);

            btn.Click += (_, __) =>
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

            searchTab.Content = container;
            mainTabControl.Items.Add(searchTab);
        }
    }
}
