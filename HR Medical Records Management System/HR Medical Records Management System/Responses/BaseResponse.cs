namespace HR_Medical_Records_Management_System.Responses
{
    public class BaseResponse<T>
    {
        public bool? Success { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }
        public int? Code { get; set; }
        public int? TotalRows { get; set; }
        public string? Exception { get; set; }

        //constructor without parameters
        private BaseResponse(BaseResponseBuilder builder)
        {
            Success = builder.Success;
            Message = builder.Message;
            Data = builder.Data;
            Code = builder.Code;
            TotalRows = builder.TotalRows;
            Exception = builder.Exception;
        }

        public class BaseResponseBuilder
        {
            public bool? Success { get; private set; }
            public string? Message { get; private set;}
            public T? Data { get; private set; }
            public int? Code { get; private set; }
            public int? TotalRows { get; private set; }
            public string? Exception { get; private set; }


            public BaseResponseBuilder SetSuccess(bool? success)
            {
                Success = success;
                return this;
            }

            public BaseResponseBuilder SetMessage(string? message)
            {
                Message = message;
                return this;
            }

            public BaseResponseBuilder SetData(T? data)
            {
                Data = data;
                return this;
            }

            public BaseResponseBuilder SetCode(int? code)
            {
                Code = code;
                return this;
            }

            public BaseResponseBuilder SetTotalRows(int? totalRows)
            {
                TotalRows = totalRows;
                return this;
            }

            public BaseResponseBuilder SetException(string? exception)
            {
                Exception = exception;
                return this;
            }

            public BaseResponse<T> Build() => new BaseResponse<T>(this);

            public static BaseResponseBuilder CreateBuilder() => new BaseResponseBuilder();

            public static BaseResponse<T> SuccessResponse(T data, int totalRows = 0) =>
                CreateBuilder()
                .SetSuccess(true)
                .SetMessage("Successful request")
                .SetData(data)
                .SetCode(200)
                .SetTotalRows(totalRows)
                .Build();

            public static BaseResponse<T> ErrorResponse(string exception) =>
                CreateBuilder()
                .SetSuccess(false)
                .SetMessage("Internal Server Error: Unhandled errors")
                .SetCode(500)
                .SetException(exception)
                .Build();

            public static BaseResponse<T> NotFoundResponse(string exception) =>
                CreateBuilder()
                .SetSuccess(false)
                .SetMessage("Not Found: Resource not found")
                .SetCode(404)
                .Build();


            public static BaseResponse<T> BadRequestResponse(string exception) =>
                CreateBuilder()
                .SetSuccess(false)
                .SetMessage("Bad Request: Invalid request")
                .SetCode(400)
                .SetException(exception)
                .Build();

        }


    }
}
