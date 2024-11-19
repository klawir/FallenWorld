using UnityEngine;

namespace Game.Runtime.Management.InputDevice
{
    [System.Serializable]
    public struct InputDevicesCollector
    {
        [SerializeField] private Mouse.MouseControler _mouse;
        [SerializeField] private Keyboard _keyboard;

        internal Mouse.MouseControler Mouse => _mouse;
        internal Keyboard Keyboard => _keyboard;

        internal void Construct(GameMainManager gameMainManager)
        {
            _mouse.Construct(gameMainManager);
            _keyboard.Construct(gameMainManager);
        }
    }
}