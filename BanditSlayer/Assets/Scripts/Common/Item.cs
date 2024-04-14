using UnityEngine;

namespace Common
{
    public class Item
    {
        public enum ItemType
        {
            Axe,
            Dagger,
            Katana,
            Mace,
            Polearm,
            Rod,
            Spear,
            Staff,
            Sword,
        }

        public static int GetCost(ItemType itemType)
        {
            switch (itemType)
            {
                default:
                    case ItemType.Dagger: return 0;
                case ItemType.Axe: return 50;
                case ItemType.Spear: return 100;
                case ItemType.Sword: return 200;
                case ItemType.Katana: return 300;
                case ItemType.Polearm: return 400;
                case ItemType.Mace: return 500;
                case ItemType.Staff: return 200;
                case ItemType.Rod: return 1000;
            }
        }

        public static Sprite GetSprite(ItemType itemType)
        {
            switch (itemType)
            {
                default:
                    case ItemType.Dagger: return GameAssets.Instance.daggerSprite;
                case ItemType.Axe: return GameAssets.Instance.axeSprite;
                case ItemType.Spear: return GameAssets.Instance.spearSprite;
                case ItemType.Sword: return GameAssets.Instance.swordSprite;
                case ItemType.Katana: return GameAssets.Instance.katanaSprite;
                case ItemType.Polearm: return GameAssets.Instance.polearmSprite;
                case ItemType.Mace: return GameAssets.Instance.maceSprite;
                case ItemType.Staff: return GameAssets.Instance.staffSprite;
                case ItemType.Rod: return GameAssets.Instance.rodSprite;
            }
        }
    }
}