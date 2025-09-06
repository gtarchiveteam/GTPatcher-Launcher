# We now live on Codeberg. https://codeberg.org/GT-Archive-Team/GTPatcher-Launcher


<div align="center">
    <a href="https://github.com/gtarchiveteam/GTPatcher-Launcher/blob/main/LICENSE">
    <img src="https://img.shields.io/github/license/gtarchiveteam/GTPatcher-Launcher?style=flat"></a>
    <a href="https://github.com/gtarchiveteam/GTPatcher-Launcher/releases/latest">
    <img src="https://img.shields.io/github/downloads/gtarchiveteam/GTPatcher-Launcher/total?style=flat"></a>
    <a href="https://discord.gg/X2KX2Yc2eR">
    <img src="https://img.shields.io/discord/1193649345434751077?label=Discord&style=flat"></a>
</div>
<div aling="center">
  <img src="https://raw.githubusercontent.com/gtarchiveteam/Assets/main/Banners/gtpl-banner.png">
</div>

# GT Patcher Launcher
A launcher to download and patch particular versions of Gorilla Tag via a Steam account.

Made using [XdeltaSharp](https://github.com/pleonex/xdelta-sharp), [DepotDownloader](https://github.com/SteamRE/DepotDownloader) and [Avalonia](https://avaloniaui.net/).
## Features

- Multiple different versions avaliable to patch.
    <details>
    <summary>Currently avaliable patches</summary>
        <ul>
            <li>Holiday Overstock</li>
            <li>Neon Colors</li>
            <li>Mountains Beta</li>
            <li>Halloween 2022</li>
            <li>Monke Blocks</li>
            <li>Steam Release</li>
        </ul>
    </details>
- Proper cloudscripts (cosmetic verification, whitelist, etc).
- You can actually use Smooth Turn and Private Lobbies! (big shocker)

## FAQ

#### Why do you need my Steam password?
We use your Steam account to download the game files, as we only distribute our patches. 

We use DepotDownloader for this, so if you're uncomfortable with providing your Steam account details, you can audit DepotDownloader and/or the GTP Launcher's source code or manually download and patch the versions yourself.

#### I don't want to open the Launcher every time I want to play a GTP build. How do I add them to my library?
You can add the .exe files for the version(s) as non-Steam games in your Steam client, and include them in your library via the game properties.

#### I don't see my question/problem here

More generic questions are at the organization's [profile](https://github.com/gtarchiveteam). If neither that nor this FAQ answer your question, feel free to ask in our Discord server.

## Linux FAQ

#### I don't see a prompt to enter my Steam password!
Because of the way process spawning works on Linux, you will have to start the Launcher from a terminal to see the prompt.
