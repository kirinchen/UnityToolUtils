using UnityEngine;
using System.Collections;
using static com.surfm.rest.URestApi;
using surfm.tool;

namespace com.surfm.rest {
    public class TypeResult<R> {

        public Result result { get; private set; }

        internal TypeResult(Result r) {
            result = r;
        }

        public R getBody() {
            return result.getBody<R>();
        }

        public override string ToString() {
            return CommUtils.toJson(this);
        }




    }
}
