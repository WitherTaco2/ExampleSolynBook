using System;
using System.Reflection;
using Terraria.ModLoader;
using Terraria.ModLoader.Core;

namespace ExampleSolynBook.Items
{
    //You can copy this code with changes in CreateSolynBook() in Load() :clueless:
    public class SolynBookRegistry : ModSystem
    {
        public static Mod WotG;
        public override void Load()
        {
            //If you don't know about Reflections. Its expert programming.
            //Making a Solyn Book without NoxusBoss.dll (Wrath of the Gods as dll library) for making weak reference instead of mod reference
            //Aka adding a solyn book only if has Wrath of the Gods in mod pack but by making it optional

            //Getting a WotG mod on mod loading
            ModLoader.TryGetMod("NoxusBoss", out WotG);

            //Checking a existance of Wrath of the Gods in mod pack
            if (ModLoader.HasMod("NoxusBoss"))
            {
                //This area cab be modifed for making your own methods
                //Creating a single book
                CreateSolynBook(3, "ExampleSolynBook/Items/ExampleSolynBook");
            }
        }
        /// <summary>
        /// Register and creates a custom solyn book
        /// </summary>
        /// <param name="rarity">Value from 1 to 3 (other values not tested)</param>
        /// <param name="TexturePath">Full texture path</param>
        public void CreateSolynBook(int rarity, string TexturePath)
        {
            //Getting a Wrath of the Gods as full programm (Assembly)
            Assembly wotg = WotG.Code;

            //Getting a all mod's methods from Assembly
            //Do not use a Assembly.GetType() for weak reference mods
            var types = AssemblyManager.GetLoadableTypes(wotg);

            //Getting a LoadableBookData struct type from WotG
            Type bookDataType = FindType(types, "NoxusBoss.Core.Autoloaders.SolynBooks.LoadableBookData");

            //Creating a variable with LoadableBookData type and needed values
            object bookData = Activator.CreateInstance(bookDataType);
            bookDataType.GetField("Rarity", BindingFlags.Public | BindingFlags.Instance).SetValue(bookData, rarity); //Sets 3 stars for rarity of book
            bookDataType.GetField("TexturePath", BindingFlags.Public | BindingFlags.Instance).SetValue(bookData, TexturePath); //Sets a texture path

            //Register a Solyn Book (creating a item itself and adding to wotg mod's array)
            object result = FindType(types, "NoxusBoss.Core.Autoloaders.SolynBooks.SolynBookAutoloader").GetMethod("Create", BindingFlags.Public | BindingFlags.Static).Invoke(null, new object[] { Mod, bookData });

        }
        /// <summary>
        /// WARNING: HE DONT WORK CURRENTLY
        /// Gets a Solyn Book item id
        /// </summary>
        /// <param name="name">Name of Solyn Book. Uses texture file name. Like "ExampleSolynBook" in this Example Mod</param>
        /// <returns>Item type</returns>
        /*public static int GetSolynBook(string name)
        {
            if (ModLoader.HasMod("NoxusBoss"))
            {
                //Getting a Wrath of the Gods as full programm (Assembly)
                Assembly wotg = WotG.Code;

                //Getting a all mod's methods from Assembly
                //Do not use a Assembly.GetType() for weak reference mods
                var types = AssemblyManager.GetLoadableTypes(wotg);

                //Getting a SolynBookAutoloader static class type from WotG
                Type solynBooks = FindType(types, "NoxusBoss.Core.Autoloaders.SolynBooks.SolynBookAutoloader");

                //Getting a Books Dictionary array;
                //Yea, SolynBookAutoloader has only one FieldInfo

                //var dictionaryObject = solynBooks.GetFields()[0].GetValue(null);
                //var dictionary = dictionaryObject as Dictionary<string, object>;
                //dictionary.TryGetValue(name, out var item);
                //return (item as ModItem).Type;
            }

            return -1;
        }*/
        //Used for more compact coding
        private static Type? FindType(Type[] array, string name)
        {
            foreach (var type in array)
            {
                if (type.FullName is not null)
                {
                    //Mod.Logger.Debug(type.FullName);
                    if (type.FullName == name)
                    {
                        return type;
                    }
                }
            }
            return null;
        }
        public override void Unload()
        {
            WotG = null;
        }
    }
}
