using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameState
{
    void OnPlayGame();
    void OnSuccess();
    void OnFail();
    void OnRetry();
    void OnNextLevel();

    void OnExecution();
}
