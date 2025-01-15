using Game.Runtime.Management;
using Game.Runtime.Utility;
using System.Collections;
using System.Text;
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

        public int Value { get; private set; }

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

        public override void ComeToMe()
        {
            _playerControler.UpdateTarget(transform.localPosition);
        }

        public override void Interaction()
        {
            if (IsSpawned)
            {
                Debug.Log(name+ " Interaction()");

                StartCoroutine(DisableGameObject());
            }

            m_interactionTrying = true;
        }
        
        private IEnumerator DisableGameObject()
        {
            GlobalReferences.UIControler.CreateFloatingText(
                StringUtility.BuildString(Value.ToString(), " gold"),
                transform,
                UI.FloatingTextType.MoveY,
                Color.yellow);

            _playerControler.AddGold(Value);
            UnSubscribeHotKeyAltPressing();
            pickUpSfx.PlayRandomly();
            _globalLootManager.ClearStack(labelToReact);
            DisableLocalCollider();
            DisableGraphic();
            DeactivateLabels();

            while (pickUpSfx.IsPlaying)
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

            Debug.Log(tag);
        }

        protected override void Destroy()
        {
            GlobalReferences.GetObjectPool.ToPool(this);
            gameObject.SetActive(false);
            EndOfLifetime = true;
            SetTagToCreated();
        }

        internal void SetValue(int value)
        {
            Value = value;
            singleLabel.SetText(value.ToString() + " gold");
            labelToReact.SetText(value.ToString() + " gold");
        }
    }
}