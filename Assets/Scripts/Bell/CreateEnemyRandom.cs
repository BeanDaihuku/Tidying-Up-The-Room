using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateEnemyRandom : MonoBehaviour
{
    [SerializeField] GameObject nabe;
    [SerializeField] GameObject enemy;
    [SerializeField] float r_min = 15;
    [SerializeField] float r_max = 25;

    private float time;
    private Vector3 vector_nabe;

    void Update()
    {
        /*ベクトル正規化(normalized)のせいで第一象限しかスポーンしなくなったので
          ランダムで-1掛けて全方位にした
          でもそのせいでx軸y軸付近のスポーン率がめっちゃ低いので
          ベクトルの360°からランダムに選ぶみたいなのに直したい*/

        time += Time.deltaTime;
        if(time > 1.0f)
        {
            Vector3 center = nabe.transform.position;
            float r = Random.Range(r_min, r_max);
            Vector3 vec = new Vector3(Random.Range(0.1f, 1.0f), 0, Random.Range(0.1f, 1.0f)).normalized * r;
            Vector3 spawnpoint = center + vec + new Vector3(0, 1, 0);
            int number = Random.Range(0, 2);
            if (number == 0)
            {
                spawnpoint.x *= -1;
            }
            number = Random.Range(0, 2);
            if (number == 0)
            {
                spawnpoint.z *= -1;
            }

            //Debug.Log("spawnpoint : " + spawnpoint);

            Instantiate(enemy, spawnpoint, Quaternion.identity);
            
            time = 0f;
        }
    }
}
