
[System.Serializable]
public class Range
{
    public float Min;
    public float Max;
    public int LastRandom { get; private set; }

    public float Random()
    {
        int _min = (int)Min;
        int _max = (int)Max;
        LastRandom = UnityEngine.Random.Range(_min, _max + 1);
        return LastRandom;
    }
}
