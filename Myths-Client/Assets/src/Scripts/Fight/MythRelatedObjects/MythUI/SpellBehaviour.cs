using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellBehaviour : MonoBehaviour
{
    #region Attributes
    private Spell linkedSpell;
    
    public UnityEngine.UI.Image icon;
    public UnityEngine.UI.Text energyTag;
    public GameObject toolTip;
    private bool isMouseOver;
    private float timer;
    #endregion

    #region Getters & Setters
    public Spell LinkedSpell { get => linkedSpell; set => linkedSpell = value; }

    #endregion

    #region Unity Methods
    public void Awake()
    {
        isMouseOver = false;
        timer = 0;
        toolTip.SetActive(false);
    }
    public void Update()
    {
        if(isMouseOver)
        {
            if(timer>0.5f)
            {
                ShowToolTip();
            }
            else
            {
                timer += Time.deltaTime;
            }
        }
    }
    #endregion

    #region Display & Init Methods

    public void Init(Spell spell)
    {
        linkedSpell = spell;
        UpdateIcon();
        UpdateEnergy();
    }

    public void UpdateIcon()
    {
        var newIcon = Resources.Load<Sprite>("Spells/Icons/" + linkedSpell.Id);
        //TODO Exception handling
        this.icon.sprite = newIcon;
    }

    public void UpdateEnergy()
    {
        int spellCost = Mathf.Max(0, linkedSpell.EnergyCost - GameManager.gm.GetCostReduction(linkedSpell.Element));
        energyTag.text = spellCost.ToString();
    }

    public void ShowToolTip()
    {
        UpdateToolTipText();
        toolTip.SetActive(true);
    }

    public void UpdateToolTipText()
    {
        string element = "Neutre";
        switch(linkedSpell.Element)
        {
            case Mastery.arcane:
                element = "Arcane";
                break;
            case Mastery.light:
                element = "Lumiere";
                break;
            case Mastery.dark:
                element = "Tenebre";
                break;
            case Mastery.fire:
                element = "Feu";
                break;
            case Mastery.earth:
                element = "Terre";
                break;
            case Mastery.air:
                element = "Air";
                break;
            case Mastery.water:
                element = "Eau";
                break;

        }
        string text = linkedSpell.Name
            + "\nEnergie : " + linkedSpell.EnergyCost
            + "\nPortee : " + linkedSpell.MinRange + "-" + linkedSpell.MaxRange
            +"\nElement : "+element
            + "\n" + linkedSpell.Description;
        toolTip.GetComponentInChildren<UnityEngine.UI.Text>().text = text;
    }

    public void HideToolTip()
    {
        toolTip.SetActive(false);
    }
    #endregion

    #region Control Methods
    public void OnDrag()
    {
        GameManager.gm.selectedUnit = linkedSpell.Owner;
        GameManager.gm.selectedSpell = linkedSpell;
        TileBehaviour.UpdateCastingTiles(linkedSpell.Owner, linkedSpell);

    }

    public void OnDrop()
    {
        if (GameManager.gm.selectedTile != null &&
            GameManager.gm.selectedTile.currentTexture == GameManager.gm.selectedTile.textureTileCasting &&
            GameManager.gm.selectedUnit.Stats[Stat.energy] >=
            Mathf.Max(linkedSpell.EnergyCost - GameManager.gm.GetCostReduction(linkedSpell.Element)))
        {
            Debug.Log("Casting " + GameManager.gm.selectedSpell.Name + " on tile " + GameManager.gm.selectedTile.x
                    + " " + GameManager.gm.selectedTile.y);
            if (Utils.LineOfSight3(GameManager.gm.selectedUnit.Stats[Stat.x],
                GameManager.gm.selectedUnit.Stats[Stat.y],
                GameManager.gm.selectedTile.x,
                GameManager.gm.selectedTile.y))
            {

                Debug.Log("On LOS");
                Server.SendMessageToServer(new CastSpellMessage(linkedSpell.Owner.Id, linkedSpell.Id,
                    GameManager.gm.selectedTile.x, GameManager.gm.selectedTile.y));
            }
            else
            {
                Debug.Log("Hors LOS");
            }
            
        }
        TileBehaviour.ResetTiles();
    }

    public void OnPointerEnter()
    {
        timer = 0;
        isMouseOver = true;
    }

    public void OnPointerExit()
    {
        timer = 0;
        isMouseOver = false;
        toolTip.SetActive(false);
    }
    #endregion
}
