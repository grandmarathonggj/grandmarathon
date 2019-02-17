using UnityEngine;

namespace DefaultNamespace
{
    public class PauseMenuView : MonoBehaviour
    {
//        private bool _show = false;

        private void Start()
        {
            EventManager.StartListening(GameEvent.PAUSE,
                param =>
                {
//                    transform.Find("Panel").gameObject.SetActive(true); 
                    GetComponent<Animator>().SetTrigger("Open");
                    
                });
            EventManager.StartListening(GameEvent.UNPAUSE,
                param =>
                {
//                    transform.Find("Panel").gameObject.SetActive(false);
                    GetComponent<Animator>().SetTrigger("Close");

                });
        }

        public void OnPreviousLevelClicked()
        {
            EventManager.TriggerEvent(GameEvent.PREVIOUS_LEVEL, null);
        }

        public void OnNextLevelClicked()
        {
            EventManager.TriggerEvent(GameEvent.NEXT_LEVEL, null);
        }

        public void OnReloadLevelClicked()
        {
            EventManager.TriggerEvent(GameEvent.RETRY_LEVEL, null);
        }
    }
}