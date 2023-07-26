using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

using PineyPiney.Util;

namespace PineyPiney.Manage
{
    public class HousesTab : BuildingButtonContainer
    {

        public int columns = 4;

        NewRoomButton buttonPrefab;

        // Start is called before the first frame update
        void Start()
        {
            buttonPrefab = Prefabs.HOUSE_BUTTON;
            LoadTab();
        }

        // Update is called once per frame
        void Update()
        {

        }

        void LoadTab()
        {
            Resources.FindObjectsOfTypeAll<NPC>().Where(i => i.isActiveAndEnabled).ToList().ForEachIndexed(AddButton);
        }

        void AddButton(NPC character, int i)
        {
            Sprite sprite = character.spriteRenderer.sprite;

            NewRoomButton newButton = Instantiate(buttonPrefab);
            newButton.owner = character;
            newButton.TryGetComponent(out Image image);
            newButton.TryGetComponent(out RectTransform rectTransform);

            image.sprite = sprite;
            image.material = new Material(image.material);

            rectTransform.SetParent(transform, false);
            rectTransform.anchorMin = new(0f, 1f);
            rectTransform.anchorMax = new(0f, 1f);
            rectTransform.anchoredPosition = new(25 + (70 * (i % columns)), -(95 + 70 * (i / columns)));
            rectTransform.localScale = new(1, 1, 1);
        }
    }
}