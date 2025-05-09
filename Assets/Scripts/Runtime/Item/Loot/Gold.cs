using Game.Runtime.Management;
using Game.Runtime.Utility;
using System.Collections;
using UnityEngine;

namespace Game.Runtime.Item.Loot
{
    public class Gold : Loot, IAdditionalLoot
    {
        [SerializeField] private Game.Runtime.Audio.AudioSetControler pickUpSfx;
        [SerializeField] private ParticleSystem dropEffect;
        [SerializeField] private GameObject _additionaLootModel;

        [SerializeField] private Light _highlight;
        [SerializeField] private float _normal;
        [SerializeField] private float _glowingUp;

        private bool m_interactionTrying;

        public uint Value { get; private set; }

        protected override void OnParticleCollision(GameObject other)
        {
            bool _isThisDroppedLoot = other.GetInstanceID() == _spawnedEffect.gameObject.GetInstanceID();

            if (_isThisDroppedLoot)
            {
                HasFellOnTheGround(); 
                _dropEffect.OnHitTheGround();
            }

            if (m_interactionTrying)
            {
                Interaction();
                IsSpawned = false;
                m_interactionTrying = false;
            }
        }

        protected override void ReactionForInteraction()
        {
            GlobalReferences.CharacterControler.AddGold(Value);
        }

        protected override void EnableGraphic()
        {
            _additionaLootModel.SetActive(true);
        }

        public override void DisableGraphic()
        {
            _additionaLootModel.SetActive(false);
        }

        internal override void Drop()
        {
            _spawnedEffect = Instantiate(dropEffect);
            _dropEffect.LoadItemDefinitionForAdditionalLoot(this, _spawnedEffect);
            IsFallingToTheGround = true;
        }

        public override void Select()
        {
            bool _fellToTheGround = !IsFallingToTheGround;
            if (_fellToTheGround)
            {
                base.Select();
                _highlight.intensity = _glowingUp;
            }
        }

        public override void Deselect()
        {
            base.Deselect();

            _highlight.intensity = _normal;
        }

        public override void Interaction()
        {
            if (IsSpawned)
            {
                Debug.Log(name+ " Interaction()");
                OnInteract?.Invoke();
                StartCoroutine(DisableGameObject());
            }

            m_interactionTrying = true;
        }
        
        private IEnumerator DisableGameObject()
        {
            CreateFloatingText();
            UnSubscribeHotKeyAltPressing();
            pickUpSfx.PlayRandomly();
            DisableLocalCollider();
            DisableGraphic();
            DeactivateLabels();

            while (pickUpSfx.IsPlaying)
            {
                yield return null;
            }

            Destroy();
        }

        private void CreateFloatingText()
        {
            string textForFloatingText = StringUtility.BuildStringWithAppendLineAtTheEnd(Value.ToString(), " gold");

            GlobalReferences.UIControler.CreateFloatingText(
                textForFloatingText,
                transform,
                UI.FloatingTextType.MoveY,
                Color.yellow);
        }

        internal override void HasFellOnTheGround()
        {
            HitTheGround();
        }

        internal override void ReactivateFromObjectPool()
        {
            DisableGraphic();
            _dropEffect.RandomHeightStartPosition();
            ThrowUp();
            singleLabel.ActiveGameObject();
            labelToReact.Active();
            UpdateLabelPosition();
            EnableLocalCollider();

            m_interactionTrying = false;
            IsSpawned = false;
            EndOfLifetime = false;
            _highlight.intensity = _normal;
            gameObject.SetActive(true);
        }

        protected override void Destroy()
        {
            GlobalReferences.GetObjectPool.ToPool(this);
            gameObject.SetActive(false);
            EndOfLifetime = true;
            SetTagToCreated();
        }

        internal void SetValue(uint value)
        {
            Value = value;
            singleLabel.SetText(value.ToString() + " gold");
            labelToReact.SetText(value.ToString() + " gold");
        }
    }
}