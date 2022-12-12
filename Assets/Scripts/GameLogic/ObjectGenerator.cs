using System;
using System.Collections;
using DefaultNamespace;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObjectGenerator : MonoBehaviour
{
    [SerializeField] private PushableObject FoodObject;
    [SerializeField] private PushableObject EnemyObject;
    [Range(0, 1)] [SerializeField] private float FoodToEnemySpawnRate;
    [Range(0.001f, 10f)] [SerializeField] private float GenerationSpeed;
    [SerializeField] private Transform SpawnPoint;
    [SerializeField] private Vector2 JumpVector;
    [Range(0, 1)] [SerializeField] private float JumpChance;
    [SerializeField] private float JumpForce;
    [SerializeField] private int OrderPosition;
    [SerializeField] private Transform GarbageTransform;

    private void OnEnable()
    {
        StopAllCoroutines();
        StartCoroutine(Spawn());
    }

    public IEnumerator Spawn()
    {
        yield return new WaitForSeconds((1.3f / GenerationSpeed) * OrderPosition);
        while (true)
        {
            GenerateObject(Random.value < FoodToEnemySpawnRate ? FoodObject : EnemyObject);
            yield return new WaitForSeconds(1 / GenerationSpeed);
        }
    }

    public void GenerateObject(PushableObject gameObject)
    {
        var obj = Instantiate(gameObject, SpawnPoint.position, Quaternion.identity, GarbageTransform);
        if (Random.value < JumpChance)
        {
            obj.Rigidbody2D.AddForce(JumpVector.normalized * JumpForce);
        }
    }
}