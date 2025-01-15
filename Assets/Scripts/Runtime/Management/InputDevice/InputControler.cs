using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Runtime.Management.InputDevice
{
    public class InputControler: MonoBehaviour
    {
        [SerializeField] private Mouse.MouseControler mouse;
        [SerializeField] private Keyboard keyboard;

        private PlayerInput _playerInput;

        internal Mouse.MouseControler Mouse => mouse;
        internal Keyboard Keyboard => keyboard;

        internal void Construct(GameCreator gameMainManager)
        {
            mouse.Construct(gameMainManager);
            keyboard.Construct(gameMainManager);
            TryGetComponent(out _playerInput);
        }

        #region hero
        public void OnSkill1()
        {
            keyboard.OnClickSkill1?.Invoke();
        }

        public void OnSkill2()
        {
            keyboard.OnClickSkill2?.Invoke();
        }

        public void OnSkill3()
        {
            keyboard.OnClickSkill3?.Invoke();
        }

        public void OnClickLeftMouseOnlyForSkill()
        {
            mouse.OnClickLeftMouseOnlyForSkill();
        }

        public void OnClickRightMouseOnlyForSkill()
        {
            mouse.OnClickRightMouseOnlyForSkill();
        }

        #endregion

        public void EnableMouseEvents()
        {
            mouse.InitializeLeftButton();
        }

        public void DisableMouseEvents()
        {
            mouse.ButtonLeftDetachActions();
        }

        internal void DisableInput()
        {
            _playerInput.enabled = false;
        }

        internal void EnableInput()
        {
            _playerInput.enabled = true;
        }

        internal void ClearActionForCurrentTargetFromMouseExitActionforSkill1()
        {
            keyboard.OnClickSkill1 += mouse.DetachClearActionForCurrentTargetFromOnMouseExitAction;//dev note: wyodrebnij zachowanie z myszy i wrzuc do innj klasy
            mouse.ClearActionForCurrentTargetFor1();
        }

        internal void ClearActionForCurrentTargetFromMouseExitActionforSkill2()
        {
            keyboard.OnClickSkill2 += mouse.DetachClearActionForCurrentTargetFromOnMouseExitAction;
            mouse.ClearActionForCurrentTargetFor2();
        }

        internal void ClearActionForCurrentTargetFromMouseExitActionforSkill3()
        {
            keyboard.OnClickSkill3 += mouse.DetachClearActionForCurrentTargetFromOnMouseExitAction;
            mouse.ClearActionForCurrentTargetFor3();
        }
    }
}