using Tutorial4.Showcase.Liskov.Sticked.Interfaces;

namespace Tutorial4.Showcase.Liskov.Sticked;

public class NewNetworkFileWorker : IFileWriter, IFileReader
{
    public void WriteFile(string filepath)
    {
        Console.WriteLine($"I've read {filepath} file from network");
    }

    public void ReadFile(string filepath)
    {
        Console.WriteLine($"I've written content to {filepath} file in network");
    }
}