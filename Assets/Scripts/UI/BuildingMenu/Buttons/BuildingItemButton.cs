using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using PineyPiney.Util;

namespace PineyPiney.Manage
{

    public class BuildingItemButton : BuildingMenuButton
    {
        Image image;

        public BuildingItem prefab;

        public Color32 pri;
        public Color32 sec;
        public Color32 ter;
        public List<Color32[]> defaultColours = new();

        // Start is called before the first frame update
        void Start()
        {
            SetPrimary(defaultColours.GetOrDefault(0).GetOrDefault<Color32>(0));
            SetSecondary(defaultColours.GetOrDefault(0).GetOrDefault<Color32>(1));
            SetTertiary(defaultColours.GetOrDefault(0).GetOrDefault<Color32>(2));


        }

        // Update is called once per frame
        void Update()
        {

        }

        public void SetColour(int index, Color32 colour)
        {
            switch (index)
            {
                case 1: SetPrimary(colour); break;
                case 2: SetSecondary(colour); break;
                case 3: SetTertiary(colour); break;
            }
        }

        public void SetPrimary(Color32 colour)
        {
            pri = colour;
            image.material.SetColor("_Primary", colour);
        }

        public void SetSecondary(Color32 colour)
        {
            sec = colour;
            image.material.SetColor("_Secondary", colour);
        }

        public void SetTertiary(Color32 colour)
        {
            ter = colour;
            image.material.SetColor("_Tertiary", colour);
        }

        public void OnClick()
        {
            BuildingItem newObject = Instantiate(prefab);
            newObject.LoadFromButton(this);
            newObject.name = gameObject.name.Substring(0, gameObject.name.Length - 7);
            newObject.transform.localScale = new(1, 1, 1);

            Destroy(Builder.INSTANCE.placing);
            Builder.INSTANCE.placingButton = this;
            Builder.INSTANCE.placing = newObject;
            if (Builder.INSTANCE.IsEditing)
            {
                Builder.INSTANCE.editing.Outline = false;
                Builder.INSTANCE.editing = null;
            }
        }
    }
}