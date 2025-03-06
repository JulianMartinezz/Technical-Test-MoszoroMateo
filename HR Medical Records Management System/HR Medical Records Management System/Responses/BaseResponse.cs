namespace HR_Medical_Records_Management_System.Responses
{
    /// <summary>
    /// A generic class for representing the response of an API request, encapsulating the success status, 
    /// a message, data, HTTP status code, total rows, and any potential exception information.
    /// </summary>
    /// <typeparam name="T">The type of the data being returned in the response.</typeparam>
    public class BaseResponse<T>
    {
        public bool? Success { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }
        public int? Code { get; set; }
        public int? TotalRows { get; set; }
        public string? Exception { get; set; }


        
        private BaseResponse(BaseResponseBuilder builder)
        {
            Success = builder.Success;
            Message = builder.Message;
            Data = builder.Data;
            Code = builder.Code;
            TotalRows = builder.TotalRows;
            Exception = builder.Exception;
        }




        /// <summary>
        /// A builder class for constructing instances of BaseResponse<T> with customizable fields.
        /// </summary>
        public class BaseResponseBuilder
        {
            public bool? Success { get; private set; }
            public string? Message { get; private set; }
            public T? Data { get; private set; }
            public int? Code { get; private set; }
            public int? TotalRows { get; private set; }
            public string? Exception { get; private set; }


            /// <summary>
            /// Sets the success status of the response.
            /// </summary>
            /// <param name="success">A boolean indicating whether the request was successful.</param>
            /// <returns>The builder instance to allow method chaining.</returns>
            public BaseResponseBuilder SetSuccess(bool? success)
            {
                Success = success;
                return this;
            }


            /// <summary>
            /// Sets the message of the response, providing additional information about the request's outcome.
            /// </summary>
            /// <param name="message">The message to be included in the response.</param>
            /// <returns>The builder instance to allow method chaining.</returns>
            public BaseResponseBuilder SetMessage(string? message)
            {
                Message = message;
                return this;
            }


            /// <summary>
            /// Sets the data of the response, representing the payload returned from the request.
            /// </summary>
            /// <param name="data">The data to be returned in the response.</param>
            /// <returns>The builder instance to allow method chaining.</returns>
            public BaseResponseBuilder SetData(T? data)
            {
                Data = data;
                return this;
            }


            /// <summary>
            /// Sets the HTTP status code of the response, indicating the outcome of the request.
            /// </summary>
            /// <param name="code">The HTTP status code to be included in the response.</param>
            /// <returns>The builder instance to allow method chaining.</returns>
            public BaseResponseBuilder SetCode(int? code)
            {
                Code = code;
                return this;
            }


            /// <summary>
            /// Sets the total number of rows of the data, typically used in paginated responses.
            /// </summary>
            /// <param name="totalRows">The total number of rows matching the request criteria.</param>
            /// <returns>The builder instance to allow method chaining.</returns>
            public BaseResponseBuilder SetTotalRows(int? totalRows)
            {
                TotalRows = totalRows;
                return this;
            }


            /// <summary>
            /// Sets the exception message in case of an error, providing details about the issue that occurred.
            /// </summary>
            /// <param name="exception">The exception message to be included in the response.</param>
            /// <returns>The builder instance to allow method chaining.</returns>
            public BaseResponseBuilder SetException(string? exception)
            {
                Exception = exception;
                return this;
            }


            /// <summary>
            /// Builds the final BaseResponse<T> instance based on the set properties.
            /// </summary>
            /// <returns>The constructed BaseResponse<T> instance.</returns>
            public BaseResponse<T> Build() => new BaseResponse<T>(this);
        }

        /// <summary>
        /// Creates a new instance of the BaseResponseBuilder to start constructing a BaseResponse.
        /// </summary>
        /// <returns>A new BaseResponseBuilder instance.</returns>
        public static BaseResponseBuilder CreateBuilder() => new BaseResponseBuilder();


        /// <summary>
        /// Creates a successful response with the provided data and optional total rows count.
        /// </summary>
        /// <param name="data">The data to be included in the response.</param>
        /// <param name="totalRows">The total number of rows for paginated data (default is 0).</param>
        /// <returns>A BaseResponse<T> representing a successful request.</returns>
        public static BaseResponse<T> SuccessResponse(T data, int totalRows = 0) =>
            CreateBuilder()
            .SetSuccess(true)
            .SetMessage("Successful request")
            .SetData(data)
            .SetCode(200)
            .SetTotalRows(totalRows)
            .Build();


        /// <summary>
        /// Creates an error response indicating an internal server error.
        /// </summary>
        /// <param name="exception">The exception message providing details about the error.</param>
        /// <returns>A BaseResponse<T> representing an error response.</returns>
        public static BaseResponse<T> ErrorResponse(string exception) =>
            CreateBuilder()
            .SetSuccess(false)
            .SetMessage("Internal Server Error: Unhandled errors")
            .SetCode(500)
            .SetException(exception)
            .Build();


        /// <summary>
        /// Creates a response indicating that the requested resource was not found.
        /// </summary>
        /// <returns>A BaseResponse<T> representing a "Not Found" response.</returns>
        public static BaseResponse<T> NotFoundResponse() =>
            CreateBuilder()
            .SetSuccess(false)
            .SetMessage("Not Found: Resource not found")
            .SetCode(404)
            .Build();


        /// <summary>
        /// Creates a response indicating that the request was invalid or malformed.
        /// </summary>
        /// <param name="exception">The exception message providing details about the bad request.</param>
        /// <returns>A BaseResponse<T> representing a "Bad Request" response.</returns>
        public static BaseResponse<T> BadRequestResponse(string exception) =>
            CreateBuilder()
            .SetSuccess(false)
            .SetMessage("Bad Request: Invalid request")
            .SetCode(400)
            .SetException(exception)
            .Build();




    }
}
