using Game.Runtime.UI.Text;
using UnityEngine;

namespace Game.Runtime.Item.Loot
{
    public class Gold : Loot
    {
        [SerializeField] private Material effectMaterial;

        [Zenject.Inject]
        private FloatingText.Factory factory;

        public int Value { get; private set; }

        public override void Select()
        {
            base.Select();

            _localMesh.MeshRenderer.material = effectMaterial;
        }

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

            gettingEffect.Initialize(singleLabel, Color.yellow);
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