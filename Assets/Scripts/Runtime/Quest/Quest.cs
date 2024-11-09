
namespace Game.Runtime.Quest
{
    public class Quest
    {
        public int GetID { get; private set; }
        public string GetNameOf { get; private set; }
        public string GetDescribe { get; private set; }
        public SpriteState GetIcons { get; private set; }
        public QuestGoalType GetGoalType { get; private set; }
        public QuestStateType GetState { get; private set; }
        public Location GetLocation { get; private set; }
        public Reward GetReward { get; private set; }

        public Quest(QuestData questData)
        {
            GetID = questData.GetID;
            GetNameOf = questData.GetNameOf;
            GetDescribe = questData.GetDescribe;
            GetIcons = questData.GetImages;
            GetGoalType = questData.GetGoalType;
            GetState = QuestStateType.Deactivated;
            GetLocation = questData.GetLocation;
            GetReward = questData.GetReward;
        }

        internal void SetState(QuestStateType newState)
        {
            GetState = newState;
        }

        internal bool IsActivated
        {
            get { return GetState == QuestStateType.Activated; }
        }

        internal bool IsDeactivated
        {
            get { return GetState == QuestStateType.Deactivated; }
        }

        internal bool HasBeenComplete
        {
            get { return GetState == QuestStateType.Complete; }
        }
    }
}