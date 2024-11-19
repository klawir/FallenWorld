using Game.Runtime.Management;
using Game.Runtime.UI.HUD;
using System.Collections;
using UnityEngine;

namespace Game.Runtime.Item.Loot
{
    public class Gold : Loot
    {
        [Zenject.Inject]
        private FloatingText._factory _factory;

        [SerializeField] private Game.Runtime.Audio.AudioSetControler _pickUpSfx;
        [SerializeField] private ParticleSystem _dropEffect;
        [SerializeField] private GameObject _additionaLootModel;

        [SerializeField] private Light _highlight;
        [SerializeField] private float _normal;
        [SerializeField] private float _glowingUp;

        private bool m_interactionTrying;

        public int Value { get; private set; }

        protected override void OnParticleCollision(GameObject other)
        {
            bool _isThisDroppedLoot = other.GetInstanceID() == _spawnedEffect.gameObject.GetInstanceID();

            if (_isThisDroppedLoot)
            {
                HasFellOnTheGround(); 
                __dropEffect.OnHitTheGround();
            }

            if (m_interactionTrying)
            {
                Interaction();
                IsSpawned = false;
                m_interactionTrying = false;
            }
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
            _spawnedEffect = Instantiate(_dropEffect);
            __dropEffect.LoadItemDefinitionForGold(this, _spawnedEffect);
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

        public override void ComeToMe()
        {
            _playerManager.UpdateTarget(transform.localPosition);
        }

        public override void Interaction()
        {
            if (IsSpawned)
            {
                StartCoroutine(disableGameObject());
            }

            m_interactionTrying = true;
        }
        
        private IEnumerator disableGameObject()
        {
            CreateFloatingText();
            _playerManager.AddGold(Value);
            unSubscribeHotKeyAltPressing();
            _pickUpSfx.PlayRandomly();
            _globalLootManager.ClearStack(labelToReact);

            DisableLocalCollider();
            DisableGraphic();
            DeactivateLabels();

            while (_pickUpSfx.IsPlaying)
            {
                yield return null;
            }

            Destroy();
        }

        internal override void HasFellOnTheGround()
        {
            hitTheGround();
        }

        internal override void Reactivate()
        {
            DisableGraphic();
            __dropEffect.RandomHeightStartPosition();
            ThrowUp();
            singleLabel.ActiveGameObject();
            labelToReact.Active();
            UpdateLabelPosition();
            EnableLocalCollider();

            m_interactionTrying = false;
            IsSpawned = false;
            _pickedUp = false;
            _highlight.intensity = _normal;
        }

        protected override void Destroy()
        {
            GlobalReferences.GetObjectPool.ToPool(this);
            gameObject.SetActive(false);
            _pickedUp = true;
            SetTagToCreated();
        }

        private void CreateFloatingText()
        {
            FloatingText gettingEffect = _factory.Create();

            gettingEffect.Initialize(singleLabel, Color.yellow);
            gettingEffect.Play();
        }

        internal void SetValue(int value)
        {
            Value = value;
            singleLabel.SetText(value.ToString() + " gold");
            labelToReact.SetText(value.ToString() + " gold");
        }
    }
}