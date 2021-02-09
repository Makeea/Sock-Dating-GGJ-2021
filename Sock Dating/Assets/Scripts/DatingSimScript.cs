using Fungus;
using UnityEngine;

public class DatingSimScript : MonoBehaviour {
    void Start() {
        Application.targetFrameRate = 30;
    }

    public void SetPlayerName() {
        string playerName = GameObject.Find("sockrates").GetComponent<Flowchart>().GetStringVariable("playerName");
        GameObject.Find("PlayerCharacter").GetComponent<Character>().NameText = playerName;
    }

    public void SetSockratesName() {
        GameObject.Find("Sockrates").GetComponent<Character>().NameText = "Sockrates";
    }

    public void SetEndConditionMet() {
        int gothDisposition = GameObject.Find("goth").GetComponent<Flowchart>().GetIntegerVariable("gothDisposition");
        int fataleDisposition = GameObject.Find("fatale").GetComponent<Flowchart>().GetIntegerVariable("fataleDisposition");
        int loliDisposition = GameObject.Find("loli").GetComponent<Flowchart>().GetIntegerVariable("loliDisposition");
        int jockDisposition = GameObject.Find("jock").GetComponent<Flowchart>().GetIntegerVariable("jockDisposition");

        bool endConditionMet = gothDisposition * fataleDisposition * loliDisposition * jockDisposition != 0;
        GameObject.Find("sockrates").GetComponent<Flowchart>().SetBooleanVariable("endConditionMet", true);
        //GameObject.Find("sockrates").GetComponent<Flowchart>().SetBooleanVariable("endConditionMet", endConditionMet);

        GameObject.Find("sockrates").GetComponent<Flowchart>().SetIntegerVariable("gothDisposition", gothDisposition);
        GameObject.Find("sockrates").GetComponent<Flowchart>().SetIntegerVariable("fataleDisposition", fataleDisposition);
        GameObject.Find("sockrates").GetComponent<Flowchart>().SetIntegerVariable("loliDisposition", loliDisposition);
        GameObject.Find("sockrates").GetComponent<Flowchart>().SetIntegerVariable("jockDisposition", jockDisposition);
    }
}
