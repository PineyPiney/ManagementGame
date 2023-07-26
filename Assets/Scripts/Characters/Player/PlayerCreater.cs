using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using PineyPiney.Util;

namespace PineyPiney.Manage
{
    public class PlayerCreater : MonoBehaviour
    {

        PlayerRenderer display;

        // Start is called before the first frame update
        void Start()
        {
            TryGetComponent(out display);

            AddSkinTones();
            AddHairStyles();
            AddHairColours();
        }

        // Update is called once per frame
        void Update()
        {

        }

        void AddSkinTones()
        {
            AddOptions("Skin Tone Menu", PlayerRenderer.skinTones, PlayerRenderer.value.skinToneID);
        }

        void AddHairStyles()
        {
            AddOptions("Hair Style Menu", PlayerRenderer.hairStyles, PlayerRenderer.value.hairStyleID);
        }

        void AddHairColours()
        {
            AddOptions("Hair Colour Menu", PlayerRenderer.hairColours, PlayerRenderer.value.hairColourID);
        }

        void AddOptions<T>(string listName, Dictionary<string, T> selection, string current)
        {
            var options = selection.Select(t => new TMP_Dropdown.OptionData(t.Key)).ToList();
            var dropdown = GetDropDown(listName);
            dropdown.ClearOptions();
            dropdown.AddOptions(options);
            dropdown.value = options.FindIndex(o => o.text == current);
        }

        public void SetSkinTone(int i)
        {
            SetValue(display.SetSkinTone, "Skin Tone Menu", i);
        }

        public void SetHairStyle(int i)
        {
            SetValue(display.SetHairStyle, "Hair Style Menu", i);
        }

        public void SetHairColour(int i) 
        {
            SetValue(display.SetHairColour, "Hair Colour Menu", i);
        }

        public void SetValue(Action<string> func, string menuName, int i)
        {
            func(GetDropDown(menuName).options[i].text);
        }

        public void Done()
        {
            display.WriteValues();
            SceneUtil.Open(Scenes.MAIN_SCENE);
        }

        public TMP_Dropdown GetDropDown(string name)
        {
            var menu = GameObject.Find(name);
            return menu.GetComponent<TMP_Dropdown>();
        }
    }
}