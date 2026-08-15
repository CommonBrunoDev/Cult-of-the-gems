using UnityEngine;

public interface IGemmerState
{
    void EnterState(GemmerAI gemmer);
    void UpdateState();
}