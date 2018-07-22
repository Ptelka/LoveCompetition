
public class CrushController : Punchable {
    override protected void OnDeath()
    {
        if (IsDead())
        {
            Game.GameOver = true;
            Game.Winner = 0;
        }
    }


}
