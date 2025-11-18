using UnityEngine;

namespace Player47.UI.Menu
{
    /// <summary>
    /// Responsável por controlar as configuração do jogo, fazendo as mecanicas funcionarem.
    /// </summary>
    public class SettingsController : MonoBehaviour
	{
        public void SetWindowedMode(bool value)
		{
            if(value)
            {
                Screen.fullScreenMode = FullScreenMode.Windowed;
            }
            else
            {
                Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
            }
        }
	}

}