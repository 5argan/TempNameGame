using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework.Content;

namespace TempNameGame.AvatarComponents
{
    public class AvatarManager
    {
        public static Dictionary<string, Avatar> AvatarList { get; }

        public static void AddAvatar(string name, Avatar avatar)
        {
            if (!AvatarList.ContainsKey(name))
                AvatarList.Add(name, avatar);
        }

        public static Avatar GetAvatar(string name) =>
            AvatarList.ContainsKey(name) ? (Avatar) AvatarList[name].Clone() : null;

        public static void FromFile(string fileName, ContentManager content)
        {
            using (var stream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                try
                {
                    using (var reader = new StreamReader(stream))
                    {
                        try
                        {
                            string lineIn;

                            do
                            {
                                lineIn = reader.ReadLine();
                                if (lineIn == null) continue;

                                var avatar = Avatar.FromString(lineIn, content);
                                if (!AvatarList.ContainsKey(avatar.Name))
                                {
                                    AvatarList.Add(avatar.Name, avatar);
                                }
                            } while (lineIn != null);
                        }
                        catch (Exception)
                        {
                            // ignored
                        }
                        finally
                        {
                            reader.Close();
                        }
                    }
                }
                catch (Exception)
                {
                    // ignored
                }
                finally
                {
                    stream.Close();
                }
            }
        }
    }
}