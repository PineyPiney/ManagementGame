using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

using PineyPiney.Util;

namespace PineyPiney.Manage
{
    public class ColourSelector : MonoBehaviour
    {

        public TMP_InputField input;
        public ColourDisplay display;

        BuildingItem editing = null;
        public int colour = 1;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void SetEditing(BuildingItem item)
        {
            editing = item;
            SetColour(1);
        }

        public void SetColour(int value)
        {
            colour = value;
            input.text = Funcs.HexStringFromColour(editing.GetColour(value));
        }

        public void ChangeColour()
        {
            string value = input.text;
            string filtered = new(value.Where(c => hexChars.Contains(c)).ToArray());
            if (filtered == value && value.Length > 0)
            {
                try
                {
                    Color32 c = Funcs.ColourFromHexString(filtered);
                    display.SetColour(c);

                    editing.SetColour(colour, c);
                    if(Builder.INSTANCE.placingButton != null) Builder.INSTANCE.placingButton.SetColour(colour, c);
                }
                catch 
                {

                }
            }
            else
            {
            }
            input.text = filtered.ToUpper();
        }
    public static string hexChars = "0123456789ABCDEFabcdef";
    }
}