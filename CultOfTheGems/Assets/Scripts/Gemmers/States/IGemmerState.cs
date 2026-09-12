namespace Gemmers.States
{
    public interface IGemmerState
    {
        void EnterState(GemmerAI gemmer);
        void UpdateState();
    }
}