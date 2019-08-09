using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
namespace surfm.tool {
    public static class ObservableUtils {

        public static IObservable<Unit> actionGen(this Action action) {
            return Observable.FromEvent<Action>(h => { return h; }, h => action += h, h => action -= (h)); ;
        }

    }
}
