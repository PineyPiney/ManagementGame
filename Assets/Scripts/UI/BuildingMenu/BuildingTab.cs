using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

using PineyPiney.Util;

namespace PineyPiney.Manage
{
    public class BuildingTab : BuildingButtonContainer
    {

        public BuildingItem prefab;
        public string filePath;
        public int columns = 4;

        BuildingItemButton buttonPrefab;

        // Start is called before the first frame update
        void Start()
        {
            buttonPrefab = Prefabs.ITEM_BUTTON;
            LoadTab();
        }

        // Update is called once per frame
        void Update()
        {

        }

        void LoadTab()
        {
            Funcs.ReadMenuFile("Assets/resources/MenuConfig/BuildingMenu/" + filePath, AddButton);
        }

        BuildingItemButton AddButton(Dictionary<string, string> button, int i)
        {
            BuildingItemButton newButton = Instantiate(buttonPrefab);
            newButton.TryGetComponent(out Image image);
            newButton.TryGetComponent(out RectTransform rectTransform);

            newButton.gameObject.name = button["name"] + " Button";

            var p = button.ContainsKey("prefab") ?
                Resources.Load<BuildingItem>("Prefabs/Building/BuildingParts/" + button["prefab"]) :
                prefab;
            newButton.prefab = Instantiate(p, newButton.transform);

            newButton.prefab.name = button["name"];
            
            if (button.ContainsKey("texture"))
            {
                Sprite sprite = Resources.Load<Sprite>("Textures/edit/" + button["texture"]);

                newButton.prefab.TryGetComponent(out SpriteRenderer r);
                newButton.prefab.TryGetComponent(out BoxCollider2D c);
                r.sprite = sprite;
                c.size = sprite.bounds.size;
            }

            string[] colours = button["colours"].Split(';');
            foreach (var colour in colours)
            {
                var c = colour.Split(',').Select(Funcs.ColourFromHexString);
                newButton.defaultColours.Add(c.ToArray());
            }            

            image.material = new Material(image.material);

            rectTransform.SetParent(transform, false);
            rectTransform.anchorMin = new(0f, 1f);
            rectTransform.anchorMax = new(0f, 1f);
            rectTransform.anchoredPosition = new(25 + (70 * (i % columns)), -(95 + 70 * (i / columns)));
            rectTransform.localScale = new(1, 1, 1);

            return newButton;
        }
    }
}