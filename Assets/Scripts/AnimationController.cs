using Lascuela.Scripts.Interfaces;
using UnityEngine;

namespace Lascuela.Scripts
{
    public class AnimationController : MonoBehaviour, IAnimationController
    {
        [SerializeField]
        private Animator animator;

        public void PlayAnimation(string animationName)
        {
            animator.Play(animationName);
        }
    }
}