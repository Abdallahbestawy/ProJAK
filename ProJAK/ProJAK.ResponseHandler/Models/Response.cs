using ProJAK.ResponseHandler.Consts;

namespace ProJAK.ResponseHandler.Models
{
    public class Response<T>
    {
        public int StatusCode { get; set; }
        public bool Succeeded { get; set; }
        public string? Message { get; set; }
        public List<string>? Errors { get; set; }
        public T? Data { get; set; }

        public static Response<T> CreateResponse(ResponseType responseType, string? message, List<string>? errors, T? data)
        {
            return new Response<T>
            {
                StatusCode = (int)responseType,
                Succeeded = IsSuccessResponse(responseType),
                Message = message,
                Errors = errors,
                Data = data
            };
        }

        private static bool IsSuccessResponse(ResponseType responseType)
        {
            return responseType == ResponseType.Success ||
                   responseType == ResponseType.Created ||
                   responseType == ResponseType.NoContent;
        }

        public static Response<T> Success(T data, string? message = null)
        {
            return CreateResponse(ResponseType.Success, message, null, data);
        }

        public static Response<T> Created(string? message = null)
        {
            return CreateResponse(ResponseType.Created, message, null, default);
        }

        public static Response<T> Deleted(string? message = null)
        {
            return CreateResponse(ResponseType.Success, message, null, default);
        }

        public static Response<T> Updated(string? message = null)
        {
            return CreateResponse(ResponseType.Success, message, null, default);
        }

        public static Response<T> NoContent(string? message = null)
        {
            return CreateResponse(ResponseType.NoContent, message, null, default);
        }

        public static Response<T> BadRequest(string? message = null, List<string>? errors = null)
        {
            return CreateResponse(ResponseType.BadRequest, message, errors, default);
        }

        public static Response<T> ServerError(string? message = null, List<string>? errors = null)
        {
            return CreateResponse(ResponseType.InternalServerError, message, errors, default);
        }
    }
}
