using UnityEngine;

namespace Game.Management
{
    [System.Serializable]
    public struct InputDevicesWrapper
    {
        [SerializeField] private Mouse.Mouse mouse;
        [SerializeField] private Keyboard keyboard;

        internal Mouse.Mouse Mouse => mouse;
        internal Keyboard Keyboard => keyboard;

        internal void Construct(GameMainManager gameMainManager)
        {
            mouse.Construct(gameMainManager); 
            keyboard.Construct(gameMainManager);
        }
    }
}