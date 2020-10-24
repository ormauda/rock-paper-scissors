using Assets.Scripts.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Classes
{
    public class AttacksBalancer
    {
        public static AttackType GetDominatorOf(AttackType attack)
        {
            switch (attack)
            {
                case AttackType.Rock: return AttackType.Paper;
                case AttackType.Paper: return AttackType.Scissors;
                case AttackType.Scissors: return AttackType.Rock;
                default: throw new ArgumentException($"The is no such type of attack: {attack}");
            }
        }
    }
}
