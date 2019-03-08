using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEquip
{
    void Equip(AttributeSys attributeSys, GameValuesSys valuesSys);
    void UnEquip(AttributeSys attributeSys, GameValuesSys valuesSys);
}

public interface IState
{
    void Enter(AttributeSys attributeSys, GameValuesSys valuesSys);
    void Update(AttributeSys attributeSys, GameValuesSys valuesSys);
    void Exit(AttributeSys attributeSys, GameValuesSys valuesSys);
}

public enum EquipType
{
    health,
    magic,
    strength,
    agile,
    intellgence,
    lucky,
    physique,
    attack,
    dodgeRate
}

public class e_Health : IEquip
{
    private int value;
    public e_Health(int value)
    {
        this.value = value;
    }

    public void Equip(AttributeSys attributeSys, GameValuesSys valuesSys)
    {
        valuesSys.healthLimit += value;
    }

    public void UnEquip(AttributeSys attributeSys, GameValuesSys valuesSys)
    {
        valuesSys.healthLimit -= value;
    }
}

public class e_Magic : IEquip
{
    private int value;
    public e_Magic(int value)
    {
        this.value = value;
    }

    public void Equip(AttributeSys attributeSys, GameValuesSys valuesSys)
    {
        valuesSys.magicLimit += value;
    }

    public void UnEquip(AttributeSys attributeSys, GameValuesSys valuesSys)
    {
        valuesSys.magicLimit -= value;
    }
}

public class e_Strength : IEquip
{
    private int value;
    public e_Strength(int value)
    {
        this.value = value;
    }

    public void Equip(AttributeSys attributeSys, GameValuesSys valuesSys)
    {
        attributeSys.AddStrength(value);
    }

    public void UnEquip(AttributeSys attributeSys, GameValuesSys valuesSys)
    {
        attributeSys.AddStrength(-value);
    }
}

public class e_Agile : IEquip
{
    private int value;
    public e_Agile(int value)
    {
        this.value = value;
    }

    public void Equip(AttributeSys attributeSys, GameValuesSys valuesSys)
    {
        attributeSys.AddAgile(value);
    }

    public void UnEquip(AttributeSys attributeSys, GameValuesSys valuesSys)
    {
        attributeSys.AddAgile(-value);
    }
}

public class e_Intelligence : IEquip
{
    private int value;
    public e_Intelligence(int value)
    {
        this.value = value;
    }

    public void Equip(AttributeSys attributeSys, GameValuesSys valuesSys)
    {
        attributeSys.AddIntelligence(value);
    }

    public void UnEquip(AttributeSys attributeSys, GameValuesSys valuesSys)
    {
        attributeSys.AddIntelligence(-value);
    }
}

public class e_Physicque : IEquip
{
    private int value;
    public e_Physicque(int value)
    {
        this.value = value;
    }

    public void Equip(AttributeSys attributeSys, GameValuesSys valuesSys)
    {
        attributeSys.AddPhysique(value);
    }

    public void UnEquip(AttributeSys attributeSys, GameValuesSys valuesSys)
    {
        attributeSys.AddPhysique(-value);
    }
}

public class e_Lucky : IEquip
{
    private int value;
    public e_Lucky(int value)
    {
        this.value = value;
    }

    public void Equip(AttributeSys attributeSys, GameValuesSys valuesSys)
    {
        attributeSys.AddLucky(value);
    }

    public void UnEquip(AttributeSys attributeSys, GameValuesSys valuesSys)
    {
        attributeSys.AddLucky(-value);
    }
}

public class e_Attack : IEquip
{
    private int value;
    public e_Attack(int value)
    {
        this.value = value;
    }

    public void Equip(AttributeSys attributeSys, GameValuesSys valuesSys)
    {
        valuesSys.attackValue += value;
    }

    public void UnEquip(AttributeSys attributeSys, GameValuesSys valuesSys)
    {
        valuesSys.attackValue -= value;
    }
}

public class EquipmentsFactory
{
    public IEquip SpawnProduct(EquipType equipType,int value)
    {
        switch (equipType)
        {
            case EquipType.health:
                return new e_Health(value);
            case EquipType.agile:
                return new e_Agile(value);
            case EquipType.strength:
                return new e_Strength(value);
            case EquipType.intellgence:
                return new e_Intelligence(value);
            case EquipType.magic:
                return new e_Magic(value);
            case EquipType.physique:
                return new e_Physicque(value);
            case EquipType.lucky:
                return new e_Lucky(value);
            case EquipType.attack:
                return new e_Attack(value);
            default:
                Debug.LogError("error");
                return null;
        }
    }
}