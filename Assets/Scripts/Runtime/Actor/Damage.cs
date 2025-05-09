using Game.Runtime.Item.Definition;
using Game.Runtime.Skill;

[System.Serializable]
public class Damage : Range
{
    public Damage()
    {
        Min = 0f;
        Max = 0f;
    }

    public void IncreaseBy(float value)
    {
        Min += value;
        Max += value;
    }

    public void IncreaseBy(Upgrade fromSkill)
    {
        Min += fromSkill.Damage.Min;
        Max += fromSkill.Damage.Max;
    }

    public void IncreaseBy(Equipment item)
    {
        Min += item.Statistics.MinDamage;
        Max += item.Statistics.MaxDamage;
    }

    public void DecreaseBy(Equipment item)
    {
        Min -= item.Statistics.MinDamage;
        Max -= item.Statistics.MaxDamage;
    }

    public override string ToString()
    {
        return LastRandom.ToString();
    }
}