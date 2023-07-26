using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using PineyPiney.Util;

namespace PineyPiney.Manage
{
    public class PlayerRenderer : MonoBehaviour
    {

        [Range(-1f, 1f)]
        public float cameraOffset = 0.5f;

        public Texture2D main;
        public Texture2D eyes;
        public Texture2D nose;
        public Texture2D mouth;
        public Transform hair { get { return transform.Find("Hair"); } }

        string skinToneID = skinTones.First().Key;
        Color32 skinTone = skinTones.First().Value;

        string hairStyleID = hairStyles.First().Key;
        Sprite hairStyle
        {
            get { return hair.GetComponent<SpriteRenderer>().sprite; }
            set { hair.GetComponent<SpriteRenderer>().sprite = value; }
        }

        string hairColourID = hairColours.First().Key;
        Color32 hairColour = hairColours.First().Value;

        public Renderer sr { get { return GetComponent<SpriteRenderer>(); } }

        // Start is called before the first frame update
        void Start()
        {
            ReadValues();

            sr.material.SetTexture("_EyeTex", eyes);
            sr.material.SetTexture("_NoseTex", nose);
            sr.material.SetTexture("_MouthTex", mouth);

            SetSkinTone(skinTone);
            SetHairColour(hairColour);
        }

        // Update is called once per frame
        void Update()
        {
        }

        public void SetSkinTone(string name)
        {
            skinToneID = name;
            skinTone = skinTones[name];
            SetSkinTone(skinTone);
        }

        public void SetHairStyle(string name)
        {
            hairStyleID = name;
            hairStyle = Resources.Load<Sprite>(hairDirectory + hairStyles[name]);
        }

        public void SetHairColour(string name)
        {
            hairColourID = name;
            hairColour = hairColours[name];
            SetHairColour(hairColour);
        }

        public void SetSkinTone(Color32 colour)
        {
            sr.material.SetColor("_SkinTone", colour);
        }

        public void SetHairColour(Color32 colour)
        {
            hair.GetComponent<SpriteRenderer>().material.SetColor("_Color", colour);
        }

        public void WriteValues()
        {
            value.skinToneID = skinToneID;
            value.hairStyleID = hairStyleID;
            value.hairColourID = hairColourID;
        }

        public void ReadValues()
        {
            if(value.skinToneID != null) SetSkinTone(value.skinToneID);
            if (value.hairStyleID != null) SetHairStyle(value.hairStyleID);
            else value.hairStyleID = hairStyleID;
            if(value.hairColourID != null) SetHairColour(value.hairColourID);
        }

        public static Dictionary<string, Color32> skinTones = Funcs.ReadMenuFileDict("Assets/resources/MenuConfig/CharacterMenu/skin_tones.menu", (d, i) => (d["name"], Funcs.ColourFromHexString(d["colour"])));

        public static string hairDirectory = "Textures/Characters/Player/hair/";
        public static Dictionary<string, string> hairStyles = Funcs.ReadMenuFileDict("Assets/resources/MenuConfig/CharacterMenu/hair_styles.menu", (d, i) => (d["name"], d["texture"]));

        public static Dictionary<string, Color32> hairColours = Funcs.ReadMenuFileDict("Assets/resources/MenuConfig/CharacterMenu/hair_colours.menu", (d, i) => (d["name"], Funcs.ColourFromHexString(d["colour"])));


        public struct TransferValues {
            public string skinToneID;
            public string hairStyleID;
            public string hairColourID;
        }

        public static TransferValues value;
    }
}