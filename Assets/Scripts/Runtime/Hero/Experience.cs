using System;

namespace Game.Runtime.Actor.Hero
{
    [System.Serializable]
    public class Experience : StorageForValue
    {
        private const float _MULTIPLIER_PER_LEVEL = 0.2f;
        private int _bank;

        public Action OnLeveLUp;
        internal int GetLeveL { get; private set; }
        public bool IsEmpty { get { return current <= 0; } }

        public override void Initialize()
        {
            base.Initialize();
            initializeDefaultLevel();
        }

        public override void AddToCurrent(float value)
        {
            base.AddToCurrent(value);

            if (IsFull)
            {
                newLevel();
                calculateBank();
                resetCurrentValue();
                calculateNewTotalValue();
                addTheRestBank();
                OnLeveLUp?.Invoke();
            }
        }

        private void addTheRestBank()
        {
            AddToCurrent(_bank);
        }

        private void calculateNewTotalValue()
        {
            total += total * _MULTIPLIER_PER_LEVEL;
        }

        private void resetCurrentValue()
        {
            current = 0f;
        }

        private void calculateBank()
        {
            _bank = (int)(current - total);
        }

        private void newLevel()
        {
            GetLeveL++;
        }

        public void LoadSavedLevel(int level)
        {
            GetLeveL = level;
        }

        private void initializeDefaultLevel()
        {
            GetLeveL = 1;
        }
    }
}