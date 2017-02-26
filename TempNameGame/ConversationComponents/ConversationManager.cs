using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TempNameGame.ConversationComponents
{
    public class ConversationManager
    {
        public static Dictionary<string, Conversation> ConversationList { get; private set; } =
            new Dictionary<string, Conversation>();

        public ConversationManager()
        {
        }

        public static void AddConversation(string name, Conversation conversation)
        {
            if (!ConversationList.ContainsKey(name))
                ConversationList.Add(name, conversation);
        }

        public static Conversation GetConversation(string name) =>
            ConversationList.ContainsKey(name) ? ConversationList[name] : null;

        public static bool ContainsConversation(string name) =>
            ConversationList.ContainsKey(name);


        public static void ToFile(string fileName)
        {
            var xmlDoc = new XmlDocument();
            var root = xmlDoc.CreateElement("Conversations");
            xmlDoc.AppendChild(root);

            foreach (var key in ConversationList.Keys)
            {
                var c = GetConversation(key);
                var conversation = xmlDoc.CreateElement("Conversation");
                var name = xmlDoc.CreateAttribute("Name");
                name.Value = key;
                conversation.Attributes.Append(name);

                var firstScene = xmlDoc.CreateAttribute("FirstScene");
                firstScene.Value = c.FirstScene;
                conversation.Attributes.Append(firstScene);

                var backgroundName = xmlDoc.CreateAttribute("BackgroundName");
                backgroundName.Value = c.BackgroundName;
                conversation.Attributes.Append(backgroundName);

                var fontName = xmlDoc.CreateAttribute("FontName");
                fontName.Value = c.FontName;
                conversation.Attributes.Append(fontName);

                foreach (var sc in c.Scenes.Keys)
                {
                    var g = c.Scenes[sc];
                    var scene = xmlDoc.CreateElement("GameScene");

                    var sceneName = xmlDoc.CreateAttribute("Name");
                    sceneName.Value = sc;
                    scene.Attributes.Append(sceneName);

                    var text = xmlDoc.CreateElement("Text");
                    text.InnerText = c.Scenes[sc].Text;

                    foreach (var option in g.Options)
                    {
                        var sceneOption = xmlDoc.CreateElement("GameSceneOption");

                        var oText = xmlDoc.CreateAttribute("Text");
                        oText.Value = option.OptionText;
                        sceneOption.Attributes.Append(oText);

                        var oOption = xmlDoc.CreateAttribute("Option");
                        oOption.Value = option.OptionScene;
                        sceneOption.Attributes.Append(oOption);

                        var oAction = xmlDoc.CreateAttribute("Action");
                        oAction.Value = option.OptionAction.ToString();
                        sceneOption.Attributes.Append(oAction);

                        var oParam = xmlDoc.CreateAttribute("Parameter");
                        oParam.Value = option.OptionAction.Parameter;
                        sceneOption.Attributes.Append(oParam);

                        scene.AppendChild(sceneOption);
                    }

                    conversation.AppendChild(scene);
                }
                root.AppendChild(conversation);
            }

            var settings = new XmlWriterSettings
            {
                Indent = true,
                Encoding = Encoding.UTF8
            };
            var stream = new FileStream(fileName, FileMode.Create, FileAccess.Write);
            var writer = XmlWriter.Create(stream, settings);
            xmlDoc.Save(writer);
        }


        public static void FromFile(string fileName, Game game, bool editor = false)
        {
            var xmlDoc = new XmlDocument();

            try
            {
                xmlDoc.Load(fileName);
                var root = xmlDoc.FirstChild;
                if (root.Name == "xml")
                    root = root.NextSibling;

                if (root == null || root.Name != "Conversations")
                    throw new Exception("Invalid conversation file!");

                foreach (XmlNode node in root.ChildNodes)
                {
                    if (node.Name == "#comment") continue;
                    if (node.Name != "Conversation") throw new Exception("Invalid conversation file!");

                    var conversationName = node.Attributes["Name"].Value;
                    var firstScene = node.Attributes["FirstScene"].Value;
                    var backgroundName = node.Attributes["BackgroundName"].Value;
                    var fontName = node.Attributes["FontName"].Value;

                    var background = game.Content.Load<Texture2D>(@"Backgrounds\" + backgroundName);
                    var font = game.Content.Load<SpriteFont>(@"Fonts\" + fontName);
                    var conversation = new Conversation(conversationName, firstScene, background, font)
                    {
                        BackgroundName = backgroundName,
                        FontName = fontName
                    };

                    foreach (XmlNode sceneNode in node.ChildNodes)
                    {
                        var text = "";
                        var sceneName = "";

                        if (sceneNode.Name != "GameScene")
                            throw new Exception("Invalid conversation file!");

                        sceneName = sceneNode.Attributes["Name"].Value;
                        var sceneOptions = new List<SceneOption>();

                        foreach (XmlNode innerNode in sceneNode.ChildNodes)
                        {
                            if (innerNode.Name == "Text") text = innerNode.InnerText;
                            if (innerNode.Name != "GameSceneOption") continue;

                            var optionText = innerNode.Attributes["Text"].Value;
                            var optionScene = innerNode.Attributes["Option"].Value;
                            var optionAction = innerNode.Attributes["Action"].Value;
                            var optionParam = innerNode.Attributes["Parameter"].Value;

                            var action = new SceneAction
                            {
                                Parameter = optionParam,
                                Action = (ActionType) Enum.Parse(typeof(ActionType), optionAction)
                            };

                            var option = new SceneOption(optionText, optionScene, action);
                            sceneOptions.Add(option);
                        }

                        var scene = editor ? new GameScene(text, sceneOptions) : new GameScene(game, text, sceneOptions);
                        conversation.AddScene(sceneName, scene);
                    }
                    ConversationList.Add(conversationName, conversation);
                }
            }
            catch (Exception)
            {
                // ignored
            }
            finally
            {
                xmlDoc = null;
            }
        }

        public static void ClearConversations()
        {
            ConversationList = new Dictionary<string, Conversation>();
        }

        public static void CreateConversations(Game game)
        {
            var sceneTexture = game.Content.Load<Texture2D>(@"Scenes\scenebackground");
            var sceneFont = game.Content.Load<SpriteFont>(@"Fonts\InterfaceFont");

            var c = new Conversation("MarissaHello", "Hello", sceneTexture, sceneFont)
            {
                BackgroundName = "scenebackground",
                FontName = "scenefont"
            };

            var options = new List<SceneOption>();
            var option = new SceneOption("Good bye.", "",
                new SceneAction {Action = ActionType.End, Parameter = "none"});
            options.Add(option);

            var scene = new GameScene(game, "Hello, my name is Marissa. I'm still learning about summoning avatars.",
                options);
            c.AddScene("Hello", scene);
            ConversationList.Add("MarissaHello", c);

            c = new Conversation("LanceHello", "Hello", sceneTexture, sceneFont)
            {
                BackgroundName = "scenebackground",
                FontName = "scenefont"
            };

            options = new List<SceneOption>();
            option = new SceneOption("Yes", "ILikeFire",
                new SceneAction {Action = ActionType.Talk, Parameter = "non2"});
            options.Add(option);

            option = new SceneOption("No", "IDislikeFire",
                new SceneAction {Action = ActionType.Talk, Parameter = "non2"});
            options.Add(option);

            scene = new GameScene(game, "Fire avatars are my favorites. Do you like fire type avatars too?", options);
            c.AddScene("Hello", scene);
            options = new List<SceneOption>();
            option = new SceneOption("Good bye.", "", new SceneAction() {Action = ActionType.End, Parameter = "none"});
            options.Add(option);

            scene = new GameScene(game, "That's cool. I wouldn't want to hug one though.", options);
            c.AddScene("ILikeFire", scene);
            options = new List<SceneOption>();
            option = new SceneOption("Good bye.", "", new SceneAction() {Action = ActionType.End, Parameter = "none"});
            options.Add(option);

            scene = new GameScene(game, "Each to their own I guess.", options);
            c.AddScene("IDislikeFire", scene);
            ConversationList.Add("LanceHello", c);
        }
    }
}