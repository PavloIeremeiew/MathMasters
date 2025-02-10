using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MathMasters.Entities
{
    [CreateAssetMenu(fileName = "NewLevel", menuName = "ScriptableObjects/Level")]
    public class Level : ScriptableObject
    {
        public Question[] Questions;
    }
}
