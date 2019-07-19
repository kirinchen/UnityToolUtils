using DG.Tweening;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace surfm.tool {
    public class PlayInOut : MonoBehaviour {

        public enum AnimUid {
            MoveIn , MoveOut
        }
        public AnimUid currentAId = AnimUid.MoveOut;
        private List<DOTweenAnimation> dotAnimas = new List<DOTweenAnimation>();

        protected virtual void Awake() {
            dotAnimas.AddRange( GetComponents<DOTweenAnimation>());
            foreach (DOTweenAnimation da in dotAnimas) da.autoKill = false;
        }

        public void play(AnimUid uid) {
            if (currentAId == uid) return;
            currentAId = uid;
            findAnim(uid).ForEach(da=> da.DORestartById(uid.ToString()));
        }



        public List<DOTweenAnimation> findAnim(AnimUid aimUid) {
            return dotAnimas.Where(d => d.id.Equals(aimUid.ToString())).ToList();
        }



    }
}
                            