using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FloatingDamagePooler : MonoBehaviour
{
    private List<FloatingDamage> pooler = new List<FloatingDamage>();
    private const int MAX_OBJ = 10;

    [SerializeField]
    private Canvas canvas;

    public void CreateOrUseObj(string text)
    {
        FloatingDamage fd;

        if (pooler.Count < MAX_OBJ)
        {
            var obj = (GameObject)GameObject.Instantiate(Resources.Load("Prefabs/FloatingDamage"));
            obj.transform.SetParent(this.canvas.transform, false);

            fd = obj.GetComponent<FloatingDamage>();
            pooler.Add(fd);
        }
        else
        {
            if (pooler.Where(f => f.gameObject.activeSelf).Count() == MAX_OBJ)
                return;

            fd = pooler.Where(f => !f.gameObject.activeSelf).First().GetComponent<FloatingDamage>();
            fd.gameObject.SetActive(true);
        }

        fd.transform.position = Camera.main.WorldToScreenPoint(this.transform.position);
        fd.TextDamage.text = text;
        fd.ShowMe();
    }
}
