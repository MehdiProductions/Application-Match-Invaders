using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class GameManagerTest
    {
  
        [Test]
        public void GameManagerTestSimplePasses()
        {
            GameManager gamemanager = GameObject.Find("GameManager").GetComponent<GameManager>();
            GameObject LevelManager = GameObject.Find("LevelManager");
            GameObject Player = GameObject.Find("Player");
            gamemanager.RestartGame();
            Assert.NotNull(LevelManager);
            Assert.NotNull(Player);
            
        }

      
        [UnityTest]
        public IEnumerator GameManagerTestWithEnumeratorPasses()
        {
          
            

            yield return null;
        }
    }
}
