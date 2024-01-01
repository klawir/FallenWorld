using Game.Runtime.Actor.Hero;
using Game.Runtime.Audio;
using Game.Runtime.Saved;
using Game.Runtime.UI.Window.Transportation;
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

        internal System.Action OnActivate;
        internal System.Action OnTeleport;
        internal System.Action<Waypoint> OnInteractionWithWaypointArg;

        private StringBuilder _stringBuilderForLabel;
        internal bool Activated => activated;

        public override void Initialize()
        {
            base.Initialize();

            _stringBuilderForLabel = new StringBuilder();
            _stringBuilderForLabel.Append(name).AppendLine().Append("Waypoint");

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
        }

        public override void Select()
        {
            base.Select();

            _globalLabel.SetText(_stringBuilderForLabel.ToString());
        }

        public override void Interaction()
        {
            base.Interaction();
            OnInteractionWithWaypointArg?.Invoke(this);
        }

        public override void Teleport(Paladin hero)
        {
            hero.Teleport(this);
            OnTeleport?.Invoke();
            teleportSfx.PlayRandomly();
        }

        internal SavedDataOfWaypoint GetStateDataForSaving()
        {
            SavedDataOfWaypoint _savedData = new SavedDataOfWaypoint();

            if (activated)
            {
                _savedData.Set(ID);
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
            ParticleSystem.MainModule mainModule;
            for (int i = activateEffect.Length - 1; i >= 0; i--)
            {
                mainModule = activateEffect[i].main;
                mainModule.prewarm = true;
            }
        }

        private void AwaitingForActivate()
        {
            OnInteraction += Activate;
        }

        private void ChangeBechaviourForInteraction(SelectionMenu waypointMenu)
        {
            OnInteraction += waypointMenu.Open;
        }

        private void Activate()
        {
            for (int i = activateEffect.Length - 1; i >= 0; i--)
            {
                activateEffect[i].Play();
            }

            activateSfx.PlayRandomly();
            activated = true;

            OnInteraction -= Activate;
            OnActivate?.Invoke();
        }
    }
}