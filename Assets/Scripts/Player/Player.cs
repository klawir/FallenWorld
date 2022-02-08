using UnityEngine;
using Zenject;

namespace Game.Player
{
    public class Player : Actor, IPlayer
    {
        [SerializeField] private Combat combat;
        [SerializeField] private Navigation navigation;

        public Combat Combat { get; private set; }
        public Navigation Navigation { get; private set; }
        public ITarget ITarget { get; private set; }

        [Inject]
        public void Construct()
        {
            Combat = combat;
            Navigation = navigation;
        }

        public override void DealDamage()
        {
            ITarget.GetDamage(damage);
        }

        public override void UpdateTarget(Transform target)
        {
            Combat.UpdateTarget(target);
            Navigation.UpdateTarget(target);
            Combat.SetDistance();
        }

        public void UpdateTarget(Vector3 newPosition)
        {
            Navigation.UpdateTarget(newPosition);
        }

        public void SetTarget(Transform transformTarget)
        {
            ITarget = transformTarget.GetComponent<ITarget>();
        }
    }
}