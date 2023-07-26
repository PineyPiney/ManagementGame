
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

using PineyPiney.Manage;

namespace PineyPiney.Util
{
    public static class Prefabs
    {
        public static BuildingItemButton ITEM_BUTTON { get { return Get<BuildingItemButton>("Prefabs/UI/ItemButton"); } }
        public static NewRoomButton HOUSE_BUTTON { get { return Get<NewRoomButton>("Prefabs/UI/HouseButton"); } }

        public static BuildingOutline BUILDING_OUTLINE { get { return Get<BuildingOutline>("Prefabs/UI/BuildingOutline"); } }

        public static House HOUSE { get { return Get<House>("Prefabs/Building/House"); } }

        public static Door DOOR { get { return Get<Door>("Prefabs/Building/BuildingParts/Door"); } }

        public static Wall WALL { get { return Get<Wall>("Prefabs/Building/BuildingParts/Wall"); } }

        public static Material COLOUR {  get { return Get<Material>("Shaders/Colour"); } }

        public static T Get<T>(string file) where T : Object
        {
            return Resources.Load<T>(file);
        }

        public static Material GetColourMaterial(Color32 colour)
        {
            var m = Object.Instantiate(COLOUR);
            m.SetColor("Tint", colour);
            return m;
        }
    }
}
