using Tutorial4.Showcase.Liskov.Sticked.Interfaces;

namespace Tutorial4.Showcase.Liskov.Sticked;

// You just implement what you need
public class NewRestrictedFileWorker : IFileReader
{
    public void ReadFile(string filepath)
    {
        Console.WriteLine($"I've read protected {filepath} file");
    }
}