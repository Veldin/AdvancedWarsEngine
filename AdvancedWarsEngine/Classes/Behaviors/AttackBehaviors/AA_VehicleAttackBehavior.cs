﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdvancedWarsEngine.Classes.Enums;

namespace AdvancedWarsEngine.Classes
{
    class AA_VehicleAttackBehavior : IAttackBehavior
    {
        public float Attack(Unit unit, GameObject gameObject)
        {
            // Setting some local variables
            float baseValue     = 30;                       // The baseValue
            float attackValue   = baseValue;                // The total attack value which will be returned

            // Check if the attacked gameObject is a Unit
            if (gameObject is Unit)
            {
                // Set the gameObject as Unit
                Unit enemyUnit = gameObject as Unit;

                // Check if the unitType is Air. If so increase attackValue by 50%
                if (enemyUnit.UnitType == EUnitType.Air)
                {
                    attackValue += baseValue * 0.5f; 
                }
            }

            // Return the attackValue
            return attackValue;
        }
    }
}
