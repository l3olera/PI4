using UnityEngine;

namespace MidiaAdapt.UI.Menu
{
	public class SettingsController : MonoBehaviour
	{
        /// <summary>
        /// Responsável por controlar as configuração do jogo, fazendo as mecanicas funcionarem.
        /// </summary>
        /// 

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