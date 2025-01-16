using var input = new StreamReader(Console.OpenStandardInput());
using var output = new StreamWriter(Console.OpenStandardOutput());

var n = Convert.ToInt16(input.ReadLine());
var options = new System.Text.Json.JsonSerializerOptions
{
    PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase,
    MaxDepth = 1024
};

for (short j = 0; j < n; j++)
{
    var len = ushort.Parse(input.ReadLine());
    var strBuilder = new System.Text.StringBuilder();

    for (short i = 0; i < len; i++)
    {
        var line = input.ReadLine();
        strBuilder.Append(line);
    }

    var directory = System.Text.Json.JsonSerializer.Deserialize<Folder>(strBuilder.ToString(),options);
    var count = Count(directory);
    output.WriteLine(count);
}

int CountAllFiles(Folder root)
{
    var count = 0;
    if (root.Files!=null)
    {
        count = root.Files.Length;
    }
    if (root.Folders == null) return count;

    int result = count;
    foreach (var folder in root.Folders) result = result + CountAllFiles(folder);
    return result;
}

int Count(Folder root)
{
    var count = 0;
    var hasVirusFiles = root.Files?.Any(file => file.EndsWith(".hack"));

    if (hasVirusFiles.HasValue && hasVirusFiles.Value)
    {
        count += CountAllFiles(root);
        return count;
    }

    if (root.Folders == null) return count;
    count += root.Folders.Sum(Count);
    return count;
}

public struct Folder
{
    public string Dir { get; set; }
    public string[] Files { get; set; }
    public Folder[] Folders { get; set; }
}