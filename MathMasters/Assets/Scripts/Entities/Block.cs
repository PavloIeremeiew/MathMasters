using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MathMasters.Entities
{
    [CreateAssetMenu(fileName = "Block", menuName = "ScriptableObjects/Block")]
    public class Block : ScriptableObject
    {
        public Level[] Levels;
    }
}
