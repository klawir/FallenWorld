using Game.Runtime.Management;
using UnityEngine;

namespace Game.Runtime.Audio
{
    [System.Serializable]
    public class AudioControler
    {
        [SerializeField] private AudioClip _audioClipSell;

        private AudioSource _audioSourceSell;
        private AudioSource _clickOnItem;
        private AudioSource _inventoryOpen;
        private AudioSource _inventoryClose;
        private AudioSource _tradeOpen;
        private AudioSource _tradeClose;

        public void Initialize()
        {
            var characterControler = GlobalReferences.CharacterControler;
            _clickOnItem = characterControler.Character.ClickOnItemAudioSource;
            _inventoryOpen = characterControler.Character.InventoryOpenAudioSource;
            _inventoryClose = characterControler.Character.InventoryCloseAudioSource;
            _tradeOpen = characterControler.Character.TradeOpenAudioSource;
            _tradeClose = characterControler.Character.TradeCloseAudioSource;
            _audioSourceSell = characterControler.Character.SellItemAudioSource;
            _audioSourceSell.clip = _audioClipSell;
        }

        public void PlayPickUp()
        {
            _clickOnItem.Play();
        }

        public void PlayOpenInventory()
        {
            _inventoryOpen.Play();
        }

        public void PlayCloseInventory()
        {
            _inventoryClose.Play();
        }

        public void PlayOpenTrade()
        {
            _tradeOpen.Play();
        }

        public void PlayCloseTrade()
        {
            _tradeClose.Play();
        }

        public void PlayOpenMap()
        {
            _inventoryOpen.Play();
        }

        public void PlayCloseMap()
        {
            _inventoryClose.Play();
        }

        internal void PlayTakeInventoryItemSFX()
        {
            CreateGameObjectSFX(_clickOnItem.clip);
        }

        internal void PlaySFX(AudioClip audioClipPlace)
        {
            CreateGameObjectSFX(audioClipPlace);
        }

        internal void PlaySellSFX()
        {
            CreateGameObjectSFX(_audioClipSell);
        }

        private void CreateGameObjectSFX(AudioClip sfxToPlay)
        {
            GameObject gameObjectWithSFX = new GameObject(nameof(gameObjectWithSFX), typeof(AudioSource));
            gameObjectWithSFX.TryGetComponent(out AudioSource audioPlayer);
            audioPlayer.clip = sfxToPlay;
            audioPlayer.Play();
            UnityEngine.Object.Destroy(gameObjectWithSFX, sfxToPlay.length);
        }
    }
}