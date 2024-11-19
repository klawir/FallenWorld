using Game.Runtime.Management;
using Game.Runtime.Skill.Controler;
using UnityEngine;

namespace Game.Runtime.Actor.Hero
{
    public class Mage : Hero
    {
        public override void Initialize()
        {
            base.Initialize();

            OnAddNewSkill += turnOnSkillOnRightSide;
            _isIcurrentSkillBindedUnderRightMouseButtonNull = true;
        }
        
        private void turnOnSkillOnRightSide(Skill.Controler.SkillControler newSkill)
        {
            var uiSkillContainer = GlobalReferences.SceneHierarchy.SkillsForRightMouse;
            uiSkillContainer.TurnOn();
            OnAddNewSkill -= turnOnSkillOnRightSide;
        }

        public override void OnSkill1()
        {
            if (!_isIcurrentSkillBindedUnderAlphaKey1Null &&
                !IsSwingingWithTheWeapon)
            {
                IcurrentUsedSkill = IcurrentSkillBindedUnderAlphaKey1;
                IcurrentUsedSkill.Trigger();
                clearActionForCurrentTargetFromMouseExitActionforSkill1();
            }
        }

        public override void OnSkill2()
        {
            if (!_isIcurrentSkillBindedUnderAlphaKey2Null &&
                !IsSwingingWithTheWeapon)
            {
                IcurrentUsedSkill = IcurrentSkillBindedUnderAlphaKey2;
                IcurrentUsedSkill.Trigger();
                clearActionForCurrentTargetFromMouseExitActionforSkill2();
            }
        }

        public override void OnSkill3()
        {
            if (!_isIcurrentSkillBindedUnderAlphaKey3Null && 
                !IsSwingingWithTheWeapon)
            {
                IcurrentUsedSkill = IcurrentSkillBindedUnderAlphaKey3;
                IcurrentUsedSkill.Trigger();
                clearActionForCurrentTargetFromMouseExitActionforSkill3();
            }
        }

        public override void OnSkillLeftMouse()
        {
            if (!_isIcurrentSkillBindedUnderLeftMouseButtonNull && 
                !IsSwingingWithTheWeapon)
            {
                IcurrentUsedSkill = IcurrentSkillBindedUnderLeftMouseButton;
                IcurrentUsedSkill.Trigger();
                _mouseControler.OnButtonLeftUpForLmb();
            }
        }

        public override void OnSkillRightMouse()
        {
            if (!_isIcurrentSkillBindedUnderRightMouseButtonNull && 
                !IsSwingingWithTheWeapon)
            {
                IcurrentUsedSkill = IcurrentSkillBindedUnderRightMouseButton;
                IcurrentUsedSkill.Trigger();
                _mouseControler.OnButtonRightUpForRmb();
            }
        }

        internal override void SetCurrentSkillOnRightMouse(UI.GUI.Button.Spell pickedSkill)
        {
            base.SetCurrentSkillOnRightMouse(pickedSkill);
            if (pickedSpellIsTheSame())
            {
                return;
            }

            OnSetCurrentSkill?.Invoke(_extractedSkill, Enums.SkillSlotType.RightMouse);
            //dev note: update UI for SkillsForRightMouse

            IcurrentSkillBindedUnderRightMouseButton = _extractedSkill;//dev note: na poczatku jest null
            _isIcurrentSkillBindedUnderRightMouseButtonNull = false;
        }

        internal override void SetCurrentSkillForAlpha1(UI.GUI.Button.Spell pickedSkill)
        {
            base.SetCurrentSkillForAlpha1(pickedSkill);

            bool pickedSpellIsTheSame = !_isIcurrentSkillBindedUnderAlphaKey1Null &&
                IcurrentSkillBindedUnderAlphaKey1.UniquieID == _extractedSkill.UniquieID;

            if (pickedSpellIsTheSame)
            {
                return;
            }

            OnSetCurrentSkill?.Invoke(_extractedSkill, Enums.SkillSlotType.Alpha1);

            IcurrentSkillBindedUnderAlphaKey1 = _extractedSkill;
            _isIcurrentSkillBindedUnderAlphaKey1Null = false;
        }

        internal override void SetCurrentSkillForAlpha2(UI.GUI.Button.Spell pickedSpell)
        {
            base.SetCurrentSkillForAlpha2(pickedSpell);

            bool _pickedSpellIsTheSame = !_isIcurrentSkillBindedUnderAlphaKey2Null &&
                IcurrentSkillBindedUnderAlphaKey2.UniquieID == _extractedSkill.UniquieID;

            if (_pickedSpellIsTheSame)
            {
                return;
            }

            OnSetCurrentSkill?.Invoke(_extractedSkill, Enums.SkillSlotType.Alpha2);
            IcurrentSkillBindedUnderAlphaKey2 = _extractedSkill;
            _isIcurrentSkillBindedUnderAlphaKey2Null = false;
        }

        internal override void SetCurrentSkillForAlpha3(UI.GUI.Button.Spell pickedSpell)
        {
            base.SetCurrentSkillForAlpha3(pickedSpell);

            bool _pickedSpellIsTheSame = !_isIcurrentSkillBindedUnderAlphaKey3Null &&
                IcurrentSkillBindedUnderAlphaKey3.UniquieID == _extractedSkill.UniquieID;

            if (_pickedSpellIsTheSame)
            {
                return;
            }

            OnSetCurrentSkill?.Invoke(_extractedSkill, Enums.SkillSlotType.Alpha3);
            IcurrentSkillBindedUnderAlphaKey3 = _extractedSkill;
            _isIcurrentSkillBindedUnderAlphaKey3Null = false;
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
            _normalAttack = InitializeShootNormalAttack();
            base.InitializeNormalAttack();

            var uiSkillContainer = GlobalReferences.SceneHierarchy.SkillsForRightMouse;
            uiSkillContainer.TurnOff();
        }

        private NormalAttackShoot InitializeShootNormalAttack()
        {
            ISkillControler _createdNormalAttack = _spellBuilder.CreateShootNormalAttack();
            _createdNormalAttack.Initialize();
            NormalAttackShoot _shootNormalAttackControler = (NormalAttackShoot)_createdNormalAttack;

            return _shootNormalAttackControler;
        }
    }
}