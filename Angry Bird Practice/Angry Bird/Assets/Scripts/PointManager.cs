using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PointManager : MonoBehaviour
{
    public int totalPoints = 0;
    public TextMeshProUGUI pointText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddPoints(int adj)
    {
        totalPoints += adj;
        pointText.text = "points:"+totalPoints.ToString();
    }
}
