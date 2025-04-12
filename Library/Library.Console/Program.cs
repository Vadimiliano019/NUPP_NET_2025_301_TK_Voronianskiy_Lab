using System;
using Library.Common;

namespace Library.ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Створюємо сервіс для роботи з книгами
            var service = new CrudService<Book>();

            // 1) Створення двох книг
            var book1 = new Book
            {
                Id = Guid.NewGuid(),
                Title = "C# Basics",
                Author = "John Smith"
            };
            service.Create(book1);

            var book2 = new Book
            {
                Id = Guid.NewGuid(),
                Title = "Advanced C#",
                Author = "Jane Doe"
            };
            service.Create(book2);

            // 2) Вивід усіх книг
            Console.WriteLine("📚 All Books:");
            foreach (var book in service.ReadAll())
            {
                Console.WriteLine($"- {book.Title} by {book.Author} (ID: {book.Id})");
            }

            // 3) Видалення першої книги
            service.Remove(book1);

            // 4) Вивід після видалення
            Console.WriteLine("\n📚 After removing the first book:");
            foreach (var book in service.ReadAll())
            {
                Console.WriteLine($"- {book.Title} by {book.Author} (ID: {book.Id})");
            }

            // 5) Збереження в JSON-файл
            string filePath = "books.json";
            service.Save(filePath);
            Console.WriteLine($"\n✅ Data saved to {filePath}");

            // 6) Завантаження з JSON в новий сервіс
            var newService = new CrudService<Book>();
            newService.Load(filePath);

            // 7) Вивід даних із файлу
            Console.WriteLine("\n📦 Loaded from file:");
            foreach (var book in newService.ReadAll())
            {
                Console.WriteLine($"- {book.Title} by {book.Author} (ID: {book.Id})");
            }

            // Затримка, щоб консоль не закрилася одразу
            Console.WriteLine("\nНатисніть будь‑яку клавішу, щоб вийти...");
            Console.ReadKey();
        }
    }
}
