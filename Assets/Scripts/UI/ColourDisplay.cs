using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PineyPiney.Manage
{
    public class ColourDisplay : MonoBehaviour
    {

        Image image;

        // Start is called before the first frame update
        void Start()
        {
            TryGetComponent(out image);
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void UpdateDisplay()
        {

        }

        public void SetColour(Color32 colour)
        {
            image.color = colour;
        }
    }
}