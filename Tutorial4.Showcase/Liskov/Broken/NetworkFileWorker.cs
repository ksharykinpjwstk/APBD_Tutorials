namespace Tutorial4.Showcase.Liskov.Broken;

public class NetworkFileWorker(string pathToFile) : FileWorker(pathToFile)
{
    public override void ReadFile()
    {
        Console.WriteLine($"I've read {Path} file from network");
    }

    public override void WriteFile()
    {
        Console.WriteLine($"I've written content to {Path} file in network");
    }
}