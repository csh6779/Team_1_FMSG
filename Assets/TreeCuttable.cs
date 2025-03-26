using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeCuttable : ToolHit //ToolHit은 나중에 도구를 이용한 상호작용을 위해 만듬
{
    //SerializeField로 설정해주면, 코드에서가 아닌 Unity안 컴포넌트에서 값을 수정할 수 있게 해줍니다.
    [SerializeField] GameObject pickUpDrop;
    [SerializeField] int dropCount = 5;
    [SerializeField] float spread = 0.7f;


    public override void Hit() //virtual값을 가지고 있어 override가능
    {
        while (dropCount > 0)
        {
            dropCount -= 1;

            Vector3 position = transform.position;
            position.x += spread * UnityEngine.Random.value - spread / 2;
            position.y += spread * UnityEngine.Random.value - spread / 2;
            GameObject go = Instantiate(pickUpDrop);
            go.transform.position = position;
        }
        Destroy(gameObject);
    }
}