using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace com.surfm.rest {
    public class RestException : Exception {

        public RestException(string message)  : base(message) { }


    }
}
