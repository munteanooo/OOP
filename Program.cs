using System;
using System.Collections.Generic;

public class Book
{
    public string Title { get; set; }
    public string ISBN { get; set; }  
    public string Author { get; set; }

    public override string ToString()
    {
        return $"Title: {Title}, ISBN: {ISBN}, Author: {Author}";
    }
}

public class Library
{
    private List<Book> books = new List<Book>();

    public void AddBook()
    {
        Book newBook = new Book();

        Console.Write("Enter book title: ");
        newBook.Title = Console.ReadLine();

        while (true)
        {
            Console.Write("Enter book ISBN (13 digits with spaces allowed): ");
            string isbnInput = Console.ReadLine();

            string cleanedIsbn = isbnInput.Replace(" ", "");

            if (cleanedIsbn.Length == 13 && long.TryParse(cleanedIsbn, out _))
            {
                newBook.ISBN = isbnInput; 
                break;
            }
            else
            {
                Console.WriteLine("Invalid ISBN. Please enter a 13-digit number (spaces are allowed).");
            }
        }

        Console.Write("Enter book author: ");
        newBook.Author = Console.ReadLine();

        books.Add(newBook);
        Console.WriteLine("Book added successfully!\n");
    }

    public void DeleteBook()
    {
        Console.Write("Enter ISDN of the book to delete: ");
        string titleToDelete = Console.ReadLine();

        Book bookToRemove = books.Find(book => book.ISBN == titleToDelete);
        if (bookToRemove != null)
        {
            books.Remove(bookToRemove);
            Console.WriteLine("Book deleted successfully!\n");
        }
        else
        {
            Console.WriteLine("Book ISBN not found!\n");
        }
    }

    public void ShowBooks()
    {
        if (books.Count > 0)
        {
            Console.WriteLine("Books in the library:");
            foreach (Book book in books)
            {
                Console.WriteLine(book.ToString());
            }
        }
        else
        {
            Console.WriteLine("No books in the library.\n");
        }
    }
}

public class Program
{
    public static void Main()
    {
        Library library = new Library();
        bool exit = false;

        while (!exit)
        {
            Console.WriteLine("\nChoose an option:");
            Console.WriteLine("1. Add a book");
            Console.WriteLine("2. Delete a book");
            Console.WriteLine("3. Show all books");
            Console.WriteLine("4. Exit");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    library.AddBook();
                    break;
                case "2":
                    library.DeleteBook();
                    break;
                case "3":
                    library.ShowBooks();
                    break;
                case "4":
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Invalid option, try again.");
                    break;
            }
        }
    }
}
