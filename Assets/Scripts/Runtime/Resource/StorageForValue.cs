using System.Text;
using Game.Runtime.Utility;
using UnityEngine;

namespace Game.Runtime
{
    public class StorageForValue
    {
        [SerializeField] protected float total = 100f;
        [SerializeField] protected float current;

        public float Total => total;
        public float Current => current;

        public virtual void Initialize()
        {

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
            int _current = (int)current;
            string tmp = _current.ToString();

            return StringUtility.BuildStringWithAppendLineAtTheEnd(tmp, " / ", total.ToString());
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