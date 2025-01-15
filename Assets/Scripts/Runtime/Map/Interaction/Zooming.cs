using Game.Runtime.Management;
using System;

namespace Game.Runtime.Map.Interaction
{
    public class Zooming : AbsMapFeatures
    {
        private const float _STEP = 0.1f;
        private System.Action _onZoomIn;
        private System.Action _onZoomOut;

        private float m_Step;
        private float m_worldCameraOrthographicSize;

        private GlobalSettings.MenuOptions.MapSettings _mapSettings;

        public Zooming(PinLocation pinLocation, Scrolling _scrolling)
        {
            Initialize();
            _onZoomIn += pinLocation.IncreaseScaleOfSpawnedCorners;
            _onZoomIn += _scrolling.DecreaseSpeed;
            _onZoomIn += _scrolling.updateWorldCameraOrthographicSize;
            _onZoomIn += _scrolling.restoreWorldCameraPositionWhenWillBeOutOfTheMap;

            _onZoomOut += pinLocation.DecreseScaleOfSpawnedCorners;
            _onZoomOut += _scrolling.IncreaseSpeed;
            _onZoomOut += _scrolling.updateWorldCameraOrthographicSize;
            _onZoomOut += _scrolling.restoreWorldCameraPositionWhenWillBeOutOfTheMap;
        }

        public override void Initialize()
        {
            base.Initialize();

            _mapSettings = GlobalReferences.GetMapControler.MapSettings;
            updateWorldCameraOrthographicSize();
        }

        public override void Execute()
        {
            if (isMouseScrollWhellMovingIn() && canCameraZoomIn())
            {
                calculateStep();
                stepIn();

                _onZoomIn?.Invoke();
                updateWorldCameraOrthographicSize();
            }

            if (isMouseScrollWhellMovingOut() && canCameraZoomOut())
            {
                calculateStep();
                stepOut();

                _onZoomOut?.Invoke();
                updateWorldCameraOrthographicSize();
            }
        }

        private void updateWorldCameraOrthographicSize()
        {
            m_worldCameraOrthographicSize = _worldCamera.orthographicSize;
        }

        private bool canCameraZoomIn()
        {
            return m_worldCameraOrthographicSize > _mapSettings.ZoomLimitMin;
        }

        private bool canCameraZoomOut()
        {
            return m_worldCameraOrthographicSize < _mapSettings.ZoomLimitMax;
        }

        private bool isMouseScrollWhellMovingIn()
        {
            return _mouseControler.IsMouseScrollWhellMovingIn();
        }

        private bool isMouseScrollWhellMovingOut()
        {
            return _mouseControler.IsMouseScrollWhellMovingOut();
        }

        private void calculateStep()
        {
            m_Step = _STEP * _mapSettings.ZoomSpeed;
        }

        private void stepIn()
        {
            _worldCamera.orthographicSize -= m_Step;
        }

        private void stepOut()
        {
            _worldCamera.orthographicSize += m_Step;
        }

        internal void AttachToZoomIn(Action method)
        {
            _onZoomIn += method;
        }

        internal void AttachToZoomOut(Action method)
        {
            _onZoomOut += method;
        }
    }
}