public class BuildInfo
{
    public string? PatchName { get; set; }
    public string? PatchDescription { get; set; }
    public string? PatchLink { get; set; }
    public string? GameLink { get; set; } // only used on builds predating steam
    public ulong? ManifestId { get; set; }
    public bool? IsSteam { get; set; }

    public override string ToString()
    {
        return PatchName ?? "No Name";
    }
}