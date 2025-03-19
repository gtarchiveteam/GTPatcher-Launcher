namespace GTPatcher.Types;

/// <summary>
/// A class for a patch and its associated metadata and gameversion.
/// </summary>
/// <param name="PatchName">The full friendly name of the game version</param>
/// <param name="PatchShorthand">Name used when creating the folder to hold the game files. Should ALWAYS be a valid NTFS file name.</param>
/// <param name="PatchDescription">Any extra info relevant to the patch or game version.</param>
/// <param name="PatchLink">Direct URL to the xdelta3 patch file</param>
/// <param name="GameLink">Only used for builds never released to Steam. Currently unimplemented.</param>
/// <param name="GameName">Name of the exe file, game data, etc... futureproofing for old game versions</param>
/// <param name="ManifestId">Steam manifest ID. Only used for builds on Steam.</param>
/// <param name="IsSteam">Used to decide if it's a Steam build or not</param>
/// <param name="Branch">Steam branch to download from. Only used for builds on Steam.</param>
public class Patch
{
    public string PatchName { get; set; }
    public string PatchShorthand { get; set; }
    public string PatchDescription { get; set; }
    public string PatchLink { get; set; }
    public string GameLink { get; set; }
    public string GameName { get; set; }
    public long ManifestId { get; set; }
    public bool IsSteam { get; set; }
    public string Branch { get; set; }
}