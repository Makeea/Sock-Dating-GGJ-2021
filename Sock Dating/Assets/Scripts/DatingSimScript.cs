using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Fungus;
using UnityEngine;

public class DatingSimScript : MonoBehaviour {
    private GameObject dog;
    private SpriteRenderer dogRenderer;
    private Animator dogAnimator;
    private LinkedList<Vector3> dogPath;
    private LinkedListNode<Vector3> dogNextPosition;

    private float DOG_Y_MIN = float.PositiveInfinity;
    private float DOG_Y_MAX = float.NegativeInfinity;
    private const float DOG_SCALE_MIN = 0.3f;
    private const float DOG_SCALE_MAX = 1f;

    void Start() {
        Application.targetFrameRate = 30;

        dog = GameObject.Find("doggie");
        dogAnimator = dog.GetComponent<Animator>();
        dogRenderer = dog.GetComponent<SpriteRenderer>();


        dogPath = new LinkedList<Vector3>();
        foreach (Transform node in GameObject.Find("doggiePath").transform) {
            dogPath.AddLast(node.position);
            if (node.position.y < DOG_Y_MIN) {
                DOG_Y_MIN = node.position.y;
            }

            if (node.position.y > DOG_Y_MAX) {
                DOG_Y_MAX = node.position.y;
            }
        }

        dogNextPosition = dogPath.First;
        dog.transform.position = dogNextPosition.Value;

        StartCoroutine(MoveDog());
    }

    public void SetPlayerName() {
        string playerName = GameObject.Find("sockrates").GetComponent<Flowchart>().GetStringVariable("playerName");
        GameObject.Find("PlayerCharacter").GetComponent<Character>().NameText = playerName;
    }

    public void SetSockratesName() {
        GameObject.Find("SockratesPortrait").GetComponent<Character>().NameText = "Sockrates";
    }

    public void SetEndConditionMet() {
        int gothDisposition = GameObject.Find("goth").GetComponent<Flowchart>().GetIntegerVariable("gothDisposition");
        int fataleDisposition = GameObject.Find("fatale").GetComponent<Flowchart>().GetIntegerVariable("fataleDisposition");
        int loliDisposition = GameObject.Find("loli").GetComponent<Flowchart>().GetIntegerVariable("loliDisposition");
        int jockDisposition = GameObject.Find("jock").GetComponent<Flowchart>().GetIntegerVariable("jockDisposition");

        bool endConditionMet = gothDisposition * fataleDisposition * loliDisposition * jockDisposition != 0;
        GameObject.Find("sockrates").GetComponent<Flowchart>().SetBooleanVariable("endConditionMet", endConditionMet);

        GameObject.Find("sockrates").GetComponent<Flowchart>().SetIntegerVariable("gothDisposition", gothDisposition);
        GameObject.Find("sockrates").GetComponent<Flowchart>().SetIntegerVariable("fataleDisposition", fataleDisposition);
        GameObject.Find("sockrates").GetComponent<Flowchart>().SetIntegerVariable("loliDisposition", loliDisposition);
        GameObject.Find("sockrates").GetComponent<Flowchart>().SetIntegerVariable("jockDisposition", jockDisposition);
    }

    private IEnumerator MoveDog() {
        yield return new WaitForSeconds(Random.Range(0f, 5f));

        if (dogNextPosition.Value.x > dog.transform.position.x) {
            dogRenderer.flipX = false;
        } else {
            dogRenderer.flipX = true;
        }


        float scaleFactor = (DOG_Y_MAX - dogNextPosition.Value.y) / (DOG_Y_MAX - DOG_Y_MIN);
        float newScale = DOG_SCALE_MIN + ((DOG_SCALE_MAX - DOG_SCALE_MIN) * scaleFactor);

        dog.transform.DOScale(newScale, 1);

        dogAnimator.SetBool("isMoving", true);
        dog.transform.DOMove(dogNextPosition.Value, Random.Range(1f, 2f)).OnComplete(() => {
            dogAnimator.SetBool("isMoving", false);

            if (dogNextPosition == dogPath.Last) {
                dogNextPosition = dogPath.First;
            } else {
                dogNextPosition = dogNextPosition.Next;
            }
            StartCoroutine(MoveDog());
        });
    }

    public void ShowDog() {
        dog.transform.position = Vector3.zero;
        dogRenderer.enabled = true;
    }

    public void HideDog() {
        dogRenderer.enabled = false;
    }
}
