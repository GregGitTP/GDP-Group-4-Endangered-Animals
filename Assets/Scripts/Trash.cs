using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : MonoBehaviour {
    public enum TrashType { Paper, Plastic, Metal, General };
    public TrashType type = TrashType.General;
}
