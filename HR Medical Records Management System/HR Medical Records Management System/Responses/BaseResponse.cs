namespace HR_Medical_Records_Management_System.Responses
{
    public class BaseResponse<T>
    {
        public bool? Success { get; set; }
        public string? Message { get; set; }
        public List<T?> Data { get; set; }
        public int? Code { get; set; }
        public int? TotalRows { get; set; }
        public string? Exception { get; set; }

        //constructor without parameters
        public BaseResponse(){}

        //constructor for success response
        public BaseResponse(List<T> data, string message, int code, int totalRows)
        {
            Success= true;
            Data= data;
            Message= message;
            Code= code;
            TotalRows= totalRows;
        }
        //constructor for Unsuccess response
        public BaseResponse(string message, int code, string exception)
        {
            Success=false;
            Message=message;
            Code=code;
            Exception=exception;
        }

    }
}
