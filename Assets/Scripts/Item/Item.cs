using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemType itemType;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        Player player = other.GetComponent<Player>();

        switch (itemType)
        {
            case ItemType.LifeUp:
                player.LifeUp();
                break;

            case ItemType.Coffee:
                player.StartCoffee();
                break;

            case ItemType.MachineGun:
                player.StartMachineGun();
                break;

            case ItemType.NuclearBomb:
                player.UseNuclearBomb();
                break;

            case ItemType.ShotGun:
                player.StartShotGun();
                break;

            case ItemType.SmokeBomb:
                player.UseSmokeBomb();
                break;

            case ItemType.SheriffBadge:
                player.StartSheriffBadge();
                break;

            case ItemType.TombStone:
                player.StartTombStone();
                break;

            case ItemType.WagonWheel:
                player.StartWagonWheel();
                break;
        }

        Destroy(gameObject);
    }
}