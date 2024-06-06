using System.Collections.Generic;

public sealed class Hunt
{
    public int Id { get; set; }
    public string Title { get; set; } = default!;
    public string Description { get; set; } = string.Empty;
    //public ICollection<Assignment> Assignments { get; set; }
}

public sealed class Assignment
{
    public int Id { get; set; }
    public Hint Hint { get; set; }
    public Solution Solution { get; set; }
}

public sealed class Solution
{
    public int Id { get; set; }
    public int Type { get; set; }
    public string Data { get; set; } = string.Empty;
}

public sealed class Hint
{
    public int Id { get; set; }
    public int HintType { get; set; }
    public string Data { get; set; } = string.Empty;
}
