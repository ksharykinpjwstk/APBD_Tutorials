namespace Tutorial4.Showcase.Liskov.Broken;

public abstract class FileWorker(string pathToFile)
{
    protected readonly string Path = pathToFile;

    public virtual void ReadFile()
    {
        Console.WriteLine($"I've read {Path} file");
    }
    
    public virtual void WriteFile()
    {
        Console.WriteLine($"I've written content to {Path} file");
    }
}