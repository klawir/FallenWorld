using System;

namespace Game.Runtime.Actor.Hero
{
    [System.Serializable]
    public class Experience : StorageForValue
    {
        [UnityEngine.SerializeField] private Game.Runtime.GlobalSettings.ScriptableObjectDefinition.Character _characterGlobalSettings;
        private float _multipierPerLevel;
        private int _bank;

        public Action OnLeveLUp;
        internal int GetLeveL { get; private set; }
        public bool IsEmpty { get { return current <= 0; } }

        public override void Initialize()
        {
            base.Initialize();
            InitializeDefaultLevel();
            _multipierPerLevel = _characterGlobalSettings.MultipierPerLevel;
        }

        public override void AddToCurrent(float value)
        {
            base.AddToCurrent(value);

            if (IsFull)
            {
                NewLevel();
                CalculateBank();
                ResetCurrentValue();
                CalculateNewTotalValue();
                AddTheRestBank();
                OnLeveLUp?.Invoke();
            }
        }

        private void AddTheRestBank()
        {
            AddToCurrent(_bank);
        }

        private void CalculateNewTotalValue()
        {
            total += total * _multipierPerLevel;
        }

        private void ResetCurrentValue()
        {
            current = 0f;
        }

        private void CalculateBank()
        {
            _bank = (int)(current - total);
        }

        private void NewLevel()
        {
            GetLeveL++;
        }

        public void LoadSavedLevel(int level)
        {
            GetLeveL = level;
        }

        private void InitializeDefaultLevel()
        {
            GetLeveL = 1;
        }
    }
}