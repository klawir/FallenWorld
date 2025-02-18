using Game.Runtime.Management;
using System;
using UnityEngine;

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
        private Camera _worldCamera;

        public Zooming(PinLocation pinLocation, Scrolling _scrolling)
        {
            Initialize();
            _onZoomIn += pinLocation.IncreaseScaleOfSpawnedCorners;
            _onZoomIn += _scrolling.DecreaseSpeed;
            _onZoomIn += _scrolling.UpdateWorldCameraOrthographicSize;
            _onZoomIn += _scrolling.RestoreWorldCameraPositionWhenWillBeOutOfTheMap;

            _onZoomOut += pinLocation.DecreseScaleOfSpawnedCorners;
            _onZoomOut += _scrolling.IncreaseSpeed;
            _onZoomOut += _scrolling.UpdateWorldCameraOrthographicSize;
            _onZoomOut += _scrolling.RestoreWorldCameraPositionWhenWillBeOutOfTheMap;
        }

        public override void Initialize()
        {
            base.Initialize();

            _worldCamera = GlobalReferences.CameraCollection.World;
            _mapSettings = GlobalReferences.GetMapControler.MapSettings;
            UpdateWorldCameraOrthographicSize();
        }

        public override void Execute()
        {
            if (IsMouseScrollWhellMovingIn() && CanCameraZoomIn())
            {
                CalculateStep();
                StepIn();

                _onZoomIn?.Invoke();
                UpdateWorldCameraOrthographicSize();
            }

            if (IsMouseScrollWhellMovingOut() && CanCameraZoomOut())
            {
                CalculateStep();
                StepOut();

                _onZoomOut?.Invoke();
                UpdateWorldCameraOrthographicSize();
            }
        }

        private void UpdateWorldCameraOrthographicSize()
        {
            m_worldCameraOrthographicSize = _worldCamera.orthographicSize;
        }

        private bool CanCameraZoomIn()
        {
            return m_worldCameraOrthographicSize > _mapSettings.ZoomLimitMin;
        }

        private bool CanCameraZoomOut()
        {
            return m_worldCameraOrthographicSize < _mapSettings.ZoomLimitMax;
        }

        private bool IsMouseScrollWhellMovingIn()
        {
            return GlobalReferences.InputControler.Mouse.IsMouseScrollWhellMovingIn();
        }

        private bool IsMouseScrollWhellMovingOut()
        {
            return GlobalReferences.InputControler.Mouse.IsMouseScrollWhellMovingOut();
        }

        private void CalculateStep()
        {
            m_Step = _STEP * _mapSettings.ZoomSpeed;
        }

        private void StepIn()
        {
            _worldCamera.orthographicSize -= m_Step;
        }

        private void StepOut()
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