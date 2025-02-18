using Game.Runtime.Scenes.CreatingNewCharacter;
using UnityEngine;

namespace Game.Runtime.CreatingNewCharacter
{
    public class Mage : CharacterToCreate
    {
        [SerializeField] private ParticleSystem[] casting;
        [SerializeField] private Game.Runtime.Audio.AudioSetControler castingSfx;

        internal override void ReturnToIdle()
        {
            base.ReturnToIdle();

            StopCastingFX();
            castingSfx.Stop();
        }

        internal override void StopPlayAudio()
        {
            castingSfx.Stop();
        }

        public void Casting()
        {
            StartCastingFX();
            castingSfx.PlayRandomly();
        }

        private void StartCastingFX()
        {
            for (int i = casting.Length - 1; i >= 0; i--)
            {
                casting[i].Play();
            }
        }

        private void StopCastingFX()
        {
            for (int i = casting.Length - 1; i >= 0; i--)
            {
                casting[i].Stop();
            }
        }
    }
}