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
            Spear,
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
            }
        }
        
        public static int GetDamage(ItemType itemType)
        {
            switch (itemType)
            {
                default:
                case ItemType.Dagger: return 10;
                case ItemType.Axe: return 15;
                case ItemType.Spear: return 20;
                case ItemType.Sword: return 25;
                case ItemType.Katana: return 30;
                case ItemType.Polearm: return 35;
                case ItemType.Mace: return 40;
            }
        }
        
        public static float GetAttackRange(ItemType itemType)
        {
            switch (itemType)
            {
                default:
                case ItemType.Dagger: return 1;
                case ItemType.Axe: return 1.2f;
                case ItemType.Spear: return 2f;
                case ItemType.Sword: return 1.5f;
                case ItemType.Katana: return 1.8f;
                case ItemType.Polearm: return 2f;
                case ItemType.Mace: return 2f;
            }
        }
        
        public static Sprite GetSprite(ItemType itemType)
        {
            GameObject weaponPrefab = GetWeaponPrefab(itemType);
            if (weaponPrefab != null)
            {
                return GetSpriteFromPrefab(weaponPrefab);
            }
            else
            {
                Debug.LogWarning($"Weapon prefab for {itemType} not found.");
                return null;
            }
        }

        public static GameObject GetWeaponPrefab(ItemType itemType)
        {
            switch (itemType)
            {
                default:
                case ItemType.Dagger: return GameAssets.Instance.daggerPrefab;
                case ItemType.Axe: return GameAssets.Instance.axePrefab;
                case ItemType.Spear: return GameAssets.Instance.spearPrefab;
                case ItemType.Sword: return GameAssets.Instance.swordPrefab;
                case ItemType.Katana: return GameAssets.Instance.katanaPrefab;
                case ItemType.Polearm: return GameAssets.Instance.polearmPrefab;
                case ItemType.Mace: return GameAssets.Instance.macePrefab;
            }
        }
        
        private static Sprite GetSpriteFromPrefab(GameObject prefab)
        {
            SpriteRenderer spriteRenderer = prefab.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                return spriteRenderer.sprite;
            }
            else
            {
                Debug.LogWarning("SpriteRenderer not found on weapon prefab.");
                return null;
            }
        }
    }
}