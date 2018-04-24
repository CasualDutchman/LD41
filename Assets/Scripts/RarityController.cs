using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RarityController : MonoBehaviour {

    public static RarityController instance;

    //private int weigthCommon = 50, weigthUncommon = 25, weigthRare = 13, weigthEpic = 9, weigthLegendary = 3;
    public int[] weigths = new int[] { 3, 9, 13, 25, 50 };

    public float[] strengthRarity;
    public float[] healthRarity;

    public Material matCommon, matUncommon, matRare, matEpic, matLegendary;

    public Tool[] toolsCommon, toolsUncommon, toolsRare, toolsEpic, toolsLegendary;
    public Enemy[] enemiesCommon, enemiesUncommon, enemiesRare, enemiesEpic, enemiesLegendary;

    void Awake () {
        instance = this;
	}

    public Tool GetRarityTool() {
        int i = GetRarity();
        switch (i) {
            default: case 0: return toolsCommon[Random.Range(0, toolsCommon.Length)];
            case 1: return toolsUncommon[Random.Range(0, toolsUncommon.Length)];
            case 2: return toolsRare[Random.Range(0, toolsRare.Length)];
            case 3: return toolsEpic[Random.Range(0, toolsEpic.Length)];
            case 4: return toolsLegendary[Random.Range(0, toolsLegendary.Length)];
        }
    }

    public Enemy GetRarityEnemy() {
        int i = GetRarity();
        switch (i) {
            default: case 0: return enemiesCommon[Random.Range(0, enemiesCommon.Length)];
            case 1: return enemiesUncommon[Random.Range(0, enemiesUncommon.Length)];
            case 2: return enemiesRare[Random.Range(0, enemiesRare.Length)];
            case 3: return enemiesEpic[Random.Range(0, enemiesEpic.Length)];
            case 4: return enemiesLegendary[Random.Range(0, enemiesLegendary.Length)];
        }
    }

    int GetRarity() {
        int range = 0;
        for (int i = 0; i < weigths.Length; i++) {
            range += weigths[i];
        }

        int rand = Random.Range(0, range);
        int test = 0;

        for (int i = 0; i < weigths.Length; i++) {
            test += weigths[i];
            if (rand < test) {
                return weigths.Length - 1 - i;
            }
        }

        return 0;
    }

    public Material GetRarityMaterial(Rarity r) {
        switch (r) {
            case Rarity.Common: return matCommon;
            case Rarity.Uncommon: return matUncommon;
            case Rarity.Rare: return matRare;
            case Rarity.Epic: return matEpic;
            case Rarity.Legendary: return matLegendary;
        }
        return matCommon;
    }

    public float GetRarityStrength(Rarity r) {
        switch (r) {
            default:  case Rarity.Common: return strengthRarity[0];
            case Rarity.Uncommon: return strengthRarity[1];
            case Rarity.Rare: return strengthRarity[2];
            case Rarity.Epic: return strengthRarity[3];
            case Rarity.Legendary: return strengthRarity[4];
        }
    }

    public float GetRarityHealth(Rarity r) {
        switch (r) {
            default: case Rarity.Common: return healthRarity[0];
            case Rarity.Uncommon: return healthRarity[1];
            case Rarity.Rare: return healthRarity[2];
            case Rarity.Epic: return healthRarity[3];
            case Rarity.Legendary: return healthRarity[4];
        }
    }
}

public enum Rarity { Common, Uncommon, Rare, Epic, Legendary }
