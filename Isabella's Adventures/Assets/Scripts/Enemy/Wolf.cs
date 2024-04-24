using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Wolf : MonoBehaviour
{
    [SerializeField] private List<ItemsData> _itemsDatas;
    [SerializeField] private ItemsData _experience;
    [SerializeField] private WolfDie _wolfDie;

    private ItemsData _itemsData;
    
    public float speed;
    public GameObject pl;
    Transform front, down;
    public bool isRight, tow, dow, canHurt;

    void Start()
    {
        pl = GameObject.FindGameObjectWithTag("Player");
        front = transform.GetChild(0);
        down = transform.GetChild(1);
        tow = false;
        canHurt = true;
    }
    
    void FixedUpdate()
    {
        if (canHurt == false)
        {
            GetComponent<SpriteRenderer>().color = Color.red;
        }
        else
        {
            GetComponent<SpriteRenderer>().color = Color.white;
        }
        tow = Physics2D.OverlapCircle(front.position, 0.2f, LayerMask.GetMask("Ground"));
        dow = Physics2D.OverlapCircle(down.position, 0.2f, LayerMask.GetMask("Ground"));
        if (tow == true || dow == false)
        {
            isRight = !isRight;
        }
        if (Physics2D.OverlapCircle(front.position, 0.1f, LayerMask.GetMask("Character")))
        {
            isRight = !isRight;
        }
        if (Vector2.Distance(pl.transform.position, transform.position) < 20)
        {
            if (isRight == true)
            {
                //GetComponent<SpriteRenderer>().flipX = false;
                transform.localScale = new Vector2(1, 1);
                transform.Translate(Vector2.right * speed / 100);
            }
            else
            {
                //GetComponent<SpriteRenderer>().flipX = true;
                transform.localScale = new Vector2(-1, 1);
                transform.Translate(-Vector2.right * speed / 100);
            }
        }
    }

    public GameObject ItemDrop()
    {
        int randomIndex = Random.Range(0, _itemsDatas.Count);
        ItemsData data = _itemsDatas[randomIndex];
        
        GameObject item= Instantiate(data.Prefab, transform.position, Quaternion.identity);
        item.GetComponent<Items>().GetItemId(data.TypeID);
        item.GetComponent<Items>().Count = data.Value;
        
        return item;
    }

    public GameObject Experience()
    {
        GameObject exp = Instantiate(_experience.Prefab, transform.position, Quaternion.identity);
        exp.GetComponent<Items>().GetItemId(_experience.TypeID);
        exp.GetComponent<Items>().Count = _experience.Value;
        return exp;
    }
}
