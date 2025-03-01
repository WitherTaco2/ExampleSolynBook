using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace ExampleSolynBook
{
    public class ExamplePlayer : ModPlayer
    {
        //New players starts with Example Solyn Book
        public override IEnumerable<Item> AddStartingItems(bool mediumCoreDeath)
        {
            static Item createItem(int type)
            {
                Item i = new Item();
                i.SetDefaults(type);
                return i;
            }

            if (!mediumCoreDeath)
                //Uses book's texture file name as internal name of item
                yield return createItem(ExampleSolynBook.mod.Find<ModItem>("ExampleSolynBook").Type);
        }
    }
}
