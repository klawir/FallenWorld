using System.Text;
using UnityEngine;

namespace Game.Runtime
{
    public class StorageForValue
    {
        [SerializeField] protected float total = 100f;
        [SerializeField] protected float current;

        protected StringBuilder _stringBuilder;

        public float Total => total;
        public float Current => current;

        public virtual void Initialize()
        {
            _stringBuilder = new StringBuilder();
        }

        public virtual void AddToCurrent(float value)
        {
            current += value;
        }

        public virtual void RemoveFromCurrent(float calculatedValue)
        {
            current -= calculatedValue;
        }

        public virtual void UpdateTotal(float calculatedValue)
        {
            total = calculatedValue;
        }

        public virtual void LoadSaved(float value, float ofTotal)
        {
            current = value;
            total = ofTotal;
        }

        public string TextAmount()
        {
            _stringBuilder.Clear();
            return _stringBuilder.Append((int)current).Append(" / ").Append(total).ToString();
        }

        public float FillAmount
        {
            get { return current / total; }
        }

        public bool IsFull
        {
            get { return current >= total; }
        }

        public bool IsNotFull
        {
            get { return current < total; }
        }
    }
}