
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using UnityEngine;

public class TerainGenerator : MonoBehaviour
{
    
    public Bloks Ground;
    public Bloks Mud;
    public Transform zero;
    public int Height, Width;
    public Gamer gamer;
    bool spawner = false;
    public Tree tree;
    public Bloks stone;
    public Tree appleTree;
    public Things apple;
    public Patroler patroler;
    public Obstacle obstacle;




    private void Start()
    {
        Generate();
    }
    public void Generate()
    {
        
        
        int y = 20;
        for (int i = 0; i < Width; i++)
        {
          
            
            if (i % Random.Range(1,6) == 0)
            {
                y += Random.Range(-1, 2);
            }
            for(int j = y; j > 0; j--)
            {
                if (y==j)
                {
                    if (Random.Range(0, 100) % 7 == 0)
                    {      
                        var cell = Instantiate(tree.itemPrefab, zero);
                        cell.transform.localPosition = new Vector3(i, y + 1, 0);
                     
                        
                    }
                    if (Random.Range(0, 1000) % 50  == 0)
                    {
                        {
                            var cell = Instantiate(appleTree.itemPrefab, zero);
                            cell.transform.localPosition = new Vector3(i, y + 1, 0);
                            for(int k = 0; k < Random.Range(1, 6);k++)
                            {
                                var tempPatrol = Instantiate(patroler.gameObject);
                                tempPatrol.transform.localPosition = new Vector3(i + k, y + 1, 0);
                               var tempObject= Instantiate(apple.itemPrefab,zero);
                               tempObject.transform.localPosition = new Vector3(i + k, y + 1, 0);
                            }
                        }
                    }
                    if((Random.Range(0, Width) % 20 == 0))
                    {
                        var tempObstacle = Instantiate(obstacle);
                        obstacle.transform.localPosition = new Vector3(i, y + 1, 0);
                    }
                }
                if (j > y - 1)
                {
                    var cell = Instantiate(Ground.transform.gameObject, zero);

                    cell.transform.localPosition = new Vector3(i, j, 0);
                    if (!spawner)
                    {
                        gamer.transform.localPosition = new Vector3(Random.Range(0, Width), j, 0);
                    }
                    
                }
                else if (j > y - 5 )
                {
                    var cell = Instantiate(Mud.transform.gameObject, zero);
                    cell.transform.localPosition = new Vector3(i, j, 0);
                }
                else
                {
                    
                   
                    var cell = Instantiate(stone.transform.gameObject, zero);
                    cell.transform.localPosition = new Vector3(i, j, 0);
                }
               
            }

        }
        
    }

   
}