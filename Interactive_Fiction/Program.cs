using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interactive_Fiction
{
    internal class Program
    {

        // static char[,] worldMap = new char[,]
        static string[,] storyBook = new string[,]
        {
          //  |------------------------------------------------------------------------------------------------------------|
            {"Interactive Fiction Prototype\t*****************************\t\tBy Chris Schnurr", "blank", "blank", "blank", "blank"},
            {"   A dusty dirt road comes to a crossroads. A sign at the fork indicates that there is a village to the left\tand a forest to the right. ", "Choice A1", "Choice B1", "3", "4"},
            {"Story Page 2", "Choice A2", "Choice B2", "5", "6",},
        };

        static int pageNumber = 0;
        static string getPageContents;
        static int defaultRow = 5;
        static int defaultColumn = 5;
        static string[] options = {"blank" , "blank"};
        static int newChoice;
        static string playerChose;

        static void Main(string[] args)
        {
            bool gameOver = false;
            
            Console.SetWindowSize(120, 40);
            Console.BufferHeight = Console.WindowHeight;

            GameFrame();
            PrintStory();
            Console.SetCursorPosition(3, 33);
            Console.Write("Press any key to continue. ");
            Console.ReadKey(true);
            Console.Clear();

            pageNumber++;

            while (gameOver == false)
            {
                for (int count = 1; count < storyBook.GetLength(1); count++)
                {
                    options[count - 1] = storyBook[pageNumber, count];
                }

                GameFrame();
                PrintStory();

                newChoice = Choice(true, options);
                playerChose = options[newChoice];

                if (playerChose == storyBook[pageNumber, 1])
                {
                    pageNumber = Int32.Parse(storyBook[pageNumber, 3]);
                }
                else if (playerChose == storyBook[pageNumber, 2])
                {
                    pageNumber = Int32.Parse(storyBook[pageNumber, 4]);
                }
                else if (playerChose == "-1")
                {
                    QuestionQuit();
                }

                Console.SetCursorPosition(3, 33);
                Console.WriteLine("Player chose " + playerChose);
                Console.ReadKey(true);

                if (pageNumber == 99)
                {
                    gameOver = true;
                }
            }
                GameEnd();
        }

        static void PrintStory()
        {
            Console.SetCursorPosition(5, 5);
            getPageContents = storyBook[pageNumber, 0];
            string[] PageContents = getPageContents.Split('\t');

            int pageRow = defaultRow;

            foreach (var line in PageContents)
            {
                Console.SetCursorPosition(defaultColumn, pageRow);
                Console.WriteLine(line);
                pageRow++;
            }
            


        }

        static void GameFrame()
        {
            Console.SetCursorPosition(1, 1);

            // Console.Write("│┤╡╢←╕╣║╗╝╜╛┐╚╔╩╦╠═╬╧╔╩╦╠═╬╧╨╤╥╙╘╒╓╫╪┘┌█▄");╟╞╟╞┼──

            Console.Write("╔");

            for (int i = 0; i < 117; i++)
            {
                Console.Write("═");
            }

            Console.WriteLine("╗");

            for (int i = 2; i < 31; i++)
            {
                Console.SetCursorPosition(1, i);
                Console.Write("║");

                for (int x = 0; x < 117; x++)
                {
                    Console.Write(" ");
                }

                Console.Write("║");
            }

            Console.SetCursorPosition(1, Console.CursorTop);
            Console.Write("╟");

            for (int i = 0; i < 117; i++)
            {
                Console.Write("─");
            }

            Console.Write("╢");

            for (int i = 32; i < 37; i++)
            {
                Console.SetCursorPosition(1, i);
                Console.Write("║");

                for (int x = 0; x < 117; x++)
                {
                    Console.Write(" ");
                }

                Console.Write("║");
            }


            Console.SetCursorPosition(1, Console.CursorTop);
            Console.Write("╚");

            for (int i = 0; i < 117; i++)
            {
                Console.Write("═");
            }

            Console.WriteLine("╝");
        }

        public static int Choice(bool canCancel, params string[] options)
        {

            int startX = 3; // (Console.CursorLeft + 1);
            int startY = 33; // Console.CursorTop;
            const int optionsPerLine = 1;

            int currentSelection = 0;

            ConsoleKey key;

            Console.CursorVisible = false;

            do
            {
                Console.SetCursorPosition(startX, startY);

                for (int i = 0; i < options.Length; i++)
                {
                    if (i == currentSelection)
                    {
                        Console.BackgroundColor = ConsoleColor.Blue;
                    }

                    Console.WriteLine(" " + options[i] + " ");

                    Console.SetCursorPosition(startX, Console.CursorTop);

                    Console.ResetColor();
                }

                key = Console.ReadKey(true).Key;

                switch (key)
                {
                    case ConsoleKey.LeftArrow:
                        {
                            if (currentSelection % optionsPerLine > 0)
                                currentSelection--;
                            break;
                        }
                    case ConsoleKey.RightArrow:
                        {
                            if (currentSelection % optionsPerLine < optionsPerLine - 1)
                                currentSelection++;
                            break;
                        }
                    case ConsoleKey.UpArrow:
                        {
                            if (currentSelection >= optionsPerLine)
                                currentSelection -= optionsPerLine;
                            break;
                        }
                    case ConsoleKey.DownArrow:
                        {
                            if (currentSelection + optionsPerLine < options.Length)
                                currentSelection += optionsPerLine;
                            break;
                        }
                    case ConsoleKey.Escape:
                        {
                            if (canCancel)
                                return -1;
                            break;
                        }
                }
            } while (key != ConsoleKey.Enter);

            Console.CursorVisible = true;

            return currentSelection;
        }

        static void QuestionQuit()
        {
            Console.SetCursorPosition(5, 5);
            Console.WriteLine("Are you sure you want to quit?");

            options[0] = "YES";
            options[1] = "NO";

            newChoice = Choice(false, options);
            playerChose = options[newChoice];

            if (playerChose == storyBook[pageNumber, 1])
            {
                pageNumber = Int32.Parse(storyBook[pageNumber, 3]);
            }
            else if (playerChose == storyBook[pageNumber, 2])
            {
                pageNumber = Int32.Parse(storyBook[pageNumber, 4]);
            }

        static void GameEnd()
        {
            GameFrame();
            Console.SetCursorPosition(5, 5);
            Console.WriteLine("Thank you for testing my Interactive Fiction Prototype!");
            Console.SetCursorPosition(3, 33);
            Console.Write("Press any key to quit. ");
            Console.ReadKey(true);
        }
    }
}
