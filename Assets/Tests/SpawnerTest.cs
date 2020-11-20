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
        public void Test()
        {



        }




        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator KeyTestWithEnumeratorPasses()
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
            //Spawn a clone of the item
            spawn.Spawn(go.transform.position, go.transform.rotation);
            //Confirm they are different objects
            Assert.AreNotEqual(toDestroy, spawn.CurrentObject);

            Object.DestroyImmediate(spawn.CurrentObject);
            yield return null;
            Assert.AreEqual(null ,spawn.CurrentObject);
            yield return null;


        }
    }
}
