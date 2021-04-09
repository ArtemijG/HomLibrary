using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.Entity;


namespace Library_WPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DataBase.Context db;
        public MainWindow()
        {
            InitializeComponent();
            db = new DataBase.Context();
            LibraryLoad();
        }


        private void BooksLoad()
        {
            using (DataBase.Context db = new DataBase.Context())
            {
                var books = db.Books.Select(p => new
                {
                    Id = p.Id,
                    Name = p.Name,
                    Year = p.Year,
                    Description = p.Description,
                    Genre = p.Genre.Name,
                    Quantity = p.Quantity,
                    Author = p.Author.Name
                });
                BooksGrid.ItemsSource = books.ToList();

                var author = db.Authors.Select(p => p.Name);
                ListAuthors.ItemsSource = author.ToList();

                var genre = db.Genres.Select(p => p.Name);
                Genre.ItemsSource = genre.ToList();
            }
        }

       

        private void booksView_Click(object sender, RoutedEventArgs e)
        {

            if (Readers.Visibility == Visibility.Visible)
                Readers.Visibility = Visibility.Collapsed;
            else if (Library.Visibility == Visibility.Visible)
                Library.Visibility = Visibility.Collapsed;
            else Authors.Visibility = Visibility.Collapsed;


            Books.Visibility = Visibility.Visible;

            BooksLoad();
            db.Dispose();
        }



        private void ReadersLoad()
        {
            using (DataBase.Context db = new DataBase.Context())
            {
                var readers = db.Readers.Select(p => new
                {
                    Id = p.Id,
                    Name = p.Name
                });
                ReadersGrid.ItemsSource = readers.ToList();
            }
        }

        private void readersView_Click(object sender, RoutedEventArgs e)
        {

            if (Books.Visibility == Visibility.Visible)
                Books.Visibility = Visibility.Collapsed;
            else if (Library.Visibility == Visibility.Visible)
                Library.Visibility = Visibility.Collapsed;
            else  Authors.Visibility = Visibility.Collapsed;

            Readers.Visibility = Visibility.Visible;

            ReadersLoad();
            db.Dispose();
        }


        private void AuthorsLoad()
        {
            using (DataBase.Context db = new DataBase.Context())
            {
                var authors = db.Authors.Select(p => new
                {
                    Id = p.Id,
                    Name = p.Name
                });
                AuthorsGrid.ItemsSource = authors.ToList();
            }
        }

        private void authorsView_Click(object sender, RoutedEventArgs e)
        {
            if (Books.Visibility == Visibility.Visible)
                Books.Visibility = Visibility.Collapsed;
            else if (Library.Visibility == Visibility.Visible)
                Library.Visibility = Visibility.Collapsed;
            else Readers.Visibility = Visibility.Collapsed;

            Authors.Visibility = Visibility.Visible;

            AuthorsLoad();
            db.Dispose();
        }


        private void LibraryLoad()
        {
            using (DataBase.Context db = new DataBase.Context())
            {
                //db = new DataBase.Context();
                //db.Libraries.Load(); // загружаем данные
                //LibraryGrid.ItemsSource = db.Libraries.Local.ToBindingList();
                 var libraries = db.Libraries.Select(p => new
                 {
                     Id = p.Id,
                     NameReader = p.Reader.Name,
                     NameBook = p.Book.Name,
                     DateOfTook = p.DateOfTook,
                     DateOfReturn = p.DateOfReturn,
                     Author = p.Book.Author.Name

                 });

                 LibraryGrid.ItemsSource = libraries.ToList();

                var reader = db.Readers.Select(p => p.Name);
                Reader.ItemsSource = reader.ToList();


                var book = db.Books.Select(p => p.Name);
                Book.ItemsSource = book.ToList();

                db.Dispose();
            }
        
        }
        private void libraryView_Click(object sender, RoutedEventArgs e)
        {
            

            if (Books.Visibility == Visibility.Visible)
                Books.Visibility = Visibility.Collapsed;
            else if( Authors.Visibility == Visibility.Visible)
                Authors.Visibility = Visibility.Collapsed;
            else Readers.Visibility = Visibility.Collapsed;

            Library.Visibility = Visibility.Visible;

            LibraryLoad();
            /*using (DataBase.Context db = new DataBase.Context())
            {
                db = new DataBase.Context();
                db.Libraries.Load(); // загружаем данные
                LibraryGrid.ItemsSource = db.Libraries.Local.ToBindingList();
            }*/
            
        }
        private void Add_Author(object sender, RoutedEventArgs e)
        {
            try
            {
                using (DataBase.Context db = new DataBase.Context())
                {
                    var author = db.Authors.Where(p => p.Name == Name_Author.Text);

                    if (author.Count() == 0)
                    {
                        DataBase.Author a = new DataBase.Author { Name = Name_Author.Text };
                        db.Authors.Add(a);
                        db.SaveChanges();
                        AuthorsLoad();
                    }
                    else MessageBox.Show("Данный автор уже существует");
                }
            }
            catch (Exception e1)
            {
                MessageBox.Show("Данные введены не верно");
            }
        }

        private void Add_Book(object sender, RoutedEventArgs e)
        {
            try
            {
                using (DataBase.Context db = new DataBase.Context())
                {



                    var book = db.Books.Where(p =>
                        p.Name == Name_Book.Text &&
                        p.Author.Name == ListAuthors.SelectedItem.ToString());

                    var genre = db.Genres.Where(p => p.Name == Genre.SelectedItem.ToString());

                    var a = db.Authors.Where(p => p.Name == ListAuthors.SelectedItem.ToString());

                    if (book.Count() == 0)
                    {
                        DataBase.Book b = new DataBase.Book { Name = Name_Book.Text, Year = Convert.ToInt32(Year.Text), Description = Description.Text, Genre = genre.FirstOrDefault(), Quantity = Convert.ToInt32(Quantity.Text), Author = a.FirstOrDefault() };

                        db.Books.Add(b);
                        db.SaveChanges();
                        BooksLoad();
                    }
                    else MessageBox.Show("Данная книга уже существует");
                }
            }
            catch (Exception e1)
            {
                MessageBox.Show("Данные введены не верно");
            }
            
        }


    private void Add_Reader(object sender, RoutedEventArgs e)
    {
        try
        {
            using (DataBase.Context db = new DataBase.Context())
            {
                var reader = db.Readers.Where(p => p.Name == Name_Reader.Text);

                if (reader.Count() == 0)
                {
                    DataBase.Reader r = new DataBase.Reader { Name = Name_Reader.Text };
                    db.Readers.Add(r);
                    db.SaveChanges();
                    ReadersLoad();
                }
                else MessageBox.Show("Данный читатель уже существует");
            }
        }
        catch (Exception e1)
        {
            MessageBox.Show("Данные введены не верно");
        }
    }
        

        private void CreateLibrary_Click(object sender, System.ComponentModel.CancelEventArgs e)
        {
            db.Dispose();
        }
        
        private void Library_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            db.Dispose();
        }

        private void Add_Took(object sender, RoutedEventArgs e)
        {
            try
            {
                    using (DataBase.Context db = new DataBase.Context())
                {
                var reader = db.Readers.Where(p => p.Name == Reader.Text);

                var book = db.Books.Where(p => p.Name == Book.Text);

                var libr = db.Libraries.Where(p => p.Book.Name == Book.Text && p.Reader.Name == Reader.Text);


                DateTime d1 = DateTime.Today;
                    if (libr.Count() == 0)
                    {
                    DataBase.Library l = new DataBase.Library { DateOfTook = d1, DateOfReturn = d1.AddMonths(2),  Book = book.FirstOrDefault(), Reader = reader.FirstOrDefault() };
                    db.Libraries.Add(l);
                    db.SaveChanges();
                    LibraryLoad();
                    MessageBox.Show("Книга выдана на 2 месяца");
                     }
                else MessageBox.Show("Данная запись уже существует");
                }
            db.Dispose();
        }
            catch (Exception e1)
            {
                MessageBox.Show("Данные введены не верно");
            }
        }


    }

}
