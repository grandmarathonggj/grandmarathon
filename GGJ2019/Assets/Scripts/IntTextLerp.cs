using System.Collections;
using TMPro;
using UnityEngine;

namespace DefaultNamespace
{
    public class IntTextLerp : MonoBehaviour
    {
        public int current = 0;
        public int target = 100000;
        public int start = 0;
        public float duration = 200f;
        public float lerp = 0;
        public float currentTime = 0;
        public TextMeshProUGUI scoreText;

        private void Start()
        {
            scoreText = GetComponent<TextMeshProUGUI>();

//            StartCoroutine(coLerpText());
        }


//        private IEnumerator coLerpText()
//        {
//            
//        }

        private void Update()
        {
            if (lerp <= 1)
            {
                currentTime += Time.deltaTime;

                lerp = currentTime / duration;
                current = (int) Mathf.Lerp(start, target, lerp);
                scoreText.text = current.ToString();
            }
        }
    }
}