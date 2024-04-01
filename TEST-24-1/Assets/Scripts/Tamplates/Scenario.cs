using Core;
using System.Collections.Generic;
using UnityEngine;

namespace Tamplates
{
    [CreateAssetMenu(fileName = "Scenario", menuName = "Scenario")]
    public class Scenario : ScriptableObject
    {
        public bool _isTimer = false;
        public int _timeGoal = 0;
        public List<StrInt> _goals = new();
    }
}
