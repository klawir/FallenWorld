
using UnityEngine;

namespace Game.Runtime.Transportation
{
    [System.Serializable]
    public struct PortalsController
    {
        [SerializeField] private Portal nearHero;
        [SerializeField] private Portal inTown;

        internal void Summon()
        {
            nearHero.Close();
            nearHero.PlaceNearHero();

            nearHero.Summon();
            inTown.Summon();

            inTown.OnInterract += Close;
        }

        private void Close()
        {
            nearHero.Close();
            inTown.Close();
            inTown.OnInterract = null;
        }

        internal void ClearMouseActions()
        {
            nearHero.ClearActionForMouseExit();
            nearHero.ClearActionForMouseOver();

            inTown.ClearActionForMouseExit();
            inTown.ClearActionForMouseOver();
        }

        internal void RestoreMouseActions()
        {
            nearHero.AttachDefaultActionOnMouseExit();
            nearHero.AttachDefaultActionOnMouseOver();

            inTown.AttachDefaultActionOnMouseExit();
            inTown.AttachDefaultActionOnMouseOver();
        }
    }
}