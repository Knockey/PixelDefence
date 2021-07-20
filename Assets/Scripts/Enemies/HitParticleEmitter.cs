using UnityEngine;

public class HitParticleEmitter : MonoBehaviour
{
    [SerializeField] private ParticleSystem _hitParticlePrefab;
    [SerializeField] private Transform _paricleSystemPlace;

    public void EmitParticle()
    {
        var hitParticle = Instantiate(_hitParticlePrefab, _paricleSystemPlace.position, _hitParticlePrefab.transform.rotation);
        hitParticle.Play();
    }
}
