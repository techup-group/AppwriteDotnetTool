using System;
using System.Collections.Generic;

public class UserSelection
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="options"></param>
    /// <param name="prompt"></param>
    /// <returns></returns>
    public static string GetSelection(IEnumerable<string> options, string prompt = "Select an option:")
    {
        var optionList = options.ToList();
        var selectedIndex = 0;

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
    /// 
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
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