using System;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour
{
    public int ID { get; set; }
    public string Name { get; set; }
    private List<IEquip> Equips = new List<IEquip>();

    public List<IEquip> GetEquipments() { return Equips; }

    public List<SpawnEquip> spawns = new List<SpawnEquip>();
    [Serializable]
    public class SpawnEquip
    {
        public EquipType equipType;
        public int value;
    }

    // Start is called before the first frame update
    void Start()
    {
        EquipmentsFactory factory = new EquipmentsFactory();
        foreach(var v in spawns)
        {
            Equips.Add(factory.SpawnProduct(v.equipType, v.value));
        }
    }
}
