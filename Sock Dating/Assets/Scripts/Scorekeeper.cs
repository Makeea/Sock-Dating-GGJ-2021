
using UnityEngine;

public static class Scorekeeper {
    public static int loliScore = 0;
    public static int jockScore = 0;
    public static int femmeFataleScore = 0;

    private const int WIN_THRESHOLD = 10;

    public static void UpdateLoliScore(int amount) {
        loliScore += amount;
    }

    public static void UpdateJockScore(int amount) {
        jockScore += amount;
    }

    public static void UpdateFemmeFataleScore(int amount) {
        femmeFataleScore += amount;
    }

    public static void GameEnd() {
        int highestScore = loliScore;
        if (jockScore > highestScore) {
            highestScore = jockScore;
        }
        if (femmeFataleScore > highestScore) {
            highestScore = femmeFataleScore;
        }

        if (highestScore > WIN_THRESHOLD) {
            Debug.Log("win");
        } else {
            Debug.Log("lose");
        }
    }

}
