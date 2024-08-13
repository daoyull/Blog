namespace Blog.Lib.Entity;

public class Archive
{
    public string? Time { get; set; }

    public string? Color { get; set; }

    public List<ArchiveBlogItem> Blogs { get; set; } = new();
}

public class ArchiveBlogItem
{
    public string? Day { get; set; }
    public string? Title { get; set; }
    public long Id { get; set; }
}