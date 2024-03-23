namespace Tutorial4.Showcase.Liskov.Broken;

// For example we work in restricted mode and can't write to files
public class RestrictedFileWorker(string pathToFile) : FileWorker(pathToFile)
{
    public override void ReadFile()
    {
        Console.WriteLine($"I've read protected {Path} file");
    }

    public override void WriteFile()
    {
        throw new Exception("Can't perform write operations");
    }
}