using Game.Runtime.Actor.Hero;
using Game.Runtime.Audio;
using Game.Runtime.Saved;
using Game.Runtime.UI.GUI.Window.Transportation;
using Game.Runtime.Utility;
using UnityEngine;

namespace Game.Runtime.Transportation
{
    [System.Serializable]
    public class Waypoint : AbsPortal
    {
        [SerializeField] private bool activated;
        [SerializeField] private ParticleSystem[] activateEffect;
        [SerializeField] private AudioSetControler activateSfx;
        [SerializeField] private AudioSetControler teleportSfx;
        [SerializeField] private UI.HUD.Label.NonCombatActor.Label _label;

        internal System.Action OnActivate;
        internal System.Action OnTeleport;
        internal System.Action<Waypoint> OnInteractionWithWaypointArg;

        private string _stringForLabel;
        private ParticleSystem.MainModule _effectMainModule;
        private SavedDataOfWaypoint _savedData;
        internal bool Activated => activated;

        public override void Initialize()
        {
            base.Initialize();

            var _selectionMenu = _gameMainManager.SceneHierarchy.WaypointMenu;
            if (activated)
            {
                Activate();
                ChangeBechaviourForInteraction(_selectionMenu);
            }

            else
            {
                AwaitingForActivate();
                OnActivate += ActivateDefaultBechaviourForInteraction;
            }

            OnInteractionWithWaypointArg += _selectionMenu.ShowLocations;
            OnInteractionWithWaypointArg += _selectionMenu.UpdateLocationName;

            OnTeleport += _selectionMenu.Close;

            void ActivateDefaultBechaviourForInteraction()
            {
                _selectionMenu.AddDiscovered(this);
                ChangeBechaviourForInteraction(_selectionMenu);
                OnActivate -= ActivateDefaultBechaviourForInteraction;
            }

            _label.InitializeOwner(transform);
            _label.Initialization();

            _stringForLabel = StringUtility.BuildStringWithAppendLineAtTheEnd(name);
            _stringForLabel = StringUtility.BuildStringNoAppendLineAtTheEnd(_stringForLabel, "Waypoint");
            _label.SetText(_stringForLabel.ToString());
            _savedData = new SavedDataOfWaypoint();
            Debug.Log(name+ " Initialization()");
        }

        public override void Select()
        {
            _label.Enable();
            UpdateLabelPosition();
            SubscribeLabelToCameraFollow();
            SelectModel();
        }

        public override void Deselect()
        {
            UnSubscribeLabelFromCameraFollow();
            DeselectModel();
            _label.Disable();
        }

        internal override void UpdateLabelPosition()
        {
            _label.UpdatePosition(CalculateToCameraPerspective());
        }

        public override void Interaction()
        {
            base.Interaction();
            OnInteractionWithWaypointArg?.Invoke(this);
        }

        public override void Teleport(Character hero)
        {
            hero.Warp(this);
            OnTeleport?.Invoke();
            teleportSfx.PlayRandomly();
        }

        internal SavedDataOfWaypoint GetStateDataForSaving()
        {
            if (activated)
            {
                _savedData.ID = ID;
            }

            return _savedData;
        }

        internal void LoadSaved()
        {
            EnablePrewarm();
            Activate();
        }

        private void EnablePrewarm()
        {
            for (int i = activateEffect.Length - 1; i >= 0; i--)
            {
                _effectMainModule = activateEffect[i].main;
                _effectMainModule.prewarm = true;
            }
        }

        private void AwaitingForActivate()
        {
            OnInteract += Activate;
        }

        private void ChangeBechaviourForInteraction(SelectionMenu waypointMenu)
        {
            OnInteract += waypointMenu.Open;
        }

        private void Activate()
        {
            for (int i = activateEffect.Length - 1; i >= 0; i--)
            {
                activateEffect[i].Play();
            }

            activateSfx.PlayRandomly();
            activated = true;

            OnInteract -= Activate;
            OnActivate?.Invoke();
        }
    }
}