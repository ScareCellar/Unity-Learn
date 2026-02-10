using UnityEngine;

public interface IEquipable
{
    void OnEquip(Actor owner);

    void OnUnequip(Actor owner);
}
