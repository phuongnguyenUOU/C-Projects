using System;
using System.Collections.Generic;

namespace TodoApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create an empty list to hold todo items
            List<string> todoList = new List<string>();

            // Loop through the program until the user exits
            while (true)
            {
                // Display the current todo list
                Console.WriteLine("Todo List:");
                for (int i = 0; i < todoList.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {todoList[i]}");
                }
                Console.WriteLine();

                // Prompt the user for an action
                Console.WriteLine("Enter an action:");
                Console.WriteLine("1. Add todo item");
                Console.WriteLine("2. Remove todo item");
                Console.WriteLine("3. Exit");
                Console.Write("> ");

                // Get the user's input
                string input = Console.ReadLine();

                // Process the user's input
                if (input == "1")
                {
                    Console.Write("Enter todo item: ");
                    string item = Console.ReadLine();
                    todoList.Add(item);
                    Console.WriteLine($"Added \"{item}\" to todo list.");
                }
                else if (input == "2")
                {
                    Console.Write("Enter item number to remove: ");
                    int index = int.Parse(Console.ReadLine()) - 1;
                    if (index >= 0 && index < todoList.Count)
                    {
                        string item = todoList[index];
                        todoList.RemoveAt(index);
                        Console.WriteLine($"Removed \"{item}\" from todo list.");
                    }
                    else
                    {
                        Console.WriteLine("Invalid item number.");
                    }
                }
                else if (input == "3")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid input.");
                }

                Console.WriteLine();
            }

            Console.WriteLine("Goodbye!");
        }
    }
}