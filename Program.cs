using System;


class Program
{
    static void Main(string[] args)
    {

        bool running = true;
        while (running)
        {

            UserInterface.DisplayMainMenu();
            string input = UserInterface.GetUserInput();

            switch (input)
            {
                case "1":
                    BreathingActivity breathingActivity = new BreathingActivity();
                    breathingActivity.Start();
                    break;
                case "2":
                    ReflectionActivity reflectionActivity = new ReflectionActivity();
                    reflectionActivity.Start();
                    break;
                case "3":
                    ListingActivity listingActivity = new ListingActivity();
                    listingActivity.Start();
                    break;
                case "4":
                    running = false;
                    break;
                default:
                    UserInterface.DisplayErrorMessage("Invalid input, please try again.");
                    break;
            }
        }
    }
}


class UserInterface
{
    public static void DisplayMainMenu()
    {
        Console.WriteLine("\nPlease choose an activity:");
        Console.WriteLine("===========================");
        Console.WriteLine("1. Breathing");
        Console.WriteLine("2. Reflection");
        Console.WriteLine("3. Listing");
        Console.WriteLine("4. Exit\n");
    }

    public static void DisplayErrorMessage(string errorMessage)
    {
        Console.WriteLine(errorMessage);
    }

    public static string GetUserInput()
    {
        Console.Write("> ");
        return Console.ReadLine();
    }
}

abstract class Activity
{
    protected int duration;

    public virtual void Start()
    {
        Console.Write(GetStartingMessage());
        Thread.Sleep(3000);
        duration = Convert.ToInt32(Console.ReadLine());
        DoActivity();
        Console.WriteLine(GetEndingMessage());
        Thread.Sleep(3000);
    }

    protected abstract string GetStartingMessage();

    protected abstract string GetEndingMessage();

    protected abstract void DoActivity();
}

class BreathingActivity : Activity
{
    private string inhaleMessage = "Breathe in...";
    private string exhaleMessage = "Breathe out...";
    private int countdown = 5;

    protected override string GetStartingMessage()
    {
        return "Breathing Activity:\nThis activity will help you relax by walking your through breathing in and out slowly.\nClear your mind and focus on your breathing.\nHow long would you like to do this activity for (in seconds)? ";
    }

    protected override string GetEndingMessage()
    {
        return "\nGood job! You have completed the Breathing Activity for " + duration + " seconds.";
    }

    protected override void DoActivity()
    {
        for (int i = 0; i < duration; i++)
        {
            if (i % 2 == 0)
            {
                Console.WriteLine(inhaleMessage);
            }
            else
            {
                Console.WriteLine(exhaleMessage);
            }
            Thread.Sleep(countdown * 1000);
        }
    }
}


class ReflectionActivity : Activity
{
    private static Random random = new Random();

    protected override string GetStartingMessage()
    {
        return $"Reflection Activity:\nThis activity will help you reflect on times in your life when you have shown strength and resilience.\nThis will help you recognize the power you have and how you can use it in other aspects of your life.\nHow long would you like to do this activity for (in seconds)? ";
    }

    protected override string GetEndingMessage()
    {
        return $"\nGreat job! You completed the Reflection Activity for {duration} seconds.";
    }

    protected override void DoActivity()
    {
        List<string> prompts = new List<string>
        {
            "Think of a time when you stood up for someone else.",
            "Think of a time when you did something really difficult.",
            "Think of a time when you helped someone in need.",
            "Think of a time when you did something truly selfless."
        };

        List<string> questions = new List<string>
        {
            "Why was this experience meaningful to you?",
            "Have you ever done anything like this before?",
            "How did you get started?",
            "How did you feel when it was complete?",
            "What made this time different than other times when you were not as successful?",
            "What is your favorite thing about this experience?",
            "What could you learn from this experience that applies to other situations?",
            "What did you learn about yourself through this experience?",
            "How can you keep this experience in mind in the future?"
        };

        int timeElapsed = 0;

        string prompt = prompts[random.Next(prompts.Count)];
        Console.WriteLine(prompt);

        Thread.Sleep(6000);
        timeElapsed += 6;

        while (timeElapsed < duration)
        {
            if (timeElapsed % 6 == 0)
            {
                Console.WriteLine(questions[random.Next(questions.Count)]);
            }

            Thread.Sleep(1000);
            timeElapsed += 1;
        }
    }
}

class ListingActivity : Activity
{
    private static Random random = new Random();

    protected override string GetStartingMessage()
    {
        return $"Listing Activity:\nThis activity will help you reflect on the good things in your life\nby having you list as many things as you can.\nHow long would you like to do this activity for (in seconds)? ";
    }

    protected override string GetEndingMessage()
    {
        return $"\nGreat job! You listed {itemCount} items.";
    }

    protected override void DoActivity()
    {
        List<string> prompts = new List<string>
        {
            "Who are people that you appreciate?",
            "What are personal strengths of yours?",
            "Who are people that you have helped this week?",
            "When have you felt the Holy Ghost this month?",
            "Who are some of your personal heroes?"
        };

        string prompt = prompts[random.Next(prompts.Count)];

        Console.WriteLine($"Your prompt is:");
        Console.WriteLine(prompt);
        Console.WriteLine($"You have {duration} seconds to list as many items as you can.");

        itemCount = 0;
        var startTime = DateTime.Now;
        while (DateTime.Now.Subtract(startTime).TotalSeconds < duration)
        {
            string item = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(item))
            {
                break;
            }
            itemCount++;
        }
    }

    private int itemCount;
}