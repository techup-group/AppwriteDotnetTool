using System;
using System.Collections.Generic;

public class UserSelection
{
    /// <summary>
    /// Displays a list of options and allows the user to select one using the arrow keys and Enter.
    /// </summary>
    /// <param name="options">The list of options to display.</param>
    /// <param name="prompt">The prompt to display above the options.</param>
    /// <returns>The selected option.</returns>
    public static string GetSelection(IEnumerable<string> options, string prompt = "Select an option:")
    {
        var optionList = options.ToList();
        var selectedIndex = 0;

        // Loop until the user selects an option using the Enter key
        while (true)
        {
            Console.Clear();
            Console.WriteLine(prompt);

            for (int i = 0; i < optionList.Count; i++)
            {
                if (i == selectedIndex)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"> {optionList[i]}");
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine($"  {optionList[i]}");
                }
            }

            var key = Console.ReadKey(true).Key;

            if (key == ConsoleKey.DownArrow)
            {
                selectedIndex = Math.Min(selectedIndex + 1, optionList.Count - 1);
            }
            else if (key == ConsoleKey.UpArrow)
            {
                selectedIndex = Math.Max(selectedIndex - 1, 0);
            }
            else if (key == ConsoleKey.Enter)
            {
                return optionList[selectedIndex];
            }
        }
    }

    /// <summary>
    /// Prompts the user to answer a yes/no question and returns the result.
    /// </summary>
    /// <param name="message">The prompt to display to the user.</param>
    /// <returns>True if the user answers yes, false otherwise.</returns>
    public static bool GetBooleanAnswer(string message)
    {
        while (true)
        {
            Console.Write(message + " (y/n): ");
            var response = Console.ReadKey().Key;

            if (response == ConsoleKey.Y)
            {
                Console.WriteLine();
                return true;
            }
            else if (response == ConsoleKey.N)
            {
                Console.WriteLine();
                return false;
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("Invalid input. Please enter 'y' for yes or 'n' for no.");
            }
        }
    }
}