
using Game.UI.Text;
using UnityEngine;

namespace Game.Item
{
    public class Gold : Loot
    {
        [SerializeField] private Material dropMaterial;
        [SerializeField] private FloatingText gettingEffectPrefab;

        [Zenject.Inject]
        private FloatingText.Factory factory;

        public int Value { get; private set; }
        internal Material DropMaterial => dropMaterial;


        internal override void HasFellOnTheGround()
        {
            base.HasFellOnTheGround();

            EnableLocalCollider();
        }

        public override void GoTo() =>
            _playerManager.UpdateTarget(transform.localPosition);

        public override void Interaction()
        {
            base.Interaction();

            CreateFloatingText();
            _playerManager.AddGold(Value);
            DisableLocalCollider();
            DisableGraphic();
            DetachHotKeyAltPressing();
            DetachUpdateHUDPosition();
            _globalLootManager.DeleteSingle(this);
            DeleteLabels();
            DeleteFromTheGround();
            Destroy(_localMesh.MeshFilter);
        }

        protected override void Destroy()
        {
            Destroy(gameObject, 2f);
            audioPlayer.Play();
        }

        private void CreateFloatingText()
        {
            FloatingText gettingEffect = factory.Create();

            gettingEffect.Initialize(singleLabel);
            gettingEffect.SetColor(Color.yellow);
            gettingEffect._startWorldPosition = singleLabel.Owner.transform.position;
            gettingEffect.Play();
        }

        internal void SetValue(int value)
        {
            Value = value;
            singleLabel.SetText(value.ToString());
            labelToReact.SetText(value.ToString());
        }
    }
}