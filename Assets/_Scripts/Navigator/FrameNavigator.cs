using System;
using System.Collections.Generic;
using System.Linq;

namespace NavigatorTool
{
    public class FrameNavigator
    {
        private List<FramePath> _framePaths;

        private delegate IFrame SearchFrame(IFrame frame);
        private event SearchFrame searchFrame = _ => _;

        IFrame _currentScreen;
        IFrame _currentPopup;

        public void InitializeFrames(List<FramePath> framePaths)
        {
            _framePaths = framePaths;
            _framePaths.ForEach(path => path.Frame.GetComponent<IFrame>().Initialize());
        }

        public void OpenFrameById(string id)
        {
            var frameFounded = searchFrame(FindFrameWithId(id));
            OpenFrame(frameFounded);
        }

        public void CloseFrameById(string id)
        {
            var frameFounded = searchFrame(FindFrameWithId(id));
            CloseFrame(frameFounded);
        }

        public T OpenFrameByType<T>() where T : IFrame
        {
            var frameFounded = searchFrame(FindFrameOfType<T>());
            return (T)OpenFrame(frameFounded);
        }

        #region OpenFrame

        private IFrame OpenFrame(IFrame frameToOpen) //TODO: Open-close....
        {
            if (frameToOpen is IPopUp)
            {
                return OpenPopUp((IPopUp)frameToOpen);

            }
            else if (frameToOpen is IScreen)
            {
                return OpenScreen((IScreen)frameToOpen);
            }
            else 
            {
                return EnableScreen((IPanel)frameToOpen);
            }
        }

        private IFrame OpenPopUp(IPopUp popUpToOpen)
        {
            _currentPopup?.Hide();
            _currentPopup = popUpToOpen;

            popUpToOpen.Show();

            return _currentPopup;
        }

        private IFrame OpenScreen(IScreen screenToOpen)
        {
            _currentPopup?.Hide();
            _currentPopup = null;

            if (screenToOpen.Equals(_currentScreen))
            {
                return _currentScreen;
            }

            _currentScreen?.Hide();

            _currentScreen = screenToOpen;

            _currentScreen.Show();

            return _currentScreen;
        }

        private IFrame EnableScreen(IPanel panelToEnable)
        {
            if (panelToEnable.IsEnabled) panelToEnable.Hide();
            else panelToEnable.Show();

            return panelToEnable;
        }

        #endregion

        #region CloseFrame

        private void CloseFrame(IFrame frameToClose)
        {
            if (frameToClose is IPopUp)
            {
                ClosePopUp((IPopUp)frameToClose);

            }
            else if (frameToClose is IScreen)
            {
                CloseScreen((IScreen)frameToClose);
            }
        }

        private void ClosePopUp(IPopUp popUpToClose)
        {
            popUpToClose.Hide();
        }

        private void CloseScreen(IScreen screenToClose)
        {
            _currentScreen?.Show();
            _currentPopup?.Hide();
            _currentPopup = null;
            screenToClose.Hide();
        }

        # endregion

        #region SearchFrame

        private T FindFrameOfType<T>() where T : IFrame =>
            (T)FindFrameOfType(typeof(T));

        private IFrame FindFrameOfType(Type frameType)
        {
            var foundByType = SearchFrameOfType(_framePaths.Select(path => path.Frame.GetComponent<IFrame>()), frameType);
            if (foundByType == null)
                throw new Exception($"Can't find a reference for screen of type {frameType}");
            return foundByType;
        }

        private IFrame FindFrameWithId(string id)
        {
            var foundById = SearchFrameWithId(_framePaths, id);
            if (foundById == null)
                throw new Exception($"Can't find a reference for screen with id {id}");
            return foundById;
        }

        private IFrame SearchFrameOfType(IEnumerable<IFrame> frames, Type frameType) =>
            frames.FirstOrDefault(frame => frame.GetType() == frameType);
        private IFrame SearchFrameWithId(IEnumerable<FramePath> framePaths, string id) =>
            framePaths.FirstOrDefault(path => path.Id == id).Frame.GetComponent<IFrame>();

        #endregion

    }

    public interface IFrame
    {
        void Initialize();
        void Show();
        void Hide();
    }

    public interface IScreen : IFrame
    {

    }

    public interface IPanel : IFrame
    {
        bool IsEnabled { get; }
    }

    public interface IPopUp : IFrame
    {

    }

    [Serializable]
    public struct FramePath
    {
        public string Id;
        public UnityEngine.GameObject Frame;
    }

    [Serializable]
    public struct NavigationButton
    {
        public UnityEngine.UI.Button button;
        public string toScreenId;
    }

    [Serializable]
    public struct CloseButton
    {
        public UnityEngine.UI.Button button;
        public string frameIdToClose;
    }
}
