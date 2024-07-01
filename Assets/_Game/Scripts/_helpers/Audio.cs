using UnityEngine;

namespace Leaf._helpers
{
    [System.Serializable]
    public class Audio
    {
        public string Name;
        [SerializeField]
        private AudioClip[] _clipArray;
        public AudioClip Clip { get { return _clipArray[Random.Range(0, _clipArray.Length)]; } }
        public float Volume = 1f;
        public float Pitch = 1f;
        public bool Loop = false;
    }
}