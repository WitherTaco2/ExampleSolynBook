using Terraria.ModLoader;

namespace ExampleSolynBook
{
    // Please read https://github.com/tModLoader/tModLoader/wiki/Basic-tModLoader-Modding-Guide#mod-skeleton-contents for more information about the various files in a mod.
    public class ExampleSolynBook : Mod
    {
        //Mod type of Wrath of the Gods itself
        public static Mod mod;
        public override void Load()
        {
            mod = this;
        }
        public override void Unload()
        {
            mod = null;
        }
    }
}
