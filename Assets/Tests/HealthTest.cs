using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEditor;

namespace Tests
{
    public class HealthTest
    {
     
        [Test]
        public void HealthTestSimplePasses()
        {
            Lives lives = GameObject.Find("TxtLives").GetComponent<Lives>();
            Assert.GreaterOrEqual(lives.health, 0);
            int health = lives.health;
            lives.LooseLife();
            Assert.Greater(health, lives.health);
        }

    }
}
