using BestHTTP;
using surfm.tool;
using System;
namespace com.surfm.rest {
    public class RestException : Exception {

        public HTTPResponse response { get; internal set; }
        public Exception parseDtoException { get; internal set; }
        public RestFailDto dto { get; internal set; }

        public RestException(HTTPResponse r, string message) : base(message) {
            response = r;
            dto = getFailDto();
        }

        public RestFailDto getFailDto() {
            try {
                return (RestFailDto)CommUtils.convertByJson(response.DataAsText, typeof(RestFailDto));
            } catch (Exception e) {
                parseDtoException = e;
                return null;
            }
        }


    }
}
