using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class SlotController : MonoBehaviour
{

    public Transform[] reels;
    public Image[] slotIcons;
    public List<Col> slotMatix;
    public Sprite blank;

    public Button spinButton;
    public Button autoSpinButton;
    public bool isAutoSpin = false;

    void Start()
    {
        RandomizeReel();
        spinButton.onClick.AddListener(() => StartCoroutine(SpinLoop()));
        autoSpinButton.onClick.AddListener(() => StartCoroutine(AutoSpinLoop()));
    }
    IEnumerator SpinLoop()
    {
        ToggleBtnGrp(false);
        InititateSlot();
        PlayerData.instance.Updatebalance(-BetController.betAmount);

        yield return new WaitForSeconds(0.5f);
        RandomizeReel();
        yield return new WaitForSeconds(2.5f);
        PopulateSlot();
        yield return StopSlot();
        ToggleBtnGrp(true);
    }

    IEnumerator AutoSpinLoop()
    {
        if (isAutoSpin) yield break;
        isAutoSpin = true;

        while (isAutoSpin)
        {
            ToggleBtnGrp(false);
            PlayerData.instance.Updatebalance(-BetController.betAmount);

            InititateSlot();
            yield return new WaitForSeconds(0.5f);
            RandomizeReel();
            yield return new WaitForSeconds(2.5f);
            PopulateSlot();
            yield return StopSlot();
            yield return new WaitForSeconds(2f);
        }
        ToggleBtnGrp(true);
        isAutoSpin = false;

    }
    void InititateSlot()
    {
        foreach (var reel in reels)
        {
            reel.transform.localPosition = new Vector2(reel.transform.localPosition.x, 560);
            reel.DOLocalMoveY(-560, 0.35f).SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear);
        }
        RandomizeReel();

    }

    void PopulateSlot()
    {
        List<int> resutlt = GenerateRandomResult();

        for (int i = 0; i < resutlt.Count; i++)
        {

            if (resutlt[i] == 0)
            {
                slotMatix[i].row[1].img.sprite = blank;
                slotMatix[i].row[0].img.sprite = null;
                slotMatix[i].row[2].img.sprite = null;
                Debug.Log("entered here" + i);
            }
            else
            {
                slotMatix[i].row[0].img.sprite = blank;
                slotMatix[i].row[2].img.sprite = blank;
                slotMatix[i].row[1].img.sprite = null;

            }

        }
    }

    IEnumerator StopSlot()
    {
        foreach (var reel in reels)
        {
            reel.DOKill();
            reel.transform.localPosition = new Vector2(reel.transform.localPosition.x, 560);
            reel.DOLocalMoveY(0, 0.5f).SetEase(Ease.Linear);
            yield return new WaitForSeconds(0.25f);
        }
        yield return new WaitForSeconds(0.25f);

    }
    void RandomizeReel()
    {
        int count = 0;
        foreach (var item in slotIcons)
        {
            if (count % 2 == 0) item.sprite = blank;
            else
                item.color = Random.ColorHSV();
            count++;
        }
    }

    List<int> GenerateRandomResult()
    {
        List<int> result = new List<int>();

        result.Add(Random.Range(0, 2));
        result.Add(Random.Range(0, 2));
        result.Add(Random.Range(0, 2));
        result.Add(Random.Range(0, 2));

        return result;
    }

    void ToggleBtnGrp(bool toggle)
    {

        spinButton.interactable = toggle;
        autoSpinButton.interactable = toggle;
    }
}

[System.Serializable]
public class Col
{
    public List<SlotIcon> row = new List<SlotIcon>();
}