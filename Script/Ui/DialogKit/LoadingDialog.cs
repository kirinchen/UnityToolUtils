using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace surfm.tool {
    public class LoadingDialog : DialogKit.Dialog {

        public string testT ;

        public override void show(bool b) {
            base.show(b);
            testT = Time.time + "";
        }
    }
}
