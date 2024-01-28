using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace NavigatorTool
{
    public class FrameNavigatorInstaller : MonoBehaviour
    {
        [SerializeField] private string firstScreenId;
        [SerializeField] private UnityEvent OnInitialize;
        [SerializeField] private List<FramePath> framePaths;

        private List<FramePath> _currentFramePaths = new();

        private void Awake()
        {
            _currentFramePaths = framePaths;

            OnInitialize?.Invoke();
            InitializeSimpleNavigator();
            NavigateToFirstScreen();
        }

        private void InitializeSimpleNavigator()
        {
            FrameNavigatorProvider.Reset();
            FrameNavigatorProvider.FrameNavigator.InitializeFrames(_currentFramePaths);
        }

        private void NavigateToFirstScreen()
        {
           FrameNavigatorProvider.FrameNavigator.OpenFrameById(firstScreenId);
        }
    }
}

