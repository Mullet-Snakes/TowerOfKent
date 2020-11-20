using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class SpawnerTest
    {
        [Test]
        public void TestSpawnnerSpawning()
        {
            //Add new gameobject
            GameObject go = new GameObject();
            //Attach Spawner component
            Spawner spawn = go.AddComponent<Spawner>();
            //Add gameobject to be spawned
            GameObject toDestroy = new GameObject();
            //Make it the item to spawn
            spawn.itemToSpawn = toDestroy;
            //Check they are equal
            Assert.AreEqual(spawn.itemToSpawn, toDestroy);
            //Check current object is null
            Assert.IsNull(spawn.CurrentObject);
            //Spawn a clone of the item
            spawn.Spawn(go.transform.position, go.transform.rotation);
            //Confirm they are different objects
            Assert.AreNotEqual(toDestroy, spawn.CurrentObject);
            //Confirm the current object is not null;
            Assert.NotNull(spawn.CurrentObject);

        }




        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator KeyTestWithEnumeratorPasses()
        {
           
            yield return null;

        }
    }
}
