using System;
using System.Collections.Generic;
using System.Text;

public class TodoItem
{
    public string Title;
    public string Description;
    public bool IsCompleted;

    public TodoItem(string title, string description, bool isCompleted = false)
    {
        Title = title;
        Description = description;
        IsCompleted = isCompleted;
    }
}

public class Program
{
    private static List<TodoItem> todoItems = new List<TodoItem>();
    public static string filePath = "items.txt";

    public static void Main()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("To-Do List Application");
            Console.WriteLine("1. Add Item");
            Console.WriteLine("2. View Items");
            Console.WriteLine("3. Mark Item Complete");
            Console.WriteLine("4. Save data");
            Console.WriteLine("5. Load data");
            Console.WriteLine("6. Exit");
            Console.Write("Choose an option: ");

            var option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    AddItem();
                    break;
                case "2":
                    ViewItems();
                    break;
                case "3":
                    MarkItemComplete();
                    break;
                case "4":
                    SaveData();
                    break;
                case "5":
                    LoadData();
                    break;
                case "6":
                    return;
                default:
                    Console.WriteLine("Invalid option, Please Try Again!");
                    Console.WriteLine("Press Enter to continue..");
                    Console.ReadLine();
                    break;
            }
        }
    }

    private static void AddItem()
    {
        Console.Write("Enter Title: ");
        var title = Console.ReadLine();
        Console.Write("Enter the description: ");
        var description = Console.ReadLine();
        if (!string.IsNullOrEmpty(title) && !string.IsNullOrEmpty(description))
        {
            todoItems.Add(new TodoItem(title, description));
        }
        else
        {
            Console.WriteLine("Title and Description cannot be empty.");
            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
        }
    }

    private static void ViewItems()
    {
        int count = 1;

        // Check if the list is empty
        if (todoItems.Count == 0)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Your list is currently empty. Please add some items");
            Console.ResetColor();
            Console.WriteLine("Press Enter to Continue....");
            Console.ReadLine();
            return; // Exit the method early
        }

        bool allCompleted = true;

        // Display all items
        foreach (var item in todoItems)
        {
            Console.Write($"{count}. ");
            Console.Write($"Title: {item.Title}\nDescription: {item.Description}\nStatus: ");
            if (item.IsCompleted)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Completed");
            }
            else
            {
                allCompleted = false;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("NOT Completed");
            }
            Console.ResetColor();
            count++;
        }

        // Display message if all tasks are completed
        if (allCompleted)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Well done! All tasks are marked as completed!");
            Console.ResetColor();
        }

        Console.WriteLine("Press Enter to Continue....");
        Console.ReadLine();
    }


    private static void LoadData()
    {
        if (File.Exists(filePath))
        {
            todoItems.Clear();
            string[] items = File.ReadAllLines(filePath);
            for (int i = 0; i < items.Length; i++)
            {
                string[] itemSplits = items[i].Split(';');
                string title = itemSplits[0].Split(":")[1];
                string description = itemSplits[1].Split(":")[1];
                string isCompleted = itemSplits[2].Split(":")[1];

                bool Completed = isCompleted == "true" ? true : false; 
                TodoItem item = new TodoItem(title, description, Completed);
                todoItems.Add(item);

            }
        }
    }
    public static void SaveData()
    {
        StringBuilder sb = new StringBuilder();
        foreach (TodoItem item in todoItems) 
        {
            sb.Append($"Title:{item.Title};");
            sb.Append($"Description:{item.Description};");
            sb.Append($"Status:{item.IsCompleted}");
            sb.Append(Environment.NewLine);
        }

        File.WriteAllText(filePath, sb.ToString());
    }

    private static void MarkItemComplete()
    {
        Console.Write("Enter the number of item to mark complete: ");
        bool isValid = int.TryParse(Console.ReadLine(), out int indx);
        if (isValid && indx > 0 && indx <= todoItems.Count)
        {
            todoItems[indx - 1].IsCompleted = true;
        }
        else
        {
            Console.WriteLine("Invalid item number!");
            Console.WriteLine("Press Enter to Continue...");
            Console.ReadLine();
        }
    }
}
