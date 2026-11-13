using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoadoutUI : MonoBehaviour
{
    [SerializeField] GameObject[] jetpacks;
    [SerializeField] TMP_Text nameLabel, statLabel;
    [SerializeField] Button prevButton, nextButton;
    [SerializeField] Transform previewImage;

    int index;

    void OnEnable()
    {
        index = GameData.Equipped;
        Show();
    }

    public void Next() => Step(1);

    public void Prev() => Step(-1);

    void Step(int dir)
    {
        int n = jetpacks.Length;

        for (int i = 1; i <= n; i++)
        {
            int c = ((index + dir * i) % n + n) % n;

            if (GameData.Owns(c))
            {
                index = c;
                break;
            }
        }

        GameData.Equipped = index;

        Show();

        Fx.Punch(this, previewImage, 0.06f);
        Fx.Punch(this, nameLabel.transform, 0.12f);
    }

    void Show()
    {
        for (int i = 0; i < jetpacks.Length; i++)
        {
            jetpacks[i].SetActive(i == index);
        }

        nameLabel.SetText(JetpackInfo.Names[index]);
        statLabel.SetText(JetpackInfo.Stats[index]);

        int owned = 0;

        for (int i = 0; i < jetpacks.Length; i++)
        {
            if (GameData.Owns(i))
                owned++;
        }

        prevButton.interactable = nextButton.interactable = owned > 1;
    }
}