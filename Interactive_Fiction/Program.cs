using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interactive_Fiction
{
    internal class Program
    {
        // here we initialize the main components we need for the story engine
        static string[] story = 
        { 
            "Chris' Castle of Fates;Christopher Schnurr", 
            "A great, menacing castle stands before you. A large wooden door leads inside, while a side path leads around to the back.;Proceed inside;Follow the path;2;3;", 
            "The castle lobby is dimly lit with a few candles. Cobwebs decorate the corners of the room and dust covers the furniture. You can take a door to the left and up to the second level, or straight to enter the main hallway.;Head upstairs;Move to the hallway;4;5",
            "You make your way around the side of the castle. Around the first corner you find a beautiful garden full of plants and flowers. The pathway continues on to the back of the castle.;Stop to smell the flowers;Proceed along the path;6;10",
            "You stand amidst a large library. Book cases line the walls and a large sofa sits in the center of the room. There are no apparent exits except to head back down to the main level.;Inspect the books;Return to main level;7;2", 
            "The main floor hallway is plain and ordinary, other than the layer of dust and cobwebs. One door leads to the kitchen, another to the lounge.;Head to Kitchen;Proceed to Lounge;11;12;", 
            "You decide to take a moment to enjoy the garden. You choose a particularly large red flower, move in close, and inhale. The flower is so intoxicating you do not notice the vine wrap around your ankle. The vine suddenly yanks you into the brush and the garden swallows you whole. You are dead.", 
            "You approach the nearest bookshelf and begin to browse the titles. Most of the books are old and warn, covered in dust from years of stillness, except for two that appear to have been moved recently.;Look at book titled 'Greed';Look at book titled 'Hurricane Irene';8;9",
            "You reach for 'Greed' but it only tilts forward fromt he shelf, triggering a hidden mechanism. The book case slowly slides to the side revealing a dark, secret hallway.;Proceed down the secret hallway;Return to the Library;15;4",
            "You pull 'Hurricane Irene' from the shelf and open it. A huge gust of wind emits from the book, knocking it from your hand and onto the floor, still open. The room fills with the torrential chaos of Hurricane Irene, battering the library, furniture, and you until essentially nothing is left. You are dead.",
            "GARDEN PATH",
            "The kitchen appears to have been cleaned shortly before being abandoned. The counters are clear and tidy beneath the layer of dust. A large dingy fridge sits silently in the corner and a back door leads out to a porch.;Open the Fridge;Head to the Porch;13;14",
            "LOUNGE",
            "FRIDGE",
            "PORCH",
            "SECRET HALLWAY",
            "16",
            "17",
            "18",
            "19",
            "20",

        };

        static string[] currentPageContents = {"blank"};
        static int currentPageNumber = 1;
        static char[] separators = new char[] {';'};

        static System.Media.SoundPlayer click = new System.Media.SoundPlayer(@"C:\C# Projects\Interactive_Fiction\Interactive_Fiction\bin\Data\Sounds\click.wav");

        static void Main(string[] args)
        {
            bool gameOver = false;
            Console.CursorVisible = false;
            Console.SetBufferSize(Console.WindowWidth, Console.WindowHeight);

            // splash/intro page:

            string temp = story[0];
            currentPageContents = temp.Split(separators, StringSplitOptions.RemoveEmptyEntries);

            PrintFrame();

            Console.WriteLine(" Welcome to the Interactive Fiction Engine! The interactive story you will be enjoying is titled:");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(" " + currentPageContents[0]);
            Console.ResetColor();
            Console.Write(" by ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(currentPageContents[1]);
            Console.ResetColor();
            Console.WriteLine(".");
            Console.WriteLine();
            Console.WriteLine(" You can press escape at any time to exit.");
            Console.WriteLine();
            Console.WriteLine(" Please press any key to begin.");

            PrintFrame();

            Console.ReadKey(true);
            click.Play();

            // game loop:

            while (gameOver == false)
            {
                temp = story[currentPageNumber];
                currentPageContents = temp.Split(separators, StringSplitOptions.RemoveEmptyEntries);

                // if a page has no choices included it is considered an end page and is handled here:

                if (currentPageContents.Length == 1)
                {
                    Console.Clear();

                    PrintFrame();

                    PrintPlot(currentPageContents[0]);

                    Console.WriteLine();
                    Console.WriteLine(" You've completed the story!");
                    Console.WriteLine();
                    Console.WriteLine(" A: Restart");
                    Console.WriteLine(" B: Quit");

                    PrintFrame();

                    ConsoleKeyInfo end = Console.ReadKey(true);
                    click.Play();

                    if (end.Key == ConsoleKey.A)
                    {
                        currentPageNumber = 1;
                    }

                    if (end.Key == ConsoleKey.B)
                    {
                        gameOver = true;
                    }

                    if (end.Key == ConsoleKey.Escape)
                    {
                        gameOver = true;
                    }
                }

                // if a page includes two choices and two destinations (a standard page) it is handled here:

                if (currentPageContents.Length == 5)
                {
                    Console.Clear();

                    PrintFrame();

                    PrintPlot(currentPageContents[0]);

                    Console.WriteLine();
                    Console.WriteLine(" Make Your Choice:");
                    Console.WriteLine();
                    Console.Write(" A: ");
                    Console.ForegroundColor = ConsoleColor.Green; 
                    Console.WriteLine(currentPageContents[1]);
                    Console.ResetColor();
                    Console.Write(" B: ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(currentPageContents[2]);
                    Console.ResetColor();

                    PrintFrame();

                    ConsoleKeyInfo choice = Console.ReadKey(true);
                    click.Play();

                    int check;

                    if (choice.Key == ConsoleKey.A)
                    {
                        check = ErrorCheck(currentPageContents[3]);

                        if (check != 0)
                        {
                            currentPageNumber = check;
                        }
                    }

                    if (choice.Key == ConsoleKey.B)
                    {
                        check = ErrorCheck(currentPageContents[4]);

                        if (check != 0)
                        {
                            currentPageNumber = check;
                        }
                    }

                    if (choice.Key == ConsoleKey.Escape)
                    {
                        gameOver = true;
                    }
                }

                // if a page does not fall into the above cases it is handled here:

                if (currentPageContents.Length != 1 && currentPageContents.Length != 5)
                {
                    Console.Clear();

                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(" Error: the page we are trying to access is not formatted to fit our engine and can not be displayed. Please contact the story author.");
                    Console.ResetColor();

                    Console.WriteLine(" A: Restart");
                    Console.WriteLine(" B: Quit");
                    Console.WriteLine();

                    ConsoleKeyInfo choice = Console.ReadKey(true);
                    click.Play();

                    if (choice.Key == ConsoleKey.A)
                    {
                        currentPageNumber = 1;
                    }

                    if (choice.Key == ConsoleKey.B)
                    {
                        int nextPage = Int32.Parse(currentPageContents[4]);
                        currentPageNumber = nextPage;
                    }
                }
            }

            // After the player quits, this is the goodbye message:

            temp = story[0];
            currentPageContents = temp.Split(separators, StringSplitOptions.RemoveEmptyEntries);

            Console.Clear();

            PrintFrame();

            Console.Write(" Thank you for enjoying ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(currentPageContents[0]);
            Console.ResetColor();
            Console.Write(" by ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(currentPageContents[1]);
            Console.ResetColor();
            Console.WriteLine("!");
            Console.WriteLine();
            Console.WriteLine(" Brought to you by the Interactive Fiction Engine, by Christopher Schnurr.");
            Console.WriteLine();
            Console.WriteLine(" Please press any key to end.");

            PrintFrame();

            Console.ReadKey(true);
            click.Play();

        }

        // PrintPlot takes the plot element from the current page and breaks it into lines of around 118 characters, then displays the line:
        static void PrintPlot(string plot)
        {
            Console.ForegroundColor = ConsoleColor.Green;

            if (plot.Length < 118)
            {
                Console.WriteLine(" " + plot);
            }

            if (plot.Length > 118)
            {                
                string newplot = " ";
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
                                            
                    if (lengthcheck.Length > 118)
                    {
                        Console.WriteLine(newplot);
                        newplot = " ";
                        lengthcheck = " ";
                    }
                }

                if (lengthcheck != null)
                {
                    Console.WriteLine(newplot);
                }                
            }

            Console.ResetColor();
        }

        // PrintFrame just draws the frame element
        static void PrintFrame()
        {
            Console.WriteLine();
            Console.WriteLine("╔╗═════════════════════════════════════════════════════════════════════════════════════════════════════════════════╗");
            Console.WriteLine("╚╝═════════════════════════════════════════════════════════════════════════════════════════════════════════════════╝");
            Console.WriteLine();

        }

        // ErrorCheck makes sure the destination page numbers in the story are valid. If not, displays an error message to prevent crashing.

        static int ErrorCheck(string pageNum)
        {
            int nextPage;
            bool isNumber = int.TryParse(pageNum, out nextPage);

            if (isNumber == true)
            {
                return nextPage;
            }

            else if (isNumber == false)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(" Error: the destination page number is invalid or improperly formatted. Please contact the story author.");
                Console.ResetColor();

                Console.ReadKey(true);
                click.Play();

                return 0;
            }

            return 0;
        }
    }
}

