using System;
using UnityEngine;

namespace Game.Runtime.Resource.ResourceControler
{
    [Serializable]
    public class ExperienceControler : AbsRenewableResourceControler
    {
        [SerializeField] private Actor.Hero.Experience experience;

        public override bool IsEmpty { get { return experience.IsEmpty; } }
        public override bool IsNotFull { get { return experience.IsNotFull; } }
        public override bool IsFull { get { return experience.IsFull; } }

        public float Current { get { return experience.Current; } }
        public float Total { get { return experience.Total; } }
        public int GetLeveL { get { return experience.GetLeveL; } }

        public override void Initialize()
        {
            experience.Initialize();
            bar.UpdateFillAmount(experience);
            bar.UpdateText(experience);
        }

        public void AddToCurrent(float value)
        {
            experience.AddToCurrent(value);
            bar.UpdateFillAmount(experience);
            bar.UpdateText(experience);
        }

        public void AddToCurrent(Quest.Quest quest)
        {
            float value = quest.GetReward.GetExperience;
            AddToCurrent(value);
        }

        public void LoadSavedLevel(int level)
        {
            experience.LoadSavedLevel(level);
        }

        internal void LoadSaved(float experienceCurrent, float experienceTotal)
        {
            experience.LoadSaved(experienceCurrent, experienceTotal);
            bar.UpdateFillAmount(experience);
            bar.UpdateText(experience);
        }

        internal void SubscribeOnLeveLUp(Action newAction)
        {
            experience.OnLeveLUp += newAction;
        }
    }
}