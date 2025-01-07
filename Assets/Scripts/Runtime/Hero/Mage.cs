using Game.Runtime.Management;
using Game.Runtime.Skill.Controler;
using Game.Runtime.UI;

namespace Game.Runtime.Actor.Hero
{
    public class Mage : Character
    {
        private void Start()
        {
            OnAddNewSkill += GlobalReferences.PlayerSkillControler.TurnOnSkillOnRightSide;
        }

        internal override void TryUseSkill(SkillControler currentSkillBindedUnderAKey)
        {
            if (!IsSwingingWithTheWeapon)
            {
                IcurrentUsedSkill = currentSkillBindedUnderAKey;
                IcurrentUsedSkill.Trigger();
            }
        }

        public void OnAttackAnimationStart()
        {
            IcurrentUsedSkill.OnAnimationStart();
        }

        protected override void SetModelOfWeapon(Item.Equipment item)
        {
            equipmentAsVisualModel.SetModelOfWeapon(item, HeroType.Mage);
        }

        protected override void SetModelOfOffHand(Item.Equipment item)
        {
            equipmentAsVisualModel.SetModelOfOffHand(item, HeroType.Mage);
        }

        protected override void SetModelOfHelmet(Item.Equipment item)
        {
            equipmentAsVisualModel.SetModelOfHelmet(item, HeroType.Mage);
        }

        protected override void SetModelOfChest(Item.Equipment item)
        {
            equipmentAsVisualModel.SetModelOfChest(item, HeroType.Mage);
        }

        protected override void InitializeNormalAttack()
        {
            _normalAttack = CreateNormalAttack();
            base.InitializeNormalAttack();
            UIControler uIControler = _gameMainManager.UIControler;
            uIControler.InitializeNormalAttack(HeroType.Mage, _normalAttack);
            CharacterSkillControler playerSkillControler = GlobalReferences.PlayerSkillControler;
            playerSkillControler.InitializeNormalAttack(HeroType.Mage, _normalAttack);
        }

        private NormalAttackShoot CreateNormalAttack()
        {
            ISkillControler _createdNormalAttack = _spellBuilder.CreateShootNormalAttack();
            _createdNormalAttack.Initialize();
            NormalAttackShoot _shootNormalAttackControler = (NormalAttackShoot)_createdNormalAttack;

            return _shootNormalAttackControler;
        }
    }
}