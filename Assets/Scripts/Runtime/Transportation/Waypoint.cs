using Game.Runtime.Actor.Hero;
using Game.Runtime.Audio;
using Game.Runtime.Saved;
using Game.Runtime.UI.GUI.Window.Transportation;
using System.Text;
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

        private StringBuilder _stringBuilderForLabel;
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
                changeBechaviourForInteraction(_selectionMenu);
            }

            else
            {
                awaitingForActivate();
                OnActivate += ActivateDefaultBechaviourForInteraction;
            }

            OnInteractionWithWaypointArg += _selectionMenu.ShowLocations;
            OnInteractionWithWaypointArg += _selectionMenu.UpdateLocationName;

            OnTeleport += _selectionMenu.Close;

            void ActivateDefaultBechaviourForInteraction()
            {
                _selectionMenu.AddDiscovered(this);
                changeBechaviourForInteraction(_selectionMenu);
                OnActivate -= ActivateDefaultBechaviourForInteraction;
            }

            _label.InitializeOwner(transform);
            _label.Initialization();

            _stringBuilderForLabel = new StringBuilder();
            _stringBuilderForLabel.Append(name).AppendLine().Append("Waypoint");
            _label.SetText(_stringBuilderForLabel.ToString());
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
                _savedData.Set(ID);
            }

            return _savedData;
        }

        internal void LoadSaved()
        {
            enablePrewarm();
            Activate();
        }

        private void enablePrewarm()
        {
            for (int i = activateEffect.Length - 1; i >= 0; i--)
            {
                _effectMainModule = activateEffect[i].main;
                _effectMainModule.prewarm = true;
            }
        }

        private void awaitingForActivate()
        {
            OnInteract += Activate;
        }

        private void changeBechaviourForInteraction(SelectionMenu waypointMenu)
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