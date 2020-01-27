namespace Assets._1_Assets._1_Test.Scripts.State
{
    public interface State
    {
        State Update(PhysicsObject po, PlayerData playerData);
    }
}
