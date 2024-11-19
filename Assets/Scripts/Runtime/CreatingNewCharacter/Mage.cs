using Game.Runtime.Scenes.CreatingNewCharacter;
using UnityEngine;

namespace Game.Runtime.CreatingNewCharacter
{
    public class Mage : CharacterToCreate
    {
        [SerializeField] private ParticleSystem[] _casting;
        [SerializeField] private Game.Runtime.Audio.AudioSetControler _castingSfx;

        internal override void ReturnToIdle()
        {
            base.ReturnToIdle();

            stop_castingFX();
            _castingSfx.Stop();
        }

        internal override void StopPlayAudio()
        {
            _castingSfx.Stop();
        }

        public void _casting()
        {
            start_castingFX();
            _castingSfx.PlayRandomly();
        }

        private void start_castingFX()
        {
            for (int i = _casting.Length - 1; i >= 0; i--)
            {
                _casting[i].Play();
            }
        }

        private void stop_castingFX()
        {
            for (int i = _casting.Length - 1; i >= 0; i--)
            {
                _casting[i].Stop();
            }
        }
    }
}