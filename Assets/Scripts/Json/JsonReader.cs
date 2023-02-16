using System;
using UnityEngine;

namespace Json
{
    public class JsonReader : MonoBehaviour
    {
        public TextAsset json;
        
        [System.Serializable]
        public class Text {
            public string page;
        }
        
        [System.Serializable]
        public class Texts
        {
            public Text[] text;
        }
        
        
        public Texts text = new Texts();
        
        private void Start() => text = JsonUtility.FromJson<Texts>(json.text);
        
    }
}