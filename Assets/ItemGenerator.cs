using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ItemGenerator : MonoBehaviour
{
    //carPrefabを入れる
    public GameObject carPrefab;
    //coinPrefabを入れる
    public GameObject coinPrefab;
    //cornPrefabを入れる
    public GameObject conePrefab;
    //スタート地点
    private int startPos = -160;
    //ゴール地点
    private int goalPos = 120;
    //アイテムを出すx方向の範囲
    private float posRange = 3.4f;

    //メインカメラ
    private GameObject mainCamera;


    //オブジェクト破棄
    public int main = 0;
    public int car = 0;
    public int cone = 0;
    public int coin = 0;

    //生成したObjectを持っておくためのList
    List<GameObject> coin_prefab = new List<GameObject>();
    List<GameObject> cone_prefab = new List<GameObject>();
    List<GameObject> car_prefab = new List<GameObject>();

    // Use this for initialization
    void Start()
    {
        //一定の距離ごとにアイテムを生成
        for (int i = startPos; i < goalPos; i += 15)
        {
            //どのアイテムを出すのかをランダムに設定
            int num = Random.Range(1, 11);
            if (num <= 2)
            {
                //コーンをx軸方向に一直線に生成
                for (float j = -1; j <= 1; j += 0.4f)
                {
                    GameObject cone = Instantiate(conePrefab) as GameObject;
                    cone.transform.position = new Vector3(4 * j, cone.transform.position.y, i);

                    //生成したインスタンスをリストで持っておく
                    cone_prefab.Add(cone);
                }
            }
            else
            {

                //レーンごとにアイテムを生成
                for (int j = -1; j <= 1; j++)
                {
                    //アイテムの種類を決める
                    int item = Random.Range(1, 11);
                    //アイテムを置くZ座標のオフセットをランダムに設定
                    int offsetZ = Random.Range(-5, 6);
                    //60%コイン配置:30%車配置:10%何もなし
                    if (1 <= item && item <= 6)
                    {
                        //コインを生成
                        GameObject coin = Instantiate(coinPrefab) as GameObject;
                        coin.transform.position = new Vector3(posRange * j, coin.transform.position.y, i + offsetZ);

                        //生成したインスタンスをリストで持っておく
                        coin_prefab.Add(coin);
                    }
                    else if (7 <= item && item <= 9)
                    {
                        //車を生成
                        GameObject car = Instantiate(carPrefab) as GameObject;
                        car.transform.position = new Vector3(posRange * j, car.transform.position.y, i + offsetZ);

                        //生成したインスタンスをリストで持っておく
                        car_prefab.Add(car);
                    }
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        this.mainCamera = GameObject.Find("Main Camera");
        // メインカメラz座標
        main = (int)mainCamera.transform.position.z;
        
        for (int i = 0; i < car_prefab.Count; i++) {
            if (this.car_prefab[i] != null)
            {
                // 車z座標
                car = int.Parse(this.car_prefab[i].transform.position.z.ToString());
                if (main > car)
                {
                    Destroy(car_prefab[i]);
                }
            }
        }
        for (int j = 0; j < cone_prefab.Count; j++){
            if (this.cone_prefab[j] != null)
            {
                // コーンz座標
                cone = int.Parse(this.cone_prefab[j].transform.position.z.ToString());
                if (main > cone)
                {
                    Destroy(cone_prefab[j]);
                }
            }
        }
        for (int k = 0; k < coin_prefab.Count; k++){
            if (this.coin_prefab[k] != null)
            {
                // コインz座標
                coin = int.Parse(this.coin_prefab[k].transform.position.z.ToString());
                if (main > coin)
                {
                    Destroy(coin_prefab[k]);
                }
            }
        }
    }
}