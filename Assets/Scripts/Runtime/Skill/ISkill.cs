
public interface ISkill
{
    void Initialize();
    void TryUse();
    int UniquieID { get; }
    int Rank { get; }
    float Range { get; }
}
