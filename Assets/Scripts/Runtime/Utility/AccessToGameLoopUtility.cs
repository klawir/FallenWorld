
namespace Game.Runtime.Utility
{
    public class AccessToGameLoopUtility
    {
        internal System.Action OnUpdate;

        internal void Update()
        {
            OnUpdate?.Invoke();
        }
    }
}