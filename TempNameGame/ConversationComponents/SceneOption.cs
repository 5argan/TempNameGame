namespace TempNameGame.ConversationComponents
{
    public enum ActionType
    {
        Talk, End, Change, Quest, Buy, Sell, GiveItems, GiveKey
    }

    public class SceneAction
    {
        public ActionType Action;
        public string Parameter;
    }
    public class SceneOption
    {
        public string OptionText { get; set; }
        public string OptionScene { get; set; }
        public SceneAction OptionAction { get; set; }

        public SceneOption(string text, string scene, SceneAction action)
        {
            OptionText = text;
            OptionScene = scene;
            OptionAction = action;
        }
    }
}