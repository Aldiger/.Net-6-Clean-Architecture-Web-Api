using Microsoft.AspNetCore.Http;
using System.Runtime.Serialization;

namespace WebApi.Core.Common
{
    [DataContract(Name = "{0}")]
    public class Response<T>
    {
        #region | Ctor |

        public Response()
        {
            Messages = new Dictionary<string, string>();
        }

        #endregion

        #region | Properties |

        [DataMember]
        public ServiceResponseStatuses Status { get; set; }

        [DataMember]
        public T Data { get; set; }

        [DataMember]
        public Dictionary<string, string> Messages { get; set; }

        /// <summary>
        /// Response StatusCode => 200,404,500 vs.
        /// </summary>
        [DataMember]
        public int StatusCode { get; set; }

        #endregion

        #region | Helper Methods |

        public static Response<T> CreateResponse(T data, int statusCode)
        {
            var response = new Response<T> { Status = ServiceResponseStatuses.Success, Data = data, StatusCode = statusCode };
            return response;
        }

        /// <summary>
        /// To add Message to response
        /// </summary>
        /// <param name="messageKey"></param>
        /// <param name="message"></param>
        public void AddMessage(string messageKey, string message)
        {
            var alreadyExist = Messages.Any(eachMessage => string.Compare(eachMessage.Key, messageKey, StringComparison.OrdinalIgnoreCase) == 0);

            if (!alreadyExist)
                Messages.Add(messageKey, message);
        }

        public bool IsSuccessful()
        {
            StatusCode = StatusCodes.Status200OK;
            return Status == ServiceResponseStatuses.Success;
        }
        #endregion
    }
}
