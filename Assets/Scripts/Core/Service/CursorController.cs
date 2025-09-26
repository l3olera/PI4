using UnityEngine;

namespace Player47.Core.Service
{
    public class CursorController
    {
        public static void LockCursor()
        {
            Cursor.lockState = CursorLockMode.Locked;   
            Cursor.visible = false;
        }

        public static void UnlockCursor()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    } 
}
