using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interactive_Fiction
{
    internal class Program
    {
        static string[] story = { "Welcome to Ozzy's Odyssey!", "A great, menacing castle stands before you. A large wooden door leads inside, while a side road leads around to the back.;Proceed inside;Follow the path;2;3;" , "You venture inside the castle. The lobby is dimly lit with a few candles. Cobwebs decorate the corners of the room and dust covers the furniture. You can take a door to the left and up to the second level, or straight to enter the main hallway.;Head upstairs;Move to the hallway;4;5" , "You make your way around the side of the castle. Around the first corner you find a beautiful garden full of plants and flowers. The pathway continues on to the back of the castle.;Stop to smell the flowers;Proceed along the path;6;7" , "4" , "5" , "You decide to take a moment to enjoy the garden. You choose a particularly large red flower, move in close, and inhale. The flower is so intoxicating you do not notice the vine wrap around your ankle. The vine suddenly yanks you into the brush and the garden swallows you whole. You are dead." , "7" };

        static string[] currentPageContents = { "blank"};
        static int currentPageNumber = 1;
        static char[] separators = new char[] { ';'};
        static void Main(string[] args)
        {
            bool gameOver = false;

            Console.WriteLine();
            Console.WriteLine(story[0]);
            Console.WriteLine();
            Console.WriteLine("Please press any key to begin the story.");
            Console.ReadKey(true);

            while (gameOver == false)
            {
                GetStory();
                
                if (currentPageContents.Length == 1)
                {
                    Console.Clear();

                    PrintPlot(currentPageContents[0]);

                    Console.WriteLine();
                    Console.WriteLine(currentPageContents[0]);
                    Console.WriteLine();
                    Console.WriteLine("You've completed the story!");
                    Console.WriteLine();
                    Console.WriteLine("A: Restart");
                    Console.WriteLine("B: Quit");
                    Console.WriteLine();

                    ConsoleKeyInfo end = Console.ReadKey(true);

                    if (end.Key == ConsoleKey.A)
                    {
                        currentPageNumber = 1;
                    }

                    if (end.Key == ConsoleKey.B)
                    {
                        gameOver = true;
                    }
                }

                if (currentPageContents.Length == 5)
                {
                    Console.Clear();

                    PrintPlot(currentPageContents[0]);

                    Console.WriteLine();
                    Console.WriteLine("Make Your Choice!");
                    Console.WriteLine();
                    Console.WriteLine("A: " + currentPageContents[1]);
                    Console.WriteLine("B: " + currentPageContents[2]);
                    Console.WriteLine();

                    ConsoleKeyInfo choice = Console.ReadKey(true);

                    if (choice.Key == ConsoleKey.A)
                    {
                        int nextPage = Int32.Parse(currentPageContents[3]);
                        currentPageNumber = nextPage;
                    }

                    if (choice.Key == ConsoleKey.B)
                    {
                        int nextPage = Int32.Parse(currentPageContents[4]);
                        currentPageNumber = nextPage;
                    }

                }
            }
        }

        static void GetStory()
        {
            string temp = story[currentPageNumber];
            currentPageContents = temp.Split(separators, StringSplitOptions.RemoveEmptyEntries);
        }

        static void PrintPlot(string plot)
        {


            if (plot.Length < 119)
            {
                Console.WriteLine(plot);
            }

            if (plot.Length > 119)
            {
                string newplot = null;
                string lengthcheck = null;

                string[] plotwords = plot.Split(' ');

                for (int i = 0; i < plotwords.Length; i++)
                {
                    newplot = newplot + plotwords[i];
                    newplot = newplot + " ";

                    if (i + 1 < plotwords.Length)
                    {
                        lengthcheck = newplot + plotwords[i + 1];
                    }
                                            
                    if (lengthcheck.Length > 119 || i +1 == plotwords.Length)
                    {
                        Console.WriteLine(newplot);
                        newplot = null;
                        lengthcheck = " ";
                    }
                }
                
            }

        }
    }
}

