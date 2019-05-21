using UnityEngine;
using System.Collections;
using static com.surfm.rest.URestApi;

namespace com.surfm.rest {
    public class TypeResult<R> {

        public Result result { get; private set; }

        internal TypeResult(Result r) {
            result = r;
        }

        public R getBody() {
            return result.getBody<R>();
        }




    }
}
