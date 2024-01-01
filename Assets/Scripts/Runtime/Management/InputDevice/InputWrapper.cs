using UnityEngine;

namespace Game.Runtime.Management.InputDevice
{
    [System.Serializable]
    public struct InputWrapper
    {
        [SerializeField] private Mouse.MouseControler mouse;
        [SerializeField] private Keyboard keyboard;

        internal Mouse.MouseControler Mouse => mouse;
        internal Keyboard Keyboard => keyboard;

        internal void Construct(GameMainManager gameMainManager)
        {
            mouse.Construct(gameMainManager);
            keyboard.Construct(gameMainManager);
        }
    }
}