using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

enum Block
{
    red,
    white,
    inBlock,
    outBlock
}

public class HoleGame : MonoBehaviour
{
    [SerializeField] private GameObject redBlock;
    [SerializeField] private GameObject whiteBlock;
    [SerializeField] private GameObject whiteOutLine;
    [SerializeField] private Text roundText;
    [SerializeField] private Text timerText;

    private Block state;
    private int count = 5;
    private int round = 1;
    //private GameObject test;

    void Start()
    {
        StartCoroutine(GameUpdate());
    }

    IEnumerator GameUpdate()
    {
        while (true)
        {
            if (round > 5)
            {
                print("End 2Round");
                yield break;
            }

            roundText.text = round + "Round";
            RandBlock();
            yield return StartCoroutine(CountDown());

            switch(state)
            {
                case Block.red:
                    redBlock.SetActive(false);
                    yield return new WaitForSeconds(2f);
                    redBlock.SetActive(true);
                    break;
                case Block.white:
                    whiteBlock.SetActive(false);
                    whiteOutLine.SetActive(false);
                    yield return new WaitForSeconds(2f);
                    whiteBlock.SetActive(true);
                    whiteOutLine.SetActive(true);
                    break;
                case Block.inBlock:
                    redBlock.SetActive(false);
                    whiteBlock.SetActive(false);
                    yield return new WaitForSeconds(2f);
                    redBlock.SetActive(true);
                    whiteBlock.SetActive(true);
                    break;
                case Block.outBlock:
                    whiteOutLine.SetActive(false);
                    yield return new WaitForSeconds(2f);
                    whiteOutLine.SetActive(true);
                    break;
            }


            round++;
            yield return new WaitForSeconds(3f);
        }
    }

    IEnumerator CountDown()
    {
        while (true)
        {
            if (count == 0)
            {
                count = 5;
                yield break;
            }

            timerText.text = count.ToString();
            yield return new WaitForSeconds(1f);
            count--;
        }
    }

    void RandBlock()
    {
        int rand = Random.Range(0, 4);
        state = (Block)rand;
    }
}
